namespace Blogifier.Caches;

public static class CacheKeys
{
  public const string BlogData = "blogifier";
  public const string BlogMailData = $"{BlogData}:mail";
  public const string CategoryItemes = $"{BlogData}:category:itemes";
}
