using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regenerated_ViTriCongViec2086 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_RoleMapper_TenantId",
            //    table: "RoleMapper");

            //migrationBuilder.DropIndex(
            //    name: "IX_KeywordDetail_TenantId",
            //    table: "KeywordDetail");

            //migrationBuilder.DropIndex(
            //    name: "IX_DynamicAction_TenantId",
            //    table: "DynamicAction");

            //migrationBuilder.DropIndex(
            //    name: "IX_CA_DocumentStatus_TenantId",
            //    table: "CA_DocumentStatus");

            //migrationBuilder.CreateTable(
            //    name: "ViTriCongViecs",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CreationTime = table.Column<DateTime>(nullable: false),
            //        CreatorUserId = table.Column<long>(nullable: true),
            //        LastModificationTime = table.Column<DateTime>(nullable: true),
            //        LastModifierUserId = table.Column<long>(nullable: true),
            //        IsDeleted = table.Column<bool>(nullable: false),
            //        DeleterUserId = table.Column<long>(nullable: true),
            //        DeletionTime = table.Column<DateTime>(nullable: true),
            //        TenDonVi = table.Column<string>(nullable: true),
            //        TenCongViec = table.Column<string>(nullable: true),
            //        GhiChu = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ViTriCongViecs", x => x.Id);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "ViTriCongViecs");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RoleMapper_TenantId",
            //    table: "RoleMapper",
            //    column: "TenantId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_KeywordDetail_TenantId",
            //    table: "KeywordDetail",
            //    column: "TenantId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_DynamicAction_TenantId",
            //    table: "DynamicAction",
            //    column: "TenantId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CA_DocumentStatus_TenantId",
            //    table: "CA_DocumentStatus",
            //    column: "TenantId");
        }
    }
}
