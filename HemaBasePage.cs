using System;
using System.Web.UI;

namespace HEMASaw
{
    public class HemaBasePage :System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            // Perform the authentication check
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/login.aspx");
            }
        }
    }
}