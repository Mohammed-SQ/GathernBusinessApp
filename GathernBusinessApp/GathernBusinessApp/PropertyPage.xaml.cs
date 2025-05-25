using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace GathernBusinessApp;

public partial class PropertyPage : ContentPage
{
    public ObservableCollection<Property> Properties { get; set; }

    public PropertyPage()
    {
        InitializeComponent();

        Properties = new ObservableCollection<Property>
        {
            new Property
            {
                Title = "فندق",
                Location = "الخبر --",
                UnitsText = "1 وحدات",
                Image = "property_sample.png"
            },
            new Property
            {
                Title = "فندق",
                Location = "كود الوحدة 042195",
                UnitsText = "غير معروض (اوف لاين)",
                Image = "property_sample.png"
            }
        };

        BindingContext = this;
    }

    // Navigation methods
    private async void OnCalendarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//CalendarPage");
    }

    private async void OnBookingClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//BookingPage");
    }

    private async void OnDashboardClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//DashboardPage");
    }

    private async void OnMoreClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//ExtraPage");
    }
}

public class Property
{
    public string Title { get; set; }
    public string Location { get; set; }
    public string UnitsText { get; set; }
    public string Image { get; set; }
}
