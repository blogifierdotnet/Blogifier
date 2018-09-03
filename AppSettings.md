Global application settings saved in the `appsettings.json` file.
It is read/write so, when admin updates settings in UI, they also written in the file.
When file is updated, admin screen will also pull updated value from the file.

```
"Blogifier": {
  "DbProvider": "SQLite",
  "ConnString": "DataSource=app.db",
  "Title": "Blog title",
  "Description": "Short description of the blog",
  "Cover": "data/admin/cover-desk.jpg",
  "Logo": "lib/img/logo-white.png",
  "Avatar": "lib/img/avatar.jpg",
  "Theme": "Standard",
  "BlogThemes": null,
  "ItemsPerPage": 10,
  "UseDescInPostList": true,
  "ImportTypes": "zip,7z,xml,pdf,doc,docx,xls,xlsx,mp3,avi",
  "ImageExtensions": "png,jpg,gif,bmp,tiff",
  "DemoMode": true
}
```

### Settings

Most settings are self-explanatory, here some that might require clarification:

`DbProvider` and `ConnectionString` are used when you need to switch to another database, like MS SQL Server or PostgreSQL

`ItemsPerPage` sets page size for all lists in application, so everything that requires paging will use it

`UseDescInPostList` when set to `true` returns post description instead of content in the post lists

`ImportTypes` defines what type of files should be downloaded when importing posts from RSS feed

`ImageExtensions` used to identify what types should be treated as images, so Blogifier will generate `img` tag and not just a link

`DemoMode` disables password update, so that demo site won't be locked