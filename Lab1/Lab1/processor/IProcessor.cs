using System.Collections.Generic;

namespace Lab1.processor
{
    public interface IProcessor
    {
        Report BuildReport(ICollection<Student> students);
    }
}