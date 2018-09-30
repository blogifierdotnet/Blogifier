using Microsoft.AspNetCore.Hosting;
using System;
using System.Diagnostics;

namespace App.Pages.Admin.Upgrade
{
    public class UpgradeModel : AdminPageModel
    {
        private IApplicationLifetime ApplicationLifetime { get; set; }

        public bool IsUpgrading = false;

        public UpgradeModel(IApplicationLifetime appLifetime)
        {
            ApplicationLifetime = appLifetime;
        }

        public void OnGet()
        {
            // check for newer version
        }

        public void OnPost()
        {
            IsUpgrading = true;

            // Download new version
            //string zipPath = @"c:\demo\foo.zip";
            //string extractPath = @"c:\demo\extract";
            //ZipFile.ExtractToDirectory(zipPath, extractPath);

            // start upgrade process
            //Process p = new Process();
            //p.StartInfo.FileName = "dotnet";
            //p.StartInfo.Arguments = "Upgrade.dll";
            //p.StartInfo.UseShellExecute = false;
            //p.StartInfo.CreateNoWindow = false;
            //p.Start();

            ApplicationLifetime.StopApplication();

            //Program.Main(null);

            //Process.GetCurrentProcess().Kill();
            //Environment.Exit(0);
            //Program.Shutdown();
        }
    }
}