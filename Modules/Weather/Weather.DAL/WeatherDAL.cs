using Core.DAL;
using Microsoft.EntityFrameworkCore;
using Weather.Infrastructure.DAL;

namespace Weather.DAL
{
    public class WeatherDAL : BaseDAL<Infrastructure.Models.Weather>, IWeatherDAL
    {
        public WeatherDAL(DbContext dBContext) : base(dBContext)
        {
        }
    }
}
