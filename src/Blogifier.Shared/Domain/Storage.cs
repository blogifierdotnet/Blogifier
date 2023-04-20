using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogifier.Shared;

public class Storage
{
  [Key]
  public int Id { get; set; }
  public int AuthorId { get; set; }
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public DateTime CreatedAt { get; set; }
  public bool IsDeleted { get; set; }
  public DateTime? DeletedAt { get; set; }
  [StringLength(256)]
  public string Name { get; set; }
  [StringLength(2048)]
  public string Url { get; set; }
  [StringLength(128)]
  public string ContentType { get; set; }
  public int StorageType { get; set; }
}
