using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class update_TableUngVien12222 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TieuDe",
                table: "UngVien",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NVPhuTrach",
                table: "UngVien",
                nullable: true);
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TieuDe",
                table: "UngVien");

            migrationBuilder.DropColumn(
                name: "NVPhuTrach",
                table: "UngVien");
        }
    }
}
