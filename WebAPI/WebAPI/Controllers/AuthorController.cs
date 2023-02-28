using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using WebAPI.DAL;
using WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly BookContext _dbContext;
        public AuthorController(BookContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Author
        [HttpGet]
        public IActionResult Get()
        {
            var Authors = _dbContext.Authors.ToList();
            return new OkObjectResult(Authors);
        }

        // GET: api/Author/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var Author = _dbContext.Authors.FirstOrDefault(x => x.Id == id);
            return new OkObjectResult(Author);
        }

        // POST: api/Author
        [HttpPost]
        public IActionResult Post([FromBody] Author Author)
        {
            if (Author != null)
            {
                using var scope = new TransactionScope();
                _dbContext.Authors.Add(Author);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = Author.Id }, Author);
            }
            return new NoContentResult();
        }

        // PUT: api/Author/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Author Author)
        {
            if (Author != null)
            {
                using var scope = new TransactionScope();
                _dbContext.Authors.Add(Author);
                scope.Complete();
                return new OkResult();
            }
            return new NoContentResult();
        }

        // DELETE: api/Author/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _dbContext.Authors.Remove(_dbContext.Authors.FirstOrDefault(x => x.Id == id));
            return new OkResult();
        }
    }
}
