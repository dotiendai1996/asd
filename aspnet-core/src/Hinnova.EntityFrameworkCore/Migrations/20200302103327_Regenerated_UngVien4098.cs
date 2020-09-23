using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regenerated_UngVien4098 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "UngVien",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UngVien",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "UngVien");
        }
    }
}
