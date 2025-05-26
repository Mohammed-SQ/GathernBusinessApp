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
            await Shell.Current.GoToAsync("//TutorialPage");
        }

        // Navigate absolutely to AddPropertyPage
        private async void OnAddPropertyClicked(object sender, EventArgs e)
        {
            int userID = 1; // Replace with actual UserID retrieval (e.g., from authentication service or session)
            await Shell.Current.GoToAsync("//AddPropertyPage", new Dictionary<string, object>
    {
        { "userID", userID }
    });
        }
    }
}
