
namespace WeatherForecast.Data
{
    public class JsonStore : IDataStore
    {
        public string FileName { get; set; } = Path.Combine("./data", $"weatherExport_{DateTime.Today.ToString("yyyyMMdd")}.json");
        public bool SaveForecast(WeatherExport forecast)
        {
            string json = JsonConvert.SerializeObject(forecast);
            File.WriteAllText(FileName, json);

            return true;
        }
    }
}