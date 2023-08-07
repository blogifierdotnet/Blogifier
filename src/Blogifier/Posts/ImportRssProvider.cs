using Blogifier.Extensions;
using Blogifier.Shared;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;

namespace Blogifier.Posts;

public class ImportRssProvider
{
  public ImportDto Analysis(string feedUrl)
  {
    using var xml = XmlReader.Create(feedUrl);
    var feed = SyndicationFeed.Load(xml);

    var result = new ImportDto
    {
      BaseUrl = feed.Id,
      Posts = new List<PostEditorDto>(),
    };

    foreach (var item in feed.Items)
    {
      var content = ((TextSyndicationContent)item.Content).Text;
      var post = new PostEditorDto
      {
        Slug = item.Id,
        Title = item.Title.Text,
        Description = GetDescription(item.Summary.Text),
        Content = content,
        PublishedAt = item.PublishDate.DateTime,
        PostType = PostType.Post,
      };

      if (item.ElementExtensions != null)
      {
        foreach (SyndicationElementExtension ext in item.ElementExtensions)
        {
          if (ext.GetObject<XElement>().Name.LocalName == "summary")
            post.Description = GetDescription(ext.GetObject<XElement>().Value);

          if (ext.GetObject<XElement>().Name.LocalName == "cover")
            post.Cover = ext.GetObject<XElement>().Value;
        }
      }

      if (item.Categories != null)
      {
        post.Categories ??= new List<CategoryDto>();
        foreach (var category in item.Categories)
        {
          post.Categories.Add(new CategoryDto
          {
            Content = category.Name
          });
        }
      };
      result.Posts.Add(post);
    }
    return result;
  }

  static string GetDescription(string description)
  {
    description = description.StripHtml();
    if (description.Length > 450) description = description.Substring(0, 446) + "...";
    return description;
  }
}
