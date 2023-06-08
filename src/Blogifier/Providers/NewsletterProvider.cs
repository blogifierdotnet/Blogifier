using Blogifier.Data;
using Blogifier.Extensions;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Providers;

public class NewsletterProvider
{
  private readonly AppDbContext _db;
  private readonly EmailProvider _emailProvider;

  public NewsletterProvider(AppDbContext db, EmailProvider emailProvider)
  {
    _db = db;
    _emailProvider = emailProvider;
  }


  public async Task<List<Subscriber>> GetSubscribers()
  {
    return await _db.Subscribers.AsNoTracking().OrderByDescending(s => s.Id).ToListAsync();
  }

  public async Task<bool> AddSubscriber(Subscriber subscriber)
  {
    var existing = await _db.Subscribers.AsNoTracking().Where(s => s.Email == subscriber.Email).FirstOrDefaultAsync();
    if (existing == null)
    {
      subscriber.CreatedAt = DateTime.UtcNow;
      _db.Subscribers.Add(subscriber);
      return await _db.SaveChangesAsync() > 0;
    }
    return true;
  }

  public async Task<bool> RemoveSubscriber(int id)
  {
    var existing = _db.Subscribers.Where(s => s.Id == id).FirstOrDefault();
    if (existing != null)
    {
      _db.Subscribers.Remove(existing);
      return await _db.SaveChangesAsync() > 0;
    }
    return false;
  }


  public async Task<List<Newsletter>> GetNewsletters()
  {
    return await _db.Newsletters.AsNoTracking()
        .Include(n => n.Post)
        .OrderByDescending(n => n.Id)
        .ToListAsync();
  }

  private async Task<bool> SaveNewsletter(int postId, bool success)
  {
    var existing = await _db.Newsletters.Where(n => n.PostId == postId).FirstOrDefaultAsync();

    if (existing != null)
    {
      existing.Success = success;
    }
    else
    {
      var newsletter = new Newsletter()
      {
        PostId = postId,
        Success = success,
        Post = _db.Posts.Where(p => p.Id == postId).FirstOrDefault()
      };
      _db.Newsletters.Add(newsletter);
    }

    return await _db.SaveChangesAsync() > 0;
  }

  public async Task<bool> SendNewsletter(int postId)
  {
    var post = await _db.Posts.AsNoTracking().Where(p => p.Id == postId).FirstOrDefaultAsync();
    if (post == null)
      return false;

    var subscribers = await _db.Subscribers.AsNoTracking().ToListAsync();
    if (subscribers == null || subscribers.Count == 0)
      return false;

    var settings = await _db.MailSettings.AsNoTracking().FirstOrDefaultAsync();
    if (settings == null || settings.Enabled == false)
      return false;

    string subject = post.Title;
    string content = post.Content.MdToHtml();

    bool sent = await _emailProvider.SendEmail(settings, subscribers, subject, content);
    bool saved = await SaveNewsletter(postId, sent);

    return sent && saved;
  }

  public async Task<bool> RemoveNewsletter(int id)
  {
    var existing = _db.Newsletters.Where(s => s.Id == id).FirstOrDefault();
    if (existing != null)
    {
      _db.Newsletters.Remove(existing);
      return await _db.SaveChangesAsync() > 0;
    }
    return false;
  }


  public async Task<MailSetting> GetMailSettings()
  {
    var settings = await _db.MailSettings.AsNoTracking().FirstOrDefaultAsync();
    return settings == null ? new MailSetting() : settings;
  }

  public async Task<bool> SaveMailSettings(MailSetting mail)
  {
    var existing = await _db.MailSettings.FirstOrDefaultAsync();
    if (existing == null)
    {
      var newMail = new MailSetting()
      {
        Host = mail.Host,
        Port = mail.Port,
        UserEmail = mail.UserEmail,
        UserPassword = mail.UserPassword,
        FromEmail = mail.FromEmail,
        FromName = mail.FromName,
        ToName = mail.ToName,
        Enabled = mail.Enabled,
        Blog = _db.Blogs.FirstOrDefault()
      };
      _db.MailSettings.Add(newMail);
    }
    else
    {
      existing.Host = mail.Host;
      existing.Port = mail.Port;
      existing.UserEmail = mail.UserEmail;
      existing.UserPassword = mail.UserPassword;
      existing.FromEmail = mail.FromEmail;
      existing.FromName = mail.FromName;
      existing.ToName = mail.ToName;
      existing.Enabled = mail.Enabled;
    }
    return await _db.SaveChangesAsync() > 0;
  }
}
