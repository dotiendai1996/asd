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

namespace Hinnova.QLNS.Importing
{
    public class UngVienListExcelDataReader : EpPlusExcelImporterBase<UngVienImportDto>, IUngVienListExcelDataReader
    {
        private readonly ILocalizationSource _localizationSource;

        public UngVienListExcelDataReader(ILocalizationManager localizationManager)
        {
            _localizationSource = localizationManager.GetSource(HinnovaConsts.LocalizationSourceName);
        }

        public List<UngVienImportDto> GetUngVienFromExcel(byte[] fileBytes)
        {
            int[] sheetIndex = new int[] { 0 };
            return ProcessExcelFileDynamicRowAndSheet(fileBytes, ProcessExcelRow, 4, sheetIndex);
        }

        private UngVienImportDto ProcessExcelRow(ExcelWorksheet worksheet, Dictionary<string, int> columns, int row)
        {
            if (IsRowEmpty(worksheet, row))
            {
                return null;
            }
            var exceptionMessage = new StringBuilder();
            var dataImport = new UngVienImportDto();

            try
            {
                dataImport.MaUngVien = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.MaUngVien), columns, exceptionMessage);
                dataImport.HoVaTen = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.HoVaTen), columns, exceptionMessage);
                dataImport.ViTriUngTuyenCode = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.ViTriUngTuyenCode), columns, exceptionMessage);
                dataImport.KenhTuyenDungCode = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.KenhTuyenDungCode), columns, exceptionMessage);
                dataImport.GioiTinhCode = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.GioiTinhCode), columns, exceptionMessage);
                dataImport.NgaySinh = GetRequiredValueDatetimeFromRowOrNull(worksheet, row, nameof(dataImport.NgaySinh), columns, exceptionMessage);
                dataImport.SoCMND = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.SoCMND), columns, exceptionMessage);
                dataImport.NgayCap = GetRequiredValueDatetimeFromRowOrNull(worksheet, row, nameof(dataImport.NgayCap), columns, exceptionMessage);
                dataImport.NoiCap = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.NoiCap), columns, exceptionMessage);
                dataImport.TinhThanhName = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.TinhThanhName), columns, exceptionMessage);
                dataImport.TinhTrangHonNhanCode = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.TinhTrangHonNhanCode), columns, exceptionMessage);
                dataImport.TrinhDoDaoTaoCode = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.TrinhDoDaoTaoCode), columns, exceptionMessage);
                dataImport.TrinhDoVanHoa = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.TrinhDoVanHoa), columns, exceptionMessage);
                dataImport.NoiDaoTaoName = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.NoiDaoTaoID), columns, exceptionMessage);
                dataImport.Khoa = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.Khoa), columns, exceptionMessage);
                dataImport.ChuyenNganh = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.ChuyenNganh), columns, exceptionMessage);
                dataImport.XepLoaiCode = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.XepLoaiCode), columns, exceptionMessage);
                dataImport.NamTotNghiep = GetRequiredValueIntFromRowOrNull(worksheet, row, nameof(dataImport.NamTotNghiep), columns, exceptionMessage);
                dataImport.TrangThaiCode = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.TrangThaiCode), columns, exceptionMessage);
                dataImport.TienDoTuyenDungCode = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.TienDoTuyenDungCode), columns, exceptionMessage);
                dataImport.TepDinhKem = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.TepDinhKem), columns, exceptionMessage);
                dataImport.RECORD_STATUS = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.RECORD_STATUS), columns, exceptionMessage);
                dataImport.MARKER_ID = GetRequiredValueIntFromRowOrNull(worksheet, row, nameof(dataImport.MARKER_ID), columns, exceptionMessage);
                dataImport.AUTH_STATUS = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.AUTH_STATUS), columns, exceptionMessage);
                dataImport.CHECKER_ID = GetRequiredValueIntFromRowOrNull(worksheet, row, nameof(dataImport.CHECKER_ID), columns, exceptionMessage);
                dataImport.APPROVE_DT = GetRequiredValueDatetimeFromRowOrNull(worksheet, row, nameof(dataImport.APPROVE_DT), columns, exceptionMessage);
                dataImport.DienThoai = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.DienThoai), columns, exceptionMessage);
                dataImport.DiaChi = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.DiaChi), columns, exceptionMessage);
                dataImport.Email = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.Email), columns, exceptionMessage);
                dataImport.Time1 = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.Time1), columns, exceptionMessage);
                dataImport.Day1 = GetRequiredValueDatetimeFromRowOrNull(worksheet, row, nameof(dataImport.Day1), columns, exceptionMessage);
                dataImport.Time2 = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.Time2), columns, exceptionMessage);
                dataImport.Time3 = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.Time3), columns, exceptionMessage);
                dataImport.Day2 = GetRequiredValueDatetimeFromRowOrNull(worksheet, row, nameof(dataImport.Day2), columns, exceptionMessage);
                dataImport.Day3 = GetRequiredValueDatetimeFromRowOrNull(worksheet, row, nameof(dataImport.Day3), columns, exceptionMessage);
                dataImport.Note = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.Note), columns, exceptionMessage);
                dataImport.TenCTY = GetRequiredValueFromRowOrNull(worksheet, row, nameof(dataImport.TenCTY), columns, exceptionMessage);
            }
            catch (System.Exception exception)
            {
                dataImport.Exception = exception.Message;
            }

            return dataImport;
        }

        private int? GetRequiredValueIntFromRowOrNull(ExcelWorksheet worksheet, int row, string columnName, Dictionary<string, int> allcolumns, StringBuilder exceptionMessage)
        {
            if (allcolumns.ContainsKey(columnName))
            {
                var columnIndex = allcolumns[columnName];
                var cellValue = worksheet.Cells[row, columnIndex].Value;

                if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
                {
                    return int.Parse(cellValue.ToString());
                }

                exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName));
            }

            return null;
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
