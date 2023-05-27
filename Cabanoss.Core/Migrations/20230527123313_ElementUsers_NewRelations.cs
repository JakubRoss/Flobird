using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cabanoss.Core.Migrations
{
    /// <inheritdoc />
    public partial class ElementUsers_NewRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElementUsers_BoardsUser_BoardUserId_ElementId",
                table: "ElementUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ElementUsers_Element_ElementId",
                table: "ElementUsers");

            migrationBuilder.RenameColumn(
                name: "ElementId",
                table: "ElementUsers",
                newName: "ElementId_eu");

            migrationBuilder.RenameColumn(
                name: "BoardUserId",
                table: "ElementUsers",
                newName: "UserId_eu");

            migrationBuilder.RenameIndex(
                name: "IX_ElementUsers_ElementId",
                table: "ElementUsers",
                newName: "IX_ElementUsers_ElementId_eu");

            migrationBuilder.AddColumn<int>(
                name: "BoardUserBoardId",
                table: "ElementUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BoardUserUserId",
                table: "ElementUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ElementUsers_BoardUserBoardId_BoardUserUserId",
                table: "ElementUsers",
                columns: new[] { "BoardUserBoardId", "BoardUserUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ElementUsers_BoardsUser_BoardUserBoardId_BoardUserUserId",
                table: "ElementUsers",
                columns: new[] { "BoardUserBoardId", "BoardUserUserId" },
                principalTable: "BoardsUser",
                principalColumns: new[] { "BoardId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ElementUsers_Element_ElementId_eu",
                table: "ElementUsers",
                column: "ElementId_eu",
                principalTable: "Element",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ElementUsers_Users_UserId_eu",
                table: "ElementUsers",
                column: "UserId_eu",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElementUsers_BoardsUser_BoardUserBoardId_BoardUserUserId",
                table: "ElementUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ElementUsers_Element_ElementId_eu",
                table: "ElementUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ElementUsers_Users_UserId_eu",
                table: "ElementUsers");

            migrationBuilder.DropIndex(
                name: "IX_ElementUsers_BoardUserBoardId_BoardUserUserId",
                table: "ElementUsers");

            migrationBuilder.DropColumn(
                name: "BoardUserBoardId",
                table: "ElementUsers");

            migrationBuilder.DropColumn(
                name: "BoardUserUserId",
                table: "ElementUsers");

            migrationBuilder.RenameColumn(
                name: "ElementId_eu",
                table: "ElementUsers",
                newName: "ElementId");

            migrationBuilder.RenameColumn(
                name: "UserId_eu",
                table: "ElementUsers",
                newName: "BoardUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ElementUsers_ElementId_eu",
                table: "ElementUsers",
                newName: "IX_ElementUsers_ElementId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElementUsers_BoardsUser_BoardUserId_ElementId",
                table: "ElementUsers",
                columns: new[] { "BoardUserId", "ElementId" },
                principalTable: "BoardsUser",
                principalColumns: new[] { "BoardId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ElementUsers_Element_ElementId",
                table: "ElementUsers",
                column: "ElementId",
                principalTable: "Element",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
