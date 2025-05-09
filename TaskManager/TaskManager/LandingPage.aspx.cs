using System;

namespace TaskManager
{
    public partial class LandingPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Page load logic here (if needed)
            if (!IsPostBack)
            {
                // Add any initialization logic if required
            }
        }

        protected void btnGetStarted_Click(object sender, EventArgs e)
        {
            // Redirect user to Task Manager or another page
            Response.Redirect("TaskManager.aspx");
        }
    }
}
