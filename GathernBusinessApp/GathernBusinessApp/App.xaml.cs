using Microsoft.Maui.Controls;

namespace GathernBusinessApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            _ = Shell.Current.GoToAsync("//SplashPage"); // Use _ to handle the async task
        }
    }
}