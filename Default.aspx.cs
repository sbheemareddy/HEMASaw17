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

namespace HEMASaw
{
    public partial class _Default : Page
    {
        private int workOrder = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ParseAndPopulateTextBoxes("Date: 12/27/2023, WO#:1685996, Block#:123, Badge#:123, Slice#:3, Saw#:4, Min:0.622, Max:0.638, Ave:0.633");
          //  PopulateSystemData(workOrder);

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Do somthing
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
                            txtDate.Text = value;
                            break;
                        case "WO#":
                            txtWO.Text = value;
                            workOrder = int.Parse(value.ToString());
                            break;
                        case "Block#":
                            txtBlock.Text = value;
                            break;
                        case "Badge#":
                            txtBadge.Text = value;
                            break;
                        case "Slice#":
                            txtSlice.Text = value;
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
                            txtAvg.Text = value;
                            break;
                    }
                }
            }
        }

        private void PopulateSystemData(int workOrder)
        {
          //  WOStatus wOStatus= HemaSawDAO.GetSystemData(workOrder);

            //if (wOStatus !=null)
            //{
            //    txtStatus.Text = wOStatus.Status;
            //    txtSlicingBatch.Text = wOStatus.Slice_Batch;
            //    txtDescription.Text = wOStatus.Description;
            //}
        }
    }
}