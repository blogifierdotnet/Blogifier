using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogifier.Data.Migrations.Sqlite
{
  /// <inheritdoc />
  public partial class Storage : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "DeletedAt",
          table: "Storages");

      migrationBuilder.DropColumn(
          name: "IsDeleted",
          table: "Storages");

      migrationBuilder.AddColumn<DateTime>(
          name: "UploadAt",
          table: "Storages",
          type: "TEXT",
          nullable: false,
          defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "UploadAt",
          table: "Storages");

      migrationBuilder.AddColumn<DateTime>(
          name: "DeletedAt",
          table: "Storages",
          type: "TEXT",
          nullable: true);

      migrationBuilder.AddColumn<bool>(
          name: "IsDeleted",
          table: "Storages",
          type: "INTEGER",
          nullable: false,
          defaultValue: false);
    }
  }
}
