using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regenerated_Template7361 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Templates_TenantId",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Templates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Templates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "Templates",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Templates",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Templates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Templates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Templates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "Templates",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Templates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Templates_TenantId",
                table: "Templates",
                column: "TenantId");
        }
    }
}
