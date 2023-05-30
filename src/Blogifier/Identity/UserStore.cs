using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blogifier.Identity;

public class UserStore : UserStore<UserInfo>
{
  public UserStore(DbContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
  {
  }
}
