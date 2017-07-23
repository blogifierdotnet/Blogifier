﻿using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Blogifier.Core.Common
{
    public class AppSettingsLoader
    {
        public void LoadFromConfigFile(IConfiguration config)
        {
            try
            {
                if (config != null)
                {
                    if (!string.IsNullOrEmpty(config.GetConnectionString("DefaultConnection")))
                        ApplicationSettings.ConnectionString = config.GetConnectionString("DefaultConnection");

                    var section = config.GetSection("Blogifier");
                    if (section != null)
                    {
                        // system settings

                        if (section["SingleBlog"] != null)
                            ApplicationSettings.SingleBlog = section.GetValue<bool>("SingleBlog");

                        if (section["UseInMemoryDatabase"] != null)
                            ApplicationSettings.UseInMemoryDatabase = section.GetValue<bool>("UseInMemoryDatabase");

                        if (section["ConnectionString"] != null)
                            ApplicationSettings.ConnectionString = section.GetValue<string>("ConnectionString");

                        if (section["EnableLogging"] != null)
                            ApplicationSettings.EnableLogging = section.GetValue<bool>("EnableLogging");

                        if (section["BlogStorageFolder"] != null)
                            ApplicationSettings.BlogStorageFolder = section.GetValue<string>("BlogStorageFolder");

                        if (section["SupportedStorageFiles"] != null)
                            ApplicationSettings.SupportedStorageFiles = section.GetValue<string>("SupportedStorageFiles");

                        if (section["AdminTheme"] != null)
                            ApplicationSettings.AdminTheme = section.GetValue<string>("AdminTheme");

                        if (section["BlogTheme"] != null)
                            ApplicationSettings.BlogTheme = section.GetValue<string>("BlogTheme");

                        // applicatin settings

                        if (section["Title"] != null)
                            ApplicationSettings.Title = section.GetValue<string>("Title");

                        if (section["Description"] != null)
                            ApplicationSettings.Description = section.GetValue<string>("Description");

                        if (section["ItemsPerPage"] != null)
                            ApplicationSettings.ItemsPerPage = section.GetValue<int>("ItemsPerPage");


                        if (section["ProfileAvatar"] != null)
                            ApplicationSettings.ProfileAvatar = section.GetValue<string>("ProfileAvatar");

                        if (section["ProfileLogo"] != null)
                            ApplicationSettings.ProfileLogo = section.GetValue<string>("ProfileLogo");

                        if (section["ProfileImage"] != null)
                            ApplicationSettings.ProfileImage = section.GetValue<string>("ProfileImage");

                        if (section["PostImage"] != null)
                            ApplicationSettings.PostImage = section.GetValue<string>("PostImage");

                        // troubleshooting

                        if (section["AddContentTypeHeaders"] != null)
                            ApplicationSettings.AddContentTypeHeaders = section.GetValue<bool>("AddContentTypeHeaders");

                        if (section["AddContentLengthHeaders"] != null)
                            ApplicationSettings.AddContentLengthHeaders = section.GetValue<bool>("AddContentLengthHeaders");

                        if (section["PrependFileProvider"] != null)
                            ApplicationSettings.PrependFileProvider = section.GetValue<bool>("PrependFileProvider");
                    }
                }
            }
            catch { }
        }
    }
}
