using System.Collections.Generic;
using System.Linq;

namespace Lab1.processor
{
    public class Processor : IProcessor
    {
        public Report BuildReport(ICollection<Student> students)
        {
            var report = new Report
            {
                StudentReports = students.Select(s => new StudentReport
                { Student = s, MeanSubjectsMark = s.LessonMarks.Values.Average() }).ToList()
            };

            var lessons = new Dictionary<string, ICollection<int>>();
            foreach (var student in students)
                foreach (var (lesson, mark) in student.LessonMarks)
                {
                    if (!lessons.ContainsKey(lesson)) lessons.Add(lesson, new List<int>());
                    lessons[lesson].Add(mark);
                }

            report.MeanSubjectMarks = lessons.ToDictionary(p => p.Key, p => p.Value.Average());

            return report;
        }
    }
}