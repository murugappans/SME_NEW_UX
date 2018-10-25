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
    public partial class LeaveTranferAndEncash : System.Web.UI.Page
    {
         string comp_id = "", emp_code = "";
        protected bool supervisor;

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            comp_id = Session["Compid"].ToString();
            emp_code = Session["EmpCode"].ToString();
            if (!IsPostBack)
            {
                string SQLQuery;
                SQLQuery = "select emp_code from employee where emp_supervisor=" + emp_code + " and company_id=" + comp_id;
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQLQuery, null);
                if (dr.Read())
                {
                    if ((Utility.ToInteger(dr[0].ToString())) > 0) { supervisor = true; }
                    else { supervisor = false; }
                }
            }
        }
    }
}
