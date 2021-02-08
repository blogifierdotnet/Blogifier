using System;
using Blogifier.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Blogifier.Core
{
    public static class DateTimeExtensions
    {
        public static string ToFriendlyDateTimeString(this DateTime Date)
        {
            return FriendlyDate(Date) + " @ " + Date.ToString("t").ToLower();
        }

        public static string ToFriendlyShortDateString(this DateTime Date)
        {
            return $"{Date.ToString("MMM dd")}, {Date.Year}";
        }

        public static string ToFriendlyDateString(this DateTime Date)
        {
            return FriendlyDate(Date);
        }

        static string FriendlyDate(DateTime date)
        {
            string FormattedDate = "";
            if (date.Date == DateTime.Today)
            {
                FormattedDate = "Today";
            }
            else if (date.Date == DateTime.Today.AddDays(-1))
            {
                FormattedDate = "Yesterday";
            }
            else if (date.Date > DateTime.Today.AddDays(-6))
            {
                // *** Show the Day of the week
                FormattedDate = date.ToString("dddd").ToString();
            }
            else
            {
                FormattedDate = date.ToString("MMMM dd, yyyy");
            }
            return FormattedDate;
        }
    }

    public static class RequestExtensions
    {
        public static string ExtractAbsoluteUri(this HttpRequest request)
        {
            var appItem = request.HttpContext.RequestServices.GetRequiredService<IOptionsMonitor<AppItem>>();
            if (appItem.CurrentValue.SitemapBaseUri != null)
                return appItem.CurrentValue.SitemapBaseUri;
            
            return $"{request.Scheme}://{request.Host.ToUriComponent()}{request.PathBase.ToUriComponent()}";
        }
    }
}