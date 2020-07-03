namespace BookShop.Statistics
{
    using BookShop.Infrastructure;
    using BookShop.Messages.Books;
    using BookShop.Services;
    using BookShop.Statistics.Data;
    using BookShop.Statistics.Messages;
    using BookShop.Statistics.Services.BookViews;
    using BookShop.Statistics.Services.ReviewViews;
    using BookShop.Statistics.Services.Statistics;
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
                .AddWebService<StatisticsDbContext>(this.Configuration)
                .AddTransient<IDataSeeder, StatisticsDataSeeder>()
                .AddTransient<IBookViewService, BookViewService>()
                .AddTransient<IReviewViewService, ReviewViewService>()
                .AddTransient<IStatisticsService, StatisticsService>()
                .AddMessaging(typeof(ReviewCreatedConsumer), 
                              typeof(ReviewViewedConsumer),
                              typeof(BookCreatedConsumer),
                              typeof(BookViewedConsumer));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseWebService(env)
                .Initialize();
        }
    }
}