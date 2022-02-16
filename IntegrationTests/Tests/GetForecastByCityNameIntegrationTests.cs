using BL.Interfaces;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace IntegrationTests.Tests
{
    public class GetForecastByCityNameIntegrationTests
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        private readonly IWeatherRepository _weatherRepository;
        private readonly IValidator _validator;
        private readonly IWeatherService _weatherService;
        private readonly string _key;
        private readonly string _currentWeatherUrl;
        private readonly string _coordinatesrUrl;
        private readonly string _forecastrUrl;
        private readonly int _min;
        private readonly int _max;
        private readonly int _forecastHour;

        [Theory]
        [InlineData("Oslo")]
        public async void GetForecastAsync_CorrectInput_ReturnMessageWithData(string input)
        {
            
        }
    }
}
