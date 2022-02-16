using BL.CustomExceptions;
using BL.Interfaces;
using BL.Services;
using DAL.Interfaces;
using DAL.Repositories;
using IntegrationTests.Configurations;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

        public GetForecastByCityNameIntegrationTests()
        {
            _config = new TestConfig().InitConfiguration();
            _key = _config["APIkey"];
            _currentWeatherUrl = _config["currentWeatherUrl"];
            _coordinatesrUrl = _config["converterUrl"];
            _forecastrUrl = _config["forecastUrl"];
            _min = int.Parse(_config["min"]);
            _max = int.Parse(_config["max"]);
            _forecastHour = int.Parse(_config["forecastHour"]);
            _client = new HttpClient();
            _weatherRepository = new WeatherRepository(_key, _coordinatesrUrl, _forecastrUrl, _currentWeatherUrl, _client);
            _validator = new WeatherInputValidator(_min, _max);
            _weatherService = new WeatherServices(_weatherRepository, _validator, _forecastHour);
        }

        [Theory]
        [InlineData("Oslo", 5)]
        [InlineData("Minsk", 4)]
        [InlineData("London", 3)]
        [InlineData("Cairo", 2)]
        [InlineData("Canberra", 1)]
        public async void GetForecastAsync_CorrectInput_ReturnMessageWithData(string input, int days)
        {
            //Arrange
            var appendix1 = " Dress warm";
            var appendix2 = " It's fresh";
            var appendix3 = " Good weather";
            var appendix4 = " It's time to go to the beach";
            var regex = new Regex(@".?\d+.\d+");
            var message = string.Empty;

            for(int i = 0; i < days; i++)
            {
                message = $"Day {i+1}. In city {input} {regex} °C now.{appendix1} || {appendix2} || {appendix3} || {appendix4}";
            }

            //Act
            var output = await _weatherService.GetWeatherByCityNameAsync(input);

            //Assert

            Assert.NotNull(output.Message);
            Assert.Matches(message, output.Message);
            Assert.False(output.IsBadRequest);
        }

        [Theory]
        [InlineData("", 5)]
        [InlineData("", 0)]
        [InlineData("Oslo", 0)]
        public async Task GetForecastAsync_EmptyStringInput_ReturnExceptionAsync(string input, int days)
        {
            //Arrange
            var message = "Empty input field";

            //Act
            var output = await Assert.ThrowsAsync<EmptyInputException>(() => _weatherService.GetForecastByCityNameAsync(input, days));

            //Assert
            Assert.Equal(message, output.Message);
        }

        [Fact]
        public async Task GetForecastAsync_IncorrectInput_ReturnExceptionAsync()
        {
            //Arrange
            var input = "Incorrect_input";
            var message = "City not found or input was incorrect";

            //Act
            var output = await _weatherService.GetWeatherByCityNameAsync(input);

            //Assert
            Assert.True(output.IsBadRequest);
            Assert.Equal(output.Message, message);
        }
    }
}
