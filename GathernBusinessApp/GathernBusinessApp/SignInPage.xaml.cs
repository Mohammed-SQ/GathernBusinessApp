using System;
using System.Text.RegularExpressions;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Microsoft.Data.SqlClient;

namespace GathernBusinessApp
{
    public partial class SignInPage : ContentPage
    {
        const string ConnectionString =
            "Data Source=SQL6032.site4now.net,1433;" +
            "Initial Catalog=db_ab93cb_gathernapp;" +
            "User Id=db_ab93cb_gathernapp_admin;" +
            "Password=m1234567;" +
            "Encrypt=True;TrustServerCertificate=False;";

        public SignInPage()
        {
            InitializeComponent();
        }

        async void OnBackTapped(object sender, EventArgs e)
        {
            // Navigate back to boarding
            await Shell.Current.GoToAsync("//boarding");
        }

        void OnPhoneEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            // Remove non-digits, enforce max 9
            var raw = e.NewTextValue ?? "";
            var digits = Regex.Replace(raw, @"\D", "");
            if (digits.Length > 9)
                digits = digits.Substring(0, 9);

            if (digits.StartsWith("05"))
            {
                digits = digits.Substring(1);
            }

            if (digits != raw)
                PhoneEntry.Text = digits;

            // Enable only if exactly 9 digits and starts with '5'
            var ok = Regex.IsMatch(digits, @"^5\d{8}$");
            ContinueButton.IsEnabled = ok;
            ContinueButton.BackgroundColor = ok ? Color.FromArgb("#6200EE") : Colors.Gray;
        }

        async void OnContinueTapped(object sender, EventArgs e)
        {
            var phone = PhoneEntry.Text?.Trim();
            if (!Regex.IsMatch(phone ?? "", @"^5\d{8}$"))
            {
                await DisplayAlert("خطأ", "الرجاء إدخال رقم جوال صالح، يبدأ بـ5 ويتكون من 9 أرقام", "حسنًا");
                return;
            }

            int userId;
            try
            {
                await using var conn = new SqlConnection(ConnectionString);
                await conn.OpenAsync();

                const string sql = @"
DECLARE @UserID INT;
SELECT @UserID = UserID
  FROM dbo.Users
 WHERE Phone = @phone;

IF @UserID IS NULL
BEGIN
    INSERT INTO dbo.Users (Phone)
    VALUES (@phone);
    SET @UserID = SCOPE_IDENTITY();
END;

SELECT @UserID;";

                await using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@phone", phone);

                userId = (int)await cmd.ExecuteScalarAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("خطأ في الاتصال", ex.Message, "حسنًا");
                return;
            }

            // persist
            Preferences.Set("IsLoggedIn", true);
            Preferences.Set("UserPhone", phone);
            Preferences.Set("UserID", userId);

            // navigate
            await Shell.Current.GoToAsync("//main");
        }
    }
}
