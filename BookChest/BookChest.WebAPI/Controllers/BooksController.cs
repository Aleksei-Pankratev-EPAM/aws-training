using BookChest.API;
using BookChest.Domain.Models;
using BookChest.Domain.Services;
using BookChest.WebAPI.ExceptionFilters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookChest.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(InvalidIsbnExceptionFilter))]
    [TypeFilter(typeof(DocumentDoesNotExistExceptionFilter))]
    
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repository;
        private readonly IBookFactory _bookFactory;
        private readonly IIsbnFactory _isbnFactory;

        public BooksController(
            IBookRepository repository,
            IBookFactory bookFactory,
            IIsbnFactory isbnFactory)
        {
            _repository = repository;
            _bookFactory = bookFactory;
            _isbnFactory = isbnFactory;
        }
        
        // GET: api/Books?isbn=951
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookVM>>> GetBooks(string isbn)
        {
            var books = await _repository.Find(isbn);

            return books.Select(Convert).ToList();
        }

        // GET: api/Books/951-41-0717-9
        [HttpGet("{isbnString}")]
        public async Task<ActionResult<BookVM>> GetBook(string isbnString)
        {
            var isbn = _isbnFactory.Create(isbnString);

            var book = await _repository.Get(isbn);

            if (book == null)
            {
                return NotFound();
            }

            return Convert(book);
        }

        // PUT: api/Books/951-41-0717-9
        [HttpPut("{isbnString}")]
        public async Task<IActionResult> PutBook(string isbnString, EditBookVM bookVm)
        {
            var isbn = _isbnFactory.Create(isbnString);

            Book? book = await _repository.Get(isbn);

            if (book == null)
            {
                return NotFound();
            }

            book.Title = bookVm.Title;
            book.Description = bookVm.Description;
            await _repository.Update(book);
            
            return NoContent();
        }
        
        
        // POST: api/Books
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookVM bookVm)
        {
            var book = _bookFactory.Create(bookVm.Isbn, bookVm.Title, bookVm.Description);

            await _repository.Create(book);

            var res = Convert(book);
            return CreatedAtAction(nameof(GetBook), new { isbnString = res.Isbn }, res);
        }
        
        // DELETE: api/Books/951-41-0717-9 
        [HttpDelete("{isbnString}")]
        public async Task<ActionResult<BookVM>> DeleteBook(string isbnString)
        {
            var isbn = _isbnFactory.Create(isbnString);

            await _repository.Delete(isbn);

            return NoContent();
        }

        private BookVM Convert(Book book)
        {
            return new BookVM
            {
                Isbn = book.Isbn.ToString(IsbnFormat.IncludeHyphens),
                Title = book.Title,
                Description = book.Description
            };
        }
    }
}
