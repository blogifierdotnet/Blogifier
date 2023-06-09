using AutoMapper;
using Blogifier.Caches;
using Blogifier.Options;
using Blogifier.Posts;
using Blogifier.Shared;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blogifier.Newsletters;

public class EmailManager
{
  private readonly ILogger _logger;
  private readonly IMapper _mapper;
  private readonly MarkdigProvider _markdigProvider;
  private readonly OptionProvider _optionProvider;
  private readonly PostProvider _postProvider;
  private readonly NewsletterProvider _newsletterProvider;
  private readonly SubscriberProvider _subscriberProvider;

  public EmailManager(
    ILogger<EmailManager> logger,
    IMapper mapper,
    MarkdigProvider markdigProvider,
    OptionProvider optionProvider,
    PostProvider postProvider,
    NewsletterProvider newsletterProvider,
    SubscriberProvider subscriberProvider)
  {
    _logger = logger;
    _mapper = mapper;
    _markdigProvider = markdigProvider;
    _optionProvider = optionProvider;
    _postProvider = postProvider;
    _newsletterProvider = newsletterProvider;
    _subscriberProvider = subscriberProvider;
  }

  public async Task<SendNewsletterState> SendNewsletter(int postId)
  {
    var newsletter = await _newsletterProvider.FirstOrDefaultByPostIdAsync(postId);
    if (newsletter != null && newsletter.Success) return SendNewsletterState.NewsletterSuccess;

    var post = await _postProvider.GetAsync(postId);
    if (post == null) return SendNewsletterState.NotPost;

    var subscribers = await _subscriberProvider.GetItemsAsync();
    if (!subscribers.Any()) return SendNewsletterState.NotSubscriber;

    var settings = await GetSettingsAsync();
    if (settings == null || settings.Enabled == false) return SendNewsletterState.NotMailEnabled;

    var subject = post.Title;
    var content = _markdigProvider.ToHtml(post.Content);

    var sent = await Send(settings, subscribers, subject, content);
    if (newsletter == null)
    {
      await _newsletterProvider.AddAsync(postId, sent);
    }
    else
    {
      await _newsletterProvider.UpdateAsync(newsletter.Id, sent);
    }
    return sent ? SendNewsletterState.OK : SendNewsletterState.SentError;
  }

  public async Task<MailSettingDto?> GetSettingsAsync()
  {
    var key = CacheKeys.BlogMailData;
    var value = await _optionProvider.GetByValueAsync(key);
    if (value != null)
    {
      var data = JsonSerializer.Deserialize<MailSettingData>(value);
      return _mapper.Map<MailSettingDto>(data);
    }
    return null;
  }

  public async Task PutSettingsAsync(MailSettingDto input)
  {
    var key = CacheKeys.BlogMailData;
    var data = _mapper.Map<MailSettingData>(input);
    var value = JsonSerializer.Serialize(data);
    await _optionProvider.SetValue(key, value);
  }


  private async Task<bool> Send(MailSettingDto settings, IEnumerable<SubscriberDto> subscribers, string subject, string content)
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

  private SmtpClient GetClient(MailSettingDto settings)
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
