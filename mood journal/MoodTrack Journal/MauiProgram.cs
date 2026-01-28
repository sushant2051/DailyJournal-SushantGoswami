using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoodTrack_Journal.Data;
using MoodTrack_Journal.Services;

namespace MoodTrack_Journal
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            // Configure SQLite database
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "moodtrackjournal.db");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            // Register services
            builder.Services.AddScoped<IJournalService, JournalService>();
            builder.Services.AddScoped<IMoodService, MoodService>();

            var app = builder.Build();

            // Ensure database is created
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.EnsureCreated();
            }

            return app;
        }
    }
}
