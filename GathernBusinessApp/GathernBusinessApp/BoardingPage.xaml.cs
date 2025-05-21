using Microsoft.Maui.Controls;

namespace GathernBusinessApp
{
    public partial class BoardingPage : ContentPage
    {
        public BoardingPage()
        {
            InitializeComponent();
        }

        // Navigate to your “Register Property” flow
        async void OnRegisterPropertyClicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("//signin");
        }

        // Navigate to your “Host Sign-In” flow
        async void OnSignInClicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("//signin");
        }
    }
}
