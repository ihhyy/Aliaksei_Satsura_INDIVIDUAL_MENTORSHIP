using System;
using System.Collections.Generic;
using System.Text;

namespace BL.CustomExceptions
{
    public class IncorrectDaysRangeException : Exception
    {
        public IncorrectDaysRangeException()
            : base(string.Format("Incorrect days input")) { }
    }
}
