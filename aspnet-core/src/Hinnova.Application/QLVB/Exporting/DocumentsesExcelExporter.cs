using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class DocumentsesExcelExporter : EpPlusExcelExporterBase, IDocumentsesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public DocumentsesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetDocumentsForViewDto> documentses)
        {
            return CreateExcelPackage(
                "Documentses.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Documentses"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Number"),
                        L("TypeDocID"),
                        L("PlaceRecevie"),
                        L("SaveTo"),
                        L("Summary"),
                        L("ApprovedBy"),
                        L("Attachment"),
                        L("TypeRecevie"),
                        L("StartDate"),
                        L("EndDate"),
                        L("Status"),
                        L("Note"),
                        L("Priority"),
                        L("IncommingNumber"),
                        L("IncommingDate"),
                        L("Pages"),
                        L("Author"),
                        L("GroupAuthor"),
                        L("MoreInformation"),
                        L("Position"),
                        L("Range"),
                        L("IsActive"),
                        L("Order")
                        );

                    AddObjects(
                        sheet, 2, documentses,
                        _ => _.Documents.Number,
                        _ => _.Documents.DocumentTypeId,
                        _ => _.Documents.PlaceReceive,
                        _ => _.Documents.SaveTo,
                        _ => _.Documents.Summary,
                        _ => _.Documents.ApprovedBy,
                        _ => _.Documents.Attachment,
                        _ => _.Documents.TypeReceive,
                        _ => _timeZoneConverter.Convert(_.Documents.StartDate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.Documents.EndDate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Documents.Status,
                        _ => _.Documents.Note,
                        _ => _.Documents.Priority,
                        _ => _.Documents.IncommingNumber,
                        _ => _.Documents.IncommingDate,
                        _ => _.Documents.Pages,
                        _ => _.Documents.Author,
                        _ => _.Documents.GroupAuthor,
                        _ => _.Documents.MoreInformation,
                        _ => _.Documents.Position,
                        _ => _.Documents.Range,
                        _ => _.Documents.IsActive,
                        _ => _.Documents.Order
                        );

					var startDateColumn = sheet.Column(10);
                    startDateColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					startDateColumn.AutoFit();
					var endDateColumn = sheet.Column(11);
                    endDateColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					endDateColumn.AutoFit();
					

                });
        }
    }
}
