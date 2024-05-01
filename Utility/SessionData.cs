using HEMASaw.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace HEMASaw.Utility
{
    public class SessionData
    {
        public (int workOrder, string sliceBatch, string blockBatch, int sliceNum, string qrScanData) GetSessionValues(HttpSessionState session)
        {
            // Parse the workOrder from the session data
            int workOrder;
            if (int.TryParse(session["Workorder"]?.ToString(), out workOrder) == false)
            {
                throw new InvalidCastException("The value in session for 'Workorder' could not be parsed to an integer.");
            }

            // Retrieve the other session values as strings
            string sliceBatch = session["Slice_Batch"]?.ToString() ?? throw new InvalidOperationException("No value found in session for 'Slice_Batch'.");
            string blockBatch = session["Block_Batch"]?.ToString() ?? throw new InvalidOperationException("No value found in session for 'Block_Batch'.");
            int sliceNum;
            if (int.TryParse(session["SliceNum"]?.ToString(), out sliceNum) == false)
            {
                throw new InvalidCastException("The value in session for 'Workorder' could not be parsed to an integer.");
            }
            string qrScanData = session["QRScanData"]?.ToString() ?? throw new InvalidOperationException("No value found in session for 'QRScanData'.");

            // Return the values as a tuple
            return (workOrder, sliceBatch, blockBatch, sliceNum, qrScanData);
        }

        public QRCodeData GetQRCodeData(string inputText)
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
                            qRCodeData.WO = int.Parse(value);
                            break;
                        case "Block#":
                            qRCodeData.BlockBatch = value;
                            break;
                        case "Slice#":
                            qRCodeData.SliceNum = double.Parse(value);
                            break;
                        case "Date":
                            qRCodeData.QRCodeDate = value;
                            break;
                        case "Badge#":
                            break;
                        case "Saw#":
                            qRCodeData.Saw = value;
                            break;
                        case "Min":
                            qRCodeData.Min = double.Parse(value);
                            break;
                        case "Max":
                            qRCodeData.Max = double.Parse(value);
                            break;
                        case "Ave":
                            qRCodeData.Ave = double.Parse(value);
                            break;
                    }
                }
            }

            return qRCodeData;
        }
    } 
}
