using Core.Infrastructure;

namespace Weather.Infrastructure.DAL
{
    public interface IWeatherDAL : ICanAddEntity<Models.Weather>, ICanSaveChanges
    {
    }
}
