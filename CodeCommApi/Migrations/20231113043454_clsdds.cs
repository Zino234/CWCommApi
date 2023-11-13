using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeCommApi.Migrations
{
    /// <inheritdoc />
    public partial class clsdds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    UserID1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID2 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => new { x.UserID1, x.UserID2 });
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => new { x.UserId, x.GroupId });
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupLogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupIsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserProfilePicUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserIsVerified = table.Column<bool>(type: "bit", nullable: false),
                    UserAccountIsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    UserCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupsGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Groups_GroupsGroupId",
                        column: x => x.GroupsGroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_UserId",
                table: "Groups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupsGroupId",
                table: "Users",
                column: "GroupsGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_UserId",
                table: "Groups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_UserId",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
