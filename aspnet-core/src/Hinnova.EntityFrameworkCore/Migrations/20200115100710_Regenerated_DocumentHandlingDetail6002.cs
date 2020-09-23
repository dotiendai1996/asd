using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regenerated_DocumentHandlingDetail6002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CA_DocumentHandlingDetails",
                table: "CA_DocumentHandlingDetails");

            migrationBuilder.DropColumn(
                name: "CoHandling",
                table: "CA_DocumentHandlingDetails");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CA_DocumentHandlingDetails");

            migrationBuilder.DropColumn(
                name: "MainHandling",
                table: "CA_DocumentHandlingDetails");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CA_DocumentHandlingDetails");

            migrationBuilder.DropColumn(
                name: "Superior",
                table: "CA_DocumentHandlingDetails");

            migrationBuilder.DropColumn(
                name: "ToKnow",
                table: "CA_DocumentHandlingDetails");

            migrationBuilder.RenameTable(
                name: "CA_DocumentHandlingDetails",
                newName: "DocumentHandlingDetails");

            migrationBuilder.RenameIndex(
                name: "IX_CA_DocumentHandlingDetails_TenantId",
                table: "DocumentHandlingDetails",
                newName: "IX_DocumentHandlingDetails_TenantId");

            migrationBuilder.AddColumn<int>(
                name: "DocumentHandlingId",
                table: "DocumentHandlingDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Superios",
                table: "DocumentHandlingDetails",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentHandlingDetails",
                table: "DocumentHandlingDetails",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentHandlingDetails",
                table: "DocumentHandlingDetails");

            migrationBuilder.DropColumn(
                name: "DocumentHandlingId",
                table: "DocumentHandlingDetails");

            migrationBuilder.DropColumn(
                name: "Superios",
                table: "DocumentHandlingDetails");

            migrationBuilder.RenameTable(
                name: "DocumentHandlingDetails",
                newName: "CA_DocumentHandlingDetails");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentHandlingDetails_TenantId",
                table: "CA_DocumentHandlingDetails",
                newName: "IX_CA_DocumentHandlingDetails_TenantId");

            migrationBuilder.AddColumn<bool>(
                name: "CoHandling",
                table: "CA_DocumentHandlingDetails",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CA_DocumentHandlingDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MainHandling",
                table: "CA_DocumentHandlingDetails",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CA_DocumentHandlingDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Superior",
                table: "CA_DocumentHandlingDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ToKnow",
                table: "CA_DocumentHandlingDetails",
                type: "bit",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CA_DocumentHandlingDetails",
                table: "CA_DocumentHandlingDetails",
                column: "Id");
        }
    }
}
