using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEMASaw
{
    public partial class BarcodeScannerPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the hidden field contains a scanned barcode value
            if (!string.IsNullOrEmpty(hidWOBarcode.Value))
            {
                // Process the scanned barcode
                string scannedBarcode = hidWOBarcode.Value;
                // Do something with the scanned barcode, e.g., handle it in your code-behind logic
                // For demonstration purposes, let's just display an alert
                Response.Write("<script>alert('Scanned barcode: " + scannedBarcode + "');</script>");
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