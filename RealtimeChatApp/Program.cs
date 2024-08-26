using Microsoft.EntityFrameworkCore;
using RealtimeChatApp.Data;
using RealtimeChatApp.Hubs;
using RealtimeChatApp.Repository;
using RealtimeChatApp.Services;

namespace RealtimeChatApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            Configure(app);

            if (app.Environment.IsDevelopment())
            {
                CheckDbConnection(app);
            }

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration["AzureSQLDatabase:ConnectionString"]));

            services.AddSignalR().AddAzureSignalR(configuration["AzureSignalRService:ConnectionString"]);

            services.AddSingleton<ISentimentAnalysisService, SentimentAnalysisService>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
        }

        private static void Configure(WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapHub<ChatHub>("/chathub");
        }

        private static void CheckDbConnection(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                try
                {
                    if (!dbContext.Database.CanConnect())
                    {
                        throw new Exception("Cannot connect to the database.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to connect to database: {ex.Message}");
                }
            }
        }
    }
}
