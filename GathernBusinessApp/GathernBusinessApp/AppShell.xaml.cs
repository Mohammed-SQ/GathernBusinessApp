using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.Threading.Tasks;

namespace GathernBusinessApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // Fire-and-forget our startup router
            _ = PerformInitialRouting();
        }

        async Task PerformInitialRouting()
        {
            // Give the Shell a moment
            await Task.Delay(100);

            bool isLoggedIn = Preferences.Get("IsLoggedIn", false);
            bool hasBoarded = Preferences.Get("HasCompletedBoarding", false);

            if (!isLoggedIn)
            {
                // User has never signed in
                await GoToAsync("//signinsplash");
            }
            else if (!hasBoarded)
            {
                // User signed in, but never finished the tutorial
                await GoToAsync("//SplashPage");
            }
            else
            {
                // User signed in *and* completed boarding → straight into app
                await GoToAsync("//main");
            }
        }
    }
}
