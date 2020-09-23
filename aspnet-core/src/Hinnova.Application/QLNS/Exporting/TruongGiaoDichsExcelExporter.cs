using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLNSDtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLNSExporting
{
    public class TruongGiaoDichsExcelExporter : EpPlusExcelExporterBase, ITruongGiaoDichsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public TruongGiaoDichsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetTruongGiaoDichForViewDto> truongGiaoDichs)
        {
            return CreateExcelPackage(
                "TruongGiaoDichs.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("TruongGiaoDichs"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Code"),
                        L("CDName"),
                        L("Value"),
                        L("GhiChu")
                        );

                    AddObjects(
                        sheet, 2, truongGiaoDichs,
                        _ => _.TruongGiaoDich.Code,
                        _ => _.TruongGiaoDich.CDName,
                        _ => _.TruongGiaoDich.Value,
                        _ => _.TruongGiaoDich.GhiChu
                        );

					

                });
        }
    }
}
