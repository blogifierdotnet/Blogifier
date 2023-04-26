using System.Collections.Generic;

namespace Blogifier.Shared;

public class ImportDto
{
  public string BaseUrl { get; set; } = default!;
  public List<ImportPostDto> Posts { get; set; } = default!;
}
