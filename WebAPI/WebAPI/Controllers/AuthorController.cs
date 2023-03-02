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
    public class AuthorController : BaseController<Author, IGenericRepository<Author>>
    {
        public AuthorController(IGenericRepository<Author> repository) : base(repository)
        {
        }
    }
}
