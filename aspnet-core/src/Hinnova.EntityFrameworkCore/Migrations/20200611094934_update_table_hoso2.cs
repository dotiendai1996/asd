using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class update_table_hoso2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonViCongTacID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "TinhThanhIDHKTT",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "TinhThanhIDHN",
                table: "HoSo");

            migrationBuilder.AddColumn<string>(
                name: "DonViCongTac",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhThanhHKTT",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhThanhHN",
                table: "HoSo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonViCongTac",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "TinhThanhHKTT",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "TinhThanhHN",
                table: "HoSo");

            migrationBuilder.AddColumn<int>(
                name: "DonViCongTacID",
                table: "HoSo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TinhThanhIDHKTT",
                table: "HoSo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TinhThanhIDHN",
                table: "HoSo",
                type: "int",
                nullable: true);
        }
    }
}
