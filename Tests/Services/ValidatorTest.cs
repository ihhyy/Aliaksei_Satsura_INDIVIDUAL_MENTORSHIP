using BL.Interfaces;
using BL.Services;
using DAL.Entities;
using System;
using Tests.Fixtures;
using Xunit;

namespace Tests.Services
{
    public class ValidatorTest
    {
        private readonly WeatherFixture _weatherFixture;
        private readonly IValidator _validator;

        public ValidatorTest()
        {
            _weatherFixture = new WeatherFixture();
            _validator = new ValidatorServices();
        }

        [Fact]
        public void ValidateInput_IncorrectInput()
        {
            //Arrange
            var input = "";

            //Act
            Action result = () => _validator.ValidateInput(input);

            //Assert
            Assert.Throws<Exception>(result);
        }

        [Fact]
        public void ValidateOutput_IncorrectOutput()
        {
            //Arrange
            Weather output = null;

            //Act
            Action result = () => _validator.ValidateOutput(output);

            //Assert
            Assert.Throws<Exception>(result);
        }

        [Fact]
        public void ValidateOutput_CorrectOutput()
        {
            //Arrange
            var output = _weatherFixture.GetWeather()[0];

            //Act
            Action result = () => _validator.ValidateOutput(output);
            result.Invoke();

            //Assert
            Assert.True(true);
        }

        [Fact]
        public void ValidateInput_CorrectInput()
        {
            //Arrange
            var input = "string";

            //Act
            Action result = () => _validator.ValidateInput(input);
            result.Invoke();

            //Assert
            Assert.True(true);
        }
    }
}
