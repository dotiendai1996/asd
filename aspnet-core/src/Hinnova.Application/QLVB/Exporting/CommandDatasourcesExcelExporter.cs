using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class CommandDatasourcesExcelExporter : EpPlusExcelExporterBase, ICommandDatasourcesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public CommandDatasourcesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetCommandDatasourceForViewDto> commandDatasources)
        {
            return CreateExcelPackage(
                "CommandDatasources.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("CommandDatasources"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Command"),
                        L("Key"),
                        L("Value"),
                        L("DynamicDatasourceId"),
                        L("Order"),
                        L("IsActive")
                        );

                    AddObjects(
                        sheet, 2, commandDatasources,
                        _ => _.CommandDatasource.Command,
                        _ => _.CommandDatasource.Key,
                        _ => _.CommandDatasource.Value,
                        _ => _.CommandDatasource.DynamicDatasourceId,
                        _ => _.CommandDatasource.Order,
                        _ => _.CommandDatasource.IsActive
                        );

					

                });
        }
    }
}
