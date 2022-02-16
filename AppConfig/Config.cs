using AppConfig.Interfaces;
using System.Configuration;

namespace AppConfig
{
    public class Config : IConfig
    {
        public string Key => ConfigurationManager.AppSettings["APIKey"];

        public string CurrentWeatherUrl => ConfigurationManager.AppSettings["currentWeatherUrl"];

        public string CoordinatesUrl => ConfigurationManager.AppSettings["currentWeatherUrl"];

        public string ForecastUrl => ConfigurationManager.AppSettings["forecastUrl"];

        public string ForecastHour => ConfigurationManager.AppSettings["forecastHour"];

        public string MinDays => ConfigurationManager.AppSettings["minForecastDays"];

        public string MaxDays => ConfigurationManager.AppSettings["maxForecastDays"];
    }
}
