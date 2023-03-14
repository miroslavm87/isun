namespace Weather.Infrastructure.BLLs;
public interface IWeatherUpdaterBLL
{
    public Task UpdateWeathersForCities(string[] cities);
}
