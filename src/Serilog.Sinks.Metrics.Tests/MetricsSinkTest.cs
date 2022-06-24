using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Sinks.Metrics.Tests
{
  public class MetricsSinkTest
  {
    [Fact]
    public void MetricsLogTest_Ctor() => new MetricsSink("MetricsSink");

    public static IEnumerable<object[]> LogEventLevels =>
      Enum.GetValues<LogEventLevel>().Select(level => new object[] { level });

    [Theory, MemberData(nameof(LogEventLevels))]
    public void MetricsSinkTest_Emit(LogEventLevel level)
    {
      // arrange
      var sink = new MetricsSink("MetricsSink");
      var messageTemplate = new MessageTemplate(Array.Empty<MessageTemplateToken>());
      var logEvent = new LogEvent(DateTimeOffset.FromUnixTimeSeconds(0), level, null, messageTemplate, Array.Empty<LogEventProperty>());

      // act
      sink.Emit(logEvent);
    }
  }
}
