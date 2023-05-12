using Blogifier.Models;
using System.Collections.Generic;

namespace Blogifier.Shared;

public class PagerModel<T> : MainModel
{
  public PagerModel(string absoluteUrl, IEnumerable<T> itmes, int currentPage, int itemsPerPage = 10) : base(absoluteUrl)
  {
    CurrentPage = currentPage;
    ItemsPerPage = itemsPerPage;
    Newer = CurrentPage - 1;
    ShowNewer = CurrentPage > 1;
    Older = currentPage + 1;
    Items = itmes;
  }
  public IEnumerable<T> Items { get; set; }
  public int CurrentPage { get; set; } = 1;
  public int ItemsPerPage { get; set; }
  public int Newer { get; set; }
  public bool ShowNewer { get; set; }
  public int Older { get; set; }
}
