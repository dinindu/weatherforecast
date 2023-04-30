namespace WeatherForecast.Models;

public class ForecastRequest
{
    public int ForecastDays { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public bool IsValid()
    {
        //TODO: implement validation
        return true;
    }
}
