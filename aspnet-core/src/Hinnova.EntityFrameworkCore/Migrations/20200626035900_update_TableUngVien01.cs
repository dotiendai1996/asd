using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class update_TableUngVien01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NguoiPheDuyet",
                table: "UngVien");

            migrationBuilder.AddColumn<int>(
                name: "MaNguoiPheDuyet",
                table: "UngVien",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaNguoiPheDuyet",
                table: "UngVien");

            migrationBuilder.AddColumn<string>(
                name: "NguoiPheDuyet",
                table: "UngVien",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
