using DotLiveCaching.API.Entities;
using DotLiveCaching.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text;
using System.Text.Json;

namespace DotLiveCaching.API.Controllers
{
    [ApiController]
    [Route("api/states")]
    public class StatesController : ControllerBase
    {
        private readonly EcommerceDbContext _context;
        private readonly IDistributedCache _cache;
        public StatesController(EcommerceDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<State>? states;

            var cacheStateString = await _cache.GetStringAsync("states");

            if (string.IsNullOrWhiteSpace(cacheStateString))
            {
                states = await _context.States.ToListAsync();

                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
                    SlidingExpiration = TimeSpan.FromSeconds(1200)
                };

                await _cache.SetAsync("states", GetBytes(states), options);
            } else
            {
                states = JsonSerializer.Deserialize<List<State>>(cacheStateString);
            }

            // 
            return Ok(states);
        }

        private byte[] GetBytes(object value)
            => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value));
    }
}
