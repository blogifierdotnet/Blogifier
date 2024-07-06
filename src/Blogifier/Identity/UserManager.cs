using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Identity;

public class UserManager(
  IUserStore<UserInfo> store,
  IOptions<IdentityOptions> optionsAccessor,
  IPasswordHasher<UserInfo> passwordHasher,
  IEnumerable<IUserValidator<UserInfo>> userValidators,
  IEnumerable<IPasswordValidator<UserInfo>> passwordValidators,
  ILookupNormalizer keyNormalizer,
  IdentityErrorDescriber errors,
  IServiceProvider services,
  ILogger<UserManager<UserInfo>> logger,
  UserProvider userProvider) : UserManager<UserInfo>(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
{
  protected readonly UserProvider _userProvider = userProvider;

  public Task<UserInfo> FindByIdAsync(int userId) => _userProvider.FindAsync(userId);
}
