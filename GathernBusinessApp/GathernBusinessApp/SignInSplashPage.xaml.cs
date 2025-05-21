// SignInSplashPage.xaml.cs
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GathernBusinessApp
{
    public partial class SignInSplashPage : ContentPage
    {
        readonly List<char> _letters = new() { 'G', 'a', 't', 'h', 'e', 'r', 'n' };

        public SignInSplashPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // PHASE 1: one big letter in center
            foreach (var c in _letters)
            {
                CenterLetterHolder.Content = new Label
                {
                    Text = c.ToString(),
                    FontSize = 200,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = c == 'n'
                                        ? Color.FromArgb("#FF9F1C")
                                        : Color.FromArgb("#6200EE"),
                    Opacity = 0
                };

                var lbl = (Label)CenterLetterHolder.Content;
                await lbl.FadeTo(1, 200, Easing.CubicOut);
                await Task.Delay(200);
                await lbl.FadeTo(0, 200, Easing.CubicIn);
            }

            // PHASE 2: full word assembly
            CenterLetterHolder.IsVisible = false;
            WordStack.IsVisible = true;

            foreach (var c in _letters)
            {
                var lbl = new Label
                {
                    Text = c.ToString(),
                    FontSize = 200,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = c == 'n'
                                        ? Color.FromArgb("#FF9F1C")
                                        : Color.FromArgb("#6200EE"),
                    Opacity = 0,
                    TranslationX = 300
                };
                WordStack.Children.Add(lbl);

                await Task.WhenAll(
                    lbl.FadeTo(1, 200, Easing.CubicOut),
                    lbl.TranslateTo(0, 0, 200, Easing.CubicOut)
                );
            }

            // PHASE 3: subtitle slide in
            await Task.WhenAll(
                SubtitleLabel.FadeTo(1, 300, Easing.CubicInOut),
                SubtitleLabel.TranslateTo(0, 0, 300, Easing.CubicInOut)
            );

            // Hold for 3 seconds before navigating
            await Task.Delay(3000);

            await Shell.Current.GoToAsync("//boarding");
        }
    }
}
