using HEMASaw.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HEMASaw.WO
{
    public partial class WOPage : System.Web.UI.Page
    {
        private int workOrder = 0;
        private string slicebatch = string.Empty;
        private string blockbatch = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["EmpID"] = "0086";
            Session["EmployeeName"] = "MENDOZA, MARGARITO";
            ParseAndPopulateTextBoxes("Date: 12/27/2023, WO#:1685996, Block#:173318, Badge#:123, Slice#:000173837A, Saw#:4, Min:0.622, Max:0.638, Ave:0.633");
            PopulateSystemData(workOrder , slicebatch, blockbatch);
            lblEmpID.InnerText = Session["EmpID"].ToString();
            lblEmpName.InnerText = Session["EmployeeName"].ToString();
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
                            lblQRCodeDate.InnerText = value;
                            break;
                        case "WO#":
                            txtWO.Text = value;
                            workOrder = int.Parse(value.ToString());
                            break;
                        case "Block#":
                            blockbatch = value;
                            lblBlockBatch.InnerText = value;
                            // txtBlock.Text = value;
                            break;
                        case "Badge#":

                            break;
                        case "Slice#":
                            slicebatch = value;
                            lblSliceBatch.InnerText = value;

                            break;
                        case "Saw#":
                            lblSaw.InnerText = value;
                            break;
                        case "Min":
                            lblMin.InnerText = value;
                            break;
                        case "Max":
                            lblMax.InnerText = value;
                            break;
                        case "Ave":
                            lblAve.InnerText = value;
                            break;
                    }
                }
            }
        }

        private void PopulateSystemData(int workOrder, string slicebatch, string blockbatch)
        {
            WOData wOData = HemaSawDAO.GetSystemData(workOrder, slicebatch, blockbatch);

            if (wOData != null)
            {
                lblDescription.InnerText = wOData.Description;
                lblLegacyPart.InnerText = wOData.VisualPartID;
                lblTargetDensity.InnerText = wOData.Density.ToString();
                lblDensityTol.InnerText = wOData.DensityTol.ToString();
                lblCutSlice.InnerText = wOData.SliceNum.ToString();
                Session["Thickness"] = wOData.Thickness.ToString();
            }
        }

        protected void btnQRCodeScan_Click(object sender, EventArgs e)
        {

        }

        protected void btnClearData_Click(object sender, EventArgs e)
        {

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

            double DensityPCF = weight / (width * length * thickness) / 1728;


            
            
            lblPCFCalculated.InnerText = DensityPCF.ToString();
            bool acceptDataSuccess = true;

            if ((DensityPCF <= (double.Parse(lblTargetDensity.InnerText)+ double.Parse(lblDensityTol.InnerText))) || (DensityPCF >= (double.Parse(lblTargetDensity.InnerText) - double.Parse(lblDensityTol.InnerText))))
            {
                // Data acceptance succeeded, keep the button green
                divpass.Visible = true;
                divfail.Visible = false;
            }
            else
            {
                divpass.Visible = false;
                divfail.Visible = true;
            }
        }

        protected void btnKeyinWO_Click(object sender, EventArgs e)
        {

        }

        protected void btnReprintTags_Click(object sender, EventArgs e)
        {

        }
    }
}