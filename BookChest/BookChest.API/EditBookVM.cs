namespace BookChest.API
{
    public class EditBookVM
    {
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
