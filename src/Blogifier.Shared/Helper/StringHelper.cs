using System.Text.RegularExpressions;

namespace Blogifier.Helper;

public static partial class StringHelper
{

  [GeneratedRegex("<script[^>]*>[\\s\\S]*?</script>", RegexOptions.Compiled)]
  private static partial Regex ScriptGeneratedRegex();
  public static string RemoveScriptTags(string input) => ScriptGeneratedRegex().Replace(input, string.Empty);


  [GeneratedRegex("<img[^>]*>[\\s\\S]*?>", RegexOptions.Compiled)]
  private static partial Regex ImgGeneratedRegex();
  public static string RemoveImgTags(string input) => ImgGeneratedRegex().Replace(input, string.Empty);


  [GeneratedRegex("<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.Compiled)]
  private static partial Regex ImgSrcGeneratedRegex();
  public static Match MatchImgSrc(string input) => ImgSrcGeneratedRegex().Match(input);

  [GeneratedRegex("<img[^>]*?src\\s*=\\s*[\"']?([^'\" >]+?)[ '\"][^>]*?>", RegexOptions.Compiled)]
  private static partial Regex ImgTagsGeneratedRegex();
  public static MatchCollection MatchesImgTags(string input) => ImgTagsGeneratedRegex().Matches(input);


  [GeneratedRegex("(?i)<a\\b[^>]*?>(?<text>.*?)</a>", RegexOptions.Compiled)]
  private static partial Regex FileGeneratedRegex();
  public static MatchCollection MatchesFile(string input) => FileGeneratedRegex().Matches(input);

}
