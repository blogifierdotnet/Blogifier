using Blogifier.Core.Data;
using Blogifier.Shared;
using Blogifier.Shared.Extensions;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Providers
{
	public interface IEmailProvider
	{
		Task<bool> SendEmail(List<Subscriber> subscribers, string subject, string content, string origin);
      Task<bool> SendVerificationEmail(Author account, string origin);
	}

	public class MailKitProvider : IEmailProvider
	{
      private readonly AppDbContext _db;
      private static string _salt;
		public MailKitProvider(AppDbContext db, IConfiguration configuration) 
      { 
         _db = db;
         _salt = configuration.GetSection("Blogifier").GetValue<string>("Salt");
      }

      private SmtpClient GetClient(MailSetting settings)
		{
			try
			{
            var client = new SmtpClient();
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            client.Connect(settings.Host, settings.Port, SecureSocketOptions.Auto);
            client.Authenticate(settings.UserEmail, settings.UserPassword);

            return client;
         }
			catch (Exception ex)
			{
            Serilog.Log.Error($"Error connecting to SMTP client: {ex.Message}");
            return null;
			}
         
      }

		public async Task<bool> SendEmail(List<Subscriber> subscribers, string subject, string content, string origin)
		{                 
         var settings = await _db.MailSettings.AsNoTracking().FirstOrDefaultAsync();
			if (settings == null || settings.Enabled == false)
				return false;

         var client = GetClient(settings);
         if (client == null)
            return false;

         

			foreach (var subscriber in subscribers)
			{
            
            var token = TokenHandling.GenerateToken(_salt, subscriber.Email);
            var unsubscribe = 
$@"
<br/><br/><br/><br/><br/>
You received the above message because you have subscribed to our newsletter.
<br/><br/>
To unsubscribe from these messages click <a href={origin}admin/unsubscribe?token={token}>here</a>
<br/>
<br/>";
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = content + unsubscribe;
				try
				{
               var message = new MimeMessage();
               message.Subject = subject;
               message.Body = bodyBuilder.ToMessageBody();
               
               message.From.Add(new MailboxAddress(settings.FromName, settings.FromEmail));
               message.To.Add(new MailboxAddress(settings.ToName, subscriber.Email));
               client.Send(message);
            }
				catch (Exception ex)
				{
               Serilog.Log.Warning($"Error sending email to {subscriber.Email}: {ex.Message}");
            }
         }

         client.Disconnect(true);
         return await Task.FromResult(true);
      }
      public async Task<bool> SendVerificationEmail(Author account, string origin)
      {
         string token = account.VerificationToken;
         var verifyUrl = $"{origin}admin/verify-email?token={account.VerificationToken}";
         string body = 
$@"
<p>
Dear {origin} user,
</p>
<br/>
<p>
We have received a request to authorize this email address for use with {origin}. If you requested this verification, please go to the following URL to confirm that you are authorized to use this email address:
{verifyUrl}
</p>
<p>
Your request to register your email address on this site will not be processed unless you confirm the address using this URL. This link expires 7 days after your original request.
</p>
<p>
If you did NOT request to verify this email address, do not click on the link. Please note that many times, the situation isn't a phishing attempt, but either a misunderstanding of how to use our service, or someone setting up email-sending capabilities on your behalf as part of a legitimate service, but without having fully communicated the procedure first. 
</p>
<p>
If you are still concerned, please reply and let us know that you did not request the verification.
</p>
";
         return await Send(
				to: account.Email,
				subject: $"Verify your email address from {origin}",
				html: $@"<h4>Verify Email</h4>
							<p>Thanks for registering!</p>
							{body}"
			);
      }
      private async Task<bool> Send(string to, string subject, string html, string from = null)
      {
         var settings = await _db.MailSettings.AsNoTracking().FirstOrDefaultAsync();
			if (settings == null || settings.Enabled == false)
				return false;
         // send email
         var client = GetClient(settings);
         if (client == null)
            return false;

         // create message
         var email = new MimeMessage();
         email.From.Add(MailboxAddress.Parse(from ?? settings.FromEmail));
         email.To.Add(MailboxAddress.Parse(to));
         email.Subject = subject;
         email.Body = new TextPart(TextFormat.Html) { Text = html };

         client.Send(email);
         client.Disconnect(true);
         return await Task.FromResult(true);
      }
   }
}
