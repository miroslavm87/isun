using Core.Infrastructure.Models;

namespace Weather.Infrastructure.Models
{
    public class Weather : IEntityWithId
    {
        public int Id { get; set; }
        public string? City { get; set; }
        public int? Temperature { get; set; }
        public int? Precipitation { get; set; }
        public double? WindSpeed { get; set; }
        public string? Summary { get; set; }
    }
}
