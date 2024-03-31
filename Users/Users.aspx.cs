using HEMASaw.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEMASaw.Users
{
    public partial class Users : HemaBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            // bind GridView with user data
            GridViewUsers.DataSource = GetUserList();
            GridViewUsers.DataBind();
        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            // Redirect to a page for creating a new user
            Response.Redirect("createuser.aspx");
        }

        protected void GridViewUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Handle editing user details
            string EmployeeID = GridViewUsers.DataKeys[e.NewEditIndex].Value.ToString().Trim();
            Response.Redirect($"CreateUser.aspx?userId={EmployeeID}");
        }

        protected void GridViewUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Handle deleting user
            int userId = Convert.ToInt32(GridViewUsers.DataKeys[e.RowIndex].Value);
            // Example: DeleteUser(userId);
            // After deletion, re-bind the GridView
            BindGridView();
        }

        // Example method to get user data (replace with your actual data access logic)
        private List<User> GetUserList()
        {
            return HemaSawDAO.GetUserList('Y'); // Example: Get active users
        }

        protected void GridViewUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewUsers.PageIndex = e.NewPageIndex;
            // Rebind your data here
            BindGridView(); // Example method to bind data to GridView
        }

        protected void btnQRCodeScan_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

    }
}