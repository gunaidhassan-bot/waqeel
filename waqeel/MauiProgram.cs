using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;
using waqeel.Services;

namespace waqeel
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

            // تسجيل الخدمات اللازمة
            builder.Services.AddSingleton<DataService>();
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddTransient<MainPage>();

            return builder.Build();
        }
    }
}
