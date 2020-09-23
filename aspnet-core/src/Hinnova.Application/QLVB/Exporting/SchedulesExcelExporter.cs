using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class SchedulesExcelExporter : EpPlusExcelExporterBase, ISchedulesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public SchedulesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetScheduleForViewDto> schedules)
        {
            return CreateExcelPackage(
                "Schedules.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Schedules"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ScheduleTypeID"),
                        L("DateCreated"),
                        L("DateOccur"),
                        L("FromTime"),
                        L("ToTime"),
                        L("Content"),
                        L("Notes")
                        );

                    AddObjects(
                        sheet, 2, schedules,
                        _ => _.Schedule.ScheduleTypeID,
                        _ => _timeZoneConverter.Convert(_.Schedule.DateCreated, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.Schedule.DateOccur, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Schedule.FromTime,
                        _ => _.Schedule.ToTime,
                        _ => _.Schedule.Content,
                        _ => _.Schedule.Notes
                        );

					var dateCreatedColumn = sheet.Column(2);
                    dateCreatedColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					dateCreatedColumn.AutoFit();
					var dateOccurColumn = sheet.Column(3);
                    dateOccurColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					dateOccurColumn.AutoFit();
					

                });
        }
    }
}
