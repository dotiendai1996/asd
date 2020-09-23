using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class update_tableUngVien113 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NgayPheDuyet",
                table: "UngVien",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiPheDuyet",
                table: "UngVien",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayPheDuyet",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "NguoiPheDuyet",
                table: "UngVien");
        }
    }
}
