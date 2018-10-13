using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IWebService
    {
        Task<string> CheckForLatestRelease();
        Task<string> DownloadLatestRelease();
    }

    public class WebService : IWebService
    {
        IDataService _db;
        static HttpClient client = new HttpClient();
        static string _repoUrl = "https://api.github.com/repos/blogifierdotnet/Blogifier/releases/latest";

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
            HttpResponseMessage response = await client.GetAsync(_repoUrl);

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

                    var field = _db.CustomFields.Single(f => f.Name == Constants.NewestVersion && f.AuthorId == 0);
                    if(field == null || (field != null && int.Parse(field.Content) < latest))
                    {
                        _db.CustomFields.Add(new Data.CustomField
                        {
                            AuthorId = 0,
                            Name = Constants.NewestVersion,
                            Content = latest.ToString()
                        });
                        _db.Complete();
                    }
                }
            }

            return await Task.FromResult(result);
        }

        public async Task<string> DownloadLatestRelease()
        {
            var uloadDir = "_upgrade";
            var msg = "";
            try
            {
                HttpResponseMessage response = await client.GetAsync(_repoUrl);
                if (response.IsSuccessStatusCode)
                {
                    var repo = await response.Content.ReadAsAsync<Data.Github.Repository>();
                    var zipUrl = repo.assets[0].browser_download_url;
                    var zipPath = $"{uloadDir}{Path.DirectorySeparatorChar.ToString()}{repo.tag_name}.zip";

                    using (var client = new HttpClient())
                    {
                        using (var result = await client.GetAsync(zipUrl))
                        {
                            if (result.IsSuccessStatusCode)
                            {
                                var zipBites = await result.Content.ReadAsByteArrayAsync();

                                if (!Directory.Exists(uloadDir))
                                    Directory.CreateDirectory(uloadDir);

                                File.WriteAllBytes(zipPath, zipBites);

                                ZipFile.ExtractToDirectory(zipPath, uloadDir);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                msg = ex.Message;
            }

            return await Task.FromResult(msg);
        }
    }
}