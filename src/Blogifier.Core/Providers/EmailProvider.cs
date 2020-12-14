using Blogifier.Shared;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Providers
{
	public interface IEmailProvider
	{
		Task<bool> SendEmail(MailSetting settings, List<Subscriber> subscribers, string subject, string content);
	}

	public class MailKitProvider : IEmailProvider
	{
		public MailKitProvider() { }

		public async Task<bool> SendEmail(MailSetting settings, List<Subscriber> subscribers, string subject, string content)
		{                 
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
   }
}
