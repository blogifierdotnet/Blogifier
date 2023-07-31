## To change Database Provider
Update provider and connection string in the `appsettings.json`:

#### SQLite
``` json
"Blogifier": {
  "DbProvider": "Sqlite",
  "ConnString": "Data Source=App_Data/blogifier.db",
   ...
}
```
It is recommended to put the database file under the App_Data folder. The logs and local pictures in the project will be stored in this path for persistence.

#### SqlServer
``` json
"Blogifier": {
   "DbProvider": "SqlServer",
   "ConnString": "Data Source=mssql; User Id=sa; Password=Password; Initial Catalog=blogifier;TrustServerCertificate=True",
   ...
}
```
In the latest version of sql server connection, SqlClient will perform a secure connection by default, and you need to add a server certificate to the system. The example adds TrustServerCertificate=True to ignore this requirement. You can also delete this ignore and enable a secure connection.

#### MySql
``` json
"Blogifier": {
   "DbProvider": "MySql",
   "ConnString": "server=mysql;user=root;password=password;database=blogifier",
   ...
}
```

#### Postgres
``` json
"Blogifier": {
   "DbProvider": "Postgres",
   "ConnString": "Host=postgres;Username=postgres;Password=password;Database=blogifier;",
   ...
}
```
In the above example, ConnString requires you to fill in the correct database host address username and password to connect normally


## When a change to an entity field requires a database migration

The database migration is stored in the src/Blogifier/Data/Migrations directory. The current project is still under development. When there is a modification, this directory may be deleted for quick migration. After the project is officially released, it is no longer recommended to delete the updated database migrate.

The following is the way to generate a new migration or delete the previous migration command. Before executing the command, please configure the corresponding DbProvider and ConnString in appsettings.json and then execute the corresponding migration command
``` shell
# Revert Migration Tool
dotnet tool restore

# Jump to project directory
cd src/Blogifier

# Sqlite
dotnet ef migrations add Init --context SqliteDbContext --output-dir Data/Migrations/Sqlite
dotnet ef migrations remove --context SqliteDbContext

# SqlServer
dotnet ef migrations add Init --context SqlServerDbContext --output-dir Data/Migrations/SqlServer
dotnet ef migrations remove --context SqlServerDbContext

# MySql
dotnet ef migrations add Init --context MySqlDbContext --output-dir Data/Migrations/MySql
dotnet ef migrations remove --context MySqlDbContext

# Postgres
dotnet ef migrations add Init --context PostgresDbContext --output-dir Data/Migrations/Postgres
dotnet ef migrations remove --context PostgresDbContext
```

### Warn 
Do not add or delete database migration at will. After the application generates data, random migration may cause data loss. This project will automatically apply the migration when it starts.
