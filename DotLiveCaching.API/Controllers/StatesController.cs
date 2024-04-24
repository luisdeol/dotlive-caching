using DotLiveCaching.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DotLiveCaching.API.Controllers
{
    [ApiController]
    [Route("api/states")]
    public class StatesController : ControllerBase
    {
        private readonly EcommerceDbContext _context;
        private readonly IMemoryCache _cache;
        public StatesController(EcommerceDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var states = await _cache.GetOrCreateAsync("states",
                async e =>
                {
                    return await _context.States.ToListAsync();
                });

            return Ok(states);
        }
    }
}
