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

        public WeatherRepository()
        {
            _client = new HttpClient();
            _API = ConfigurationManager.AppSettings["url"];
        }

        public async Task<Weather> GetWeatherByCityNameAsync(string cityName, string key)
        {
            var response = await _client.GetAsync($"{_API}q={cityName}&appid={key}&units=metric");
            var responseBody = await response.Content.ReadAsStringAsync();
            var weather = JsonConvert.DeserializeObject<Weather>(responseBody);

            if (weather.Cod >= 500)
                throw new Exception("Server error");
            else
                return weather;
        }
    }
}

