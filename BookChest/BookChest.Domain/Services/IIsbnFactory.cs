using BookChest.Domain.Models;

namespace BookChest.Domain.Services
{
    public interface IIsbnFactory
    {
        /// <summary>
        /// Create an instance of <see cref="Isbn"/> by given string.
        /// </summary>
        /// <exception cref="T:BookChest.Domain.Exceptions.InvalidIsbnException">If provided value does not match ISBN standard.</exception>

        Isbn Create(string isbnString);
    }
}
