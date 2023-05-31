namespace Blogifier.Shared;

public class AboutDto
{
  public string? Version { get; set; }
  public string OperatingSystem { get; set; } = default!;
  public string? DatabaseProvider { get; set; }
}
