using System;
using System.Data;
using System.Data.SqlClient;

namespace TaskManager
{
    public partial class Tasks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTasks();
            }
        }

        private void LoadTasks()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TaskManagerDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Title, Description, Deadline FROM Tasks";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable tasks = new DataTable();
                adapter.Fill(tasks);
                TaskGrid.DataSource = tasks;
                TaskGrid.DataBind();
            }
        }
    }
}
