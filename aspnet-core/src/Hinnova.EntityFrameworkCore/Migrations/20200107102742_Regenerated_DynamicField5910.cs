using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regenerated_DynamicField5910 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClassAttach",
                table: "DynamicField",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WidthDescription",
                table: "DynamicField",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassAttach",
                table: "DynamicField");

            migrationBuilder.DropColumn(
                name: "WidthDescription",
                table: "DynamicField");
        }
    }
}
