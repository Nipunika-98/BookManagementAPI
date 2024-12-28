using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookManagementAPI.Models;
using BookManagementAPI.Data;

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
        public JsonResult CreateEdit(BookModel book
            )
        {
            if(book.Id == 0)
            {
                _context.Books.Add(book);

            }
            else
            {
                var bookInDb = _context.Books.Find(book.Id);
                if(bookInDb == null)
                    return new JsonResult(NotFound());

                bookInDb = book;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(book));

        }

        // Get
        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Books.Find(id);

            if(result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        // Delete
        [HttpDelete] 
        public JsonResult Delete(int id) 
        {
            var result = _context.Books.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            _context.Books.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        // Get all
        [HttpGet()]
        public JsonResult GetAll()
        {
            var result = _context.Books.ToList();

            return new JsonResult(Ok(result));
        }
    }
}
