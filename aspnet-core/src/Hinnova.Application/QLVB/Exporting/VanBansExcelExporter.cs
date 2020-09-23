using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class VanbansExcelExporter : EpPlusExcelExporterBase, IVanbansExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public VanbansExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetVanbanForViewDto> vanbans)
        {
            return CreateExcelPackage(
                "Vanbans.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Vanbans"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("TenCongViec"),
                        L("NgayGiaoViec"),
                        L("HanKetThuc"),
                        L("NguoiXuLy"),
                        L("TienDoChung"),
                        L("TinhTrang"),
                        L("NoiDung")
                        );

                    AddObjects(
                        sheet, 2, vanbans,
                        _ => _.Vanban.TenCongViec,
                        _ => _timeZoneConverter.Convert(_.Vanban.NgayGiaoViec, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.Vanban.HanKetThuc, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Vanban.NguoiXuLy,
                        _ => _.Vanban.TienDoChung,
                        _ => _.Vanban.TinhTrang,
                        _ => _.Vanban.NoiDung
                        );

					var ngayGiaoViecColumn = sheet.Column(2);
                    ngayGiaoViecColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayGiaoViecColumn.AutoFit();
					var hanKetThucColumn = sheet.Column(3);
                    hanKetThucColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					hanKetThucColumn.AutoFit();
					

                });
        }
    }
}
