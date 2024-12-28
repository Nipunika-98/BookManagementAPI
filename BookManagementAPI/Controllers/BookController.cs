using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookManagementAPI.Models;
using BookManagementAPI.Data;
using System.Linq;

namespace BookManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ApiContext _context;

        public BookController(ApiContext context)
        {
            _context = context;
        }

        // Create/Edit
        [HttpPost]
        public ActionResult<BookModel> CreateEdit([FromBody] BookModel book)
        {
            if (book == null)
            {
                return BadRequest(new { message = "Book data cannot be null." });
            }

            try
            {
                if (book.Id == 0)
                {
                    // Creating a new record
                    _context.Books.Add(book);
                    _context.SaveChanges();
                    return CreatedAtAction(nameof(Get), new { id = book.Id }, book); 
                }
                else
                {
                    // Updating an existing record
                    var bookInDb = _context.Books.Find(book.Id);
                    if (bookInDb == null)
                    {
                        return NotFound(new { message = $"Book with ID {book.Id} not found." });
                    }

                    // Update properties
                    bookInDb.Title = book.Title;
                    bookInDb.Author = book.Author;
                    bookInDb.Isbn = book.Isbn;
                    bookInDb.PublicationDate = book.PublicationDate;
                    _context.SaveChanges();

                    return Ok(bookInDb); 
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }

        // Get single book by ID
        [HttpGet("{id}")]
        public ActionResult<BookModel> Get(int id)
        {
            var result = _context.Books.Find(id);

            if (result == null)
                return NotFound(new { message = $"Book with ID {id} not found." });

            return Ok(result);
        }

        // Delete a book
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _context.Books.Find(id);

            if (result == null)
                return NotFound(new { message = $"Book with ID {id} not found." });

            _context.Books.Remove(result);
            _context.SaveChanges();

            return NoContent(); 
        }

        // Get all books
        [HttpGet]
        public ActionResult<IEnumerable<BookModel>> GetAll()
        {
            var result = _context.Books.ToList();
            return Ok(result); 
        }
    }
}
