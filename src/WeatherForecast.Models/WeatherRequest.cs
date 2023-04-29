namespace WeatherForecast.Models;

public class WeatherRequest
{
    public int ForecastDays { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }

    public bool IsValid()
    {
        //TODO: implement validation
        return true;
    }
}
