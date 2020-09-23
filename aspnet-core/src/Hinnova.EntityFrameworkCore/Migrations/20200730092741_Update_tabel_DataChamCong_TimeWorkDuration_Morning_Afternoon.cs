using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Update_tabel_DataChamCong_TimeWorkDuration_Morning_Afternoon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeWorkDuration",
                table: "DataChamCongs");

            migrationBuilder.AddColumn<double>(
                name: "TimeWorkAfternoonDuration",
                table: "DataChamCongs",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TimeWorkMorningDuration",
                table: "DataChamCongs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeWorkAfternoonDuration",
                table: "DataChamCongs");

            migrationBuilder.DropColumn(
                name: "TimeWorkMorningDuration",
                table: "DataChamCongs");

            migrationBuilder.AddColumn<double>(
                name: "TimeWorkDuration",
                table: "DataChamCongs",
                type: "float",
                nullable: true);
        }
    }
}
