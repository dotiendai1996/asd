using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLNS.Exporting
{
    public class ViTriCongViecsExcelExporter : EpPlusExcelExporterBase, IViTriCongViecsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ViTriCongViecsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetViTriCongViecForViewDto> viTriCongViecs)
        {
            return CreateExcelPackage(
                "ViTriCongViecs.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ViTriCongViecs"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("TenCty"),
                        L("HopDongHienTai"),
                        L("SoHD"),
                        L("DonViCongTacName"),
                        L("ChoNgoi"),
                        L("NoiDaoTaoID"),
                        L("LoaiHopDongID"),
                        L("MaSoNoiKCB"),
                        L("NoiDangKyKCBID"),
                        L("NgayHetHanBHYT"),
                        L("SoTheBHYT"),
                        L("MaTinhCap"),
                        L("MaSoBHXH"),
                        L("SoSoBHXH"),
                        L("TyLeDongBH"),
                        L("NgayThamGiaBH"),
                        L("ThamGiaCongDoan"),
                        L("NganHangCode"),
                        L("TkNganHang"),
                        L("DonViSoCongChuanCode"),
                        L("SoCongChuan"),
                        L("LuongDongBH"),
                        L("LuongCoBan"),
                        L("BacLuongCode"),
                        L("SoNgayPhep"),
                        L("NgayChinhThuc"),
                        L("NgayThuViec"),
                        L("NgayTapSu"),
                        L("SoSoQLLaoDong"),
                        L("DiaDiemLamViecCode"),
                        L("QuanLyGianTiep"),
                        L("QuanLyTrucTiep"),
                        L("TrangThaiLamViecCode"),
                        L("Bac"),
                        L("Cap"),
                        L("ChucDanh"),
                        L("MaChamCong"),
                        L("DiaChiLHKC"),
                        L("EmailLHKC"),
                        L("DtDiDongLHKC"),
                        L("DtNhaRiengLHKC"),
                        L("QuanHeLHKC"),
                        L("HoVaTenLHKC"),
                        L("DiaChiHN"),
                        L("TinhThanhIDHN"),
                        L("QuocGiaHN"),
                        L("LaChuHo"),
                        L("MaSoHoGiaDinh"),
                        L("SoSoHoKhau"),
                        L("DiaChiHKTT"),
                        L("TinhThanhIDHKTT"),
                        L("QuocGiaHKTT"),
                        L("Facebook"),
                        L("Skype"),
                        L("NoiSinh"),
                        L("TinhThanhID"),
                        L("NguyenQuan"),
                        L("EmailKhac"),
                        L("EmailCoQuan"),
                        L("EmailCaNhan"),
                        L("DtKhac"),
                        L("DtNhaRieng"),
                        L("DtCoQuan"),
                        L("DtDiDong"),
                        L("TepDinhKem"),
                        L("TinhTrangHonNhanCode"),
                        L("XepLoaiCode"),
                        L("NamTotNghiep"),
                        L("ChuyenNganh"),
                        L("Khoa"),
                        L("TrinhDoDaoTaoCode"),
                        L("TrinhDoVanHoa"),
                        L("NgayHetHan"),
                        L("NoiCap"),
                        L("NgayCap"),
                        L("SoCMND"),
                        L("QuocTich"),
                        L("TonGiao"),
                        L("DanToc"),
                        L("ViTriCongViecCode"),
                        L("DonViCongTacID"),
                        L("MSTCaNhan"),
                        L("NgaySinh"),
                        L("GioiTinhCode"),
                        L("AnhDaiDien"),
                        L("HoVaTen"),
                        L("MaNhanVien"),
                        L("ChiNhanh"),
                        L("DVT"),
                        L("NgayKyHDKTH"),
                        L("NgayKyHD36TH"),
                        L("NgayKyHD12TH"),
                        L("NgayKyHDTV"),
                        L("NgayKYHDCTV"),
                        L("NgayKyHDKV"),
                        L("NgayKYHDTT"),
                        L("NgayKyHD")
                        );

                    AddObjects(
                        sheet, 2, viTriCongViecs,
                        _ => _.ViTriCongViec.TenCty,
                        _ => _.ViTriCongViec.HopDongHienTai,
                        _ => _.ViTriCongViec.SoHD,
                        _ => _.ViTriCongViec.DonViCongTacName,
                        _ => _.ViTriCongViec.ChoNgoi,
                        _ => _.ViTriCongViec.NoiDaoTaoID,
                        _ => _.ViTriCongViec.LoaiHopDongID,
                        _ => _.ViTriCongViec.MaSoNoiKCB,
                        _ => _.ViTriCongViec.NoiDangKyKCBID,
                        _ => _timeZoneConverter.Convert(_.ViTriCongViec.NgayHetHanBHYT, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.ViTriCongViec.SoTheBHYT,
                        _ => _.ViTriCongViec.MaTinhCap,
                        _ => _.ViTriCongViec.MaSoBHXH,
                        _ => _.ViTriCongViec.SoSoBHXH,
                        _ => _.ViTriCongViec.TyLeDongBH,
                        _ => _timeZoneConverter.Convert(_.ViTriCongViec.NgayThamGiaBH, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.ViTriCongViec.ThamGiaCongDoan,
                        _ => _.ViTriCongViec.NganHangCode,
                        _ => _.ViTriCongViec.TkNganHang,
                        _ => _.ViTriCongViec.DonViSoCongChuanCode,
                        _ => _.ViTriCongViec.SoCongChuan,
                        _ => _.ViTriCongViec.LuongDongBH,
                        _ => _.ViTriCongViec.LuongCoBan,
                        _ => _.ViTriCongViec.BacLuongCode,
                        _ => _.ViTriCongViec.SoNgayPhep,
                        _ => _timeZoneConverter.Convert(_.ViTriCongViec.NgayChinhThuc, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.ViTriCongViec.NgayThuViec, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.ViTriCongViec.NgayTapSu, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.ViTriCongViec.SoSoQLLaoDong,
                        _ => _.ViTriCongViec.DiaDiemLamViecCode,
                        _ => _.ViTriCongViec.QuanLyGianTiep,
                        _ => _.ViTriCongViec.QuanLyTrucTiep,
                        _ => _.ViTriCongViec.TrangThaiLamViecCode,
                        _ => _.ViTriCongViec.Bac,
                        _ => _.ViTriCongViec.Cap,
                        _ => _.ViTriCongViec.ChucDanh,
                        _ => _.ViTriCongViec.MaChamCong,
                        _ => _.ViTriCongViec.DiaChiLHKC,
                        _ => _.ViTriCongViec.EmailLHKC,
                        _ => _.ViTriCongViec.DtDiDongLHKC,
                        _ => _.ViTriCongViec.DtNhaRiengLHKC,
                        _ => _.ViTriCongViec.QuanHeLHKC,
                        _ => _.ViTriCongViec.HoVaTenLHKC,
                        _ => _.ViTriCongViec.DiaChiHN,
                        _ => _.ViTriCongViec.TinhThanhIDHN,
                        _ => _.ViTriCongViec.QuocGiaHN,
                        _ => _.ViTriCongViec.LaChuHo,
                        _ => _.ViTriCongViec.MaSoHoGiaDinh,
                        _ => _.ViTriCongViec.SoSoHoKhau,
                        _ => _.ViTriCongViec.DiaChiHKTT,
                        _ => _.ViTriCongViec.TinhThanhIDHKTT,
                        _ => _.ViTriCongViec.QuocGiaHKTT,
                        _ => _.ViTriCongViec.Facebook,
                        _ => _.ViTriCongViec.Skype,
                        _ => _.ViTriCongViec.NoiSinh,
                        _ => _.ViTriCongViec.TinhThanhID,
                        _ => _.ViTriCongViec.NguyenQuan,
                        _ => _.ViTriCongViec.EmailKhac,
                        _ => _.ViTriCongViec.EmailCoQuan,
                        _ => _.ViTriCongViec.EmailCaNhan,
                        _ => _.ViTriCongViec.DtKhac,
                        _ => _.ViTriCongViec.DtNhaRieng,
                        _ => _.ViTriCongViec.DtCoQuan,
                        _ => _.ViTriCongViec.DtDiDong,
                        _ => _.ViTriCongViec.TepDinhKem,
                        _ => _.ViTriCongViec.TinhTrangHonNhanCode,
                        _ => _.ViTriCongViec.XepLoaiCode,
                        _ => _.ViTriCongViec.NamTotNghiep,
                        _ => _.ViTriCongViec.ChuyenNganh,
                        _ => _.ViTriCongViec.Khoa,
                        _ => _.ViTriCongViec.TrinhDoDaoTaoCode,
                        _ => _.ViTriCongViec.TrinhDoVanHoa,
                        _ => _timeZoneConverter.Convert(_.ViTriCongViec.NgayHetHan, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.ViTriCongViec.NoiCap,
                        _ => _timeZoneConverter.Convert(_.ViTriCongViec.NgayCap, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.ViTriCongViec.SoCMND,
                        _ => _.ViTriCongViec.QuocTich,
                        _ => _.ViTriCongViec.TonGiao,
                        _ => _.ViTriCongViec.DanToc,
                        _ => _.ViTriCongViec.ViTriCongViecCode,
                        _ => _.ViTriCongViec.DonViCongTacID,
                        _ => _.ViTriCongViec.MSTCaNhan,
                        _ => _timeZoneConverter.Convert(_.ViTriCongViec.NgaySinh, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.ViTriCongViec.GioiTinhCode,
                        _ => _.ViTriCongViec.AnhDaiDien,
                        _ => _.ViTriCongViec.HoVaTen,
                        _ => _.ViTriCongViec.MaNhanVien,
                        _ => _.ViTriCongViec.ChiNhanh,
                        _ => _.ViTriCongViec.DVT,
                        _ => _timeZoneConverter.Convert(_.ViTriCongViec.NgayKyHDKTH, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.ViTriCongViec.NgayKyHD36TH, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.ViTriCongViec.NgayKyHD12TH, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.ViTriCongViec.NgayKyHDTV, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.ViTriCongViec.NgayKYHDCTV, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.ViTriCongViec.NgayKyHDKV, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.ViTriCongViec.NgayKYHDTT, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.ViTriCongViec.NgayKyHD, _abpSession.TenantId, _abpSession.GetUserId())
                        );

					var ngayHetHanBHYTColumn = sheet.Column(10);
                    ngayHetHanBHYTColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayHetHanBHYTColumn.AutoFit();
					var ngayThamGiaBHColumn = sheet.Column(16);
                    ngayThamGiaBHColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayThamGiaBHColumn.AutoFit();
					var ngayChinhThucColumn = sheet.Column(26);
                    ngayChinhThucColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayChinhThucColumn.AutoFit();
					var ngayThuViecColumn = sheet.Column(27);
                    ngayThuViecColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayThuViecColumn.AutoFit();
					var ngayTapSuColumn = sheet.Column(28);
                    ngayTapSuColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayTapSuColumn.AutoFit();
					var ngayHetHanColumn = sheet.Column(73);
                    ngayHetHanColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayHetHanColumn.AutoFit();
					var ngayCapColumn = sheet.Column(75);
                    ngayCapColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayCapColumn.AutoFit();
					var ngaySinhColumn = sheet.Column(83);
                    ngaySinhColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngaySinhColumn.AutoFit();
					var ngayKyHDKTHColumn = sheet.Column(90);
                    ngayKyHDKTHColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayKyHDKTHColumn.AutoFit();
					var ngayKyHD36THColumn = sheet.Column(91);
                    ngayKyHD36THColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayKyHD36THColumn.AutoFit();
					var ngayKyHD12THColumn = sheet.Column(92);
                    ngayKyHD12THColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayKyHD12THColumn.AutoFit();
					var ngayKyHDTVColumn = sheet.Column(93);
                    ngayKyHDTVColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayKyHDTVColumn.AutoFit();
					var ngayKYHDCTVColumn = sheet.Column(94);
                    ngayKYHDCTVColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayKYHDCTVColumn.AutoFit();
					var ngayKyHDKVColumn = sheet.Column(95);
                    ngayKyHDKVColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayKyHDKVColumn.AutoFit();
					var ngayKYHDTTColumn = sheet.Column(96);
                    ngayKYHDTTColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayKYHDTTColumn.AutoFit();
					var ngayKyHDColumn = sheet.Column(97);
                    ngayKyHDColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayKyHDColumn.AutoFit();
					

                });
        }
    }
}
