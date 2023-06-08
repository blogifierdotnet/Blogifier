using System;
using System.Text.RegularExpressions;

namespace Blogifier.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            char[] a = str.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
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

        public static string RemoveScriptTags(this string str)
        {
            Regex scriptRegex = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            return scriptRegex.Replace(str, "");
        }

        public static string RemoveImgTags(this string str)
        {
            Regex scriptRegex = new Regex(@"<img[^>]*>[\s\S]*?>");
            return scriptRegex.Replace(str, "");
        }
    }
}
