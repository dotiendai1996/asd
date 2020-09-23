using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Table_DataChamCong_Field_DiTre_VeSom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeViolatingRuleDuration",
                table: "DataChamCongs");

            migrationBuilder.AddColumn<double>(
                name: "TimeViolatingRuleFirstDuration",
                table: "DataChamCongs",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TimeViolatingRuleLastDuration",
                table: "DataChamCongs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeViolatingRuleFirstDuration",
                table: "DataChamCongs");

            migrationBuilder.DropColumn(
                name: "TimeViolatingRuleLastDuration",
                table: "DataChamCongs");

            migrationBuilder.AddColumn<double>(
                name: "TimeViolatingRuleDuration",
                table: "DataChamCongs",
                type: "float",
                nullable: true);
        }
    }
}
