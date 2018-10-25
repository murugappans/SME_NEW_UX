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

namespace SMEPayroll.IR8A
{
    public partial class Ir8aGrid : System.Web.UI.Page
    {
        string compid = "", empcode = "";

        string sSQL = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string s = Request.QueryString["id"].ToString();
            string empCode = Request.QueryString["empCode"].ToString();
            string year = Request.QueryString["year"].ToString();
            string compId = Request.QueryString["compId"].ToString();
            if (s == "IR8a")
            {
                sSQL = "sp_EMP_IR8A_MonthReports";
                SqlParameter[] parms = new SqlParameter[3];

                parms[0] = new SqlParameter("@year", Utility.ToInteger(year));
                parms[1] = new SqlParameter("@companyid", Utility.ToInteger(compId));
                parms[2] = new SqlParameter("@EmpCode", Utility.ToInteger(empCode));
                RadGrid1.DataSource = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);
                RadGrid1.DataBind();
            }
            

        }
    }
}
