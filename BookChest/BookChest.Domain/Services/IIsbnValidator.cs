namespace BookChest.Domain.Services
{
    public interface IIsbnValidator
    {
        bool IsValidIsbn(string isbnString);

        string Sanitize(string isbnString);
    }
}
