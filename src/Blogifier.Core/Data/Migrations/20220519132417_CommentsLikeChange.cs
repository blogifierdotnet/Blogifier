using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogifier.Core.Data.Migrations
{
    public partial class CommentsLikeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentsLike_Comments_CommentId",
                table: "CommentsLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentsLike",
                table: "CommentsLike");

            migrationBuilder.RenameTable(
                name: "CommentsLike",
                newName: "CommentsLikes");

            migrationBuilder.RenameIndex(
                name: "IX_CommentsLike_CommentId",
                table: "CommentsLikes",
                newName: "IX_CommentsLikes_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentsLikes",
                table: "CommentsLikes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentsLikes_Comments_CommentId",
                table: "CommentsLikes",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentsLikes_Comments_CommentId",
                table: "CommentsLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentsLikes",
                table: "CommentsLikes");

            migrationBuilder.RenameTable(
                name: "CommentsLikes",
                newName: "CommentsLike");

            migrationBuilder.RenameIndex(
                name: "IX_CommentsLikes_CommentId",
                table: "CommentsLike",
                newName: "IX_CommentsLike_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentsLike",
                table: "CommentsLike",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentsLike_Comments_CommentId",
                table: "CommentsLike",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
