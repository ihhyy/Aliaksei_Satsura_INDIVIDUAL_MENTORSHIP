using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConfig.Interfaces
{
    public interface IConfig
    {
        string Key { get; }

        string CurrentWeatherUrl { get; }

        string CoordinatesUrl { get; }

        string ForecastUrl { get; }

        string ForecastHour { get; }

        string MinDays { get; }

        string MaxDays { get; }
    }
}
