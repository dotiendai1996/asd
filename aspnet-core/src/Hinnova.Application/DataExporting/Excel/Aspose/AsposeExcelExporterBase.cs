using Abp.AspNetZeroCore.Net;
using Abp.Dependency;
using Aspose.Cells;
using Hinnova.Dto;
using Hinnova.Storage;
using System.Data;
using System.IO;

namespace Hinnova.DataExporting.Excel.EpPlus
{
    public abstract class AsposeExcelExporterBase : HinnovaServiceBase, ITransientDependency
    {
        private readonly ITempFileCacheManager _tempFileCacheManager;

        protected AsposeExcelExporterBase(ITempFileCacheManager tempFileCacheManager)
        {
            _tempFileCacheManager = tempFileCacheManager;
        }

        protected FileDto CreateExcelPackage(string templateFile, DataSet dataSet, string reportName)
        {
            //
            var file = new FileDto(reportName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);

            var designer = new WorkbookDesigner { Workbook = new Workbook(templateFile) };

            //Set data row
            designer.SetDataSource(dataSet);
            designer.Process();

            MemoryStream excelStream = new MemoryStream();
            designer.Workbook.Save(excelStream, SaveFormat.Xlsx);

            _tempFileCacheManager.SetFile(file.FileToken, excelStream.ToArray());

            return file;
        }
        protected FileDto CreateTemplateExcelPackage(string templateFile, string reportName)
        {
            var fileDto = new FileDto(reportName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
            var fileByteArrays = File.ReadAllBytes(templateFile);
            _tempFileCacheManager.SetFile(fileDto.FileToken, fileByteArrays);

            return fileDto;
        }
    }
}