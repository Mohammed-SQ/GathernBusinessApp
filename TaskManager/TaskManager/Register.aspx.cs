using System;
using System.Data.SqlClient;
using System.Configuration;

namespace TaskManager
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string username = Request.Form["txtUsername"];
            string email = Request.Form["txtEmail"];
            string password = Request.Form["txtPassword"];

            string connString = ConfigurationManager.ConnectionStrings["TaskManagerDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO Login (Username, Email, Password) VALUES (@Username, @Email, @Password)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
