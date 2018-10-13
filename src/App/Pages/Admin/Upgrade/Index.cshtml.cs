using Core.Services;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace App.Pages.Admin.Upgrade
{
    public class UpgradeModel : AdminPageModel
    {
        private IApplicationLifetime _app;
        private IWebService _ws;

        public bool IsUpgrading = false;

        public UpgradeModel(IApplicationLifetime app, IWebService ws)
        {
            _app = app;
            _ws = ws;
        }

        public void OnGet()
        {
            // check for newer version
        }

        public async Task OnPost()
        {
            IsUpgrading = true;

            if (string.IsNullOrEmpty(await _ws.DownloadLatestRelease()))
            {
                // start upgrade process
                //Process p = new Process();
                //p.StartInfo.FileName = "dotnet";
                //p.StartInfo.Arguments = "Upgrade.dll";
                //p.StartInfo.UseShellExecute = false;
                //p.StartInfo.CreateNoWindow = false;
                //p.Start();

                //_app.StopApplication();

                //Program.Main(null);
                //Process.GetCurrentProcess().Kill();
                //Environment.Exit(0);
                //Program.Shutdown();
            }
        }
    }
}