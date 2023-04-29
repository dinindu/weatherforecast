using CommandLine;
namespace WeatherForecast.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    Console.WriteLine($"Getting weather forecast for {o.Days} day/s at latitude: {o.Latitude} and longitude: {o.Longitude}");
                });
        }
    }
}
