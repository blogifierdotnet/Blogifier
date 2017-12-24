using System;
using System.Threading.Tasks;

namespace Blogifier.Core.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAssetRepository Assets { get; }
        IProfileRepository Profiles { get; }
        ICategoryRepository Categories { get; }
        IPostRepository BlogPosts { get; }
        ICustomRepository CustomFields { get; }
        ISubscriberRepository Subscribers { get; }

        Task<int> Complete();
    }
}
