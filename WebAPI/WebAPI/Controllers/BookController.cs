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
        public BookController(IGenericRepository<Book> repository) : base(repository)
        {
        }
    }
}
