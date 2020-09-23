using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.Management.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.Management.Exporting
{
    public class SqlStoreParamsExcelExporter : EpPlusExcelExporterBase, ISqlStoreParamsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public SqlStoreParamsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetSqlStoreParamForViewDto> sqlStoreParams)
        {
            return CreateExcelPackage(
                "SqlStoreParams.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("SqlStoreParams"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("SqlConfigId"),
                        L("Code"),
                        L("Format"),
                        L("Name"),
                        L("IsActive"),
                        L("ValueString"),
                        L("ValueInt")
                        );

                    AddObjects(
                        sheet, 2, sqlStoreParams,
                        _ => _.SqlStoreParam.SqlConfigId,
                        _ => _.SqlStoreParam.Code,
                        _ => _.SqlStoreParam.Format,
                        _ => _.SqlStoreParam.Name,
                        _ => _.SqlStoreParam.IsActive,
                        _ => _.SqlStoreParam.ValueString,
                        _ => _.SqlStoreParam.ValueInt
                        );

					

                });
        }
    }
}
