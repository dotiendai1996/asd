using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regenerated_ViTriCongViec8438 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "TenCongViec",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "TenDonVi",
                table: "ViTriCongViecs");

            migrationBuilder.AddColumn<string>(
                name: "AnhDaiDien",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bac",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BacLuongCode",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChiNhanh",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChoNgoi",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChucDanh",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChuyenNganh",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DVT",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DanToc",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiaChiHKTT",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiaChiHN",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiaChiLHKC",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiaDiemLamViecCode",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DonViCongTacID",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DonViCongTacName",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DonViSoCongChuanCode",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DtCoQuan",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DtDiDong",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DtDiDongLHKC",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DtKhac",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DtNhaRieng",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DtNhaRiengLHKC",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailCaNhan",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailCoQuan",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailKhac",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailLHKC",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GioiTinhCode",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HoVaTen",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HoVaTenLHKC",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HopDongHienTai",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Khoa",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LaChuHo",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoaiHopDongID",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LuongCoBan",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LuongDongBH",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MSTCaNhan",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaChamCong",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaNhanVien",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaSoBHXH",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaSoHoGiaDinh",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaSoNoiKCB",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaTinhCap",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NamTotNghiep",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NganHangCode",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayCap",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayChinhThuc",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayHetHan",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayHetHanBHYT",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKYHDCTV",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKYHDTT",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKyHD",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKyHD12TH",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKyHD36TH",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKyHDKTH",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKyHDKV",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKyHDTV",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgaySinh",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTapSu",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayThamGiaBH",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayThuViec",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NguyenQuan",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoiCap",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoiDangKyKCBID",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoiDaoTaoID",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NoiSinh",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuanHeLHKC",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuanLyGianTiep",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuanLyTrucTiep",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuocGiaHKTT",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuocGiaHN",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuocTich",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Skype",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoCMND",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoCongChuan",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoHD",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SoNgayPhep",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "SoSoBHXH",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoSoHoKhau",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoSoQLLaoDong",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoTheBHYT",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenCty",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TepDinhKem",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ThamGiaCongDoan",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TinhThanhID",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhThanhIDHKTT",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhThanhIDHN",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhTrangHonNhanCode",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TkNganHang",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TonGiao",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiLamViecCode",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrinhDoDaoTaoCode",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrinhDoVanHoa",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TyLeDongBH",
                table: "ViTriCongViecs",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ViTriCongViecCode",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XepLoaiCode",
                table: "ViTriCongViecs",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LuongCoBan",
                table: "HoSo",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnhDaiDien",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "Bac",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "BacLuongCode",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "Cap",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "ChiNhanh",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "ChoNgoi",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "ChucDanh",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "ChuyenNganh",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "DVT",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "DanToc",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "DiaChiHKTT",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "DiaChiHN",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "DiaChiLHKC",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "DiaDiemLamViecCode",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "DonViCongTacID",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "DonViCongTacName",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "DonViSoCongChuanCode",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "DtCoQuan",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "DtDiDong",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "DtDiDongLHKC",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "DtKhac",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "DtNhaRieng",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "DtNhaRiengLHKC",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "EmailCaNhan",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "EmailCoQuan",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "EmailKhac",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "EmailLHKC",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "GioiTinhCode",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "HoVaTen",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "HoVaTenLHKC",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "HopDongHienTai",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "Khoa",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "LaChuHo",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "LoaiHopDongID",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "LuongCoBan",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "LuongDongBH",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "MSTCaNhan",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "MaChamCong",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "MaNhanVien",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "MaSoBHXH",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "MaSoHoGiaDinh",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "MaSoNoiKCB",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "MaTinhCap",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NamTotNghiep",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NganHangCode",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NgayCap",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NgayChinhThuc",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NgayHetHan",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NgayHetHanBHYT",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NgayKYHDCTV",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NgayKYHDTT",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NgayKyHD",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NgayKyHD12TH",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NgayKyHD36TH",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NgayKyHDKTH",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NgayKyHDKV",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NgayKyHDTV",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NgaySinh",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NgayTapSu",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NgayThamGiaBH",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NgayThuViec",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NguyenQuan",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NoiCap",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NoiDangKyKCBID",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NoiDaoTaoID",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "NoiSinh",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "QuanHeLHKC",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "QuanLyGianTiep",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "QuanLyTrucTiep",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "QuocGiaHKTT",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "QuocGiaHN",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "QuocTich",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "Skype",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "SoCMND",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "SoCongChuan",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "SoHD",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "SoNgayPhep",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "SoSoBHXH",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "SoSoHoKhau",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "SoSoQLLaoDong",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "SoTheBHYT",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "TenCty",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "TepDinhKem",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "ThamGiaCongDoan",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "TinhThanhID",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "TinhThanhIDHKTT",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "TinhThanhIDHN",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "TinhTrangHonNhanCode",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "TkNganHang",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "TonGiao",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "TrangThaiLamViecCode",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "TrinhDoDaoTaoCode",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "TrinhDoVanHoa",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "TyLeDongBH",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "ViTriCongViecCode",
                table: "ViTriCongViecs");

            migrationBuilder.DropColumn(
                name: "XepLoaiCode",
                table: "ViTriCongViecs");

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "ViTriCongViecs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenCongViec",
                table: "ViTriCongViecs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenDonVi",
                table: "ViTriCongViecs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "LuongCoBan",
                table: "HoSo",
                type: "float",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
