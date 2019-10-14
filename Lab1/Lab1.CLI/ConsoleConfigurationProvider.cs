using Lab1.config;

namespace Lab1.CLI
{
    public class ConsoleConfigurationProvider : IConfigurationProvider
    {
        public Config Config { get; set; }
    }
}