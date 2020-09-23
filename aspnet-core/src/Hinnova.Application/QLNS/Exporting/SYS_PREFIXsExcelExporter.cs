using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLNSDtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLNSExporting
{
    public class SYS_PREFIXsExcelExporter : EpPlusExcelExporterBase, ISYS_PREFIXsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public SYS_PREFIXsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetSYS_PREFIXForViewDto> syS_PREFIXs)
        {
            return CreateExcelPackage(
                "SYS_PREFIXsxlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("SYS_PREFIXs"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Code"),
                        L("Prefix"),
                        L("Description")
                        );

                    AddObjects(
                        sheet, 2, syS_PREFIXs,
                        _ => _.SYS_PREFIX.Code,
                        _ => _.SYS_PREFIX.Prefix,
                        _ => _.SYS_PREFIX.Description
                        );

					

                });
        }
    }
}
