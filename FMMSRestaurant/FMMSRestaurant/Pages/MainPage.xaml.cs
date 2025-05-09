using Microsoft.Maui.Controls;

namespace FMMSRestaurant.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = Application.Current; // Set BindingContext to App
    }

    private void OnPhoneCheckBoxChanged(object sender, CheckedChangedEventArgs e)
    {
        PhoneEntry.IsEnabled = e.Value;
        PhoneEntry.IsVisible = e.Value;
        PhoneError.IsVisible = false;
        if (!e.Value) PhoneEntry.Text = "";
    }

    private async void OnContinueClicked(object sender, EventArgs e)
    {
        bool isValid = true;

        // Validate full name
        if (string.IsNullOrWhiteSpace(FullNameEntry.Text))
        {
            FullNameError.IsVisible = true;
            isValid = false;
        }
        else
        {
            FullNameError.IsVisible = false;
        }

        // Validate phone number if provided
        if (PhoneCheckBox.IsChecked && !string.IsNullOrWhiteSpace(PhoneEntry.Text))
        {
            string phone = PhoneEntry.Text.Replace(" ", "").Replace("+966", "");
            if (!phone.StartsWith("5") || phone.Length != 9 || !long.TryParse(phone, out _))
            {
                PhoneError.IsVisible = true;
                isValid = false;
            }
            else
            {
                PhoneError.IsVisible = false;
            }
        }
        else
        {
            PhoneError.IsVisible = false;
        }

        if (isValid)
        {
            App.IsFormSubmitted = true; // Update the global state
            // Update only the main content area, keeping the sidebars
            MainContentGrid.Children.Clear();
            MainContentGrid.BackgroundColor = Colors.White;
            MainContentGrid.Children.Add(new Label
            {
                Text = "Welcome to FMMS Restaurant",
                TextColor = Colors.Black,
                FontSize = 24,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            });
            await Task.Delay(1000); // Brief delay for user feedback
            await Shell.Current.GoToAsync("//MenuPage"); // Navigate to MenuPage after submission
        }
    }

    private async void OnHomeClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void OnMenuClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MenuPage");
    }

    private async void OnTableClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//TablePage");
    }

    private async void OnOrderClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//OrderPage");
    }

    private async void OnStaffOrdersClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//StaffOrdersPage");
    }

    private async void OnPlaceOrderClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Order Placed", "Your order has been placed!", "OK");
    }
}