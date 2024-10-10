using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class PlayerService(IBaseRepository<Player> repository) : BaseService<Player>(repository)
    {
        /// <summary>
        /// Find and return the country where the players have the best average winning ratio.
        /// </summary>
        /// <returns>Country with the best winning ratio</returns>
        public async Task<Country?> GetCountryWithBestWinningRatio()
        {
            return (await GetAll())
            // For each player, get his country and winning ratio.
            .Select(p => new
            {
                p.Country,
                p.WinningRatio
            })
            // Group by country and calculate average winning ratio.
            .GroupBy(p => p.Country)
            .Select(g => new
            {
                Country = g.Key,
                AverageWinningRatio = g.Average(p => p.WinningRatio)
            })
            // Return the country with the best average winning ratio.
            .OrderByDescending(g => g.AverageWinningRatio)
            .FirstOrDefault()
            ?.Country;
        }

        /// <summary>
        /// Get average IMC of all players.
        /// </summary>
        /// <returns>Average IMC of all players</returns>
        public async Task<double?> GetAverageIMC()
        {
            return (await GetAll())
            .Select(p => p.IMC)
            .Average();
        }

        /// <summary>
        /// Calculate and return the players median height.
        /// How to calculate median : https://fr.wikipedia.org/wiki/Glossaire_des_statistiques#M%C3%A9diane
        /// </summary>
        /// <returns>Players median height</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<double?> GetMedianHeight()
        {
            var heights = (await GetAll()).Select(p => p.Data?.Height).OrderBy(h => h).ToList();

            int count = heights.Count;
            if (count == 0)
            {
                throw new InvalidOperationException("No players to calculate median.");
            }
            else if (count % 2 == 1)
            {
                // Odd number of players, return the middle one
                return heights[count / 2];
            }
            else
            {
                // Even number of players, return the average of the two middle values
                return (heights[(count / 2) - 1] + heights[count / 2]) / 2.0;
            }
        }
    }
}