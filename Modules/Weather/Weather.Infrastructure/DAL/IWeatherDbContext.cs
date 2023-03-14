using Microsoft.EntityFrameworkCore;

namespace Weather.Infrastructure.DAL
{
    public interface IWeatherDbContext
    {
        public DbSet<Models.Weather> Weathers { get; set; }
    }
}
