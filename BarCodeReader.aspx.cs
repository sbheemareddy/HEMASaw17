using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEMASaw
{
    public partial class BarCodeReader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // txtBarcode.Attributes.Add("onfocus", "this.value=''");
            }
        }

        // Event handler for the hidden button click
        protected void btnHidden_Click(object sender, EventArgs e)
        {
            // This event handler will be triggered when the hidden button is clicked (after scanning a barcode)
            // You can handle the postback logic here if needed
        }
    }
}