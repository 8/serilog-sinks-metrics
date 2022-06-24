using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SampleApp;

public static partial class EntryPoint
{
  public class FizzBuzzLoggingService : BackgroundService
  {
    readonly ILogger<FizzBuzzLoggingService> _logger;

    public FizzBuzzLoggingService(ILogger<FizzBuzzLoggingService> logger)
    {
      this._logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation("Starting up...");

      for (int i = 1; i < Int32.MaxValue; i++)
      {
        await Task.Delay(TimeSpan.FromSeconds(1));

        if (i % 15 == 0)
          _logger.LogError("FizzBuzz");
        else if (i % 5 == 0)
          _logger.LogWarning("Buzz");
        else if (i % 3 == 0)
          _logger.LogInformation("Fizz");
        else
          _logger.LogDebug($"{i}");
      }
    }
  }
}