using System;
using Microsoft.Maui.Controls;

namespace GathernBusinessApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // If you ever want to go home (not used right now)
        async void OnExploreClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//DashboardPage");
        }

        // Navigate absolutely to AddPropertyPage
        async void OnAddPropertyClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AddPropertyPage");
        }
    }
}
