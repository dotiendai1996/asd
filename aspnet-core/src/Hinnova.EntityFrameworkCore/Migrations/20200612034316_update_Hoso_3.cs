using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class update_Hoso_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoiDangKyKCBID",
                table: "HoSo");

            migrationBuilder.AddColumn<string>(
                name: "NoiDangKyKCB",
                table: "HoSo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoiDangKyKCB",
                table: "HoSo");

            migrationBuilder.AddColumn<int>(
                name: "NoiDangKyKCBID",
                table: "HoSo",
                type: "int",
                nullable: true);
        }
    }
}
