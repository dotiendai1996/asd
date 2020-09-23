using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLNS.Exporting
{
    public class QuyTrinhCongTacsExcelExporter : EpPlusExcelExporterBase, IQuyTrinhCongTacsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public QuyTrinhCongTacsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetQuyTrinhCongTacForViewDto> quyTrinhCongTacs)
        {
            return CreateExcelPackage(
                "QuyTrinhCongTacs.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("QuyTrinhCongTacs"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("TenCty"),
                        L("DateTo"),
                        L("DateFrom"),
                        L("ViTriCongViecCode"),
                        L("DonViCongTacID"),
                        L("QuanLyTrucTiep"),
                        L("TrangThaiCode"),
                        L("GhiChu")
                        );

                    AddObjects(
                        sheet, 2, quyTrinhCongTacs,
                        _ => _.QuyTrinhCongTac.TenCty,
                        _ => _timeZoneConverter.Convert(_.QuyTrinhCongTac.DateTo, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.QuyTrinhCongTac.DateFrom, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.QuyTrinhCongTac.ViTriCongViecCode,
                        _ => _.QuyTrinhCongTac.DonViCongTacID,
                        _ => _.QuyTrinhCongTac.QuanLyTrucTiep,
                        _ => _.QuyTrinhCongTac.TrangThaiCode,
                        _ => _.QuyTrinhCongTac.GhiChu
                        );

					var dateToColumn = sheet.Column(2);
                    dateToColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					dateToColumn.AutoFit();
					var dateFromColumn = sheet.Column(3);
                    dateFromColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					dateFromColumn.AutoFit();
					

                });
        }
    }
}
