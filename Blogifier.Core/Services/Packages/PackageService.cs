using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Services.Packages
{
    public interface IPackageService
    {
        Task<List<PackageListItem>> Find(PackageType packageType);
        Task<PackageListItem> Single(string id);
    }

    public class PackageService : IPackageService
    {
        private readonly ICompositeViewEngine _engine;
        private readonly IUnitOfWork _db;

        public PackageService(IUnitOfWork db, ICompositeViewEngine engine)
        {
            _db = db;
            _engine = engine;
        }

        public Task<List<PackageListItem>> Find(PackageType packageType)
        {
            if(packageType == PackageType.Widgets)
            {
                return Task.FromResult(Widgets());
            }
            else
            {
                return null;
            }
        }

        public Task<PackageListItem> Single(string id)
        {
            var item = Widgets().Where(w => w.Title == id).FirstOrDefault();
            return Task.FromResult(item);
        }

        List<PackageListItem> Widgets()
        {
            var widgets = new List<PackageListItem>();

            foreach (var assembly in Configuration.GetAssemblies())
            {
                var name = assembly.GetName().Name;

                if (name != "Blogifier.Core")
                {
                    var path = $"~/Views/Shared/Components/{name}/Settings.cshtml";
                    var view = _engine.GetView("", path, false);

                    var item = new PackageListItem
                    {
                        Title = name,
                        Description = name,
                        Version = assembly.GetName().Version.ToString(),
                        LastUpdated = System.IO.File.GetLastWriteTime(assembly.Location)
                    };

                    try
                    {
                        Type t = assembly.GetType("PackageInfo");
                        if (t != null)
                        {
                            var info = (IPackageInfo)Activator.CreateInstance(t);
                            var attributes = info.GetAttributes();
                            if(attributes != null)
                            {
                                item.Author = string.IsNullOrEmpty(attributes.Author) ? "Unknown" : attributes.Author;
                                item.Cover = string.IsNullOrEmpty(attributes.Cover) ? BlogSettings.Cover : attributes.Cover;
                                item.Description = attributes.Description;
                                item.Icon = string.IsNullOrEmpty(attributes.Icon) ? BlogSettings.Logo : attributes.Icon;
                                item.ProjectUrl = attributes.ProjectUrl;
                                item.Tags = attributes.Tags;
                                item.Title = attributes.Title;
                            }
                        }
                    }
                    catch { }

                    var disabled = Disabled();
                    var maxLen = 70;

                    item.Description = item.Description.Length > maxLen ? item.Description.Substring(0, maxLen) + "..." : item.Description;
                    item.HasSettings = view.Success;
                    item.Enabled = disabled == null || !disabled.Contains(name);
                    widgets.Add(item);
                }
            }
            return widgets;
        }

        List<string> Disabled()
        {
            var field = _db.CustomFields.GetValue(CustomType.Application, 0, Constants.DisabledPackages);
            return string.IsNullOrEmpty(field) ? null : field.Split(',').ToList();
        }
    }

    public enum PackageType
    {
        Widgets,
        Themes,
        Plugins
    }
}