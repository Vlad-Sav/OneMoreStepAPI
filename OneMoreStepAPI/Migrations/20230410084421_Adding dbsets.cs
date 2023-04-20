using Microsoft.EntityFrameworkCore.Migrations;

namespace OneMoreStepAPI.Migrations
{
    public partial class Addingdbsets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersStickers_Sticker_StickerId",
                table: "UsersStickers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sticker",
                table: "Sticker");

            migrationBuilder.RenameTable(
                name: "Sticker",
                newName: "Stickers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stickers",
                table: "Stickers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsersPinnedStickers_StickerId",
                table: "UsersPinnedStickers",
                column: "StickerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersPinnedStickers_Stickers_StickerId",
                table: "UsersPinnedStickers",
                column: "StickerId",
                principalTable: "Stickers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersStickers_Stickers_StickerId",
                table: "UsersStickers",
                column: "StickerId",
                principalTable: "Stickers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersPinnedStickers_Stickers_StickerId",
                table: "UsersPinnedStickers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersStickers_Stickers_StickerId",
                table: "UsersStickers");

            migrationBuilder.DropIndex(
                name: "IX_UsersPinnedStickers_StickerId",
                table: "UsersPinnedStickers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stickers",
                table: "Stickers");

            migrationBuilder.RenameTable(
                name: "Stickers",
                newName: "Sticker");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sticker",
                table: "Sticker",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersStickers_Sticker_StickerId",
                table: "UsersStickers",
                column: "StickerId",
                principalTable: "Sticker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
