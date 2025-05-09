using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace TaskManager1
{
    public partial class TaskManager : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["TaskManager1"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            lblWelcome.Text = $"{Session["Username"]}";

            if (!IsPostBack)
            {
                InitializePage();
                LoadActiveTasks();
            }
        }

        private void InitializePage()
        {
            SetProfilePlaceholders();
            LoadAboutYourself();
            LoadActiveTasks();
            LoadCompletedTasks();
            HighlightTodayDate();
            HighlightCalendarDates();
            ActiveTabField.Value = "activeTab";
        }

        private void SetProfilePlaceholders()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Username, AboutYourself FROM Login WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", Session["Email"]?.ToString() ?? string.Empty);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    if (Username != null)
                        Username.Attributes["placeholder"] = reader["Username"]?.ToString() ?? "Enter your username";

                    if (AboutYourself != null)
                        AboutYourself.Attributes["placeholder"] = reader["AboutYourself"]?.ToString() ?? "Tell us about yourself...";
                }
            }
        }

        protected string GetPriorityText(object priority)
        {
            if (priority == null) return "Unknown";

            switch (Convert.ToInt32(priority))
            {
                case 1: return "Low";
                case 2: return "Medium";
                case 3: return "High";
                default: return "Unknown";
            }
        }


        protected void TaskCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox taskCheckbox = (CheckBox)sender; 
            GridViewRow row = (GridViewRow)taskCheckbox.NamingContainer; 

            int taskId = Convert.ToInt32(ActiveTasksGrid.DataKeys[row.RowIndex].Value); 
            bool isCompleted = taskCheckbox.Checked; 

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Tasks SET IsCompleted = @IsCompleted WHERE TaskID = @TaskID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IsCompleted", isCompleted);
                cmd.Parameters.AddWithValue("@TaskID", taskId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadActiveTasks();
            LoadCompletedTasks();
        }


        private void LoadAboutYourself()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT AboutYourself FROM Login WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", Session["Email"]?.ToString());

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                }
            }
        protected void DeleteAllCompleted_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Tasks WHERE IsCompleted = 1"; 
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

          
            LoadActiveTasks();
            LoadCompletedTasks();
        }

        protected void TaskCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            DateTime currentDate = e.Day.Date;
            string userEmail = Session["Email"]?.ToString();
            bool hasTask = false;
            bool isHighPriority = false;
            string taskDetails = "";

            if (!string.IsNullOrEmpty(userEmail))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Updated query: Exclude completed tasks
                    string query = @"
                SELECT Title, Description, 
                CASE 
                    WHEN Priority = 1 THEN 'Low'
                    WHEN Priority = 2 THEN 'Medium'
                    WHEN Priority = 3 THEN 'High'
                    ELSE 'Unknown'
                END AS PriorityText
                FROM Tasks 
                WHERE Email = @Email AND DueDate = @DueDate AND IsCompleted = 0";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    cmd.Parameters.AddWithValue("@DueDate", currentDate);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        hasTask = true;

                        string priorityText = reader["PriorityText"].ToString();
                        if (priorityText == "High")
                            isHighPriority = true;

                        taskDetails += $"<b>{reader["Title"]}</b> - Priority: {priorityText}<br>{reader["Description"]}<br/>";
                    }
                }
            }

            if (currentDate == DateTime.Today)
            {
                e.Cell.BackColor = System.Drawing.Color.LightBlue;
            }
            else if (isHighPriority)
            {
                e.Cell.BackColor = System.Drawing.Color.Red;
                e.Cell.ForeColor = System.Drawing.Color.White;
            }
            else if (hasTask)
            {
                e.Cell.BackColor = System.Drawing.Color.Pink;
            }

            string safeTaskDetails = string.IsNullOrEmpty(taskDetails)
                ? "No Tasks Due On This Day!"
                : taskDetails.Replace("'", "\\'");

            e.Cell.Attributes["onclick"] = $"showModal('{currentDate.ToShortDateString()}', `{safeTaskDetails}`)";
            e.Cell.Attributes["style"] = "cursor: pointer;";
        }





        private void HighlightTodayDate()
        {
            TaskCalendar.TodaysDate = DateTime.Today;
        }

        private void HighlightCalendarDates()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT DueDate FROM Tasks WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", Session["Email"]?.ToString());

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime taskDate = reader.GetDateTime(0);
                    TaskCalendar.SelectedDates.Add(taskDate);
                }
            }
        }
        protected string GetDueDateMessage(object dueDateObj)
        {
            if (dueDateObj == null || dueDateObj == DBNull.Value)
                return "No due date";

            DateTime dueDate;
            if (DateTime.TryParse(dueDateObj.ToString(), out dueDate))
            {
                int daysLeft = (dueDate - DateTime.Today).Days;

                if (daysLeft < 0)
                    return $"<span style='color: red;'>Overdue by {-daysLeft} days</span>";
                else if (daysLeft == 0)
                    return "<span style='color: orange;'>Due today</span>";
                else
                    return $"<span style='color: green;'>Due in {daysLeft} days</span>";
            }
            return "Invalid date";
        }
        protected void FilterOptions_CheckedChanged(object sender, EventArgs e)
        {
            string selectedFilter = FilterOptions.SelectedValue;
            ApplyFilter(selectedFilter);
        }

        private void ApplyFilter(string filterOption)
        {
            string query = "";
            switch (filterOption)
            {
                case "DueDate":
                    query = @"
            SELECT 
                TaskID, 
                Title, 
                Description, 
                CASE 
                    WHEN Priority = 1 THEN 'Low'
                    WHEN Priority = 2 THEN 'Medium'
                    WHEN Priority = 3 THEN 'High'
                    ELSE 'Unknown'
                END AS PriorityText, 
                FORMAT(DueDate, 'yyyy-MM-dd') AS DueDate, 
                Tags, 
                IsCompleted 
            FROM Tasks 
            WHERE Email = @Email 
            ORDER BY 
                CASE 
                    WHEN CAST(DueDate AS DATE) = CAST(GETDATE() AS DATE) THEN 0 -- Due Today
                    WHEN DueDate > GETDATE() THEN 1 -- Upcoming tasks
                    ELSE 2 -- Overdue tasks
                END, DueDate ASC";
                    break;

                case "Priority":
                    query = @"
            SELECT 
                TaskID, 
                Title, 
                Description, 
                CASE 
                    WHEN Priority = 1 THEN 'Low'
                    WHEN Priority = 2 THEN 'Medium'
                    WHEN Priority = 3 THEN 'High'
                    ELSE 'Unknown'
                END AS PriorityText, 
                FORMAT(DueDate, 'yyyy-MM-dd') AS DueDate, 
                Tags, 
                IsCompleted 
            FROM Tasks 
            WHERE Email = @Email 
            ORDER BY Priority DESC, DueDate ASC"; 
                    break;

                case "Latest":
                    query = @"
            SELECT 
                TaskID, 
                Title, 
                Description, 
                CASE 
                    WHEN Priority = 1 THEN 'Low'
                    WHEN Priority = 2 THEN 'Medium'
                    WHEN Priority = 3 THEN 'High'
                    ELSE 'Unknown'
                END AS PriorityText, 
                FORMAT(DueDate, 'yyyy-MM-dd') AS DueDate, 
                Tags, 
                IsCompleted 
            FROM Tasks 
            WHERE Email = @Email 
            ORDER BY TaskID DESC";
                    break;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", Session["Email"].ToString());

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                ActiveTasksGrid.DataSource = dt;
                ActiveTasksGrid.DataBind();
            }
        }



        protected void ResetFilter_Click(object sender, EventArgs e)
        {
            FilterOptions.ClearSelection();
            ApplyFilter("DueDate"); 
        }






        protected void UpdateProfile_Click(object sender, EventArgs e)
        {
            string newUsername = Username.Text.Trim();
            string aboutYourself = AboutYourself.Value.Trim();

      
            if (string.IsNullOrWhiteSpace(newUsername) && string.IsNullOrWhiteSpace(aboutYourself))
            {
                lblErrorMessage.Text = "Please fill in at least one field to update.";
                lblErrorMessage.Visible = true;
                return;
            }

         
            if (!string.IsNullOrWhiteSpace(newUsername) && newUsername.Length < 4)
            {
                lblErrorMessage.Text = "Username must be at least 4 characters long.";
                lblErrorMessage.Visible = true;
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE Login SET ";
                    List<string> updates = new List<string>();
                    if (!string.IsNullOrWhiteSpace(newUsername))
                        updates.Add("Username = @Username");

                    if (!string.IsNullOrWhiteSpace(aboutYourself))
                        updates.Add("AboutYourself = @AboutYourself");

                    query += string.Join(", ", updates) + " WHERE Email = @Email";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    if (!string.IsNullOrWhiteSpace(newUsername))
                        cmd.Parameters.AddWithValue("@Username", newUsername);

                    if (!string.IsNullOrWhiteSpace(aboutYourself))
                        cmd.Parameters.AddWithValue("@AboutYourself", aboutYourself);

                    cmd.Parameters.AddWithValue("@Email", Session["Email"]?.ToString());

                    cmd.ExecuteNonQuery();
                }

                lblErrorMessage.Text = "Profile updated successfully!";
                lblErrorMessage.ForeColor = System.Drawing.Color.Green;
                lblErrorMessage.Visible = true;

                if (!string.IsNullOrWhiteSpace(newUsername))
                    lblWelcome.Text = $"Welcome {newUsername}";
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = $"Error updating profile: {ex.Message}";
                lblErrorMessage.Visible = true;
            }
        }



        protected void SaveTaskButton_Click(object sender, EventArgs e)
        {
            string title = TaskTitle.Text.Trim();
            string description = TaskDescription.Text.Trim();
            string priorityText = TaskPriority.SelectedValue; 
            string tags = TaskTags.Text.Trim();
            DateTime dueDate = string.IsNullOrEmpty(TaskDueDate.Text) ? DateTime.Now.Date : DateTime.Parse(TaskDueDate.Text);

            if (string.IsNullOrEmpty(title))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Title is required.');", true);
                return;
            }

            int priority = 0; 
            switch (priorityText)
            {
                case "Low":
                    priority = 1;
                    break;
                case "Medium":
                    priority = 2;
                    break;
                case "High":
                    priority = 3;
                    break;
                default:
                    priority = 1;
                    break;
            }

            string email;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Email FROM Login WHERE Username = @Username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", Session["Username"]?.ToString());

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result == null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Email not found.');", true);
                    return;
                }
                email = result.ToString();
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Tasks 
                        (Title, Description, Priority, DueDate, Tags, Email, IsCompleted) 
                        VALUES (@Title, @Description, @Priority, @DueDate, @Tags, @Email, 0)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(description) ? (object)DBNull.Value : description);
                cmd.Parameters.AddWithValue("@Priority", priority); 
                cmd.Parameters.AddWithValue("@DueDate", dueDate);
                cmd.Parameters.AddWithValue("@Tags", string.IsNullOrEmpty(tags) ? (object)DBNull.Value : tags);
                cmd.Parameters.AddWithValue("@Email", email);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            TaskTitle.Text = "";
            TaskDescription.Text = "";
            TaskDueDate.Text = "";
            TaskTags.Text = "";
            LoadActiveTasks();

            ClientScript.RegisterStartupScript(this.GetType(), "closeModal", "closeCreateTaskModal();", true);
        }





        protected void MarkCompleteCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            GridViewRow row = (GridViewRow)checkBox.NamingContainer;
            int taskId = Convert.ToInt32(checkBox.Attributes["CommandArgument"]);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Tasks SET IsCompleted = 1 WHERE TaskID = @TaskID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TaskID", taskId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadActiveTasks();
            LoadCompletedTasks();
        }


        protected void ActiveTasksGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteTask")
            {
                int taskId = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Tasks WHERE TaskID = @TaskID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TaskID", taskId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                LoadActiveTasks();
            }
        }

        protected void CompletedTasksGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RemoveTask") 
            {
                try
                {
                    int taskId = Convert.ToInt32(e.CommandArgument);

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM Tasks WHERE TaskID = @TaskID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@TaskID", taskId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    LoadCompletedTasks();
                }
                catch (Exception ex)
                {
                    Response.Write($"Error: {ex.Message}");
                }
            }
        }

        private void LoadActiveTasks()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                TaskID, 
                Title, 
                Description, 
                CASE 
                    WHEN Priority = 1 THEN 'Low'
                    WHEN Priority = 2 THEN 'Medium'
                    WHEN Priority = 3 THEN 'High'
                    ELSE 'Unknown'
                END AS PriorityText, 
                FORMAT(DueDate, 'MM/dd/yyyy') AS DueDate, 
                Tags, 
                IsCompleted 
            FROM Tasks 
            WHERE Email = @Email AND IsCompleted = 0
            ORDER BY 
                CASE 
                    WHEN CAST(DueDate AS DATE) = CAST(GETDATE() AS DATE) THEN 0
                    WHEN DueDate > GETDATE() THEN 1
                    ELSE 2
                END, 
                Priority DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", Session["Email"].ToString());

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                ActiveTasksGrid.DataSource = dt;
                ActiveTasksGrid.DataBind();

                noTasksMessage.Visible = ActiveTasksGrid.Rows.Count == 0;

            }
        }
        protected void ActiveTasksGrid_DataBound(object sender, EventArgs e)
        {
            noTasksMessage.Visible = ActiveTasksGrid.Rows.Count == 0;
        }

        protected void SignOutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/LandingPage.aspx");
        }


        private void LoadCompletedTasks()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
        SELECT 
            TaskID, 
            Title, 
            Description, 
            CASE 
                WHEN Priority = 1 THEN 'Low'
                WHEN Priority = 2 THEN 'Medium'
                WHEN Priority = 3 THEN 'High'
                ELSE 'Unknown'
            END AS PriorityText, 
            FORMAT(DueDate, 'MM/dd/yyyy') AS DueDate, 
            Tags 
        FROM Tasks 
        WHERE Email = @Email AND IsCompleted = 1";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", Session["Email"].ToString());

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                CompletedTasksGrid.DataSource = dt;
                CompletedTasksGrid.DataBind();
            }
        }




    }
}
