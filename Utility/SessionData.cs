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
    }
}