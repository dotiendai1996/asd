using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class update_table_ungVien1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GioiTinhCode",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "KenhTuyenDungCode",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "NoiDaoTaoID",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "TinhThanhID",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "TinhTrangHonNhanCode",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "TrinhDoDaoTaoCode",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "ViTriUngTuyenCode",
                table: "UngVien");

            migrationBuilder.AddColumn<string>(
                name: "GioiTinh",
                table: "UngVien",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KenhTuyenDung",
                table: "UngVien",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoiDaoTao",
                table: "UngVien",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhThanh",
                table: "UngVien",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhTrangHonNhan",
                table: "UngVien",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrinhDoDaoTao",
                table: "UngVien",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ViTriUngTuyen",
                table: "UngVien",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GioiTinh",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "KenhTuyenDung",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "NoiDaoTao",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "TinhThanh",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "TinhTrangHonNhan",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "TrinhDoDaoTao",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "ViTriUngTuyen",
                table: "UngVien");

            migrationBuilder.AddColumn<string>(
                name: "GioiTinhCode",
                table: "UngVien",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KenhTuyenDungCode",
                table: "UngVien",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoiDaoTaoID",
                table: "UngVien",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TinhThanhID",
                table: "UngVien",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhTrangHonNhanCode",
                table: "UngVien",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrinhDoDaoTaoCode",
                table: "UngVien",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ViTriUngTuyenCode",
                table: "UngVien",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
