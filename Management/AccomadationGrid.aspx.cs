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
    public partial class AccomadationGrid : System.Web.UI.Page
    {
        string compid = "", empcode = "";

        string sSQL = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string compId = Request.QueryString["compId"].ToString();
            string accCode = Request.QueryString["accCode"].ToString();
            sSQL = "sp_AccomadationDetails";
            SqlParameter[] parms = new SqlParameter[2];

            parms[0] = new SqlParameter("@compId", compId);
            parms[1] = new SqlParameter("@accCode", Convert.ToString(accCode));

            RadGrid1.DataSource = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);
            RadGrid1.DataBind();

        }
    }
}