using Core.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Data
{
    public class PostListModel
    {
        public IEnumerable<PostItem> Posts { get; set; }
        public Pager Pager { get; set; }
    }

    public class PostListFilter
    {
        HttpRequest _req;

        public PostListFilter(HttpRequest request)
        {
            _req = request;
        }

        public string Page { get {
                return string.IsNullOrEmpty(_req.Query["page"])
                    ? "" : _req.Query["page"].ToString();
            }
        }
        public string Status { get {
                return string.IsNullOrEmpty(_req.Query["status"]) 
                    ? "A" : _req.Query["status"].ToString();
            }
        }
        public string Search { get {
                return string.IsNullOrEmpty(_req.Query["search"])
                    ? "" : _req.Query["search"].ToString();
            }
        }
        public string Qstring { get {
                var q = "";
                if (!string.IsNullOrEmpty(Status)) q += $"&status={Status}";
                if (!string.IsNullOrEmpty(Search)) q += $"&search={Search}";
                return q;
            }
        }

        public string IsChecked(string status)
        {
            return status == Status ? "checked" : "";
        }
    }

    public class PostItem : IEquatable<PostItem>
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        [Required]
        public string Content { get; set; }
        public string Cover { get; set; }
        public int PostViews { get; set; }
        public double Rating { get; set; }
        public DateTime Published { get; set; }

        public Author Author { get; set; }
        public SaveStatus Status { get; set; }

        #region IEquatable
        // to be able compare two posts
        // if(post1 == post2) { ... }
        public bool Equals(PostItem other)
        {
            if (Id == other.Id)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }

    public enum SaveStatus
    {
        Saving = 1, Publishing = 2, Unpublishing = 3
    }

    public enum PublishedStatus
    {
        All, Published, Drafts
    }
}