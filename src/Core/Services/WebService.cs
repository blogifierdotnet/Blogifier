using Microsoft.Extensions.Configuration;
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
        IConfiguration _config;
        static HttpClient client = new HttpClient();

        public WebService(IDataService db, IConfiguration config)
        {
            _db = db;
            _config = config;

            // required by Github
            if (!client.DefaultRequestHeaders.Contains("User-Agent"))
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Blogifier");
            }
        }

        public async Task<string> CheckForLatestRelease()
        {
            string result = "";

            HttpResponseMessage response = await client.GetAsync(getGithubUrl());

            if (response.IsSuccessStatusCode)
            {
                var repo = await response.Content.ReadAsAsync<Data.Github.Repository>();

                int current, latest;

                int.TryParse(AppSettings.Version.Replace(".", "").Substring(0, 2), out current);
                int.TryParse(repo.tag_name.ReplaceIgnoreCase("v", "").Replace(".", "").Substring(0, 2), out latest);

                // at least version 2.1.x.x for auto-upgrade
                if(current > 20 && current < latest)
                {
                    var dwnUrl = repo.assets[0].browser_download_url;
                    result = $"The new Blogifier <a href='{repo.html_url}' class='alert-link' target='_blank'>{repo.name}</a> is available for download";

                    var field = _db.CustomFields.Single(f => f.Name == Constants.NewestVersion && f.AuthorId == 0);

                    if (field == null)
                    {
                        _db.CustomFields.Add(new Data.CustomField
                        {
                            AuthorId = 0,
                            Name = Constants.NewestVersion,
                            Content = latest.ToString()
                        });
                        _db.Complete();
                    }
                    else
                    {
                        if (int.Parse(field.Content) < latest)
                        {
                            await _db.CustomFields.SaveCustomValue(Constants.NewestVersion, latest.ToString());
                        }
                    }
                }
            }

            return await Task.FromResult(result);
        }

        public async Task<string> DownloadLatestRelease()
        {
            var msg = "";
            try
            {
                HttpResponseMessage response = await client.GetAsync(getGithubUrl());
                if (response.IsSuccessStatusCode)
                {
                    var repo = await response.Content.ReadAsAsync<Data.Github.Repository>();
                    var zipUrl = repo.assets[0].browser_download_url;
                    var zipPath = $"{Constants.UpgradeDirectory}{Path.DirectorySeparatorChar.ToString()}{repo.tag_name}.zip";

                    using (var client = new HttpClient())
                    {
                        using (var result = await client.GetAsync(zipUrl))
                        {
                            if (result.IsSuccessStatusCode)
                            {
                                var zipBites = await result.Content.ReadAsByteArrayAsync();

                                if (!Directory.Exists(Constants.UpgradeDirectory))
                                    Directory.CreateDirectory(Constants.UpgradeDirectory);

                                File.WriteAllBytes(zipPath, zipBites);

                                ZipFile.ExtractToDirectory(zipPath, Constants.UpgradeDirectory);
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

        string getGithubUrl()
        {
            var section = _config.GetSection(Constants.ConfigSectionKey);

            return (section != null && !string.IsNullOrEmpty(section.GetValue<string>(Constants.ConfigRepoKey))) ?
                section.GetValue<string>(Constants.ConfigRepoKey) :
                "https://api.github.com/repos/blogifierdotnet/Blogifier/releases/latest";
        }
    }
}