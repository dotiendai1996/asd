using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regenerated_QuanLyNghiPhep6184 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhongBan",
                table: "QuanLyNghiPheps");

            migrationBuilder.DropColumn(
                name: "QuanLyTrucTiep",
                table: "QuanLyNghiPheps");

            migrationBuilder.DropColumn(
                name: "TruongBoPhan",
                table: "QuanLyNghiPheps");

            migrationBuilder.AddColumn<int>(
                name: "DonViCongTacID",
                table: "QuanLyNghiPheps",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDuyet",
                table: "QuanLyNghiPheps",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NguoiDuyetID",
                table: "QuanLyNghiPheps",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuanLyTrucTiepID",
                table: "QuanLyNghiPheps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenCTY",
                table: "QuanLyNghiPheps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrangThai",
                table: "QuanLyNghiPheps",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TruongBoPhanID",
                table: "QuanLyNghiPheps",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonViCongTacID",
                table: "QuanLyNghiPheps");

            migrationBuilder.DropColumn(
                name: "NgayDuyet",
                table: "QuanLyNghiPheps");

            migrationBuilder.DropColumn(
                name: "NguoiDuyetID",
                table: "QuanLyNghiPheps");

            migrationBuilder.DropColumn(
                name: "QuanLyTrucTiepID",
                table: "QuanLyNghiPheps");

            migrationBuilder.DropColumn(
                name: "TenCTY",
                table: "QuanLyNghiPheps");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "QuanLyNghiPheps");

            migrationBuilder.DropColumn(
                name: "TruongBoPhanID",
                table: "QuanLyNghiPheps");

            migrationBuilder.AddColumn<int>(
                name: "PhongBan",
                table: "QuanLyNghiPheps",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuanLyTrucTiep",
                table: "QuanLyNghiPheps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TruongBoPhan",
                table: "QuanLyNghiPheps",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
