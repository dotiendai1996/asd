using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regenerated_Documents7643 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Documentses",
                table: "Documentses");

            migrationBuilder.RenameTable(
                name: "Documentses",
                newName: "Documents");

            migrationBuilder.RenameIndex(
                name: "IX_Documentses_TenantId",
                table: "Documents",
                newName: "IX_Documents_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.RenameTable(
                name: "Documents",
                newName: "Documentses");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_TenantId",
                table: "Documentses",
                newName: "IX_Documentses_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documentses",
                table: "Documentses",
                column: "Id");
        }
    }
}
