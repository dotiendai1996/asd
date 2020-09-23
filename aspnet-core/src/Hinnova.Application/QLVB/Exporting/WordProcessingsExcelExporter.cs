using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class WordProcessingsExcelExporter : EpPlusExcelExporterBase, IWordProcessingsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public WordProcessingsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetWordProcessingForViewDto> wordProcessings)
        {
            return CreateExcelPackage(
                "WordProcessings.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("WordProcessings"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ReceivePlace"),
                        L("Name"),
                        L("Content"),
                        L("Status"),
                        L("Comment"),
                        L("KeyWordId")
                        );

                    AddObjects(
                        sheet, 2, wordProcessings,
                        _ => _.WordProcessing.ReceivePlace,
                        _ => _.WordProcessing.Name,
                        _ => _.WordProcessing.Content,
                        _ => _.WordProcessing.Status,
                        _ => _.WordProcessing.Comment,
                        _ => _.WordProcessing.KeyWordId
                        );

					

                });
        }
    }
}
