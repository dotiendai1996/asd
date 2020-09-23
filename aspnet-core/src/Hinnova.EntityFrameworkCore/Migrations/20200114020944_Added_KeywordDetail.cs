using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Added_KeywordDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HistoryUploads_TenantId",
                table: "HistoryUploads");

            migrationBuilder.DropColumn(
                name: "KeywordId",
                table: "CA_DocumentHandling");

            migrationBuilder.AddColumn<bool>(
                name: "CoHandling",
                table: "CA_DocumentHandling",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MainHandling",
                table: "CA_DocumentHandling",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ToKnow",
                table: "CA_DocumentHandling",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KeywordDetail",
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
                    KeywordId = table.Column<int>(nullable: false),
                    IsLeader = table.Column<bool>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    MainHandling = table.Column<bool>(nullable: false),
                    CoHandling = table.Column<bool>(nullable: false),
                    ToKnow = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordDetail", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeywordDetail_TenantId",
                table: "KeywordDetail",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeywordDetail");

            migrationBuilder.DropColumn(
                name: "CoHandling",
                table: "CA_DocumentHandling");

            migrationBuilder.DropColumn(
                name: "MainHandling",
                table: "CA_DocumentHandling");

            migrationBuilder.DropColumn(
                name: "ToKnow",
                table: "CA_DocumentHandling");

            migrationBuilder.AddColumn<int>(
                name: "KeywordId",
                table: "CA_DocumentHandling",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryUploads_TenantId",
                table: "HistoryUploads",
                column: "TenantId");
        }
    }
}
