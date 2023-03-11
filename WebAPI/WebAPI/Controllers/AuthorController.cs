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
        public AuthorController(IGenericRepository<Author> repository) : base(repository)
        {
        }
    }
}
