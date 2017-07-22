using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Blogifier.Core.Data.Models
{
    public class BlogBaseModel
    {
        public virtual string LogoImg { get; set; }
        public virtual string LogoUrl { get; set; }
        public virtual string CoverImg { get; set; }
        public virtual string PageTitle { get; set; }
        public Dictionary<string, string> CustomFields { get; set; }
    }

    public class BlogPostDetailModel : BlogBaseModel
    {
        public override string LogoImg { get => string.IsNullOrEmpty(Profile.Logo) ? ApplicationSettings.ProfileLogo : Profile.Logo; }
        public override string CoverImg { get => BlogPost.Image; }
        public override string LogoUrl { get => Constants.BlogRoute + Profile.Slug; }
        public override string PageTitle { get => BlogPost.Title; }

        public Profile Profile { get; set; }
        public BlogPost BlogPost { get; set; }
        public List<SelectListItem> BlogCategories { get; set; }
        public CustomField DisqusScript { get; set; }
    }

    public class BlogPostsModel : BlogBaseModel
    {
        public override string LogoImg { get => ApplicationSettings.ProfileLogo; }
        public override string CoverImg { get => ApplicationSettings.ProfileImage; }
        public override string LogoUrl { get => Constants.BlogRoute; }
        public override string PageTitle { get => ApplicationSettings.Title; }

        public IEnumerable<PostListItem> Posts { get; set; }
        public Pager Pager { get; set; }
    }

    public class BlogCategoryModel : BlogBaseModel
    {
        public override string LogoImg { get => string.IsNullOrEmpty(Profile.Logo) ? ApplicationSettings.ProfileLogo : Profile.Logo; }
        public override string CoverImg { get => string.IsNullOrEmpty(Profile.Image) ? ApplicationSettings.ProfileImage : Profile.Image; }
        public override string LogoUrl { get => Constants.BlogRoute + Profile.Slug; }
        public override string PageTitle { get => "Category: " + Category.Title; }

        public Profile Profile { get; set; }
        public Category Category { get; set; }
        public IEnumerable<PostListItem> Posts { get; set; }
        public Pager Pager { get; set; }
    }

    public class BlogAuthorModel : BlogBaseModel
    {
        public override string LogoImg { get => string.IsNullOrEmpty(Profile.Logo) ? ApplicationSettings.ProfileLogo : Profile.Logo; }
        public override string CoverImg { get => string.IsNullOrEmpty(Profile.Image) ? ApplicationSettings.ProfileImage : Profile.Image; }
        public override string LogoUrl { get => Constants.BlogRoute + Profile.Slug; }
        public override string PageTitle { get => Profile.Title; }

        public Profile Profile { get; set; }
        public IEnumerable<PostListItem> Posts { get; set; }
        public Pager Pager { get; set; }
    }
}
