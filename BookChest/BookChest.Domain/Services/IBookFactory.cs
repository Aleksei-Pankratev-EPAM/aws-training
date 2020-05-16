using BookChest.Domain.Models;

namespace BookChest.Domain.Services
{
    public interface IBookFactory
    {
        Book Create(string isbnString, string title = "", string description = "", uint version = 0);
    }
}
