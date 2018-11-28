using System;

namespace LibrarySystem.Exceptions
{
    public class UnexpectedTypeException : Exception
    {
        public UnexpectedTypeException() : base()
        {

        }

        public UnexpectedTypeException(string message) : base(message)
        {

        }
    }
}
