using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regenerated_Vanban3487 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_vanbans_TenantId",
                table: "vanbans");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "vanbans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "vanbans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_vanbans_TenantId",
                table: "vanbans",
                column: "TenantId");
        }
    }
}
