using Microsoft.Maui.Controls;

namespace FMMSRestaurant.Pages;

public partial class StaffOrdersPage : ContentPage
{
    public StaffOrdersPage()
    {
        InitializeComponent();
        BindingContext = Application.Current; // Set BindingContext to App
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