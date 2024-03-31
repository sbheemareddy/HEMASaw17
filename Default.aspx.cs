using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using HEMASaw.DAO;
using Telerik.Reporting;
using Telerik.Reporting.Processing;
using Report = Telerik.Reporting.Report;

namespace HEMASaw
{
    public partial class _Default : HemaBasePage
    {
         protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtQRScanData.Focus();
            }
        }

        private void ParseAndPopulateTextBoxes(string inputText)
        {
            // Split the input text by commas to get individual key-value pairs
            string[] keyValuePairs = inputText.Split(',');

            foreach (string pair in keyValuePairs)
            {
                // Split each key-value pair by colon
                string[] keyValue = pair.Trim().Split(':');

                if (keyValue.Length == 2)
                {
                    // Trim whitespace from key and value
                    string key = keyValue[0].Trim();
                    string value = keyValue[1].Trim();

                    // Populate textboxes based on the key
                    switch (key)
                    {
                        case "WO#":
                            txtWorkOrder.Text = value;
                            break;
                        case "Block#":
                            //blockbatch = value;
                            txtBlockBatch.Text = value;
                            break;
                        case "Slice#":
                            // slicebatch = value;
                            txtSliceBatch.Text = value;
                            break;
           
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridView(); 
        }

        private DataTable PerformSearch(int workOrder, string blockBatch, string sliceBatch)
        {

             return HemaSawDAO.SearchWO(workOrder, sliceBatch, blockBatch);
        }

        protected void gvSearchResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSearchResults.PageIndex = e.NewPageIndex;
            // Rebind the GridView with the new page index
            BindGridView(); 
        }

        private void BindGridView()
        {
            // Retrieve search criteria from the form
            int workOrder = 0;
            int.TryParse(txtWorkOrder.Text.Trim().ToString().ToUpper(),out workOrder);
            string blockBatch = txtBlockBatch.Text.Trim();
            string sliceBatch = txtSliceBatch.Text.Trim();

            if (workOrder>0)
            {
                // Call a method to perform the search
                DataTable searchResults = PerformSearch(workOrder, blockBatch, sliceBatch);

                // Bind search results to DataGrid
                gvSearchResults.DataSource = searchResults;
                gvSearchResults.DataBind();
            }
            else
            {
                gvSearchResults.DataBind();
                gvSearchResults.EmptyDataText = "No work orders for the search criteria.";
            }

        }

        protected void gvSearchResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string workOrder = gvSearchResults.DataKeys[rowIndex]["Workorder"].ToString();
                string sliceBatch = gvSearchResults.DataKeys[rowIndex]["Slice_Batch"].ToString();
                string blockBatch = gvSearchResults.DataKeys[rowIndex]["Block_Batch"].ToString();
                string sliceNum = gvSearchResults.DataKeys[rowIndex]["SliceNum"].ToString();

                Session["Workorder"] = workOrder;
                Session["Slice_Batch"] = sliceBatch;
                Session["Block_Batch"] = blockBatch;
                Session["SliceNum"] = sliceNum;
                Session["QRScanData"] = txtQRScanData.Text;

                Response.Redirect("~/WO/WOPage.aspx");
            }
        }

        protected void txtQRScanData_TextChanged(object sender, EventArgs e)
        {
            string TestQRScanValue = "Date: 12/27/2023, WO#:1685996, Block#:, Badge#:123, Slice#:000173837A, Saw#:4, Min:0.622, Max:0.638, Ave:0.633";
            if (!string.IsNullOrWhiteSpace(txtQRScanData.Text))
            {
                ParseAndPopulateTextBoxes(TestQRScanValue);
                BindGridView();
            }
        }

        protected void gvSearchResults_DataBound(object sender, EventArgs e)
        {
            if (gvSearchResults.Rows.Count == 0)
            {
                // No search results, display the message
                gvSearchResults.EmptyDataText = "No work orders for the search criteria.";
            }
        }
    }


}