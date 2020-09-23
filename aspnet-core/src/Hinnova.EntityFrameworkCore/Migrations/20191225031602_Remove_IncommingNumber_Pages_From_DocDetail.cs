using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Remove_IncommingNumber_Pages_From_DocDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InomingNumber",
                table: "CA_DocumentDetails");

            migrationBuilder.DropColumn(
                name: "Pages",
                table: "CA_DocumentDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InomingNumber",
                table: "CA_DocumentDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Pages",
                table: "CA_DocumentDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
