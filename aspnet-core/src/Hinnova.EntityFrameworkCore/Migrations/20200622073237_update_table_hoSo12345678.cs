using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class update_table_hoSo12345678 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "HopDongHienTaiID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "MaCTYID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "MucLuong",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NganHangID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NgayKyHD12",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NgayKyHD36",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NguoiNhapCV",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "PhongBanID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "SDT",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "STK",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "TrinhDoDaoTaoID",
                table: "HoSo");

            migrationBuilder.RenameColumn(
                name: "NgayKyHDTv",
                table: "HoSo",
                newName: "NgayKyHDTV");

            migrationBuilder.RenameColumn(
                name: "NgayKyHDTT",
                table: "HoSo",
                newName: "NgayKYHDTT");

            migrationBuilder.RenameColumn(
                name: "NgayKyHDCTV",
                table: "HoSo",
                newName: "NgayKYHDCTV");

            migrationBuilder.AlterColumn<double>(
                name: "TyLeDongBH",
                table: "HoSo",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TinhThanhIDHN",
                table: "HoSo",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TinhThanhIDHKTT",
                table: "HoSo",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TinhThanhID",
                table: "HoSo",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "SoNgayPhep",
                table: "HoSo",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SoCongChuan",
                table: "HoSo",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoiDangKyKCBID",
                table: "HoSo",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayThuViec",
                table: "HoSo",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayThamGiaBH",
                table: "HoSo",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTapSu",
                table: "HoSo",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgaySinh",
                table: "HoSo",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayKyHDTV",
                table: "HoSo",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayKYHDTT",
                table: "HoSo",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayKyHDKV",
                table: "HoSo",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayKyHDKTH",
                table: "HoSo",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayKYHDCTV",
                table: "HoSo",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayKyHD",
                table: "HoSo",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayHetHanBHYT",
                table: "HoSo",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayHetHan",
                table: "HoSo",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayChinhThuc",
                table: "HoSo",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCap",
                table: "HoSo",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NamTotNghiep",
                table: "HoSo",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LuongDongBH",
                table: "HoSo",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LoaiHopDongID",
                table: "HoSo",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DonViCongTacName",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HopDongHienTai",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKyHD12TH",
                table: "HoSo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKyHD36TH",
                table: "HoSo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TenCty",
                table: "HoSo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonViCongTacName",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "HopDongHienTai",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NgayKyHD12TH",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NgayKyHD36TH",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "TenCty",
                table: "HoSo");

            migrationBuilder.RenameColumn(
                name: "NgayKyHDTV",
                table: "HoSo",
                newName: "NgayKyHDTv");

            migrationBuilder.RenameColumn(
                name: "NgayKYHDTT",
                table: "HoSo",
                newName: "NgayKyHDTT");

            migrationBuilder.RenameColumn(
                name: "NgayKYHDCTV",
                table: "HoSo",
                newName: "NgayKyHDCTV");

            migrationBuilder.AlterColumn<double>(
                name: "TyLeDongBH",
                table: "HoSo",
                type: "float",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "TinhThanhIDHN",
                table: "HoSo",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TinhThanhIDHKTT",
                table: "HoSo",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TinhThanhID",
                table: "HoSo",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "SoNgayPhep",
                table: "HoSo",
                type: "float",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "SoCongChuan",
                table: "HoSo",
                type: "float",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoiDangKyKCBID",
                table: "HoSo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayThuViec",
                table: "HoSo",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayThamGiaBH",
                table: "HoSo",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTapSu",
                table: "HoSo",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgaySinh",
                table: "HoSo",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayKyHDTv",
                table: "HoSo",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayKyHDKV",
                table: "HoSo",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayKyHDKTH",
                table: "HoSo",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayKyHD",
                table: "HoSo",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayKyHDTT",
                table: "HoSo",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayKyHDCTV",
                table: "HoSo",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayHetHanBHYT",
                table: "HoSo",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayHetHan",
                table: "HoSo",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayChinhThuc",
                table: "HoSo",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCap",
                table: "HoSo",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "NamTotNghiep",
                table: "HoSo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "LuongDongBH",
                table: "HoSo",
                type: "float",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LoaiHopDongID",
                table: "HoSo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HopDongHienTaiID",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaCTYID",
                table: "HoSo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "MucLuong",
                table: "HoSo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "NganHangID",
                table: "HoSo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKyHD12",
                table: "HoSo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKyHD36",
                table: "HoSo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiNhapCV",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhongBanID",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SDT",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STK",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrinhDoDaoTaoID",
                table: "HoSo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
