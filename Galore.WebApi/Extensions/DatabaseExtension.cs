using System;
using Galore.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Galore.WebApi.Extensions
{
    public static class DatabaseExtension
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Add check if Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT) == "Production" then use mock context?
            services.AddDbContext<GaloreDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Galore.WebApi")));
            
            // services.AddDbContext<UserContext>(options =>
            // options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("Galore.WebApi")));
        }
    }
}