using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Text;

namespace IRAS
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            int i = 1;
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }


        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            int i = 0;
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            int i = 0;
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            try
            {
                HttpContext ctx = HttpContext.Current;
                Exception exception = ctx.Server.GetLastError();
                StringBuilder strerror = new StringBuilder();
                strerror.Append("<html xmlns='http://www.w3.org/1999/xhtml' ><head runat='server'><title>Error Page</title></head><body><form id='errorform1' runat='server'>");
                strerror.Append("<div><table style='width: 60%; height: 60%'><tr><td style='width: 100%; height: 19px'><span style='font-size: 9pt; font-family: Tahoma; mso-fareast-font-family: 'Times New Roman';mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA'>");
                strerror.Append("<strong>PAYROLL ENCOUNTERED THE FOLLOWING ISSUE</strong></span><br/></td></tr>");
                strerror.Append("<tr><td style='width: 100%'><span style='font-size: 9pt; font-family: Tahoma; mso-fareast-font-family: 'Times New Roman';mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA'><strong>ISSUE:</strong></span></td></tr>");
                strerror.Append("<tr><td style='width: 100%'><strong><span style='font-size: 9pt; vertical-align: top; font-family: Tahoma'>-----------------------------------------------------------------------------------------------------------------------</span></strong></td></tr>");
                strerror.Append("<tr><td style='width: 100%'><span style='font-size: 9pt; font-family: Tahoma; mso-fareast-font-family: 'Times New Roman';mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA'><span style='font-size: 9pt; font-family: Tahoma; mso-fareast-font-family: 'Times New Roman';mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA'>");
                strerror.Append("<strong>Offending URL</strong></span><span style='font-size: 9pt; font-family: Tahoma;mso-fareast-font-family: 'Times New Roman'; mso-ansi-language: EN-US; mso-fareast-language: EN-US;mso-bidi-language: AR-SA'>: ");
                strerror.Append(ctx.Request.Url.ToString());
                strerror.Append("</span></span></td></tr><tr><td style='width: 100%'><span style='font-size: 9pt; font-family: Tahoma; mso-fareast-font-family: 'Times New Roman';mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA'>");
                strerror.Append("<strong>Source: </strong></span><span style='font-size: 9pt; font-family: Tahoma;mso-fareast-font-family: 'Times New Roman'; mso-ansi-language: EN-US; mso-fareast-language: EN-US;mso-bidi-language: AR-SA'>");
                strerror.Append(exception.Source);
                strerror.Append("</span></td></tr><tr><td style='width: 100%'><span style='font-size: 9pt; font-family: Tahoma; mso-fareast-font-family: 'Times New Roman';mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA'>");
                strerror.Append("<strong>User Message: ");
                strerror.Append(exception.InnerException.Message);
                strerror.Append("</strong></span></td></tr><tr><td style='width: 100%; height: 36px'><span style='font-size: 9pt; font-family: Tahoma; mso-fareast-font-family: 'Times New Roman';mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA'>");
                strerror.Append("<strong>Sys Message: </strong></span><span style='font-size: 9pt; font-family: Tahoma;mso-fareast-font-family: 'Times New Roman'; mso-ansi-language: EN-US; mso-fareast-language: EN-US;mso-bidi-language: AR-SA'>");
                strerror.Append(exception.Message);
                strerror.Append("</span></td></tr><tr><td style='width: 100%'><span style='font-size: 9pt; font-family: Tahoma; mso-fareast-font-family: 'Times New Roman';   mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA'>");
                strerror.Append("<strong>Stack Trace :</strong></span><span style='font-size: 9pt; font-family: Tahoma;mso-fareast-font-family: 'Times New Roman'; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA'>");
                strerror.Append(exception.StackTrace);
                strerror.Append("</span></td></tr><tr><td style='width: 100%; height: 33px'><span style='font-size: 9pt; font-family: Tahoma; mso-fareast-font-family: 'Times New Roman';mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA'><strong>Company Name: </strong></span><span style='font-size: 9pt; font-family: Tahoma;mso-fareast-font-family: 'Times New Roman'; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA'>");
                strerror.Append(Session["CompanyName"].ToString());
                strerror.Append("</span></td></tr><tr><td style='width: 100%; height: 21px'><p class='MsoNormal' style='margin: 0in 0in 0pt'><b style='mso-bidi-font-weight: normal'><span style='font-size: 9pt; font-family: Tahoma'>-----------------------------------------------------------------------------------------------------------------------</span></b><span style='font-size: 9pt; font-family: Tahoma'><?xml namespace='' ns='urn:schemas-microsoft-com:office:office' prefix='o' ?><o:p></o:p></span></p></td></tr>");
                strerror.Append("<tr><td style='width: 100%'><span style='font-size: 9pt; font-family: Tahoma; mso-fareast-font-family: 'Times New Roman';mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA'>");
                strerror.Append("<strong>ACTION:</strong></span></td></tr> <tr><td style='width: 100%'><p class='MsoNormal' style='margin: 0in 0in 0pt'><b style='mso-bidi-font-weight: normal'><span style='font-size: 9pt; font-family: Tahoma'>Contact</span></b><span style='font-size: 9pt; font-family: Tahoma'>: PAYROLL Administrator or<o:p></o:p></span></p><p class='MsoNormal' style='margin: 0in 0in 0pt'><b style='mso-bidi-font-weight: normal'><span style='font-size: 9pt; font-family: Tahoma'>Email</span></b><span style='font-size: 9pt; font-family: Tahoma'>: The above issue to <a href='mailto:support@smepayroll.com'>support@smepayroll.com</a><o:p></o:p></span></p></td></tr>");
                strerror.Append("<tr><td style='width: 100%; height: 41px'><strong><span style='font-size: 9pt; font-family: Tahoma'>-----------------------------------------------------------------------------------------------------------------------</span></strong></td></tr>");
                strerror.Append("<tr><td style='width: 100%'></td></tr><tr><td style='width: 100%'></td></tr><tr><td style='width: 100%'></td></tr></table></div></form></body></html>");
                ctx.Response.Write(strerror);
                ctx.Server.ClearError();
            }
            catch
            {
            }
            finally
            {
            }
        }


    }
}