Blogifier uses default ASP.NET Core Identity. User data saved in the database in the `AspNet...` 
tables and managed by calling standard UserManager and SignInManager.

### User Roles.
There are no real roles, but Blogifier differentiates between admin and regular users. Admin can 
manage application functionality, including create/delete users. Standard users can only write 
and publish posts and upload and manage files.

### Identity options
Identity options defined in the `Startup.cs` and can be modified if do not fit requirements.
By default, they pretty relaxed: you can use any characters in the user name and only password 
restriction is to be at least four characters long.

```csharp
services.AddIdentity<AppUser, IdentityRole>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.AllowedUserNameCharacters = null;
})
```