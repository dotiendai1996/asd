using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class DynamicFieldsExcelExporter : EpPlusExcelExporterBase, IDynamicFieldsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public DynamicFieldsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetDynamicFieldForViewDto> dynamicFields)
        {
            return CreateExcelPackage(
                "DynamicFields.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("DynamicFields"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ModuleId"),
                        L("TableName"),
                        L("Name"),
                        L("TypeField"),
                        L("Width"),
                        L("NameDescription"),
                        L("WidthDescription"),
                        L("ClassAttach")
                        );

                    AddObjects(
                        sheet, 2, dynamicFields,
                        _ => _.DynamicField.ModuleId,
                        _ => _.DynamicField.TableName,
                        _ => _.DynamicField.Name,
                        _ => _.DynamicField.TypeField,
                        _ => _.DynamicField.Width,
                        _ => _.DynamicField.NameDescription,
                        _ => _.DynamicField.WidthDescription,
                        _ => _.DynamicField.ClassAttach
                        );

					

                });
        }
    }
}
