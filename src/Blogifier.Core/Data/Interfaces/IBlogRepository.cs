using Blogifier.Core.Data.Domain;

namespace Blogifier.Core.Data.Interfaces
{
    public interface IBlogRepository : IRepository<Publisher>
    {

    }

    public enum BlogImgType
    {
        Avatar,
        Logo,
        HeaderBg
    }
}
