using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class DynamicDatasourceExcelExporter : EpPlusExcelExporterBase, IDynamicDatasourceExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public DynamicDatasourceExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetDynamicDatasourceForViewDto> dynamicDatasource)
        {
            return CreateExcelPackage(
                "DynamicDatasource.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("DynamicDatasource"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Type"),
                        L("ObjectId"),
                        L("DynamicFieldId"),
                        L("Order"),
                        L("IsActive")
                        );

                    AddObjects(
                        sheet, 2, dynamicDatasource,
                        _ => _.DynamicDatasource.Type,
                        _ => _.DynamicDatasource.ObjectId,
                        _ => _.DynamicDatasource.DynamicFieldId,
                        _ => _.DynamicDatasource.Order,
                        _ => _.DynamicDatasource.IsActive
                        );

					

                });
        }
    }
}
