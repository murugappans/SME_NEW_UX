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
using System.Text;

namespace SMEPayroll.Payroll
{
    public partial class addition : System.Web.UI.UserControl
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        private object _dataItem = null;
        int compid;
        string varEmpCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {

           
            AjaxPro.Utility.RegisterTypeForAjax(typeof(addition));
            compid = Utility.ToInteger(Session["Compid"]);
            varEmpCode = Session["EmpCode"].ToString();
            if (this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.ToString() == "System.Web.UI.HtmlControls.HtmlForm")
            {
                ((HtmlInputHidden)this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.FindControl("txtRadId")).Value = this.ClientID;
            }
            //if (this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.ToString() == "System.Web.UI.HtmlControls.HtmlForm")
            //{
            //    ((HtmlInputHidden)this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.FindControl("txtRadId")).Value = this.ClientID;
            //}
            //drpaddtype.Attributes.Add("OnChange", "EnablePayableOtions();"); murugan
            drpCurrency.Attributes.Add("OnChange", "GetExchangeValue();");
            drpiras_approval.Attributes.Add("OnChange", "EnableApproval();");
            txtamt.Attributes.Add("onBlur", "GetExchangeValue();");

            btnUpdate.Attributes.Add("onclick", "javascript:return ValidateForm();");
            tr1.Attributes.Add("style", "display:none");
            tr2.Attributes.Add("style", "display:none");
            tr3.Attributes.Add("style", "display:none");
            tr4.Attributes.Add("style", "display:none");
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            //Check for Company Level... ... ... ...
            string strMC = "Select MultiCurr From company Where Company_Id=" + compid;
            int multiCurr = 0;

            SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, strMC, null);
            while (dr1.Read())
            {
                if (dr1.GetValue(0) != null )
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

          //  lblComid.Text = compid.ToString();
            //drpCurrency.SelectedIndexChanged += new EventHandler(drpCurrency_SelectedIndexChanged);
            //txtamt.TextChanged += new EventHandler(txtamt_TextChanged);

            xmldtYear1.ConnectionString = Session["ConString"].ToString();

            
        }

        void txtamt_TextChanged(object sender, EventArgs e)
        {
            //Get value from TextBox and show in label
            int COPT = 0; int mc = 0;
            string strcon = "Select ConversionOpt,MultiCurr FROM Company where company_id=" + compid;
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
                if (COPT == 2 || COPT == 4)
                {
                    string sd, ed = "";

                    sd = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
                    //ed = transperiod2.Date.Month.ToString() + "/" + transperiod2.Date.Day.ToString() + "/" + transperiod2.Date.Year.ToString();
                    string exrate = "select  top 1  Rate from dbo.ExchangeRate where Currency_id =" + drpCurrency.SelectedValue + " and [Date]<='" + sd + "'  Order by [Date] desc";

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
                    if (txtamt.Text == "")
                    {
                        val = 0;
                    }
                    else
                    {
                        val = Convert.ToDouble(txtamt.Text);
                    }
                    if (val == 0)
                    {
                        lblEr.Text = "";
                    }
                    else
                    {
                        val = val * exchangeRate;
                        lblEr.Text = val.ToString() + " ( " + txtamt.Text + " X " + exchangeRate.ToString() + ")";
                    }
                }
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

                if (COPT == 2 || COPT == 4)
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.DataBinding += new EventHandler(addition_DataBinding);
        }

