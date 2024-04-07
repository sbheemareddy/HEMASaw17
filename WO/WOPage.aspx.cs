﻿using HEMASaw.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEMASaw.WO
{
    public partial class WOPage : HemaBasePage
    {
        string TestQRScanValue = "Date: 12/27/2023, WO#:1685996, Block#:, Badge#:123, Slice#:000173837A, Saw#:4, Min:0.622, Max:0.638, Ave:0.633";

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                densityDiv.Attributes["class"] = "card";
                int workOrder = int.Parse(Session["Workorder"].ToString());
                string sliceBatch = Session["Slice_Batch"].ToString();
                string blockBatch = Session["Block_Batch"].ToString();
                string sliceNum = Session["SliceNum"].ToString();
                string qrScanData = Session["QRScanData"].ToString();
                PopulateSystemData(workOrder, sliceBatch, blockBatch, sliceNum);
  
                //TestQRScanValue
                if (!string.IsNullOrEmpty(qrScanData))
                {
                    ParseAndPopulateTextBoxes(qrScanData);
                }
                else
                {
                    PopulateQRDataFromSystem(workOrder, sliceBatch, blockBatch, sliceNum);
                }
            }
            densityDiv.Style["background-color"] = "#f0f0f0";
            lblAcceptanceMsg.InnerHtml = string.Empty;
            hidDataChanged.Value = "0";
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
                        case "Date":
                            txtQRCodeDate.Text = value;
                            break;
                        case "WO#":
                            txtWO.Text = value;
                            //workOrder = int.Parse(value.ToString());
                            break;
                        case "Block#":
                            //blockbatch = value;
                            txtBlockBatch.Text = value;
                            // txtBlock.Text = value;
                            break;
                        case "Badge#":

                            break;
                        case "Slice#":
                           // slicebatch = value;
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

        private void PopulateSystemData(int workOrder, string slicebatch, string blockbatch, string sliceNum)
        {
            WOData wOData = HemaSawDAO.GetSystemData(workOrder, slicebatch, blockbatch, sliceNum);

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
                txtLength.Text = wOData.Length.ToString();
                txtWidth.Text = wOData.Width.ToString();
                txtWeight.Text = wOData.Weight.ToString();

                Session["widthOrig"] = wOData.Width.ToString();
                Session["weightOrig"] = wOData.Weight.ToString();
                Session["lengthOrig"] = wOData.Length.ToString();
                Session["Thickness"] = wOData.Thickness.ToString();
                Session["SystemData"] = wOData;
                lblTargetDensity.InnerHtml = $"Target Density: <b>{wOData.Density}</b> Density Tol: <b>{wOData.DensityTol}</b>";
                lblTgtCellCount.InnerHtml = $"Target Cell Count: <b>{wOData.TargetCellCount}</b> Min: <b>{wOData.MinCellCount}</b> Max: <b>{wOData.MaxCellCount}</b>";

            }
        }

        private void PopulateQRDataFromSystem(int workOrder, string slicebatch, string blockbatch, string sliceNum)
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
            divpass.Visible = false;
        }

        protected void btnAcceptData_Click(object sender, EventArgs e)
        {
            //Session["Thickness"] = wOData.Thickness.ToString();
            double width = 0.0;
            double.TryParse(txtWidth.Text, out width);
   
            double weight = 0.0;
            double.TryParse(txtWeight.Text, out weight);

            double length = 0.0;
            double.TryParse(txtLength.Text, out length);

            double thickness = 0.0;
            double.TryParse(Session["Thickness"].ToString(), out thickness);

            double CC = (width * length * thickness)  / 1728;

            double DensityPCF = weight / CC;

            double psFCC = (width * length ) / 144;

            double DensityPSF = weight / psFCC;


            lblPCFCalculated.InnerText = DensityPCF.ToString("0.000");

            double tolHigher = double.Parse(txtTargetDensity.Text) + double.Parse(txtDensityTol.Text);

            double tolLower = (double.Parse(txtTargetDensity.Text) - double.Parse(txtDensityTol.Text));

            if (DensityPCF <= tolHigher && DensityPCF >= tolLower)
            {
                divpass.Visible = true;
                lblAcceptanceMsg.InnerHtml = "Acceptance Passed";
                densityDiv.Style["background-color"] = "green";
                int ID = int.Parse(Session["SliceID"].ToString());
                string EmployeeID = Session["EmployeeID"].ToString();
                HemaSawDAO.AcceptSliceData(ID,  EmployeeID, ddlOptions.SelectedValue, length, width, weight, txtComments.Text.Trim(), DensityPCF, DensityPSF);
            }
            else
            {
                divpass.Visible = false;
                lblAcceptanceMsg.InnerHtml = "Acceptance Failed";
                densityDiv.Style["background-color"] = "Red";
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
            int cellCount = int.Parse(txtCellCount.Text.Trim());

            bool isValidCellCount = cellCount >= minCellCount && cellCount <= maxCellCount;
            if (DensityPCF <= tolHigher && DensityPCF >= tolLower && isValidCellCount )
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
    }
}

