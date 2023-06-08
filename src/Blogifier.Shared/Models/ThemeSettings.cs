using System.Collections.Generic;

namespace Blogifier.Shared;

public class Field
{
  public string Id { get; set; } = default!;
  public string Label { get; set; } = default!;
  public string Type { get; set; } = default!;
  public string Value { get; set; } = default!;
  public List<string> Options { get; set; } = default!;
}

public class Section
{
  public string Label { get; set; } = default!;
  public List<Field> Fields { get; set; } = default!;
}

public class ThemeSettings
{
  public List<Section> Sections { get; set; } = default!;
}
