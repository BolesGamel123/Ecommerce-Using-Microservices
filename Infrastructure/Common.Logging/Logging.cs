using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
    public static class Logging
    {
        public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger => (context, loggerConfiguration) =>
        {
            Serilog.Debugging.SelfLog.Enable(Console.Error);

            var env = context.HostingEnvironment;
            loggerConfiguration.MinimumLevel.Information()
            .Enrich.FromLogContext()
            .Enrich.WithProperty("ApplicationName", env.ApplicationName)
            .Enrich.WithProperty("EnvironmentName", env.EnvironmentName)
            .Enrich.WithExceptionDetails()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Warning)
            .WriteTo.Console();

            if (context.HostingEnvironment.IsDevelopment())
            {
                loggerConfiguration.MinimumLevel.Override("Catalog", LogEventLevel.Debug);
                loggerConfiguration.MinimumLevel.Override("Basket", LogEventLevel.Debug);
                loggerConfiguration.MinimumLevel.Override("Discount", LogEventLevel.Debug);
                loggerConfiguration.MinimumLevel.Override("Ordering", LogEventLevel.Debug);

            }

            var elasticUrl = context.Configuration.GetValue<string>("ElasticConfiguration:Uri");
            if (!string.IsNullOrEmpty(elasticUrl))
            {
                var appName = env.ApplicationName?.ToLower().Replace(".", "-");
                loggerConfiguration.WriteTo.Elasticsearch(
                    new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri(elasticUrl))
                    {
                        AutoRegisterTemplate = true,
                        TemplateName = $"ecommerce-template-{appName}",
                        AutoRegisterTemplateVersion = Serilog.Sinks.Elasticsearch.AutoRegisterTemplateVersion.ESv8,
                        IndexFormat = $"ecommerce-logs-{appName}-{{0:yyyy.MM.dd}}",
                        MinimumLogEventLevel = LogEventLevel.Debug,
                        NumberOfShards = 1,
                        NumberOfReplicas = 0
                    });
            }
        };
           
    }
}
