using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IWebService
    {
        Task<string> CheckForLatestRelease();
    }

    public class WebService : IWebService
    {
        IDataService _db;
        static HttpClient client = new HttpClient();

        public WebService(IDataService db)
        {
            _db = db;

            // required by Github
            if (!client.DefaultRequestHeaders.Contains("User-Agent"))
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Blogifier");
            }
        }

        public async Task<string> CheckForLatestRelease()
        {
            string result = "";
            var url = "https://api.github.com/repos/blogifierdotnet/Blogifier/releases/latest";

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var repo = await response.Content.ReadAsAsync<Data.Github.Repository>();

                int current, latest;

                int.TryParse(AppSettings.Version.Replace(".", "").Substring(0, 2), out current);
                int.TryParse(repo.tag_name.ReplaceIgnoreCase("v", "").Replace(".", "").Substring(0, 2), out latest);

                if(current < latest)
                {
                    var dwnUrl = repo.assets[0].browser_download_url;
                    result = $"The new Blogifier <a href='{repo.html_url}' class='alert-link' target='_blank'>{repo.name}</a> is available for download";
                }
            }

            return await Task.FromResult(result);
        }
    }
}