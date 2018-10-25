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

namespace SMEPayroll.Main
{
    public partial class SessionExpire : System.Web.UI.Page
    {
        private void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
            if (HttpContext.Current.Session["ANBPRODUCT"] != null)
            {
                if ((HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMS")||HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMSI")
                {
                      
                    Response.Redirect("Login_soft.aspx?pid=WMS&_uid="+Session["Username"]);
                    //Response.Redirect("Frames/SessionTimeout.html?pid=WMS");//Commented
                    // Response.Redirect("LoginWMS.aspx");
                }
                else
                {
                    Response.Redirect("Login_soft.aspx?pid=SMS&_uid=" + Session["Username"]);
                    //Response.Redirect("Frames/SessionTimeout.html?pid=SMS");//Commented
                    // Response.Redirect("Login.aspx");
                }
            }
            else
            {

                var _uid = Utility.ToString(Session["Username"]);
                if (string.IsNullOrWhiteSpace(_uid))
                    Response.Redirect("Index.aspx?sessiontimeout=true");
                else
                    Response.Redirect("Index.aspx&_uid=" + Session["Username"]);
                //Response.Redirect("Frames/SessionTimeout.html?pid=_nothing");//Commented
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion
    }
}
