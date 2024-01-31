using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeCommApi.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    UserID1 = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserID2 = table.Column<Guid>(type: "TEXT", nullable: false),
                    ChatId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => new { x.UserID1, x.UserID2 });
                });

            migrationBuilder.CreateTable(
                name: "DirectMessages",
                columns: table => new
                {
                    MessageId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MessageType = table.Column<int>(type: "INTEGER", nullable: false),
                    MessageBody = table.Column<string>(type: "TEXT", nullable: false),
                    MessageChatId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MessageSenderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MessageReceiverId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MessageSentTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MessageTimeDelivered = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MessageTimeSeen = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MessageUpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MessageIsDeleted = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectMessages", x => x.MessageId);
                });

            migrationBuilder.CreateTable(
                name: "GroupMessages",
                columns: table => new
                {
                    MessageId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MessageType = table.Column<int>(type: "INTEGER", nullable: false),
                    MessageBody = table.Column<string>(type: "TEXT", nullable: false),
                    GroupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MessageSentBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    MessageTimeSent = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EditedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    MessageIsDeleted = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMessages", x => x.MessageId);
                });

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

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GroupId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => new { x.UserId, x.GroupId });
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GroupName = table.Column<string>(type: "TEXT", nullable: false),
                    GroupDescription = table.Column<string>(type: "TEXT", nullable: false),
                    GroupLogo = table.Column<string>(type: "TEXT", nullable: false),
                    GroupCreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GroupIsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    UserEmail = table.Column<string>(type: "TEXT", nullable: false),
                    UserPhone = table.Column<string>(type: "TEXT", nullable: false),
                    UserProfilePicUrl = table.Column<string>(type: "TEXT", nullable: true),
                    UserPassword = table.Column<string>(type: "TEXT", nullable: false),
                    UserIsVerified = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserAccountIsDisabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserCreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GroupsGroupId = table.Column<Guid>(type: "TEXT", nullable: true)
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
                name: "DirectMessages");

            migrationBuilder.DropTable(
                name: "GroupMessages");

            migrationBuilder.DropTable(
                name: "GroupMessageStatuses");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
