## Blogifier.Core [![MyGet](https://buildstats.info/myget/rtur/Blogifier.Core)](https://www.myget.org/feed/rtur/package/nuget/Blogifier.Core)

## Blogifier.Core.PostgreSql [![MyGet](https://buildstats.info/myget/rtur/Blogifier.Core.PostgreSql)](https://www.myget.org/feed/rtur/package/nuget/Blogifier.Core.PostgreSql)

This project currently in the pre-release and not completely ready for production use.

## What is Blogifier.Core

The goal of this project is to "blogify" new and existing ASP.NET applications; Blogifier.Core built and published as a [Nuget package](https://www.myget.org/feed/rtur/package/nuget/Blogifier.Core) that can be installed by ASP.NET application to provide common blogging functionality.

[![Introductory Youtube video](https://img.youtube.com/vi/vb0lYJKGHSw/0.jpg)](https://www.youtube.com/watch?v=vb0lYJKGHSw)

## System Requirements

* Windows or Linux
* ASP.NET Core 1.1
* .NET Framework 4.5.2
* Visual Studio 2017 or VS Code
* ASP.NET Authentication
* SQL Server (Windows) or PostgreSql (Linux)

Designed for cross-platform development, every build pushed to Windows and Linux servers. For Linux use `Blogifier.Core.PostgreSql` package.

## Getting Started

1. Open in VS 2017 and run WebApp sample application
2. Register new user
3. You should be able navigate to `/blog` and `/admin`

## Using Blogifier.Core Nuget Package

1. In VS 2017, create new ASP.NET Core 1.1 Web Application with user authentication (single user accounts)
2. Open Nuget Package Manager console and run this command:
```
Install-Package Blogifier.Core -Source https://www.myget.org/F/rtur/api/v3/index.json
```
3. Configure services and application in Startup.cs:
```csharp
public void ConfigureServices(IServiceCollection services)
{
  ...
  Blogifier.Core.Configuration.InitServices(services);
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

| Name | Description | Default Value
| --------- | ----------- | ------------ |
| UseInMemoryDatabase | If in-memory or SQL database should be used | `true` |
| ConnectionString | Database connection string | `Server=.\SQLEXPRESS;Database=Blogifier;Trusted_Connection=True;` |
| EnableLogging | To enable application logging | `false` |
| BlogStorageFolder | Folder for uploaded files/images | `blogifier/data` |
| SupportedStorageFiles | File types allowed for upload | `zip,txt,mp3,mp4,pdf,doc,docx,xls,xlsx,xml` |
| AdminTheme | Administration theme | `Standard` |
| Title | Appliation title | `Blog Name` |
| Description | Application description | `Short description of the blog` |
| ItemsPerPage | Number of items per page in the lists | `10` |
| AddContentTypeHeaders | Set content type for embedded resources | `true` |
| AddContentLengthHeaders | Set content length for embedded resources | `false` |
| PrependFileProvider | Prepend file provider for embedded resources | `false` |

The last three items are to adjust resource embedding for different environments. For example IIS on Azure runs with default values, nginx + Kestrel on Linux requires all three set to `true`

## Database Providers

Blogifier.Core implements Entity Framework (code first) as ORM. It uses MS SQL Server provider out of the box and PostgreSql as secondary provider.

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

For Linux, use [Blogifier.Core.PosgreSql](https://www.myget.org/feed/rtur/package/nuget/Blogifier.Core.PostgreSql) package built specifically for PostgreSql database.
