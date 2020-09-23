using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regenerated_DynamicDatasource3503 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DynamicFieldId",
                table: "DynamicDatasource",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DynamicDatasource",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ObjectId",
                table: "DynamicDatasource",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "DynamicDatasource",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DynamicFieldId",
                table: "DynamicDatasource");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DynamicDatasource");

            migrationBuilder.DropColumn(
                name: "ObjectId",
                table: "DynamicDatasource");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "DynamicDatasource");
        }
    }
}
