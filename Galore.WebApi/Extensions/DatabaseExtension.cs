using System;
using Galore.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Galore.WebApi.Extensions
{
    /**
        DatabaseExtension.cs
        Connects the application to the database
     */
    public static class DatabaseExtension
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GaloreDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Galore.WebApi")));
            
        }
    }
}