using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blogifier.Core.Data;
using Blogifier.Core.Extensions;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Blogifier.Core.Providers;

public interface IAuthorProvider
{
    Task<List<Author>> GetAuthors();
    Task<Author> FindByEmail(string email);
    Task<bool> Verify(LoginModel model);
    Task<bool> Register(RegisterModel model);
    Task<bool> Add(Author author);
    Task<bool> Update(Author author);
    Task<bool> ChangePassword(RegisterModel model);
    Task<bool> Remove(int id);
}

public class AuthorProvider : IAuthorProvider
{
    private static string _salt;
    private readonly AppDbContext _db;
    private readonly string _reCaptchaSecretKey;

    public AuthorProvider(AppDbContext db, IConfiguration configuration)
    {
        _db = db;
        _salt = configuration.GetSection("Blogifier").GetValue<string>("Salt");
        _reCaptchaSecretKey = configuration.GetSection("ReCaptcha").GetValue<string>("ReCaptchaSecretKey");
    }

    public async Task<List<Author>> GetAuthors()
    {
        return await _db.Authors.ToListAsync();
    }

    public async Task<Author> FindByEmail(string email)
    {
        return await Task.FromResult(_db.Authors.FirstOrDefault(a => a.Email == email));
    }

    public async Task<bool> Verify(LoginModel model)
    {
        #region Captcha Validation

        Log.Warning("Verifying reCaptcha response");

        // maybe redundant...
        if (string.IsNullOrEmpty(model.CaptchaResponse))
        {
            Log.Warning("Captcha not verified properly");
            return false;
        }

        using (var client = new HttpClient())
        {
            var response = await client.PostAsJsonAsync("https://www.google.com/recaptcha/api/siteverify",
                new
                {
                    secret = _reCaptchaSecretKey,
                    response = model.CaptchaResponse
                });
            if (!response.IsSuccessStatusCode)
            {
                Log.Warning("Captcha not verified properly");
                return false;
            }
        }

        #endregion

        Log.Warning($"Verifying password for {model.Email}");

        var existing = await Task.FromResult(_db.Authors.FirstOrDefault(a => a.Email == model.Email));

        if (existing == null)
        {
            Log.Warning($"User with email {model.Email} not found");
            return false;
        }

        if (existing.Password == model.Password.Hash(_salt))
        {
            Log.Warning($"Successful login for {model.Email}");
            return true;
        }

        Log.Warning("Password does not match");
        return false;
    }

    public async Task<bool> Register(RegisterModel model)
    {
        var isAdmin = false;
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
                DateCreated = DateTime.UtcNow
            };

            _db.Blogs.Add(blog);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Warning($"Error registering new blog: {ex.Message}");
                return false;
            }
        }

        blog = await _db.Blogs.Include(b => b.Authors).FirstOrDefaultAsync();
        if (blog == null)
            return false;

        author = new Author
        {
            DisplayName = model.Name,
            Email = model.Email,
            Password = model.Password.Hash(_salt),
            IsAdmin = isAdmin,
            Avatar = string.Format(Constants.AvatarDataImage, model.Name.Substring(0, 1).ToUpper()),
            Bio = "The short author bio.",
            DateCreated = DateTime.UtcNow
        };

        blog.Authors.Add(author);

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
        author.DateCreated = DateTime.UtcNow;

        blog.Authors.Add(author);

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
            // do not remove last admin account
            if (_db.Authors.Where(a => a.IsAdmin).Count() == 1)
                return false;

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
