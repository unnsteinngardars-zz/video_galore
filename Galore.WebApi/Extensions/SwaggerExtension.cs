
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Galore.WebApi.Extensions
{
    /**
        SwaggerExtension.cs
        Generates swagger documentation when project is built and run
     */
    public static class SwaggerExtension
    {
        public static void ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            // TODO: Skoða response status kóða 
            services.AddSwaggerGen( c => {
                c.SwaggerDoc( "v1", new Info {
                    Version = "v1",
                    Title = "Galore Web API",
                    Description = "A web API for video system Galore",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "Asdis, Unnsteinn and Orn", Email = "asdis15@ru.is" },
                });
            });
        }
    }
}