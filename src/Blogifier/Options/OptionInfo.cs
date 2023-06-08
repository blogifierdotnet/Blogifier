using System;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Options;

public class OptionInfo
{
  [Key]
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  [StringLength(256)]
  public string Key { get; set; } = default!;
  public string Value { get; set; } = default!;
}
