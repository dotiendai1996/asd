using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class update_table_hoso3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NgayHetHan",
                table: "HoSo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKyHD",
                table: "HoSo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayHetHan",
                table: "HoSo");

            migrationBuilder.DropColumn(
                name: "NgayKyHD",
                table: "HoSo");
        }
    }
}
