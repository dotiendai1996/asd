using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class MemorizeKeywordsExcelExporter : EpPlusExcelExporterBase, IMemorizeKeywordsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public MemorizeKeywordsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetMemorizeKeywordForViewDto> memorizeKeywords)
        {
            return CreateExcelPackage(
                "MemorizeKeywords.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("MemorizeKeywords"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("KeyWord"),
                        L("IsActive")
                        );

                    AddObjects(
                        sheet, 2, memorizeKeywords,
                        _ => _.MemorizeKeyword.KeyWord,
                        _ => _.MemorizeKeyword.IsActive
                        );

					

                });
        }
    }
}
