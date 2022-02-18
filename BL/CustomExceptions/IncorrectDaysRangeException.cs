using System;

namespace BL.CustomExceptions
{
    public class IncorrectDaysRangeException : Exception
    {
        public IncorrectDaysRangeException()
            : base(string.Format("Incorrect days input")) { }
    }
}
