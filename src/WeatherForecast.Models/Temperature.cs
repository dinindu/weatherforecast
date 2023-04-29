
namespace WeatherForecast.Models
{
    public class Temperature
    {
        public double TemperatureCelsius { get; set; }
        public double TemperatureFahrenheit
        {
            get
            {
                return (TemperatureCelsius * 1.8f) + 32;
            }
        }
    }
}