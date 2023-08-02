using Blogifier.Shared;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Blogifier.Storages;

public interface IStorageProvider
{
  Task<bool> ExistsAsync(string slug);
  Task<StorageDto?> GetCheckStoragAsync(string path);
  Task<StorageDto?> GetAsync(string slug, Func<Stream, CancellationToken, Task> callback);
  Task<StorageDto> AddAsync(DateTime uploadAt, int userid, string path, string fileName, Stream stream, string contentType);
  Task<StorageDto> AddAsync(DateTime uploadAt, int userid, string path, string fileName, byte[] bytes, string contentType);
}
