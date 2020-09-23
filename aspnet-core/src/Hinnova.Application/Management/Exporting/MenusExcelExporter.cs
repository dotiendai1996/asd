using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.Management.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.Management.Exporting
{
    public class MenusExcelExporter : EpPlusExcelExporterBase, IMenusExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public MenusExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetMenuForViewDto> menus)
        {
            return CreateExcelPackage(
                "Menus.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Menus"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Name"),
                        L("Title"),
                        L("Icon"),
                        L("Description"),
                        L("Parent"),
                        //L("IsParent"),
                        L("Link"),
                        //L("Type"),
                        L("CreationTime"),
                        L("LastModificationTime"),
                        L("IsDeleted"),
                        L("DeletionTime"),
                        L("Index"),
                        //L("IsDelimiter"),
                        L("RequiredPermissionName")
                        );

                    AddObjects(
                        sheet, 2, menus,
                        _ => _.Menu.Name,
                        _ => _.Menu.Title,
                        _ => _.Menu.Icon,
                        _ => _.Menu.Description,
                        _ => _.Menu.Parent,
                        //_ => _.Menu.IsParent,
                        _ => _.Menu.Link,
                        //_ => _.Menu.Type,
                        _ => _timeZoneConverter.Convert(_.Menu.CreationTime, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.Menu.LastModificationTime, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Menu.IsDeleted,
                        _ => _timeZoneConverter.Convert(_.Menu.DeletionTime, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Menu.Index,
                        //_ => _.Menu.IsDelimiter,
                        _ => _.Menu.RequiredPermissionName
                        );

					var creationTimeColumn = sheet.Column(9);
                    creationTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					creationTimeColumn.AutoFit();
					var lastModificationTimeColumn = sheet.Column(10);
                    lastModificationTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					lastModificationTimeColumn.AutoFit();
					var deletionTimeColumn = sheet.Column(12);
                    deletionTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					deletionTimeColumn.AutoFit();
					

                });
        }
    }
}
