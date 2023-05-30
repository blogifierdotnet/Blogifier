using System.Collections.Generic;

namespace Blogifier.Shared;

public class ImportDto
{
  public string BaseUrl { get; set; } = default!;
  public List<PostEditorDto> Posts { get; set; } = default!;
}
