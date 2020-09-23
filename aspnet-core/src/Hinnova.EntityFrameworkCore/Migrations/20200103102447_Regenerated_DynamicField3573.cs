using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regenerated_DynamicField3573 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "DynamicField");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DynamicField");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "DynamicField");

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

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "DynamicField",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DynamicField",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "DynamicField",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
