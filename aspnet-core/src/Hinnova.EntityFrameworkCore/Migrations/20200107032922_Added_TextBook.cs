using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Added_TextBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TextBooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(nullable: true),
                    SoDen = table.Column<int>(nullable: false),
                    NgayDen = table.Column<DateTime>(nullable: false),
                    SoHieuGoc = table.Column<string>(nullable: true),
                    CoQuanBanHanh = table.Column<string>(nullable: true),
                    TrichYeu = table.Column<string>(nullable: true),
                    NguoiChiDao = table.Column<string>(nullable: true),
                    Nguoi_DonVi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextBooks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TextBooks_TenantId",
                table: "TextBooks",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TextBooks");
        }
    }
}
