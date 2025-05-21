using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

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

        async void OnContinueTapped(object sender, EventArgs e)
        {
            var phone = PhoneEntry.Text?.Trim();
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

            // navigate on success
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
