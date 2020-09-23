using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLNS.Exporting
{
    public class QuanLyNghiPhepsExcelExporter : EpPlusExcelExporterBase, IQuanLyNghiPhepsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public QuanLyNghiPhepsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetQuanLyNghiPhepForViewDto> quanLyNghiPheps)
        {
            return CreateExcelPackage(
                "QuanLyNghiPheps.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("QuanLyNghiPheps"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("TenNhanVien"),
                        L("DonViCongTacID"),
                        L("MaNV"),
                        L("NghiPhep"),
                        L("CongTac"),
                        L("NgayBatDau"),
                        L("NgayKetThuc"),
                        L("LyDo"),
                        L("QuanLyTrucTiepID"),
                        L("TruongBoPhanID"),
                        L("TepDinhKem"),
                        L("TenCTY"),
                        L("NguoiDuyetID"),
                        L("NgayDuyet"),
                        L("TrangThai")
                        );

                    AddObjects(
                        sheet, 2, quanLyNghiPheps,
                        _ => _.QuanLyNghiPhep.TenNhanVien,
                        _ => _.QuanLyNghiPhep.DonViCongTacID,
                        _ => _.QuanLyNghiPhep.MaNV,
                        _ => _.QuanLyNghiPhep.NghiPhep,
                        _ => _.QuanLyNghiPhep.CongTac,
                        _ => _timeZoneConverter.Convert(_.QuanLyNghiPhep.NgayBatDau, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.QuanLyNghiPhep.NgayKetThuc, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.QuanLyNghiPhep.LyDo,
                        _ => _.QuanLyNghiPhep.QuanLyTrucTiepID,
                        _ => _.QuanLyNghiPhep.TruongBoPhanID,
                        _ => _.QuanLyNghiPhep.TepDinhKem,
                        _ => _.QuanLyNghiPhep.TenCTY,
                        _ => _.QuanLyNghiPhep.NguoiDuyetID,
                        _ => _timeZoneConverter.Convert(_.QuanLyNghiPhep.NgayDuyet, _abpSession.TenantId, _abpSession.GetUserId())
                  
                        );

					var ngayBatDauColumn = sheet.Column(6);
                    ngayBatDauColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayBatDauColumn.AutoFit();
					var ngayKetThucColumn = sheet.Column(7);
                    ngayKetThucColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayKetThucColumn.AutoFit();
					var ngayDuyetColumn = sheet.Column(14);
                    ngayDuyetColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayDuyetColumn.AutoFit();
					

                });
        }
    }
}
