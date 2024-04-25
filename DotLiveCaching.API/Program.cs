
using DotLiveCaching.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DotLiveCaching.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<EcommerceDbContext>(o => o.UseSqlServer("Server=EEU-COMPUTADOR\\SQLEXPRESS; Database=DotLiveCachingDb; Integrated Security=True; trustServerCertificate=true"));

            //builder.Services.AddMemoryCache();

            //builder.Services.AddStackExchangeRedisCache(o =>
            //{
            //    o.InstanceName = "DotLiveCaching-";
            //    o.Configuration = "localhost:6379";
            //});

            builder.Services.AddStackExchangeRedisCache(o =>
            {
                o.InstanceName = "DotLiveCaching-";
                o.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
                {
                    Ssl = true,
                    Password =
                    "SENHA",
                    EndPoints = new StackExchange.Redis.EndPointCollection { { "seuhost.redis.cache.windows.net", 6380 } },

                };
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
