using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    /// <summary>
    /// An abstract base service for managing business operations related to entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="repository"></param>
    public abstract class BaseService<T>(IBaseRepository<T> repository) where T : BaseEntity
    {
        protected IBaseRepository<T> repository { get; set; } = repository;

        public async Task<T?> GetById(int id)
        {
            return await repository.GetById(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await repository.GetAll();
        }
    }
}