using Microsoft.Extensions.Logging;
using Weather.Infrastructure.BLLs;
using Weather.Infrastructure.DAL;

namespace Weather.BLL
{
    public class WeatherUpdaterBLL : IWeatherUpdaterBLL
    {
        private readonly IWeatherGetterBLL _weatherGetterBLL;
        private readonly IWeatherDAL _weatherDal;
        private readonly ILogger _logger;

        public WeatherUpdaterBLL(IWeatherDAL weatherDAL, IWeatherGetterBLL weatherGetterBLL, ILogger<WeatherUpdaterBLL> logger)
        {
            _weatherDal = weatherDAL;
            _logger = logger;
            _weatherGetterBLL = weatherGetterBLL;
        }
        public async Task UpdateWeathersForCities(string[] cities)
        {
            if (cities == null || cities.Length == 0)
            {
                _logger.LogInformation("Cities list is empty");
                return;
            }

            _logger.LogInformation("Start saving new weather information for {cities}", string.Join(',', cities));


            foreach (var city in cities)
            {
                try
                {
                    var weather = await _weatherGetterBLL.GetWeatherForCity(city);
                    _logger.LogInformation($"Received weather for city {city} : {@weather}", city, weather);
                    await _weatherDal.AddEntityAsync(new()
                    {
                        City = weather.City,
                        Temperature = weather.Temperature,
                        Precipitation = weather.Precipitation,
                        Summary = weather.Summary,
                        WindSpeed = weather.WindSpeed,
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Failed update weather for city {city}. Application continue working.");
                }
            }
            await _weatherDal.SaveChangesAsync();
        }
    }
}
