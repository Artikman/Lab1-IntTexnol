using System.Collections.Generic;

namespace Lab1.processor.reader
{
    public interface IReader<T>
    {
        ICollection<T> ReadStudents();
    }
}