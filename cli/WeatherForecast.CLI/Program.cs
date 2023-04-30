using CommandLine;
using WeatherForecast.Controllers;
using WeatherForecast.Data;
using WeatherForecast.Services;
using WeatherForecast.Models;
namespace WeatherForecast.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Parse the command line arguments
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    Console.WriteLine($"Getting Weather Forecast summary for {o.Days} day/s at Latitude: {o.Latitude} and Longitude: {o.Longitude}");

                    var forecastRequest = new ForecastRequest()
                    {
                        ForecastDays = o.Days,
                        Latitude = o.Latitude,
                        Longitude = o.Longitude
                    };

                    InitiateForecastSummary(forecastRequest).Wait();
                });
        }

        public static async Task<bool> InitiateForecastSummary(ForecastRequest forecastRequest)
        {
            bool success = false;
            string errorMessage = string.Empty;
            //TODO: Get configurations from a common config file
            var fileName = Path.Combine("./output/", $"weatherExport_{DateTime.Today.ToString("yyyyMMdd")}.json");
            try
            {
                var openMeteoService = new OpenMeteoService();
                openMeteoService.BaseURL = "https://api.open-meteo.com";

                var jsonStore = new JsonStore();
                jsonStore.FileName = fileName;

                var forecastController = new ForecastController(openMeteoService, jsonStore);

                (success, errorMessage) = await forecastController.ProcessForecastSummary(forecastRequest);
            }
            catch (Exception e)
            {
                success = false;
                Console.WriteLine($"Error Occurred: {e.Message}");
            }
            finally
            {
                if (success)
                {
                    Console.WriteLine($"Successfully exported Weather Forecast to the file: {fileName}");
                }
                else
                {
                    Console.WriteLine("Failed to export Weather Forecast!");
                    Console.WriteLine($"Error(s):");
                    Console.WriteLine(errorMessage);
                }
            }
            return success;
        }
    }
}
