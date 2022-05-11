using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogifier.Core.Data.Migrations
{
    public partial class CommentWithLike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentLiked",
                table: "Comments");

            migrationBuilder.CreateTable(
                name: "CommentsLike",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommentId = table.Column<long>(type: "INTEGER", nullable: false),
                    ExpressDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LikedUserEmail = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentsLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentsLike_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentsLike_CommentId",
                table: "CommentsLike",
                column: "CommentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentsLike");

            migrationBuilder.AddColumn<int>(
                name: "CommentLiked",
                table: "Comments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
