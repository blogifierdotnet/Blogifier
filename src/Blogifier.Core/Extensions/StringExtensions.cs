using Markdig;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Renderers.Html.Inlines;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Blogifier.Core
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

        public static string MdToHtml(this string str)
        {
            var mpl = new MarkdownPipelineBuilder()
                .UsePipeTables()
                .UseAdvancedExtensions()
                .Build();

            return Markdown.ToHtml(str, mpl);
        }

        public class ZenImageMarkdownOptions
        {
            public Uri Host { get; set; }
            public List<LinkInline> RenderedLinks { get; } = new List<LinkInline>();
        }
        
        public class ZenImageMarkdownExtensions : IMarkdownExtension
        {
            private readonly ZenImageMarkdownOptions _options;

            public ZenImageMarkdownExtensions(ZenImageMarkdownOptions options)
            {
                _options = options;
            }
            
            public void Setup(MarkdownPipelineBuilder pipeline)
            {
                // pipeline.InlineParsers.Insert(0, new ZenImageInlineParser());
            }

            public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
            {
                if (renderer is HtmlRenderer htmlRenderer)
                {
                    var inlineRenderer = htmlRenderer.ObjectRenderers.FindExact<LinkInlineRenderer>();
                    if (inlineRenderer != null)
                    {
                        inlineRenderer.TryWriters.Remove(TryLinkInlineRenderer);
                        inlineRenderer.TryWriters.Add(TryLinkInlineRenderer);
                    }
                }
            }
            
            private bool TryLinkInlineRenderer(HtmlRenderer renderer, LinkInline linkInline)
            {
                if (!linkInline.IsImage || linkInline.Url == null)
                {
                    return false;
                }

                var figureElement = new XElement(
                    "figure",
                    new XElement(
                        "img",
                        new XAttribute("src", _options.Host + linkInline.Url)),
                    new XElement("figcaption", linkInline.Title));

                renderer.Write(figureElement.ToString());
                
                _options.RenderedLinks.Add(linkInline);

                return true;
                /*
               
               
               
               <figure>     
   <img src="http://example.com/pic1.jpg" width="1200" height="900">
   <figcaption>
      Первый андроид-фермер смотрит на свои угодья
   </figcaption>  
 </figure>
               */
            }
        }
     
        
        public static (string content, LinkInline[] images) MdToZen(this string str, Uri host)
        {
            var zenImageMarkdownOptions = new ZenImageMarkdownOptions{Host = host};
            var mpl = new MarkdownPipelineBuilder()
                .Use(new ZenImageMarkdownExtensions(zenImageMarkdownOptions))
                .UsePipeTables()
                .UseAdvancedExtensions()
                .Build();

            var content = Markdown.ToHtml(str, mpl);
            return (content, zenImageMarkdownOptions.RenderedLinks.ToArray());
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        // true if string ends with image extension
        public static bool IsImagePath(this string str)
        {
            var exts = AppSettings.ImageExtensions.Split(',');

            foreach (var ext in exts)
            {
                if(str.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        // true if string is valid email address
        public static bool IsEmail(this string str)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(str);
                return addr.Address == str;
            }
            catch
            {
                return false;
            }
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
                if (idxEnd > idx)
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

            return HttpUtility.HtmlEncode(text).Replace("%", string.Empty);
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
}