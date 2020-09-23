using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regenerated_Promulgated7468 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_promulgateds",
                table: "promulgateds");

            migrationBuilder.RenameTable(
                name: "promulgateds",
                newName: "Promulgateds");

            migrationBuilder.RenameIndex(
                name: "IX_promulgateds_TenantId",
                table: "Promulgateds",
                newName: "IX_Promulgateds_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Promulgateds",
                table: "Promulgateds",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Promulgateds",
                table: "Promulgateds");

            migrationBuilder.RenameTable(
                name: "Promulgateds",
                newName: "promulgateds");

            migrationBuilder.RenameIndex(
                name: "IX_Promulgateds_TenantId",
                table: "promulgateds",
                newName: "IX_promulgateds_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_promulgateds",
                table: "promulgateds",
                column: "Id");
        }
    }
}
