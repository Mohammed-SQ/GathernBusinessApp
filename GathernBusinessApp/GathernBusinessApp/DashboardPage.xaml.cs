using System;
using Microsoft.Maui.Controls;

namespace GathernBusinessApp;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        InitializeComponent();
    }

    private async void OnNotificationClicked(object sender, EventArgs e)
    {
        // TODO: Navigate or show notifications page
        await DisplayAlert("إشعار", "تم النقر على أيقونة الإشعارات", "حسناً");
    }

    private async void OnChatClicked(object sender, EventArgs e)
    {
        // TODO: Navigate to chat page
        await DisplayAlert("دردشة", "تم النقر على أيقونة الدردشة", "حسناً");
    }

    private async void OnSupportClicked(object sender, EventArgs e)
    {
        // TODO: Navigate to support page
        await DisplayAlert("الدعم", "تم النقر على أيقونة الدعم", "حسناً");
    }

    private async void OnCalendarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//CalendarPage");
    }

    private async void OnBookingClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//BookingPage");
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
