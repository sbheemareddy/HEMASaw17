using HEMASaw.DAO;
using System;
using System.Web.UI;

namespace HEMASaw
{
    public partial class ChangePassword : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["employeeID"] != null)
            {
                txtEmployeeId.Text = Request.QueryString["employeeID"];
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            string employeeID = txtEmployeeId.Text.Trim();
            string currentPassword = txtCurrentPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();

            if (IsValidCurrentPassword(currentPassword))
            {
                if (UpdatePassword(employeeID, newPassword))
                {
                    lblMessage.Text = "Password changed successfully.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Visible = true;
                }
                else
                {
                    lblMessage.Text = "Failed to change password. Please try again later.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Visible = true;
                }
            }
            else
            {
                lblMessage.Text = "Incorrect current password.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Visible = true;
            }
        }

        private bool IsValidCurrentPassword(string currentPassword)
        {
            var user = HemaSawDAO.GetUserById(txtEmployeeId.Text.Trim());
            if (user.Active)
            {
                string storedPasswordHash = user.EmployeePassword;

                if (!string.IsNullOrEmpty(storedPasswordHash))
                {
                    string hashedPassword = HemaSawDAO.ComputeHash(currentPassword, storedPasswordHash.Split(':')[0]);
                    return (hashedPassword == storedPasswordHash.Split(':')[1]);
                }
            }
                return false;
        }

        private bool UpdatePassword(string employeeID, string password)
        {
            return HemaSawDAO.UpdatePassword(employeeID, password);
        }
    }
}
