using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    /// <inheritdoc />
    public abstract class BaseRepository<T>(string filePath) : IBaseRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Return matching entity or null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Matching entity or null</returns>
        public async Task<T?> GetById(int id)
        {
            return (await GetData()).FirstOrDefault(item => item.Id.Equals(id));
        }

        /// <summary>
        /// Return all <typeparamref name="T"/> entities.
        /// </summary>
        /// <returns>All <typeparamref name="T"/> entities</returns>
        public async Task<List<T>> GetAll()
        {
            return await GetData();
        }

        /// <summary>
        /// Read data from file.
        /// </summary>
        /// <returns>Deserialized list of entities</returns>
        private async Task<List<T>> GetData()
        {
            using FileStream stream = File.OpenRead(filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return await FileDataToData(stream, options);
        }

        /// <summary>
        /// Deserialize the stream into a List of entities. Child class should override this method.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="options"></param>
        /// <returns>Deserialized list of entities</returns>
        /// <exception cref="NotImplementedException"></exception>
        protected virtual Task<List<T>> FileDataToData(FileStream stream, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}