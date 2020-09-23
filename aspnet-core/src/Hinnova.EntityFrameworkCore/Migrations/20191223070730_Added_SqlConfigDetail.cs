using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Added_SqlConfigDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SqlConfigDetail",
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
                    SqlConfigId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Format = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Width = table.Column<string>(nullable: true),
                    ColNum = table.Column<int>(nullable: true),
                    GroupLevel = table.Column<int>(nullable: true),
                    IsDisplay = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: true),
                    TextAlign = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: true),
                    IsSum = table.Column<bool>(nullable: false),
                    IsFreePane = table.Column<bool>(nullable: false),
                    IsParent = table.Column<bool>(nullable: false),
                    ParentCode = table.Column<string>(nullable: true),
                    GroupSort = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlConfigDetail", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SqlConfigDetail_TenantId",
                table: "SqlConfigDetail",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SqlConfigDetail");
        }
    }
}
