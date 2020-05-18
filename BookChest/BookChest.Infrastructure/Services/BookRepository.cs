using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using BookChest.Domain.Exceptions;
using BookChest.Domain.Messages;
using BookChest.Domain.Models;
using BookChest.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookChest.Infrastructure.Services
{
    internal class BookRepository : IBookRepository
    {
        #region Constructors

        private readonly BookChestDbContext _dbContext;
        private readonly IBookFactory _bookFactory;
        private readonly IBookQueuePublisher _bookQueuePublisher;

        public BookRepository(
            BookChestDbContext dbContext,
            IBookFactory bookFactory,
            IBookQueuePublisher bookQueuePublisher)
        {
            _dbContext = dbContext;
            _bookFactory = bookFactory;
            _bookQueuePublisher = bookQueuePublisher;
        }

        #endregion Constructors

        #region Public Methods

        public async Task Create(Book book)
        {
            var document = Convert(book);

            await _dbContext.SaveAsync(document);

            await _bookQueuePublisher.Notify(BookAction.Created, book.Isbn);
        }

        public async Task Delete(Isbn isbn)
        {
            var isbnString = IsbnToString(isbn);

            await _dbContext.DeleteAsync<BookDocument>(isbnString);

            await _bookQueuePublisher.Notify(BookAction.Deleted, isbn);
        }

        public async Task<IList<Book>> Find(string isbnPart)
        {
            var condition = new ScanCondition(nameof(BookDocument.isbn), ScanOperator.BeginsWith, isbnPart);
            var documents = await _dbContext
                .ScanAsync<BookDocument>(new[] { condition })
                .GetNextSetAsync();

            return documents.Select(Convert).ToList();
        }

        public async Task<Book?> Get(Isbn isbn)
        {
            var isbnString = IsbnToString(isbn);

            BookDocument? document = await _dbContext.LoadAsync<BookDocument>(isbnString);

            if (document == null)
                return null;

            return Convert(document);
        }

        public async Task Update(Book book)
        {
            var isbnString = IsbnToString(book.Isbn);

            var document = await _dbContext.LoadAsync<BookDocument>(isbnString);

            if (document == null)
                throw new DocumentDoesNotExistException("Book does not exist");

            document.title = book.Title;
            document.description = book.Description;

            await _dbContext.SaveAsync(document);

            await _bookQueuePublisher.Notify(BookAction.Updated, book.Isbn);
        }

        #endregion Public Methods

        #region Private Methods

        private BookDocument Convert(Book book)
        {
            return new BookDocument
            {
                isbn = IsbnToString(book.Isbn),
                title = book.Title,
                description = book.Description
            };
        }

        private Book Convert(BookDocument document)
        {
            return _bookFactory.Create(
                document.isbn,
                document.title,
                document.description
                );
        }

        private string IsbnToString(Isbn isbn) => isbn.ToString(IsbnFormat.IncludeHyphens);

        #endregion Private Methods

        #region Nested Classes

        [DynamoDBTable("book-chest")]
        public class BookDocument
        {
            [DynamoDBHashKey] public string isbn { get; set; } = string.Empty;

            [DynamoDBProperty] public string title { get; set; } = string.Empty;

            [DynamoDBProperty] public string description { get; set; } = string.Empty;
        }

        #endregion Nested Classes
    }
}
