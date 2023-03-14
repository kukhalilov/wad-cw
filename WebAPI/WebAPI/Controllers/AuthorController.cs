using DataAccess.Logging;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

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
    }
}
