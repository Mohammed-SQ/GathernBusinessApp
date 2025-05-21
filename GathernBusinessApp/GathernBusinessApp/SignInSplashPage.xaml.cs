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

            LettersStack.Children.Clear();
            foreach (var c in _letters)
            {
                LettersStack.Children.Add(new Label
                {
                    Text = c.ToString(),
                    FontSize = 60,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = c == 'n'
                              ? Color.FromArgb("#FF9F1C")
                              : Color.FromArgb("#6200EE"),
                    Opacity = 0,
                    TranslationY = -30
                });
            }

            for (int i = 0; i < LettersStack.Children.Count; i++)
            {
                var lbl = (Label)LettersStack.Children[i];
                await Task.WhenAll(
                  lbl.FadeTo(1, 300, Easing.CubicOut),
                  lbl.TranslateTo(0, 0, 300, Easing.SpringOut)
                );
                await Task.Delay(100);
            }

            await SubtitleLabel.FadeTo(1, 500, Easing.CubicInOut);
            await Task.Delay(800);

            await Shell.Current.GoToAsync("//boarding");
        }
    }
}
