using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Added_Memorize_Keywords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TextBooks_TenantId",
                table: "TextBooks");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "TextBooks");

            migrationBuilder.CreateTable(
                name: "Memorize_Keywordses",
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
                    TenGoiNho = table.Column<string>(nullable: true),
                    XuLyChinh = table.Column<string>(nullable: true),
                    DongXuLy = table.Column<string>(nullable: true),
                    DeBiet = table.Column<string>(nullable: true),
                    Head_ID = table.Column<int>(nullable: false),
                    Full_Name = table.Column<string>(nullable: true),
                    Prefix = table.Column<string>(nullable: true),
                    Hire_Date = table.Column<DateTime>(nullable: false),
                    KeyWord = table.Column<string>(nullable: true),
                    IsActive = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memorize_Keywordses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Memorize_Keywordses_TenantId",
                table: "Memorize_Keywordses",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Memorize_Keywordses");

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "TextBooks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TextBooks_TenantId",
                table: "TextBooks",
                column: "TenantId");
        }
    }
}
