using BL.Interfaces;
using BL.CustomExceptions;

namespace BL.Services
{
    public class Validator : IValidator
    {
        public void ValidateInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new EmptyInputException();
        }
    }
}
