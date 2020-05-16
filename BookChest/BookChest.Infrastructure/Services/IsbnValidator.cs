using BookChest.Domain.Services;
using Validator = ProductCodeValidator.IsbnValidator;

namespace BookChest.Infrastructure.Services
{
    internal class IsbnValidator : IIsbnValidator
    {
        public bool IsValidIsbn(string isbnString)
        {
            return Validator.IsValidIsbn(isbnString);
        }

        public string Sanitize(string isbnString)
        {
            return Validator.RemovePrefix(isbnString).Trim();
        }
    }
}
