using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Transactions;
using WebAPI.DAL;
using WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookContext _dbContext;
        public BookController(BookContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Book
        [HttpGet]
        public IActionResult Get()
        {
            var books = _dbContext.Books.ToList();
            return new OkObjectResult(books);
        }

        // GET: api/Book/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var Book = _dbContext.Books.FirstOrDefault(x => x.Id == id);
            return new OkObjectResult(Book);
        }

        // POST: api/Book
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            if (book != null)
            {
                using var scope = new TransactionScope();
                _dbContext.Books.Add(book);                  
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
            }
            return new NoContentResult();
        }

        // PUT: api/Book/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Book book)
        {
            if (book != null)
            {
                using var scope = new TransactionScope();
                _dbContext.Books.Add(book);
                scope.Complete();
                return new OkResult();
            }
            return new NoContentResult();
        }

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _dbContext.Books.Remove(_dbContext.Books.FirstOrDefault(x => x.Id == id));
            return new OkResult();
        }
    }
}
