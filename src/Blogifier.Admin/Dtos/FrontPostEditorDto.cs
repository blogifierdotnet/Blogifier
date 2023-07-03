using System.Collections.Generic;
using Blogifier.Shared;

namespace Blogifier.Admin;

public class FrontPostEditorDto : PostEditorDto
{
  public List<FrontFileDto> Files { get; set; } = default!;
}
