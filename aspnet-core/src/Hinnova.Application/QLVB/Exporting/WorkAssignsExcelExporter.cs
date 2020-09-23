using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class WorkAssignsExcelExporter : EpPlusExcelExporterBase, IWorkAssignsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public WorkAssignsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetWorkAssignForViewDto> workAssigns)
        {
            return CreateExcelPackage(
                "WorkAssigns.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("WorkAssigns"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Name"),
                        L("StartDate"),
                        L("EndDate"),
                        L("Assignee"),
                        L("Progress"),
                        L("Status"),
                        L("Description"),
                        L("Action")
                        );

                    AddObjects(
                        sheet, 2, workAssigns,
                        _ => _.WorkAssign.Name,
                        _ => _timeZoneConverter.Convert(_.WorkAssign.StartDate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.WorkAssign.EndDate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.WorkAssign.Assignee,
                        _ => _.WorkAssign.Progress,
                        _ => _.WorkAssign.Status,
                        _ => _.WorkAssign.Description,
                        _ => _.WorkAssign.Action
                        );

					var startDateColumn = sheet.Column(2);
                    startDateColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					startDateColumn.AutoFit();
					var endDateColumn = sheet.Column(3);
                    endDateColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					endDateColumn.AutoFit();
					

                });
        }
    }
}
