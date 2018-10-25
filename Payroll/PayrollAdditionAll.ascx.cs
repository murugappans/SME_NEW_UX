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


namespace SMEPayroll.Payroll
{
    public partial class PayrollAdditionAll1 : System.Web.UI.UserControl
    {
        int compid;
        string varEmpCode = "";
        string sSQL = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
            varEmpCode = Session["EmpCode"].ToString();
            if (!IsUserControlPostBack)
            {
                this.ViewState.Add("IsUserControlPostBack", true);
                bindgrid();
                DataSet ds_employee = new DataSet();
                sSQL = "SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null and company_id=" + compid + " order by emp_name";
                ds_employee = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                drpemployee.DataSource = ds_employee.Tables[0];
                drpemployee.DataTextField = ds_employee.Tables[0].Columns["emp_name"].ColumnName.ToString();
                drpemployee.DataValueField = ds_employee.Tables[0].Columns["emp_code"].ColumnName.ToString();
                drpemployee.DataBind();
            }
        }
        private DataSet AllowanceDetails
        {
            get
            {
                DataSet ds = new DataSet();

                if (compid == 1)
                    sSQL = "SELECT [id], [desc], tax_payable_options, company_id FROM [additions_types] where company_id=-1 or company_id=" + compid + " And (OptionSelection='General' )";
                else
                    sSQL = "SELECT [id], [desc], tax_payable_options, company_id FROM [additions_types] where company_id=" + compid + " And (OptionSelection='General' )";

                ds = GetDataSet(sSQL);
                return ds;
            }
        }
        private void bindgrid()
        {
            RadGrid1.DataSource = AllowanceDetails;
            RadGrid1.DataBind();
        }
        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }
        protected void drpemployee_databound(object sender, EventArgs e)
        {
            drpemployee.Items.Insert(0, new ListItem("- All Employees -", "-1"));
            drpemployee.Items.Insert(0, new ListItem("-select-", "-select-"));
        }

    
        protected bool IsUserControlPostBack
        {
            get
            {
                return this.ViewState["IsUserControlPostBack"] != null;
            }
        }
        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            int result = 0;
            if (e.CommandName == "UpdateAll")
            {   foreach (GridItem item in RadGrid1.MasterTableView.Items)
                {
                    string sqlQuery = "INSERT INTO EMP_ADDITIONS(trx_period,created_on,emp_code,additionsforYear,trx_type,trx_amount) ";
                    string transId = "";
                    double amount = 0;
                    int i = 0;

                    SqlParameter[] parms = new SqlParameter[6];
                    parms[i++] = new SqlParameter("@trx_period", Utility.ToString(RadDatePicker1.SelectedDate));
                    parms[i++] = new SqlParameter("@created_on", Utility.ToLongDate(System.DateTime.Today.ToLongDateString()));
                    parms[i++] = new SqlParameter("@emp_code", Utility.ToString(drpemployee.SelectedValue));
                    parms[i++] = new SqlParameter("@additionsforYear", Utility.ToString(drpAdditionYear.SelectedValue));
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        transId = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("id"));
                        TextBox txtbox = (TextBox)dataItem.FindControl("txtAmount");
                         amount = Utility.ToDouble(txtbox.Text);
                         parms[i++] = new SqlParameter("@trx_type", Utility.ToInteger(transId));
                        parms[i++] = new SqlParameter("@trx_amount", Utility.ToString(amount));
                    }
                    if (amount > 0)
                    {
                        sqlQuery = "INSERT INTO EMP_ADDITIONS(trx_period,created_on,emp_code,additionsforYear,trx_type,trx_amount) ";
                        sqlQuery = sqlQuery + "values ( @trx_period,@created_on,@emp_code,@additionsforYear,@trx_type,@trx_amount)";
                        result = result + DataAccess.ExecuteNonQuery(sqlQuery, parms);
                    }

                }
            }
            if (result > 0)
            {
                lblerr.Text  = "No Of Additions Inserted : " + result;
            }
        }
    }
}
