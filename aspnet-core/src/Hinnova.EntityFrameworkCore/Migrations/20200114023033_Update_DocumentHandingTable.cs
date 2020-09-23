using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Update_DocumentHandingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoHandling",
                table: "CA_DocumentHandling");

            migrationBuilder.DropColumn(
                name: "MainHandling",
                table: "CA_DocumentHandling");

            migrationBuilder.DropColumn(
                name: "ToKnow",
                table: "CA_DocumentHandling");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CoHandling",
                table: "CA_DocumentHandling",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MainHandling",
                table: "CA_DocumentHandling",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ToKnow",
                table: "CA_DocumentHandling",
                type: "bit",
                nullable: true);
        }
    }
}
