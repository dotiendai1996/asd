using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Added_UngVien : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UngVien",
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
                    MaUngVien = table.Column<string>(nullable: true),
                    HoVaTen = table.Column<string>(nullable: true),
                    ViTriUngTuyenCode = table.Column<string>(nullable: true),
                    KenhTuyenDungCode = table.Column<string>(nullable: true),
                    GioiTinhCode = table.Column<string>(nullable: true),
                    NgaySinh = table.Column<DateTime>(nullable: true),
                    SoCMND = table.Column<string>(nullable: true),
                    NgayCap = table.Column<DateTime>(nullable: true),
                    NoiCap = table.Column<string>(nullable: true),
                    TinhThanhID = table.Column<int>(nullable: true),
                    TinhTrangHonNhanCode = table.Column<string>(nullable: true),
                    TrinhDoDaoTaoCode = table.Column<string>(nullable: true),
                    TrinhDoVanHoa = table.Column<string>(nullable: true),
                    NoiDaoTaoID = table.Column<int>(nullable: true),
                    Khoa = table.Column<string>(nullable: true),
                    ChuyenNganh = table.Column<string>(nullable: true),
                    XepLoaiCode = table.Column<string>(nullable: true),
                    NamTotNghiep = table.Column<int>(nullable: true),
                    TrangThaiCode = table.Column<string>(nullable: true),
                    TienDoTuyenDungCode = table.Column<string>(nullable: true),
                    TepDinhKem = table.Column<string>(nullable: true),
                    RECORD_STATUS = table.Column<string>(nullable: true),
                    MARKER_ID = table.Column<int>(nullable: true),
                    AUTH_STATUS = table.Column<string>(nullable: true),
                    CHECKER_ID = table.Column<int>(nullable: true),
                    APPROVE_DT = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UngVien", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UngVien");
        }
    }
}
