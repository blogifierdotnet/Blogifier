using Blogifier.Core.Data;
using Blogifier.Shared;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Providers
{
	public interface IEmailProvider
	{
		Task<bool> SendEmail(List<Subscriber> subscribers, string subject, string content);
      Task<bool> SendVerificationEmail(Author account, string origin);
	}

	public class MailKitProvider : IEmailProvider
	{
      private readonly AppDbContext _db;
		public MailKitProvider(AppDbContext db) 
      { 
         _db = db;
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

		public async Task<bool> SendEmail(List<Subscriber> subscribers, string subject, string content)
		{                 
         var settings = await _db.MailSettings.AsNoTracking().FirstOrDefaultAsync();
			if (settings == null || settings.Enabled == false)
				return false;

         var client = GetClient(settings);
         if (client == null)
            return false;

         var bodyBuilder = new BodyBuilder();
         bodyBuilder.HtmlBody = content;      

			foreach (var subscriber in subscribers)
			{
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
			string message;
			if (!string.IsNullOrEmpty(origin))
			{
				// origin exists if request sent from browser single page app (e.g. Angular or React)
				// so send link to verify via single page app
				var verifyUrl = $"{origin}admin/verify-email?token={account.VerificationToken}";
				message = $@"<p>Please click the below link to verify your email address:</p>
								<p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
			}
			else
			{
				// origin missing if request sent directly to api (e.g. from Postman)
				// so send instructions to verify directly with api
				message = $@"<p>Please use the below token to verify your email address with the <code>/admin/verify-email</code> url:</p>
								<p><code>{account.VerificationToken}</code></p>";
			}

			return await Send(
				to: account.Email,
				subject: "Sign-up Verification API - Verify Email",
				html: $@"<h4>Verify Email</h4>
							<p>Thanks for registering!</p>
							{message}"
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
