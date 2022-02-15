using Config.Interfaces;
using System.Configuration;

namespace Config
{
    public class Configuration : IConfig
    {
        public string Key => ConfigurationManager.AppSettings["APIKey"];

        public string CurrentWeatherUrk => ConfigurationManager.AppSettings["currentWeatherUrl"];

        public string CoordinatesUrl => ConfigurationManager.AppSettings["currentWeatherUrl"];

        public string ForecastUrl => ConfigurationManager.AppSettings["forecastUrl"];
    }
}
