using System;
using System.IO;
using CommandLine;
using Lab1.config;
using Lab1.processor;
using Lab1.processor.reader;
using Lab1.processor.writer;

namespace Lab1.CLI
{
    public class Program
    {
        private static readonly IWriterResolver WriterResolver = new ReportWriterResolver();
		
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<ConfigOptions>(args)
                .WithParsed(RunProcessor);
        }

        private static void RegisterWriters(IConfigurationProvider configurationProvider)
        {
            WriterResolver.RegisterWriter(FileType.Excel, new ExcelWriter(configurationProvider));
            WriterResolver.RegisterWriter(FileType.Json, new JsonWriter(configurationProvider));
        }

        private static void RunProcessor(ConfigOptions opts)
        {
            var config = new Config
            {
                InputFileName = opts.InputFileName,
                OutputFileName = opts.OutputFileName,
                OutputFileType = opts.OutputFileType
            };

            var configurationProvider = new ConsoleConfigurationProvider { Config = config };
            RegisterWriters(configurationProvider);

            var processor = new Processor();
            var reader = new CsvStudentsReader(configurationProvider);

            try
            {
                var students = reader.ReadStudents();

                var report = processor.BuildReport(students);
                var writer = WriterResolver.ResolveWriter<Report>(config.OutputFileType);

                writer.Write(report);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"File not found!!! {e.Message}");
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Wrong file format!!! {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}