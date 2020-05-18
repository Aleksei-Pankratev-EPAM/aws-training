namespace BookChest.Domain.Models
{
    public class Book
    {
        public Isbn Isbn { get; }

        public string Title { get; set; }

        public string Description { get; set; }

        public uint Version { get; set; }

        public Book(Isbn isbn, string title, string description, uint version)
        {
            Isbn = isbn;
            Title = title;
            Description = description;
            Version = version;
        }
    }
}
