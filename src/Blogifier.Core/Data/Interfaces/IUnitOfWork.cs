using System;

namespace Blogifier.Core.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAssetRepository Assets { get; }
        IProfileRepository Profiles { get; }
        ICategoryRepository Categories { get; }
        IPostRepository Posts { get; }

        int Complete();
    }
}
