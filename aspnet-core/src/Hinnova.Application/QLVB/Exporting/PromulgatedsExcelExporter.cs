using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class PromulgatedsExcelExporter : EpPlusExcelExporterBase, IPromulgatedsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PromulgatedsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPromulgatedForViewDto> promulgateds)
        {
            return CreateExcelPackage(
                "Promulgateds.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Promulgateds"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Name"),
                        L("DisplayName"),
                        L("Representative"),
                        L("Leader"),
                        L("Position")
                        );

                    AddObjects(
                        sheet, 2, promulgateds,
                        _ => _.Promulgated.Name,
                        _ => _.Promulgated.DisplayName,
                        _ => _.Promulgated.Representative,
                        _ => _.Promulgated.Leader,
                        _ => _.Promulgated.Position
                        );

					

                });
        }
    }
}
