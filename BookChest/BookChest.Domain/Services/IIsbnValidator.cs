namespace BookChest.Domain.Services
{
    public interface IIsbnValidator
    {
        bool IsValidIsbn(string isbnString);
    }
}