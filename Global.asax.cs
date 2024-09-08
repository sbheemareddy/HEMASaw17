using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Telerik.Reporting;
using Telerik.Reporting.Services;


namespace HEMASaw
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

             // Add the Telerik Reporting license key
        //ReportServiceConfiguration.Instance.LicenseKey = "Your_License_Key_Here";


        }
    }
}