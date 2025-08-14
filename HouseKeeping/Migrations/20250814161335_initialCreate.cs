using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseKeeping.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Housekeepers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Housekeepers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyJobEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    DayWork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HousekeeperId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyJobEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyJobEntries_Housekeepers_HousekeeperId",
                        column: x => x.HousekeeperId,
                        principalTable: "Housekeepers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyJobEntries_HousekeeperId",
                table: "DailyJobEntries",
                column: "HousekeeperId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyJobEntries");

            migrationBuilder.DropTable(
                name: "Housekeepers");
        }
    }
}
