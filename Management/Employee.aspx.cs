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
    public partial class Employee : System.Web.UI.Page
    { 
        string comp_id = "";
        protected bool approve;
        protected bool required;
    
        private void Page_Load(object sender, System.EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            comp_id = Session["Compid"].ToString();

            #region Calling document management page from 4.0 framework
            DocId.HRef = "http://localhost/DocumentManagement/DocumentManagement/Index.aspx?compid="+comp_id+"&user="+Session["anbsysadmingroup"]+"&pwd="+Session["pwd"]+"";
            #endregion

            if (!IsPostBack)
            {
                string SQLQuery;
               
                SQLQuery = "select timesheet_approve from company where company_id=" + comp_id;
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQLQuery, null);
                if (dr.Read())
                {
                    if ((dr[0].ToString()) == "1") { approve = true; required = true; }
                    else { approve = false; required = false; }
                }
            }
        }
    }
}
