# Serilog.Sinks.Metrics

A Serilog sink that creates a single `System.Diagnostic.Metrics.Meter` and `System.Diagnostic.Metrics.Counter<int>`s for each `LogEventLevel` and increments them whenever a `LogEvent` of that level occurs.


## Getting Started

To use the metrics sink, first install the NuGet package:

```
dotnet add package Serilog.Sinks.Metrics
```

Then enable the sink using `WriteTo.Metrics()`:

```cs
Log.Logger = new LoggerConfiguration()
  .WriteTo.Metrics()
  .CreateLogger();

Log.LogInformation("Hello, world!");
```


## Exporting the Metrics

This sink creates the meter and the counters, but it **does not** host an endpoint - this must be handled by your application eg. by using `OpenTelemetry.Sdk.CreateMeterProviderBuilder()` and adding the name of Meter (= "MetricsSink") created by the sink .


```cs
using var meterProvider = Sdk.CreateMeterProviderBuilder()
    .AddPrometheusExporter(options => {
      options.HttpListenerPrefixes = new[] { endpoint };
      options.StartHttpListener = true;
    })
    .AddMeter("MetricsSink") // <- default name of the Meter created by the sink
    .Build();
```


## Sample Application

There is a sample application in the repository that uses the sink and uses the sink and exports the metrics using a MeterProvider on `http://localhost:9464/metrics` with following output:

```
# HELP logevents_debug The total number of logevents with loglevel 'Debug'
# TYPE logevents_debug counter
logevents_debug 21 1656092296802

# HELP logevents_information The total number of logevents with loglevel 'Information'
# TYPE logevents_information counter
logevents_information 15 1656092296802

# HELP logevents_warning The total number of logevents with loglevel 'Warning'
# TYPE logevents_warning counter
logevents_warning 5 1656092296802

# HELP logevents_error The total number of logevents with loglevel 'Error'
# TYPE logevents_error counter
logevents_error 2 1656092296802
```
