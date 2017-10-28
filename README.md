## Blogifier.Core [![NuGet](https://img.shields.io/nuget/v/Blogifier.Core.svg)](https://www.nuget.org/packages/Blogifier.Core)

The goal of this project is to "blogify" ASP.NET applications; Blogifier.Core built and published as a [Nuget package](https://www.nuget.org/packages/Blogifier.Core) that can be installed by ASP.NET application to provide common blogging functionality.

## Demo site

The [demo site](http://blogifiercore.azurewebsites.net) is a playground you can use to check out Blogifier features. You can register new user and write post to test admin panel.

![demo site](https://user-images.githubusercontent.com/1932785/30626484-dfc57f74-9d8f-11e7-9896-4dedcaad641b.PNG)

## System Requirements

* Windows or Linux
* ASP.NET Core 2.0
* Visual Studio 2017 or VS Code
* Authentication enabled
* Entity Framework Core

Designed for cross-platform development, every build pushed to Windows and Linux servers.

## Getting Started

1. Open in VS 2017 and run WebApp sample application
2. Register new user
3. You should be able navigate to `/blog` and `/admin`

## Using Blogifier.Core Nuget Package

1. In VS 2017, create new ASP.NET Core 2.0 Web Application with user authentication (single user accounts)
2. Open Nuget Package Manager console and run this command:
```
Install-Package Blogifier.Core
```
3. Configure services and application in Startup.cs to use Blogifier (example for MS SQL Server):
```csharp
public void ConfigureServices(IServiceCollection services)
{
  var connectionString = Configuration.GetConnectionString("DefaultConnection");
  System.Action<DbContextOptionsBuilder> databaseOptions = options => options.UseSqlServer(connectionString);
  services.AddDbContext<ApplicationDbContext>(databaseOptions);
  ...
  services.AddMvc();
  Blogifier.Core.Configuration.InitServices(services, databaseOptions, Configuration);
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
  ...
  Blogifier.Core.Configuration.InitApplication(app, env);
}
```
4. You should be able to run application and navigate to `/blog` and `/admin`

## Security

* Blogifier.Core inherits user authentication from `parent` application and acts accordingly.
* If user authenticated but there is no profile for user identity, navigating to `/admin` will redirect to profile page. Filling in profile will effectively create a new blog. 
* First application user will be marked as application administrator and will be able manage application settings.

## Application Settings

Default application settings can be overwritten in application `appsettings.json` configuration file (you can add one if not exists). For example, to change connection string for your database provider:

```json
{
  "Blogifier": {
    "ConnectionString": "your connection string here"
  }
}
```

[More on application settings](https://github.com/blogifierdotnet/Blogifier.Core/wiki/Application-Settings)

## Database Providers

Blogifier.Core implements Entity Framework (code first) as ORM. It uses MS SQL Server provider by default but supports other Entity Framework databases.

For example, to use PostgreSql provider you would install "Npgsql.EntityFrameworkCore.PostgreSQL" package and then use it instead of MS Sql Server:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "User ID=appuser;Password=blogifier;Host=localhost;Port=5432;Database=Blogifier;Pooling=true;"
  }
}
```

```csharp
public void ConfigureServices(IServiceCollection services)
{
  var connectionString = Configuration.GetConnectionString("DefaultConnection");
  System.Action<DbContextOptionsBuilder> databaseOptions = options => options.UseNpgsql(connectionString);
  services.AddDbContext<ApplicationDbContext>(databaseOptions);
  ...
  services.AddMvc();
  Blogifier.Core.Configuration.InitServices(services, databaseOptions, Configuration);
}
```

Connection string cascades based on conditions:
* Use default built-in Blogifier connection string
* Use default parent application connection string in `appsettings.json`
* Use Blogifier connection string in `appsettings.json`.

## Administration

Blogifier provides built-in full featured administration pannel to create, update and publish posts.

![admin](https://user-images.githubusercontent.com/1932785/30626534-25b5b260-9d90-11e7-814e-fc14f510f23e.PNG)

## Credits

In 1.2 release, we've got a good number of pull requests from [alexandrudanpop](https://github.com/alexandrudanpop) helping significantly improve our testing suite. Greatly appreciated!
