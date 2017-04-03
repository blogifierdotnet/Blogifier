using Blogifier.Core.Data.Domain;

namespace Blogifier.Core.Data.Interfaces
{
    public interface IProfileRepository : IRepository<Profile>
    {

    }

    public enum BlogImgType
    {
        Avatar,
        Logo,
        HeaderBg
    }
}
