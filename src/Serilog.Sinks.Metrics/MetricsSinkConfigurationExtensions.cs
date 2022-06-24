using Serilog.Configuration;

namespace Serilog.Sinks.Metrics;

public static class MetricsSinkConfigurationExtensions
{
  public static LoggerConfiguration Metrics(
    this LoggerSinkConfiguration sinkConfiguration,
    string meterName = nameof(MetricsSink)) =>
    sinkConfiguration.Sink(new MetricsSink(meterName));
}
