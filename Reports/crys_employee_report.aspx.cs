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

namespace SMEPayroll.Reports
{
    public partial class crys_employee_report : System.Web.UI.Page
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

                string sSQL = "select emp_code, isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name' from employee where company_id = {0} order by emp_name";
                sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                cmbEmp.Items.Clear();
                cmbEmp.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", ""));
                cmbEmp.Items.Add(new System.Web.UI.WebControls.ListItem("--All--", "-1"));
                while (dr.Read())
                {
                    cmbEmp.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                }
                cmbEmp.Value = "";
            }
        }
        protected void bnsubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrintReport.aspx?QS=empreport~empcode|" + cmbEmp.Value + "~compid|" + compid);
        }
    }
}