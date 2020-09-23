using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLNS.Exporting
{
    public class ConfigEmailsExcelExporter : EpPlusExcelExporterBase, IConfigEmailsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ConfigEmailsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetConfigEmailForViewDto> configEmails)
        {
            return CreateExcelPackage(
                "ConfigEmails.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ConfigEmails"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("DiaChiEmail"),
                        L("TenHienThi"),
                        L("DiaChiIP"),
                        L("CongSMTP"),
                        L("CheckSSL"),
                        L("CheckThongTin"),
                        L("TenMien"),
                        L("TenTruyCap"),
                        L("MatKhau"),
                        L("TenCTY")
                        );

                    AddObjects(
                        sheet, 2, configEmails,
                        _ => _.ConfigEmail.DiaChiEmail,
                        _ => _.ConfigEmail.TenHienThi,
                        _ => _.ConfigEmail.DiaChiIP,
                        _ => _.ConfigEmail.CongSMTP,
                        _ => _.ConfigEmail.CheckSSL,
                        _ => _.ConfigEmail.CheckThongTin,
                        _ => _.ConfigEmail.TenMien,
                        _ => _.ConfigEmail.TenTruyCap,
                        _ => _.ConfigEmail.MatKhau,
                        _ => _.ConfigEmail.TenCTY
                        );

					

                });
        }
    }
}
