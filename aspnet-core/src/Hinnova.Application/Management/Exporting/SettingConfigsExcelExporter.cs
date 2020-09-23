using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.Management.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.Management.Exporting
{
    public class SettingConfigsExcelExporter : EpPlusExcelExporterBase, ISettingConfigsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public SettingConfigsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetSettingConfigForViewDto> settingConfigs)
        {
            return CreateExcelPackage(
                "SettingConfigs.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("SettingConfigs"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Code"),
                        L("ValueString"),
                        L("ValueInt"),
                        L("ValueHtml"),
                        L("Image")
                        );

                    AddObjects(
                        sheet, 2, settingConfigs,
                        _ => _.SettingConfig.Code,
                        _ => _.SettingConfig.ValueString,
                        _ => _.SettingConfig.ValueInt,
                        _ => _.SettingConfig.ValueHtml,
                        _ => _.SettingConfig.Image
                        );

					

                });
        }
    }
}
