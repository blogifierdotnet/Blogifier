using Blogifier.Core.Common;
using Blogifier.Core.Data.Models;
using Blogifier.Models.AccountViewModels;
using System.Collections.Generic;

namespace Blogifier.Models.Admin
{
    public class UsersViewModel : AdminBaseModel
    {
        public RegisterViewModel RegisterModel { get; set; }

        public IEnumerable<ProfileListItem> Blogs { get; set; }
        public Pager Pager { get; set; }
    }
}
