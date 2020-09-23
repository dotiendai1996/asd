using System;
using System.Collections.Generic;
using OfficeOpenXml;
using System.IO;
using Twilio.Rest.Api.V2010.Account;

namespace Hinnova.DataExporting.Excel.EpPlus
{
    public abstract class EpPlusExcelImporterBase<TEntity>
    {
        public List<TEntity> ProcessExcelFile(byte[] fileBytes, Func<ExcelWorksheet, int, TEntity> processExcelRow)
        {
            var entities = new List<TEntity>();

            using (var stream = new MemoryStream(fileBytes))
            {
                using (var excelPackage = new ExcelPackage(stream))
                {
                    foreach (var worksheet in excelPackage.Workbook.Worksheets)
                    {
                        var entitiesInWorksheet = ProcessWorksheet(worksheet, processExcelRow);

                        entities.AddRange(entitiesInWorksheet);
                    }
                }
            }

            return entities;
        }

        public List<TEntity> ProcessExcelFile(byte[] fileBytes, Func<ExcelWorksheet, Dictionary<string, int>, int, TEntity> processExcelRow)
        {
            var entities = new List<TEntity>();

            using (var stream = new MemoryStream(fileBytes))
            {
                using (var excelPackage = new ExcelPackage(stream))
                {
                    foreach (var worksheet in excelPackage.Workbook.Worksheets)
                    {
                        var columns = GetPropertyNameToColumnIndexTable(worksheet, worksheet.Dimension.Start.Row + 1, worksheet.Dimension.Start.Column);
                        var entitiesInWorksheet = ProcessWorksheet(worksheet, columns, processExcelRow);

                        entities.AddRange(entitiesInWorksheet);
                    }
                }
            }

            return entities;
        }
        public List<TEntity> ProcessExcelFileDynamicRow(byte[] fileBytes, Func<ExcelWorksheet, Dictionary<string, int>, int, TEntity> processExcelRow, int rowDataStart)
        {
            var entities = new List<TEntity>();

            using (var stream = new MemoryStream(fileBytes))
            {
                using (var excelPackage = new ExcelPackage(stream))
                {
                    foreach (var worksheet in excelPackage.Workbook.Worksheets)
                    {
                        var columns = GetPropertyNameToColumnIndexTable(worksheet, worksheet.Dimension.Start.Row + rowDataStart - 2, worksheet.Dimension.Start.Column);
                        var entitiesInWorksheet = ProcessWorksheetDynamicRow(worksheet, columns, processExcelRow, rowDataStart - 1);

                        entities.AddRange(entitiesInWorksheet);
                    }
                }
            }
            return entities;
        }
        public List<TEntity> ProcessExcelFileDynamicRowAndSheet(byte[] fileBytes, Func<ExcelWorksheet, Dictionary<string, int>, int, TEntity> processExcelRow, int rowDataStart, int[] sheetIndex)
        {
            var entities = new List<TEntity>();

            using (var stream = new MemoryStream(fileBytes))
            {
                using (var excelPackage = new ExcelPackage(stream))
                {
                    foreach (var worksheet in excelPackage.Workbook.Worksheets)
                    {
                        foreach (var i in sheetIndex)
                        {
                            if (worksheet.Index == i)
                            {
                                var columns = GetPropertyNameToColumnIndexTable(worksheet, worksheet.Dimension.Start.Row + rowDataStart - 2, worksheet.Dimension.Start.Column);
                                var entitiesInWorksheet = ProcessWorksheetDynamicRow(worksheet, columns, processExcelRow, rowDataStart - 1);
                                entities.AddRange(entitiesInWorksheet);
                            }
                        }
                    }
                }
            }
            return entities;
        }

        private List<TEntity> ProcessWorksheet(ExcelWorksheet worksheet, Func<ExcelWorksheet, int, TEntity> processExcelRow)
        {
            var entities = new List<TEntity>();

            for (var i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
            {
                try
                {
                    var entity = processExcelRow(worksheet, i);

                    if (entity != null)
                    {
                        entities.Add(entity);
                    }
                }
                catch (Exception)
                {
                    //ignore
                }
            }

            return entities;
        }

        private List<TEntity> ProcessWorksheet(ExcelWorksheet worksheet, Dictionary<string, int> columns, Func<ExcelWorksheet, Dictionary<string, int>, int, TEntity> processExcelRow)
        {
            var entities = new List<TEntity>();

            for (var i = worksheet.Dimension.Start.Row + 2; i <= worksheet.Dimension.End.Row; i++)
            {
                try
                {
                    var entity = processExcelRow(worksheet, columns, i);

                    if (entity != null)
                    {
                        entities.Add(entity);
                    }
                }
                catch (Exception)
                {
                    //ignore
                }
            }

            return entities;
        }
        private List<TEntity> ProcessWorksheetDynamicRow(ExcelWorksheet worksheet, Dictionary<string, int> columns, Func<ExcelWorksheet, Dictionary<string, int>, int, TEntity> processExcelRow, int rowDataStart)
        {
            var entities = new List<TEntity>();

            for (var i = worksheet.Dimension.Start.Row + rowDataStart; i <= worksheet.Dimension.End.Row; i++)
            {
                try
                {
                    var entity = processExcelRow(worksheet, columns, i);

                    if (entity != null)
                    {
                        entities.Add(entity);
                    }
                }
                catch (Exception)
                {
                    //ignore
                }
            }

            return entities;
        }

        private Dictionary<string, int> GetPropertyNameToColumnIndexTable(ExcelWorksheet worksheet, int rowIndex, int beginColumn)
        {
            Dictionary<string, int> propertiesTable = new Dictionary<string, int>();
            int columnsNumber = worksheet.Dimension.End.Column;

            for (int colIndex = beginColumn; colIndex <= columnsNumber; colIndex++)
            {
                var cellValue = worksheet.Cells[rowIndex, colIndex].Value;
                if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
                {
                    var key = cellValue.ToString().Trim();
                    if (!propertiesTable.ContainsKey(key))
                        propertiesTable.Add(key, colIndex);
                }
            }

            return propertiesTable;
        }
    }
}
