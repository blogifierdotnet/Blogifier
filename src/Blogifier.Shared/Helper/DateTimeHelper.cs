using System;

namespace Blogifier.Helper;

public static class DateTimeHelper
{
  public static string ToFriendlyShortDateString(DateTime? date, string defaultString = "")
  {
    if (!date.HasValue) return defaultString;
    return ToFriendlyShortDateString(date.Value);
  }

  public static string ToFriendlyShortDateString(DateTime date)
  {
    return $"{date:MMM dd}, {date.Year}";
  }
}
