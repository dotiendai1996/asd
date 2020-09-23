using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regenerated_DocumentHandlingDetail7477 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CoHandling",
                table: "DocumentHandlingDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MainHandling",
                table: "DocumentHandlingDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ToKnow",
                table: "DocumentHandlingDetails",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoHandling",
                table: "DocumentHandlingDetails");

            migrationBuilder.DropColumn(
                name: "MainHandling",
                table: "DocumentHandlingDetails");

            migrationBuilder.DropColumn(
                name: "ToKnow",
                table: "DocumentHandlingDetails");
        }
    }
}
