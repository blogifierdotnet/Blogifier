
### User Login and Registration
On the first login, user gets redirected to the `admin/register` page to register a new account. 
Once account created, registration page is disabled and this user becomes a blog owner.

### Authentication
The blog posts, including blog themes, are all run as public, server-side rendered MVC site. 

Anything under `admin` is Blazor Web Assembly application and is guarded by custom authentication provider (`BlogAuthenticationStateProvider`). 
User password is one-way hashed and saved in the `Authors` table on the back-end. 
The salt used to hash password pulled from `appsettings.json` configuration file and should be updated **before** creating user account.

```
"Blogifier": {
  ...
  "Salt": "SECRET-CHANGE-ME!"
}
```