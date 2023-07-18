using Blogifier.Data;
using Blogifier.Identity;
using Blogifier.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Storages;

public class Storage : AppEntity<int>
{
  public int UserId { get; set; }
  public UserInfo User { get; set; } = default!;
  public DateTime CreatedAt { get; set; }
  public DateTime UploadAt { get; set; }
  [StringLength(2048)]
  public string Slug { get; set; } = default!;
  [StringLength(1024)]
  public string Name { get; set; } = default!;
  [StringLength(2048)]
  public string Path { get; set; } = default!;
  public long Length { get; set; }
  [StringLength(128)]
  public string ContentType { get; set; } = default!;
  public StorageType Type { get; set; }
  //public List<StorageReference>? StorageReferences { get; set; }
}
