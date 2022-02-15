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
        private readonly string _coordinatesUrl;
        private readonly string _forecastUrl;

        public WeatherRepository(string key, string coorinatesUrl, string forecastUrl, string currentWeatherUrl, HttpClient client)
        {
            _client = client;
            _key = key;
            _coordinatesUrl = coorinatesUrl;
            _forecastUrl = forecastUrl;
            _currentWeatherUrl = currentWeatherUrl;
        }

        public async Task<Weather> GetWeatherByCityNameAsync(string cityName)
        {
            var response = await _client.GetAsync($"{_currentWeatherUrl}q={cityName}&appid={_key}&units=metric");
            var responseBody = await response.Content.ReadAsStringAsync();
            var weather = JsonConvert.DeserializeObject<Weather>(responseBody);

            return weather;
        }

        public async Task<Forecast> GetForecastByCityNameAsync(string cityName)
        {
            var cityCoord = await GetCoordinatesByCityName(cityName);
            var weatherForecast = new Forecast();

            if(cityCoord.Coord == null)
            {
                weatherForecast.IsBadRequest = true;
                return weatherForecast;
            }
            else
            {
                weatherForecast.IsBadRequest = false;
            }

            var response = await _client.GetAsync($"{_forecastUrl}lat={cityCoord.Coord.Lat}&lon={cityCoord.Coord.Lon}&appid={_key}&units=metric");
            var responseBody = await response.Content.ReadAsStringAsync();
            weatherForecast = JsonConvert.DeserializeObject<Forecast>(responseBody);

            return weatherForecast;
        }

        private async Task<CityCoordinates> GetCoordinatesByCityName(string cityName)
        {
            var response = await _client.GetAsync($"{_coordinatesUrl}q={cityName}&appid={_key}");
            var responseBody = await response.Content.ReadAsStringAsync();
            var cityCoordinates = JsonConvert.DeserializeObject<CityCoordinates>(responseBody);

            return cityCoordinates;
        }
    }
}

