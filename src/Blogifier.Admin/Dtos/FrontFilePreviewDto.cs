using Microsoft.AspNetCore.Components.Forms;

namespace Blogifier.Admin;

public class FrontFilePreviewDto
{
  public string FileName { get; set; } = default!;
  public string Url { get; set; } = default!;
  public string Selection { get; set; } = default!;
  public IBrowserFile BrowserFile { get; set; } = default!;
}
