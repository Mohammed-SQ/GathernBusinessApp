using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Data.SqlClient;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace GathernBusinessApp
{
    public partial class PropertyPage : ContentPage, INotifyPropertyChanged
    {
        // 1) ObservableCollection of Dictionary to back the CollectionView
        public ObservableCollection<Dictionary<string, object>> PropertyList { get; }
            = new ObservableCollection<Dictionary<string, object>>();

        // 2) Command with Dictionary parameter
        public Command<Dictionary<string, object>> SavePropertyCommand { get; }

        // 3) Your cloud connection string
        private readonly string _connectionString
            = "Data Source=SQL6032.site4now.net,1433;Initial Catalog=db_ab93cb_gathernapp;User Id=db_ab93cb_gathernapp_admin;Password=m1234567;";

        public PropertyPage()
        {
            InitializeComponent();
            BindingContext = this;

            // wire up the Save button
            SavePropertyCommand = new Command<Dictionary<string, object>>(SaveProperty);

            // load on startup
            LoadProperties();
        }

        private async void LoadProperties()
        {
            var userId = Preferences.Get("UserID", 0);
            if (userId <= 0)
            {
                await Shell.Current.GoToAsync("//SignInPage");
                return;
            }

            PropertyList.Clear();

            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                var sql = @"
SELECT  
  PropertyID, PropertyName, Category, City, Direction,
  Area, Bedrooms, SingleUnits, MasterUnits, Bathrooms,
  Amenities, Features, Description, CreatedDate, PhotoCount,
  UserID, Visibility
FROM Properties
WHERE UserID = @uid";

                using var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@uid", userId);

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    // Build a Dictionary for each row
                    var dict = new Dictionary<string, object>
                    {
                        ["PropertyID"] = reader.GetInt32(0),
                        ["PropertyName"] = reader.GetString(1),
                        ["Category"] = reader.GetString(2),
                        ["City"] = reader.GetString(3),
                        ["Direction"] = reader.GetString(4),
                        ["Area"] = reader.GetInt32(5),
                        ["Bedrooms"] = reader.GetInt32(6),
                        ["SingleUnits"] = reader.GetInt32(7),
                        ["MasterUnits"] = reader.GetInt32(8),
                        ["Bathrooms"] = reader.GetInt32(9),
                        ["Amenities"] = reader.IsDBNull(10) ? "" : reader.GetString(10),
                        ["Features"] = reader.IsDBNull(11) ? "" : reader.GetString(11),
                        ["Description"] = reader.IsDBNull(12) ? "" : reader.GetString(12),
                        ["CreatedDate"] = reader.GetDateTime(13),
                        ["PhotoCount"] = reader.GetInt32(14),
                        ["UserID"] = reader.GetInt32(15),
                        ["Visibility"] = reader.GetString(16),
                    };

                    // extra computed fields
                    dict["IsVisible"] = dict["Visibility"].ToString() == "معروض";
                    dict["UnitsText"] = $"{dict["Visibility"]} ({dict["PropertyID"]} وحدات)";

                    PropertyList.Add(dict);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("خطأ", $"فشل في تحميل العقارات: {ex.Message}", "موافق");
            }
        }

        private async void SaveProperty(Dictionary<string, object> prop)
        {
            // pull out your values
            var id = (int)prop["PropertyID"];
            var name = (string)prop["PropertyName"];
            var category = (string)prop["Category"];
            var city = (string)prop["City"];
            var direction = (string)prop["Direction"];
            var isVisible = (bool)prop["IsVisible"];

            // simple validation
            if (string.IsNullOrWhiteSpace(name)
             || string.IsNullOrWhiteSpace(category)
             || string.IsNullOrWhiteSpace(city)
             || string.IsNullOrWhiteSpace(direction))
            {
                await DisplayAlert("خطأ", "يرجى ملء جميع الحقول", "موافق");
                return;
            }

            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                var sql = @"
UPDATE Properties
   SET PropertyName = @n,
       Category     = @c,
       City         = @ci,
       Direction    = @d,
       Visibility   = @v
 WHERE PropertyID  = @id";

                using var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@c", category);
                cmd.Parameters.AddWithValue("@ci", city);
                cmd.Parameters.AddWithValue("@d", direction);
                cmd.Parameters.AddWithValue("@v", isVisible ? "معروض" : "غير معروض");

                await cmd.ExecuteNonQueryAsync();

                // update our dictionary so UI refreshes
                prop["Visibility"] = isVisible ? "معروض" : "غير معروض";
                prop["UnitsText"] = $"{prop["Visibility"]} ({id} وحدات)";
                OnPropertyChanged(nameof(PropertyList));

                await DisplayAlert("نجاح", "تم تحديث العقار بنجاح", "موافق");
            }
            catch (Exception ex)
            {
                await DisplayAlert("خطأ", $"فشل في تحديث العقار: {ex.Message}", "موافق");
            }
        }

        // bottom nav handlers
        private async void OnCalendarClicked(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//CalendarPage");

        private async void OnBookingClicked(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//BookingPage");

        private async void OnDashboardClicked(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//DashboardPage");

        private async void OnMoreClicked(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//ExtraPage");

        // INotifyPropertyChanged implementation
        public new event PropertyChangedEventHandler PropertyChanged;
        protected new void OnPropertyChanged(string propName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
