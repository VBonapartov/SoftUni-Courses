namespace BookShop.Notifications
{
    using BookShop.Infrastructure;
    using BookShop.Notifications.Hub;
    using BookShop.Notifications.Infrastructure;
    using BookShop.Notifications.Messages;
    using BookShop.Services.Identity;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                 //.AddCors()
                .AddScoped<ICurrentTokenService, CurrentTokenService>()
                .AddTransient<JwtCookieAuthenticationMiddleware>()
                
                // Използва се само когато token-a се изпраща директно, тъй като SignalR 
                // не го добавя в Authorization хедър
                //.AddTokenAuthentication(
                //    this.Configuration,
                //    JwtConfiguration.BearerEvents)
                
                .AddTokenAuthentication(this.Configuration)
                .AddMessaging(typeof(BookCreatedConsumer))
                .AddSignalR();

            services.AddCors(options =>
            {
                options
                    .AddPolicy(name: MyAllowSpecificOrigins,
                            builder =>
                            {
                                builder
                                    .WithOrigins("https://localhost:44338")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowCredentials();
                            });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseRouting()
                .UseCors(MyAllowSpecificOrigins)
                //.UseCors(options => options
                //    .WithOrigins("https://localhost:44338")
                //    .AllowAnyHeader()
                //    .AllowAnyMethod()
                //    .AllowCredentials())
                .UseMiddleware<JwtCookieAuthenticationMiddleware>()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints
                .MapHub<NotificationsHub>("/notifications"));
        }
    }
}