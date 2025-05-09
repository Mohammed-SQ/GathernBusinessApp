using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace testing
{
    public partial class TaskManager : System.Web.UI.Page
    {
        // Connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["TaskManagerDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Load tasks when page loads
            if (!IsPostBack)
            {
                LoadTasks();
            }
        }

        [WebMethod]
        public static string CreateTask(string title, string description, string priority, string dueDate, string tags)
        {
            try
            {
                DateTime taskDueDate;
                if (!DateTime.TryParse(dueDate, out taskDueDate))
                {
                    return "Invalid due date format!";
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Tasks (Title, Description, Priority, Deadline, Tags, IsCompleted) " +
                                   "VALUES (@Title, @Description, @Priority, @Deadline, @Tags, @IsCompleted)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@Priority", priority);
                        cmd.Parameters.AddWithValue("@Deadline", taskDueDate);
                        cmd.Parameters.AddWithValue("@Tags", tags);
                        cmd.Parameters.AddWithValue("@IsCompleted", false); // Default value

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return "Task created successfully!";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        // Button click event for creating a task
        protected void btnCreateTask_Click(object sender, EventArgs e)
        {
            try
            {
                string taskTitle = txtTaskTitle.Text;
                string taskDescription = txtTaskDescription.Text;
                string taskPriority = ddlPriority.SelectedValue;
                DateTime taskDueDate;
                string taskTags = txtTags.Text;

                // Basic validation
                if (string.IsNullOrEmpty(taskTitle) || string.IsNullOrEmpty(taskDescription))
                {
                    lblMessage.Text = "Title and Description are required!";
                    lblMessage.Visible = true;
                    return;
                }

                if (!DateTime.TryParse(txtDueDate.Text, out taskDueDate))
                {
                    lblMessage.Text = "Please enter a valid due date!";
                    lblMessage.Visible = true;
                    return;
                }

                // Insert data into Tasks table
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Tasks (Title, Description, Priority, Deadline, Tags, IsCompleted) " +
                                   "VALUES (@Title, @Description, @Priority, @Deadline, @Tags, @IsCompleted)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", taskTitle);
                        cmd.Parameters.AddWithValue("@Description", taskDescription);
                        cmd.Parameters.AddWithValue("@Priority", taskPriority);
                        cmd.Parameters.AddWithValue("@Deadline", taskDueDate);
                        cmd.Parameters.AddWithValue("@Tags", taskTags);
                        cmd.Parameters.AddWithValue("@IsCompleted", false); // Default value for new tasks

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                // Clear the form fields
                ClearForm();

                // Reload tasks to reflect changes in the UI
                LoadTasks();

                // Redirect to the same page to prevent form resubmission on refresh
                Response.Redirect(Request.Url.ToString(), true);
            }
            catch (Exception ex)
            {
                // Log the exception message to diagnose any errors during data insertion
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.Visible = true;
            }
        }

        // Load tasks from the database and display them
        private void LoadTasks()
        {
            taskContainer.InnerHtml = ""; // Clear previous tasks

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Tasks WHERE IsCompleted = 0";  // Fetch only active tasks

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Dynamically create task HTML
                        string taskHtml = $@"
                            <div class='task-card'>
                                <input type='checkbox' class='checkbox' onchange='moveToCompleted(this)' data-task-id='{reader["TaskID"]}' />
                                <div class='task-details'>
                                    <div class='task-title'>{reader["Title"]}</div>
                                    <div class='task-meta'>
                                        <span>Priority: {reader["Priority"]}</span>
                                        <span>Tags: {reader["Tags"]}</span>
                                    </div>
                                    <p>{reader["Description"]}</p>
                                    <div class='task-due'>Due: {reader["Deadline"]}</div>
                                </div>
                                <button class='delete-btn' data-task-id='{reader["TaskID"]}' onclick='deleteTask(this)'>X</button>
                            </div>";

                        // Add the HTML content to the container
                        taskContainer.InnerHtml += taskHtml;
                    }
                }
            }
        }

        // Clear form inputs
        private void ClearForm()
        {
            txtTaskTitle.Text = "";
            txtTaskDescription.Text = "";
            ddlPriority.SelectedIndex = 0;
            txtDueDate.Text = "";
            txtTags.Text = "";
        }
    }
}
