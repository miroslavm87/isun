using Microsoft.EntityFrameworkCore;
using Weather.Infrastructure.DAL;

namespace ISunDatabase
{
    public class ISunDbContext : DbContext, IWeatherDbContext
    {
        public DbSet<Weather.Infrastructure.Models.Weather> Weathers { get; set; }

        public ISunDbContext(DbContextOptions<ISunDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}