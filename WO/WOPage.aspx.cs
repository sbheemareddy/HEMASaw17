using HEMASaw.DAO;
using HEMASaw.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEMASaw.WO
{
    public partial class WOPage : HemaBasePage
    {
       // string TestQRScanValue = "Date: 12/27/2023, WO#:1685996, Block#:, Badge#:123, Slice#:000173837A, Saw#:4, Min:0.622, Max:0.638, Ave:0.633";

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                densityDiv.Attributes["class"] = "card";
                SessionData sessionData = new SessionData();

                // Get the session values using the GetSessionValues method
                var (workOrder, sliceBatch, blockBatch, sliceNum, qrScanData) = sessionData.GetSessionValues(this.Session);

                //TestQRScanValue
                bool isScannedWO = false;
                if (!string.IsNullOrEmpty(qrScanData))
                {
                    ParseAndPopulateTextBoxes(qrScanData);
                    isScannedWO = true;
                }
                else
                {
                    PopulateQRDataFromSystem(workOrder, sliceBatch, blockBatch, sliceNum);
                }

                PopulateSystemData(workOrder, sliceBatch, blockBatch, sliceNum, isScannedWO);
            }
            densityDiv.Style["background-color"] = "#f0f0f0";
            lblAcceptanceMsg.InnerHtml = string.Empty;
            divpass.Visible = false;
            //HasDataChanged();
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
                    string key = keyValue[0].Trim();
                    string value = keyValue[1].Trim();

                    switch (key)
                    {
                        case "Date":
                            txtQRCodeDate.Text = value;
                            break;
                        case "WO#":
                            txtWO.Text = value;
                            break;
                        case "Block#":
                            txtBlockBatch.Text = value;
                            break;
                        case "Badge#":

                            break;
                        case "Slice#":
                            txtSliceBatch.Text = value;

                            break;
                        case "Saw#":
                            txtSaw.Text = value;
                            break;
                        case "Min":
                            txtMin.Text = value;
                            break;
                        case "Max":
                            txtMax.Text = value;
                            break;
                        case "Ave":
                            txtAve.Text = value;
                            break;
                    }
                }
            }
        }

        private void PopulateSystemData(int workOrder, string slicebatch, string blockbatch, int sliceNum,bool isScannedWO = false)
        {
            WOData wOData = HemaSawDAO.GetSystemData(workOrder, slicebatch, blockbatch, sliceNum, isScannedWO);

            if (wOData != null)
            {
                txtMaterial.Text = wOData.Material;
                txtDescription.Text = wOData.Description;
                txtLegacyPart.Text = wOData.VisualPartID;
                txtTargetDensity.Text = wOData.Density.ToString();
                txtDensityTol.Text = wOData.DensityTol.ToString();
                txtCutSlice.Text = wOData.SliceNum.ToString();
                txtTargetCellCount.Text = wOData.TargetCellCount.ToString();
                txtMinCellCount.Text = wOData.MinCellCount.ToString();
                txtMaxCellCount.Text = wOData.MaxCellCount.ToString();
                //txtLength.Text = wOData.Length.ToString();
                txtLength.Text = (wOData.Length.ToString() == "0" ? "" : wOData.Length.ToString());
                //txtWidth.Text = wOData.Width.ToString();
                txtWidth.Text = (wOData.Width.ToString() == "0" ? "" : wOData.Width.ToString());
                //txtWeight.Text = wOData.Weight.ToString();
                txtWeight.Text = (wOData.Weight.ToString() == "0" ? "" : wOData.Weight.ToString());
                txtComments.Text = wOData.Comments.ToString();
                // Assuming wOData.CellCount is an integer
                txtCellCount.Text =( wOData.CellCount.ToString() == "0" ? "" : wOData.CellCount.ToString());
                // txtQRCodeDate.Text = DateTime.Now.ToString("dd-MM-yyyy");

                Session["widthOrig"] = wOData.Width.ToString();
                Session["weightOrig"] = wOData.Weight.ToString();
                Session["lengthOrig"] = wOData.Length.ToString();
                Session["Thickness"] = wOData.Thickness.ToString();
                Session["SystemData"] = wOData;
                lblTargetDensity.InnerHtml = $"Target Density:&nbsp;&nbsp;&nbsp;<b>{wOData.Density}</b>&nbsp;&nbsp;&nbsp;Density Tol:&nbsp;&nbsp;&nbsp;<b>{wOData.DensityTol}</b>";
                lblTgtCellCount.InnerHtml = $"Target Cell Count:&nbsp;&nbsp;&nbsp;<b>{wOData.TargetCellCount}</b>&nbsp;&nbsp;&nbsp;Min:&nbsp;&nbsp;&nbsp;<b>{wOData.MinCellCount}</b>&nbsp;&nbsp;&nbsp;Max:&nbsp;&nbsp;&nbsp;<b>{wOData.MaxCellCount}</b>";

                btnPrevious.Enabled = wOData.HasPrevious;
                btnFirst.Enabled = wOData.HasPrevious;
                btnLast.Enabled = wOData.HasNext;
                btnNext.Enabled = wOData.HasNext;
                btnLast.Attributes["LastSliceNum"] = wOData.LastSliceNum.ToString();
            //LastSliceNum
        }
        }

        private void PopulateQRDataFromSystem(int workOrder, string slicebatch, string blockbatch, int sliceNum)
        {
            QRCodeData qRCodeData  = HemaSawDAO.GetQRDataFromSystem(workOrder, slicebatch, blockbatch, sliceNum);

            if (qRCodeData != null)
            {
                txtQRCodeDate.Text = qRCodeData.QRCodeDate;
                txtWO.Text = qRCodeData.WO.ToString();
                txtBlockBatch.Text = qRCodeData.BlockBatch;
                txtSliceBatch.Text = qRCodeData.SliceBatch;
                txtSaw.Text = qRCodeData.Saw;
                txtMin.Text = qRCodeData.Min.ToString();
                txtMax.Text = qRCodeData.Max.ToString();
                txtAve.Text = qRCodeData.Ave.ToString();
                txtQRCodeDate.Text = qRCodeData.QRCodeDate.ToString();

            }
        }

        protected void btnQRCodeScan_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        protected void btnClearData_Click(object sender, EventArgs e)
        {
            txtLength.Text = string.Empty;
            txtWeight.Text = string.Empty;
            txtWidth.Text = string.Empty;
            txtComments.Text = string.Empty;
            txtCellCount.Text = string.Empty;
            lblPCFCalculated.InnerText = string.Empty;
            divpass.Visible = false;
        }

        protected void btnAcceptData_Click(object sender, EventArgs e)
        {
            rfvWidth.Validate();
            rfvWeight.Validate();
            if (Page.IsValid)
            {
                double width = 0.0;
                double.TryParse(txtWidth.Text, out width);

                double weight = 0.0;
                double.TryParse(txtWeight.Text, out weight);

                double length = 0.0;
                double.TryParse(txtLength.Text, out length);

                double thickness = 0.0;
                double.TryParse(Session["Thickness"].ToString(), out thickness);

                double CC = (width * length * thickness) / 1728;

                double DensityPCF = weight / CC;

                double psFCC = (width * length) / 144;

                double DensityPSF = weight / psFCC;


                lblPCFCalculated.InnerText = DensityPCF.ToString("0.000");

                double tolHigher = double.Parse(txtTargetDensity.Text) + double.Parse(txtDensityTol.Text);

                double tolLower = (double.Parse(txtTargetDensity.Text) - double.Parse(txtDensityTol.Text));

                int minCellCount = int.Parse(txtMinCellCount.Text.Trim());
                int maxCellCount = int.Parse(txtMaxCellCount.Text.Trim());
                int cellCount;
                if (!int.TryParse(txtCellCount.Text.Trim(), out cellCount))
                {
                    cellCount = 0; // Set cellCount to 0 if parsing fails or if the input is empty
                }


                DateTime qrCodeDate;
                if (string.IsNullOrEmpty(txtQRCodeDate.Text.Trim()) || !DateTime.TryParse(txtQRCodeDate.Text.Trim(), out qrCodeDate))
                {
                    qrCodeDate = DateTime.Today;
                }
                else
                {
                    // Check if the parsed date is within the acceptable range
                    if (qrCodeDate < SqlDateTime.MinValue.Value || qrCodeDate > SqlDateTime.MaxValue.Value)
                    {
                        // If the parsed date is out of range, set it to the minimum or maximum allowed value
                        qrCodeDate = (qrCodeDate < SqlDateTime.MinValue.Value) ? SqlDateTime.MinValue.Value : SqlDateTime.MaxValue.Value;
                    }
                }

                //DateTime.TryParseExact(txtQRCodeDate.Text.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out qrCodeDate);

               // bool isValidCellCount = cellCount >= minCellCount && cellCount <= maxCellCount;
                //if (DensityPCF <= tolHigher && DensityPCF >= tolLower && isValidCellCount)
                //{
                //divpass.Visible = true;
                //lblAcceptanceMsg.InnerHtml = "Acceptance Passed";
                //densityDiv.Style["background-color"] = "green";

                densityDiv.Style["background-color"] = "#f0f0f0";
                lblAcceptanceMsg.InnerHtml = string.Empty;
                divpass.Visible = true;
                int ID = int.Parse(Session["SliceID"].ToString());
                string EmployeeID = Session["EmployeeID"].ToString();
                SessionData sessionData = new SessionData();
                QRCodeData qrscanData = sessionData.GetQRCodeData(Session["QRScanData"].ToString());
                int SliceId= HemaSawDAO.AcceptSliceData(ID, EmployeeID, ddlOptions.SelectedValue, length, width, weight, txtComments.Text.Trim(), DensityPCF, DensityPSF, cellCount, qrCodeDate, qrscanData);
                Session["SliceID"] = SliceId;

                //}
                //else
                //{
                //    divpass.Visible = false;
                //    lblAcceptanceMsg.InnerHtml = "Acceptance Failed";
                //    densityDiv.Style["background-color"] = "Red";
                //}
            }
            
        }

        protected void btnSearchWO_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        protected void btnReprintTags_Click(object sender, EventArgs e)
        {

        }

        protected void btnPrintSliceLabel_Click(object sender, EventArgs e)
        {

            var url = @"../Report/SliceLabelReport.aspx?reportName=SliceLabel";
            Response.Write("<script> window.open( '" + url + "','_blank' ); </script>");
            Response.End();
        }

        protected void btnPrintSummaryLabel_Click(object sender, EventArgs e)
        {
            var url = @"../Report/SliceLabelReport.aspx?reportName=SummaryLabel";
            Response.Write("<script> window.open( '" + url + "','_blank' ); </script>");
            Response.End();

        }

        protected void btnCheckDensity_Click(object sender, EventArgs e)
        {
            double width = 0.0;
            double.TryParse(txtWidth.Text, out width);

            double weight = 0.0;
            double.TryParse(txtWeight.Text, out weight);

            double length = 0.0;
            double.TryParse(txtLength.Text, out length);

            double thickness = 0.0;
            double.TryParse(Session["Thickness"].ToString(), out thickness);

            double CC = (width * length * thickness) / 1728;

            double DensityPCF = weight / CC;

            lblPCFCalculated.InnerText = DensityPCF.ToString("0.000");

            double tolHigher = double.Parse(txtTargetDensity.Text) + double.Parse(txtDensityTol.Text);

            double tolLower = (double.Parse(txtTargetDensity.Text) - double.Parse(txtDensityTol.Text));

            int minCellCount = int.Parse(txtMinCellCount.Text.Trim());
            int maxCellCount = int.Parse(txtMaxCellCount.Text.Trim());
            // int cellCount = int.Parse(txtCellCount.Text.Trim());

            //bool isValidCellCount = cellCount >= minCellCount && cellCount <= maxCellCount;
            //if (DensityPCF <= tolHigher && DensityPCF >= tolLower && isValidCellCount )
            if (DensityPCF <= tolHigher && DensityPCF >= tolLower)
            {
                lblAcceptanceMsg.InnerHtml = "Acceptance Passed";
                densityDiv.Style["background-color"] = "green";
            }
            else
            {
                densityDiv.Style["background-color"] = "Red";
                lblAcceptanceMsg.InnerHtml = "Acceptance Failed";
            }
            divpass.Visible = false;

        }

        protected void txtLength_TextChanged(object sender, EventArgs e)
        {
            HasDataChanged();
        }

        protected void txtWidth_TextChanged(object sender, EventArgs e)
        {
            HasDataChanged();
        }

        protected void txtWeight_TextChanged(object sender, EventArgs e)
        {
            HasDataChanged();
        }
   
        private void HasDataChanged ()
        {
           if (Session["widthOrig"].ToString() != txtWidth.Text.Trim() || Session["lengthOrig"].ToString() != txtLength.Text.Trim() || Session["weightOrig"].ToString() != txtWeight.Text.Trim())
            { 
                hidDataChanged.Value = "1";
            }
            else
            {
                hidDataChanged.Value = "0";
            }
        }

        protected void btnFirst_Click(object sender, EventArgs e)
        {
            SessionData sessionData = new SessionData();
            // Get the session values using the GetSessionValues method
            var (workOrder, sliceBatch, blockBatch, sliceNum, qrScanData) = sessionData.GetSessionValues(this.Session);
            PopulateSystemData(workOrder, sliceBatch, blockBatch, 1);
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            SessionData sessionData = new SessionData();
            // Get the session values using the GetSessionValues method
            var (workOrder, sliceBatch, blockBatch, sliceNum, qrScanData) = sessionData.GetSessionValues(this.Session);
            int prevSliceNum = int.Parse(txtCutSlice.Text) - 1;
            PopulateSystemData(workOrder, sliceBatch, blockBatch, prevSliceNum);

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            SessionData sessionData = new SessionData();
            // Get the session values using the GetSessionValues method
            var (workOrder, sliceBatch, blockBatch, sliceNum, qrScanData) = sessionData.GetSessionValues(this.Session);
            int nextSliceNum = int.Parse(txtCutSlice.Text)+1;
            PopulateSystemData(workOrder, sliceBatch, blockBatch, nextSliceNum);
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            SessionData sessionData = new SessionData();
            // Get the session values using the GetSessionValues method
            var (workOrder, sliceBatch, blockBatch, sliceNum, qrScanData) = sessionData.GetSessionValues(this.Session);
            int lastSliceNum = int.Parse(btnLast.Attributes["LastSliceNum"]);
            PopulateSystemData(workOrder, sliceBatch, blockBatch, lastSliceNum);
        }
    }
}

