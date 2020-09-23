using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class Memorize_KeywordsesExcelExporter : EpPlusExcelExporterBase, IMemorize_KeywordsesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public Memorize_KeywordsesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetMemorize_KeywordsForViewDto> memorize_Keywordses)
        {
            return CreateExcelPackage(
                "Memorize_Keywordses.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Memorize_Keywordses"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("TenGoiNho"),
                        L("XuLyChinh"),
                        L("DongXuLy"),
                        L("DeBiet"),
                        L("Head_ID"),
                        L("Full_Name"),
                        L("Prefix"),
                        L("Hire_Date"),
                        L("KeyWord"),
                        L("IsActive")
                        );

                    AddObjects(
                        sheet, 2, memorize_Keywordses,
                        _ => _.Memorize_Keywords.TenGoiNho,
                        _ => _.Memorize_Keywords.XuLyChinh,
                        _ => _.Memorize_Keywords.DongXuLy,
                        _ => _.Memorize_Keywords.DeBiet,
                        _ => _.Memorize_Keywords.Head_ID,
                        _ => _.Memorize_Keywords.Full_Name,
                        _ => _.Memorize_Keywords.Prefix,
                        _ => _timeZoneConverter.Convert(_.Memorize_Keywords.Hire_Date, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Memorize_Keywords.KeyWord,
                        _ => _.Memorize_Keywords.IsActive
                        );

					var hire_DateColumn = sheet.Column(8);
                    hire_DateColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					hire_DateColumn.AutoFit();
					

                });
        }
    }
}
