using Blogifier.Shared;

namespace Blogifier.Admin;

public class FrontPostEditorDto : PostEditorDto
{
  public bool Selected { get; set; }
  public bool? ImportComplete { get; set; }
}
