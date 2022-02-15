using BL.Interfaces;
using BL.CustomExceptions;
using AppConfig.Interfaces;

namespace BL.Services
{
    public class WeatherInputValidator : IValidator
    {
        private readonly IConfig _config;

        public WeatherInputValidator(IConfig config)
        {
            _config = config;
        }


        public void ValidateInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new EmptyInputException();
        }

        public void ValidateMultiInput(string input, int days)
        {
            var min = int.Parse(_config.MinDays);
            var max = int.Parse(_config.MaxDays);

            if (days == 0)
                throw new EmptyInputException();

            if (days < min || days > max)
                throw new IncorrectDaysRangeException();


            this.ValidateInput(input);
        }
    }
}
