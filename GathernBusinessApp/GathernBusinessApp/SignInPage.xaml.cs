using Microsoft.Maui.Controls;

namespace GathernBusinessApp
{
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
        }

        async void OnContinueTapped(object sender, System.EventArgs e)
        {
            var phone = PhoneEntry.Text?.Trim();
            if (string.IsNullOrWhiteSpace(phone))
            {
                await DisplayAlert("خطأ", "الرجاء إدخال رقم الجوال", "حسنًا");
                return;
            }

            // TODO: call your back-end to verify/OTP…
            // assume success:
            Preferences.Set("IsLoggedIn", true);
            Preferences.Set("UserPhone", phone);

            await Shell.Current.GoToAsync("//main");
        }
    }
}
