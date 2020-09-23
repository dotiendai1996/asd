using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLNSDtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLNSExporting
{
    public class NoiDaoTaosExcelExporter : EpPlusExcelExporterBase, INoiDaoTaosExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public NoiDaoTaosExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetNoiDaoTaoForViewDto> noiDaoTaos)
        {
            return CreateExcelPackage(
                "NoiDaoTaos.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("NoiDaoTaos"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("TenNoiDaoTao"),
                        L("MaNoiDaoTao"),
                        L("DiaChi"),
                        L("KhuVuc")
                        );

                    AddObjects(
                        sheet, 2, noiDaoTaos,
                        _ => _.NoiDaoTao.TenNoiDaoTao,
                        _ => _.NoiDaoTao.MaNoiDaoTao,
                        _ => _.NoiDaoTao.DiaChi,
                        _ => _.NoiDaoTao.KhuVuc
                        );

					

                });
        }
    }
}
