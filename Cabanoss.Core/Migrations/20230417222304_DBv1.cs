using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cabanoss.Core.Migrations
{
    /// <inheritdoc />
    public partial class DBv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardsUser_Users_UserId",
                table: "BoardsUser");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "BoardsUser");

            migrationBuilder.AddColumn<int>(
                name: "Roles",
                table: "BoardsUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardsUser_Users_UserId",
                table: "BoardsUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardsUser_Users_UserId",
                table: "BoardsUser");

            migrationBuilder.DropColumn(
                name: "Roles",
                table: "BoardsUser");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "BoardsUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardsUser_Users_UserId",
                table: "BoardsUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
