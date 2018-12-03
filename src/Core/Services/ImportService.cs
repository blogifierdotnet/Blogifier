using Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Core.Services
{
    public interface IImportService
    {
        Task<List<ImportMessage>> Import(IFormFile file, string user, string webRoot = "/");
        Task<List<ImportMessage>> Import(string fileName, string user, string webRoot = "/");
    }

    public class ImportService : IImportService
    {
        IDataService _db;
        IStorageService _ss;
        List<ImportMessage> _msgs;
        string _usr;
        string _url;
        string _webRoot;

        public ImportService(IDataService db, IStorageService ss)
        {
            _db = db;
            _ss = ss;
            _msgs = new List<ImportMessage>();
        }

        public async Task<List<ImportMessage>> Import(IFormFile file, string usr, string webRoot = "/")
        {
            _usr = usr;
            _webRoot = webRoot;
            return await ImportFeed(new StreamReader(file.OpenReadStream(), Encoding.UTF8));
        }

        public async Task<List<ImportMessage>> Import(string fileName, string usr, string webRoot = "/")
        {
            _usr = usr;
            _webRoot = webRoot;
            return await ImportFeed(new StreamReader(fileName, Encoding.UTF8));
        }

        async Task<List<ImportMessage>> ImportFeed(StreamReader reader)
        {
            using (var xmlReader = XmlReader.Create(reader, new XmlReaderSettings() { }))
            {
                var feedReader = new RssFeedReader(xmlReader);

                while (await feedReader.Read())
                {
                    if (feedReader.ElementType == SyndicationElementType.Link)
                    {
                        var link = await feedReader.ReadLink();
                        _url = link.Uri.ToString();

                        if (_url.ToLower().EndsWith("/rss"))
                            _url = _url.Substring(0, _url.Length - 4);

                        if (_url.EndsWith("/"))
                            _url = _url.Substring(0, _url.Length - 1);
                    }

                    if (feedReader.ElementType == SyndicationElementType.Item)
                    {
                        try
                        {
                            var item = await feedReader.ReadItem();

                            PostItem post = new PostItem
                            {
                                Author = await _db.Authors.GetItem(a => a.AppUserName == _usr),
                                Title = item.Title,
                                Description = item.Title,
                                Content = item.Description,
                                Slug = await GetSlug(item.Title),
                                Published = item.Published.DateTime,
                                Status = SaveStatus.Publishing
                            };

                            if(item.Categories != null)
                            {
                                var blogCats = new List<string>();
                                foreach (var cat in item.Categories)
                                {
                                    blogCats.Add(cat.Name);
                                }
                                post.Categories = string.Join(",", blogCats);
                            }

                            _msgs.Add(new ImportMessage { ImportType = ImportType.Post, Status = Status.Success, Message = post.Title });

                            await ImportPost(post);
                        }
                        catch (Exception ex)
                        {
                            _msgs.Add(new ImportMessage { ImportType = ImportType.Post, Status = Status.Error, Message = ex.Message });
                        }
                    }
                }
            }
            return await Task.FromResult(_msgs);
        }

        async Task ImportPost(PostItem post)
        {
            await ImportImages(post);
            await ImportFiles(post);

            var converter = new ReverseMarkdown.Converter();
            post.Content = converter.Convert(post.Content);

            var blog = await _db.CustomFields.GetBlogSettings();
            post.Cover = blog.Cover;

            await _db.BlogPosts.SaveItem(post);
        }

        async Task ImportImages(PostItem post)
        {
            var links = new List<ImportAsset>();
            string rgx = @"<img[^>]*?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>";

            if (string.IsNullOrEmpty(post.Content))
                return;

            var matches = Regex.Matches(post.Content, rgx, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            if (matches != null)
            {
                foreach (Match m in matches)
                {
                    var uri = "";
                    try
                    {
                        var tag = m.Groups[0].Value;
                        var path = string.Format("{0}/{1}", post.Published.Year, post.Published.Month);

                        uri = Regex.Match(tag, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;

                        uri = ValidateUrl(uri);
                        
                        AssetItem asset;
                        if (uri.Contains("data:image"))
                        {
                            asset = await _ss.UploadBase64Image(uri, _webRoot, path);
                        }
                        else
                        {
                            asset = await _ss.UploadFromWeb(new Uri(uri), _webRoot, path);
                        }

                        var mdTag = $"![{asset.Title}]({_webRoot}{asset.Url})";

                        post.Content = post.Content.ReplaceIgnoreCase(tag, mdTag);

                        _msgs.Add(new ImportMessage
                        {
                            ImportType = ImportType.Image,
                            Status = Status.Success,
                            Message = $"{tag} -> {mdTag}"
                        });
                    }
                    catch (Exception ex)
                    {
                        _msgs.Add(new ImportMessage
                        {
                            ImportType = ImportType.Image,
                            Status = Status.Error,
                            Message = $"{m.Groups[0].Value} -> {uri} ->{ex.Message}"
                        });
                    }
                }
            }
        }

        async Task ImportFiles(PostItem post)
        {
            var links = new List<ImportAsset>();
            var rgx = @"(?i)<a\b[^>]*?>(?<text>.*?)</a>";
            string[] exts = AppSettings.ImportTypes.Split(',');

            if (string.IsNullOrEmpty(post.Content))
                return;

            MatchCollection matches = Regex.Matches(post.Content, rgx, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            if (matches != null)
            {
                foreach (Match m in matches)
                {
                    try
                    {
                        var tag = m.Value;
                        var src = XElement.Parse(tag).Attribute("href").Value;
                        var mdTag = "";

                        foreach (var ext in exts)
                        {
                            if (src.ToLower().EndsWith($".{ext}"))
                            {
                                var uri = ValidateUrl(src);
                                var path = string.Format("{0}/{1}", post.Published.Year, post.Published.Month);
                                var asset = await _ss.UploadFromWeb(new Uri(uri), _webRoot, path);

                                mdTag = $"[{asset.Title}]({_webRoot}{asset.Url})";

                                post.Content = post.Content.ReplaceIgnoreCase(m.Value, mdTag);

                                _msgs.Add(new ImportMessage
                                {
                                    ImportType = ImportType.Attachement,
                                    Status = Status.Success,
                                    Message = $"{tag} -> {mdTag}"
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _msgs.Add(new ImportMessage {
                            ImportType = ImportType.Attachement,
                            Status = Status.Error,
                            Message = $"{m.Value} -> {ex.Message}"
                        });
                    }
                }
            }
        }

        async Task<string> GetSlug(string title)
        {
            string slug = title.ToSlug();
            BlogPost post;

            post = _db.BlogPosts.Single(p => p.Slug == slug);

            if (post == null)
                return await Task.FromResult(slug);

            for (int i = 2; i < 100; i++)
            {
                post = _db.BlogPosts.Single(p => p.Slug == $"{slug}{i}");

                if (post == null)
                {
                    return await Task.FromResult(slug + i.ToString());
                }
            }

            return await Task.FromResult(slug);
        }

        string ValidateUrl(string link)
        {
            var url = link;

            if (url.StartsWith("~"))
            {
                url = url.Replace("~", _url);
            }
            if (url.StartsWith("/"))
            {
                url = string.Concat(_url, url);
            }
            return url;
        }
    }

    public class ImportMessage
    {
        public ImportType ImportType { get; set; }
        public Status Status { get; set; }
        public string Message { get; set; }
    }

    public class ImportAsset
    {
        public AssetType AssetType { get; set; }
        public string Tag { get; set; }
        public string Src { get; set; }
    }

    public enum Status
    {
        Success, Warning, Error
    }

    public enum ImportType
    {
        Post, Image, Attachement
    }
}