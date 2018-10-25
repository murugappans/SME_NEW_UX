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
using Telerik.Web.UI;

namespace SMEPayroll.CPF
{
    public partial class chequeproprint : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int compid;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.LCID = 2057;
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compid = Utility.ToInteger(Session["Compid"]);
           
            if (!IsPostBack)
            {
               
                cmbmonth.Value = Utility.ToString(System.DateTime.Today.Month);
                cmbYear.Value = Utility.ToString(System.DateTime.Today.Year);
                RadDatePicker1.SelectedDate = System.DateTime.Now;
            }
        }

        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }
        private DataSet PayDetails
        {
            get
            {
                DataSet ds = new DataSet();

                string sSQL = "select (select isnull(emp_name,'')+' '+isnull(emp_lname,'') from employee where emp_code=pd.emp_id) 'EmpName','" + RadDatePicker1.SelectedDate.Value.ToString("dd/MM/yyyy") + "' 'Date',";//,convert(varchar(20),ph.start_period,103) 'Date',";
                sSQL += "Convert(numeric(10,2),convert(varchar(20),DecryptByAsymKey(AsymKey_ID('AsymKey'), netpay))) 'netpay','Salary for the month of' + ' '  + DATENAME(month, ph.start_period) [description]";
                sSQL += "from prepare_payroll_hdr ph,prepare_payroll_detail pd where pd.emp_id in(select emp_code from employee where company_id = '" + compid + "') ";
                sSQL += "and pd.trx_id=ph.trx_id and status='G' and month(ph.start_period)='" + cmbmonth.Value + "' and year(ph.start_period)='" + cmbYear.Value + "'";
                ds = GetDataSet(sSQL);
                return ds;
            }
        }

        protected void bindgrid(object sender, ImageClickEventArgs e)
        {
            RadGrid1.Visible = true;
            Button1.Visible = true;
            CheckBox1.Visible = true;
            RadGrid1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.IgnorePaging = CheckBox1.Checked;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.ExportSettings.FileName = "Sheet1";
            RadGrid1.MasterTableView.ExportToExcel();
        }

        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            this.RadGrid1.DataSource = this.PayDetails;
        }
    }
   
}
