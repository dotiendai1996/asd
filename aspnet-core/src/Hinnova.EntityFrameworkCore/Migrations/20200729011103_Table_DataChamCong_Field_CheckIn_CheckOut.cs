using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Table_DataChamCong_Field_CheckIn_CheckOut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CheckIn",
                table: "DataChamCongs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckOut",
                table: "DataChamCongs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckIn",
                table: "DataChamCongs");

            migrationBuilder.DropColumn(
                name: "CheckOut",
                table: "DataChamCongs");
        }
    }
}
