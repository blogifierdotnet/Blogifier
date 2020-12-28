using Blogifier.Core.Data;
using Blogifier.Core.Extensions;
using Blogifier.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Blogifier.Core.Providers
{
	public interface IRssImportProvider
	{
      Task<List<ImportMessage>> Import(IFormFile file, int userId, string webRoot = "/");
      Task<List<ImportMessage>> Import(string fileName, int userId, string webRoot = "/");
   }

	public class RssImportProvider : IRssImportProvider
	{
      private readonly AppDbContext _dbContext;
      private readonly IStorageProvider _storageProvider;
      private readonly List<ImportMessage> _importMessages;
      private readonly string _defaultCover = "img/cover.png";
      private int _userId;
      private string _url;
      private string _webRoot;

      public RssImportProvider(AppDbContext dbContext, IStorageProvider storageProvider)
      {
         _dbContext = dbContext;
         _storageProvider = storageProvider;
         _importMessages = new List<ImportMessage>();
      }

      public async Task<List<ImportMessage>> Import(IFormFile file, int userId, string webRoot = "/")
      {
         _userId = userId;
         _webRoot = webRoot;
         return await ImportFeed(new StreamReader(file.OpenReadStream(), Encoding.UTF8));
      }

      public async Task<List<ImportMessage>> Import(string fileName, int userId, string webRoot = "/")
      {
         _userId = userId;
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
                     ISyndicationItem item = await feedReader.ReadItem();
                     Blog blog = await _dbContext.Blogs.FirstOrDefaultAsync();

                     Post post = new Post()
                     {
                        AuthorId = _userId,
                        Blog = blog,
                        Title = item.Title,
                        Slug = await GetSlug(item.Title),
                        Description = item.Title,
                        Content = item.Description,
                        Cover = $"{_webRoot}{_defaultCover}",
                        Published = item.Published.DateTime,
                        DateCreated = item.Published.DateTime,
                        DateUpdated = item.LastUpdated.DateTime
                     };

                     if (item.Categories != null)
                     {
                        if (post.Categories == null)
                           post.Categories = new List<Category>();

                        foreach (var category in item.Categories)
                        {
                           post.Categories.Add(new Category()
                           {
                              Content = category.Name,
                              DateCreated = DateTime.UtcNow,
                              DateUpdated = DateTime.UtcNow
                           });
                        }
                     }
                     
                     if(await ImportPost(post))
							{
                        _importMessages.Add(new ImportMessage
                        {
                           ImportType = ImportType.Post, Status = Status.Success, Message = post.Title
                        });
                     }
							else
							{
                        _importMessages.Add(new ImportMessage
                        {
                           ImportType = ImportType.Post, Status = Status.Warning, Message = $"Post {post.Title} was not saved to the database"
                        });
                     }
                  }
                  catch (Exception ex)
                  {
                     _importMessages.Add(new ImportMessage { 
                        ImportType = ImportType.Post, Status = Status.Error, Message = $"Error saving post: {ex.Message}" 
                     });
                  }
               }
            }
         }
         return _importMessages;
      }

		async Task<bool> ImportPost(Post post)
		{
			await ImportImages(post);
			//await ImportFiles(post);

			var converter = new ReverseMarkdown.Converter();
			post.Content = converter.Convert(post.Content);

			await _dbContext.Posts.AddAsync(post);
         return await _dbContext.SaveChangesAsync() > 0;
      }

		async Task ImportImages(Post post)
      {
         //var links = new List<ImportAsset>();
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

                  //AssetItem asset;
                  //if (uri.Contains("data:image"))
                  //{
                  //   asset = await _ss.UploadBase64Image(uri, _webRoot, path);
                  //}
                  //else
                  //{
                     var mdTag = await _storageProvider.UploadFromWeb(new Uri(uri), _webRoot, path);
                  //}

                  //var mdTag = $"![{asset.Title}]({_webRoot}{asset.Url})";

                  post.Content = post.Content.ReplaceIgnoreCase(tag, mdTag);

                  _importMessages.Add(new ImportMessage
                  {
                     ImportType = ImportType.Image,
                     Status = Status.Success,
                     Message = $"{tag} -> {mdTag}"
                  });
               }
               catch (Exception ex)
               {
                  _importMessages.Add(new ImportMessage
                  {
                     ImportType = ImportType.Image,
                     Status = Status.Error,
                     Message = $"{m.Groups[0].Value} -> {uri} ->{ex.Message}"
                  });
               }
            }
         }
      }

		//async Task ImportFiles(PostItem post)
		//{
		//   var links = new List<ImportAsset>();
		//   var rgx = @"(?i)<a\b[^>]*?>(?<text>.*?)</a>";
		//   string[] exts = AppSettings.ImportTypes.Split(',');

		//   if (string.IsNullOrEmpty(post.Content))
		//      return;

		//   MatchCollection matches = Regex.Matches(post.Content, rgx, RegexOptions.IgnoreCase | RegexOptions.Singleline);

		//   if (matches != null)
		//   {
		//      foreach (Match m in matches)
		//      {
		//         try
		//         {
		//            var tag = m.Value;
		//            var src = XElement.Parse(tag).Attribute("href").Value;
		//            var mdTag = "";

		//            foreach (var ext in exts)
		//            {
		//               if (src.ToLower().EndsWith($".{ext}"))
		//               {
		//                  var uri = ValidateUrl(src);
		//                  var path = string.Format("{0}/{1}", post.Published.Year, post.Published.Month);
		//                  var asset = await _ss.UploadFromWeb(new Uri(uri), _webRoot, path);

		//                  mdTag = $"[{asset.Title}]({_webRoot}{asset.Url})";

		//                  post.Content = post.Content.ReplaceIgnoreCase(m.Value, mdTag);

		//                  _msgs.Add(new ImportMessage
		//                  {
		//                     ImportType = ImportType.Attachement,
		//                     Status = Status.Success,
		//                     Message = $"{tag} -> {mdTag}"
		//                  });
		//               }
		//            }
		//         }
		//         catch (Exception ex)
		//         {
		//            _msgs.Add(new ImportMessage
		//            {
		//               ImportType = ImportType.Attachement,
		//               Status = Status.Error,
		//               Message = $"{m.Value} -> {ex.Message}"
		//            });
		//         }
		//      }
		//   }
		//}

		async Task<string> GetSlug(string title)
		{
			string slug = title.ToSlug();
			Post post = await _dbContext.Posts.SingleOrDefaultAsync(p => p.Slug == slug);

			if (post == null)
				return slug;

			for (int i = 2; i < 100; i++)
			{
				post = await _dbContext.Posts.AsNoTracking()
               .SingleAsync(p => p.Slug == $"{slug}{i}");

				if (post == null)
					return await Task.FromResult(slug + i.ToString());
			}
			return slug;
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
}
