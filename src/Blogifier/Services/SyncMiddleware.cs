using System;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Blogifier.Core.Providers;
using Blogifier.Shared;
using Blogifier.Shared.Extensions;
using IdentityModel;

namespace Blogifier.Services
{
    public class SyncMiddleware
    {
        private readonly RequestDelegate _next;

        public SyncMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext _httpContext, IAuthorProvider _authorProvider, IStorageProvider _storageProvider)
        {
            System.Console.WriteLine("Happend Once");
            //await _next(_httpContext);
            if (_httpContext.Request.Path.StartsWithSegments("/api"))
            {
                System.Console.WriteLine(_httpContext.Request.Path);
            }
            if (_httpContext.User.Identity.IsAuthenticated)
            {
                Author tempAuthor = CreateFromOIDC(_httpContext.User);
                string tempSub = _httpContext.User.FindFirstValue(JwtClaimTypes.Subject);
                string avatarToSync = tempAuthor.Avatar;
                tempAuthor.Avatar = tempAuthor.Avatar.VerifyAvatar();

                if (_httpContext.User.HasClaim("role", "AutoBloger"))
                {
                    tempAuthor.IsAdmin = true;
                }

                // Sync with local DB on Bio firstly
                var existingUser = await _authorProvider.FindByOpenId(tempSub);

                if (existingUser is null)
                {
                    Console.WriteLine("Has no local data, need to Sync!");
                    bool syncResult = await SyncAuthorWithDB(tempAuthor, _authorProvider);
                    System.Console.WriteLine($"Sync result is {syncResult};");
                    if (syncResult && !String.Equals(tempAuthor.Avatar, "default.png"))
                    {
                        System.Console.WriteLine(tempAuthor.Avatar);
                        await _storageProvider.SyncAvatarFromWeb(new Uri($"https://auth.prime-minister.pub/images/user_avatars/{avatarToSync}.png"));
                    }
                }

                else
                {
                    // Sync with Avatar/Name/Email
                    existingUser.Avatar = existingUser.Avatar.VerifyAvatar();
                    var oldAvatarName = existingUser.Avatar;
                    if (existingUser.DisplayName != tempAuthor.DisplayName || existingUser.Email != tempAuthor.Email || existingUser.Avatar != tempAuthor.Avatar)
                    {
                        System.Console.WriteLine("----Update Profile----");
                        tempAuthor.Bio = existingUser.Bio;
                        await _authorProvider.Update(tempAuthor);
                    }

                    if (oldAvatarName != tempAuthor.Avatar)
                    {
                        System.Console.WriteLine("----Update Avatar----");
                        await _storageProvider.SyncAvatarFromWeb(new Uri($"https://auth.prime-minister.pub/images/user_avatars/{tempAuthor.Avatar}.png"));
                        await _storageProvider.DeleteOldAvatar(oldAvatarName);
                    }
                }
            }
            await _next(_httpContext);
        }
        protected Author CreateFromOIDC(ClaimsPrincipal user)
        {
            var tempAuthor = new Author();
            var tempGuid = user.FindFirstValue(JwtClaimTypes.Subject);
            var tempEmail = user.FindFirstValue(JwtClaimTypes.Email);
            var tempName = user.FindFirstValue(JwtClaimTypes.Name);
            var tempAvatar = user.FindFirstValue(JwtClaimTypes.Picture);

            tempAuthor.DisplayName = tempName;
            tempAuthor.Email = tempEmail;
            tempAuthor.OpenGuid = tempGuid;
            tempAuthor.IsAdmin = false;
            tempAuthor.Avatar = tempAvatar;
            if (String.IsNullOrEmpty(tempAvatar))
            {
                tempAuthor.Avatar = "default";
            }
            return tempAuthor;
        }

        protected async Task<bool> SyncAuthorWithDB(Author author, IAuthorProvider _authorProvider)
        {
            Author authorToSync = author;
            authorToSync.Bio = "Update Bio/更新简介";
            authorToSync.DateCreated = DateTime.UtcNow;
            return await _authorProvider.CreateFromAuthor(authorToSync);
        }
    }
}