        void addition_DataBinding(object sender, EventArgs e)
        {
            /* Binding Country Drop down list*/
            DataSet ds_employee = new DataSet();
            string sSQL = "";

            if (Utility.ToString(Session["GroupName"]) == "Super Admin")
            {
                sSQL = "SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where emp_name is not null and (termination_date is null  or termination_date >= getdate()) and  company_id=" + compid + " order by emp_name";
            }
            else
            {
                //sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE termination_date is null and Company_ID=" + compid + " And  Len([Time_Card_No]) >= 0 and emp_code='" + Session["EmpCode"].ToString() + "' And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                if (Utility.GetGroupStatus(compid) == 1)
                {
                    sSQL = "SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where emp_name is not null and (termination_date is null  or termination_date >= getdate()) and  company_id=" + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") order by emp_name";
                }
                else
                {
                    sSQL = "SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where emp_name is not null and(termination_date is null  or termination_date >= getdate()) and  company_id=" + compid + " order by emp_name";
                }
            }
            ds_employee = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            drpemployee.DataSource = ds_employee.Tables[0];
            drpemployee.DataTextField = ds_employee.Tables[0].Columns["emp_name"].ColumnName.ToString();
            drpemployee.DataValueField = ds_employee.Tables[0].Columns["emp_code"].ColumnName.ToString();
            drpemployee.DataBind();

            //Currecny Data binding
            DataSet dscurr = new DataSet();
            string sqlCurr = "Select id, Currency + ':-->' + Symbol Curr from currency";
            dscurr = DataAccess.FetchRS(CommandType.Text, sqlCurr, null);
            drpCurrency.DataSource = dscurr.Tables[0];
            drpCurrency.DataTextField = dscurr.Tables[0].Columns["Curr"].ColumnName.ToString();
            drpCurrency.DataValueField = dscurr.Tables[0].Columns["id"].ColumnName.ToString();
            drpCurrency.DataBind();

            //Selected Values
            object Employeeid = DataBinder.Eval(DataItem, "emp_code");
            object iras_approval = DataBinder.Eval(DataItem, "iras_approval");
            object iras_approval_date = DataBinder.Eval(DataItem, "iras_approval_date");
            object additionsyears = DataBinder.Eval(DataItem, "additionsforyear");
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

            //object Employeeid = DataBinder.Eval(DataItem, "emp_code");
            //object iras_approval = DataBinder.Eval(DataItem, "iras_approval");
            //object iras_approval_date = DataBinder.Eval(DataItem, "iras_approval_date");
            //object additionsyears = DataBinder.Eval(DataItem, "additionsforyear");

            if (iras_approval != DBNull.Value)
            {
                drpiras_approval.SelectedValue = Utility.ToString(iras_approval.ToString());
                if (iras_approval.ToString() == "Yes")
                {
                    tr4.Attributes.Add("style", "display:block");
                }
                else
                {
                    tr4.Attributes.Add("style", "display:none");
                }
            }
            else
            {
                tr4.Attributes.Add("style", "display:none");
            }

            if (additionsyears != DBNull.Value)
            {
                drpAdditionYear.SelectedValue = Utility.ToString(additionsyears.ToString());
            }
            else
            {
                drpAdditionYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
            }

            if (iras_approval_date != DBNull.Value)
            {

                DateTime dt;
                dt = new DateTime();
                dt = System.Convert.ToDateTime(iras_approval_date);
                txtiras_approval_date.Text = dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString();
            }
            if (Employeeid != DBNull.Value)
            {
                drpemployee.SelectedValue = Employeeid.ToString();
            }


            DataSet ds_additiontype = new DataSet();
            if (compid == 1)
                sSQL = "SELECT [id], [desc], tax_payable_options, company_id FROM [additions_types] where (Active=1 or Active is null) and  ((company_id=-1 or company_id=" + compid + ") And (OptionSelection='General' Or OptionSelection='Claim')) OR (upper(isShared)='YES')";
            else
                //kumar
              //  sSQL = "SELECT [id], [desc], tax_payable_options, company_id FROM [additions_types] where ((company_id=" + compid + ") And (OptionSelection='General' Or OptionSelection='Claim')) OR (upper(isShared)='YES')";

                sSQL = "SELECT [id], [desc], tax_payable_options, company_id FROM [additions_types] where (Active=1 or Active is null) and ((company_id=" + compid + ") And (OptionSelection='General')) OR (upper(isShared)='YES')";
          
            
            ds_additiontype = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            drpaddtype.DataSource = ds_additiontype.Tables[0];
            drpaddtype.DataTextField = ds_additiontype.Tables[0].Columns["desc"].ColumnName.ToString();
            drpaddtype.DataValueField = ds_additiontype.Tables[0].Columns["id"].ColumnName.ToString();
            drpaddtype.DataBind();

            drplumsum.DataSource = ds_additiontype.Tables[0];
            drplumsum.DataTextField = ds_additiontype.Tables[0].Columns["tax_payable_options"].ColumnName.ToString();
            drplumsum.DataValueField = ds_additiontype.Tables[0].Columns["id"].ColumnName.ToString();
            drplumsum.DataBind();

            object addition_type = DataBinder.Eval(DataItem, "id");
            if (addition_type != DBNull.Value)
            {
                string strswitch = "";
                DataRow[] dr;
                drpaddtype.SelectedValue = addition_type.ToString();
                dr = ds_additiontype.Tables[0].Select("id ='" + addition_type + "' And company_id=" + compid);
                if (dr.Length > 0)
                {
                    strswitch = dr[0]["tax_payable_options"].ToString();
                    ((HtmlInputHidden)this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.FindControl("txtLumSum")).Value = strswitch;
                }

                string strtax_payable_options = ((DropDownList)this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.FindControl("drplumsumswitch")).SelectedItem.Text;
                if (strtax_payable_options.ToString().IndexOf("," + strswitch + ",") >= 0)
                {
                    tr1.Attributes.Add("style", "display:block");
                    tr2.Attributes.Add("style", "display:block");
                    tr3.Attributes.Add("style", "display:block");
                    if (drpiras_approval.SelectedItem.Value.ToString() == "Yes")
                    {
                        tr4.Attributes.Add("style", "display:block");
                    }
                    else
                    {
                        tr4.Attributes.Add("style", "display:none");
                    }
                }
                else
                {
                    tr1.Attributes.Add("style", "display:none");
                    tr2.Attributes.Add("style", "display:none");
                    tr3.Attributes.Add("style", "display:none");
                    tr4.Attributes.Add("style", "display:none");
                }

            }

        }
        protected void drpemployee_databound(object sender, EventArgs e)
        {
            drpemployee.Items.Insert(0, new ListItem("- All Employees -", "-1"));
            drpemployee.Items.Insert(0, new ListItem("-select-", "-select-"));            
        }

