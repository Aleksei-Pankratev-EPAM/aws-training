namespace BookChest.API
{
    public class BookVM
    {
        /// <summary>
        /// International Standard Book Number with or without separators.
        /// See https://en.wikipedia.org/wiki/International_Standard_Book_Number.
        /// <example>978-80-86056-31-9</example>
        /// </summary>
        public string Isbn { get; set; } = string.Empty;

        /// <summary>
        /// Book title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Description of the book.
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
