using HEMASaw.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEMASaw
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Get the entered employee ID
            string employeeID = txtEmployeeID.Text.Trim();

            // Here you can add your validation logic for the employee ID
            if (IsValidEmployeeID(employeeID))
            {
                // Successful login, redirect to the home page or any other page
                // Sign in the user using Forms Authentication
                FormsAuthentication.SetAuthCookie(employeeID, false);

                // Redirect to the home page or any other page
                Response.Redirect("Default.aspx");
            }
            else
            {
                // Invalid employee ID, display error message
                lblMessage.Text = "Invalid Employee ID";
                lblMessage.Visible = true;
            }
        }

        // Example validation logic for the employee ID
        private bool IsValidEmployeeID(string employeeID)
        {
            var user = HemaSawDAO.GetUserById(employeeID);
            if (user.Active)
            {
                Session["EmployeeID"] = user.EmployeeID;
                Session["FirstName"] = user.FirstName;
                Session["LastName"] = user.LastName;
                Session["EmployeeRole"] = user.EmployeeRole;
                Session["EmployeeRoleDesc"] = user.EmployeeRoleDesc;
            }
            return user.Active;
        }
    }
}