using BookChest.Domain.Models;
using System;

namespace BookChest.Domain.Services
{
    public interface IIsbnFactory
    {
        IIsbn Create(string isbnString);
    }

    internal class IsbnFactory : IIsbnFactory
    {
        private readonly IIsbnValidator _isbnValidator;

        public IsbnFactory(IIsbnValidator isbnValidator)
        {
            _isbnValidator = isbnValidator;
        }

        /// <exception cref="T:System.ArgumentException">If provided value does not match ISBN standard.</exception>
        public IIsbn Create(string isbnString)
        {
            if (!_isbnValidator.IsValidIsbn(isbnString))
                throw new ArgumentException($"Provided string {isbnString} is not a valid ISBN");

            var sanitizedIsbn = _isbnValidator.Sanitize(isbnString);

            return new Isbn(sanitizedIsbn);
        }
    }
}
