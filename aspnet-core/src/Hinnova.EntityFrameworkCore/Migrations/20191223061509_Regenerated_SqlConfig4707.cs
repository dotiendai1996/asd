using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regenerated_SqlConfig4707 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CA_TypeHandes");

            migrationBuilder.DropIndex(
                name: "IX_CA_DocumentHandling_TenantId",
                table: "CA_DocumentHandling");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "CA_TypeHandles");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "CA_TypeHandles");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CA_TypeHandles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SqlConfig",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsRawQuery = table.Column<bool>(nullable: false),
                    SqlContent = table.Column<string>(nullable: true),
                    GroupLevel = table.Column<int>(nullable: true),
                    DisplayType = table.Column<int>(nullable: true),
                    Version = table.Column<int>(nullable: true),
                    IsDynamicColumn = table.Column<bool>(nullable: false),
                    TypeGetColumn = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlConfig", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SqlConfig_TenantId",
                table: "SqlConfig",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SqlConfig");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CA_TypeHandles");

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
