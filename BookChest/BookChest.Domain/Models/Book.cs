namespace BookChest.Domain.Models
{
    internal class Book : IBook
    {
        public IIsbn Isbn { get; private set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Book(IIsbn isbn, string title = "", string description = "")
        {
            Isbn = isbn;
            Title = title;
            Description = description;
        }
    }
}
