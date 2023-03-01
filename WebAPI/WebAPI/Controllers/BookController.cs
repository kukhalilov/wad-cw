using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Transactions;
using WebAPI.DAL;
using WebAPI.Models;
using WebAPI.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        // GET: api/Book
        [HttpGet]
        public IActionResult Get()
        {
            var book = _bookRepository.GetBooks();
            return new OkObjectResult(book);
        }
        // GET: api/Book/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _bookRepository.GetBookById(id);
            return new OkObjectResult(book);
        }
        // POST: api/Book
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            using (var scope = new TransactionScope())
            {
                _bookRepository.AddBook(book);
                scope.Complete();
                return CreatedAtAction("Get", new { id = book.Id }, book);
            }
        }
        // PUT: api/Book/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Book book)
        {
            if (book != null)
            {
                using (var scope = new TransactionScope())
                {
                    _bookRepository.UpdateBook(book.Id);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }
        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookRepository.DeleteBook(id);
            return new OkResult();
        }
    }
}
