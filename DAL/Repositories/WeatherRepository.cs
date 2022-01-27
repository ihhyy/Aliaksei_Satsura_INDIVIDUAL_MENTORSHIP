using DAL.Entities;
using DAL.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {

        private readonly string APIKey;
        private readonly HttpClient client;

        public WeatherRepository()
        {
            APIKey = ConfigurationManager.AppSettings["APIKey"];
            client = new HttpClient();
        }

        public async Task<Weather> GetWeatherByCityNameAsync(string cityName)
        {
            HttpResponseMessage response = await client.GetAsync("https://api.openweathermap.org/data/2.5/weather?q=" + cityName + "&appid=" + APIKey + "&units=metric");
            var responceBody = await response.Content.ReadAsStringAsync();
            var weather = JsonConvert.DeserializeObject<Weather>(responceBody);
            return weather;
        }
    }
}
