using Microsoft.Maui.Controls;

namespace GathernBusinessApp
{
    public partial class BookingPage : ContentPage
    {
        private bool _filtersVisible;

        public BookingPage()
        {
            InitializeComponent();
            _filtersVisible = false;
            if (StatusPicker == null || SourcePicker == null)
            {
                // Log or handle the error if controls are not initialized
                System.Diagnostics.Debug.WriteLine("StatusPicker or SourcePicker not initialized.");
            }
        }

        private void OnToggleSearchClicked(object sender, EventArgs e)
        {
            _filtersVisible = !_filtersVisible;
            FilterPanel.IsVisible = _filtersVisible;
            BtnToggleSearch.Source = _filtersVisible ? "arrow_up_icon.png" : "arrow_down_icon.png";
        }

        private void OnLastBookingsTapped(object sender, EventArgs e)
        {
            LblLastBookings.TextColor = Color.FromArgb("#6200EE");
            LblLastBookings.FontAttributes = FontAttributes.Bold;
            LblUpcomingGuests.TextColor = Color.FromArgb("#8B7B7B");
            LblUpcomingGuests.FontAttributes = FontAttributes.None;
            // TODO: Load last bookings list
        }

        private void OnUpcomingGuestsTapped(object sender, EventArgs e)
        {
            LblUpcomingGuests.TextColor = Color.FromArgb("#6200EE");
            LblUpcomingGuests.FontAttributes = FontAttributes.Bold;
            LblLastBookings.TextColor = Color.FromArgb("#8B7B7B");
            LblLastBookings.FontAttributes = FontAttributes.None;
            // TODO: Load upcoming guests list
        }

        private void OnShowResultsClicked(object sender, EventArgs e)
        {
            if (StatusPicker != null && SourcePicker != null)
            {
                string status = StatusPicker.SelectedItem?.ToString() ?? "";
                string source = SourcePicker.SelectedItem?.ToString() ?? "";
                // TODO: Implement search logic with status and source
            }
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            if (StatusPicker != null && SourcePicker != null)
            {
                StatusPicker.SelectedIndex = -1;
                SourcePicker.SelectedIndex = -1;
                OnToggleSearchClicked(sender, e); // Hide the filter panel
            }
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
}