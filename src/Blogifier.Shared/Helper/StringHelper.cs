using System.Text.RegularExpressions;

namespace Blogifier.Helper;

public static partial class StringHelper
{

  [GeneratedRegex("<script[^>]*>[\\s\\S]*?</script>", RegexOptions.Compiled)]
  private static partial Regex HtmlScriptGeneratedRegex();
  public static string RemoveHtmlScriptTags(string input) => HtmlScriptGeneratedRegex().Replace(input, string.Empty);


  [GeneratedRegex("<img[^>]*>[\\s\\S]*?>", RegexOptions.Compiled)]
  private static partial Regex HtmlImgGeneratedRegex();
  public static string RemoveHtmlImgTags(string input) => HtmlImgGeneratedRegex().Replace(input, string.Empty);


  [GeneratedRegex("<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.Compiled)]
  private static partial Regex HtmlImgSrcGeneratedRegex();
  public static Match MatchHtmlImgSrc(string input) => HtmlImgSrcGeneratedRegex().Match(input);

  [GeneratedRegex("<img[^>]*?src\\s*=\\s*[\"']?([^'\" >]+?)[ '\"][^>]*?>", RegexOptions.Compiled)]
  private static partial Regex HtmlImgTagsGeneratedRegex();
  public static MatchCollection MatchesHtmlImgTags(string input) => HtmlImgTagsGeneratedRegex().Matches(input);


  [GeneratedRegex("(?i)<a\\b[^>]*?>(?<text>.*?)</a>", RegexOptions.Compiled)]
  private static partial Regex HtmlFileGeneratedRegex();
  public static MatchCollection MatchesHtmlFile(string input) => HtmlFileGeneratedRegex().Matches(input);

}
