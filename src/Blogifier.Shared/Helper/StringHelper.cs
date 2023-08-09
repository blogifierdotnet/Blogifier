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
  public static partial Regex HtmlImgSrcGeneratedRegex();

  [GeneratedRegex("<img[^>]*?src\\s*=\\s*[\"']?([^'\" >]+?)[ '\"][^>]*?>", RegexOptions.Compiled)]
  public static partial Regex HtmlImgTagsGeneratedRegex();

  [GeneratedRegex("(?i)<a\\b[^>]*?>(?<text>.*?)</a>", RegexOptions.Compiled)]
  public static partial Regex HtmlFileGeneratedRegex();

  [GeneratedRegex("!\\[[^\\]]*\\]\\((blob:[^)]+)\\)", RegexOptions.Compiled)]
  public static partial Regex MarkdownImgBlobGeneratedRegex();

  [GeneratedRegex(@"!\[(?<filename>[^\]]+)\]\(data:image\/(?<type>.+);base64,(?<data>.+?)\)", RegexOptions.Compiled)]
  public static partial Regex MarkdownDataImageBase64BlobGeneratedRegex();
}
