using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Lab1.config;

namespace Lab1.processor.reader
{
    public class CsvStudentsReader : IReader<Student>
    {
        private readonly Config _config;

        public CsvStudentsReader(IConfigurationProvider configurationProvider)
        {
            _config = configurationProvider.Config;
        }

        public ICollection<Student> ReadStudents()
        {
            if (!File.Exists(_config.InputFileName)) throw new FileNotFoundException("Input file does not exist");
            if (Path.GetExtension(_config.InputFileName) != Constants.Csv)
                throw new FormatException($"Wrong input file format. Expected {Constants.Csv}");

            var records = new List<Student>();

            using (var reader = new StreamReader(_config.InputFileName))
            using (var csv = new CsvReader(reader))
            {
                csv.Read();
                csv.ReadHeader();
                var lessons = new List<string>(csv.Context.HeaderRecord)
                    .Where(h => h != Constants.FirstName && h != Constants.LastName && h != Constants.MiddleName)
                    .ToList();

                while (csv.Read())
                {
                    var student = new Student
                    {
                        FirstName = csv.GetField(Constants.FirstName),
                        LastName = csv.GetField(Constants.LastName),
                        MiddleName = csv.GetField(Constants.MiddleName)
                    };


                    foreach (var lesson in lessons)
                    {
                        int.TryParse(csv.GetField(lesson), out var mark);
                        student.LessonMarks.Add(lesson, mark);
                    }

                    records.Add(student);
                }
            }

            return records;
        }
    }
}