using BookChest.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookChest.Domain.Services
{
    public interface IBookRepository
    {
        Task Create(Book book);

        Task Delete(Isbn isbn);

        Task<IList<Book>> Find(string isbnPart);

        Task<Book?> Get(Isbn isbn);

        Task Update(Book book);
    }
}
