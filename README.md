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


## License

This library is dual Licensed under the WTFPL or MIT.
That means you are free to choose either the WTFPL or the MIT License.The reason for giving you the option is that the even though the WTFPL is the more permissive license, the MIT License is better known.


### COPYRIGHT - WTFPL

Copyright © 2022 Martin Kramer (https://lostindetails.com)
This work is free. You can redistribute it and/or modify it under the
terms of the Do What The Fuck You Want To Public License, Version 2,
as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.


### COPYRIGHT - MIT

Copyright © 2022 Martin Kramer (https://lostindetails.com)

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
