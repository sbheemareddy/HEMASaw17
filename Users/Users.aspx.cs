using HEMASaw.DAO;
using System;
using System.Collections.Generic;
using System.Data;
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
            List<User> users= GetUserList();
            ViewState["users"] = users;
            GridViewUsers.DataSource = users;
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
            char showAllUsersValue = chkShowAllUsers.Checked ? 'N' : 'Y';
            return HemaSawDAO.GetUserList(showAllUsersValue); // Example: Get active users
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

        protected void GridViewUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Retrieve the list of users from ViewState
            List<User> dtrslt = (List<User>)ViewState["users"];

            // Retrieve the current sort expression and direction from ViewState
            string currentSortExpression = ViewState["SortExpression"] as string;
            SortDirection currentSortDirection = ViewState["SortDirection"] != null ? (SortDirection)ViewState["SortDirection"] : SortDirection.Ascending;

            // Determine the new sort direction
            SortDirection newSortDirection;

            if (currentSortExpression == e.SortExpression)
            {
                // If the user clicks on the same column, toggle the sort direction
                newSortDirection = currentSortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;
            }
            else
            {
                // If the user clicks on a different column, set the direction to ascending by default
                newSortDirection = SortDirection.Ascending;
            }

            // Perform sorting based on the sort expression and direction
            if (e.SortExpression == "EmployeeID")
            {
                if (newSortDirection == SortDirection.Ascending)
                {
                    dtrslt = dtrslt.OrderBy(u => u.EmployeeID).ThenBy(u => u.FirstName).ThenBy(u => u.LastName).ToList();
                }
                else
                {
                    dtrslt = dtrslt.OrderByDescending(u => u.EmployeeID).ThenByDescending(u => u.FirstName).ThenByDescending(u => u.LastName).ToList();
                }
            }
            else if (e.SortExpression == "FirstName")
            {
                if (newSortDirection == SortDirection.Ascending)
                {
                    dtrslt = dtrslt.OrderBy(u => u.FirstName).ThenBy(u => u.LastName).ThenBy(u => u.EmployeeID).ToList();
                }
                else
                {
                    dtrslt = dtrslt.OrderByDescending(u => u.FirstName).ThenByDescending(u => u.LastName).ThenByDescending(u => u.EmployeeID).ToList();
                }
            }
            else if (e.SortExpression == "LastName")
            {
                if (newSortDirection == SortDirection.Ascending)
                {
                    dtrslt = dtrslt.OrderBy(u => u.LastName).ThenBy(u => u.FirstName).ThenBy(u => u.EmployeeID).ToList();
                }
                else
                {
                    dtrslt = dtrslt.OrderByDescending(u => u.LastName).ThenByDescending(u => u.FirstName).ThenByDescending(u => u.EmployeeID).ToList();
                }
            }

            // Update the ViewState with the sorted list, new sort expression, and new sort direction
            ViewState["users"] = dtrslt;
            ViewState["SortExpression"] = e.SortExpression;
            ViewState["SortDirection"] = newSortDirection;

            // Rebind the GridView control to reflect the sorted data
            GridViewUsers.DataSource = dtrslt;
            GridViewUsers.DataBind();
        }

        protected void chkShowAllUsers_CheckedChanged(object sender, EventArgs e)
        {
            List<User> users = GetUserList();
            ViewState["users"] = users;
            GridViewUsers.DataSource = users;
            GridViewUsers.DataBind();
        }
    }
}