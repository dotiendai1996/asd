using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class DocumentDetailsExcelExporter : EpPlusExcelExporterBase, IDocumentDetailsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public DocumentDetailsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetDocumentDetailForViewDto> documentDetails)
        {
            return CreateExcelPackage(
                "DocumentDetails.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("DocumentDetails"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("IncomingNumber"),
                        L("Pages"),
                        L("Datehandle"),
                        L("Typehandle"),
                        L("Description"),
                        L("Status"),
                        L("IsStared"),
                        L("Priority")
                        );

                    AddObjects(
                        sheet, 2, documentDetails,
                        _ => _timeZoneConverter.Convert(_.DocumentDetail.Datehandle, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.DocumentDetail.Typehandle,
                        _ => _.DocumentDetail.Description,
                        _ => _.DocumentDetail.Status,
                        _ => _.DocumentDetail.IsStared,
                        _ => _.DocumentDetail.Priority
                        );

					var datehandleColumn = sheet.Column(3);
                    datehandleColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					datehandleColumn.AutoFit();
					

                });
        }
    }
}
