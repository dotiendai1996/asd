using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Update_Table_DataChamCong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeDuration",
                table: "DataChamCongs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProcessDate",
                table: "DataChamCongs",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaChamCong",
                table: "DataChamCongs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOut",
                table: "DataChamCongs",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckIn",
                table: "DataChamCongs",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<double>(
                name: "TimeOTDuration",
                table: "DataChamCongs",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TimeViolatingRuleDuration",
                table: "DataChamCongs",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TimeWorkDuration",
                table: "DataChamCongs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeOTDuration",
                table: "DataChamCongs");

            migrationBuilder.DropColumn(
                name: "TimeViolatingRuleDuration",
                table: "DataChamCongs");

            migrationBuilder.DropColumn(
                name: "TimeWorkDuration",
                table: "DataChamCongs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProcessDate",
                table: "DataChamCongs",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "MaChamCong",
                table: "DataChamCongs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOut",
                table: "DataChamCongs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckIn",
                table: "DataChamCongs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeDuration",
                table: "DataChamCongs",
                type: "datetime2",
                nullable: true);
        }
    }
}
