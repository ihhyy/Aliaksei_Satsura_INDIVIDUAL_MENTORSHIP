using DAL.Entities;
using DAL.Interfaces;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly HttpClient _client;
        private readonly string _API;
        private readonly string _key;

        public WeatherRepository(string key, string API)
        {
            _client = new HttpClient();
            _API = API;
            _key = key;
        }

        public async Task<Weather> GetWeatherByCityNameAsync(string cityName)
        {
            var response = await _client.GetAsync($"{_API}q={cityName}&appid={_key}&units=metric");
            var responseBody = await response.Content.ReadAsStringAsync();
            var weather = JsonConvert.DeserializeObject<Weather>(responseBody);

            if (weather.Cod >= 500)
                throw new Exception($"Server error: {weather.Main}");
            else
                return weather;
        }
    }
}

