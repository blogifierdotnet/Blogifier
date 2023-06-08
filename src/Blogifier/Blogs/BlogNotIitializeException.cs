using System;

namespace Blogifier.Blogs;

public class BlogNotIitializeException : Exception
{
  public BlogNotIitializeException()
  {
  }

  public BlogNotIitializeException(string message)
      : base(message)
  {
  }

  public BlogNotIitializeException(string message, Exception inner)
      : base(message, inner)
  {
  }
}
