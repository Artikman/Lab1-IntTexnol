using System.Collections.Generic;

namespace Lab1.processor
{
    public class Report
    {
        public ICollection<StudentReport> StudentReports { get; set; }
        public IDictionary<string, double> MeanSubjectMarks { get; set; } = new Dictionary<string, double>();
    }

    public class StudentReport
    {
        public Student Student { get; set; }
        public double MeanSubjectsMark { get; set; }
    }
}