using System;

namespace Blogifier.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToFriendlyDateString(this DateTime Date)
        {
            string FormattedDate = "";
            if (Date.Date == DateTime.Today)
            {
                FormattedDate = "Today";
            }
            else if (Date.Date == DateTime.Today.AddDays(-1))
            {
                FormattedDate = "Yesterday";
            }
            else if (Date.Date > DateTime.Today.AddDays(-6))
            {
                // *** Show the Day of the week
                FormattedDate = Date.ToString("dddd").ToString();
            }
            else
            {
                FormattedDate = Date.ToString("MMMM dd, yyyy");
            }
            //append the time portion to the output
            FormattedDate += " @ " + Date.ToString("t").ToLower();
            return FormattedDate;
        }
    }
}
