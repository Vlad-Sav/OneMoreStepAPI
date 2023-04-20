using Microsoft.EntityFrameworkCore.Migrations;

namespace OneMoreStepAPI.Migrations
{
    public partial class Stickermodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sticker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sticker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersPinnedStickers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StickerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersPinnedStickers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersPinnedStickers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersStickers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StikerId = table.Column<int>(type: "int", nullable: false),
                    StickerId = table.Column<int>(type: "int", nullable: true),
                    UserId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersStickers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersStickers_Sticker_StickerId",
                        column: x => x.StickerId,
                        principalTable: "Sticker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersStickers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersStickers_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersPinnedStickers_UserId",
                table: "UsersPinnedStickers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersStickers_StickerId",
                table: "UsersStickers",
                column: "StickerId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersStickers_UserId",
                table: "UsersStickers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersStickers_UserId1",
                table: "UsersStickers",
                column: "UserId1",
                unique: true,
                filter: "[UserId1] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersPinnedStickers");

            migrationBuilder.DropTable(
                name: "UsersStickers");

            migrationBuilder.DropTable(
                name: "Sticker");
        }
    }
}
