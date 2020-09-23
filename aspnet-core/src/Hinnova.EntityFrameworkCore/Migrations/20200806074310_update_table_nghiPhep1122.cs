using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class update_table_nghiPhep1122 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "QuanLyNghiPheps");

            migrationBuilder.AddColumn<int>(
                name: "TrangThaiID",
                table: "QuanLyNghiPheps",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThaiID",
                table: "QuanLyNghiPheps");

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai",
                table: "QuanLyNghiPheps",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
