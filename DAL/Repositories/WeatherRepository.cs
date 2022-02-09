using DAL.Entities;
using DAL.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly HttpClient _client;
        private readonly string _currentWeatherUrl;
        private readonly string _key;
        private readonly string _converterUrl;
        private readonly string _forecastUrl;

        public WeatherRepository(string key, string currentWeatherUrl, HttpClient client, string converterUrl, string forecastUrl)
        {
            _client = client;
            _currentWeatherUrl = currentWeatherUrl;
            _key = key;
            _converterUrl = converterUrl;
            _forecastUrl = forecastUrl;
        }

        public async Task<Weather> GetWeatherByCityNameAsync(string cityName)
        {
            var response = await _client.GetAsync($"{_currentWeatherUrl}q={cityName}&appid={_key}&units=metric");
            var responseBody = await response.Content.ReadAsStringAsync();
            var weather = JsonConvert.DeserializeObject<Weather>(responseBody);

            return weather;
        }

        public async Task<Forecast> GetWForecastByCityNameAsync(string cityName)
        {
            var cityCoord = await GetCoordinatesByCityName(cityName);
            var response = await _client.GetAsync($"{_forecastUrl}lat={cityCoord.Coord.Lat}&lon={cityCoord.Coord.Lon}&appid={_key}&units=metric");
            var responseBody = await response.Content.ReadAsStringAsync();
            var weatherForecast = JsonConvert.DeserializeObject<Forecast>(responseBody);

            return weatherForecast;
        }

        private async Task<CityCoordinates> GetCoordinatesByCityName(string cityName)
        {
            var response = await _client.GetAsync($"{_converterUrl}q={cityName}&appid={_key}");
            var responseBody = await response.Content.ReadAsStringAsync();
            var cityCoordinates = JsonConvert.DeserializeObject<CityCoordinates>(responseBody);

            return cityCoordinates;
        }
    }
}

