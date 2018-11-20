Blogifier uses SQLite database by default. It is file-based, cross-platform, portable, 
easy to use and should work for most people in most cases.

### Change Database to MySQL

Another database provider supported by Blogifier out of the box is MySQL. To switch 
using MySQL just update configuration settings, for example:

```cmd
/* src/app/appsettings.json */
{
  "Blogifier": {
    "DbProvider": "MySql",
    "ConnString": "server=<server.address>;userid=root;pwd=<root_password>;port=3306;database=blogifier_;sslmode=none;",
    ...
  }
}
```

When you run application after changing provider, Blogifier will check if database exists, 
run migration scripts if required and populate database with seed data.

Alternatively create the database prior to running and a user with limited permissions

```cmd
CREATE DATABASE `blogifier_`;
CREATE USER 'bloguser'@'localhost' IDENTIFIED BY '<password>';
GRANT SELECT, INSERT, UPDATE, DELETE, CREATE, LOCK TABLES, EXECUTE, INDEX ON `blogifier_`.* TO 'bloguser'@'localhost';
```

Then add the limited user to the configuration

```cmd
/* src/app/appsettings.json */
{
  "Blogifier": {
    "DbProvider": "MySql",
    "ConnString": "server=<server.address>;userid=bloguser;pwd=<password>;port=3306;database=blogifier_;sslmode=none;",
    ...
  }
}
```
