using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class StoreDatasourcesExcelExporter : EpPlusExcelExporterBase, IStoreDatasourcesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public StoreDatasourcesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetStoreDatasourceForViewDto> storeDatasources)
        {
            return CreateExcelPackage(
                "StoreDatasources.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("StoreDatasources"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("NameStore"),
                        L("Key"),
                        L("Value"),
                        L("DynamicDatasourceId"),
                        L("Order"),
                        L("IsActive")
                        );

                    AddObjects(
                        sheet, 2, storeDatasources,
                        _ => _.StoreDatasource.NameStore,
                        _ => _.StoreDatasource.Key,
                        _ => _.StoreDatasource.Value,
                        _ => _.StoreDatasource.DynamicDatasourceId,
                        _ => _.StoreDatasource.Order,
                        _ => _.StoreDatasource.IsActive
                        );

					

                });
        }
    }
}
