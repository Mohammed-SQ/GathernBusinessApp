using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;

namespace GathernBusinessApp
{
    public partial class SplashPage : ContentPage
    {
        private readonly string[] logoImages = new string[]
        {
            "image1.jpg",
            "image2.jpg",
            "image3.jpg",
            "image4.jpg",
            "image5.jpg",
            "image6.jpg",
            "image7.jpg"
        };

        public SplashPage()
        {
            InitializeComponent();
            StartAnimation();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await Task.Delay(2000);                   // show brand briefly
            bool boarded = Preferences.Get("HasCompletedBoarding", false);
            bool loggedIn = Preferences.Get("IsLoggedIn", false);

            if (!boarded)
                await Shell.Current.GoToAsync("//boarding");
            else if (!loggedIn)
                await Shell.Current.GoToAsync("//signinsplash");
            else
                await Shell.Current.GoToAsync("//main");
        }
        private async void StartAnimation()
        {
            System.Diagnostics.Debug.WriteLine("Splash animation started");
            foreach (var image in logoImages)
            {
                LogoPlaceholder.Source = image;
                LogoPlaceholder.TranslationY = 1000; // Reset to bottom

                // Slide up to center (TranslationY = 0) - 500ms
                await LogoPlaceholder.TranslateTo(0, 0, 200, Easing.SinInOut);

                // Pause at the center for 0.5 seconds
                await Task.Delay(250);

                // Slide up to top and disappear (TranslationY = -1000) - 500ms
                await LogoPlaceholder.TranslateTo(0, -1000, 200, Easing.SinInOut);

                // Small delay before the next image
                await Task.Delay(50);
            }

            System.Diagnostics.Debug.WriteLine("Splash animation completed, navigating to MainPage");
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}