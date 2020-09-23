using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class ChangeTableName_DocumentHandlingDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentHandlingDetails",
                table: "DocumentHandlingDetails");

            migrationBuilder.RenameTable(
                name: "DocumentHandlingDetails",
                newName: "CA_DocumentHandlingDetail");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentHandlingDetails_TenantId",
                table: "CA_DocumentHandlingDetail",
                newName: "IX_CA_DocumentHandlingDetail_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CA_DocumentHandlingDetail",
                table: "CA_DocumentHandlingDetail",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CA_DocumentHandlingDetail",
                table: "CA_DocumentHandlingDetail");

            migrationBuilder.RenameTable(
                name: "CA_DocumentHandlingDetail",
                newName: "DocumentHandlingDetails");

            migrationBuilder.RenameIndex(
                name: "IX_CA_DocumentHandlingDetail_TenantId",
                table: "DocumentHandlingDetails",
                newName: "IX_DocumentHandlingDetails_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentHandlingDetails",
                table: "DocumentHandlingDetails",
                column: "Id");
        }
    }
}
