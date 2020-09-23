using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class ReceiveUnitsExcelExporter : EpPlusExcelExporterBase, IReceiveUnitsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ReceiveUnitsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetReceiveUnitForViewDto> receiveUnits)
        {
            return CreateExcelPackage(
                "ReceiveUnits.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ReceiveUnits"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Name"),
                        L("Position"),
                        L("IsActive")
                        );

                    AddObjects(
                        sheet, 2, receiveUnits,
                        _ => _.ReceiveUnit.Name,
                        _ => _.ReceiveUnit.Position,
                        _ => _.ReceiveUnit.IsActive
                        );

					

                });
        }
    }
}
