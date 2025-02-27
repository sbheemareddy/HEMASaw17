﻿using System;
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
using HEMASaw.Utility;
using log4net;

namespace HEMASaw
{
    public partial class _Default : HemaBasePage
    {
        //string TestQRScanValue = "Date: 12/27/2023, WO#:1685996, Block#:123, Badge#:123, Slice#:1, Saw#:10, Min:0.38420, Max:0.38200, Ave:0.38220";

        private const string strEmptyDataText = "No work orders for the search criteria.";
        private static readonly ILog Logger = LogManager.GetLogger(typeof(_Default));
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                txtQRScanData.Focus();
            //}
        }

        private void ParseAndPopulateTextBoxes(string inputText)
        {
            try
            {
                // Split the input text by commas to get individual key-value pairs
                string[] keyValuePairs = inputText.Split(',');
                QRCodeData qRCodeData = new QRCodeData();

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
                                txtBlockBatch.Text = value;
                                break;
                            case "Slice#":
                                txtSliceBatch.Text = value;
                                break;
                            case "Date":
                                break;
                            case "Badge#":
                                break;
                            case "Saw#":
                                break;
                            case "Min":
                                break;
                            case "Max":
                                break;
                            case "Ave":
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error in IsPostBackParseAndPopulateTextBoxes : ", ex);
                throw;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridView(strEmptyDataText); 
        }

        private DataTable PerformSearch(int workOrder, string blockBatch, string sliceBatch, int sliceNum)
        {

             return HemaSawDAO.SearchWO(workOrder, sliceBatch, blockBatch, sliceNum);
        }

        protected void gvSearchResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSearchResults.PageIndex = e.NewPageIndex;
            // Rebind the GridView with the new page index
            BindGridView(strEmptyDataText); 
        }

        private void BindGridView(string emptyDataText)
        {
            try
            {
                // Retrieve search criteria from the form
                int workOrder = 0;
                int.TryParse(txtWorkOrder.Text.Trim().ToString().ToUpper(), out workOrder);
                string blockBatch = txtBlockBatch.Text.Trim();
                string sliceBatch = txtSliceBatch.Text.Trim();

                int sliceNum = -1;
                int.TryParse(txtSliceNum.Text.Trim().ToString().ToUpper(), out sliceNum);
                gvSearchResults.EmptyDataText = emptyDataText;
                if (workOrder > 0)
                {
                    // Call a method to perform the search
                    DataTable searchResults = PerformSearch(workOrder, blockBatch, sliceBatch, sliceNum);

                    // Bind search results to DataGrid
                    gvSearchResults.DataSource = searchResults;
                    gvSearchResults.DataBind();

                }
                else
                {
                    gvSearchResults.DataSource = null;
                    gvSearchResults.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error in BindGridView : ", ex);
                throw;
            }

        }

        protected void gvSearchResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "View")
                {
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    string workOrder = gvSearchResults.DataKeys[rowIndex]["Workorder"].ToString();
                    string sliceBatch = gvSearchResults.DataKeys[rowIndex]["Slice_Batch"].ToString();
                    string blockBatch = gvSearchResults.DataKeys[rowIndex]["Block_Batch"].ToString();
                    string sliceNum = gvSearchResults.DataKeys[rowIndex]["SliceNum"].ToString();
                    string sliceID = gvSearchResults.DataKeys[rowIndex]["ID"].ToString();

                    Session["Workorder"] = workOrder;
                    Session["Slice_Batch"] = sliceBatch;
                    Session["Block_Batch"] = blockBatch;
                    Session["SliceNum"] = sliceNum;
                    Session["QRScanData"] = string.Empty;
                    Session["SliceID"] = sliceID;
                    Response.Redirect("~/WO/WOPage.aspx");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error in gvSearchResults_RowCommand : ", ex);
                throw;
            }
        }

        protected void txtQRScanData_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtQRScanData.Text))
                {
                    ParseAndPopulateTextBoxes(txtQRScanData.Text);
                    SessionData sesssionData = new SessionData();
                    QRCodeData qRCodeData = sesssionData.GetQRCodeData(txtQRScanData.Text.Trim());
                    RedirectToWOPage(qRCodeData);
                    BindGridView(strEmptyDataText);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error in txtQRScanData_TextChanged : ", ex);
                throw;
            }
        }

        private void RedirectToWOPage(QRCodeData qRCodeData)
        {
            try
            {
                DataTable dataTable = HemaSawDAO.GetUniqueQRRecord(qRCodeData);
                if (dataTable != null && dataTable.Rows.Count == 1)
                {
                    Session["Workorder"] = dataTable.Rows[0]["Workorder"].ToString();
                    Session["Slice_Batch"] = dataTable.Rows[0]["Slice_Batch"].ToString();
                    Session["Block_Batch"] = dataTable.Rows[0]["Block_Batch"].ToString();
                    Session["SliceNum"] = dataTable.Rows[0]["SliceNum"].ToString();
                    Session["SliceID"] = dataTable.Rows[0]["Id"].ToString();
                    Session["QRScanData"] = txtQRScanData.Text;
                    Response.Redirect("~/WO/WOPage.aspx");
                }
                else
                {
                    gvSearchResults.DataSource = dataTable;
                    gvSearchResults.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error in RedirectToWOPage : ", ex);
                throw;
            }
        }

        protected void gvSearchResults_DataBound(object sender, EventArgs e)
        {
            if (gvSearchResults.Rows.Count == 0)
            {
                gvSearchResults.EmptyDataText = "No work orders for the search criteria.";
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {           
            txtQRScanData.Text = string.Empty;
            txtWorkOrder.Text = string.Empty;
            txtBlockBatch.Text = string.Empty;
            txtSliceBatch.Text = string.Empty;            
            txtSliceNum.Text = string.Empty;
            BindGridView(string.Empty);
            txtQRScanData.Focus();
        }
    }
}