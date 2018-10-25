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
using System.Data.Sql;
using Telerik.Web.UI;

namespace SMEPayroll.Payroll
{
    public partial class claimaddition : System.Web.UI.UserControl
    {
        private object _dataItem = null;
        int compid;
        string varEmpCode = "";
        static string username = "";
        static string sUserName = "";
       
        protected void Page_Load(object sender, EventArgs e)
        {
           AjaxPro.Utility.RegisterTypeForAjax(typeof(claimaddition));

            compid = Utility.ToInteger(Session["Compid"]);
            varEmpCode = Session["EmpCode"].ToString();
            sUserName = Utility.ToString(Session["Username"]);
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            if (drpemployee.SelectedValue == "-select-")
            {
                lblsupervisor.Text = "";
            }
            //btnUpdate.Attributes.Add("OnClientClick", "ValidateData('" + txtbankcode.);
         //  cmpEndDate.ValueToCompare = DateTime.Now.ToShortDateString();

            //Check for Company Level.........
            string strMC = "Select MultiCurr From company Where Company_Id=" + compid;
            int multiCurr = 0;

            SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, strMC, null);
            while (dr1.Read())
            {
                if (dr1.GetValue(0) != null)
                {
                    if (dr1.GetValue(0).ToString() != "")
                    {
                        multiCurr = Convert.ToInt32(dr1.GetValue(0));
                    }
                }
            }
            if (multiCurr == 0)
            {
                drpCurrency.Enabled = false;
            }

            txtamt.Attributes.Add("onBlur", "GetExchangeValue();");
            drpCurrency.Attributes.Add("OnChange", "GetExchangeValue();");
            lblComid.Text = compid.ToString();


            string SQLQueryA;
            // SQLQuery = "select count(emp_code) from employee where company_id=" + compid + " and emp_code=" + varEmpCode + " and GroupId in(select GroupId from usergroups where company_id=" + compid + " and GroupName='Super Admin')";


