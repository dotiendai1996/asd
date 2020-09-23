using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Added_Menu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CA_TypeHandes");

            migrationBuilder.DropIndex(
                name: "IX_CA_DocumentHandling_TenantId",
                table: "CA_DocumentHandling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menu",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "CA_TypeHandles");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "CA_TypeHandles");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Menu");

            migrationBuilder.RenameTable(
                name: "Menu",
                newName: "CA_Menus");

            migrationBuilder.RenameIndex(
                name: "IX_Menu_TenantId",
                table: "CA_Menus",
                newName: "IX_CA_Menus_TenantId");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CA_TypeHandles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CA_Menus",
                table: "CA_Menus",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CA_Menus",
                table: "CA_Menus");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CA_TypeHandles");

            migrationBuilder.RenameTable(
                name: "CA_Menus",
                newName: "Menu");

            migrationBuilder.RenameIndex(
                name: "IX_CA_Menus_TenantId",
                table: "Menu",
                newName: "IX_Menu_TenantId");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "CA_TypeHandles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "CA_TypeHandles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "Menu",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Menu",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "Menu",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menu",
                table: "Menu",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CA_TypeHandes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CA_TypeHandes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CA_DocumentHandling_TenantId",
                table: "CA_DocumentHandling",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_CA_TypeHandes_TenantId",
                table: "CA_TypeHandes",
                column: "TenantId");
        }
    }
}
