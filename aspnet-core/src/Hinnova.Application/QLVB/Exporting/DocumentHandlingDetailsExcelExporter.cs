using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class DocumentHandlingDetailsExcelExporter : EpPlusExcelExporterBase, IDocumentHandlingDetailsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public DocumentHandlingDetailsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetDocumentHandlingDetailForViewDto> documentHandlingDetails)
        {
            return CreateExcelPackage(
                "DocumentHandlingDetails.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("DocumentHandlingDetails"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Group"),
                        L("Person"),
                        L("Type"),
                        L("Superios"),
                        L("PersonalComment"),
                        L("MainHandling"),
                        L("CoHandling"),
                        L("ToKnow"),
                        L("StartDate"),
                        L("EndDate")
                        );

                    AddObjects(
                        sheet, 2, documentHandlingDetails,
                        _ => _.DocumentHandlingDetail.Group,
                        _ => _.DocumentHandlingDetail.Person,
                        _ => _.DocumentHandlingDetail.Type,
                        _ => _.DocumentHandlingDetail.Superios,
                        _ => _.DocumentHandlingDetail.PersonalComment,
                        _ => _.DocumentHandlingDetail.MainHandling,
                        _ => _.DocumentHandlingDetail.CoHandling,
                        _ => _.DocumentHandlingDetail.ToKnow,
                        _ => _timeZoneConverter.Convert(_.DocumentHandlingDetail.StartDate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.DocumentHandlingDetail.EndDate, _abpSession.TenantId, _abpSession.GetUserId())
                        );

					var startDateColumn = sheet.Column(6);
                    startDateColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					startDateColumn.AutoFit();
					var endDateColumn = sheet.Column(7);
                    endDateColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					endDateColumn.AutoFit();
					

                });
        }
    }
}
