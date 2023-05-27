using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cabanoss.Core.Migrations
{
    /// <inheritdoc />
    public partial class Carduser_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardUser_BoardsUser_BoardUserId_CardId",
                table: "CardUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CardUser_Card_CardId",
                table: "CardUser");

            migrationBuilder.RenameColumn(
                name: "CardId",
                table: "CardUser",
                newName: "CardId_cu");

            migrationBuilder.RenameColumn(
                name: "BoardUserId",
                table: "CardUser",
                newName: "UserId_cu");

            migrationBuilder.RenameIndex(
                name: "IX_CardUser_CardId",
                table: "CardUser",
                newName: "IX_CardUser_CardId_cu");

            migrationBuilder.AddColumn<int>(
                name: "BoardUserBoardId",
                table: "CardUser",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BoardUserUserId",
                table: "CardUser",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardUser_BoardUserBoardId_BoardUserUserId",
                table: "CardUser",
                columns: new[] { "BoardUserBoardId", "BoardUserUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CardUser_BoardsUser_BoardUserBoardId_BoardUserUserId",
                table: "CardUser",
                columns: new[] { "BoardUserBoardId", "BoardUserUserId" },
                principalTable: "BoardsUser",
                principalColumns: new[] { "BoardId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CardUser_Card_CardId_cu",
                table: "CardUser",
                column: "CardId_cu",
                principalTable: "Card",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CardUser_Users_UserId_cu",
                table: "CardUser",
                column: "UserId_cu",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardUser_BoardsUser_BoardUserBoardId_BoardUserUserId",
                table: "CardUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CardUser_Card_CardId_cu",
                table: "CardUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CardUser_Users_UserId_cu",
                table: "CardUser");

            migrationBuilder.DropIndex(
                name: "IX_CardUser_BoardUserBoardId_BoardUserUserId",
                table: "CardUser");

            migrationBuilder.DropColumn(
                name: "BoardUserBoardId",
                table: "CardUser");

            migrationBuilder.DropColumn(
                name: "BoardUserUserId",
                table: "CardUser");

            migrationBuilder.RenameColumn(
                name: "CardId_cu",
                table: "CardUser",
                newName: "CardId");

            migrationBuilder.RenameColumn(
                name: "UserId_cu",
                table: "CardUser",
                newName: "BoardUserId");

            migrationBuilder.RenameIndex(
                name: "IX_CardUser_CardId_cu",
                table: "CardUser",
                newName: "IX_CardUser_CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardUser_BoardsUser_BoardUserId_CardId",
                table: "CardUser",
                columns: new[] { "BoardUserId", "CardId" },
                principalTable: "BoardsUser",
                principalColumns: new[] { "BoardId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CardUser_Card_CardId",
                table: "CardUser",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
