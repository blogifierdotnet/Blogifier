Global application settings saved in the `appsettings.json` file.

```
"Blogifier": {
  "DbProvider": "SQLite",
  "ConnString": "DataSource=app.db",
  "Avatar": "lib/img/avatar.jpg",
  "ImageExtensions": "png,jpg,gif,bmp,tiff"
}
```

### Settings

Most settings are self-explanatory, here some that might require clarification:

* `DbProvider` and `ConnectionString` are used when you need to switch to another database, like MS SQL Server or PostgreSQL
* `ItemsPerPage` sets page size for all lists in the application, so everything that requires paging will use it
* `ImageExtensions` used to identify what types should be treated as images, so Blogifier will generate `img` tag and not just a link