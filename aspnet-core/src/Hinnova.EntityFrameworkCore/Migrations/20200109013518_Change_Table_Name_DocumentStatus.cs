using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Change_Table_Name_DocumentStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentStatus",
                table: "DocumentStatus");

            migrationBuilder.RenameTable(
                name: "DocumentStatus",
                newName: "CA_DocumentStatus");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentStatus_TenantId",
                table: "CA_DocumentStatus",
                newName: "IX_CA_DocumentStatus_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CA_DocumentStatus",
                table: "CA_DocumentStatus",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CA_DocumentStatus",
                table: "CA_DocumentStatus");

            migrationBuilder.RenameTable(
                name: "CA_DocumentStatus",
                newName: "DocumentStatus");

            migrationBuilder.RenameIndex(
                name: "IX_CA_DocumentStatus_TenantId",
                table: "DocumentStatus",
                newName: "IX_DocumentStatus_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentStatus",
                table: "DocumentStatus",
                column: "Id");
        }
    }
}
