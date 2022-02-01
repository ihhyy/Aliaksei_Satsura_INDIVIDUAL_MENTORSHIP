using BL.Interfaces;
using DAL.Entities;
using System;

namespace BL.Services
{
    public class ValidatorServices : IValidator
    {
        public void ValidateInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new Exception("Empty input string");
        }

        public void ValidateOutput(Weather output)
        {
            if (output == null)
                throw new Exception("Incorrect city name or city not found");
        }
    }
}
