namespace BookChest.Domain.Models
{
    public class Isbn
    {
        private const string Prefix = "ISBN ";
        private const char Hyphen = '-';
        private static readonly string HyphenString = Hyphen.ToString();

        private readonly string[] _parts;

        /// <summary>
        /// Create an ISBN instance by give sanitized string.
        /// </summary>
        /// <param name="isbnString">Sanitized string containing hyphens</param>
        public Isbn(string isbnString)
        {
            _parts = isbnString.Split(Hyphen);
        }

        public string ToString(IsbnFormat format)
        {
            var separator = HasFlag(format, IsbnFormat.IncludeHyphens)
                ? HyphenString
                : string.Empty;

            var isbnString = string.Join(separator, _parts);

            if (HasFlag(format, IsbnFormat.IncludePrefix))
                isbnString = Prefix + isbnString;

            return isbnString;
        }

        private bool HasFlag(IsbnFormat format, IsbnFormat flag)
        {
            return (format & flag) == flag;
        }
    }
}
