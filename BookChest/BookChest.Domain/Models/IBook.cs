namespace BookChest.Domain.Models
{
    public interface IBook
    {
        IIsbn Isbn { get; }

        string Title { get; set; }

        string Description { get; set; }
    }
}
