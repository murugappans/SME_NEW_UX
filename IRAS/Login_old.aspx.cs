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
using System.Data.SqlClient;
using System.Xml;

namespace IRAS
{
    public partial class Login : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string country = "301";
        protected void Page_Load(object sender, EventArgs e)
        {
            XmlDocument docXML_CON = new XmlDocument();
            docXML_CON.Load(HttpContext.Current.Server.MapPath("~/XML/xmldata.xml"));
         
            XmlNodeList nodcountryCON = docXML_CON.GetElementsByTagName("country");
      
            for (int i = 0; i < nodcountryCON.Count; i++)
            {
                country = ((XmlElement)(nodcountryCON[i])).GetAttribute("id");
            }

            HttpContext.Current.Session["Country"] = country;
            
            lblyear.Text = "Current Year : " + DateTime.Now.Year.ToString();
            if (!IsPostBack)
            {
                Session.LCID = 2057;
                HttpContext.Current.Session.Clear();
                string sSQL = "Select Company_Id, Company_Name From Company";
                Utility.FillDropDown(drpcompany, sSQL);
            }
        }
        protected void BtnLogin(object sender, EventArgs e)
        {
            if (drpcompany.SelectedIndex.ToString() == "0")
            {
                Label1.Visible = true;
                Label1.Text = "Please select the company name and then try again";
            }
            else
            {
                try
                {
                    bool Login_OK = Utility.GetLoginOK(drpcompany.SelectedItem.Value, txtUserName.Value.ToString(), txtPwd.Value.ToString()); 
                    if (Login_OK == true)
                    {

                        XmlDocument docXML = new XmlDocument();
                        docXML.Load(HttpContext.Current.Server.MapPath("~/XML/xmldata.xml"));
                        XmlNodeList nodTitles = docXML.GetElementsByTagName("ANBProduct");
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



                        HttpContext.Current.Session["IR8AYEAR"] = cmbYear.SelectedItem.Text.ToString();
                        Utility.setAllrights(txtUserName.Value.ToString(), drpcompany.SelectedItem.Value);
                        Response.Redirect("frames/default.aspx");
                    }
                    else
                    {
                        this.Label1.Visible = true;
                        Label1.Text = "Invalid Login (or) Inactive User Account. Please Try Again";
                    }
                }
                catch (Exception exc)
                {
                    throw exc;
                }
            }
        }
    }
}
