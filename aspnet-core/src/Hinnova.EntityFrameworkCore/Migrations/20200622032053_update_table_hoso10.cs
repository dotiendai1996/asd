using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class update_table_hoso10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChuyenNganhID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "GioiTinh",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NganHang",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "TinhThanhHN",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "TinhTrangHonNhanID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "ViTriCongViecID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "XepLoaiID",
                table: "HoSo");

            //migrationBuilder.AlterColumn<double>(
            //    name: "LuongCoBan",
            //    table: "HoSo",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(max)",
            //    oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChuyenNganh",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GioiTinhCode",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LoaiHopDongID",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NganHangCode",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TinhThanhIDHN",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhTrangHonNhanCode",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ViTriCongViecCode",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XepLoaiCode",
                table: "HoSo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChuyenNganh",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "GioiTinhCode",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "LoaiHopDongID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NganHangCode",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "TinhThanhIDHN",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "TinhTrangHonNhanCode",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "ViTriCongViecCode",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "XepLoaiCode",
                table: "HoSo");

            //migrationBuilder.AlterColumn<string>(
            //    name: "LuongCoBan",
            //    table: "HoSo",
            //    type: "nvarchar(max)",
            //    nullable: true,
            //    oldClrType: typeof(double));

            migrationBuilder.AddColumn<string>(
                name: "ChuyenNganhID",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GioiTinh",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NganHang",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhThanhHN",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TinhTrangHonNhanID",
                table: "HoSo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViTriCongViecID",
                table: "HoSo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "XepLoaiID",
                table: "HoSo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
