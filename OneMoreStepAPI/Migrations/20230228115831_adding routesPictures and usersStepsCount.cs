using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OneMoreStepAPI.Migrations
{
    public partial class addingroutesPicturesandusersStepsCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoutesPictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutesPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoutesPictures_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersStepsCount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StepsCount = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersStepsCount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersStepsCount_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoutesPictures_RouteId",
                table: "RoutesPictures",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersStepsCount_UserId",
                table: "UsersStepsCount",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoutesPictures");

            migrationBuilder.DropTable(
                name: "UsersStepsCount");
        }
    }
}
