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
	}
}
