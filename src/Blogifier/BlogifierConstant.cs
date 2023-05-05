using System.Collections.Generic;

namespace Blogifier;

public class BlogifierConstant
{
  public const string PolicyCorsName = "BlogifierPolicy";
  public const string OptionsName = "Blogifier";
  public const string StorageObjectUrl = "/storage/object";
  public const string OutputCacheExpire1 = "Expire1";
  public const string ThemeKey = "theme";

  public static readonly Dictionary<string, string> DefaultOption = new()
  {
    { ThemeKey, "standard" }
  };
}
