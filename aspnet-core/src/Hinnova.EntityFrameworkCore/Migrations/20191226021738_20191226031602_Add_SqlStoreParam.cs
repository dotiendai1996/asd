using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class _20191226031602_Add_SqlStoreParam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceRecevie",
                table: "CA_Document");

            migrationBuilder.DropColumn(
                name: "TypeRecevie",
                table: "CA_Document");

            migrationBuilder.AddColumn<string>(
                name: "CellTemplate",
                table: "SqlConfigDetail",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KeyWord",
                table: "CA_MemorizeKeywords",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CA_MemorizeKeywords",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DocumentDetailId",
                table: "CA_DocumentHandling",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CA_DocumentDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PlaceReceive",
                table: "CA_Document",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeReceive",
                table: "CA_Document",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SqlStoreParam",
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
                    SqlConfigId = table.Column<int>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Format = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ValueString = table.Column<string>(nullable: true),
                    ValueInt = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlStoreParam", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CA_MemorizeKeywords_KeyWord",
                table: "CA_MemorizeKeywords",
                column: "KeyWord",
                unique: true,
                filter: "[KeyWord] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SqlStoreParam_TenantId",
                table: "SqlStoreParam",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SqlStoreParam");

            migrationBuilder.DropIndex(
                name: "IX_CA_MemorizeKeywords_KeyWord",
                table: "CA_MemorizeKeywords");

            migrationBuilder.DropColumn(
                name: "CellTemplate",
                table: "SqlConfigDetail");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CA_MemorizeKeywords");

            migrationBuilder.DropColumn(
                name: "DocumentDetailId",
                table: "CA_DocumentHandling");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CA_DocumentDetails");

            migrationBuilder.DropColumn(
                name: "PlaceReceive",
                table: "CA_Document");

            migrationBuilder.DropColumn(
                name: "TypeReceive",
                table: "CA_Document");

            migrationBuilder.AlterColumn<string>(
                name: "KeyWord",
                table: "CA_MemorizeKeywords",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceRecevie",
                table: "CA_Document",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeRecevie",
                table: "CA_Document",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
