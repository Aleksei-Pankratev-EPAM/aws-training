using System;

namespace BookChest.Domain.Models
{
    public class Isbn : IEquatable<Isbn>
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

        #region IEquatable implementation

        public bool Equals(Isbn other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (_parts.Length != other._parts.Length)
            {
                return false;
            }

            for (int i = 0; i < _parts.Length; i++)
            {
                if (!string.Equals(_parts[i], other._parts[i], StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((Isbn) obj);
        }

        public override int GetHashCode()
        {
            return (_parts != null ? _parts.GetHashCode() : 0);
        }

        public static bool operator ==(Isbn left, Isbn right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Isbn left, Isbn right)
        {
            return !Equals(left, right);
        }

        #endregion IEquatable implementation
    }
}
