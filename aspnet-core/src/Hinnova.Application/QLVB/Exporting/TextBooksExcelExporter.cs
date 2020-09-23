using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLVB.Dtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLVB.Exporting
{
    public class TextBooksExcelExporter : EpPlusExcelExporterBase, ITextBooksExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public TextBooksExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetTextBookForViewDto> textBooks)
        {
            return CreateExcelPackage(
                "TextBooks.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("TextBooks"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("SoDen"),
                        L("NgayDen"),
                        L("SoHieuGoc"),
                        L("CoQuanBanHanh"),
                        L("TrichYeu"),
                        L("NguoiChiDao"),
                        L("Nguoi_DonVi"),
                        L("FileDinhKem")
                        );

                    AddObjects(
                        sheet, 2, textBooks,
                        _ => _.TextBook.SoDen,
                        _ => _timeZoneConverter.Convert(_.TextBook.NgayDen, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.TextBook.SoHieuGoc,
                        _ => _.TextBook.CoQuanBanHanh,
                        _ => _.TextBook.TrichYeu,
                        _ => _.TextBook.NguoiChiDao,
                        _ => _.TextBook.Nguoi_DonVi,
                        _ => _.TextBook.FileDinhKem
                        );

					var ngayDenColumn = sheet.Column(2);
                    ngayDenColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayDenColumn.AutoFit();
					

                });
        }
    }
}
