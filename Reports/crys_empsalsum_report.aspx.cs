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

namespace SMEPayroll.Reports
{
    public partial class crys_empsalsum_report : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int compid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compid = Utility.ToInteger(Session["Compid"].ToString());
            if (!IsPostBack)
            {
                cmbYear.Value = Utility.ToString(System.DateTime.Today.Year);
                cmbmonth1.Value = Utility.ToString(System.DateTime.Now.AddMonths(-1));
                cmbmonth2.Value = Utility.ToString(System.DateTime.Today.Month);
            }
        }
        protected void bnsubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrintReport.aspx?QS=empsalsum~company_id|" + compid + "~year|" + cmbYear.Value + "~month1|" + cmbmonth1.Value + "~month2|" + cmbmonth2.Value);
        }
    }
}
