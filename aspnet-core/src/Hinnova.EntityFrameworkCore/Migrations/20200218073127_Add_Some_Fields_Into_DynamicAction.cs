using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Add_Some_Fields_Into_DynamicAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DynamicAction");

            migrationBuilder.DropColumn(
                name: "IsTopPosition",
                table: "DynamicAction");

            migrationBuilder.AlterColumn<string>(
                name: "HasTransfer",
                table: "DynamicAction",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "HasCreate",
                table: "DynamicAction",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDelete",
                table: "DynamicAction",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasReport",
                table: "DynamicAction",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasSaveAndCreate",
                table: "DynamicAction",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "DynamicAction",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasCreate",
                table: "DynamicAction");

            migrationBuilder.DropColumn(
                name: "HasDelete",
                table: "DynamicAction");

            migrationBuilder.DropColumn(
                name: "HasReport",
                table: "DynamicAction");

            migrationBuilder.DropColumn(
                name: "HasSaveAndCreate",
                table: "DynamicAction");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "DynamicAction");

            migrationBuilder.AlterColumn<bool>(
                name: "HasTransfer",
                table: "DynamicAction",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DynamicAction",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTopPosition",
                table: "DynamicAction",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
