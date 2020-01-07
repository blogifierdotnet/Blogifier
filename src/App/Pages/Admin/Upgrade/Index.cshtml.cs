using Core;
using Core.Services;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Threading.Tasks;

namespace App.Pages.Admin.Upgrade
{
    public class UpgradeModel : AdminPageModel
    {
        private IHostApplicationLifetime _app;
        private IWebService _ws;
        private IDataService _db;

        public bool IsUpgrading = false;
        public string OldVersion;
        public string NewVersion;

        public UpgradeModel(IHostApplicationLifetime app, IWebService ws, IDataService db)
        {
            _app = app;
            _ws = ws;
            _db = db;
        }

        public void OnGet()
        {
            OldVersion = AppSettings.Version.Substring(0, 3);

            var field = _db.CustomFields.Single(f => f.Name == Constants.NewestVersion && f.AuthorId == 0);

            if(field != null)
            {
                NewVersion = field.Content.Substring(0, 1) + "." + field.Content.Substring(1, 1);
            }
        }

        public async Task OnPost()
        {
            IsUpgrading = true;

            if (string.IsNullOrEmpty(await _ws.DownloadLatestRelease()))
            {
                // start upgrade process
                Process p = new Process();
                p.StartInfo.FileName = "dotnet";
                p.StartInfo.Arguments = "Upgrade.dll";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = false;
                p.Start();

                _app.StopApplication();
            }
        }
    }
}