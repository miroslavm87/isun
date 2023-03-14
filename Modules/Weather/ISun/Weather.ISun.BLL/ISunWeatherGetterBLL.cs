using Weather.Infrastructure.BLLs;
using Weather.Infrastructure.Contracts;

namespace Weather.ISun.BLL
{
    public class ISunWeatherGetterBLL : IWeatherGetterBLL
    {
        private readonly ISunWeatherIntegrationBLL _isunWeatherIntegrationBLL;

        public ISunWeatherGetterBLL(ISunWeatherIntegrationBLL isunWeatherIntegrationBLL)
        {
            _isunWeatherIntegrationBLL = isunWeatherIntegrationBLL;
        }

        public async Task<WeatherContract> GetWeatherForCity(string city)
        {
            return await _isunWeatherIntegrationBLL.GetWeatherForCity(city);
        }
    }
}