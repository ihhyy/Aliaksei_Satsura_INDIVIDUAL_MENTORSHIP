using System;
using System.Collections.Generic;
using System.Text;

namespace Config.Interfaces
{
    public interface IConfig
    {
        public string Key { get; }

        public string CurrentWeatherUrk { get; }

        public string CoordinatesUrl { get; }

        public string ForecastUrl { get;  }
    }
}
