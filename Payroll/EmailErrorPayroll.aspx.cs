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
using System.Collections.Generic;

namespace SMEPayroll.Payroll
{
    public partial class EmailErrorPayroll : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected void Page_Load(object sender, EventArgs e)
        {

            this.label2.Text = Request.QueryString["Err"].ToString();

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            
            //if (!IsPostBack)
            //{
            //    if (Session["Name"] != null)
            //    {
            //        List<NameList> nlist = new List<NameList>();
            //        nlist = Session["Name"] as List<NameList>;

            //        Label1.Text = nlist.Count.ToString();


            //        RadGrid1.DataSource = nlist;
            //        RadGrid1.DataBind();

            //    }
            //}

        }
    }
}
