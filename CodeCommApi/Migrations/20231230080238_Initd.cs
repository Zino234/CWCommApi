using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeCommApi.Migrations
{
    /// <inheritdoc />
    public partial class Initd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserID2",
                table: "Chats",
                column: "UserID2");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_UserID1",
                table: "Chats",
                column: "UserID1",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_UserID2",
                table: "Chats",
                column: "UserID2",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_UserID1",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_UserID2",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_UserID2",
                table: "Chats");
        }
    }
}
