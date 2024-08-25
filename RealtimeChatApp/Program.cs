using RealtimeChatApp.Hubs;
using RealtimeChatApp.Services;

namespace RealtimeChatApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = CreateBuilder(args);

            builder.Configuration.AddUserSecrets<Program>();

            var app = builder.Build();

			ConfigureApp(app);

			app.Run();
		}

		private static WebApplicationBuilder CreateBuilder(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton(new SentimentAnalysisService("<Your-Endpoint>", "<Your-API-Key>"));

            builder.Services.AddSignalR().AddAzureSignalR(builder.Configuration["AzureSignalRConnectionString"]);

            return builder;
		}

		private static void ConfigureApp(WebApplication app)
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

			// Configure endpoints
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.MapHub<ChatHub>("/chathub");
		}
	}

}
