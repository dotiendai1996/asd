using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class HistoryUploadsExcelExporter : EpPlusExcelExporterBase, IHistoryUploadsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public HistoryUploadsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetHistoryUploadForViewDto> historyUploads)
        {
            return CreateExcelPackage(
                "HistoryUploads.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("HistoryUploads"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("File")
                        );

                    AddObjects(
                        sheet, 2, historyUploads,
                        _ => _.HistoryUpload.File
                        );

					

                });
        }
    }
}
