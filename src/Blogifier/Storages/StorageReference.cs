using Blogifier.Shared;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogifier.Storages;

public class StorageReference
{
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public DateTime CreatedAt { get; set; }
  public int StorageId { get; set; }
  public Storage Storage { get; set; } = default!;
  public int EntityId { get; set; }
  public StorageReferenceType Type { get; set; }
  public Post? Post { get; set; }
}
