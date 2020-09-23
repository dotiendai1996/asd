using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLNS.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;
using System.Data;
using System.IO;
using System;

namespace Hinnova.QLNS.Exporting
{
    public class DataChamCongsExcelExporter : AsposeExcelExporterBase, IDataChamCongsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public DataChamCongsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(DataSet dataChamCongs, DateTime dateTime)
        {
            var templateFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Template", "ChamCong", "ChamCongTemplate.xls");
            return CreateExcelPackage(templateFile, dataChamCongs, $"BaoCaoChamCong_Thang{dateTime.Month}_{DateTime.Now.ToString(AppConsts.DateTimeExportFormat)}.xlsx");
        }
        public FileDto ExportToFile(DataSet dataChamCongs)
        {
            var templateFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Template", "ChamCong", "ChamCongFilterTemplate.xls");
            return CreateExcelPackage(templateFile, dataChamCongs, $"BaoCaoChamCong_{DateTime.Now.ToString(AppConsts.DateTimeExportFormat)}.xlsx");
        }
    }
}
