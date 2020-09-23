using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Added_HopDong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HopDong",
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
                    NhanVienId = table.Column<int>(nullable: true),
                    HoTenNhanVien = table.Column<string>(nullable: true),
                    ViTriCongViecCode = table.Column<string>(nullable: true),
                    SoHopDong = table.Column<string>(nullable: true),
                    NgayKy = table.Column<DateTime>(nullable: true),
                    DonViCongTacID = table.Column<int>(nullable: true),
                    TenHopDong = table.Column<string>(nullable: true),
                    LoaiHopDongCode = table.Column<string>(nullable: true),
                    HinhThucLamViecCode = table.Column<string>(nullable: true),
                    NgayCoHieuLuc = table.Column<DateTime>(nullable: true),
                    NgayHetHan = table.Column<DateTime>(nullable: true),
                    LuongCoBan = table.Column<double>(nullable: true),
                    LuongDongBaoHiem = table.Column<double>(nullable: true),
                    TyLeHuongLuong = table.Column<double>(nullable: true),
                    NguoiDaiDienCongTy = table.Column<string>(nullable: true),
                    ChucDanh = table.Column<string>(nullable: true),
                    TrichYeu = table.Column<string>(nullable: true),
                    TepDinhKem = table.Column<string>(nullable: true),
                    GhiChu = table.Column<string>(nullable: true),
                    RECORD_STATUS = table.Column<string>(nullable: true),
                    MARKER_ID = table.Column<int>(nullable: true),
                    AUTH_STATUS = table.Column<string>(nullable: true),
                    CHECKER_ID = table.Column<int>(nullable: true),
                    APPROVE_DT = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HopDong", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HopDong");
        }
    }
}
