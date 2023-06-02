using Blogifier.Shared;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Newsletters;

public class EmailProvider
{
  private readonly ILogger _logger;

  public EmailProvider(ILogger<EmailProvider> logger)
  {
    _logger = logger;
  }

  public async Task<bool> SendEmail(MailSetting settings, List<Subscriber> subscribers, string subject, string content)
  {
    var client = GetClient(settings);
    if (client == null)
      return false;

    var bodyBuilder = new BodyBuilder
    {
      HtmlBody = content
    };

    foreach (var subscriber in subscribers)
    {
      try
      {
        var message = new MimeMessage
        {
          Subject = subject,
          Body = bodyBuilder.ToMessageBody()
        };
        message.From.Add(new MailboxAddress(settings.FromName, settings.FromEmail));
        message.To.Add(new MailboxAddress(settings.ToName, subscriber.Email));
        client.Send(message);
      }
      catch (Exception ex)
      {
        _logger.LogWarning("Error sending email to {Email}: {Message}", subscriber.Email, ex.Message);
      }
    }
    client.Disconnect(true);
    return await Task.FromResult(true);
  }

  private SmtpClient GetClient(MailSetting settings)
  {
    try
    {
      var client = new SmtpClient
      {
        ServerCertificateValidationCallback = (s, c, h, e) => true
      };
      client.Connect(settings.Host, settings.Port, SecureSocketOptions.Auto);
      client.Authenticate(settings.UserEmail, settings.UserPassword);
      return client;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error connecting to SMTP client");
      throw;
    }

  }
}
