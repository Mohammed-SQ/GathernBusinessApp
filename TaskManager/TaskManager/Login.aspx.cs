using System;
using System.Configuration;
using System.Data.SqlClient;

namespace TaskManager
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Any initialization or page load logic can go here.
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Response.Write("<script>alert('Please fill in both email and password.');</script>");
                return;
            }

            string connString = ConfigurationManager.ConnectionStrings["TaskManagerDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT COUNT(*) FROM Login WHERE Email = @Email AND Password = @Password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    conn.Close();

                    if (count == 1)
                    {
                        Response.Redirect("TaskManager.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid email or password. Please try again.');</script>");
                    }
                }
            }
        }
    }
}
