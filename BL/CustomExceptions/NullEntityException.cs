using System;

namespace BL.CustomExceptions
{
    public class NullEntityException : Exception
    {
        public NullEntityException()
            :base(string.Format("City not found or incorrect input"))
        { }
    }
}
