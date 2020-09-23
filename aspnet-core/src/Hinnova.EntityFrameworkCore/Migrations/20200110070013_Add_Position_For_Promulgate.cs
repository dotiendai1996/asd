using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Add_Position_For_Promulgate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "CA_DocumentTypes");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "CA_Promulgateds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "CA_Promulgateds");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CA_DocumentTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
