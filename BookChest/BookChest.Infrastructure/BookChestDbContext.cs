using BookChest.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookChest.Infrastructure
{
    public class BookChestDbContext : DbContext
    {
        public BookChestDbContext(DbContextOptions<BookChestDbContext> options)
            : base(options)
        {
        }

        public DbSet<IBook>? Books { get; set; }
    }
}
