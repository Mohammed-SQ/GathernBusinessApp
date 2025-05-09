using System;
using System.Configuration;
using System.Data.SqlClient;

namespace TaskManager1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear error message on page load
            lblErrorMessage.Visible = false;
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            // Only proceed if the page validation is successful
            if (Page.IsValid)
            {
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();

                string connString = ConfigurationManager.ConnectionStrings["TaskManager1"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "SELECT Username, Email FROM Login WHERE Email = @Email AND Password = @Password";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Store user session after successful login
                                Session["Username"] = reader["Username"].ToString();
                                Session["Email"] = reader["Email"].ToString();

                                // Redirect to the main page of the task manager
                                Response.Redirect("TaskManager.aspx");
                            }
                            else
                            {
                                // Display error message if credentials are incorrect
                                lblErrorMessage.Visible = true;
                                lblErrorMessage.Text = "Invalid email or password. Please try again.";
                            }
                        }
                    }
                }
            }
        }
    }
}
