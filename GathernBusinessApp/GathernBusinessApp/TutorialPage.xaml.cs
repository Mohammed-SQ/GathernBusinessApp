using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace GathernBusinessApp
{
    public partial class TutorialPage : ContentPage
    {
        class Slide
        {
            public string Title, Subtitle, ImageFile;
            public Slide(string t, string s, string img)
                => (Title, Subtitle, ImageFile) = (t, s, img);
        }

        readonly List<Slide> _slides = new()
        {
            new("أولاً:",  "أضف بيانات و صور العقار",           "tutorial1.jpg"),
            new("ثانياً:", "حدد السعر والشروط",                "tutorial2.jpg"),
            new("ثالثاً:", "عقارك سيُعرض على أكثر من 2 مليون",  "tutorial3.jpg"),
            new("رابعاً:", "استعد لاستقبال الضيوف",             "tutorial4.jpg"),
            new("خامساً:", "بعد تأكيد الحجز يُوصلك إشعارات",     "tutorial5.jpg"),
            new("سادساً:", "بتوصلك الفلوس",                    "tutorial6.jpg"),
            new("سابعاً:", "اختر بلونك مستضيفاً للسياح...",      "tutorial7.jpg"),
        };

        int _index;

        public TutorialPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _index = 0;
            ShowSlide();
        }

        void ShowSlide()
        {
            var slide = _slides[_index];
            SlideTitle.Text = slide.Title;
            SlideSubtitle.Text = slide.Subtitle;
            SlideImage.Source = slide.ImageFile;
        }

        async Task AnimateSlide(int newIndex, bool backward = false)
        {
            // bounds
            if (newIndex < 0)
            {
                await Shell.Current.GoToAsync("//MainPage");
                return;
            }

            if (newIndex >= _slides.Count)
            {
                await Shell.Current.GoToAsync("//AddPropertyPage");
                return;
            }

            var dir = backward ? -1 : 1;
            uint duration = 200;
            double offset = dir * SlideContainer.Width;

            // Animate all out
            var outSlide = SlideContainer.TranslateTo(-offset, 0, duration, Easing.CubicIn);
            var outTitle = SlideTitle.TranslateTo(-offset, 0, duration, Easing.CubicIn);
            var outSubtitle = SlideSubtitle.TranslateTo(-offset, 0, duration, Easing.CubicIn);

            await Task.WhenAll(outSlide, outTitle, outSubtitle);

            // Update content
            _index = newIndex;
            ShowSlide();

            // Position off-screen (opposite side)
            SlideContainer.TranslationX = offset;
            SlideTitle.TranslationX = offset;
            SlideSubtitle.TranslationX = offset;

            // Animate all back in
            var inSlide = SlideContainer.TranslateTo(0, 0, duration, Easing.CubicOut);
            var inTitle = SlideTitle.TranslateTo(0, 0, duration, Easing.CubicOut);
            var inSubtitle = SlideSubtitle.TranslateTo(0, 0, duration, Easing.CubicOut);

            await Task.WhenAll(inSlide, inTitle, inSubtitle);
        }


        async void OnNextClicked(object sender, EventArgs e)
            => await AnimateSlide(_index + 1);

        async void OnBackTapped(object sender, EventArgs e)
            => await AnimateSlide(_index - 1, backward: true);

        async void OnSkipTapped(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//AddPropertyPage");
    }
}
