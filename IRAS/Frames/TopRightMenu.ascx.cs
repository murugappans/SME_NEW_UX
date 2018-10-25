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
using System.Xml;
namespace IRAS
{
    public partial class TopRightMenu : System.Web.UI.UserControl
    {
        string comp_id;
        string sUserName;
        public string menuUrl = "";
        public string menuLeftUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            comp_id = Session["Compid"].ToString();
            sUserName = Utility.ToString(Session["Username"]);
        }
    }
}
