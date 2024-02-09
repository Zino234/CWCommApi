using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeCommApi.Migrations
{
    /// <inheritdoc />
    public partial class AlteredThePostModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupMessageStatuses");

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PostTitle = table.Column<string>(type: "TEXT", nullable: true),
                    PostType = table.Column<int>(type: "INTEGER", nullable: false),
                    PostBody = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PostCreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PostUpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PostIsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_GroupId",
                table: "UserGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_Groups_GroupId",
                table: "UserGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_Users_UserId",
                table: "UserGroups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_Groups_GroupId",
                table: "UserGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_Users_UserId",
                table: "UserGroups");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_UserGroups_GroupId",
                table: "UserGroups");

            migrationBuilder.CreateTable(
                name: "GroupMessageStatuses",
                columns: table => new
                {
                    GroupMessageId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeliveredAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SeenAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMessageStatuses", x => new { x.GroupMessageId, x.UserId });
                });
        }
    }
}
