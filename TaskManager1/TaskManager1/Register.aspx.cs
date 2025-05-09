using System;
using System.Data.SqlClient;
using System.Configuration;

namespace TaskManager1
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // No initialization required for now
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string birthday = txtBirthday.Text.Trim();  // Get the Birthday value
            string aboutYourself = txtAboutYourself.Text.Trim();  // Get the AboutYourself value

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                // Optionally handle validation on the server side if needed
                return;
            }

            string connString = ConfigurationManager.ConnectionStrings["TaskManager1"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "INSERT INTO Login (Username, Email, Password, Birthday, AboutYourself) " +
                                   "VALUES (@Username, @Email, @Password, @Birthday, @AboutYourself)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters for the required fields
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);

                        // Add parameters for the optional fields, using DBNull if they are empty
                        cmd.Parameters.AddWithValue("@Birthday", string.IsNullOrEmpty(birthday) ? DBNull.Value : (object)birthday);
                        cmd.Parameters.AddWithValue("@AboutYourself", string.IsNullOrEmpty(aboutYourself) ? DBNull.Value : (object)aboutYourself);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                // Optionally redirect the user to a success page or login page
                Response.Redirect("/Login.aspx");
            }
            catch (Exception ex)
            {
                // Handle any errors (e.g., log the error or display a message to the user)
                Response.Write($"<script>alert('An error occurred: {ex.Message}');</script>");
            }
        }
    }
}
