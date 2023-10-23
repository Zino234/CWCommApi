using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeCommApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedChatsModel : Migration
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
                    table.ForeignKey(
                        name: "FK_Chats_Users_UserID1",
                        column: x => x.UserID1,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chats_Users_UserID2",
                        column: x => x.UserID2,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserID2",
                table: "Chats",
                column: "UserID2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chats");
        }
    }
}
