using Blogifier.Data;
using Blogifier.Extensions;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Providers;

public class AuthorProvider
{
  private readonly ILogger _logger;
  private readonly AppDbContext _db;
  private readonly string _salt;

  public AuthorProvider(ILogger<AuthorProvider> logger, AppDbContext db, IConfiguration configuration)
  {
    _logger = logger;
    _db = db;
    _salt = configuration.GetSection("Blogifier").GetValue<string>("Salt")!;
  }

  public async Task<List<Author>> GetAuthors()
  {
    return await _db.Authors.ToListAsync();
  }

  public async Task<Author> FindByEmail(string email)
  {
    return await _db.Authors.Where(a => a.Email == email).FirstAsync();
  }

  public async Task<bool> Verify(LoginModel model)
  {
    _logger.LogWarning("Verifying password for {model.Email}", model.Email);

    var existing = await _db.Authors.Where(a => a.Email == model.Email).FirstOrDefaultAsync();

    if (existing == null)
    {
      _logger.LogWarning("User with email {model.Email} not found", model.Email);
      return false;
    }

    if (existing.Password == model.Password.Hash(_salt))
    {
      _logger.LogWarning("Successful login for {model.Email}", model.Email);
      return true;
    }
    else
    {
      _logger.LogWarning($"Password does not match");
      return false;
    }
  }

  public async Task<bool> Register(RegisterModel model)
  {
    bool isAdmin = false;
    var author = await _db.Authors.Where(a => a.Email == model.Email).FirstOrDefaultAsync();
    if (author != null)
      return false;

    var blog = await _db.Blogs.Include(b => b.Authors).FirstOrDefaultAsync();
    if (blog == null)
    {
      isAdmin = true; // first blog record - set user as admin
      blog = new Blog
      {
        Title = "Blog Title",
        Description = "Short Blog Description",
        Theme = "Standard",
        ItemsPerPage = 10,
      };

      _db.Blogs.Add(blog);
      try
      {
        await _db.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        Serilog.Log.Warning($"Error registering new blog: {ex.Message}");
        return false;
      }
    }

    blog = await _db.Blogs.Include(b => b.Authors).FirstOrDefaultAsync();
    if (blog == null) return false;

    author = new Author
    {
      DisplayName = model.Name,
      Email = model.Email,
      Password = model.Password.Hash(_salt),
      IsAdmin = isAdmin,
      Avatar = string.Format(Constants.AvatarDataImage, model.Name.Substring(0, 1).ToUpper()),
      Bio = "The short author bio.",
    };
    blog.Authors!.Add(author);
    return await _db.SaveChangesAsync() > 0;
  }

  public async Task<bool> Add(Author author)
  {
    var existing = await _db.Authors.Where(a => a.Email == author.Email).OrderBy(a => a.Id).FirstOrDefaultAsync();
    if (existing != null)
      return false;

    var blog = await _db.Blogs.Include(b => b.Authors).OrderBy(b => b.Id).FirstOrDefaultAsync();
    if (blog == null)
      return false;

    author.IsAdmin = false;
    author.Password = author.Password.Hash(_salt);
    author.Avatar = string.Format(Constants.AvatarDataImage, author.DisplayName.Substring(0, 1).ToUpper());

    blog.Authors!.Add(author);
    return await _db.SaveChangesAsync() > 0;
  }

  public async Task<bool> Update(Author author)
  {
    var existing = await _db.Authors
        .Where(a => a.Email == author.Email)
        .FirstOrDefaultAsync();

    if (existing == null)
      return false;

    if (existing.IsAdmin && !author.IsAdmin)
    {
      // do not remove last admin account
      if (_db.Authors.Where(a => a.IsAdmin).Count() == 1)
        return false;
    }

    existing.Email = author.Email;
    existing.DisplayName = author.DisplayName;
    existing.Bio = author.Bio;
    existing.Avatar = author.Avatar;
    existing.IsAdmin = author.IsAdmin;

    return await _db.SaveChangesAsync() > 0;
  }

  public async Task<bool> ChangePassword(RegisterModel model)
  {
    var existing = await _db.Authors
        .Where(a => a.Email == model.Email)
        .FirstOrDefaultAsync();

    if (existing == null)
      return false;

    existing.Password = model.Password.Hash(_salt);

    return await _db.SaveChangesAsync() > 0;
  }

  public async Task<bool> Remove(int id)
  {
    var existingAuthor = await _db.Authors.Where(a => a.Id == id).FirstOrDefaultAsync();
    if (existingAuthor == null)
      return false;

    _db.Authors.Remove(existingAuthor);
    await _db.SaveChangesAsync();
    return true;
  }
}
