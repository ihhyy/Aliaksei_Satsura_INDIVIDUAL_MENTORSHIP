using System;

namespace BL.CustomExceptions
{
    public class EmptyInputException : Exception
    {
        public EmptyInputException() :
            base(string.Format("Empty input field"))
        { }
    }
}
