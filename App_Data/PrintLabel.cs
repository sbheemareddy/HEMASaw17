using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Centiv;
using Centiv.ParcelPackOut;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
//using Microsoft.Reporting.WinForms;
using Microsoft.Reporting.WebForms;

/// <summary>
/// Summary description for PrintLabel
/// </summary>
public class PrintLabel
{
    public int m_currentPageIndex;
    public IList<Stream> m_streams;

    #region Print Label
    public IList<Stream> Export(ServerReport report)
    {
        try
        {
            string deviceInfo =
                    "<DeviceInfo>" +
                      "<OutputFormat>EMF</OutputFormat>" +
                      "  <MarginTop>0.25in</MarginTop>" +
                      "  <MarginLeft>0.25in</MarginLeft>" +
                      "  <MarginRight>0.25in</MarginRight>" +
                      "  <MarginBottom>0.25in</MarginBottom>" +
                      " <StartPage>0</StartPage>" +
                    "</DeviceInfo>";

            m_streams = new List<Stream>();

            string encoding;
            string mimeType;
            string extension;
            Warning[] warnings;
            string[] streamIDs = null;
            Byte[][] pages = null;

            //Create Byte array containing the rendered image. of the 1st page.
            Centiv.General.WriteEventLog("Export RENDER START");
            Byte[] firstPage = report.Render("Image", deviceInfo, out mimeType, out encoding, out extension, out streamIDs, out warnings);
            Centiv.General.WriteEventLog("Export RENDER COMPLETE");

            m_streams.Add(new MemoryStream(firstPage));

            // The total number of pages of the report is 1 + the streamIDs         
            int m_numberOfPages = streamIDs.Length + 1;
            pages = new Byte[m_numberOfPages][];

            // The first page was already rendered
            pages[0] = firstPage;


            for (int pageIndex = 1; pageIndex < m_numberOfPages; pageIndex++)
            {
                // Build device info based on start page
                deviceInfo = String.Format(
                      "<DeviceInfo>" +
                          "<OutputFormat>EMF</OutputFormat>" +
                          "  <MarginTop>0.25in</MarginTop>" +
                          "  <MarginLeft>0.25in</MarginLeft>" +
                          "  <MarginRight>0.25in</MarginRight>" +
                          "  <MarginBottom>0.25in</MarginBottom>" +
                          " <StartPage>{0}</StartPage>" +
                      "</DeviceInfo>", pageIndex + 1);

                //Render the page to a byte array.
                pages[pageIndex] = report.Render("Image", deviceInfo, out mimeType, out encoding, out extension, out streamIDs, out warnings);

                //create a stream of the page's byte array
                m_streams.Add(new MemoryStream(pages[pageIndex]));

                //set the position of the stream to 0 to make sure when the stream is read
                //it starts from the beginning.
                m_streams[m_streams.Count - 1].Position = 0;
            }

            m_currentPageIndex = 0;
        }
        catch (Exception ex)
        {
            bool rethrow = ExceptionPolicy.HandleException(ex, "Exception Policy");
            if (rethrow)
            {
                throw;
            }
        }
      
        return m_streams;
    }

