using System.Collections.Generic;
using System.Text;

namespace Lab1.processor
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public IDictionary<string, int> LessonMarks { get; set; } = new Dictionary<string, int>();
    }
}