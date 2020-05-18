namespace BookChest.Domain.Messages
{
    public class BookChangeMessage
    {
        public BookAction Action { get; set; }

        public string Isbn { get; set; } = string.Empty;
    }
}
