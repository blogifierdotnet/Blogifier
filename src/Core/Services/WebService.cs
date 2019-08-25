using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IWebService
    {
        Task<string> CheckForLatestRelease();
        Task<string> DownloadLatestRelease();
        Task<List<string>> GetNotifications();
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

            HttpResponseMessage response = await client.GetAsync(getGithubRepoUrl());

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
                HttpResponseMessage response = await client.GetAsync(getGithubRepoUrl());
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

        public async Task<List<string>> GetNotifications()
        {
            var notifications = new List<string>();
            HttpResponseMessage response = await client.GetAsync(getGithubNotificationsUrl());

            if (response.IsSuccessStatusCode)
            {
                var folder = await response.Content.ReadAsAsync<List<Data.Github.GithubFile>>();
                if(folder != null && folder.Count > 0)
                {
                    foreach (var file in folder)
                    {
                        var message = ReadFileFromUrl(file.download_url);
                        notifications.Add(message);
                    }
                }
            }
            return await Task.FromResult(notifications);
        }

        string ReadFileFromUrl(string url)
        {
            var webRequest = WebRequest.Create(url);

            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                return reader.ReadToEnd();
            }
        }

        string getGithubRepoUrl()
        {
            var section = _config.GetSection(Constants.ConfigSectionKey);

            return (section != null && !string.IsNullOrEmpty(section.GetValue<string>(Constants.ConfigRepoKey))) ?
                section.GetValue<string>(Constants.ConfigRepoKey) :
                "https://api.github.com/repos/blogifierdotnet/Blogifier/releases/latest";
        }

        string getGithubNotificationsUrl()
        {
            var section = _config.GetSection(Constants.ConfigSectionKey);

            return (section != null && !string.IsNullOrEmpty(section.GetValue<string>(Constants.ConfigNotificationsKey))) ?
                section.GetValue<string>(Constants.ConfigNotificationsKey) :
                "https://api.github.com/repos/blogifierdotnet/Upgrade/contents/Notifications";
        }
    }
}