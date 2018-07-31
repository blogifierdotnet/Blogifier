using System;
using System.Globalization;

namespace Core.Helpers
{
    public class SystemClock
    {
        public static DateTime Now()
        {
            return DateTime.UtcNow;
        }

        public static DateTime RssPubishedToDateTime(string date)
        {
            DateTime result = DateTime.MinValue;
            string[] formats = { "ddd, dd MMM yyyy HH:mm:ss zzz", "ddd, d MMM yyyy HH:mm:ss zzz" };

            foreach (var str in formats)
            {
                try
                {
                    result = DateTime.ParseExact(date, str, DateTimeFormatInfo.InvariantInfo);
                    return result;
                }
                catch { }
            }
            return result;
        }
    }
}
