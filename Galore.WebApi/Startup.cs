using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galore.Repositories.Context;
using Galore.Repositories.Implementations;
using Galore.Repositories.Interfaces;
using Galore.Services.implementations;
using Galore.Services.Interfaces;
using Galore.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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

            services.AddDbContext<UserContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("Galore.WebApi")));

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITapeService, TapeService>();
            services.AddTransient<ILoanService, LoanService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IRecommendationService, RecommendationService>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITapeRepository, TapeRepository>();
            services.AddTransient<ILoanRepository, LoanRepository>();

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
            app.UseMvc();

        }
    }
}
