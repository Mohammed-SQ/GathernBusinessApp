using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Microsoft.Data.SqlClient;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GathernBusinessApp
{
    public partial class SignInPage : ContentPage
    {
        // your Azure/SQL connection string
        const string ConnectionString =
            "Data Source=SQL6032.site4now.net,1433;" +
            "Initial Catalog=db_ab93cb_gathernapp;" +
            "User Id=db_ab93cb_gathernapp_admin;" +
            "Password=m1234567;" +
            "Encrypt=True;TrustServerCertificate=False;";

        public SignInPage()
        {
            InitializeComponent();
            ContinueButton.IsEnabled = false;
            PhoneEntry.TextChanged += OnPhoneEntryTextChanged;
        }

        // only allow digits, must start with '5' and be exactly 9 chars
        void OnPhoneEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            // strip non-digits
            var digits = Regex.Replace(e.NewTextValue ?? "", @"\D", "");
            if (digits != e.NewTextValue)
                PhoneEntry.Text = digits;

            // must start with 5
            if (digits.Length > 0 && digits[0] != '5')
            {
                ContinueButton.IsEnabled = false;
                return;
            }

            // enable only when exactly 9 digits
            ContinueButton.IsEnabled = Regex.IsMatch(digits, @"^5\d{8}$");
        }

        // pressed the back arrow
        async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//main");
        }

        // tapped "المتابعة"
        async void OnContinueTapped(object sender, EventArgs e)
        {
            var phone = PhoneEntry.Text;
            if (string.IsNullOrWhiteSpace(phone))
            {
                await DisplayAlert("خطأ", "الرجاء إدخال رقم الجوال", "حسنًا");
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

SELECT @UserID;
";

                await using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@phone", phone);

                userId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }
            catch (Exception ex)
            {
                await DisplayAlert("خطأ في الاتصال", ex.Message, "حسنًا");
                return;
            }

            // save into preferences
            Preferences.Set("IsLoggedIn", true);
            Preferences.Set("UserPhone", phone);
            Preferences.Set("UserID", userId);

            // go to main
            await Shell.Current.GoToAsync("//main");
        }
    }
}
