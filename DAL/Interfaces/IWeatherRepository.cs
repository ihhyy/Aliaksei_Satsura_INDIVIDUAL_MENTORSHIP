using DAL.Entities;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IWeatherRepository
    {
        Task<Weather> GetWeatherByCityNameAsync(string cityName);

        Task<Forecast> GetWForecastByCityNameAsync(string cityName);
    }
}
