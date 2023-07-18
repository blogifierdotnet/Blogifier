using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Identity;

public class UserManager : UserManager<UserInfo>
{
  protected readonly UserProvider _userProvider;

  public UserManager(
    IUserStore<UserInfo> store,
    IOptions<IdentityOptions> optionsAccessor,
    IPasswordHasher<UserInfo> passwordHasher,
    IEnumerable<IUserValidator<UserInfo>> userValidators,
    IEnumerable<IPasswordValidator<UserInfo>> passwordValidators,
    ILookupNormalizer keyNormalizer,
    IdentityErrorDescriber errors,
    IServiceProvider services,
    ILogger<UserManager<UserInfo>> logger,
    UserProvider userProvider)
    : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
  {
    _userProvider = userProvider;
  }

  public Task<UserInfo> FindByIdAsync(int userId)
  {
    return _userProvider.FindAsync(userId);
  }
}
