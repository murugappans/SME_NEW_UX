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

namespace SMEPayroll.TimeSheet
{
    public partial class PhotoViewer : System.Web.UI.UserControl
    {

        public string inimage
        {
            get
            {
                if (ViewState["inimage"] == null)
                {
                    return "";
                }
                return (string)ViewState["inimage"];
            }
            set
            {

                ViewState["inimage"] = value;

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}