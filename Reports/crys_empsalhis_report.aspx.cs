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
    public partial class crys_empsalhis_report : System.Web.UI.Page
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
                cmbYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
                cmbmonth1.Value = Utility.ToString(System.DateTime.Now.AddMonths(-1));
                cmbmonth2.Value = Utility.ToString(System.DateTime.Today.Month);

                string sSQL = "select emp_code, isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name' from employee where (termination_date is null or year(termination_date)='" + cmbYear.SelectedValue + "') and company_id = {0} order by emp_name";
                sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                cmbEmp.Items.Clear();
                cmbEmp.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", ""));
                while (dr.Read())
                {
                    cmbEmp.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                }
                cmbEmp.Value = "";
            }
        }
        protected void bnsubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrintReport.aspx?QS=empsalhis~emp_code|" + cmbEmp.Value + "~year|" + cmbYear.SelectedValue + "~month1|" + cmbmonth1.Value + "~month2|" + cmbmonth2.Value);
        }
        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }

        protected void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSQL = "select emp_code, isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name' from employee where (termination_date is null or year(termination_date)='" + cmbYear.SelectedValue + "') and company_id = {0} order by emp_name";
            sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
            cmbEmp.Items.Clear();
            cmbEmp.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", ""));
           while (dr.Read())
            {
                cmbEmp.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
            }
            cmbEmp.Value = "";
        }
    }
}
