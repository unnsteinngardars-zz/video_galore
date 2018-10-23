using System.Threading.Tasks;
using Galore.Repositories.Context;
using Galore.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Galore.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.ConfigureDependencyInjections();
            services.ConfigureDatabase(Configuration);

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.ConfigureExceptionHandler();
            app.UseHttpsRedirection();
            using(var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope()) {
                var context = scope.ServiceProvider.GetService<GaloreDbContext>();
                context.Database.Migrate();
                context.SeedDatabase();
            }

            app.UseMvc();
            app.ConfigureAutoMapper();
            app.UseSwagger();
            app.UseSwaggerUI( c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Galore Web API V1");
            });
        }

    }
}
