using Blogifier.Core.Data;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Providers
{
	public interface ISubscriberProvider
	{
		Task<bool> AddSubscriber(Subscriber subscriber);
		Task<List<Subscriber>> GetSubscribers();
		Task<bool> RemoveSubscriber(int id);
		Task<bool> SendNewsletter(int postId);
		Task<List<Newsletter>> GetNewsletters();
	}

	public class SubscriberProvider : ISubscriberProvider
	{
		private readonly AppDbContext _db;

		public SubscriberProvider(AppDbContext db)
		{
			_db = db;
		}

		public async Task<bool> AddSubscriber(Subscriber subscriber)
		{
			var existing = await _db.Subscribers.AsNoTracking().Where(s => s.Email == subscriber.Email).FirstOrDefaultAsync();
			if (existing == null)
			{
				subscriber.DateCreated = DateTime.UtcNow;
				_db.Subscribers.Add(subscriber);
				return await _db.SaveChangesAsync() > 0;
			}
			return true;
		}

		public async Task<List<Subscriber>> GetSubscribers()
		{
			return await _db.Subscribers.AsNoTracking().OrderByDescending(s => s.Id).ToListAsync();
		}

		public async Task<List<Newsletter>> GetNewsletters()
		{
			return await _db.Newsletters.AsNoTracking().OrderByDescending(s => s.Id).ToListAsync();
		}

		public async Task<bool> RemoveSubscriber(int id)
		{
			var existing = _db.Subscribers.Where(s => s.Id == id).FirstOrDefault();
			if(existing != null)
			{
				_db.Subscribers.Remove(existing);
				return await _db.SaveChangesAsync() > 0;
			}
			return false;
		}

		public async Task<bool> SendNewsletter(int postId)
		{
			var post = await _db.Posts.AsNoTracking().Where(p => p.Id == postId).FirstOrDefaultAsync();
			if (post == null)
				return false;

			var subscribers = await _db.Subscribers.AsNoTracking().ToListAsync();
			if (subscribers == null || subscribers.Count == 0)
				return false;

			int sent = 0;
			int fail = 0;

			foreach (var subscriber in subscribers)
			{
				if(await SendEmail(subscriber))
				{
					sent++;
				}
				else
				{
					fail++;
				}
			}

			return await SaveNewsletter(postId, sent, fail);
		}

		private async Task<bool> SendEmail(Subscriber subscriber)
		{
			return true;
		}

		private async Task<bool> SaveNewsletter(int postId, int sentCount, int failCount)
		{
			var existing = await _db.Newsletters.AsNoTracking().Where(n => n.PostId == postId).FirstOrDefaultAsync();
			if (existing != null)
				return false;

			var newsletter = new Newsletter()
			{
				PostId = postId,
				SentCount = sentCount,
				FailCount = failCount,
				DateCreated = DateTime.UtcNow
			};

			_db.Newsletters.Add(newsletter);
			return await _db.SaveChangesAsync() > 0;
		}
	}
}
