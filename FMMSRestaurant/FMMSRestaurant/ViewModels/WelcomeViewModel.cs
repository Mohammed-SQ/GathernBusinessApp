using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FMMSRestaurant.ViewModels;

public partial class WelcomeViewModel : ObservableObject
{
    [RelayCommand]
    async Task NavigateToMenu()
    {
        await Shell.Current.GoToAsync("//MenuTablePage");
    }

    [RelayCommand]
    async Task NavigateToStaff()
    {
        await Shell.Current.GoToAsync("//StaffOrderPage");
    }
}