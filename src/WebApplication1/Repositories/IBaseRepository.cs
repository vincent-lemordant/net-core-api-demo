using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    /// <summary>
    /// Generic repository exposing the common operations to persist or retrieve entities.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T : BaseEntity
    {
        public Task<T?> GetById(int id);
        public Task<List<T>> GetAll();
    }
}