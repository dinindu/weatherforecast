using CommandLine;

namespace WeatherForecast.CLI
{
    public class Options
    {
        [Option('d', "days", Required = true, HelpText = "Number of days to get data for")]
        public int Days { get; set; }

        [Option('l', "latitude", Required = true, HelpText = "GPS Latitude")]
        public double Latitude { get; set; }

        [Option('L', "longitude", Required = true, HelpText = "GPS Longitude")]
        public double Longitude { get; set; }
    }
}