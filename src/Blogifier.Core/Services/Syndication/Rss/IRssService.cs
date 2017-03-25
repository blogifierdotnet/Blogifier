using System.Threading.Tasks;

namespace Blogifier.Core.Services.Syndication.Rss
{
    public interface IRssService
    {
        Task Import(RssImportModel model, string root);
    }
}
