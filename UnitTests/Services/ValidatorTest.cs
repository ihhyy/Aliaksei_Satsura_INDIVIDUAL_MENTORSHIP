using BL.Interfaces;
using BL.Services;
using BL.CustomExceptions;
using System;
using Xunit;

namespace Tests.Services
{
    public class ValidatorTest
    {
        private readonly IValidator _validator;
        private readonly int _min = 1;
        private readonly int _max = 5;

        public ValidatorTest()
        {
            _validator = new WeatherInputValidator(_min, _max);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ValidateInput_IncorrectInput_ThrowException(string input)
        {
            //Arrange

            //Act
            Action result = () => _validator.ValidateInput(input);

            //Assert
            var exception = Assert.Throws<EmptyInputException>(result);
            Assert.Equal("Empty input field", exception.Message);
        }

        [Fact]
        public void ValidateInput_CorrectInput_PassSuccessfully()
        {
            //Arrange
            var input = "string";

            //Act
            var exception = Record.Exception(() => _validator.ValidateInput(input));

            //Assert
            Assert.Null(exception);
        }
    }
}
