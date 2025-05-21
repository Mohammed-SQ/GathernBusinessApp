using Microsoft.Maui.Controls;

namespace GathernBusinessApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // register all our routes
            Routing.RegisterRoute("splash", typeof(SplashPage));
            Routing.RegisterRoute("boarding", typeof(TutorialPage));
            Routing.RegisterRoute("signinsplash", typeof(SignInSplashPage));
            Routing.RegisterRoute("signin", typeof(SignInPage));
            Routing.RegisterRoute("main", typeof(MainPage));
            Routing.RegisterRoute("addproperty", typeof(AddPropertyPage));
            Routing.RegisterRoute("dashboard", typeof(DashboardPage));
        }
    }
}
