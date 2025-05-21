using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.Collections.Generic;

namespace GathernBusinessApp
{
    public partial class BoardingPage : ContentPage
    {
        // simple DTO for each slide
        class Slide
        {
            public string Title { get; set; } = "";
            public string Subtitle { get; set; } = "";
            public string Image { get; set; } = "";
        }

        // your three boarding slides
        readonly List<Slide> _slides = new()
        {
            new() { Title = "أولاً:",  Subtitle = "أضف بيانات و صور العقار", Image = "boarding1.jpg" },
            new() { Title = "ثانياً:", Subtitle = "حدد السعر والشروط",    Image = "boarding2.jpg" },
            new() { Title = "ثالثاً:", Subtitle = "احصل على طلبات",       Image = "boarding3.jpg" },
        };

        int _idx = 0;

        public BoardingPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _idx = 0;
            ShowSlide();
        }

        void ShowSlide()
        {
            var current = _slides[_idx];
            SlideTitle.Text = current.Title;
            SlideSubtitle.Text = current.Subtitle;
            SlideImage.Source = current.Image;
        }

        async void OnNextTapped(object sender, System.EventArgs e)
        {
            if (_idx < _slides.Count - 1)
            {
                _idx++;
                ShowSlide();
            }
            else
            {
                // mark onboarding done
                Preferences.Set("HasCompletedBoarding", true);
                // navigate to your SignIn page (make sure your Shell route is "//signin")
                await Shell.Current.GoToAsync("//signin");
            }
        }

        async void OnSkipTapped(object sender, System.EventArgs e)
        {
            // skip straight to sign in
            Preferences.Set("HasCompletedBoarding", true);
            await Shell.Current.GoToAsync("//signin");
        }
    }
}
