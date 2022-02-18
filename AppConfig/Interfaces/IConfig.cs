namespace AppConfig.Interfaces
{
    public interface IConfig
    {
        string Key { get; }

        string CurrentWeatherUrl { get; }

        string CoordinatesUrl { get; }

        string ForecastUrl { get; }

        int ForecastHour { get; }

        int MinDays { get; }

        int MaxDays { get; }
    }
}
