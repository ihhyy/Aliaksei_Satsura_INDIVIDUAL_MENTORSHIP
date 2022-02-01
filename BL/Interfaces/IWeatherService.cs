using DAL.Entities;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IWeatherService
    {
        Task<Weather> GetWeatherByCytyNameAsync(string cityName, string key);
    }
}
