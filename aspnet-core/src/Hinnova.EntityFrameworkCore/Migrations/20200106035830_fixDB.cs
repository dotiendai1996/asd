using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class fixDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DynamicFieldId",
                table: "DynamicValue",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "DynamicField",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClassAttach",
                table: "DynamicField",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WidthDescription",
                table: "DynamicField",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassAttach",
                table: "DynamicField");

            migrationBuilder.DropColumn(
                name: "WidthDescription",
                table: "DynamicField");

            migrationBuilder.AlterColumn<int>(
                name: "DynamicFieldId",
                table: "DynamicValue",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "DynamicField",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
