using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using FMMSRestaurant.ViewModels;
using FMMSRestaurant.Pages;

namespace FMMSRestaurant;

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

        // Register services for dependency injection
        builder.Services.AddSingleton<MenuPageViewModel>();
        builder.Services.AddTransient<MenuPage>();

        return builder.Build();
    }
}