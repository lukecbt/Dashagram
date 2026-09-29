using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dashagram.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameLikesAndCommentsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostComment_Posts_PostId",
                table: "PostComment");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLike_Posts_PostId",
                table: "PostLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostLike",
                table: "PostLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostComment",
                table: "PostComment");

            migrationBuilder.RenameTable(
                name: "PostLike",
                newName: "PostLikes");

            migrationBuilder.RenameTable(
                name: "PostComment",
                newName: "PostComments");

            migrationBuilder.RenameIndex(
                name: "IX_PostLike_PostId",
                table: "PostLikes",
                newName: "IX_PostLikes_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostComment_PostId",
                table: "PostComments",
                newName: "IX_PostComments_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostLikes",
                table: "PostLikes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_Posts_PostId",
                table: "PostComments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_Posts_PostId",
                table: "PostLikes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_Posts_PostId",
                table: "PostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_Posts_PostId",
                table: "PostLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostLikes",
                table: "PostLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments");

            migrationBuilder.RenameTable(
                name: "PostLikes",
                newName: "PostLike");

            migrationBuilder.RenameTable(
                name: "PostComments",
                newName: "PostComment");

            migrationBuilder.RenameIndex(
                name: "IX_PostLikes_PostId",
                table: "PostLike",
                newName: "IX_PostLike_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostComments_PostId",
                table: "PostComment",
                newName: "IX_PostComment_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostLike",
                table: "PostLike",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostComment",
                table: "PostComment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostComment_Posts_PostId",
                table: "PostComment",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostLike_Posts_PostId",
                table: "PostLike",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
