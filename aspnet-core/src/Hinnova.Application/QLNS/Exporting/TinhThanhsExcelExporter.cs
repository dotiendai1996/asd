using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLNSDtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLNSExporting
{
    public class TinhThanhsExcelExporter : EpPlusExcelExporterBase, ITinhThanhsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public TinhThanhsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetTinhThanhForViewDto> tinhThanhs)
        {
            return CreateExcelPackage(
                "TinhThanhs.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("TinhThanhs"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("TenTinhThanh"),
                        L("MaTinhThanh")
                        );

                    AddObjects(
                        sheet, 2, tinhThanhs,
                        _ => _.TinhThanh.TenTinhThanh,
                        _ => _.TinhThanh.MaTinhThanh
                        );

					

                });
        }
    }
}
