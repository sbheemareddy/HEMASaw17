using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing.Printing;
using Telerik.Reporting;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data.SqlClient;
using HEMASaw.DAO;
using HEMASaw;

public partial class SliceLabelReport : HemaBasePage
{

    public int m_currentPageIndex;
    public IList<Stream> m_streams;

    protected void Page_Load(object sender, EventArgs e)
    {
        int SliceID = int.Parse(Session["SliceID"].ToString());
        int workOrder = int.Parse(Session["Workorder"].ToString());
        string sliceBatch = Session["Slice_Batch"].ToString();
        string blockBatch = Session["Block_Batch"].ToString();
        string sliceNum = Session["SliceNum"].ToString();
        string reportName = Path.ChangeExtension(Request.QueryString["reportName"], ".trdx");
        Report report = CustomizeReport(reportName,workOrder,sliceBatch,blockBatch,sliceNum , SliceID);
        RenderinPage(report);
    }

    public Report CustomizeReport(string filename, int workOrder ,string sliceBatch , string blockBatch , string sliceNum , int SliceID)
    {

        DataSet ds = HemaSawDAO.GetSliceSummaryLabel(workOrder, sliceBatch, blockBatch, sliceNum, SliceID);

        Report report = null;
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow data = ds.Tables[0].Rows[0];
            SliceLabelData.SliceLabelFields sliceLabelFields = SliceLabelData.GetSliceLabelLabelData(data);
            //Deserialize the .trdx report and set datasource
            if (sliceLabelFields.DenInToleranceRange == "0" && sliceLabelFields.ThickInToleranceRange == "0" && filename.ToLower() == "slicelabel.trdx")
            {
                filename = "SliceLabelDenThicTolFail.trdx";
            }
            else if (sliceLabelFields.DenInToleranceRange == "0" && filename.ToLower() == "slicelabel.trdx")
            {
                filename = "SliceLabelDenTolFail.trdx";
            }
            else if (sliceLabelFields.ThickInToleranceRange == "0" && filename.ToLower() == "slicelabel.trdx")
            {
                filename = "SliceLabelThicTolFail.trdx";
            }
            
            report = GetTelerikReportFromXml(filename);
            report.DataSource = sliceLabelFields;

            return report;
        }
        return report;
    }

    private Report GetTelerikReportFromXml(string filename)
    {
        var report = new Report();
        using (var xmlReader = System.Xml.XmlReader.Create(Server.MapPath(filename), new System.Xml.XmlReaderSettings { IgnoreWhitespace = true }))
        {
            var xmlSerializer = new Telerik.Reporting.XmlSerialization.ReportXmlSerializer();
            report = (Report)xmlSerializer.Deserialize(xmlReader);
        }
        return report;
    }

    public void PrintReport(Report report)
    {
        string printerName = "Microsoft Print to PDF";
        Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
        InstanceReportSource instanceReportSource = new InstanceReportSource();
        PrinterSettings psettings = new PrinterSettings { PrinterName = printerName };
        m_streams = new List<Stream>();
        Byte[][] pages = null;

        var deviceInfo = new System.Collections.Hashtable();
        deviceInfo["OutputFormat"] = "EMF";
        deviceInfo["StartPage"] = 0;
        deviceInfo["DpiX"] = 100;
        deviceInfo["DpiY"] = 100;

        try
        {

            instanceReportSource.ReportDocument = report;
             var renderResult = reportProcessor.RenderReport("PDF", instanceReportSource, deviceInfo);         
            //Create Byte array containing the rendered image. of the 1st page.
            Byte[] firstPage = renderResult.DocumentBytes;
            var ms = new MemoryStream(firstPage);
            m_streams.Add(new MemoryStream(firstPage));

            using (FileStream file = new FileStream(@"C:\Srinivas\SliceReport\" + RandomString(8) + ".pdf", FileMode.Create, System.IO.FileAccess.Write))
            {
                byte[] bytes = new byte[firstPage.Length];
                ms.Read(bytes, 0, (int)firstPage.Length);
                file.Write(bytes, 0, bytes.Length);
                ms.Close();
            }

            //////// The total number of pages of the report is 1 + the streamIDs         
            //////int m_numberOfPages = 1;
            //////pages = new Byte[m_numberOfPages][];
            //////pages[0] = firstPage;
            //////m_currentPageIndex = 0;

            //////if (m_streams[0].Length > 1000)
            //////{
            //////    //if there's nothing to print return
            //////    if (m_streams == null || m_streams.Count == 0)
            //////        return;

            //////    PrintDocument printDoc = new PrintDocument();
            //////   // printerName = "HP LaserJet Pro MFP M226dw UPD PS";
            //////    //set the printername, deal w/ the printer being invalid
            //////    printDoc.PrinterSettings.PrinterName = printerName;
            //////    if (!printDoc.PrinterSettings.IsValid)
            //////    {
            //////        string msg = String.Format("Can't find printer \"{0}\".", printerName);

            //////        return;
            //////    }

            //////    //set the printername, deal w/ the printer being invalid
            //////    printDoc.PrinterSettings.PrinterName = printerName;

            //////    PaperSize pageCustomSize = new PaperSize("Custom", 400, 500);
            //////    printDoc.DefaultPageSettings.PaperSize = pageCustomSize;

            //////    //Margins margins = new Margins(25, 25, 10, 10);
            //////    //printDoc.DefaultPageSettings.Margins = margins;

            //////    //attatch an event handler that will fire for each page that is printed.
            //////    printDoc.PrintPage += new PrintPageEventHandler(PrintPage);

            //////    //Set instance of print controller to hide page print popup message
            //////    printDoc.PrintController = new System.Drawing.Printing.StandardPrintController();

            //////    ////call print method, which get's the process going and ultimately calls the printpage method via the eventhandler.
            //////    printDoc.Print();
            //////   //Centiv.RawPrinterHelper.SendMemoryToPrinter(printerName, new MemoryStream(firstPage));

            //////    //Dispose of Print Document
            //////    printDoc.Dispose();
            //////}
            //////else
            //////{
            //////    //invalid report
            //////}

            DisposeReport();
        }
        catch (Exception ex)
        {

           //handle error
        }
    }


    public void RenderinPage(Report report)
    {
        string  printerName = "Microsoft Print to PDF";
        Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
        InstanceReportSource instanceReportSource = new InstanceReportSource();
        PrinterSettings psettings = new PrinterSettings { PrinterName = printerName };
        m_streams = new List<Stream>();
        Byte[][] pages = null;

        var deviceInfo = new System.Collections.Hashtable();
        deviceInfo["OutputFormat"] = "EMF";
        deviceInfo["StartPage"] = 0;
        deviceInfo["DpiX"] = 100;
        deviceInfo["DpiY"] = 100;

        try
        {
            instanceReportSource.ReportDocument = report;
            var renderResult = reportProcessor.RenderReport("PDF", instanceReportSource, deviceInfo);

            // Create Byte array containing the rendered image of the 1st page.
            Byte[] firstPage = renderResult.DocumentBytes;
            var ms = new MemoryStream(firstPage);
            m_streams.Add(new MemoryStream(firstPage));

            // Stream the PDF content to the response for inline display
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "inline; filename=" + renderResult.DocumentName + "." + renderResult.Extension);
            Response.BinaryWrite(renderResult.DocumentBytes);
            Response.End();

            // Create Byte array containing the rendered image of the 1st page.
            //Byte[] firstPage = renderResult.DocumentBytes;
            //var ms = new MemoryStream(firstPage);
            //m_streams.Add(new MemoryStream(firstPage));

            // Save the PDF to a file
            string fileName = renderResult.DocumentName + "." + renderResult.Extension;
            string path = Server.MapPath("~/Reports/");
            string filePath = Path.Combine(path, fileName);

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                fs.Write(renderResult.DocumentBytes, 0, renderResult.DocumentBytes.Length);
            }


            pdfFrame.Attributes["src"] = filePath;


            ////// You can also stream the PDF directly to the response
            ////Response.Clear();
            ////Response.ContentType = "application/pdf";
            ////Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            ////Response.BinaryWrite(renderResult.DocumentBytes);
            ////Response.End();
        }
        catch (Exception ex)
        {
            // Handle exceptions
        }
    }

    private static Random random = new Random();

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public void PrintPage(object sender, PrintPageEventArgs ev)
    {
        try
        {
            //create a new metafile based on the page that we're trying to print.
            //Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
            System.Drawing.Image img = System.Drawing.Image.FromStream(m_streams[0]);

            //draw the an image deciding what to draw, and where to place it on the page.
            //ev.Graphics.DrawImage(pageImage, 0, 0);
            ev.Graphics.DrawImage(img, new Rectangle(new Point(0, 0), new Size(400, 500)));

            //increment to the next page.
            m_currentPageIndex++;

            //decide if we've read our last stream.
            ev.HasMorePages = false;
        }
        catch (Exception ex)
        {
           //Handle Error
           
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
           //Handle Exception
        }

    }
}