    public void Print(Label lbl, string printerName, string ReportType)
    {
        //create the printdocument object
        PrintDocument printDoc = new PrintDocument();

        try
        {
            //the name of the printer.
            //const string printerName = "Microsoft Office Document Image Writer";
            //const string printerName = "\\\\MON-CHARLIE\\MON-SNOOPY"; 

            //if there's nothing to print return
            if (m_streams == null || m_streams.Count == 0)
                return;

            //set the printername, deal w/ the printer being invalid
            printDoc.PrinterSettings.PrinterName = printerName;
            if (!printDoc.PrinterSettings.IsValid)
            {
                string msg = String.Format("Can't find printer \"{0}\".", printerName);
                General.DisplayMessage(lbl, msg, General.PageStyleError);

                return;
            }

            if (ReportType == "OverPack")
            {
                printDoc.DefaultPageSettings.PaperSize = new PaperSize("Label", 380, 553);
            }
           
            //attatch an event handler that will fire for each page that is printed.
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);

            //Set instance of print controller to hide page print popup message
            printDoc.PrintController = new System.Drawing.Printing.StandardPrintController();
                        
            //call print method, which get's the process going and ultimately calls the printpage method via the eventhandler.
            printDoc.Print();

            //Dispose of Print Document
            printDoc.Dispose();

            if (ReportType == "OverPack")
            {
                General.DisplayMessage(lbl, General.msgParcelPrinted, General.PageStyleSuccess);
            }
            else
            {
                General.DisplayMessage(lbl, General.msgReportPrinted, General.PageStyleSuccess);
            }
        }
        catch (Exception ex)
        {
            bool rethrow = ExceptionPolicy.HandleException(ex, "Exception Policy");
            General.DisplayMessage(lbl, ex.Message, General.PageStyleError);

            //Dispose of Print Document
            printDoc.Dispose();

            if (rethrow)
            {
                throw;
            }
        }   
    }

    public void PrintPackageLabel(ReportViewer rvMain, Label lbl, string LabelType, string strID, string printerName)
    {
        string reportPath = string.Empty;
        List<ReportParameter> paramList = new List<ReportParameter>();

        try
        {
            if (LabelType == "OverPack")
            {
                reportPath = WebConfigurationManager.AppSettings["OverPackLabel"];
            }
            else
            {
                reportPath = WebConfigurationManager.AppSettings["ParcelLabel"];
            }

            rvMain.ServerReport.ReportPath = reportPath;
            Uri uriReport = new Uri(WebConfigurationManager.AppSettings["ReportServer"]);
            rvMain.ServerReport.ReportServerUrl = uriReport;
            IReportServerCredentials sr = new Centiv.MyReportServerCredentials();
            rvMain.ServerReport.ReportServerCredentials = sr;


            if (LabelType == "OverPack")
            {
                paramList.Add(new ReportParameter("OverPackHeaderID", strID, false));
            }
            else
            {
                paramList.Add(new ReportParameter("id", strID, false));
            }

            rvMain.ServerReport.SetParameters(paramList);

            //print label and display message.
            Centiv.General.WriteEventLog("PrintPackageLabel PRINT START - " + printerName);
            PrintReport(rvMain.ServerReport, lbl, printerName, General.LabelOverPack);
            Centiv.General.WriteEventLog("PrintPackageLabel PRINT COMPLETE - " + printerName);
        }
        catch (Exception ex)
        {
            bool rethrow = ExceptionPolicy.HandleException(ex, "Exception Policy");
            General.DisplayMessage(lbl, ex.Message, General.PageStyleError); 
            
            if (rethrow)
            {
                throw;
            }
        }
    }

    public void PrintPage(object sender, PrintPageEventArgs ev)
    {
        try
        {
            //create a new metafile based on the page that we're trying to print.
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            //draw the an image deciding what to draw, and where to place it on the page.
            //ev.Graphics.DrawImage(pageImage, 0, 0);
            ev.Graphics.DrawImage(pageImage, new Rectangle(new Point(0, 0), new Size(850, 1150)));

            //increment to the next page.
            m_currentPageIndex++;

            //decide if we've read our last stream.
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }
        catch (Exception ex)
        {
            ex.Data.Add("m_currentPageIndex", m_currentPageIndex);
            ex.Data.Add("m_streams Count", m_streams.Count);
            bool rethrow = ExceptionPolicy.HandleException(ex, "Exception Policy");
           
            if (rethrow)
            {
                throw;
            }
        }
    }

    public void PrintReport(ServerReport report, Label lbl, string printerName, string ReportType)
    {
        try
        {
            m_streams = Export(report);

            //check first record to see if larger than intial size and ok to print. 
            if (m_streams[0].Length > 1000)
            {
                Print(lbl, printerName, ReportType);
            }
            else
            {
                General.DisplayMessage(lbl, General.msgErrorInvalidReport, General.PageStyleError);
            }

            DisposeReport();
        }
        catch (Exception ex)
        {
            Centiv.General.WriteEventLog("PRINT ERROR - " + printerName + " - " + ReportType + " " + ex);

            ex.Data.Add("PrinterName", printerName);
            ex.Data.Add("ReportType", ReportType);
            bool rethrow = ExceptionPolicy.HandleException(ex, "Exception Policy");

            if (rethrow)
            {
                throw;
            }
        }
    }

    public void DisposeReport()
    {
        try
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
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

    #endregion

    
    #region Print Report Detail List
    public void PrintOverPackDetailReport(ReportViewer rvMain, Label lbl, string strID, string printerName)
    {
        string reportPath = string.Empty;
        List<ReportParameter> paramList = new List<ReportParameter>();

        try
        {
            reportPath = WebConfigurationManager.AppSettings["OverPackDetailList"];
            rvMain.ServerReport.ReportPath = reportPath;
            Uri uriReport = new Uri(WebConfigurationManager.AppSettings["ReportServer"]);
            rvMain.ServerReport.ReportServerUrl = uriReport;
            IReportServerCredentials sr = new Centiv.MyReportServerCredentials();
            rvMain.ServerReport.ReportServerCredentials = sr;

            paramList.Add(new ReportParameter("OverPackHeaderID", strID, false));
           
            rvMain.ServerReport.SetParameters(paramList);

            //print report and display message.
            PrintReport(rvMain.ServerReport, lbl, printerName, General.OverPackReport);
        }
        catch (Exception ex)
        {
            bool rethrow = ExceptionPolicy.HandleException(ex, "Exception Policy");
            General.DisplayMessage(lbl, ex.Message, General.PageStyleError);

            if (rethrow)
            {
                throw;
            }
        }
    }
    #endregion
}

