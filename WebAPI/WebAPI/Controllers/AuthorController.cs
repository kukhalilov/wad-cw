using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using WebAPI.DAL;
using WebAPI.Models;
using WebAPI.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        // GET: api/Author
        [HttpGet]
        public IActionResult Get()
        {
            var author = _authorRepository.GetAuthors();
            return new OkObjectResult(author);
        }
        // GET: api/Author/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var author = _authorRepository.GetAuthorById(id);
            return new OkObjectResult(author);
        }
        // POST: api/Author
        [HttpPost]
        public IActionResult Post([FromBody] Author author)
        {
            using (var scope = new TransactionScope())
            {
                _authorRepository.AddAuthor(author);
                scope.Complete();
                return CreatedAtAction("Get", new { id = author.Id }, author);
            }
        }
        // PUT: api/Author/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Author author)
        {
            if (author != null)
            {
                using (var scope = new TransactionScope())
                {
                    _authorRepository.UpdateAuthor(author.Id);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }
        // DELETE: api/Author/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _authorRepository.DeleteAuthor(id);
            return new OkResult();
        }
    }
}
