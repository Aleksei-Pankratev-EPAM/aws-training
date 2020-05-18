using System;

namespace BookChest.Domain.Models
{
    [Flags]
    public enum IsbnFormat
    {
        None             = 0
        , IncludePrefix  = 1 << 0
        , IncludeHyphens = 1 << 1
    }
}
