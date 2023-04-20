using Microsoft.EntityFrameworkCore.Migrations;

namespace OneMoreStepAPI.Migrations
{
    public partial class FixUsersPinnedStickerOnetoOneRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersStickers_Users_UserId1",
                table: "UsersStickers");

            migrationBuilder.DropIndex(
                name: "IX_UsersStickers_UserId1",
                table: "UsersStickers");

            migrationBuilder.DropIndex(
                name: "IX_UsersPinnedStickers_UserId",
                table: "UsersPinnedStickers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UsersStickers");

            migrationBuilder.CreateIndex(
                name: "IX_UsersPinnedStickers_UserId",
                table: "UsersPinnedStickers",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersPinnedStickers_UserId",
                table: "UsersPinnedStickers");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "UsersStickers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersStickers_UserId1",
                table: "UsersStickers",
                column: "UserId1",
                unique: true,
                filter: "[UserId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UsersPinnedStickers_UserId",
                table: "UsersPinnedStickers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersStickers_Users_UserId1",
                table: "UsersStickers",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
