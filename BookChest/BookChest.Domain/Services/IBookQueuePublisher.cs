using BookChest.Domain.Messages;
using BookChest.Domain.Models;
using System.Threading.Tasks;

namespace BookChest.Domain.Services
{
    public interface IBookQueuePublisher
    {
        Task Notify(BookAction action, Isbn isbn);
    }
}
