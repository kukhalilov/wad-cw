using DataAccess.Logging;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Author")]
    [ApiController]
    public class AuthorController : BaseController<Author, IGenericRepository<Author>>
    {
        private readonly IGenericRepository<Author> _authorRepository;
        private readonly IObserver _loggingService;

        public AuthorController(IGenericRepository<Author> repository) : base(repository)
        {
            _authorRepository = repository;
            _loggingService = new LoggingService();
            (_authorRepository as IObservable).Attach(_loggingService);
        }

        // GET: api/Author
        [HttpGet]
        public override IActionResult Get(string searchTerm = "", string sortBy = "Name",
                bool sortAsc = true, int page = 1, int pageSize = 10
            )
        {
            var authors = _authorRepository.GetAll(searchTerm, sortBy, sortAsc, page, pageSize);

            var totalCount = authors.Count();

            HttpContext.Response.Headers.Add("Authors-Total-Count", totalCount.ToString());

            return new OkObjectResult(authors);
        }
    }
}
