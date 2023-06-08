namespace Blogifier.Shared;

public class ThemeItem
{
  public string Title { get; set; } = default!;
  public string Cover { get; set; } = default!;
  public bool IsCurrent { get; set; }
  public bool HasSettings { get; set; }
}
