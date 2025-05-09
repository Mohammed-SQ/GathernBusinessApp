using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set default view if this is the first time the page is loaded
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0; // Default to View 1 (Academic Calendar)
            }
        }

        // Menu item click event handler
        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            // Switch between views based on which menu item was clicked
            switch (e.Item.Value)
            {
                case "Academic Calendar":
                    MultiView1.ActiveViewIndex = 0; // Show View 1
                    break;
                case "Admissions":
                    MultiView1.ActiveViewIndex = 1; // Show View 2
                    break;
                case "Faculty Profiles":
                    MultiView1.ActiveViewIndex = 2; // Show View 3
                    break;
                // Add cases for other menu items and corresponding views as needed
                default:
                    MultiView1.ActiveViewIndex = 0; // Default to View 1
                    break;
            }
        }
    }
}
