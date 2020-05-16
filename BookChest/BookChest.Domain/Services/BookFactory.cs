using BookChest.Domain.Models;
using System;

namespace BookChest.Domain.Services
{
    internal class BookFactory : IBookFactory
    {
        public IBook Create(string isbn, string title = "", string description = "")
        {
            throw new NotImplementedException();
        }
    }
}