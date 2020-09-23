using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regenerated_Vanban7600 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VanBans",
                table: "VanBans");

            migrationBuilder.RenameTable(
                name: "VanBans",
                newName: "vanbans");

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "vanbans",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_vanbans",
                table: "vanbans",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_vanbans_TenantId",
                table: "vanbans",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_vanbans",
                table: "vanbans");

            migrationBuilder.DropIndex(
                name: "IX_vanbans_TenantId",
                table: "vanbans");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "vanbans");

            migrationBuilder.RenameTable(
                name: "vanbans",
                newName: "VanBans");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VanBans",
                table: "VanBans",
                column: "Id");
        }
    }
}
