using BookChest.Domain.Exceptions;
using BookChest.Domain.Models;

namespace BookChest.Domain.Services
{
    internal class IsbnFactory : IIsbnFactory
    {
        private readonly IIsbnValidator _isbnValidator;

        public IsbnFactory(IIsbnValidator isbnValidator)
        {
            _isbnValidator = isbnValidator;
        }

        /// <summary>
        /// Create an instance of <see cref="Isbn"/> by given string.
        /// </summary>
        /// <exception cref="T:BookChest.Domain.Exceptions.InvalidIsbnException">If provided value does not match ISBN standard.</exception>
        public Isbn Create(string isbnString)
        {
            if (!_isbnValidator.IsValidIsbn(isbnString))
                throw new InvalidIsbnException($"Provided string {isbnString} is not a valid ISBN");

            var sanitizedIsbn = _isbnValidator.Sanitize(isbnString);

            return new Isbn(sanitizedIsbn);
        }
    }
}
