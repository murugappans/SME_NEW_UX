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
using Telerik.Web.UI;
using System.Data.SqlClient;


namespace SMEPayroll.Company
{
    public partial class CertificateInformaton : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                            Response.Redirect("../SessionExpire.aspx");

            if (Session["Certificationinfo"] == null)
            {
                //...Read Data From TextFile and show data in data grid for Certification...
                string filePath = Utility.ToString(System.Configuration.ConfigurationSettings.AppSettings["CERTIFICATEFILE_PATH"]);
                //...Read Data From TextFile and show data in data grid for Certification...
                if (System.IO.File.Exists(filePath))
                {
                    DataSet Certificationinfo = Utility.GetDataSetFromTextFile(filePath);
                    RadGridCertification.DataSource = Certificationinfo;
                    Session["Certificationinfo"] = Certificationinfo;
                    RadGridCertification.DataBind();
                }
            }
            else if (Session["Certificationinfo"] != null)
            {
                DataSet CertInfo = (DataSet)Session["Certificationinfo"];
                RadGridCertification.DataSource = CertInfo;
                RadGridCertification.DataBind();
            }
        }
    }
}
