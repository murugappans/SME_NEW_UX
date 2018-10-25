using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace SMEPayroll
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Open the XML file

            string country = "301";
            HttpContext.Current.Session.Clear();
            Session.LCID = 2057;
            XmlDocument docXML = new XmlDocument();
            docXML.Load(HttpContext.Current.Server.MapPath("~/XML/xmldata.xml"));
            XmlNodeList nodTitles = docXML.GetElementsByTagName("ANBProduct");
            XmlNodeList nodTitles2 = docXML.GetElementsByTagName("CERTIFICATE_MERGE");
            XmlNodeList nodTitles3 = docXML.GetElementsByTagName("PROJECT_REPORT");
            XmlNodeList nodTitles4 = docXML.GetElementsByTagName("JV_GL");
            XmlNodeList nodcountry = docXML.GetElementsByTagName("country");
            for (int i = 0; i < nodTitles.Count; i++)
            {
                HttpContext.Current.Session[((XmlElement)(nodTitles[i])).GetAttribute("id")] = ((XmlElement)(nodTitles[i])).GetAttribute("text");
            }

            for (int i = 0; i < nodcountry.Count; i++)
            {
                country = ((XmlElement)(nodcountry[i])).GetAttribute("id");
            }

            HttpContext.Current.Session["Country"] = country;
            //----murugan 4 whitelable
            // XmlDocument docXML2 = new XmlDocument();
            XmlNodeList nodTitles22 = docXML.GetElementsByTagName("WHITEProduct");
            for (int i = 0; i < nodTitles22.Count; i++)
            {
                HttpContext.Current.Session[((XmlElement)(nodTitles22[i])).GetAttribute("id")] = ((XmlElement)(nodTitles22[i])).GetAttribute("text");
            }

            //------------ Certificate-Merge

            for (int i = 0; i < nodTitles2.Count; i++)
            {
                HttpContext.Current.Session[((XmlElement)(nodTitles2[i])).GetAttribute("id")] = ((XmlElement)(nodTitles2[i])).GetAttribute("text");
            }
            //-------------Project reports
            for (int i = 0; i < nodTitles3.Count; i++)
            {
                HttpContext.Current.Session[((XmlElement)(nodTitles3[i])).GetAttribute("id")] = ((XmlElement)(nodTitles3[i])).GetAttribute("text");
            }

            //-------------MADSOFT GL
            for (int i = 0; i < nodTitles4.Count; i++)
            {
                HttpContext.Current.Session[((XmlElement)(nodTitles4[i])).GetAttribute("id")] = ((XmlElement)(nodTitles4[i])).GetAttribute("text");
            }

            //-----------
            string LiginType = HttpContext.Current.Session["ANBPRODUCT"].ToString();

            if (LiginType == "WMS" || LiginType == "WMSI" || LiginType == "SME" || LiginType == "SMEMC" || LiginType == "SMEA" || LiginType == "WMSA" || LiginType == "WMSAMC" || LiginType == "WMSMC")
            {
                if (Request.QueryString["sessiontimeout"] != null)
                {
                    if (Request.QueryString["sessiontimeout"].ToString() == "true")
                    {
                        Response.Redirect("Login_soft.aspx?pid=" + LiginType + "& sessiontimeout=true");
                    }
                }
                else
                    Response.Redirect("Login_soft.aspx?pid=" + LiginType);


            }

            else {
                return;
            }
        }
    }
}
