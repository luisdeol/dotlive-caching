using DotLiveCaching.API.Entities;
using DotLiveCaching.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DotLiveCaching.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly EcommerceDbContext _context;
        private readonly IMemoryCache _cache;
        private const string PRODUCT_CACHE_KEY = "products";

        public ProductsController(EcommerceDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // var products = await _context.Products.ToListAsync();

            //List<Product>? products = [];

            //if (!_cache.TryGetValue(PRODUCT_CACHE_KEY, out products)) {
            //    products = await _context.Products.ToListAsync();
            //    _cache.Set(PRODUCT_CACHE_KEY, products);
            //}

            var products = await _cache.GetOrCreateAsync(PRODUCT_CACHE_KEY, async e =>
            {
                return await _context.Products.ToListAsync();
            });

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
            var key = $"p{id}";

            var product = await _cache.GetOrCreateAsync(key, async e =>
            {
                return await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
            });

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            var key = $"p{product.Id}";
            _cache.Set(key, product);

            return NoContent();
        }
    }
}
