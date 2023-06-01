using System.Collections.Generic;

namespace Blogifier.Shared;

public class PostPagerDto
{
  public PostPagerDto(IEnumerable<PostItemDto> items, int total, int page, int pageSize)
  {
    Items = items;
    Total = total;
    Page = page;
    PageSize = pageSize;
    Pagination = total > pageSize;
  }

  public IEnumerable<PostItemDto> Items { get; set; }
  public int Total { get; set; }
  public int PageSize { get; }
  public int Page { get; }
  public bool Pagination { get; set; }
  public string? LinkToOlder { get; set; }
  public string? LinkToNewer { get; set; }
  public void Configure(string? path, string queryKey)
  {
    if (path != null && Pagination)
    {
      if (Page != 1)
      {
        var page = Page - 1;
        LinkToOlder = $"{path}?{queryKey}={page}";
      }

      if (Page * PageSize < Total)
      {
        var page = Page + 1;
        LinkToNewer = $"{path}?{queryKey}={page}";
      }
    }
  }
}
