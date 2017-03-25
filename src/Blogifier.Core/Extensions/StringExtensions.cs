using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Blogifier.Core.Extensions
{
	public static class StringExtensions
	{
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

		public static string ToSlug(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return string.Empty;

			str = str.ToLowerInvariant();
			var bytes = Encoding.GetEncoding("utf-8").GetBytes(str);
			str = Encoding.ASCII.GetString(bytes);
			str = Regex.Replace(str, @"\s", "-", RegexOptions.Compiled);
			str = Regex.Replace(str, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);
			str = str.Trim('-', '_');
			str = Regex.Replace(str, @"([-_]){2,}", "$1", RegexOptions.Compiled);
			return str;
		}

		public static bool Contains(this string source, string toCheck, StringComparison comp)
		{
			return source.IndexOf(toCheck, comp) >= 0;
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
	}
}
