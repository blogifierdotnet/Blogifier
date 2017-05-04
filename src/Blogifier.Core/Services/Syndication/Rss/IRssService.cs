using System.Threading.Tasks;
using Blogifier.Core.Data.Models;

namespace Blogifier.Core.Services.Syndication.Rss
{
    public interface IRssService
    {
        Task Import(RssImportModel model, string root);
        string Display(string absoluteUri);
    }
}
