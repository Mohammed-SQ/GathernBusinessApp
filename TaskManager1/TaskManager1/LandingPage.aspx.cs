using System;
using System.Web.UI;

namespace TaskManager1
{
    public partial class LandingPage1 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

    }
}