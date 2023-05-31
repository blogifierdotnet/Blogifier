using System;

namespace Blogifier.Helper;

public static class DateTimeHelper
{
  public static string ToFriendlyShortDateString(DateTime? date)
  {
    if (!date.HasValue) return string.Empty;
    return ToFriendlyShortDateString(date.Value);
  }

  public static string ToFriendlyShortDateString(DateTime date)
  {
    return $"{date:MMM dd}, {date.Year}";
  }
}
