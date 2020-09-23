using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLNSDtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLNSExporting
{
    public class LichSuLamViecsExcelExporter : EpPlusExcelExporterBase, ILichSuLamViecsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public LichSuLamViecsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetLichSuLamViecForViewDto> lichSuLamViecs)
        {
            return CreateExcelPackage(
                "LichSuLamViecsxlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("LichSuLamViecs"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("UngVienId"),
                        L("NoiDung"),
                        L("TepDinhKem")
                        );

                    AddObjects(
                        sheet, 2, lichSuLamViecs,
                        _ => _.LichSuLamViec.UngVienId,
                        _ => _.LichSuLamViec.NoiDung,
                        _ => _.LichSuLamViec.TepDinhKem
                        );

					

                });
        }
    }
}
