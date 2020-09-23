using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regenerated_HistoryUpload1810 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "documentID",
                table: "HistoryUploads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryUploads_TenantId",
                table: "HistoryUploads",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HistoryUploads_TenantId",
                table: "HistoryUploads");

            migrationBuilder.DropColumn(
                name: "documentID",
                table: "HistoryUploads");
        }
    }
}
