namespace Lab1.processor.writer
{
    public interface IWriter<in T>
    {
        void Write(T report);
    }
}