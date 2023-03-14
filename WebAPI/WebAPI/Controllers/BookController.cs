using DataAccess.Logging;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : BaseController<Book, IGenericRepository<Book>>
    {
        private readonly IGenericRepository<Book> _bookRepository;
        private readonly IObserver _loggingService;

        public BookController(IGenericRepository<Book> repository) : base(repository)
        {
            _bookRepository = repository;
            _loggingService = new LoggingService();
            (_bookRepository as IObservable).Attach(_loggingService);
        }
    }
}
