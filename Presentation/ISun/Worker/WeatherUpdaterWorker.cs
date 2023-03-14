using ISun.CommandLine;
using ISun.Service.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Weather.Infrastructure.BLLs;

namespace ISun.Service.Worker
{
    public class WeatherUpdaterWorker : IHostedService
    {
        private readonly ILogger<WeatherUpdaterWorker>? _logger;
        private readonly IWeatherUpdaterBLL _weatherUpdaterBLL;
        private readonly WeatherUpdaterOptions _weatherUpdaterOptions;
        private readonly CitiesArgumentExtractor _citiesArgumentExtractor;
        private bool _isRunning;
        public WeatherUpdaterWorker(ILogger<WeatherUpdaterWorker> logger, CitiesArgumentExtractor citiesArgumentExtractor, IWeatherUpdaterBLL weatherUpdaterBLL, IOptions<WeatherUpdaterOptions> weatherUpdaterOptions)
        {
            _logger = logger;
            _citiesArgumentExtractor = citiesArgumentExtractor;
            _weatherUpdaterBLL = weatherUpdaterBLL;
            _weatherUpdaterOptions = weatherUpdaterOptions.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (_citiesArgumentExtractor.Cities.Count == 0)
            {
                _logger.LogWarning("Weather updater exiting, because cities not found");
                return;
            }
            _isRunning = true;
            while (_isRunning)
            {
                await _weatherUpdaterBLL.UpdateWeathersForCities(_citiesArgumentExtractor.Cities.ToArray());
                await Task.Delay(_weatherUpdaterOptions.UpdatePeriodMS, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _isRunning = false;
            return Task.CompletedTask;
        }
    }
}
