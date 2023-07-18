using System.Text.RegularExpressions;

namespace Blogifier.Helper;

public static class StringHelper
{
  private static Regex _regexScript = new("<script[^>]*>[\\s\\S]*?</script>", RegexOptions.Compiled);
  private static Regex _regexImg = new("<img[^>]*>[\\s\\S]*?>", RegexOptions.Compiled);
  private static Regex _regexImgTags = new("<img[^>]*?src\\s*=\\s*[\"']?([^'\" >]+?)[ '\"][^>]*?>", RegexOptions.IgnoreCase);
  private static Regex _regexImgSrc = new("<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
  private static Regex _regexFile = new(@"(?i)<a\b[^>]*?>(?<text>.*?)</a>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
  public static string RemoveScriptTags(string input) => _regexScript.Replace(input, string.Empty);
  public static string RemoveImgTags(string input) => _regexImg.Replace(input, string.Empty);
  public static MatchCollection MatchesImgTags(string input) => _regexImgTags.Matches(input);
  public static Match MatchImgSrc(string input) => _regexImgSrc.Match(input);
  public static MatchCollection MatchesFile(string input) => _regexFile.Matches(input);
}
