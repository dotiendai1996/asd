using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Add_More_fields_DynamicAction_CellTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HasAssignWork",
                table: "DynamicAction",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "CellTemplate",
                table: "DynamicAction",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "DynamicAction",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CellTemplate",
                table: "DynamicAction");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "DynamicAction");

            migrationBuilder.AlterColumn<bool>(
                name: "HasAssignWork",
                table: "DynamicAction",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
