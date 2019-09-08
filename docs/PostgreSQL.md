
Blogifier uses SQLite database by default. It is file-based, cross-platform, portable, 
easy to use and should work for most people in most cases.

### Change Database to PostgreSQL

Another database provider supported by Blogifier out of the box is PostgreSQL. To switch 
using PostgreSQL just update configuration settings, for example:

```cmd
/* src/app/appsettings.json */
{
  "Blogifier": {
    "DbProvider": "Postgres",
    "ConnString": "Host=<server.address>;Username=<username>;Password=<password>;Database=blogifier_",
    ...
  }
}
```

When you run application after changing provider, Blogifier will check if database exists, 
run migration scripts if required and populate database with seed data. 

(caveat: of course for database creation and other migrations to work correctly, the 
specified user has to have the appropriate permissions with postgres.)