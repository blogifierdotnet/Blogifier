Global application settings saved in the `appsettings.json` file.

```
"Blogifier": {
  "DbProvider": "SQLite",
  "ConnString": "DataSource=app.db",
  "Avatar": "lib/img/avatar.jpg",
  "ImportTypes": "zip,7z,xml,pdf,doc,docx,xls,xlsx,mp3,avi",
  "ImageExtensions": "png,jpg,gif,bmp,tiff",
  "SeedData": false,
  "DemoMode": true
}
```

### Settings

Most settings are self-explanatory, here some that might require clarification:

* `DbProvider` and `ConnectionString` are used when you need to switch to another database, like MS SQL Server or PostgreSQL
* `ItemsPerPage` sets page size for all lists in the application, so everything that requires paging will use it
* `ImportTypes` defines what type of files should be downloaded when importing posts from RSS feed
* `ImageExtensions` used to identify what types should be treated as images, so Blogifier will generate `img` tag and not just a link
* `SeedData` when `true` will populate empty database with sample data
* `DemoMode` disables password update, so that demo site won't be locked