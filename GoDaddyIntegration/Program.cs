using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoDaddyIntegration.CommandLine;
using GoDaddyIntegration.Logging.Interfaces;
using GoDaddyIntegration.Logging.Services;

namespace GoDaddyIntegration
{
    class Program
    {
        private static readonly ILogger Logger = LoggingService.GetLoggingService();
        private static CommandLineProcessor _clp;
        static void Main(string[] args)
        {
            Logger.Info("Program startup");
            ConfigureCommandLIne(args);

            var appConfiguration = new AppConfig()
            {
                BaseUrl = System.Configuration.ConfigurationManager.AppSettings["godaddy:BaseUrl"],
                api_key = System.Configuration.ConfigurationManager.AppSettings["godaddy:apikey"],
                api_secret = System.Configuration.ConfigurationManager.AppSettings["godaddy:apisecret"],
            };

            var gdp = new GoDaddyProcessor(appConfiguration);
            var res = gdp.GetActiveDomains();

            WriteReport(res);

            Console.ReadLine();
        }

        private static void WriteReport(IEnumerable<DomainDto> domains)
        {
            var sb = new StringBuilder();
            sb.AppendLine("|Domain | Status| Expires| RenewAuto|");
            sb.AppendLine("| ---- | ---- | ---- | ---- |");
            foreach (var d in domains)
            {
                sb.AppendLine($"|{d.domain}|{d.status}|{d.expires}|{d.renewAuto}|");
            }

            System.IO.File.WriteAllText($"DomainReport-{DateTime.Now:yyyy-MM-dd_hh-mm-ss-tt}.md", sb.ToString());
        }

        private static void ConfigureCommandLIne(string[] args)
        {
            Logger.Info("Configure Command Line Settings");
            _clp = new CommandLineProcessor(args, Logger);
            Logger.Info("Debug Mode:{0}", _clp.Options.IsDebug);

        }
    }
}
