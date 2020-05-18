using BookChest.Domain.Models;

namespace BookChest.Domain.Services
{
    internal class BookFactory : IBookFactory
    {
        private readonly IIsbnFactory _isbnFactory;

        public BookFactory(IIsbnFactory isbnFactory)
        {
            _isbnFactory = isbnFactory;
        }

        public Book Create(string isbnString, string title = "", string description = "", uint version = 0)
        {
            var isbn = _isbnFactory.Create(isbnString);

            return new Book(isbn, title, description, version);
        }
    }
}
