using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cabanoss.Core.Migrations
{
    /// <inheritdoc />
    public partial class CardUsersEnitity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardUser",
                columns: table => new
                {
                    BoardUserId = table.Column<int>(type: "int", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardUser", x => new { x.BoardUserId, x.CardId });
                    table.ForeignKey(
                        name: "FK_CardUser_BoardsUser_BoardUserId_CardId",
                        columns: x => new { x.BoardUserId, x.CardId },
                        principalTable: "BoardsUser",
                        principalColumns: new[] { "BoardId", "UserId" });
                    table.ForeignKey(
                        name: "FK_CardUser_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardUser_CardId",
                table: "CardUser",
                column: "CardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardUser");
        }
    }
}
