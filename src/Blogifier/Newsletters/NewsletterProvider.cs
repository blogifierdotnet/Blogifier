using AutoMapper;
using Blogifier.Data;
using Blogifier.Extensions;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Newsletters;

public class NewsletterProvider : AppProvider<Newsletter, int>
{
  private readonly IMapper _mapper;
  private readonly EmailManager _emailProvider;

  public NewsletterProvider(
    IMapper mapper,
    AppDbContext dbContext,
    EmailManager emailProvider)
    : base(dbContext)
  {
    _mapper = mapper;

    _emailProvider = emailProvider;
  }

  public async Task<IEnumerable<NewsletterDto>> GetItemsAsync()
  {
    var query = _dbContext.Newsletters
       .AsNoTracking()
       .Include(n => n.Post)
       .OrderByDescending(n => n.CreatedAt);
    return await _mapper.ProjectTo<NewsletterDto>(query).ToListAsync();
  }

  public async Task<bool> RemoveSubscriber(int id)
  {
    var existing = _dbContext.Subscribers.Where(s => s.Id == id).FirstOrDefault();
    if (existing != null)
    {
      _dbContext.Subscribers.Remove(existing);
      return await _dbContext.SaveChangesAsync() > 0;
    }
    return false;
  }

  private async Task<bool> SaveNewsletter(int postId, bool success)
  {
    var existing = await _dbContext.Newsletters.Where(n => n.PostId == postId).FirstOrDefaultAsync();

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
        Post = _dbContext.Posts.Where(p => p.Id == postId).FirstOrDefault()
      };
      _dbContext.Newsletters.Add(newsletter);
    }

    return await _dbContext.SaveChangesAsync() > 0;
  }

  public async Task<bool> SendNewsletter(int postId)
  {
    var post = await _dbContext.Posts.AsNoTracking().Where(p => p.Id == postId).FirstOrDefaultAsync();
    if (post == null)
      return false;

    var subscribers = await _dbContext.Subscribers.AsNoTracking().ToListAsync();
    if (subscribers == null || subscribers.Count == 0)
      return false;

    var settings = await _dbContext.MailSettings.AsNoTracking().FirstOrDefaultAsync();
    if (settings == null || settings.Enabled == false)
      return false;

    string subject = post.Title;
    string content = post.Content.MdToHtml();

    bool sent = await _emailProvider.SendEmail(settings, subscribers, subject, content);
    bool saved = await SaveNewsletter(postId, sent);

    return sent && saved;
  }




  public async Task<MailSetting> GetMailSettings()
  {
    var settings = await _dbContext.MailSettings.AsNoTracking().FirstOrDefaultAsync();
    return settings == null ? new MailSetting() : settings;
  }

  public async Task<bool> SaveMailSettings(MailSetting mail)
  {
    var existing = await _dbContext.MailSettings.FirstOrDefaultAsync();
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
      };
      _dbContext.MailSettings.Add(newMail);
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
    return await _dbContext.SaveChangesAsync() > 0;
  }
}
