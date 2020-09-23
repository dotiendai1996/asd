using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class update_table_hoso1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "APPROVE_DT",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "AUTH_STATUS",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "CHECKER_ID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "DonViCongTacName",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "GioiTinhCode",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "LoaiHopDongID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "MARKER_ID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NgayHetHan",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NoiDaoTaoID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "RECORD_STATUS",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "XepLoaiCode",
                table: "HoSo");

            migrationBuilder.RenameColumn(
                name: "TenCty",
                table: "HoSo",
                newName: "TenCTY");

            migrationBuilder.AddColumn<string>(
                name: "ChiNhanh",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DVT",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GioiTinh",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MucLuong",
                table: "HoSo",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "NganHang",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKyHD12",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKyHD36",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKyHDCTV",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKyHDKTH",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKyHDKV",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKyHDTT",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKyHDTv",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoiDaoTao",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhongBan",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SDT",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STK",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrinhDoDaoTao",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XepLoai",
                table: "HoSo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChiNhanh",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "DVT",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "GioiTinh",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "MucLuong",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NganHang",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NgayKyHD12",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NgayKyHD36",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NgayKyHDCTV",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NgayKyHDKTH",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NgayKyHDKV",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NgayKyHDTT",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NgayKyHDTv",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NoiDaoTao",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "PhongBan",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "SDT",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "STK",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "TrinhDoDaoTao",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "XepLoai",
                table: "HoSo");

            migrationBuilder.RenameColumn(
                name: "TenCTY",
                table: "HoSo",
                newName: "TenCty");

            migrationBuilder.AddColumn<DateTime>(
                name: "APPROVE_DT",
                table: "HoSo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AUTH_STATUS",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CHECKER_ID",
                table: "HoSo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DonViCongTacName",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GioiTinhCode",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoaiHopDongID",
                table: "HoSo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MARKER_ID",
                table: "HoSo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayHetHan",
                table: "HoSo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoiDaoTaoID",
                table: "HoSo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RECORD_STATUS",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XepLoaiCode",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
