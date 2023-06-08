using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Blogifier.Identity;

public class RoleManager : RoleManager<RoleInfo>
{
  public RoleManager(
    IRoleStore<RoleInfo> store,
    IEnumerable<IRoleValidator<RoleInfo>> roleValidators,
    ILookupNormalizer keyNormalizer,
    IdentityErrorDescriber errors,
    ILogger<RoleManager<RoleInfo>> logger)
    : base(store, roleValidators, keyNormalizer, errors, logger)
  {
  }
}
