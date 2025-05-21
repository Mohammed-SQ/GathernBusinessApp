using Microsoft.Maui.Controls;

namespace GathernBusinessApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
