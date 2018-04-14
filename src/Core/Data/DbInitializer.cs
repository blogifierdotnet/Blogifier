using Core.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace Core.Data
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context, UserManager<AppUser> userManager)
        {
            if (context.BlogPosts.Any())
                return;

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
                            Title = "Post One",
                            Slug = "post-one",
                            Description = "The post one",
                            Content = "This is post one",
                            UserId = userId,
                            Cover = "data/admin/Sample1.png",
                            PostViews = 5,
                            Rating = 4.5,
                            Published = DateTime.UtcNow.AddDays(-100)
                        },
                        new BlogPost{
                            Title = "Post Two",
                            Slug = "post-two",
                            Description = "The post two",
                            Content = "This is post two",
                            UserId = userId,
                            Cover = "data/admin/Sample2.png",
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
                            Title = "Sample1",
                            Length = 1000,
                            UserId = userId,
                            Path = @"data\admin\Sample1.png",
                            Url = "data/admin/Sample1.png"
                        },
                        new Asset
                        {
                            AssetType = AssetType.Image,
                            DownloadCount = 200,
                            Published = DateTime.Now.AddDays(-10),
                            Title = "Sample2",
                            Length = 2000,
                            UserId = userId,
                            Path = @"data\admin\Sample2.png",
                            Url = "data/admin/Sample2.png"
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
                        }
                    };

                    foreach (Asset a in assets)
                    {
                        context.Assets.Add(a);
                    }
                }

                var user2 = new AppUser {
                    UserName = "user",
                    Email = "user@us.com",
                    DisplayName = "John Doe",
                    Avatar = "data/user/avatar.jpg",
                    Created = SystemClock.Now().AddDays(-140)
                };
                IdentityResult result2 = userManager.CreateAsync
                    (user2, "User@pass1").Result;

                if (result2.Succeeded)
                {
                    context.BlogPosts.Add(new BlogPost
                    {
                        Title = "Post Three",
                        Slug = "post-three",
                        Description = "The post three",
                        Content = "This is post three",
                        UserId = userManager.FindByNameAsync("user").Result.Id,
                        Cover = "data/admin/Sample1.png",
                        PostViews = 25,
                        Rating = 3.5,
                        Published = DateTime.UtcNow.AddDays(-10)
                    });

                    var asset2 = new Asset
                    {
                        AssetType = AssetType.Image,
                        DownloadCount = 0,
                        Published = DateTime.Now.AddDays(-8),
                        Title = "Avatar",
                        Length = 2000,
                        UserId = user2.Id,
                        Path = @"data\user\avatar.jpg",
                        Url = "data/user/avatar.jpg"
                    };
                    context.Assets.Add(asset2);
                }

                context.SaveChanges();
            }
        }
    }
}