using System.Text.RegularExpressions;

namespace Blogifier.Helper;

public static partial class StringHelper
{
  [GeneratedRegex("<script[^>]*>[\\s\\S]*?</script>", RegexOptions.Compiled)]
  private static partial Regex ScriptGeneratedRegex();
  [GeneratedRegex("<img[^>]*>[\\s\\S]*?>", RegexOptions.Compiled)]
  private static partial Regex ImgGeneratedRegex();
  [GeneratedRegex("<img[^>]*?src\\s*=\\s*[\"']?([^'\" >]+?)[ '\"][^>]*?>", RegexOptions.IgnoreCase, "zh-CN")]
  private static partial Regex ImgTagsGeneratedRegex();
  [GeneratedRegex("<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase | RegexOptions.Singleline, "zh-CN")]
  private static partial Regex ImgSrcGeneratedRegex();
  [GeneratedRegex("(?i)<a\\b[^>]*?>(?<text>.*?)</a>", RegexOptions.IgnoreCase | RegexOptions.Singleline, "zh-CN")]
  private static partial Regex FileGeneratedRegex();

  public static string RemoveScriptTags(string input) => ScriptGeneratedRegex().Replace(input, string.Empty);
  public static string RemoveImgTags(string input) => ImgGeneratedRegex().Replace(input, string.Empty);
  public static MatchCollection MatchesImgTags(string input) => ImgTagsGeneratedRegex().Matches(input);
  public static Match MatchImgSrc(string input) => ImgSrcGeneratedRegex().Match(input);
  public static MatchCollection MatchesFile(string input) => FileGeneratedRegex().Matches(input);

}
