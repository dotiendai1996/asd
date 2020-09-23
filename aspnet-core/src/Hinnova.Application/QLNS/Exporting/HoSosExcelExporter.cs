using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLNSDtos;
using Hinnova.Dto;
using Hinnova.Storage;
using System.IO;
using System;

namespace Hinnova.QLNSExporting
{
    public class HoSosExcelExporter : AsposeExcelExporterBase, IHoSosExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public HoSosExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportTemplateToFile()
        {
            var templateFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Template", "HoSo", "HoSoImportTemplate.xlsx");
            return CreateTemplateExcelPackage(templateFile, $"[Import]MauHoSo_{DateTime.Now.ToString(AppConsts.DateTimeExportFormat)}.xlsx");
        }
    }
}
