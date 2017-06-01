# Blogifier.Core [![MyGet](https://buildstats.info/myget/rtur/Blogifier.Core)](https://www.myget.org/feed/rtur/package/nuget/Blogifier.Core)

Project currently in the early Beta stage and not yet ready for any production use. Sample application set to use in-memory database and all changes will be lost when application stops or restarts.

## What is Blogifier.Core

The goal of this project is to "blogify" new and existing ASP.NET applications; Blogifier.Core built and published as a [Nuget package](https://www.myget.org/feed/rtur/package/nuget/Blogifier.Core) that can be installed by ASP.NET application to provide common blogging functionality. 

## System Requirements

 * ASP.NET Core 1.1
 * Visual Studio 2017
 * Visual Studio Code

Designed for cross-platform development, every build pushed to Windows and Linux servers

## Getting Started

1. Open in VS 2017 and run WebApp sample application
2. Register new user
3. You should be able navigate to `/blog` and `/admin`

## Using Blogifier.Core Nuget Package

1. In VS 2017, create new ASP.NET Core 1.1 application with user authentication
2. Open Nuget Package Manager and add Blogifier feed to package sources:
```
https://www.myget.org/F/rtur/api/v2
```
3. Search for "Blogifier.Core" or install from PM console:
```
Install-Package Blogifier.Core 
```
4. Configure services and application in Startup.cs:
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
5. You should be able to run application and navigate to `/blog` and `/admin`

## Security

* Blogifier.Core inherits user authentication from “parent” application and acts accordingly.
* If user authenticated but there is no profile for user’s identity, navigating to `/admin` will redirect to profile page. Filling in profile will effectively create a new blog. 
* First application user will be marked as application administrator and will be able manage application settings and profiles.

## Application Settings

Default application settings can be overwritten in application `appsettings.json` configuration file (you can add one if not exists). For example, to turn off default in-memory database provider:

```json
{
  "Blogifier": {
    "UseInMemoryDatabase": false
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

Blogifier.Core implements Entity Framework (code first) as ORM. It uses in-memory provider out of the box. 

To use SQL Server provider (local SQL Express in example), turn off default and specify connection string in the parent `appsettings.json`.

```json
{
  "Blogifier": {
    "UseInMemoryDatabase": false,
    "ConnectionString": "Server=.\\SQLEXPRESS;Database=Blogifier;Trusted_Connection=True;"
  }
}
```

For Linux, there will be separate `Blogifier.Core.PosgreSql` package built specifically for PostgreSql database.