        protected void drpaddtype_databound(object sender, EventArgs e)
        {
            drpaddtype.Items.Insert(0, new ListItem("-select-", "-select-"));
        }

        protected void drpaddtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSQL = "select [id],cpf from additions_types where id=" + Utility.ToInteger(drpaddtype.SelectedItem.Value);
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
            if (dr.Read())
            {
                lblcpf.Text = dr[1].ToString();
            }
            SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, "select tax_payable_options from additions_types where id=" + drpaddtype.SelectedValue.ToString(), null);
            string option_value = "";
            if (dr1.Read())
            {
                option_value = dr1[0].ToString();
            }

            string strtax_payable_options = ((DropDownList)this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.FindControl("drplumsumswitch")).SelectedItem.Text;

            if (strtax_payable_options.ToString().IndexOf("," + option_value + ",") >= 0)
            {
                tr1.Attributes.Add("style", "display:block");
                tr2.Attributes.Add("style", "display:block");
                tr3.Attributes.Add("style", "display:block");
                if (drpiras_approval.SelectedItem.Value.ToString() == "Yes")
                {
                    tr4.Attributes.Add("style", "display:block");
                }
                else
                {
                    tr4.Attributes.Add("style", "display:none");
                }
            }
            else
            {
                tr1.Attributes.Add("style", "display:none");
                tr2.Attributes.Add("style", "display:none");
                tr3.Attributes.Add("style", "display:none");
                tr4.Attributes.Add("style", "display:none");
            }
        }

        protected void drpirasapproval_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpiras_approval.SelectedItem.Value.ToString() == "Yes")
            {
                tr4.Attributes.Add("style", "display:block");
            }
            else
            {
                tr4.Attributes.Add("style", "display:none");
            }

        }
       

    }
}