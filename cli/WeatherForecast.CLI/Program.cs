using CommandLine;
using WeatherForecast.Controllers;
using WeatherForecast.Data;
using WeatherForecast.Services;
using WeatherForecast.Models;
using Serilog;
using Serilog.Core;

namespace WeatherForecast.CLI
{
    class Program
    {
        private static readonly Logger _logger;

        static Program()
        {
            //Set up logging
            _logger = new LoggerConfiguration()
           .MinimumLevel.Information()
           .WriteTo.File("./logs/log.txt",
               rollingInterval: RollingInterval.Day,
               rollOnFileSizeLimit: true)
           .CreateLogger();
        }

        static void Main(string[] args)
        {
            // Parse the command line arguments
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    string initialMessage = $"Getting Weather Forecast summary for {o.Days} day/s at Latitude: {o.Latitude} and Longitude: {o.Longitude}";
                    _logger.Information(initialMessage);

                    var forecastRequest = new ForecastRequest()
                    {
                        ForecastDays = o.Days,
                        Latitude = o.Latitude,
                        Longitude = o.Longitude
                    };

                    InitiateForecastSummary(forecastRequest).Wait();
                });

            _logger.Dispose();
        }

        static async Task<bool> InitiateForecastSummary(ForecastRequest forecastRequest)
        {
            bool success = false;
            string errorMessage = string.Empty;

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
                errorMessage = e.Message;
            }
            finally
            {
                if (success)
                {
                    string successMessage = $"Successfully exported Weather Forecast to the file: {fileName}";
                    Console.WriteLine(successMessage);
                    _logger.Information(successMessage);
                }
                else
                {
                    Console.WriteLine("Failed to export Weather Forecast!");
                    Console.WriteLine($"Error(s):");
                    Console.WriteLine(errorMessage);
                    _logger.Error(errorMessage);
                }
            }
            return success;
        }
    }
}
