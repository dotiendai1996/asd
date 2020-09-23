using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class update_hoso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoaiHopDongCode",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NoiDaoTaoCode",
                table: "HoSo");

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "Day1",
            //    table: "UngVien",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "Day2",
            //    table: "UngVien",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "Day3",
            //    table: "UngVien",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Note",
            //    table: "UngVien",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Time1",
            //    table: "UngVien",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Time2",
            //    table: "UngVien",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Time3",
            //    table: "UngVien",
            //    nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoaiHopDongID",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoiDaoTaoID",
                table: "HoSo",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Day1",
            //    table: "UngVien");

            //migrationBuilder.DropColumn(
            //    name: "Day2",
            //    table: "UngVien");

            //migrationBuilder.DropColumn(
            //    name: "Day3",
            //    table: "UngVien");

            //migrationBuilder.DropColumn(
            //    name: "Note",
            //    table: "UngVien");

            //migrationBuilder.DropColumn(
            //    name: "Time1",
            //    table: "UngVien");

            //migrationBuilder.DropColumn(
            //    name: "Time2",
            //    table: "UngVien");

            //migrationBuilder.DropColumn(
            //    name: "Time3",
            //    table: "UngVien");

            migrationBuilder.DropColumn(
                name: "LoaiHopDongID",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NoiDaoTaoID",
                table: "HoSo");

            migrationBuilder.AddColumn<string>(
                name: "LoaiHopDongCode",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoiDaoTaoCode",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
