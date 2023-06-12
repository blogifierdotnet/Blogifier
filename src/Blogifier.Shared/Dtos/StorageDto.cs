namespace Blogifier.Shared;

public class StorageDto
{
  public int Id { get; set; }
  public string Slug { get; set; } = default!;
  public string Name { get; set; } = default!;
  public long Length { get; set; }
  public string ContentType { get; set; } = default!;
}
