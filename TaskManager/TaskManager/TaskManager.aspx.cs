using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace TaskManager
{
    public partial class TaskManager : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["TaskManagerDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTasks();
            }
        }

        protected void btnAddTask_Click(object sender, EventArgs e)
        {
            string title = txtTaskTitle.Text;
            string description = txtTaskDescription.Text;
            string priority = ddlPriority.SelectedValue;
            string tags = txtTags.Text;
            string dueDate = txtDueDate.Text;

            if (string.IsNullOrEmpty(title))
            {
                lblMessage.Text = "Task title is required.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Tasks (Title, Description, Priority, DueDate, Tags) VALUES (@Title, @Description, @Priority, @DueDate, @Tags)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Priority", priority);
                cmd.Parameters.AddWithValue("@DueDate", dueDate);
                cmd.Parameters.AddWithValue("@Tags", tags);

                conn.Open();
                cmd.ExecuteNonQuery();
                lblMessage.Text = "Task added successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }

            LoadTasks();
        }

        private void LoadTasks()
        {
            taskContainer.Controls.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Title, Description, Priority, DueDate, Tags FROM Tasks";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Panel taskPanel = new Panel
                    {
                        CssClass = "task-card"
                    };

                    Label lblTask = new Label
                    {
                        Text = $"<b>{reader["Title"]}</b> - {reader["Priority"]} Priority<br />Due: {reader["DueDate"]} - Tags: {reader["Tags"]}<br />{reader["Description"]}"
                    };

                    taskPanel.Controls.Add(lblTask);
                    taskContainer.Controls.Add(taskPanel);
                }
            }
        }
    }
}
