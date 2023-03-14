using ISunWeatherProvider;
using System.Net.Http.Headers;
using Weather.Infrastructure.Contracts;
using Weather.ISun.BLL.Options;

namespace Weather.ISun.BLL
{
    public class ISunWeatherIntegrationBLL
    {
        private readonly ISunWeatherIntegrationOptions _sunWeatherIntegrationOptions;
        private string _authorizationToken;
        private readonly HttpClient _httpClient;
        private readonly ISunWeatherClient _isunWeatherClient;

        public ISunWeatherIntegrationBLL(HttpClient httpClient, ISunWeatherIntegrationOptions isunWeatherIntegrationOptions)
        {
            if (string.IsNullOrEmpty(isunWeatherIntegrationOptions.Host))
                throw new ArgumentException("Empty argument", nameof(isunWeatherIntegrationOptions.Host));
            if (string.IsNullOrEmpty(isunWeatherIntegrationOptions.Username))
                throw new ArgumentException("Empty argument", nameof(isunWeatherIntegrationOptions.Username));
            if (string.IsNullOrEmpty(isunWeatherIntegrationOptions.Password))
                throw new ArgumentException("Empty argument", nameof(isunWeatherIntegrationOptions.Password));

            _httpClient = httpClient;
            _sunWeatherIntegrationOptions = isunWeatherIntegrationOptions;
            _isunWeatherClient = new(isunWeatherIntegrationOptions.Host, _httpClient);
        }

        public async Task<WeatherContract> GetWeatherForCity(string city)
        {
            var token = await GetAuthorizationToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

            var weatherResult = await _isunWeatherClient.WeathersAsync(city);
            return new WeatherContract()
            {
                City = weatherResult.City,
                Temperature = weatherResult.Temperature,
                Precipitation = weatherResult.Precipitation,
                Summary = weatherResult.Summary,
                WindSpeed = weatherResult.WindSpeed
            };
        }

        public async Task<string> GetAuthorizationToken()
        {
            if (!string.IsNullOrEmpty(_authorizationToken))
            {
                return _authorizationToken;
            }

            var result = await _isunWeatherClient.AuthorizeAsync(new()
            {
                Username = _sunWeatherIntegrationOptions.Username,
                Password = _sunWeatherIntegrationOptions.Password,
            });
            return result.Token;
        }
    }
}
