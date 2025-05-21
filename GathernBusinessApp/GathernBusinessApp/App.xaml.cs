using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace GathernBusinessApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // set Shell as the MainPage
            MainPage = new AppShell();

            // kick off our “which comes first?” logic
            _ = SetupInitialNavigation();
        }

        async Task SetupInitialNavigation()
        {
            // 1) If the user has never seen boarding → show boarding
            if (!Preferences.Get("HasCompletedBoarding", false))
            {
                await Shell.Current.GoToAsync("//boarding");
                return;
            }

            // 2) If they have boarded but not signed in → show the sign-in splash
            if (!Preferences.Get("IsLoggedIn", false))
            {
                await Shell.Current.GoToAsync("//signinsplash");
                return;
            }

            // 3) Otherwise, they’re fully in → go to your main page
            await Shell.Current.GoToAsync("//main");
        }
    }
}
