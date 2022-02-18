using AppConfig.Interfaces;
using System.Collections.Specialized;
using System.Configuration;

namespace AppConfig
{
    public class Config : IConfig
    {
        public readonly NameValueCollection Configuration;
        public string Key { get; set; }
        public string CurrentWeatherUrl { get; set; }
        public string CoordinatesUrl { get; set; }
        public string ForecastUrl { get; set; }
        public int ForecastHour { get; set; }
        public int MinDays { get; set; }
        public int MaxDays { get; set; }

        public Config()
        {
            var configuration = Configuration.InitConfig();

            Key = configuration["APIKey"];
            CurrentWeatherUrl = configuration["currentWeatherUrl"];
            CoordinatesUrl = configuration["coordinatesUrl"];
            ForecastUrl = configuration["forecastUrl"];
            ForecastHour = int.Parse(configuration["forecastHour"]);
            MinDays = int.Parse(configuration["minForecastDays"]);
            MaxDays = int.Parse(configuration["maxForecastDays"]);
        }

    }
}
