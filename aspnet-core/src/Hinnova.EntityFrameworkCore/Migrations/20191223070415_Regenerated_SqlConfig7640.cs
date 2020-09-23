using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regenerated_SqlConfig7640 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "SqlConfig",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "SqlConfig",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "SqlConfig",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "SqlConfig",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SqlConfig",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "SqlConfig",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "SqlConfig",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "SqlConfig");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "SqlConfig");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "SqlConfig");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "SqlConfig");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SqlConfig");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "SqlConfig");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "SqlConfig");
        }
    }
}
