
Blogifier uses SQLite database by default. It is file-based, cross-platform, portable, 
easy to use and should work for most people in most cases.

### Change Database to SQL Server

Second database provider supported by Blogifier out of the box is MS SQL Server. To switch 
using SQL Server just update configuration settings, for example:

```cmd
/* src/app/appsettings.json */
{
  "Blogifier": {
    "DbProvider": "SqlServer",
    "ConnString": "Server=.\\SQLEXPRESS;Database=Blogifier;Trusted_Connection=True",
    ...
  }
}
```

When you run application after changing provider, Blogifier will check if database exists, 
run migration scripts if required and populate database with seed data.