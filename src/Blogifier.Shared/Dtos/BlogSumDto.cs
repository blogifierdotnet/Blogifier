namespace Blogifier.Shared;

public class BlogSumDto
{
  public string Time { get; set; } = default!;
  public int Posts { get; set; }
  public int Pages { get; set; }
  public int Views { get; set; }
  public int Subscribers { get; set; }
}
