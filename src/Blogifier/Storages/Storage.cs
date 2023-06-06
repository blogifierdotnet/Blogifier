using Blogifier.Identity;
using Blogifier.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogifier.Storages;

public class Storage
{
  [Key]
  public int Id { get; set; }
  public string UserId { get; set; } = default!;
  public UserInfo User { get; set; } = default!;
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public DateTime CreatedAt { get; set; }
  public bool IsDeleted { get; set; }
  public DateTime? DeletedAt { get; set; }
  [StringLength(2048)]
  public string Slug { get; set; } = default!;
  [StringLength(256)]
  public string Name { get; set; } = default!;
  [StringLength(2048)]
  public string Path { get; set; } = default!;
  public long Length { get; set; }
  [StringLength(128)]
  public string ContentType { get; set; } = default!;
  public StorageType Type { get; set; }
  public List<StorageReference>? StorageReferences { get; set; }
}
