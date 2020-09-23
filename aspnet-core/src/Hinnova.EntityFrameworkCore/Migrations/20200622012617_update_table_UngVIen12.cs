using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class update_table_UngVIen12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "TienDoTuyenDung",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "TinhThanh",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "TinhTrangHonNhan",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "TrinhDoDaoTao",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "ViTriUngTuyen",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "XepLoai",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "ChuyenNganh",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "DonViCongTac",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "HopDongHienTai",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NganHangCode",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NoiDangKyKCB",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NoiDaoTao",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "PhongBan",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "TenCTY",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "TinhThanhHKTT",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "TinhTrangHonNhanCode",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "TrinhDoDaoTao",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "ViTriCongViecCode",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "XepLoai",
                table: "HoSo");

            migrationBuilder.AddColumn<string>(
                name: "GioiTinhCode",
                table: "UngVien",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KenhTuyenDungCode",
                table: "UngVien",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoiDaoTaoID",
                table: "UngVien",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TienDoTuyenDungCode",
                table: "UngVien",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TinhThanhID",
                table: "UngVien",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TinhTrangHonNhanCode",
                table: "UngVien",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiCode",
                table: "UngVien",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrinhDoDaoTaoCode",
                table: "UngVien",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ViTriUngTuyenCode",
                table: "UngVien",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XepLoaiCode",
                table: "UngVien",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChuyenNganhID",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DonViCongTacID",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HopDongHienTaiID",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaCTYID",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NganHangID",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoiDangKyKCBID",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoiDaoTaoID",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PhongBanID",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TinhThanhIDHKTT",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TinhTrangHonNhanID",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrinhDoDaoTaoID",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViTriCongViecID",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "XepLoaiID",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "TienDoTuyenDungCode",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "TinhThanhID",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "TinhTrangHonNhanCode",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "TrangThaiCode",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "TrinhDoDaoTaoCode",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "ViTriUngTuyenCode",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "XepLoaiCode",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "ChuyenNganhID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "DonViCongTacID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "HopDongHienTaiID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "MaCTYID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NganHangID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NoiDangKyKCBID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NoiDaoTaoID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "PhongBanID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "TinhThanhIDHKTT",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "TinhTrangHonNhanID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "TrinhDoDaoTaoID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "ViTriCongViecID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "XepLoaiID",
                table: "HoSo");

            migrationBuilder.AddColumn<string>(
                name: "GioiTinh",
                table: "UngVien",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KenhTuyenDung",
                table: "UngVien",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoiDaoTao",
                table: "UngVien",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TienDoTuyenDung",
                table: "UngVien",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhThanh",
                table: "UngVien",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhTrangHonNhan",
                table: "UngVien",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrangThai",
                table: "UngVien",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrinhDoDaoTao",
                table: "UngVien",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ViTriUngTuyen",
                table: "UngVien",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XepLoai",
                table: "UngVien",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChuyenNganh",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DonViCongTac",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HopDongHienTai",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NganHangCode",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoiDangKyKCB",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoiDaoTao",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhongBan",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenCTY",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhThanhHKTT",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhTrangHonNhanCode",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrinhDoDaoTao",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ViTriCongViecCode",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XepLoai",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
