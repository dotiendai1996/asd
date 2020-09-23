using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Added_HoSo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HoSo",
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
                    MaNhanVien = table.Column<string>(nullable: true),
                    HoVaTen = table.Column<string>(nullable: true),
                    AnhDaiDien = table.Column<string>(nullable: true),
                    GioiTinhCode = table.Column<string>(nullable: true),
                    NgaySinh = table.Column<DateTime>(nullable: true),
                    MSTCaNhan = table.Column<string>(nullable: true),
                    DonViCongTacID = table.Column<int>(nullable: true),
                    ViTriCongViecCode = table.Column<string>(nullable: true),
                    DanToc = table.Column<string>(nullable: true),
                    TonGiao = table.Column<string>(nullable: true),
                    QuocTich = table.Column<string>(nullable: true),
                    SoCMND = table.Column<string>(nullable: true),
                    NgayCap = table.Column<DateTime>(nullable: true),
                    NoiCap = table.Column<string>(nullable: true),
                    NgayHetHan = table.Column<DateTime>(nullable: true),
                    TrinhDoVanHoa = table.Column<string>(nullable: true),
                    TrinhDoDaoTaoCode = table.Column<string>(nullable: true),
                    NoiDaoTaoCode = table.Column<string>(nullable: true),
                    Khoa = table.Column<string>(nullable: true),
                    ChuyenNganh = table.Column<string>(nullable: true),
                    NamTotNghiep = table.Column<int>(nullable: true),
                    XepLoaiCode = table.Column<string>(nullable: true),
                    TinhTrangHonNhanCode = table.Column<string>(nullable: true),
                    TepDinhKem = table.Column<string>(nullable: true),
                    DtDiDong = table.Column<string>(nullable: true),
                    DtCoQuan = table.Column<string>(nullable: true),
                    DtNhaRieng = table.Column<string>(nullable: true),
                    DtKhac = table.Column<string>(nullable: true),
                    EmailCaNhan = table.Column<string>(nullable: true),
                    EmailCoQuan = table.Column<string>(nullable: true),
                    EmailKhac = table.Column<string>(nullable: true),
                    NguyenQuan = table.Column<string>(nullable: true),
                    TinhThanhID = table.Column<int>(nullable: true),
                    NoiSinh = table.Column<string>(nullable: true),
                    Skype = table.Column<string>(nullable: true),
                    Facebook = table.Column<string>(nullable: true),
                    QuocGiaHKTT = table.Column<string>(nullable: true),
                    TinhThanhIDHKTT = table.Column<int>(nullable: true),
                    DiaChiHKTT = table.Column<string>(nullable: true),
                    SoSoHoKhau = table.Column<string>(nullable: true),
                    MaSoHoGiaDinh = table.Column<string>(nullable: true),
                    LaChuHo = table.Column<bool>(nullable: false),
                    QuocGiaHN = table.Column<string>(nullable: true),
                    TinhThanhIDHN = table.Column<int>(nullable: true),
                    DiaChiHN = table.Column<string>(nullable: true),
                    HoVaTenLHKC = table.Column<string>(nullable: true),
                    QuanHeLHKC = table.Column<string>(nullable: true),
                    DtDiDongLHKC = table.Column<string>(nullable: true),
                    DtNhaRiengLHKC = table.Column<string>(nullable: true),
                    EmailLHKC = table.Column<string>(nullable: true),
                    DiaChiLHKC = table.Column<string>(nullable: true),
                    MaChamCong = table.Column<string>(nullable: true),
                    ChucDanh = table.Column<string>(nullable: true),
                    Cap = table.Column<string>(nullable: true),
                    Bac = table.Column<string>(nullable: true),
                    TrangThaiLamViecCode = table.Column<string>(nullable: true),
                    QuanLyTrucTiep = table.Column<string>(nullable: true),
                    QuanLyGianTiep = table.Column<string>(nullable: true),
                    DiaDiemLamViecCode = table.Column<string>(nullable: true),
                    SoSoQLLaoDong = table.Column<string>(nullable: true),
                    LoaiHopDongCode = table.Column<string>(nullable: true),
                    NgayTapSu = table.Column<DateTime>(nullable: true),
                    NgayThuViec = table.Column<DateTime>(nullable: true),
                    NgayChinhThuc = table.Column<DateTime>(nullable: true),
                    SoNgayPhep = table.Column<double>(nullable: true),
                    BacLuongCode = table.Column<string>(nullable: true),
                    LuongCoBan = table.Column<double>(nullable: true),
                    LuongDongBH = table.Column<double>(nullable: true),
                    SoCongChuan = table.Column<double>(nullable: true),
                    DonViSoCongChuanCode = table.Column<string>(nullable: true),
                    TkNganHang = table.Column<string>(nullable: true),
                    NganHangCode = table.Column<string>(nullable: true),
                    ThamGiaCongDoan = table.Column<bool>(nullable: false),
                    NgayThamGiaBH = table.Column<DateTime>(nullable: true),
                    TyLeDongBH = table.Column<double>(nullable: true),
                    SoSoBHXH = table.Column<string>(nullable: true),
                    MaSoBHXH = table.Column<string>(nullable: true),
                    MaTinhCap = table.Column<string>(nullable: true),
                    SoTheBHYT = table.Column<string>(nullable: true),
                    NgayHetHanBHYT = table.Column<DateTime>(nullable: true),
                    NoiDangKyKCBID = table.Column<int>(nullable: true),
                    MaSoNoiKCB = table.Column<string>(nullable: true),
                    AUTH_STATUS = table.Column<string>(nullable: true),
                    RECORD_STATUS = table.Column<string>(nullable: true),
                    MARKER_ID = table.Column<int>(nullable: true),
                    CHECKER_ID = table.Column<int>(nullable: true),
                    APPROVE_DT = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoSo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoSo");
        }
    }
}
