using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Update_Table_DataChamCong_TimeCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckIn",
                table: "DataChamCongs");

            migrationBuilder.DropColumn(
                name: "CheckOut",
                table: "DataChamCongs");

            migrationBuilder.AddColumn<string>(
                name: "CheckTime",
                table: "DataChamCongs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckTime",
                table: "DataChamCongs");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckIn",
                table: "DataChamCongs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOut",
                table: "DataChamCongs",
                type: "datetime2",
                nullable: true);
        }
    }
}
