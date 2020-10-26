using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Localization;
using Abp.Localization.Sources;
using OfficeOpenXml;
using Hinnova.Authorization.Users.Importing.Dto;
using Hinnova.DataExporting.Excel.EpPlus;
using Hinnova.QLNS.Dtos;
using Hinnova.QLNS.Importing;
using System;

namespace Hinnova.Authorization.Users.Importing
{
    public class HoSoListExcelDataReader : EpPlusExcelImporterBase<HoSoImportDto>, IHoSoListExcelDataReader
    {
        private readonly ILocalizationSource _localizationSource;
        

        public HoSoListExcelDataReader(ILocalizationManager localizationManager)
        {
            _localizationSource = localizationManager.GetSource(HinnovaConsts.LocalizationSourceName);
        }

        public List<HoSoImportDto> GetHoSoFromExcel(byte[] fileBytes)
        {
            return ProcessExcelFile(fileBytes, ProcessExcelRow);
        }

        private HoSoImportDto ProcessExcelRow(ExcelWorksheet worksheet, Dictionary<string, int> columns, int row)
        {
            if (IsRowEmpty(worksheet, row))
            {
                return null;
            }

            var exceptionMessage = new StringBuilder();
            var dataImport = new HoSoImportDto();

            try
            {
                dataImport.MaChamCong = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.MaChamCong), columns, exceptionMessage);
                dataImport.MaNhanVien = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.MaNhanVien), columns, exceptionMessage);
                dataImport.HoVaTen = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.HoVaTen), columns, exceptionMessage);
                dataImport.ChucDanh = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.ChucDanh), columns, exceptionMessage);
                dataImport.DonViCongTacName = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.DonViCongTacName), columns, exceptionMessage);
                dataImport.HopDongHienTai = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.HopDongHienTai), columns, exceptionMessage);
                dataImport.NgaySinh = GetRequiredValueDatetimeFromRowOrNull(worksheet, row, nameof(dataImport.NgaySinh), columns, exceptionMessage);
                dataImport.GioiTinhCode = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.GioiTinhCode), columns, exceptionMessage);
                dataImport.TrinhDoDaoTaoCode = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.TrinhDoDaoTaoCode), columns, exceptionMessage);
                dataImport.NoiDaoTaoName = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.NoiDaoTaoName), columns, exceptionMessage);
                dataImport.ChuyenNganh = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.ChuyenNganh), columns, exceptionMessage);
                dataImport.XepLoaiCode = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.XepLoaiCode), columns, exceptionMessage);
                dataImport.SoCMND = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.SoCMND), columns, exceptionMessage);
                dataImport.NgayCap = GetRequiredValueDatetimeFromRowOrNull(worksheet, row, nameof(dataImport.NgayCap), columns, exceptionMessage);
                dataImport.NoiCap = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.NoiCap), columns, exceptionMessage);
                dataImport.MSTCaNhan = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.MSTCaNhan), columns, exceptionMessage);
                dataImport.NguyenQuan = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.NguyenQuan), columns, exceptionMessage);
                dataImport.DiaChiHKTT = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.DiaChiHKTT), columns, exceptionMessage);
                dataImport.DtDiDong = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.DtDiDong), columns, exceptionMessage);
                dataImport.Skype = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.Skype), columns, exceptionMessage);
                dataImport.EmailCaNhan = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.EmailCaNhan), columns, exceptionMessage);
                dataImport.Facebook = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.Facebook), columns, exceptionMessage);
                dataImport.NgayKyHD = GetRequiredValueDatetimeFromRowOrNull(worksheet, row, nameof(dataImport.NgayKyHD), columns, exceptionMessage);
                dataImport.NgayKYHDCTV = GetRequiredValueDatetimeFromRowOrNull(worksheet, row, nameof(dataImport.NgayKYHDCTV), columns, exceptionMessage);
                dataImport.NgayKyHDTV = GetRequiredValueDatetimeFromRowOrNull(worksheet, row, nameof(dataImport.NgayKyHDTV), columns, exceptionMessage);
                dataImport.NgayKyHD12TH = GetRequiredValueDatetimeFromRowOrNull(worksheet, row, nameof(dataImport.NgayKyHD12TH), columns, exceptionMessage);
                dataImport.NgayKyHD36TH = GetRequiredValueDatetimeFromRowOrNull(worksheet, row, nameof(dataImport.NgayKyHD36TH), columns, exceptionMessage);
                dataImport.NgayHetHan = GetRequiredValueDatetimeFromRowOrNull(worksheet, row, nameof(dataImport.NgayHetHan), columns, exceptionMessage);
                dataImport.TkNganHang = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.TkNganHang), columns, exceptionMessage);
                dataImport.NganHangCode = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.NganHangCode), columns, exceptionMessage);
                dataImport.ChiNhanh = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.ChiNhanh), columns, exceptionMessage);
                dataImport.SoHD = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.SoHD), columns, exceptionMessage);
                dataImport.LuongCoBan = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.LuongCoBan), columns, exceptionMessage);
                dataImport.DVT = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.DVT), columns, exceptionMessage);
                dataImport.ChoNgoi = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.ChoNgoi), columns, exceptionMessage);
                dataImport.TenCty = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.TenCty), columns, exceptionMessage);
                //dataImport.GhiChu = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.GhiChu), columns, exceptionMessage);
                //dataImport.ThoiViec = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.ThoiViec), columns, exceptionMessage);
            }
            catch (System.Exception exception)
            {
                dataImport.Exception = exception.Message;
            }

            return dataImport;
        }

        private string GetRequiredValueFromRowOrNull(ExcelWorksheet worksheet, int row, string columnName, Dictionary<string, int> allcolumns, StringBuilder exceptionMessage)
        {
            if (allcolumns.ContainsKey(columnName))
            {
                var columnIndex = allcolumns[columnName];
                var cellValue = worksheet.Cells[row, columnIndex].Value;

                if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
                {
                    return cellValue.ToString();
                }

                exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName));
            }

            return string.Empty;
        }

        private DateTime? GetRequiredValueDatetimeFromRowOrNull(ExcelWorksheet worksheet, int row, string columnName, Dictionary<string, int> allcolumns, StringBuilder exceptionMessage)
        {
            if (allcolumns.ContainsKey(columnName))
            {
                var columnIndex = allcolumns[columnName];
                var cellValue = worksheet.Cells[row, columnIndex].Value;

                if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
                {
                    return cellValue.ToString().ToDatetimeFormat();
                }

                exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName));
            }

            return null;
        }

        private DateTime GetRequiredValueDatetimeFromRow(ExcelWorksheet worksheet, int row, string columnName, Dictionary<string, int> allcolumns, StringBuilder exceptionMessage)
        {
            if (allcolumns.ContainsKey(columnName))
            {
                var columnIndex = allcolumns[columnName];
                var cellValue = worksheet.Cells[row, columnIndex].Value;

                if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
                {
                    return cellValue.ToString().ToDatetimeFormat().Value;
                }

                exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName));
            }

            return DateTime.MinValue;
        }

        private string GetRequiredValueFromRowOrNull(ExcelWorksheet worksheet, int row, int column, string columnName, StringBuilder exceptionMessage)
        {
            var cellValue = worksheet.Cells[row, column].Value;

            if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
            {
                return cellValue.ToString();
            }

            exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName));
            return null;
        }

        private string[] GetAssignedRoleNamesFromRow(ExcelWorksheet worksheet, int row, int column)
        {
            var cellValue = worksheet.Cells[row, column].Value;

            if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString()))
            {
                return new string[0];
            }

            return cellValue.ToString().Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()).ToArray();
        }

        private string GetLocalizedExceptionMessagePart(string parameter)
        {
            return _localizationSource.GetString("{0}IsInvalid", _localizationSource.GetString(parameter)) + "; ";
        }

        private bool IsRowEmpty(ExcelWorksheet worksheet, int row)
        {
            return worksheet.Cells[row, 1].Value == null || string.IsNullOrWhiteSpace(worksheet.Cells[row, 1].Value.ToString());
        }
    }
}
