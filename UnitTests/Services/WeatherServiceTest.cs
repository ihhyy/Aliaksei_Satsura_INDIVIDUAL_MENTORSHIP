using BL.CustomExceptions;
using BL.Interfaces;
using BL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Moq;
using System.Linq;
using UnitTests.Fixtures;
using Xunit;

namespace Tests.Services
{
    public class WeatherServiceTest
    {
        private readonly WeatherFixture _weatherFixture;
        private readonly ForecastFixture _forecastFixture;
        private readonly IWeatherService _weatherService;
        private readonly Mock<IValidator> _repoValidator;
        private readonly Mock<IWeatherRepository> _repoMock;
        private readonly int _forecastHour = 12;

        public WeatherServiceTest()
        {
            _weatherFixture = new WeatherFixture();
            _forecastFixture = new ForecastFixture();
            _repoValidator = new Mock<IValidator>();
            _repoMock = new Mock<IWeatherRepository>();
            _weatherService = new WeatherServices(_repoMock.Object, _repoValidator.Object, _forecastHour);

        }

        [Theory]
        [InlineData("Oslo", "In Oslo -3 °C. Dress warm")]
        [InlineData("Minsk", "In Minsk 5 °C. It's fresh")]
        [InlineData("Canberra", "In Canberra 27 °C. Good weather")]
        [InlineData("Cairo", "In Cairo 33 °C. It's time to go to the beach")]
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


        [Theory]
        [InlineData("Oslo", 5)]
        public async void GetForecastAsync_CorrectInput_ReturnMessageWithData(string cityName, int days)
        {
            var expected = _forecastFixture.GetWeather();
            var expectedMessage =
                $"Day 1: In {cityName} -10 °C. Dress warm" + "\n" +
                $"Day 2: In {cityName} -3 °C. Dress warm" + "\n" +
                $"Day 3: In { cityName} 5 °C. It's fresh" + "\n" +
                $"Day 4: In { cityName} 27 °C. Good weather" + "\n" +
                $"Day 5: In { cityName} 33 °C. It's time to go to the beach" + "\n";
            _repoMock.Setup(x => x.GetForecastByCityNameAsync(It.IsAny<string>())).ReturnsAsync(expected);

            var forecast = await _weatherService.GetForecastByCityNameAsync(cityName, days);

            Assert.Equal(forecast, expectedMessage);
            Assert.False(expected.IsBadRequest);
        }

        [Theory]
        [InlineData("Incorrect_case", 3)]
        public async void GetForecastAsync_IncorrectInput_ReturnMessageWithError(string cityName, int days)
        {
            //Arrange
            var expected = new Forecast { City = new City { Name = cityName }, IsBadRequest = true };
            _repoMock.Setup(x => x.GetForecastByCityNameAsync(It.IsAny<string>())).ReturnsAsync(expected);

            //Act
            var weather = await _weatherService.GetForecastByCityNameAsync(cityName, days);

            //Assert
            Assert.True(expected.IsBadRequest);
            Assert.Equal("City not found or input was incorrect", weather);
        }

        [Theory]
        [InlineData("Oslo", 0)]
        public async void GetForecastAsync_IncorrectInput_ReturnMessageWithError2(string cityName, int days)
        {
            //Arrange
            var expected = new Forecast { City = new City { Name = cityName}, IsBadRequest = true };
            _repoMock.Setup(x => x.GetForecastByCityNameAsync(It.IsAny<string>())).ReturnsAsync(expected);

            //Act
            var weather = await _weatherService.GetForecastByCityNameAsync(cityName, days);

            //Assert
            Assert.Equal("City not found or input was incorrect", weather);
            Assert.True(expected.IsBadRequest);
        }
    }
}