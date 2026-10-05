using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dashagram.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameUrlToKeyPostImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "PostImages",
                newName: "Key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Key",
                table: "PostImages",
                newName: "Url");
        }
    }
}
