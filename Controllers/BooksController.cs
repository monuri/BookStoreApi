using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService) => _bookService = bookService;

        [HttpGet]
        public async Task<List<Book>> Get() =>
            await _bookService.GetAsync();
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Book>> Get(string id)
        {
            var book = await _bookService.GetAsync(id);
            if (book is null)
            {
                return NotFound();
            }
            return book;
        }
        [HttpPost]
        public async Task<IActionResult> Post(Book newBook)
        {
            await _bookService.CreateAsync(newBook);
            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id:length(24)}")]

        public async Task<IActionResult> Update(string id, Book updateBook)
        {
            var book = await _bookService.GetAsync(id);
            if (book is null)
            {
                return NotFound();
            }
            updateBook.Id = book.Id;
            await _bookService.UpdateAsync(id, updateBook);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]

        public async Task<IActionResult> Delete(string id)
        {
            var book = await _bookService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            await _bookService.RemoveAsync(id);
            return NoContent();

        }
    }
}
