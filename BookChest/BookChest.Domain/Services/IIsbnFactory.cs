using BookChest.Domain.Models;

namespace BookChest.Domain.Services
{
    public interface IIsbnFactory
    {
        Isbn Create(string isbnString);
    }
}
