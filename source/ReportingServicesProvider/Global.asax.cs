using System;
using ServiceStack;

namespace ReportingServicesProvider
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Licensing.RegisterLicenseFromFileIfExists("~/ServiceStackLicense.txt".MapHostAbsolutePath());
            new AppHost().Init();
        }
    }
}