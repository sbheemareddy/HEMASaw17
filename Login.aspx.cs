using HEMASaw.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            string employeeID = txtEmployeeID.Text.Trim();
            string password = txtPassword.Text.Trim();
            var user = GetEmployeeByID(employeeID);
            if (user.Active)
            {
                string storedPasswordHash = user.EmployeePassword;

                if (!string.IsNullOrEmpty(storedPasswordHash))
                {
                    string hashedPassword = HemaSawDAO.ComputeHash(password, storedPasswordHash.Split(':')[1]);

                    if (hashedPassword == storedPasswordHash.Split(':')[0])
                    {
                        if (! user.FirstTimeLogin)
                        {
                            FormsAuthentication.SetAuthCookie(employeeID, false);
                            Response.Redirect("Default.aspx");
                        }
                        else
                        {
                            Response.Redirect($"ChangePassword.aspx?employeeID={HttpUtility.UrlEncode(employeeID)}");
                        }


                    }
                    else
                    {
                        lblMessage.Text = "Invalid password";
                        lblMessage.Visible = true;
                    }
                }
                else
                {
                    lblMessage.Text = "Invalid Employee ID";
                    lblMessage.Visible = true;
                }
            }
            else
            {
                lblMessage.Text = "Invalid Employee ID";
                lblMessage.Visible = true;
            }
        }

        private User GetEmployeeByID(string employeeID)
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
            return user;
        }
    }
}