            SQLQueryA = "select isApproveDate from company where company_id=" + compid;
            SqlDataReader drA = DataAccess.ExecuteReader(CommandType.Text, SQLQueryA, null);
            if (drA.Read())
            {
                if (Utility.ToInteger(drA[0].ToString()) > 0)
                {
                   
                    RadDatePicker2.Visible = false;
                    RadDatePicker1.Visible = false;
                    Tra.Visible = false;
                    From.Visible = false;
                    to.Visible = false;
                    //dtrow.Style.Add("display", "none");
                }
                else
                {
                    RadDatePicker2.Visible = true;
                    RadDatePicker1.Visible = true;
                    Tra.Visible = true;
                    From.Visible = true;
                    to.Visible = true;
                }
            }

         
            

        }
        public object DataItem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
        }
        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }
        bool isMultiLevel = true;
        protected void detailbind()
        {
            //string strSql = "select a.CliamsupervicerMulitilevel, a.emp_code,a.username, a.emp_name+' '+a.emp_lname 'emp_name', b.emp_name+' '+b.emp_lname emp_clsupervisor from employee a left Outer join employee b On a.emp_supervisor=b.emp_code Left Outer Join Company c on a.company_id=c.company_id   ";
            string strSql = "select a.CliamsupervicerMulitilevel, a.emp_code,a.username, a.emp_name+' '+a.emp_lname 'emp_name', b.emp_name+' '+b.emp_lname emp_clsupervisor from employee a left Outer join employee b On a.emp_clsupervisor=b.emp_code Left Outer Join Company c on a.company_id=c.company_id   ";
            
            strSql += " where  a.emp_code=" + drpemployee.SelectedValue;
            DataSet leaveset = new DataSet();
            leaveset = getDataSet(strSql);
            int temp = leaveset.Tables[0].Rows.Count;
            if (temp != 0)
            {
                lblsupervisor.Text = Utility.ToString(leaveset.Tables[0].Rows[0]["emp_clsupervisor"]);

                username = Utility.ToString(leaveset.Tables[0].Rows[0]["username"]);
                lblsupervisor_name.Text = "";

                if (leaveset.Tables[0].Rows[0]["CliamsupervicerMulitilevel"].ToString() != "-1")
                {
                    if (leaveset.Tables[0].Rows[0]["CliamsupervicerMulitilevel"].ToString() != "")
                    {


                        string wdsql = "select WL.PayrollGroupID from EmployeeAssignedToPayrollGroup EP inner join [EmployeeWorkFlowLevel] WL  on EP.PayrollGroupID = WL.PayRollGroupID  where WL.ID=" + leaveset.Tables[0].Rows[0]["CliamsupervicerMulitilevel"].ToString();

                        DataSet approverCode = new DataSet();
                        approverCode = getDataSet(wdsql);
                        int Cnt = approverCode.Tables[0].Rows.Count;
                        if (Cnt != 0)
                        {
                            isMultiLevel = true;
                            lblsupervisor.Text = approverCode.Tables[0].Rows[0]["PayrollGroupID"].ToString();
                            string sql="select emp_name from employee,[EmployeeAssignedToPayrollGroup] where emp_code=emp_id and payrollgroupid="+approverCode.Tables[0].Rows[0]["PayrollGroupID"].ToString();
                            SqlDataReader dr=DataAccess.ExecuteReader(CommandType.Text ,sql,null);
                            if (dr.Read())
                            {
                                //lblsupervisor.Text = dr[0].ToString();
                                lblsupervisor_name.Text = "[" + dr[0].ToString() + "]";
                            }
                            else {
                                lblsupervisor_name.Text = "";
                            }
                             
                        }



                    }
                }
                
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.DataBinding += new EventHandler(addition_DataBinding);
        }

        void addition_DataBinding(object sender, EventArgs e)
        {

            //Currecny Data binding
            DataSet dscurr = new DataSet();
            string sqlCurr = "Select id, Currency + ':-->' + Symbol Curr from currency";
            dscurr = DataAccess.FetchRS(CommandType.Text, sqlCurr, null);
            drpCurrency.DataSource = dscurr.Tables[0];
            drpCurrency.DataTextField = dscurr.Tables[0].Columns["Curr"].ColumnName.ToString();
            drpCurrency.DataValueField = dscurr.Tables[0].Columns["id"].ColumnName.ToString();
            drpCurrency.DataBind();

            object currency = DataBinder.Eval(DataItem, "CurrencyID");
            if (currency == null || currency.ToString() == "{}" || currency.ToString() == "")
            {
                string sqlCurr1 = "select CurrencyID from company where Company_Id =" + compid;
                SqlDataReader drcurr = DataAccess.ExecuteReader(CommandType.Text, sqlCurr1, null);
                string val = "";
                while (drcurr.Read())
                {
                    val = (string)drcurr.GetValue(0).ToString();
                }
                drpCurrency.SelectedValue = Utility.ToString(val.ToString());
            }
            else
            {
                drpCurrency.SelectedValue = Utility.ToString(currency.ToString());
            }

            /* Binding Country Drop down list*/
            DataSet ds_employee = new DataSet();
            string sSQL;
            string sgroupname = "";

            //SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "SELECT GroupName FROM UserGroups ug, Employee emp WHERE emp.GroupID = ug.GroupID AND emp.UserName = '" + sUserName + "' ", null);
            //if (dr.Read())
            //{
            //    sgroupname = Utility.ToString(dr.GetValue(0));
            //}
            //Senthil for Group Management
            sgroupname = Utility.ToString(Session["GroupName"]);

            if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Apply Claim for all") == true))
            {
                sSQL = "SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null and company_id=" + compid + " order by emp_name";
            }
            else
            {
                if (Utility.GetGroupStatus(compid) == 1)
                {

                    sSQL = "SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null  and company_id=" + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") order by emp_name"; //Grouping
                }
                else
                {
                    sSQL = "SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null and emp_code='" + varEmpCode + "' and company_id=" + compid + " order by emp_name";
                }
               
            }
            ds_employee = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            drpemployee.DataSource = ds_employee.Tables[0];
            drpemployee.DataTextField = ds_employee.Tables[0].Columns["emp_name"].ColumnName.ToString();
            drpemployee.DataValueField = ds_employee.Tables[0].Columns["emp_code"].ColumnName.ToString();
            drpemployee.DataBind();

            object Employeeid = DataBinder.Eval(DataItem, "emp_code");
            if (Employeeid != DBNull.Value)
            {
                drpemployee.SelectedValue = Employeeid.ToString();
            }


            DataSet ds_additiontype = new DataSet();
            if (compid == 1)
                sSQL = "SELECT [id], [desc] FROM [additions_types] where optionselection='Claim' and (company_id=-1 or company_id='" + compid + "')";
            else
                sSQL = "SELECT [id], [desc] FROM [additions_types] where optionselection='Claim' and company_id=" + compid;
            ds_additiontype = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            drpaddtype.DataSource = ds_additiontype.Tables[0];
            drpaddtype.DataTextField = ds_additiontype.Tables[0].Columns["desc"].ColumnName.ToString();
            drpaddtype.DataValueField = ds_additiontype.Tables[0].Columns["id"].ColumnName.ToString();
            drpaddtype.DataBind();

            object addition_type = DataBinder.Eval(DataItem, "id");
            if (addition_type != DBNull.Value)
            {
                drpaddtype.SelectedValue = addition_type.ToString();
            }


        }
     
        protected void drpemployee_DataBound(object sender, EventArgs e)
        {
            string sgroupname = Utility.ToString(Session["GroupName"]);
            if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Apply Claim for all") == true))
            {
                drpemployee.Items.Insert(0, new ListItem("- All Employees -", "-1"));
            }
            drpemployee.Items.Insert(0, new ListItem("-select-", "-select-"));

        }


        protected void drpaddtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSQL = "select [id],cpf from additions_types where id=" + Utility.ToInteger(drpaddtype.SelectedItem.Value);
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
            if (dr.Read())
            {
            }
        }

        protected void drpaddtype_DataBound(object sender, EventArgs e)
        {
            drpaddtype.Items.Insert(0, new ListItem("-select-", "-select-"));
        }

        protected void drpemployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpemployee.SelectedValue == "-select-")
            {
                lblsupervisor.Text = "";
            }
            else
            {
                detailbind();
            }
        }

        [AjaxPro.AjaxMethod]
        public string GetExchangeValue(string currencyId, int compid1, double amount)
        {
            // compid = Utility.ToInteger(Session["Compid"]);
            //Get value from TextBox and show in label
            int COPT = 0; int mc = 0;
            string labelValue = "";
            string strcon = "Select ConversionOpt,MultiCurr FROM Company where company_id=" + compid1;
            SqlDataReader drcon = DataAccess.ExecuteReader(CommandType.Text, strcon, null);

            while (drcon.Read())
            {
                if (drcon.GetValue(0) == null)
                {
                    COPT = 1;
                }
                else
                {
                    COPT = Convert.ToInt32(drcon.GetValue(0).ToString());
                }
                if (drcon.GetValue(1) == null)
                {
                    mc = 0;
                }
                else
                {
                    mc = Convert.ToInt32(drcon.GetValue(1).ToString());
                }

            }

            double exchangeRate = 0;
            if (mc == 1)
            {

                if (COPT == 2 || COPT == 3)
                {
                    string sd, ed = "";

                    sd = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
                    //ed = transperiod2.Date.Month.ToString() + "/" + transperiod2.Date.Day.ToString() + "/" + transperiod2.Date.Year.ToString();
                    string exrate = "select  top 1  Rate from dbo.ExchangeRate where Currency_id =" + currencyId + " and [Date]<='" + sd + "'  Order by [Date] desc";

                    SqlDataReader drex = DataAccess.ExecuteReader(CommandType.Text, exrate, null);
                    while (drex.Read())
                    {
                        if (drex.GetValue(0) == null)
                        {
                            exchangeRate = 1.0;
                        }
                        else
                        {
                            exchangeRate = Convert.ToDouble(drex.GetValue(0).ToString());
                        }
                    }
                    //double amount = Convert.ToDouble((e.Item.Cells[10].Text).ToString()) / exchangeRate;
                    //e.Item.Cells[10].Text = e.Item.Cells[10].Text.ToString() + " ( " + amount.ToString("0.00") + " X " + exchangeRate + ")";
                    double val = 0;

                    if (amount.ToString() == "")
                    {
                        val = 0;
                    }
                    else
                    {
                        val = Convert.ToDouble(amount);
                    }
                    if (val == 0)
                    {
                        labelValue = "";
                    }
                    else
                    {
                        val = val * exchangeRate;
                        labelValue = val.ToString() + " ( " + amount.ToString() + " X " + exchangeRate.ToString() + ")";
                    }

                }
            }

            return labelValue;

        }


    }
}