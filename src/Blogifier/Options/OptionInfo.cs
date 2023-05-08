using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogifier.Options;

public class OptionInfo
{
  [Key]
  public int Id { get; set; }
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public DateTime CreatedAt { get; set; }
  [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
  public DateTime UpdatedAt { get; set; }
  [StringLength(256)]
  public string Key { get; set; } = default!;
  public string Value { get; set; } = default!;
}
