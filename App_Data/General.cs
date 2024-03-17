using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Web.Configuration;

namespace Centiv
{
    /// <summary>
    /// Summary description for General
    /// </summary>
    /// 
    public static class General
    {
        #region Fields
        //Display Messages
        public static string msgErrorNoPrintLocation = "Your print location cannot be determined. Contact TE";
        public static string msgErrorGeneral = "Rendering label or job issue. Contact TE";
        public static string msgErrorInvalidCheckinItem = "Invalid number entered.  Please try again.";
        public static string msgErrorInvalidReportRendering = "Invalid report rendering mapping. Contact TE";
        public static string msgErrorInvalidOverPack = "Invalid over pack number. Please try again.";
        public static string msgErrorInvalidItem = "Invalid item number. Please try again.";
        public static string msgErrorInvalidOverPackNum = "Invalid over pack #. Over Pack starts with 'P' then the number.  Please try again.";
        public static string msgErrorInvalidLineItemNum = "No over pack can be found for entered line item.";
        public static string msgErrorOverPackCreation = "Error: Over pack could not be created.";
        public static string msgErrorDeleteLineItem = "Cannot delete line item.  Item has already shipped or is pending shipment.";
        public static string msgErrorAddItemFailed = "Error: Item could not be added to over pack.";
        public static string msgErrorInvalidItemNum = "Invalid item number. Please try again.";
        public static string msgErrorInvalidReport = "Invalid report size due to possible invalid ID. Cannot print.";
        public static string msgErrorItemNotFound = "Item scanned could not be found.";
        public static string msgErrorItemNoImage = "Item does not have image value.";
        public static string msgOverPackComplete = "Over pack complete";
        public static string msgOverPackDeleted = "Over pack has been deleted.";
        public static string msgOverPackCreated = "Over pack created successfully.";
        public static string msgOverPackLineRemoved = "Detail line has been removed.";
        public static string msgOverPackCleared = "Over pack detail has been cleared.";
        public static string msgParcelPrinted = "Parcel label has been printed";
        public static string msgReportPrinted = "Overpack report has been printed";
        //Page Styles
        public static string PageStyleError = "ErrorText";
        public static string PageStylePackError = "PackErrorText"; 
        public static string PageStyleSuccess = "SuccessText";

        //Label Style
        public static string LabelOverPack = "OverPack";
        public static string LabelLineItem = "LineItem";
        public static string OverPackReport = "OverPackReport"; 

        #endregion

        /// <summary>
        /// Add an application information message to the top of the page
        /// </summary>
        /// <param name="mstr"></param>
        /// <param name="Message"></param>
        public static void DisplayMessage(Label lbl, string Message, string strStyle)
        {
            try
            {
                if (lbl != null)
                {
                    lbl.Text = Message;
                    lbl.CssClass = strStyle;
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "Exception Policy");
                if (rethrow)
                {
                    throw;
                }
            }
        }

        public static int GetDatabaseTimeout()
        {
            if (WebConfigurationManager.AppSettings["DatabaseTimeout"] != null)
            {
                int timeout = 0;
                int.TryParse(WebConfigurationManager.AppSettings["DatabaseTimeout"], out timeout);
                return timeout;
            }
            else
            {
                return 30;
            }
        }

        public static void WriteEventLog(string entry)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry(entry + " " + DateTime.Now.ToString(), EventLogEntryType.Information, 101, 1);
            }
        }

        /*
        public enum MessageBeepType
        { 
            Default     = -1, 
            OK          = 0x0, 
            Error       = 0x10, 
            Question    = 0x20, 
            Warning     = 0x30, 
            Information = 0x40 
        } 

        //This allows you to 'Beep' the sounds declared in the MessageBeepType
        [DllImport("USER32.DLL", SetLastError=true)] 
        public static extern bool MessageBeep(MessageBeepType type);

        //' Allowing you to specify the frequency and duration of the sound.
        [DllImport("KERNEL32.DLL")]
        public static extern bool Beep(int frequency, int duration); 
        */


    }
}
