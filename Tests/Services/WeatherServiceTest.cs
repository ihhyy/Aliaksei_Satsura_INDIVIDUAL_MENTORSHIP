using BL.Interfaces;
using BL.Services;
using DAL.Interfaces;
using Moq;
using System.Configuration;
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
        private readonly static string _key = ConfigurationManager.AppSettings["APIKey"];

        public WeatherServiceTest()
        {
            _weatherFixture = new WeatherFixture();
            _repoValidator = new Mock<IValidator>();
            _repoMock = new Mock<IWeatherRepository>();
            _weatherService = new WeatherServices(_repoMock.Object, _repoValidator.Object);
            
        }

        [Fact]
        public async void GetWeatherAsync_CorrectInput()
        {
            //Arrange
            var cityName = _weatherFixture.GetWeather()[0].Name;
            var expectedCity = _weatherFixture.GetWeather()[0];
            _repoMock.Setup(x => x.GetWeatherByCityNameAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(expectedCity);

            //Act
            var weather = await _weatherService.GetWeatherByCytyNameAsync(cityName, _key);

            //Assert
            Assert.Equal(weather.Name, cityName);
        }
    }
}
