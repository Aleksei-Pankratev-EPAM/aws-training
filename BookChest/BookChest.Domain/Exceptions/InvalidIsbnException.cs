using System;

namespace BookChest.Domain.Exceptions
{
    public class InvalidIsbnException : ArgumentException
    {
        public InvalidIsbnException(string message) : base(message)
        {
        }
    }
}
