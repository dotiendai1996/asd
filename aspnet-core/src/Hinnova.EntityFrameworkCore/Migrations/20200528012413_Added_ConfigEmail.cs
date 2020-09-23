using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Added_ConfigEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfigEmails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(nullable: true),
                    DiaChiEmail = table.Column<string>(nullable: true),
                    TenHienThi = table.Column<string>(nullable: true),
                    DiaChiIP = table.Column<string>(nullable: true),
                    CongSMTP = table.Column<int>(nullable: false),
                    CheckSSL = table.Column<bool>(nullable: false),
                    CheckThongTin = table.Column<string>(nullable: true),
                    TenMien = table.Column<string>(nullable: true),
                    TenTruyCap = table.Column<string>(nullable: true),
                    MatKhau = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigEmails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigEmails_TenantId",
                table: "ConfigEmails",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigEmails");
        }
    }
}
