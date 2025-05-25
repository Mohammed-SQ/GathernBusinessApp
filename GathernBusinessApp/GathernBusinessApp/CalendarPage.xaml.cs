using Microsoft.Maui.Controls;

namespace GathernBusinessApp;

public partial class CalendarPage : ContentPage
{
    public CalendarPage()
    {
        InitializeComponent();
    }

    private async void OnBookingClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//BookingPage");
    }

    private async void OnDashboardClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//DashboardPage");
    }

    private async void OnPropertyClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//PropertyPage");
    }

    private async void OnMoreClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//ExtraPage");
    }
}
