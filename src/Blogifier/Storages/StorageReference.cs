namespace Blogifier.Storages;

public class StorageReference
{
  public int StorageId { get; set; }
  public Storage Storage { get; set; } = default!;
  public int ReferenceId { get; set; }
  public StorageReferenceType Type { get; set; } 
}

public enum StorageReferenceType
{
  Post = 1,
}
