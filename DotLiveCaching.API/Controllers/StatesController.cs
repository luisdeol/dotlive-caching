using DotLiveCaching.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotLiveCaching.API.Controllers
{
    [ApiController]
    [Route("api/states")]
    public class StatesController : ControllerBase
    {
        private readonly EcommerceDbContext _context;

        public StatesController(EcommerceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var states = await _context.States.ToListAsync();

            return Ok(states);
        }
    }
}
