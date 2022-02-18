using BL.Interfaces;
using BL.CustomExceptions;

namespace BL.Services
{
    public class WeatherInputValidator : IValidator
    {
        private readonly int _min;
        private readonly int _max;

        public WeatherInputValidator(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public void ValidateInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new EmptyInputException();
        }

        public void ValidateMultiInput(string input, int days)
        {
            if (days < _min || days > _max)
                throw new IncorrectDaysRangeException();

            ValidateInput(input);
        }
    }
}
