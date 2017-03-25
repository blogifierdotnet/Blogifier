using System;
using System.Collections.Generic;

namespace Blogifier.Core.Services.Syndication.Rss
{
    public class FeedItem
    {
        public Author Author { get; set; }
        public string Body { get; set; }
        public ICollection<string> Categories { get; set; } = new List<string>();
        public Uri Comments { get; set; }
        public Uri Link { get; set; }
        public string Permalink { get; set; }
        public DateTime PublishDate { get; set; }
        public string Title { get; set; }
    }
}