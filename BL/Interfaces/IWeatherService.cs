﻿using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherDto> GetWeatherByCityNameAsync(string cityName);

        Task<string> GetForecastByCityNameAsync(string cityName, int days);
    }
}
