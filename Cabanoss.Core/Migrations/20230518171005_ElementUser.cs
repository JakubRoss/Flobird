using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cabanoss.Core.Migrations
{
    /// <inheritdoc />
    public partial class ElementUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElementUsers",
                columns: table => new
                {
                    BoardUserId = table.Column<int>(type: "int", nullable: false),
                    ElementId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementUsers", x => new { x.BoardUserId, x.ElementId });
                    table.ForeignKey(
                        name: "FK_ElementUsers_BoardsUser_BoardUserId_ElementId",
                        columns: x => new { x.BoardUserId, x.ElementId },
                        principalTable: "BoardsUser",
                        principalColumns: new[] { "BoardId", "UserId" });
                    table.ForeignKey(
                        name: "FK_ElementUsers_Element_ElementId",
                        column: x => x.ElementId,
                        principalTable: "Element",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElementUsers_ElementId",
                table: "ElementUsers",
                column: "ElementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElementUsers");
        }
    }
}
