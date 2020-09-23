using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.Management.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.Management.Exporting
{
    public class SqlConfigDetailsExcelExporter : EpPlusExcelExporterBase, ISqlConfigDetailsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public SqlConfigDetailsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetSqlConfigDetailForViewDto> sqlConfigDetails)
        {
            return CreateExcelPackage(
                "SqlConfigDetails.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("SqlConfigDetails"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("SqlConfigId"),
                        L("Code"),
                        L("Name"),
                        L("Format"),
                        L("Type"),
                        L("Width"),
                        L("ColNum"),
                        L("GroupLevel"),
                        L("IsDisplay"),
                        L("Order"),
                        L("TextAlign"),
                        L("Version"),
                        L("IsSum"),
                        L("IsFreePane"),
                        L("IsParent"),
                        L("ParentCode"),
                        L("GroupSort")
                        );

                    AddObjects(
                        sheet, 2, sqlConfigDetails,
                        _ => _.SqlConfigDetail.SqlConfigId,
                        _ => _.SqlConfigDetail.Code,
                        _ => _.SqlConfigDetail.Name,
                        _ => _.SqlConfigDetail.Format,
                        _ => _.SqlConfigDetail.Type,
                        _ => _.SqlConfigDetail.Width,
                        _ => _.SqlConfigDetail.ColNum,
                        _ => _.SqlConfigDetail.GroupLevel,
                        _ => _.SqlConfigDetail.IsDisplay,
                        _ => _.SqlConfigDetail.Order,
                        _ => _.SqlConfigDetail.TextAlign,
                        _ => _.SqlConfigDetail.Version,
                        _ => _.SqlConfigDetail.IsSum,
                        _ => _.SqlConfigDetail.IsFreePane,
                        _ => _.SqlConfigDetail.IsParent,
                        _ => _.SqlConfigDetail.ParentCode,
                        _ => _.SqlConfigDetail.GroupSort
                        );

					

                });
        }
    }
}
