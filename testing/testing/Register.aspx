using System;
using System.Data.SqlClient;
using System.Web.UI;

public partial class Register : Page
{
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text;
        string password = txtPassword.Text;
        string email = txtEmail.Text;

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        using (SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString))
        {
            string query = "INSERT INTO Users (Username, PasswordHash, Email) VALUES (@Username, @PasswordHash, @Email)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
            cmd.Parameters.AddWithValue("@Email", email);

            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('Registration successful! Please log in.');</script>");
                Response.Redirect("Login.aspx");
            }
            catch (SqlException ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }
    }
}