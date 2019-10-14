using CommandLine;
using Lab1.config;

namespace Lab1.CLI
{
    public class ConfigOptions
    {
        [Option('i', "input", Required = true, HelpText = "Input file path (D:\\input.csv)")]
        public string InputFileName { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output file path (D:\\output.json)")]
        public string OutputFileName { get; set; }

        [Option('t', "type", Required = false, Default = FileType.Excel, HelpText = "Output file type (Excel|Json)")]
        public FileType OutputFileType { get; set; }
    }
}