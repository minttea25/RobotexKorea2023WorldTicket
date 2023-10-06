using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace WorldTicket.Data
{
    public class ExcelWriter
    {
        public static void WriteData(Dictionary<Sports, List<TeamData>> teams)
        {
            int rows = 1;

            using (var package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(DateTime.Now.ToString("yyyy-MM-dd.HH:mm::ss"));

                foreach (var kv in teams)
                {
                    string sportName = Enum.GetName(typeof(Sports), kv.Key);

                    worksheet.Cells[rows, 1].Value = sportName;
                    rows++;

                    // Write data from list1
                    WriteDataToWorksheet(worksheet, kv.Value, rows);
                    rows += kv.Value.Count;

                    // Add an empty line
                    worksheet.Cells[worksheet.Dimension.End.Row + 1, 1].Value = string.Empty;
                    worksheet.Cells[worksheet.Dimension.End.Row + 1, 1].Value = string.Empty;

                    rows += 2;
                }

                // Save the Excel package to a file
                File.WriteAllBytes($"{DateTime.Now:yyyy-MM-dd_HH_mm_ss}.xlsx", package.GetAsByteArray());
            }
        }

        static void WriteDataToWorksheet(ExcelWorksheet worksheet, List<TeamData> list, int startRow)
        {
            for (int i = 0; i < list.Count; i++)
            {
                worksheet.Cells[startRow + i, 1].Value = list[i].Organization;
                worksheet.Cells[startRow + i, 2].Value = list[i].Sport;
                worksheet.Cells[startRow + i, 3].Value = list[i].TeamNo;
                worksheet.Cells[startRow + i, 4].Value = list[i].TeamName;
            }
        }
    }
}
