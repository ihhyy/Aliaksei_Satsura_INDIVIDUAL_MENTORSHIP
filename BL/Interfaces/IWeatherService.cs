using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IWeatherService
    {
        Task<string> GetWeatherByCytyNameAsync(string cityName, string key);
    }
}
