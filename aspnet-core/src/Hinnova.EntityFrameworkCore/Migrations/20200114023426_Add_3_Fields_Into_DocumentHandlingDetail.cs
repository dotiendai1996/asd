using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Add_3_Fields_Into_DocumentHandlingDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CoHandling",
                table: "CA_DocumentHandlingDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MainHandling",
                table: "CA_DocumentHandlingDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ToKnow",
                table: "CA_DocumentHandlingDetails",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoHandling",
                table: "CA_DocumentHandlingDetails");

            migrationBuilder.DropColumn(
                name: "MainHandling",
                table: "CA_DocumentHandlingDetails");

            migrationBuilder.DropColumn(
                name: "ToKnow",
                table: "CA_DocumentHandlingDetails");
        }
    }
}
