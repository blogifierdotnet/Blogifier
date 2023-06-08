### To change Database Provider

1. Update provider and connection string in the `appsettings.json`:

```
"Blogifier": {
   "DbProvider": "SQLite",
   "ConnString": "Data Source=Blog.db",
   ...
}
```
Valid providers: `SQLite`, `SqlServer`, `Postgres`, `MySql` (you'll need to supply valid connection string)

2. Remove `Blogifier.Core/Data/Migrations` folder with existing migrations
3. In the Visual Studio, open `Package Manager Console`, set `Blogifier.Core`
as Default project and run these commands:

```
Add-Migration Init -o Data\Migrations
Update-Database
```

First command should re-generate provider specific code migrations and second will 
execute them and create database specified in the connection string.