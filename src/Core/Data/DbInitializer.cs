using Core.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.IO;
using System.Linq;

namespace Core.Data
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context, UserManager<AppUser> userManager, IStorageService storage)
        {
            if (context.BlogPosts.Any())
                return;

            ReloadStorage(storage);

            if (userManager.FindByNameAsync("admin").Result == null)
            {
                // app settings

                context.Settings.Add(new Setting { SettingKey = "app-title", SettingValue = "Blog title" });
                context.Settings.Add(new Setting { SettingKey = "app-desc", SettingValue = "Short description of the blog" });

                // content

                var user = new AppUser {
                    UserName = "admin",
                    Email = "admin@us.com",
                    DisplayName = "Administrator",
                    Avatar = "data/admin/avatar.png",
                    Created = SystemClock.Now().AddDays(-150),
                    IsAdmin = true
                };

                IdentityResult result = userManager.CreateAsync
                    (user, "Admin@pass1").Result;

                if (result.Succeeded)
                {
                    var userId = userManager.FindByNameAsync("admin").Result.Id;

                    var posts = new BlogPost[]
                    {
                        new BlogPost{
                            Title = "Welcome to Blogifier!",
                            Slug = "welcome-to-blogifier!",
                            Description = "Blogifier is simple, beautiful, light-weight open source blog written in .NET Core. This cross-platform, highly extendable and customizable web application brings all the best blogging features in small, portable package.",
                            Content = @"## What is Blogifier

Blogifier is simple, beautiful, light-weight open source blog written in .NET Core. This cross-platform, highly extendable and customizable web application brings all the best blogging features in small, portable package.

## System Requirements

* Windows, Mac or Linux
* ASP.NET Core 2.0
* Visual Studio 2017, VS Code or other code editor (Atom, Sublime etc)
* SQLite by default, MS SQL Server tested, EF compatible databases should work

## Getting Started

1. Clone or download source code
2. Run application in Visual Studio or using your code editor
3. Use admin/Admin@pass1 to log in as admininstrator

## Demo site

The [demo site](http://blogifier.azurewebsites.net) is a playground to check out Blogifier features. You can write and publish posts, upload files and test application before install. And no worries, it is just a sandbox and will clean itself.

![Demo-1.png](/data/admin/admin-editor.png)",
                            UserId = userId,
                            Cover = "data/admin/cover-blog.png",
                            PostViews = 5,
                            Rating = 4.5,
                            Published = DateTime.UtcNow.AddDays(-100)
                        },
                        new BlogPost{
                            Title = "Blogifier Features",
                            Slug = "blogifier-features",
                            Description = "List of the main features supported by Blogifier, includes user management, content management, plugin system, markdown editor, simple search and others. This is not the full list and work in progress.",
                            Content = @"### User Management
Blogifier is multi-user application with simple admin/user roles, allowing every user write and publish posts and administrator create new users.

### Content Management
Built-in file manager allows upload images and files and use them as links in the post editor.

![file-mgr.png](/data/admin/admin-files.png)

### Plugin System
Blogifier built as highly extendable application allowing themes, widgets and modules to be side-loaded and added to blog at runtime.

### Markdown Editor
The post editor uses markdown syntax, which many writers prefer over HTML for its simplicity.

### Simple Search
There is simple but quick and functional search in the post lists, as well as search in the image/file list in the file manager.

### Features in the work
* Categories
* RSS Feed
* Plugin management",
                            UserId = userId,
                            Cover = "data/admin/cover-globe.png",
                            PostViews = 15,
                            Rating = 4.0,
                            Published = DateTime.UtcNow.AddDays(-55)
                        }
                    };

                    foreach (BlogPost p in posts)
                    {
                        context.BlogPosts.Add(p);
                    }

                    var assets = new Asset[]
                    {
                        new Asset
                        {
                            AssetType = AssetType.Image,
                            DownloadCount = 100,
                            Published = DateTime.Now.AddDays(-15),
                            Title = "admin-editor",
                            Length = 1000,
                            UserId = userId,
                            Path = @"data\admin\admin-editor.png",
                            Url = "data/admin/admin-editor.png"
                        },
                        new Asset
                        {
                            AssetType = AssetType.Image,
                            DownloadCount = 200,
                            Published = DateTime.Now.AddDays(-10),
                            Title = "admin-files",
                            Length = 2000,
                            UserId = userId,
                            Path = @"data\admin\admin-files.png",
                            Url = "data/admin/admin-files.png"
                        },
                        new Asset
                        {
                            AssetType = AssetType.Image,
                            DownloadCount = 0,
                            Published = DateTime.Now.AddDays(-10),
                            Title = "Avatar",
                            Length = 2000,
                            UserId = userId,
                            Path = @"data\admin\avatar.png",
                            Url = "data/admin/avatar.png"
                        },

                        new Asset
                        {
                            AssetType = AssetType.Image,
                            DownloadCount = 0,
                            Published = DateTime.Now.AddDays(-10),
                            Title = "cover-blog",
                            Length = 2000,
                            UserId = userId,
                            Path = @"data\admin\cover-blog.png",
                            Url = "data/admin/cover-blog.png"
                        },
                        new Asset
                        {
                            AssetType = AssetType.Image,
                            DownloadCount = 0,
                            Published = DateTime.Now.AddDays(-10),
                            Title = "cover-desk",
                            Length = 2000,
                            UserId = userId,
                            Path = @"data\admin\cover-desk.jpg",
                            Url = "data/admin/cover-desk.jpg"
                        },
                        new Asset
                        {
                            AssetType = AssetType.Image,
                            DownloadCount = 0,
                            Published = DateTime.Now.AddDays(-10),
                            Title = "cover-globe",
                            Length = 2000,
                            UserId = userId,
                            Path = @"data\admin\cover-globe.png",
                            Url = "data/admin/cover-globe.png"
                        }
                    };

                    foreach (Asset a in assets)
                    {
                        context.Assets.Add(a);
                    }
                }

                var user2 = new AppUser {
                    UserName = "demo",
                    Email = "demo@us.com",
                    DisplayName = "Demo Account",
                    Created = SystemClock.Now().AddDays(-140)
                };
                IdentityResult result2 = userManager.CreateAsync
                    (user2, "Demo@pass1").Result;

                if (result2.Succeeded)
                {
                    context.BlogPosts.Add(new BlogPost
                    {
                        Title = "Demo post",
                        Slug = "demo-post",
                        Description = "This demo site is a sandbox to test Blogifier features. It runs in-memory and does not save any data, so you can try everything without making any mess. Have fun!",
                        Content = @"This demo site is a sandbox to test Blogifier features. It runs in-memory and does not save any data, so you can try everything without making any mess. Have fun!

#### To login:
* User: demo
* Pswd: Demo@pass1",
                        UserId = userManager.FindByNameAsync("demo").Result.Id,
                        Cover = "data/demo/demo-cover.jpg",
                        PostViews = 25,
                        Rating = 3.5,
                        Published = DateTime.UtcNow.AddDays(-10)
                    });

                    var asset2 = new Asset
                    {
                        AssetType = AssetType.Image,
                        DownloadCount = 0,
                        Published = DateTime.Now.AddDays(-8),
                        Title = "demo-cover.jpg",
                        Length = 2000,
                        UserId = user2.Id,
                        Path = @"data\demo\demo-cover.jpg",
                        Url = "data/demo/demo-cover.jpg"
                    };
                    context.Assets.Add(asset2);
                }

                context.SaveChanges();
                AppSettings.Cover = "data/admin/cover-desk.jpg";
            }
        }

        static void ReloadStorage(IStorageService storage)
        {
            try
            {
                var dirs = Directory.GetDirectories(storage.Location);
                foreach (var dir in dirs)
                {
                    if (!dir.EndsWith("_init"))
                    {
                        Directory.Delete(dir, true);
                    }
                }
                var srcLoc = Path.Combine(storage.Location, "_init");

                foreach (string dirPath in Directory.GetDirectories(srcLoc, "*",
                    SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(srcLoc, storage.Location));

                foreach (string newPath in Directory.GetFiles(srcLoc, "*.*",
                    SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(srcLoc, storage.Location), true);
            }
            catch { }
        }
    }
}