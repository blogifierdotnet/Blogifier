using Markdig;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Blogifier.Extensions;

public static class StringExtensions
{
  public static string ToThumb(this string img)
  {
    if (img.IndexOf('/') < 1) return img;

    var first = img.Substring(0, img.LastIndexOf('/'));
    var second = img.Substring(img.LastIndexOf('/'));

    return $"{first}/thumbs{second}";
  }

  public static string Capitalize(this string str)
  {
    if (string.IsNullOrEmpty(str))
      return string.Empty;
    char[] a = str.ToCharArray();
    a[0] = char.ToUpper(a[0]);
    return new string(a);
  }

  public static string MdToHtml(this string str)
  {
    var mpl = new MarkdownPipelineBuilder()
        .UsePipeTables()
        .UseAdvancedExtensions()
        .Build();

    return Markdown.ToHtml(str, mpl);
  }

  public static bool Contains(this string source, string toCheck, StringComparison comp)
  {
    return source.IndexOf(toCheck, comp) >= 0;
  }

  private static readonly Regex RegexStripHtml = new Regex("<[^>]*>", RegexOptions.Compiled);

  public static string StripHtml(this string str)
  {
    return string.IsNullOrWhiteSpace(str) ? string.Empty : RegexStripHtml.Replace(str, string.Empty).Trim();
  }

  /// <summary>
  /// Should extract title (file name) from file path or Url
  /// </summary>
  /// <param name="str">c:\foo\test.png</param>
  /// <returns>test.png</returns>
  public static string ExtractTitle(this string str)
  {
    if (str.Contains("\\"))
    {
      return string.IsNullOrWhiteSpace(str) ? string.Empty : str.Substring(str.LastIndexOf("\\")).Replace("\\", "");
    }
    else if (str.Contains("/"))
    {
      return string.IsNullOrWhiteSpace(str) ? string.Empty : str.Substring(str.LastIndexOf("/")).Replace("/", "");
    }
    else
    {
      return str;
    }
  }

  /// <summary>
  /// Converts title to valid URL slug
  /// </summary>
  /// <returns>Slug</returns>
  public static string ToSlug(this string title)
  {
    var str = title.ToLowerInvariant();
    str = str.Trim('-', '_');

    if (string.IsNullOrEmpty(str))
      return string.Empty;

    var bytes = Encoding.GetEncoding("utf-8").GetBytes(str);
    str = Encoding.UTF8.GetString(bytes);

    str = Regex.Replace(str, @"\s", "-", RegexOptions.Compiled);

    str = Regex.Replace(str, @"([-_]){2,}", "$1", RegexOptions.Compiled);
    str = RemoveIllegalCharacters(str);
    return str;
  }

  public static string Hash(this string source, string salt)
  {
    var bytes = KeyDerivation.Pbkdf2(
              password: source,
              salt: Encoding.UTF8.GetBytes(salt),
              prf: KeyDerivationPrf.HMACSHA512,
              iterationCount: 10000,
              numBytesRequested: 256 / 8);

    return Convert.ToBase64String(bytes);
  }

  public static string ReplaceIgnoreCase(this string str, string search, string replacement)
  {
    string result = Regex.Replace(
        str,
        Regex.Escape(search),
        replacement.Replace("$", "$$"),
        RegexOptions.IgnoreCase
    );
    return result;
  }

  #region Helper Methods

  static string RemoveIllegalCharacters(string text)
  {
    if (string.IsNullOrEmpty(text))
    {
      return text;
    }

    string[] chars = new string[] {
              ":", "/", "?", "!", "#", "[", "]", "{", "}", "@", "*", ".", ",",
              "\"","&", "'", "~", "$"
          };

    foreach (var ch in chars)
    {
      text = text.Replace(ch, string.Empty);
    }

    text = text.Replace("–", "-");
    text = text.Replace(" ", "-");

    text = RemoveUnicodePunctuation(text);
    text = RemoveDiacritics(text);
    text = RemoveExtraHyphen(text);

    return System.Web.HttpUtility.HtmlEncode(text).Replace("%", string.Empty);
  }

  static string RemoveUnicodePunctuation(string text)
  {
    var normalized = text.Normalize(NormalizationForm.FormD);
    var sb = new StringBuilder();

    foreach (var c in
        normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.InitialQuotePunctuation &&
                              CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.FinalQuotePunctuation))
    {
      sb.Append(c);
    }

    return sb.ToString();
  }

  static string RemoveDiacritics(string text)
  {
    var normalized = text.Normalize(NormalizationForm.FormD);
    var sb = new StringBuilder();

    foreach (var c in
        normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
    {
      sb.Append(c);
    }

    return sb.ToString();
  }

  static string RemoveExtraHyphen(string text)
  {
    if (text.Contains("--"))
    {
      text = text.Replace("--", "-");
      return RemoveExtraHyphen(text);
    }

    return text;
  }

  public static string SanitizePath(this string str)
  {
    if (string.IsNullOrWhiteSpace(str))
      return string.Empty;

    str = str.Replace("%2E", ".").Replace("%2F", "/");

    if (str.Contains("..") || str.Contains("//"))
      throw new ApplicationException("Invalid directory path");

    return str;
  }

  public static string SanitizeFileName(this string str)
  {
    str = str.SanitizePath();

    //TODO: add filename specific validation here

    return str;
  }

  #endregion
}
