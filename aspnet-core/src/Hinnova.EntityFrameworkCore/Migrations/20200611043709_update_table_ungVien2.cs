using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class update_table_ungVien2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TienDoTuyenDungCode",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "TrangThaiCode",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "XepLoaiCode",
                table: "UngVien");

            migrationBuilder.AddColumn<string>(
                name: "TienDoTuyenDung",
                table: "UngVien",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrangThai",
                table: "UngVien",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XepLoai",
                table: "UngVien",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TienDoTuyenDung",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "XepLoai",
                table: "UngVien");

            migrationBuilder.AddColumn<string>(
                name: "TienDoTuyenDungCode",
                table: "UngVien",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCode",
                table: "UngVien",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XepLoaiCode",
                table: "UngVien",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
