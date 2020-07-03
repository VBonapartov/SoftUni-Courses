namespace BookShop.Reviews
{
    using BookShop.Infrastructure;
    using BookShop.Reviews.Data;
    using BookShop.Reviews.Services.Reviews;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddWebService<ReviewsDbContext>(this.Configuration)
                .AddTransient<IReviewService, ReviewService>()
                .AddMessaging();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseWebService(env)
                .Initialize();
        }
    }
}