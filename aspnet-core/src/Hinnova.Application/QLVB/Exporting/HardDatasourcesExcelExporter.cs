using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class HardDatasourcesExcelExporter : EpPlusExcelExporterBase, IHardDatasourcesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public HardDatasourcesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetHardDatasourceForViewDto> hardDatasources)
        {
            return CreateExcelPackage(
                "HardDatasources.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("HardDatasources"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Key"),
                        L("Value"),
                        L("DynamicDatasourceId"),
                        L("Order"),
                        L("IsActive")
                        );

                    AddObjects(
                        sheet, 2, hardDatasources,
                        _ => _.HardDatasource.Key,
                        _ => _.HardDatasource.Value,
                        _ => _.HardDatasource.DynamicDatasourceId,
                        _ => _.HardDatasource.Order,
                        _ => _.HardDatasource.IsActive
                        );

					

                });
        }
    }
}
