using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Upgrade
{
    class Program
    {
        static string _appDir = "";
        static string _upgDir = "";
        static string _slash = Path.DirectorySeparatorChar.ToString();
        static List<string> _items;

        static void Main(string[] args)
        {
            _appDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            _upgDir = $"{_appDir}{_slash}_upgrade";
            _items = new List<string>();

            _items.Add($"STARTING UPGRADE IN: {_appDir}");

            // wait for web app to stop
            System.Threading.Thread.Sleep(3000);

            try
            {
                var files = Directory.GetFiles(_appDir);

                foreach (var file in GetCoreFiles())
                {
                    ReplaceFile(file);
                }

                foreach (var dir in GetCoreFolders())
                {
                    ReplaceFolder(dir);
                }
            }
            catch (Exception ex)
            {
                _items.Add(ex.Message);
            }

            var log = $"upgrade-{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.log";
            using (StreamWriter writer = new StreamWriter(log))
            {
                foreach (var item in _items)
                {
                    writer.WriteLine(item);
                }
            }

            Directory.Delete(_upgDir, true);

            //Process p = new Process();
            //p.StartInfo.FileName = "dotnet";
            //p.StartInfo.Arguments = "App.dll";
            //p.StartInfo.UseShellExecute = false;
            //p.StartInfo.CreateNoWindow = false;
            //p.Start();
        }

        static void ReplaceFile(string file)
        {
            string oldFile = $"{_appDir}{_slash}{file}";
            string newFile = $"{_upgDir}{_slash}{file}";
            try
            {
                if (File.Exists(oldFile))
                    File.Delete(oldFile);
                    
                File.Copy(newFile, oldFile);
                _items.Add($"Replacing file: {oldFile} with {newFile}");
            }
            catch (Exception fe)
            {
                _items.Add($"Error replacing file: {oldFile}: {fe.Message}");
            }
        }

        static void ReplaceFolder(string folder)
        {
            string oldFolder = $"{_appDir}{_slash}{folder}";
            string newFolder = $"{_upgDir}{_slash}{folder}";
            try
            {
                if (Directory.Exists(oldFolder))
                    Directory.Delete(oldFolder, true);

                Directory.Move(newFolder, oldFolder);
                _items.Add($"Replacing folder: {oldFolder} with {newFolder}");
            }
            catch (Exception fe)
            {
                _items.Add($"Error replacing folder: {oldFolder}: {fe.Message}");
            }
        }

        static string NameFromPath(string path)
        {
            int x = path.LastIndexOf(_slash);
            string name = path.Substring(x);
            return name;
        }

        static List<string> GetCoreFiles()
        {
            return new List<string>
            {
                "App.deps.json",
                "App.dll",
                "App.pdb",
                "App.runtimeconfig.json",
                "Common.dll",
                "Core.dll",
                "Core.pdb",
                "HtmlAgilityPack.dll",
                "Markdig.dll",
                "Microsoft.Data.Sqlite.dll",
                "Microsoft.EntityFrameworkCore.Sqlite.dll",
                "Microsoft.SyndicationFeed.ReaderWriter.dll",
                "ReverseMarkdown.dll",
                "Serilog.dll",
                "Serilog.Extensions.Logging.dll",
                "Serilog.Sinks.File.dll",
                "Serilog.Sinks.RollingFile.dll",
                "SQLitePCLRaw.batteries_green.dll",
                "SQLitePCLRaw.batteries_v2.dll",
                "SQLitePCLRaw.core.dll",
                "SQLitePCLRaw.provider.e_sqlite3.dll",
                "System.Xml.XPath.XmlDocument.dll"
            };
        }

        static List<string> GetCoreFolders()
        {
            return new List<string>
            {
                "Pages",
                $"Views{_slash}Shared",
                $"Views{_slash}Themes{_slash}Standard",
                $"wwwroot{_slash}admin",
                $"wwwroot{_slash}lib",
                $"wwwroot{_slash}themes{_slash}simple",
                $"wwwroot{_slash}themes{_slash}standard"
            };
        }
    }
}