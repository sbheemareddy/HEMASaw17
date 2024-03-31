using HEMASaw.DAO;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEMASaw.Users
{
    public partial class CreateUser : HemaBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    // Check if the page is in edit mode
                    if (!string.IsNullOrEmpty(Request.QueryString["userId"]))
                    {
                        string userId = Request.QueryString["userId"].ToString();
                        // Load user details for editing
                        LoadUserDetails(userId);
                        // Change title and button text for editing
                        litTitle.Text = "Edit User";
                        btnSave.Text = "Update";
                    }
                    else
                    {
                        // Change title for creating
                        litTitle.Text = "Create User";
                    }
                }
            }

            protected void btnSave_Click(object sender, EventArgs e)
            {

                DateTime strTermDate ;
                DateTime.TryParse(txtTermDate.Text.ToString(), out strTermDate);

                 User employee = new User();
                employee.EmployeeID = txtEmployeeID.Text.Trim();
                employee.FirstName = txtFirstName.Text.Trim();
                employee.LastName = txtLastName.Text.Trim();
                employee.Active = chkActive.Checked;
                employee.TermDate = strTermDate;
                employee.EmployeeRole = int.Parse(ddlEmployeeRole.SelectedValue);
                HemaSawDAO.UpsertEmployeeRecord(employee);
                Response.Redirect("users.aspx");
            }

            // Method to load user details for editing
            private void LoadUserDetails(string userId)
            {
                // Populate input fields with user details
                var user = HemaSawDAO.GetUserById(userId);
                txtEmployeeID.Text = user.EmployeeID;
                txtFirstName.Text = user.FirstName;
                txtLastName.Text = user.LastName;
                chkActive.Checked = user.Active;
                txtTermDate.Text = user.TermDate.HasValue ? user.TermDate.Value.ToString() : string.Empty;
                ddlEmployeeRole.SelectedValue = user.EmployeeRole.ToString();
            }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Users.aspx");
        }
    }
}