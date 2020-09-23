using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLNSDtos;
using Hinnova.Dto;
using Hinnova.Storage;

namespace Hinnova.QLNSExporting
{
    public class UngViensExcelExporter : EpPlusExcelExporterBase, IUngViensExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public UngViensExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetUngVienForViewDto> ungViens)
        {
            return CreateExcelPackage(
                "UngViens.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("UngViens"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("MaUngVien"),
                        L("HoVaTen"),
                        L("ViTriUngTuyenCode"),
                        L("KenhTuyenDungCode"),
                        L("GioiTinhCode"),
                        L("NgaySinh"),
                        L("SoCMND"),
                        L("NgayCap"),
                        L("NoiCap"),
                        L("TinhThanhID"),
                        L("TinhTrangHonNhanCode"),
                        L("TrinhDoDaoTaoCode"),
                        L("TrinhDoVanHoa"),
                        L("NoiDaoTaoID"),
                        L("Khoa"),
                        L("ChuyenNganh"),
                        L("XepLoaiCode"),
                        L("NamTotNghiep"),
                        L("TrangThaiCode"),
                        L("TienDoTuyenDungCode"),
                        L("TepDinhKem"),
                        L("RECORD_STATUS"),
                        L("MARKER_ID"),
                        L("AUTH_STATUS"),
                        L("CHECKER_ID"),
                        L("APPROVE_DT"),
                        L("DienThoai"),
                        L("Email"),
                        L("DiaChi")
                        );

                    AddObjects(
                        sheet, 2, ungViens,
                        _ => _.UngVien.MaUngVien,
                        _ => _.UngVien.HoVaTen,
                        _ => _.ViTriUngTuyenValue,
                        _ => _.KenhTuyenDungValue,
                        _ => _.GioiTinhValue,
                        _ => _timeZoneConverter.Convert(_.UngVien.NgaySinh, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.UngVien.SoCMND,
                        _ => _timeZoneConverter.Convert(_.UngVien.NgayCap, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.UngVien.NoiCap,
                        _ => _.TinhThanhValue,
                        _ => _.TinhTrangHonNhanValue,
                        _ => _.TrinhDoDaoTaoValue,
                        _ => _.UngVien.TrinhDoVanHoa,
                        _ => _.NoiDaoTaoValue,
                        _ => _.UngVien.Khoa,
                        _ => _.UngVien.ChuyenNganh,
                        _ => _.XepLoaiValue,
                        _ => _.UngVien.NamTotNghiep,
                        _ => _.TrangThaiValue,
                        _ => _.TienDoTuyenDungValue,
                        _ => _.UngVien.TepDinhKem,
                        _ => _.UngVien.RECORD_STATUS,
                        //_ => _.UngVien.MARKER_ID,
                        //_ => _.UngVien.AUTH_STATUS,
                        //_ => _.UngVien.CHECKER_ID,
                        //_ => _timeZoneConverter.Convert(_.UngVien.APPROVE_DT, _abpSession.TenantId, _abpSession.GetUserId()),
                        //_ => _.UngVien.DienThoai,
                        _ => _.UngVien.Email,
                        _ => _.UngVien.DiaChi
                        );

					var ngaySinhColumn = sheet.Column(6);
                    ngaySinhColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngaySinhColumn.AutoFit();
					var ngayCapColumn = sheet.Column(8);
                    ngayCapColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ngayCapColumn.AutoFit();
					var approvE_DTColumn = sheet.Column(26);
                    approvE_DTColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					approvE_DTColumn.AutoFit();
					

                });
        }
    }
}
