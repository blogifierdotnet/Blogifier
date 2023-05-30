using AutoMapper;
using Blogifier.Data;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
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
    var query = _dbContext.Users.Where(m => m.Id == userId);
    return await _mapper.ProjectTo<UserDto>(query).FirstAsync();
  }
}
