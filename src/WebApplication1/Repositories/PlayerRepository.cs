using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class PlayerRepository(IConfiguration configuration) : BaseRepository<Player>(configuration.GetRequiredSection("PlayerFile").Value!)
    {
        protected async override Task<List<Player>> FileDataToData(FileStream stream, JsonSerializerOptions options)
        {
            return (await JsonSerializer.DeserializeAsync<PlayersData>(stream, options))?.Players ?? [];
        }

        /// <summary>
        /// Record used to deserialize, matching the file format.
        /// </summary>
        /// <param name="Players"></param>
        public record PlayersData(List<Player> Players);
    }
}