using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class DynamicValuesExcelExporter : EpPlusExcelExporterBase, IDynamicValuesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public DynamicValuesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetDynamicValueForViewDto> dynamicValues)
        {
            return CreateExcelPackage(
                "DynamicValues.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("DynamicValues"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ObjectId"),
                        L("Key"),
                        L("Value"),
                        L("DynamicFieldId")
                        );

                    AddObjects(
                        sheet, 2, dynamicValues,
                        _ => _.DynamicValue.ObjectId,
                        _ => _.DynamicValue.Key,
                        _ => _.DynamicValue.Value,
                        _ => _.DynamicValue.DynamicFieldId
                        );

					

                });
        }
    }
}
