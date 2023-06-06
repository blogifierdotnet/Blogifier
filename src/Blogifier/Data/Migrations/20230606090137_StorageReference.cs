using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogifier.Data.Migrations
{
  /// <inheritdoc />
  public partial class StorageReference : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_Newsletters_Posts_PostId",
          table: "Newsletters");

      migrationBuilder.DropForeignKey(
          name: "FK_PostCategories_Posts_PostId",
          table: "PostCategories");

      migrationBuilder.DropForeignKey(
          name: "FK_Posts_User_UserId",
          table: "Posts");

      migrationBuilder.DropPrimaryKey(
          name: "PK_Posts",
          table: "Posts");

      migrationBuilder.RenameTable(
          name: "Posts",
          newName: "Post");

      migrationBuilder.RenameIndex(
          name: "IX_Posts_UserId",
          table: "Post",
          newName: "IX_Post_UserId");

      migrationBuilder.AddPrimaryKey(
          name: "PK_Post",
          table: "Post",
          column: "Id");

      migrationBuilder.CreateTable(
          name: "StorageReferences",
          columns: table => new
          {
            StorageId = table.Column<int>(type: "int", nullable: false),
            EntityId = table.Column<int>(type: "int", nullable: false),
            Type = table.Column<int>(type: "int", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_StorageReferences", x => new { x.StorageId, x.EntityId, x.Type });
            table.ForeignKey(
                      name: "FK_StorageReferences_Post_EntityId",
                      column: x => x.EntityId,
                      principalTable: "Post",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_StorageReferences_Storages_StorageId",
                      column: x => x.StorageId,
                      principalTable: "Storages",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateIndex(
          name: "IX_Post_Slug",
          table: "Post",
          column: "Slug",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_StorageReferences_EntityId",
          table: "StorageReferences",
          column: "EntityId");

      migrationBuilder.AddForeignKey(
          name: "FK_Newsletters_Post_PostId",
          table: "Newsletters",
          column: "PostId",
          principalTable: "Post",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "FK_Post_User_UserId",
          table: "Post",
          column: "UserId",
          principalTable: "User",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "FK_PostCategories_Post_PostId",
          table: "PostCategories",
          column: "PostId",
          principalTable: "Post",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_Newsletters_Post_PostId",
          table: "Newsletters");

      migrationBuilder.DropForeignKey(
          name: "FK_Post_User_UserId",
          table: "Post");

      migrationBuilder.DropForeignKey(
          name: "FK_PostCategories_Post_PostId",
          table: "PostCategories");

      migrationBuilder.DropTable(
          name: "StorageReferences");

      migrationBuilder.DropPrimaryKey(
          name: "PK_Post",
          table: "Post");

      migrationBuilder.DropIndex(
          name: "IX_Post_Slug",
          table: "Post");

      migrationBuilder.RenameTable(
          name: "Post",
          newName: "Posts");

      migrationBuilder.RenameIndex(
          name: "IX_Post_UserId",
          table: "Posts",
          newName: "IX_Posts_UserId");

      migrationBuilder.AddPrimaryKey(
          name: "PK_Posts",
          table: "Posts",
          column: "Id");

      migrationBuilder.AddForeignKey(
          name: "FK_Newsletters_Posts_PostId",
          table: "Newsletters",
          column: "PostId",
          principalTable: "Posts",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "FK_PostCategories_Posts_PostId",
          table: "PostCategories",
          column: "PostId",
          principalTable: "Posts",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "FK_Posts_User_UserId",
          table: "Posts",
          column: "UserId",
          principalTable: "User",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);
    }
  }
}
