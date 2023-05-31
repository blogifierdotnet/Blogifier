using Blogifier.Shared;
using System.Collections.Generic;

namespace Blogifier.Admin;

public class FrontImportDto : ImportDto
{
  public new List<FrontPostEditorDto> Posts { get; set; } = default!;
}
