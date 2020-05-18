using System;

namespace BookChest.Domain.Exceptions
{
    public class DocumentDoesNotExistException : InvalidOperationException
    {
        public DocumentDoesNotExistException(string message) : base(message)
        {
        }
    }
}
