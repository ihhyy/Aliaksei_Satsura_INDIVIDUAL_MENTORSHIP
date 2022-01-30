using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public class ValidatorServices : IValidator
    {
        public void ValidateInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new Exception("Empty input string");
        }
    }
}
