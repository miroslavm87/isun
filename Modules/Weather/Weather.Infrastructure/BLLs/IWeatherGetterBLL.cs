using Weather.Infrastructure.Contracts;

namespace Weather.Infrastructure.BLLs
{
    public interface IWeatherGetterBLL
    {
        public Task<WeatherContract> GetWeatherForCity(string city);
    }
}
