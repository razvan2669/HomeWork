using Microsoft.Extensions.Logging;
using _18.Pages;
using _18.Services;
using _18.ViewModels;

namespace _18
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Services
            builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddSingleton<StoreService>();

            // ViewModels
            builder.Services.AddTransient<StudentsViewModel>();
            builder.Services.AddTransient<StoreViewModel>();
            builder.Services.AddTransient<CartViewModel>();
            builder.Services.AddTransient<ProductDetailViewModel>();

            // Pages
            builder.Services.AddTransient<StudentsPage>();
            builder.Services.AddTransient<StorePage>();
            builder.Services.AddTransient<CartPage>();
            builder.Services.AddTransient<ProductDetailPage>();

            return builder.Build();
        }
    }
}
