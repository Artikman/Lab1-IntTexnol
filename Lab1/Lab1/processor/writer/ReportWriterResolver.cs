using System.Collections.Generic;
using Lab1.config;

namespace Lab1.processor.writer
{
    public class ReportWriterResolver : IWriterResolver
    {
        private readonly IDictionary<FileType, IWriter<Report>> _writers = new Dictionary<FileType, IWriter<Report>>();

        void IWriterResolver.RegisterWriter<T>(FileType fileType, IWriter<T> writer)
        {
            if (!(writer is IWriter<Report> reportWriter)) return;

            _writers.Add(fileType, reportWriter);
        }

        public IWriter<T> ResolveWriter<T>(FileType fileType)
        {
            if (typeof(T) != typeof(Report)) return null;
            if (!_writers.ContainsKey(fileType)) return null;

            return _writers[fileType] as IWriter<T>;
        }
    }
}