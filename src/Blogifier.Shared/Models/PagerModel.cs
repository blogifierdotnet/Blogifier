namespace Blogifier.Shared;

public class PagerModel : MainModel
{
  public PagerModel(int currentPage, MainDto main) : base(main)
  {
    CurrentPage = currentPage;
    Newer = CurrentPage - 1;
    ShowNewer = CurrentPage > 1;
    Older = currentPage + 1;
  }
  public int CurrentPage { get; set; } = 1;
  public int Total { get; set; }
  public bool NotFound { get; set; }
  public int Newer { get; set; }
  public bool ShowNewer { get; set; }
  public int Older { get; set; }
  public bool ShowOlder { get; set; }
  public string LinkToNewer { get; set; }
  public string LinkToOlder { get; set; }
  public string RouteValue { get; set; }
  public int LastPage { get; set; } = 1;
}
