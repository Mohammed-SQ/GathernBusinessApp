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

            // register the routes
            Routing.RegisterRoute("boarding", typeof(BoardingPage));
            Routing.RegisterRoute("signinsplash", typeof(SignInSplashPage));
            Routing.RegisterRoute("signin", typeof(SignInPage));
            Routing.RegisterRoute("main", typeof(MainPage));

            // fire‐and‐forget our initial router
            _ = PerformInitialRouting();
        }

        async Task PerformInitialRouting()
        {
            // a tiny delay to ensure the Shell is fully loaded
            await Task.Delay(100);

            var hasBoarded = Preferences.Get("HasCompletedBoarding", false);
            var isLoggedIn = Preferences.Get("IsLoggedIn", false);

            if (!hasBoarded)
            {
                await GoToAsync("//signinsplash");
            }
            else if (!isLoggedIn)
            {
                await GoToAsync("//SplashPage");
            }
            else
            {
                await GoToAsync("//SplashPage");
            }
        }
    }
}
