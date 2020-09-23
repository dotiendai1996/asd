using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLNSDtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLNSExporting
{
    public class HopDongsExcelExporter : EpPlusExcelExporterBase, IHopDongsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public HopDongsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetHopDongForViewDto> hopDongs)
        {
            return CreateExcelPackage(
                "HopDongs.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("HopDongs"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("NhanVienId"),
                        L("HoTenNhanVien"),
                        L("ViTriCongViecCode"),
                        L("SoHopDong"),
                        L("NgayKy"),
                        L("DonViCongTacID"),
                        L("TenHopDong"),
                        L("LoaiHopDongCode"),
                        L("HinhThucLamViecCode"),
                        L("NgayCoHieuLuc"),
                        L("NgayHetHan"),
                        L("LuongCoBan"),
                        L("LuongDongBaoHiem"),
                        L("TyLeHuongLuong"),
                        L("NguoiDaiDienCongTy"),
                        L("ChucDanh"),
                        L("TrichYeu"),
                        L("TepDinhKem"),
                        L("GhiChu"),
                        //L("RECORD_STATUS"),
                        //L("MARKER_ID"),
                        //L("AUTH_STATUS"),
                        //L("CHECKER_ID"),
                        //L("APPROVE_DT"),
                        L("Thời hạn hợp đồng")
                        );

                    AddObjects(
                        sheet, 2, hopDongs,
                        _ => _.HopDong.NhanVienId,
                        _ => _.HopDong.HoTenNhanVien,
                        _ => _.HopDong.ViTriCongViecCode,
                        _ => _.HopDong.SoHopDong,
                        _ => _timeZoneConverter.Convert(_.HopDong.NgayKy, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.DonViCongTacValue,
                        _ => _.HopDong.TenHopDong,
                        _ => _.LoaiHopDongValue,
                        _ => _.HinhThucLamViecValue,
                        _ => _timeZoneConverter.Convert(_.HopDong.NgayCoHieuLuc, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.HopDong.NgayHetHan, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.HopDong.LuongCoBan,
                        _ => _.HopDong.LuongDongBaoHiem,
                        _ => _.HopDong.TyLeHuongLuong,
                        _ => _.HopDong.NguoiDaiDienCongTy,
                        _ => _.HopDong.ChucDanh,
                        _ => _.HopDong.TrichYeu,
                        _ => _.HopDong.TepDinhKem,
                        _ => _.HopDong.GhiChu,
                        //_ => _.HopDong.RECORD_STATUS,
                        //_ => _.HopDong.MARKER_ID,
                        //_ => _.HopDong.AUTH_STATUS,
                        //_ => _.HopDong.CHECKER_ID,
                        //_ => _timeZoneConverter.Convert(_.HopDong.APPROVE_DT, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.ThoiHanhopDongTaoValue
                        );

					var ngayKyColumn = sheet.Column(5);
                    ngayKyColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayKyColumn.AutoFit();
					var ngayCoHieuLucColumn = sheet.Column(10);
                    ngayCoHieuLucColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayCoHieuLucColumn.AutoFit();
					var ngayHetHanColumn = sheet.Column(11);
                    ngayHetHanColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayHetHanColumn.AutoFit();
					var approvE_DTColumn = sheet.Column(24);
                    approvE_DTColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					approvE_DTColumn.AutoFit();
					

                });
        }
    }
}
