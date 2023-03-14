namespace Weather.Infrastructure.Contracts
{
    public record WeatherContract
    {
        public string? City { get; set; }
        public int? Temperature { get; set; }
        public int? Precipitation { get; set; }
        public double? WindSpeed { get; set; }
        public string? Summary { get; set; }
    }
}
