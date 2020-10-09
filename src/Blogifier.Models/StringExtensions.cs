using System;

namespace Blogifier.Models
{
    public static class StringExtensions
    {
        public static bool IsImagePath(this string str)
        {
            string imageExtensions = "png,jpg,gif,bmp,tiff";

            var exts = imageExtensions.Split(',');

            foreach (var ext in exts)
            {
                if (str.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
