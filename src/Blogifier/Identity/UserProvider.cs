using AutoMapper;
using Blogifier.Data;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Identity;

public class UserProvider
{
  private readonly IMapper _mapper;
  private readonly AppDbContext _dbContext;

  public UserProvider(
    IMapper mapper,
    AppDbContext dbContext)
  {
    _mapper = mapper;
    _dbContext = dbContext;
  }

  public async Task<UserDto> FindByIdAsync(string userId)
  {
    var query = _dbContext.Users
      .AsNoTracking()
      .Where(m => m.Id == userId);
    return await _mapper.ProjectTo<UserDto>(query).FirstAsync();
  }

  public async Task<IEnumerable<UserInfoDto>> GetAsync()
  {
    var query = _dbContext.Users
      .AsNoTracking();
    return await _mapper.ProjectTo<UserInfoDto>(query).ToListAsync();
  }

  public async Task<UserInfo> UpdateAsync(UserDto input)
  {
    var user = await _dbContext.Users.FirstAsync(m => m.Id == input.Id);
    user.Email = input.Email;
    user.NickName = input.NickName;
    user.Avatar = input.Avatar;
    user.Bio = input.Bio;
    _dbContext.Update(user);
    await _dbContext.SaveChangesAsync();
    return user;
  }
}
