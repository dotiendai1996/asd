using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class DocumentHandlingsExcelExporter : EpPlusExcelExporterBase, IDocumentHandlingsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public DocumentHandlingsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetDocumentHandlingForViewDto> documentHandlings)
        {
            return CreateExcelPackage(
                "DocumentHandlings.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("DocumentHandlings"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("DocumentId"),
                        L("Handler"),
                        L("HandlingDetailId"),
                        L("PlaceReceive"),
                        //L("KeywordId"),
                        L("Content"),
                        L("Status"),
                        L("Comment"),
                        L("CreationTime"),
                        L("EndDate")
                        );

                    AddObjects(
                        sheet, 2, documentHandlings,
                        _ => _.DocumentHandling.DocumentId,
                        _ => _.DocumentHandling.Handler,
                        _ => _.DocumentHandling.HandlingDetailId,
                        _ => _.DocumentHandling.PlaceReceive,
                        //_ => _.DocumentHandling.KeywordId,
                        _ => _.DocumentHandling.Content,
                        _ => _.DocumentHandling.Status,
                        _ => _.DocumentHandling.Comment,
                        _ => _.DocumentHandling.CreationTime,
                        _ => _.DocumentHandling.EndDate
                        );

					

                });
        }
    }
}
