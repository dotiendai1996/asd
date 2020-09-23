using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLNSDtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLNSExporting
{
    public class SYS_CODEMASTERSsExcelExporter : EpPlusExcelExporterBase, ISYS_CODEMASTERSsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public SYS_CODEMASTERSsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetSYS_CODEMASTERSForViewDto> syS_CODEMASTERSs)
        {
            return CreateExcelPackage(
                "SYS_CODEMASTERSsxlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("SYS_CODEMASTERSs"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Prefix"),
                        L("CurValue"),
                        L("Description"),
                        L("Active")
                        );

                    AddObjects(
                        sheet, 2, syS_CODEMASTERSs,
                        _ => _.SYS_CODEMASTERS.Prefix,
                        _ => _.SYS_CODEMASTERS.CurValue,
                        _ => _.SYS_CODEMASTERS.Description,
                        _ => _.SYS_CODEMASTERS.Active
                        );

					

                });
        }
    }
}
