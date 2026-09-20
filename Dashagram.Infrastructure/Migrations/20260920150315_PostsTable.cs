using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dashagram.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PostsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_AspNetUsers_OwnerId",
                table: "Dogs");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Dogs",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Dogs_OwnerId",
                table: "Dogs",
                newName: "IX_Dogs_UserId");

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostImages_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4127));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4812));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4813));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4815));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4815));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4816));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4817));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4817));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4818));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4819));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4819));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4820));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4820));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4821));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4822));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4822));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4823));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4824));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4824));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4825));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4826));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4826));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4827));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4828));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4828));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4829));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4830));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4830));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4831));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 20, 15, 3, 15, 207, DateTimeKind.Utc).AddTicks(4832));

            migrationBuilder.CreateIndex(
                name: "IX_PostImages_PostId",
                table: "PostImages",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_AspNetUsers_UserId",
                table: "Dogs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_AspNetUsers_UserId",
                table: "Dogs");

            migrationBuilder.DropTable(
                name: "PostImages");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Dogs",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Dogs_UserId",
                table: "Dogs",
                newName: "IX_Dogs_OwnerId");

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Breeds",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_AspNetUsers_OwnerId",
                table: "Dogs",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
