namespace BookChest.Domain.Models
{
    public interface IIsbn
    {
        string ToString(IsbnFormat format);
    }
}
