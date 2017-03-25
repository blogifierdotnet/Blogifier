using System;

namespace Blogifier.Core.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAssetRepository Assets { get; }
        IBlogRepository Blogs { get; }
        ICategoryRepository Categories { get; }
        IPostRepository Posts { get; }

        int Complete();
    }
}
