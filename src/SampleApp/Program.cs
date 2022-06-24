using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using Serilog;
using Serilog.Sinks.Metrics;

namespace SampleApp;

public static partial class EntryPoint
{
  public static void Main()
  {
    var endpoint = "http://localhost:9464";

    using var meterProvider = Sdk.CreateMeterProviderBuilder()
       .AddPrometheusExporter(options => {
         options.HttpListenerPrefixes = new[] { endpoint };
         options.StartHttpListener = true;
       })
       .AddMeter("MetricsSink") // <- default name of the Meter created by the sink
       .Build();

    using var logger = new LoggerConfiguration()
      .MinimumLevel.Debug()
      .WriteTo.Console()
      .WriteTo.Metrics()
      .CreateLogger();

    logger.Information($"Hosting metrics endpoint is available at: '{endpoint}/metrics'");

    using var app = Host.CreateDefaultBuilder()
      .UseSerilog(logger)
      .ConfigureServices(services => services.AddHostedService<FizzBuzzLoggingService>())
      .Build();

    app.Run();
  }
}