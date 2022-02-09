using BL.CustomExceptions;
using BL.Interfaces;
using BL.Services;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.Tests
{
    public class GetWeatherByCityNameIntegrationTests
    {
        private readonly IConfiguration _config;
        private readonly string _key;
        private readonly string _API;
        private readonly HttpClient _client;
        private readonly IWeatherRepository _weatherRepository;
        private readonly IValidator _validator;
        private readonly IWeatherService _weatherService;

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("testsettings.json")
                .Build();
            return config;
        }

        public GetWeatherByCityNameIntegrationTests()
        {
            _config = InitConfiguration();
            _key = _config["APIkey"];
            _API = _config["url"];
            _client = new HttpClient();
            _weatherRepository = new WeatherRepository(_key, _API, _client);
            _validator = new Validator();
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

            //Act
            var output = await _weatherService.GetWeatherByCytyNameAsync(input);
            var match = Regex.Match(output.Message, @"(\w?)(.*?)\.| (\B\W)(.*?)\.", RegexOptions.IgnorePatternWhitespace).Success;

            //Assert

            Assert.NotNull(output.Message);
            Assert.True(match);
            Assert.True(!output.IsBadRequest);
        }

        [Fact]
        public async Task GetWeatherAsync_EmptytInput_ReturnExceptionAsync()
        {
            //Arrange
            var input = string.Empty;
            var message = "Empty input field";

            //Act
            var output = await Assert.ThrowsAsync<EmptyInputException>(() => _weatherService.GetWeatherByCytyNameAsync(input));

            //Assert
            Assert.Equal(message, output.Message);
        }

        [Fact]
        public async void GetWeatherAsync_EmptytInput_ReturnMessageWithoutDataAsync()
        {
            //Arrange
            var input = "Incorrect_input";
            var message = "City not found or input was incorrect";

            //Act
            var output = await _weatherService.GetWeatherByCytyNameAsync(input);

            //Assert
            Assert.True(output.IsBadRequest);
            Assert.Equal(output.Message, message);
        }
    }
}
