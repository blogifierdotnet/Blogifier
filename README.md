## Blogifier.Core ![NuGet](https://img.shields.io/nuget/v/Blogifier.Core.svg)[![NuGet](https://img.shields.io/nuget/dt/Blogifier.Core.svg?label=%20&colorA=635951&colorB=635951)](https://www.nuget.org/packages/Blogifier.Core/)

## Blogifier.Core.PostgreSql ![NuGet](https://img.shields.io/nuget/v/Blogifier.Core.PostgreSql.svg)[![NuGet](https://img.shields.io/nuget/dt/Blogifier.Core.PostgreSql.svg?label=%20&colorA=635951&colorB=635951)](https://www.nuget.org/packages/Blogifier.Core.PostgreSql/)

The goal of this project is to "blogify" ASP.NET applications; Blogifier.Core built and published as a [Nuget package](https://www.nuget.org/packages/Blogifier.Core) that can be installed by ASP.NET application to provide common blogging functionality.

## Demo site

The [demo site](http://blogifier.azurewebsites.net) is a playground you can use to check out Blogifier features. You can register new user and write post to test admin panel.

![demo site](https://user-images.githubusercontent.com/1932785/28844834-8fd2a022-76cb-11e7-9262-13e4fb31079f.PNG)

## System Requirements

* Windows or Linux
* ASP.NET Core 1.1
* .NET Framework 4.5.2
* Visual Studio 2017 or VS Code
* Authentication enabled
* SQL Server (Windows) or PostgreSql (Linux)

Designed for cross-platform development, every build pushed to Windows and Linux servers.

## Getting Started

1. Open in VS 2017 and run WebApp sample application
2. Register new user
3. You should be able navigate to `/blog` and `/admin`

## Using Blogifier.Core Nuget Package

1. In VS 2017, create new ASP.NET Core 1.1 Web Application with user authentication (single user accounts)
2. Open Nuget Package Manager console and run this command:
```
Install-Package Blogifier.Core
```
3. Configure services and application in Startup.cs:
```csharp
public void ConfigureServices(IServiceCollection services)
{
  ...
  Blogifier.Core.Configuration.InitServices(services, Configuration);
}
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
  ...
  Blogifier.Core.Configuration.InitApplication(app, env, loggerFactory);
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

Blogifier.Core implements Entity Framework (code first) as ORM. It uses MS SQL Server provider for Blogigier.Core package and PostgreSql provider for Blogifier.Core.PostgreSql

Connection string cascades based on conditions:
* Use default built-in Blogifier connection string
* Use default parent application connection string in `appsettings.json`
* Use Blogifier connection string in `appsettings.json`.

```json
{
  "Blogifier": {
    "ConnectionString": "Server=.\\SQLEXPRESS;Database=Blogifier;Trusted_Connection=True;"
  }
}
```

## Administration

![admin](https://user-images.githubusercontent.com/1932785/28844983-225a02c8-76cc-11e7-8293-a15d7e1f0422.PNG)

![admin2](https://user-images.githubusercontent.com/1932785/28845126-99c0a6fa-76cc-11e7-9232-57cb1f42b2ad.PNG)
