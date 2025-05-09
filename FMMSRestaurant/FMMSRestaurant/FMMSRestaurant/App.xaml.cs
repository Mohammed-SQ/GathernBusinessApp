using Microsoft.Maui.Controls;

namespace FMMSRestaurant;

public partial class App : Application
{
    public static bool IsFormSubmitted { get; set; } = false;

    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }
}