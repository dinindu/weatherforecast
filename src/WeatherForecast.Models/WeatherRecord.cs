namespace WeatherForecast.Models
{
    public class WeatherRecord
    {
        public Temperature LowestTemperature { get; set; } = new Temperature();
        public Temperature HighestTemperature { get; set; } = new Temperature();

        public double Snowfall { get; set; }
    }
}