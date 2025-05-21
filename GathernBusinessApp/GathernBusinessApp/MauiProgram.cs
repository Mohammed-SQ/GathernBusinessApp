using Microsoft.Maui;
using Microsoft.Maui.Hosting;

// brings in .UseMauiCommunityToolkit()
using CommunityToolkit.Maui;
// brings in the MediaElement control registrations

namespace GathernBusinessApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                // Initialize the Community Toolkit itself
                .UseMauiCommunityToolkit()
                // and then specifically register the MediaElement feature
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    // …other fonts…
                });

            return builder.Build();
        }
    }
}
