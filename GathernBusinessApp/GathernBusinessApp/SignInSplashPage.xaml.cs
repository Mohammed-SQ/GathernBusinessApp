using Microsoft.Maui.Controls;

namespace GathernBusinessApp
{
    public partial class SignInSplashPage : ContentPage
    {
        public SignInSplashPage()
        {
            InitializeComponent();
        }

        async void OnContinueToSignIn(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//signin");

    }

}
