using System;
using System.IO;
using Newtonsoft.Json;
using Lab1.config;

namespace Lab1.processor.writer
{
    public class JsonWriter : IWriter<Report>
    {
        private readonly Config _config;

        public JsonWriter(IConfigurationProvider configurationProvider)
        {
            _config = configurationProvider.Config;
        }

        public void Write(Report report)
        {
            if (!File.Exists(_config.OutputFileName)) throw new FileNotFoundException("Output file does not exist");
            if (Path.GetExtension(_config.OutputFileName) != Constants.Json)
                throw new FormatException($"Wrong output file format. Expected {Constants.Json}");

            var json = JsonConvert.SerializeObject(report);

            using (var sw = new StreamWriter(_config.OutputFileName))
            {
                sw.WriteLine(json);
            }
        }
    }
}