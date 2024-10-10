using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// An abstract base controller for managing API endpoints related to entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="service"></param>
    [ApiController]
    [Route("api/[controller]s")]
    public abstract class BaseController<T>(BaseService<T> service) : ControllerBase where T : BaseEntity
    {

        [HttpGet("{id}")]
        public async Task<T?> GetById([FromRoute] int id)
        {
            return await service.GetById(id);
        }

        [HttpGet()]
        public virtual async Task<List<T>> GetAll()
        {
            return await service.GetAll();
        }
    }
}