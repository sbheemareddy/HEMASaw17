using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEMASaw
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // Clear all session variables
            Session.Clear();

            // Abandon the session
            Session.Abandon();

            // Remove all session cookies
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }

            // Remove all other cookies
            foreach (string cookieName in Request.Cookies.AllKeys)
            {
                if (cookieName != "ASP.NET_SessionId")
                {
                    Response.Cookies[cookieName].Value = string.Empty;
                    Response.Cookies[cookieName].Expires = DateTime.Now.AddMonths(-20);
                }
            }

            // Sign out the user
            FormsAuthentication.SignOut();
            // Redirect to the login page
            Response.Redirect("~/login.aspx");
        }
    }
}