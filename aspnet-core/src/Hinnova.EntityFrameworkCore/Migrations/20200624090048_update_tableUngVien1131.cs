using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class update_tableUngVien1131 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "NhanVienPhuTrach",
            //    table: "UngVien");

            //migrationBuilder.AddColumn<string>(
            //    name: "NVPhuTrach",
            //    table: "UngVien",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "NVPhuTrach",
            //    table: "UngVien");

            //migrationBuilder.AddColumn<string>(
            //    name: "NhanVienPhuTrach",
            //    table: "UngVien",
            //    type: "nvarchar(max)",
            //    nullable: true);
        }
    }
}
