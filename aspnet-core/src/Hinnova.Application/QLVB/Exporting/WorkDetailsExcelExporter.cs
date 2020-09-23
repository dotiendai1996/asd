using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class WorkDetailsExcelExporter : EpPlusExcelExporterBase, IWorkDetailsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public WorkDetailsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetWorkDetailForViewDto> workDetails)
        {
            return CreateExcelPackage(
                "WorkDetails.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("WorkDetails"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("WorkAssignId"),
                        L("DonePersentage"),
                        L("Date"),
                        L("NameID"),
                        L("Description"),
                        L("Repply"),
                        L("Attachment")
                        );

                    AddObjects(
                        sheet, 2, workDetails,
                        _ => _.WorkDetail.WorkAssignId,
                        _ => _.WorkDetail.DonePersentage,
                        _ => _timeZoneConverter.Convert(_.WorkDetail.Date, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.WorkDetail.NameID,
                        _ => _.WorkDetail.Description,
                        _ => _.WorkDetail.Repply,
                        _ => _.WorkDetail.Attachment
                        );

					var dateColumn = sheet.Column(3);
                    dateColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					dateColumn.AutoFit();
					

                });
        }
    }
}
