using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Blogifier.Core.Data.Models
{
    public class BlogBaseModel
    {
        public virtual string LogoImg { get; set; } = ApplicationSettings.ProfileLogo;
        public virtual string LogoUrl { get; set; } = "";
        public virtual string CoverImg { get; set; } = ApplicationSettings.ProfileImage;
        public virtual string PageTitle { get; set; } = "Blogifier";
        public virtual string PageDescription { get; set; } = ApplicationSettings.Description;
        public Dictionary<string, string> CustomFields { get; set; } = new Dictionary<string, string>();
    }

    public class BlogPostDetailModel : BlogBaseModel
    {
        public override string LogoImg { get => string.IsNullOrEmpty(Profile.Logo) ? ApplicationSettings.ProfileLogo : Profile.Logo; }
        public override string CoverImg { get => BlogPost.Image; }
        public override string LogoUrl { get => ApplicationSettings.BlogRoute + Profile.Slug; }
        public override string PageTitle { get => BlogPost.Title; }
        public override string PageDescription { get => Profile == null ? PageTitle : Profile.Description + " - " + PageTitle; }

        public Profile Profile { get; set; }
        public BlogPost BlogPost { get; set; }
        public List<SelectListItem> BlogCategories { get; set; }
    }

    public class BlogPostsModel : BlogBaseModel
    {
        public override string LogoImg { get => ApplicationSettings.ProfileLogo; }
        public override string CoverImg { get => ApplicationSettings.ProfileImage; }
        public override string LogoUrl { get => ApplicationSettings.BlogRoute; }
        public override string PageTitle { get => ApplicationSettings.Title; }

        public IEnumerable<PostListItem> Posts { get; set; }
        public Pager Pager { get; set; }
    }

    public class BlogCategoryModel : BlogBaseModel
    {
        public override string LogoImg { get => string.IsNullOrEmpty(Profile.Logo) ? ApplicationSettings.ProfileLogo : Profile.Logo; }
        public override string CoverImg { get => string.IsNullOrEmpty(Profile.Image) ? ApplicationSettings.ProfileImage : Profile.Image; }
        public override string LogoUrl { get => ApplicationSettings.BlogRoute + Profile.Slug; }
        public override string PageTitle { get => "Category: " + Category.Title; }
        public override string PageDescription { get => Profile == null ? PageTitle : Profile.Description + " - " + PageTitle; }

        public Profile Profile { get; set; }
        public Category Category { get; set; }
        public IEnumerable<PostListItem> Posts { get; set; }
        public Pager Pager { get; set; }
    }

    public class BlogAuthorModel : BlogBaseModel
    {
        public override string LogoImg { get => string.IsNullOrEmpty(Profile.Logo) ? ApplicationSettings.ProfileLogo : Profile.Logo; }
        public override string CoverImg { get => string.IsNullOrEmpty(Profile.Image) ? ApplicationSettings.ProfileImage : Profile.Image; }
        public override string LogoUrl { get => ApplicationSettings.BlogRoute + Profile.Slug; }
        public override string PageTitle { get => Profile.Title; }
        public override string PageDescription { get => Profile.Title + " - " + Profile.Description; }

        public Profile Profile { get; set; }
        public IEnumerable<PostListItem> Posts { get; set; }
        public Pager Pager { get; set; }
    }
}
