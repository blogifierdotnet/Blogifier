using System.Collections.Generic;

namespace Blogifier.Shared;

public class PostPagerDto
{
  public PostPagerDto(IEnumerable<PostItemDto> items, int total, int pageSize, int page)
  {
    Items = items;
    Total = total;
    PageSize = pageSize;
    Page = page;
    Pagination = total > pageSize;
  }
  public IEnumerable<PostItemDto> Items { get; set; }
  public int Total { get; set; }
  public int PageSize { get; }
  public int Page { get; }
  public bool Pagination { get; set; }
  public string? LinkToOlder { get; set; }
  public string? LinkToNewer { get; set; }
}
