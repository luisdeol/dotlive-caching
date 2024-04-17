using DotLiveCaching.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotLiveCaching.API.Persistence
{
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<State>(e =>
            {
                e.HasKey(s => s.Id);

                e.HasData([new State { Id = 1, Country = "BR", Name = "São Paulo" }, new State { Id = 2, Country = "BR", Name = "Rio de Janeiro" }, new State { Id = 3, Country = "BR", Name = "Minas Gerais" }, new State { Id = 4, Country = "BR", Name = "Rio Grande do Sul" }, new State { Id = 5, Country = "BR", Name = "Fortaleza" }]);
            });

            builder.Entity<Product>(e =>
            {
                e.HasKey(p => p.Id);

                e.HasOne(p => p.State)
                    .WithMany()
                    .HasForeignKey(p => p.StateId);

                e.HasData([new Product { Id = 1, Name = "Chinelo", Price = 50, StateId = 5 }, new Product { Id = 2, Name = "Cadeira Gamer", Price = 1_500, StateId = 4 }, new Product { Id = 3, Name = "Monitor 4k Ultrawide", Price = 4_000, StateId = 3 }, new Product { Id = 4, Name = "Notebook Gamer", Price = 5_000, StateId = 2 }, new Product { Id = 5, Name = "Garrafa", Price = 50, StateId = 1 }]);
            });

            base.OnModelCreating(builder);
        }
    }
}
