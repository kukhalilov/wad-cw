using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TEntity, TRepository> : ControllerBase
        where TEntity : class
        where TRepository : IGenericRepository<TEntity>
    {
        private readonly TRepository _repository;

        protected BaseController(TRepository repository)
        {
            _repository = repository;
        }

        // GET: api/{Controller}
        [HttpGet]
        public virtual IActionResult Get(string searchTerm, string sortBy,
                bool sortAsc = true, int page = 1, int pageSize = 10
            )
        {
            var (entities, _) = _repository.GetAll(searchTerm, sortBy, sortAsc, page, pageSize);
            return new OkObjectResult(entities);
        }

        // GET: api/{Controller}/5
        [HttpGet("{id}")]
        public virtual IActionResult Get(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return new OkObjectResult(entity);
        }

        // POST: api/{Controller}
        [HttpPost]
        public virtual IActionResult Post([FromBody] TEntity entity)
        {
            if (entity == null)
            {
                return BadRequest("Entity is null.");
            }

            using var scope = new TransactionScope();
            try
            {
                _repository.Add(entity);
                scope.Complete();
                return CreatedAtAction(nameof(Post), entity);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // PUT: api/{Controller}/5
        [HttpPut("{id}")]
        public virtual IActionResult Put(int id, [FromBody] TEntity entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            var existingEntity = _repository.GetById(id);
            if (existingEntity == null)
            {
                return NotFound();
            }

            using var scope = new TransactionScope();
            try
            {
                _repository.Update(id, entity);
                scope.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // DELETE: api/{Controller}/5
        [HttpDelete("{id}")]
        public virtual IActionResult Delete(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }

            _repository.Delete(id);

            return Ok();
        }
    }
}
