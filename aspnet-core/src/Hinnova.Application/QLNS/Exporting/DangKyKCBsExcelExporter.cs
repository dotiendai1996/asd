using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLNSDtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLNSExporting
{
    public class DangKyKCBsExcelExporter : EpPlusExcelExporterBase, IDangKyKCBsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public DangKyKCBsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetDangKyKCBForViewDto> dangKyKCBs)
        {
            return CreateExcelPackage(
                "DangKyKCBs.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("DangKyKCBs"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("TenNoiKCB"),
                        L("MaNoiKCB"),
                        L("DiaChi"),
                        L("TinhThanhID"),
                        L("GhiChu")
                        );

                    AddObjects(
                        sheet, 2, dangKyKCBs,
                        _ => _.DangKyKCB.TenNoiKCB,
                        _ => _.DangKyKCB.MaNoiKCB,
                        _ => _.DangKyKCB.DiaChi,
                        _ => _.DangKyKCB.TinhThanhID,
                        _ => _.DangKyKCB.GhiChu
                        );

					

                });
        }
    }
}
