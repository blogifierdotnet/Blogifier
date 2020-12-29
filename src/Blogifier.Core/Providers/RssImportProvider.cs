using Blogifier.Core.Data;
using Blogifier.Core.Extensions;
using Blogifier.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Blogifier.Core.Providers
{
	public interface IRssImportProvider
	{
      Task<ImportMessage> ImportSyndicationItem(SyndicationItem syndicationItem, int userId, Uri baseUrl, string webRoot = "/");
   }

	public class RssImportProvider : IRssImportProvider
	{
      private readonly AppDbContext _dbContext;
      private readonly IStorageProvider _storageProvider;
      private readonly string _defaultCover = "img/cover.png";
      private int _userId;
      private string _webRoot;
      private Uri _baseUrl;

      public RssImportProvider(AppDbContext dbContext, IStorageProvider storageProvider)
      {
         _dbContext = dbContext;
         _storageProvider = storageProvider;
      }

      public async Task<ImportMessage> ImportSyndicationItem(SyndicationItem syndicationItem, int userId, Uri baseUrl, string webRoot = "/")
      {
         _userId = userId;
         _webRoot = webRoot;
         _baseUrl = baseUrl;

			try
			{
            Post post = await GetPost(syndicationItem);

				if (!(await ImportPost(post)))
               return new ImportMessage
               {
                  Status = Status.Error,
                  Message = $"{post.Title} - failed to save..."
               };

            return new ImportMessage
            {
               Status = Status.Success,
               Message = $"{post.Title} - completed..."
            };
         }
			catch (Exception ex)
			{
            return new ImportMessage
            {
               Status = Status.Error,
               Message = ex.Message
            };
			}
      }

      async Task<Post> GetPost(SyndicationItem syndicationItem)
		{
         Blog blog = await _dbContext.Blogs.FirstOrDefaultAsync();

         Post post = new Post()
         {
            AuthorId = _userId,
            Blog = blog,
            Title = syndicationItem.Title.Text,
            Slug = await GetSlug(syndicationItem.Title.Text),
            Description = syndicationItem.Title.Text,
            Content = syndicationItem.Summary.Text,
            Cover = $"{_webRoot}{_defaultCover}",
            Published = syndicationItem.PublishDate.DateTime,
            DateCreated = syndicationItem.PublishDate.DateTime,
            DateUpdated = syndicationItem.LastUpdatedTime.DateTime
         };

         if (syndicationItem.ElementExtensions != null)
         {
            foreach (SyndicationElementExtension ext in syndicationItem.ElementExtensions)
            {
               if (ext.GetObject<XElement>().Name.LocalName == "summary")
                  post.Description = ext.GetObject<XElement>().Value;

               if (ext.GetObject<XElement>().Name.LocalName == "cover")
					{
						post.Cover = ext.GetObject<XElement>().Value;
						var path = string.Format("{0}/{1}/{2}", post.AuthorId, post.Published.Year, post.Published.Month);

						var mdTag = await _storageProvider.UploadFromWeb(new Uri(post.Cover), _webRoot, path);
						if(mdTag.Length > 0 && mdTag.IndexOf("(") > 2)
							post.Cover = mdTag.Substring(mdTag.IndexOf("(") + 2).Replace(")", "");
					}
            }
         }

         if (syndicationItem.Categories != null)
         {
            if (post.Categories == null)
               post.Categories = new List<Category>();

            foreach (var category in syndicationItem.Categories)
            {
               post.Categories.Add(new Category()
               {
                  Content = category.Name,
                  DateCreated = DateTime.UtcNow,
                  DateUpdated = DateTime.UtcNow
               });
            }
         }

         return post;
      }

		async Task<bool> ImportPost(Post post)
		{
			await ImportImages(post);
			await ImportFiles(post);

			var converter = new ReverseMarkdown.Converter();

			post.Description = converter.Convert(post.Description);
         post.Content = converter.Convert(post.Content);

         await _dbContext.Posts.AddAsync(post);
         return await _dbContext.SaveChangesAsync() > 0;
      }

		async Task ImportImages(Post post)
		{
			string rgx = @"<img[^>]*?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>";

			if (string.IsNullOrEmpty(post.Content))
				return;

			var matches = Regex.Matches(post.Content, rgx, RegexOptions.IgnoreCase | RegexOptions.Singleline);

         if (matches == null)
            return;
         
			foreach (Match m in matches)
			{
				try
				{
					var tag = m.Groups[0].Value;
					var path = string.Format("{0}/{1}/{2}", post.AuthorId, post.Published.Year, post.Published.Month);

					var uri = Regex.Match(tag, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
					uri = ValidateUrl(uri);
					var mdTag = "";

					if (uri.Contains("data:image"))
					   mdTag = await _storageProvider.UploadBase64Image(uri, _webRoot, path);
					else
						mdTag = await _storageProvider.UploadFromWeb(new Uri(uri), _webRoot, path);

					post.Content = post.Content.ReplaceIgnoreCase(tag, mdTag);
				}
				catch (Exception ex)
				{
					Serilog.Log.Error($"Error importing images: {ex.Message}");
				}
			}
		}

		async Task ImportFiles(Post post)
		{
			var rgx = @"(?i)<a\b[^>]*?>(?<text>.*?)</a>";
			string[] exts = new string[] { "zip", "7z", "xml", "pdf", "doc", "docx", "xls", "xlsx", "mp3", "mp4", "avi" };

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
								var path = string.Format("{0}/{1}/{2}", post.AuthorId, post.Published.Year, post.Published.Month);

								mdTag = await _storageProvider.UploadFromWeb(new Uri(uri), _webRoot, path);

								if (mdTag.StartsWith("!"))
									mdTag = mdTag.Substring(1);

								post.Content = post.Content.ReplaceIgnoreCase(m.Value, mdTag);
							}
						}
					}
					catch (Exception ex)
					{
						Serilog.Log.Error($"Error importing files: {ex.Message}");
					}
				}
			}
		}

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

			var baseUrl = _baseUrl.ToString();
			if (baseUrl.EndsWith("/"))
				baseUrl = baseUrl.Substring(0, baseUrl.Length - 1);

         if (url.StartsWith("~"))
            url = url.Replace("~", baseUrl);

         if (url.StartsWith("/"))
            url = $"{baseUrl}{url}";

			if (!(url.StartsWith("http:") || url.StartsWith("https:")))
				url = $"{baseUrl}/{url}";

         return url;
      }
   }
}
