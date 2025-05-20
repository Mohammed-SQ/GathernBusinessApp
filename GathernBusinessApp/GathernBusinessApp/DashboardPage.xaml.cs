using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;

namespace GathernBusinessApp
{
    public partial class DashboardPage : ContentPage
    {
        private readonly string connectionString = "Provider=sqloledb;Data Source=SQL6032.site4now.net,1433;Initial Catalog=db_ab93cb_gathernapp;User Id=db_ab93cb_gathernapp_admin;Password=m1234567;";
        public ObservableCollection<Property> Properties { get; } = new();

        public DashboardPage()
        {
            InitializeComponent();
            PropertiesCollection.ItemsSource = Properties;
            LoadProperties();
        }

        private async void LoadProperties()
        {
            Properties.Clear();
            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();

                var query = "SELECT Id, HostId, Name, Location, Price, Rating, ImageUrl FROM Properties WHERE HostId = @HostId";
                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HostId", "host1");
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                    if (id == 0)
                    {
                        await DisplayAlert("Warning", "Invalid Property ID in database.", "OK");
                        continue;
                    }
                    var name = reader["Name"]?.ToString() ?? "Unknown Property";
                    var location = reader["Location"]?.ToString() ?? "Unknown Location";
                    var price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0;
                    var rating = reader["Rating"] != DBNull.Value ? Convert.ToDecimal(reader["Rating"]) : 0;
                    var imageUrl = reader["ImageUrl"]?.ToString() ?? "";

                    Properties.Add(new Property
                    {
                        Id = id,
                        HostId = "host1",
                        Name = name,
                        Location = location,
                        Price = price,
                        Rating = rating,
                        ImageUrl = imageUrl
                    });
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnAddPropertyClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AddPropertyPage");
        }

        private async void OnPropertySelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0 && e.CurrentSelection[0] is Property selectedProperty)
            {
                if (selectedProperty.Id <= 0)
                {
                    await DisplayAlert("Error", "Invalid Property ID selected.", "OK");
                    return;
                }
                await Shell.Current.GoToAsync($"//BookingsPage?propertyId={selectedProperty.Id}");
            }
        }
    }

    public class Property
    {
        public int Id { get; set; }
        public string HostId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Rating { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}