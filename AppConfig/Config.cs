using AppConfig.Interfaces;
using System.Collections.Specialized;
using System.Configuration;

namespace AppConfig
{
    public class Config : IConfig
    {
        public readonly NameValueCollection Configuration;
        //public string Key => ConfigurationManager.AppSettings["APIKey"];

        //public string CurrentWeatherUrl => ConfigurationManager.AppSettings["currentWeatherUrl"];

        //public string CoordinatesUrl => ConfigurationManager.AppSettings["coordinatesUrl"];

        //public string ForecastUrl => ConfigurationManager.AppSettings["forecastUrl"];

        //public string ForecastHour => ConfigurationManager.AppSettings["forecastHour"];

        //public string MinDays => ConfigurationManager.AppSettings["minForecastDays"];

        //public string MaxDays => ConfigurationManager.AppSettings["maxForecastDays"];
        public string Key { get; set; }
        public string CurrentWeatherUrl { get; set; }
        public string CoordinatesUrl { get; set; }
        public string ForecastUrl { get; set; }
        public string ForecastHour { get; set; }
        public string MinDays { get; set; }
        public string MaxDays { get; set; }

        public Config()
        {
            var configuration = Configuration.InitConfig();

            Key = configuration["APIKey"];
            CurrentWeatherUrl = configuration["currentWeatherUrl"];
            CoordinatesUrl = configuration["coordinatesUrl"];
            ForecastUrl = configuration["forecastUrl"];
            ForecastHour = configuration["forecastHour"];
            MinDays = configuration["minForecastDays"];
            MaxDays = configuration["maxForecastDays"];
        }

    }
}
