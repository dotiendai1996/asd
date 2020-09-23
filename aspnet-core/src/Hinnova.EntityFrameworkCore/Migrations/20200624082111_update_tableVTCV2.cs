using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class update_tableVTCV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "ViTriCongViecs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "ViTriCongViecs",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        AnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Bac = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BacLuongCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Cap = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ChiNhanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ChoNgoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ChucDanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ChuyenNganh = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
            //        DVT = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DanToc = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DiaChiHKTT = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DiaChiHN = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DiaChiLHKC = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DiaDiemLamViecCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DonViCongTacID = table.Column<int>(type: "int", nullable: true),
            //        DonViCongTacName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DonViSoCongChuanCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DtCoQuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DtDiDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DtDiDongLHKC = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DtKhac = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DtNhaRieng = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DtNhaRiengLHKC = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        EmailCaNhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        EmailCoQuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        EmailKhac = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        EmailLHKC = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Facebook = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        GioiTinhCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        HoVaTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        HoVaTenLHKC = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        HopDongHienTai = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Khoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LaChuHo = table.Column<bool>(type: "bit", nullable: false),
            //        LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
            //        LoaiHopDongID = table.Column<int>(type: "int", nullable: false),
            //        LuongCoBan = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LuongDongBH = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MSTCaNhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MaChamCong = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MaNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MaSoBHXH = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MaSoHoGiaDinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MaSoNoiKCB = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MaTinhCap = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NamTotNghiep = table.Column<int>(type: "int", nullable: false),
            //        NganHangCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NgayCap = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        NgayChinhThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        NgayHetHan = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        NgayHetHanBHYT = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        NgayKYHDCTV = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        NgayKYHDTT = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        NgayKyHD = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        NgayKyHD12TH = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        NgayKyHD36TH = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        NgayKyHDKTH = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        NgayKyHDKV = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        NgayKyHDTV = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        NgayTapSu = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        NgayThamGiaBH = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        NgayThuViec = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        NguyenQuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NoiCap = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NoiDangKyKCBID = table.Column<int>(type: "int", nullable: false),
            //        NoiDaoTaoID = table.Column<int>(type: "int", nullable: false),
            //        NoiSinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        QuanHeLHKC = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        QuanLyGianTiep = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        QuanLyTrucTiep = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        QuocGiaHKTT = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        QuocGiaHN = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        QuocTich = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Skype = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SoCMND = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SoCongChuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SoHD = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SoNgayPhep = table.Column<double>(type: "float", nullable: false),
            //        SoSoBHXH = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SoSoHoKhau = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SoSoQLLaoDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SoTheBHYT = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        TenCty = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        TepDinhKem = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ThamGiaCongDoan = table.Column<bool>(type: "bit", nullable: false),
            //        TinhThanhID = table.Column<int>(type: "int", nullable: true),
            //        TinhThanhIDHKTT = table.Column<int>(type: "int", nullable: true),
            //        TinhThanhIDHN = table.Column<int>(type: "int", nullable: true),
            //        TinhTrangHonNhanCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        TkNganHang = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        TonGiao = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        TrangThaiLamViecCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        TrinhDoDaoTaoCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        TrinhDoVanHoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        TyLeDongBH = table.Column<double>(type: "float", nullable: false),
            //        ViTriCongViecCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        XepLoaiCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ViTriCongViecs", x => x.Id);
            //    });
        }
    }
}
