namespace Blogifier.Shared;

public enum SendNewsletterState
{
  OK = 0,
  NotPost = 10,
  NotSubscriber = 11,
  NotMailEnabled = 12,
  NewsletterSuccess = 13,
  SentError = 14
}
