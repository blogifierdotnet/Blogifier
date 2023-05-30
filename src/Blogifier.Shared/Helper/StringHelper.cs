using System.Text.RegularExpressions;

namespace Blogifier.Helper;

public static class StringHelper
{
  private static Regex _regexScript = new("<script[^>]*>[\\s\\S]*?</script>", RegexOptions.Compiled);
  private static Regex _regexImg = new("<img[^>]*>[\\s\\S]*?>", RegexOptions.Compiled);
  public static string RemoveScriptTags(string input) => _regexScript.Replace(input, string.Empty);
  public static string RemoveImgTags(string input) => _regexImg.Replace(input, string.Empty);
}
