using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Blogifier.Core.Data.Interfaces
{
    public interface IProfileRepository : IRepository<Profile>
    {
        IEnumerable<ProfileListItem> ProfileList(Expression<Func<Profile, bool>> predicate, Pager pager);
    }

    public enum BlogImgType
    {
        Avatar,
        Logo,
        HeaderBg
    }
}
