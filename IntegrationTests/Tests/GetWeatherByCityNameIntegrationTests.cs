using BL.CustomExceptions;
using BL.Interfaces;
using BL.Services;
using DAL.Interfaces;
using DAL.Repositories;
using IntegrationTests.Configurations;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.Tests
{
    public class GetWeatherByCityNameIntegrationTests
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        private readonly IWeatherRepository _weatherRepository;
        private readonly IValidator _validator;
        private readonly IWeatherService _weatherService;
        private readonly string _key;
        private readonly string _currentWeatherUrl;
        private readonly string _converterUrl;
        private readonly string _forecastrUrl;

        public GetWeatherByCityNameIntegrationTests()
        {
            _config = new TestConfig().InitConfiguration();
            _key = _config["APIkey"];
            _currentWeatherUrl = _config["currentWeatherUrl"];
            _converterUrl = _config["converterUrl"];
            _forecastrUrl = _config["forecastUrl"];
            _client = new HttpClient();
            _weatherRepository = new WeatherRepository(_key, _currentWeatherUrl, _client, _converterUrl, _forecastrUrl);
            _validator = new WeatherInputValidator();
            _weatherService = new WeatherServices(_weatherRepository, _validator);
        }

        [Theory]
        [InlineData("Minsk")]
        [InlineData("oslo")]
        [InlineData("LONDON")]
        [InlineData("ToKyO")]
        [InlineData("Toronto")]
        public async void GetWeatherAsync_CorrectInput_ReturnMessageWithData(string input)
        {
            //Arrange
            var appendix1 = " Dress warm";
            var appendix2 = " It's fresh";
            var appendix3 = " Good weather";
            var appendix4 = " It's time to go to the beach";
            var regex = new Regex(@".?\d+.\d+");
            var message = $"In city {input} {regex} °C now.{appendix1} || {appendix2} || {appendix3} || {appendix4}";

            //Act
            var output = await _weatherService.GetWeatherByCityNameAsync(input);

            //Assert

            Assert.NotNull(output.Message);
            Assert.Matches(message, output.Message);
            Assert.False(output.IsBadRequest);
        }

        [Fact]
        public async Task GetWeatherAsync_EmptyInput_ReturnExceptionAsync()
        {
            //Arrange
            var input = string.Empty;
            var message = "Empty input field";

            //Act
            var output = await Assert.ThrowsAsync<EmptyInputException>(() => _weatherService.GetWeatherByCityNameAsync(input));

            //Assert
            Assert.Equal(message, output.Message);
        }

        [Fact]
        public async void GetWeatherAsync_IncorrectInput_ReturnMessageWithoutDataAsync()
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
