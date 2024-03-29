using Galore.Repositories.Context;
using Galore.Repositories.Implementations;
using Galore.Repositories.Interfaces;
using Galore.Services.Implementations;
using Galore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Galore.WebApi.Extensions
{
    /**
        DIExtension
        Configures all dependency injections for the application
     */
    public static class DIExtension
    {
        public static void ConfigureDependencyInjections(this IServiceCollection services)
        {
            // Dependency Injections for all services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITapeService, TapeService>();
            services.AddTransient<ILoanService, LoanService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IRecommendationService, RecommendationService>();
            services.AddTransient<ILogService, LogService>();
            // Dependency Injections for all repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITapeRepository, TapeRepository>();
            services.AddTransient<ILoanRepository, LoanRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
        }
    }
}