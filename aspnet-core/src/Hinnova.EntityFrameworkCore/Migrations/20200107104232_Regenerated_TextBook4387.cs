using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regenerated_TextBook4387 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "TextBooks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "TextBooks",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "TextBooks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "TextBooks",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TextBooks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "TextBooks",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "TextBooks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "TextBooks");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "TextBooks");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "TextBooks");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "TextBooks");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TextBooks");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "TextBooks");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "TextBooks");
        }
    }
}
