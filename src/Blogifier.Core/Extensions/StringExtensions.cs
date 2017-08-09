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

        /// <summary>
        /// Converts title to valid URL slug
        /// </summary>
        /// <param name="str">Title</param>
        /// <returns>Slug</returns>
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

        /// <summary>
        /// Converts post body to post description
        /// </summary>
        /// <param name="str">HTML post body</param>
        /// <returns>Post decription as plain text</returns>
        public static string ToDescription(this string str)
        {
            str = str.StripHtml();
            return str.Length > 300 ? str.Substring(0, 300) : str;
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

        public static string MaskPassword(this string str)
        {
            var idx = str.IndexOf("password=", StringComparison.OrdinalIgnoreCase);

            if (idx >= 0)
            {
                var idxEnd = str.IndexOf(";", idx);
                if(idxEnd > idx)
                {
                    return str.Substring(0, idx) + "Password=******" + str.Substring(idxEnd);
                }
            }
            return str;
        }

        public static string ToPrettySize(this int value, int decimalPlaces = 0)
        {
            return ((long)value).ToPrettySize(decimalPlaces);
        }

        public static string ToPrettySize(this long value, int decimalPlaces = 0)
        {
            const long OneKb = 1024;
            const long OneMb = OneKb * 1024;
            const long OneGb = OneMb * 1024;
            const long OneTb = OneGb * 1024;

            var asTb = Math.Round((double)value / OneTb, decimalPlaces);
            var asGb = Math.Round((double)value / OneGb, decimalPlaces);
            var asMb = Math.Round((double)value / OneMb, decimalPlaces);
            var asKb = Math.Round((double)value / OneKb, decimalPlaces);

            string chosenValue = asTb > 1 ? string.Format("{0}Tb", asTb)
                : asGb > 1 ? string.Format("{0}Gb", asGb)
                : asMb > 1 ? string.Format("{0}Mb", asMb)
                : asKb > 1 ? string.Format("{0}Kb", asKb)
                : string.Format("{0}B", Math.Round((double)value, decimalPlaces));
            return chosenValue;
        }
    }
}