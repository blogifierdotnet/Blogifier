﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Models
{
    public class PostModel
    {
        public BlogItem Blog { get; set; }
        public PostItem Post { get; set; }
        public PostItem Older { get; set; }
        public PostItem Newer { get; set; }
    }

    public class ListModel
    {
        public BlogItem Blog { get; set; }
        public Author Author { get; set; } // posts by author
        public string Category { get; set; } // posts by category

        public IEnumerable<PostItem> Posts { get; set; }
        public Pager Pager { get; set; }

        public PostListType PostListType { get; set; }
    }

    public class PageListModel
    {
        public IEnumerable<PostItem> Posts { get; set; }
        public Pager Pager { get; set; }
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
        public string Categories { get; set; }
        public string Cover { get; set; }
        public int PostViews { get; set; }
        public double Rating { get; set; }
        public DateTime Published { get; set; }
        public bool IsPublished { get { return Published > DateTime.MinValue; } }
        public bool Featured { get; set; }

        public Author Author { get; set; }
        public SaveStatus Status { get; set; }
        public List<SocialField> SocialFields { get; set; }
        public bool Selected { get; set; }

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

    public enum PostListType
    {
        Blog, Category, Author, Search
    }

    public enum PostAction
    {
        Save, Publish, Unpublish
    }

    public class CategoryItem: IComparable<CategoryItem>
    {
        public string Category { get; set; }
        public int PostCount { get; set; }

        public int CompareTo(CategoryItem other)
        {
            return Category.ToLower().CompareTo(other.Category.ToLower());
        }
    }

    public enum SaveStatus
    {
        Saving = 1, Publishing = 2, Unpublishing = 3
    }

    public enum PublishedStatus
    {
        All, Published, Drafts, Featured
    }

    public enum GroupAction
    {
        Publish, Unpublish, Feature, Delete
    }
}