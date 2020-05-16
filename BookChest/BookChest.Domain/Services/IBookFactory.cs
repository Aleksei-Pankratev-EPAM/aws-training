using BookChest.Domain.Models;

namespace BookChest.Domain.Services
{
    public interface IBookFactory
    {
        IBook Create(string isbn, string title = "", string description = "");
    }
}
