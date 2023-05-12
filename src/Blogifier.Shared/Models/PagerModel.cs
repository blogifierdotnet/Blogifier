using Blogifier.Models;

namespace Blogifier.Shared;

public class PagerModel : MainModel
{
  public PagerModel(int currentPage, int itemsPerPage, string absoluteUrl) : base(absoluteUrl)
  {
    CurrentPage = currentPage;
    ItemsPerPage = itemsPerPage;
    Newer = CurrentPage - 1;
    ShowNewer = CurrentPage > 1;
    Older = currentPage + 1;
  }
  public int CurrentPage { get; set; } = 1;
  public int ItemsPerPage { get; set; }
  public int Newer { get; set; }
  public bool ShowNewer { get; set; }
  public int Older { get; set; }
}
