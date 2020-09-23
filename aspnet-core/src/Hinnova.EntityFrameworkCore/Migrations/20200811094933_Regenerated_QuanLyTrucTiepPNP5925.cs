using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regenerated_QuanLyTrucTiepPNP5925 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QLTTID",
                table: "QuanLyTrucTiepPNPs");

            migrationBuilder.AddColumn<string>(
                name: "QLTT",
                table: "QuanLyTrucTiepPNPs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QLTT",
                table: "QuanLyTrucTiepPNPs");

            migrationBuilder.AddColumn<int>(
                name: "QLTTID",
                table: "QuanLyTrucTiepPNPs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
