using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Added_QuanLyNghiPhep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuanLyNghiPheps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    TenNhanVien = table.Column<string>(nullable: true),
                    PhongBan = table.Column<int>(nullable: false),
                    MaNV = table.Column<string>(nullable: true),
                    NghiPhep = table.Column<bool>(nullable: false),
                    CongTac = table.Column<string>(nullable: true),
                    NgayBatDau = table.Column<DateTime>(nullable: false),
                    NgayKetThuc = table.Column<DateTime>(nullable: false),
                    LyDo = table.Column<string>(nullable: true),
                    QuanLyTrucTiep = table.Column<string>(nullable: true),
                    TruongBoPhan = table.Column<string>(nullable: true),
                    TepDinhKem = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuanLyNghiPheps", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuanLyNghiPheps");
        }
    }
}
