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

namespace IRAS
{
    public partial class TopControlLoginDetails : System.Web.UI.UserControl
    {
        public string myUserName = null;
        public string myLoginRights = null;
        public string EmployeeName = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}