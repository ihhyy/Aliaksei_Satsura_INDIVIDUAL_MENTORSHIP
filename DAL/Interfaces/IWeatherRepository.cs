using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IWeatherRepository
    {
        Task<Weather> GetWeatherByCityNameAsync(string cityName);
    }
}
