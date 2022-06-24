using Serilog.Core;
using Serilog.Events;
using System.Diagnostics.Metrics;

namespace Serilog.Sinks.Metrics;

public class MetricsSink : ILogEventSink
{
  readonly Meter _meter;
  readonly Dictionary<LogEventLevel, Counter<int>> _counters;

  public string MeterName => _meter.Name;

  public MetricsSink(string meterName)
  {
    _meter = new Meter(meterName);

    Counter<int> CreateCounter(LogEventLevel level) =>
      _meter.CreateCounter<int>(
        name: $"logevents_{level.ToString().ToLowerInvariant()}",
        description: $"The total number of logevents with loglevel '{level}'");

    _counters = new(Enum.GetValues<LogEventLevel>()
      .Select(l => new KeyValuePair<LogEventLevel, Counter<int>>(l, CreateCounter(l))));
  }

  public void Emit(LogEvent logEvent) => _counters[logEvent.Level].Add(1);
}
