using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class DynamicActionsExcelExporter : EpPlusExcelExporterBase, IDynamicActionsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public DynamicActionsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetDynamicActionForViewDto> dynamicActions)
        {
            return CreateExcelPackage(
                "DynamicActions.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("DynamicActions"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("LabelId"),
                        //L("IsActive"),
                        L("HasSave"),
                        L("HasReturn"),
                        L("HasTransfer"),
                        L("HasSaveAndTransfer"),
                        L("HasFinish"),
                        L("IsTopPosition"),
                        L("IsBack"),
                        L("HasAssignWork"),
                        L("Description"),
                        L("Order")
                        );

                    AddObjects(
                        sheet, 2, dynamicActions,
                        _ => _.DynamicAction.LabelId,
                        //_ => _.DynamicAction.IsActive,
                        _ => _.DynamicAction.HasSave,
                        _ => _.DynamicAction.HasReturn,
                        _ => _.DynamicAction.HasTransfer,
                        _ => _.DynamicAction.HasSaveAndTransfer,
                        _ => _.DynamicAction.HasFinish,
                        _ => _.DynamicAction.Position,
                        _ => _.DynamicAction.IsBack,
                        _ => _.DynamicAction.HasAssignWork,
                        _ => _.DynamicAction.Description,
                        _ => _.DynamicAction.Order
                        );

					

                });
        }
    }
}
