using System;
using Lab1.config;

namespace Lab1.processor.writer
{
    public interface IWriterResolver
    {
        void RegisterWriter<T>(FileType fileType, IWriter<T> writer);
        IWriter<T> ResolveWriter<T>(FileType fileType);
    }
}