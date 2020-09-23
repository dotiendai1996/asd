using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regenerated_ConfigEmail7141 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ConfigEmails_TenantId",
                table: "ConfigEmails");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ConfigEmails");

            migrationBuilder.AlterColumn<bool>(
                name: "CheckThongTin",
                table: "ConfigEmails",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CheckThongTin",
                table: "ConfigEmails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ConfigEmails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConfigEmails_TenantId",
                table: "ConfigEmails",
                column: "TenantId");
        }
    }
}
