using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogifier.Core.Data.Migrations
{
    public partial class CommentsLikeByGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LikedUserEmail",
                table: "CommentsLike",
                newName: "LikedByGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LikedByGuid",
                table: "CommentsLike",
                newName: "LikedUserEmail");
        }
    }
}
