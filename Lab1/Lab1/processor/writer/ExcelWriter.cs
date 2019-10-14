using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using OfficeOpenXml;
using Lab1.config;

namespace Lab1.processor.writer
{
    public class ExcelWriter : IWriter<Report>
    {
        private readonly Config _config;

        public ExcelWriter(IConfigurationProvider configurationProvider)
        {
            _config = configurationProvider.Config;
        }

        public void Write(Report report)
        {
            if (!File.Exists(_config.OutputFileName)) throw new FileNotFoundException("Output file does not exist");
            if (Path.GetExtension(_config.OutputFileName) != Constants.Excel)
                throw new FormatException($"Wrong output file format. Expected {Constants.Excel}");

            var table = new DataTable();

            table.Columns.Add(new DataColumn(Constants.FirstName, typeof(string)));
            table.Columns.Add(new DataColumn(Constants.LastName, typeof(string)));
            table.Columns.Add(new DataColumn(Constants.MiddleName, typeof(string)));

            var lessonIds = new Dictionary<string, int>();
            var curId = 3;
            foreach (var lesson in report.MeanSubjectMarks.Keys)
            {
                lessonIds.Add(lesson, curId++);
                table.Columns.Add(new DataColumn(lesson, typeof(double)));
            }

            table.Columns.Add(new DataColumn(Constants.Average, typeof(double)));

            foreach (var studentReport in report.StudentReports)
            {
                var row = table.NewRow();
                row[0] = studentReport.Student.FirstName;
                row[1] = studentReport.Student.LastName;
                row[2] = studentReport.Student.MiddleName;

                var cur = 3;
                foreach (var mark in studentReport.Student.LessonMarks) row[cur++] = mark.Value;

                row[cur] = studentReport.MeanSubjectsMark;

                table.Rows.Add(row);
            }

            var totalRow = table.NewRow();
            foreach (var (lesson, mark) in report.MeanSubjectMarks) totalRow[lessonIds[lesson]] = mark;

            table.Rows.Add(totalRow);

            SaveExcel(table);
        }

        private void SaveExcel(DataTable table)
        {
            var fi = new FileInfo(_config.OutputFileName);
            if (fi.Exists) fi.Delete();

            using (var pck = new ExcelPackage(fi))
            {
                var worksheet = pck.Workbook.Worksheets.Add("Students");
                worksheet.Cells.LoadFromDataTable(table, true);
                pck.Save();
            }
        }
    }
}