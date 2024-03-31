using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEMASaw
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Signout(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
        }

        protected string GetUserDisplayName()
        {
            if (Session["FirstName"] != null)
            {
                // Get user's details from your authentication mechanism
                string firstName = Session["FirstName"].ToString(); // Retrieve the user's first name
                string lastName = Session["LastName"].ToString(); // Retrieve the user's last name
                string employeeID = Session["EmployeeID"].ToString(); // Retrieve the user's employee ID

                // Format the display name
                return $"{lastName} {firstName} ({employeeID})";

            }

            return string.Empty;
        }
    }
}