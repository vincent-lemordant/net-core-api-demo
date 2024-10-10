using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    public class PlayerController(PlayerService service) : BaseController<Player>(service)
    {
        public override async Task<List<Player>> GetAll()
        {
            return [.. (await service.GetAll()).OrderBy(o => o?.Data?.Rank)];
        }

        [HttpGet("country-with-best-winning-ratio")]
        public async Task<Country?> GetCountryWithBestWinningRatio()
        {
            return await service.GetCountryWithBestWinningRatio();
        }

        [HttpGet("average-imc")]
        public async Task<double?> GetAverageIMC()
        {
            return await service.GetAverageIMC();
        }

        [HttpGet("median-height")]
        public async Task<double?> GetMedianHeight()
        {
            return await service.GetMedianHeight();
        }
    }
}