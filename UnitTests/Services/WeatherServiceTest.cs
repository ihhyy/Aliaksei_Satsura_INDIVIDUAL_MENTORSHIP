using BL.CustomExceptions;
using BL.Interfaces;
using BL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Moq;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Tests.Fixtures;
using Xunit;

namespace Tests.Services
{
    public class WeatherServiceTest
    {
        private readonly WeatherFixture _weatherFixture;
        private readonly IWeatherService _weatherService;
        private readonly Mock<IValidator> _repoValidator;
        private readonly Mock<IWeatherRepository> _repoMock;

        public WeatherServiceTest()
        {
            _weatherFixture = new WeatherFixture();
            _repoValidator = new Mock<IValidator>();
            _repoMock = new Mock<IWeatherRepository>();
            _weatherService = new WeatherServices(_repoMock.Object, _repoValidator.Object);

        }

        [Theory]
        [InlineData("Oslo", "In Oslo -3 °C now. Dress warm")]
        [InlineData("Minsk", "In Minsk 5 °C now. It's fresh")]
        [InlineData("Canberra", "In Canberra 27 °C now. Good weather")]
        [InlineData("Cairo", "In Cairo 33 °C now. It's time to go to the beach")]
        public async void GetWeatherAsync_CorrectInput_ReturnMessageWithData(string cityName, string message)
        {
            //Arrange
            var expected = _weatherFixture.GetWeather().Where(w => w.Name == cityName).FirstOrDefault();
            _repoMock.Setup(x => x.GetWeatherByCityNameAsync(It.IsAny<string>())).ReturnsAsync(expected);

            //Act
            var weather = await _weatherService.GetWeatherByCityNameAsync(cityName);

            //Assert
            Assert.Equal(weather.Message, message);
            Assert.False(weather.IsBadRequest);
        }

        [Theory]
        [InlineData("Incorrect_case")]
        public async void GetWeatherAsync_InCorrectInput_ReturnMessageWithError(string cityName)
        {
            //Arrange
            var expected = _weatherFixture.GetWeather().Where(w => w.Name == cityName).FirstOrDefault();
            _repoMock.Setup(x => x.GetWeatherByCityNameAsync(It.IsAny<string>())).ReturnsAsync(expected);

            //Act
            var weather = await _weatherService.GetWeatherByCityNameAsync(cityName);

            //Assert
            Assert.Equal("City not found or input was incorrect", weather.Message);
            Assert.True(weather.IsBadRequest);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Minsk")]
        public async void GetWeatherAsync_EmptytInput_ReturnMessageWithErrorAsync(string cityName)
        {
            //Arrange
            _repoMock.Setup(x => x.GetWeatherByCityNameAsync(It.IsAny<string>())).ReturnsAsync(() => throw new EmptyInputException());

            //Act
            var result = await Assert.ThrowsAsync<EmptyInputException>(() => _weatherService.GetWeatherByCityNameAsync(cityName));

            //Assert
            Assert.Equal("Empty input field", result.Message);
        }
    }
}