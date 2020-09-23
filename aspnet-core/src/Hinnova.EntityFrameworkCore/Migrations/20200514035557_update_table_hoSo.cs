using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class update_table_hoSo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChoNgoi",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DonViCongTacName",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoHD",
                table: "HoSo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChoNgoi",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "DonViCongTacName",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "SoHD",
                table: "HoSo");
        }
    }
}
