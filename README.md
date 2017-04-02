# Blogifier.Core ![MyGet](https://buildstats.info/myget/rtur/Blogifier.Core)

Project currently in the early Beta stage and not yet ready for any production use. Sample application set to use in-memory database and all changes will be lost when application stops or restarts.

### What is Blogifier.Core

The goal of this project is to "blogify" new or existing ASP.NET applications; Blogifier.Core built and published as a [Nuget package](https://www.myget.org/feed/rtur/package/nuget/Blogifier.Core) that can be installed by ASP.NET application to provide common blogging functionality. 

### System Requirements

 * Visual Studio 2017
 * ASP.NET Core 1.1

### Getting Started

1. Open in VS 2017 and run WebApp sample application
2. Register new user
3. You should be able navigate to `/blog` and `/admin`

### Using Blogifier.Core Nuget Package

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
