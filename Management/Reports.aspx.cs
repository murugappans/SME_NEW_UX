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

namespace SMEPayroll.Management
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            
            HtmlInputText txtinput = new HtmlInputText();
            txtinput.ID = "txtMsg";
            txtinput.Value = "V";
            txtinput.Style.Value = "WIDTH: 0px; HEIGHT: 0px";
            //txtinput.Style.Add("height", "0"); // = "height:0;width:0";

            this.Page.Controls.Add(txtinput);

            if (Session["Maintain"] != null)
            {
                int daysDiff = (int)Session["Maintain"];
                if (daysDiff < 0)
                {
                    Session["Maintain"] = daysDiff;
                    txtinput.Value = "I";
                }
                
            }
        }
    }
}
