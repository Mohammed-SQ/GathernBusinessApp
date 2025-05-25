using Microsoft.Maui.Controls;

namespace GathernBusinessApp;

public partial class BookingPage : ContentPage
{
    public BookingPage()
    {
        InitializeComponent();
    }

    private async void OnCalendarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//CalendarPage");
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
