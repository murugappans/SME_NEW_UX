using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;

namespace SMEPayroll.Invoice
{
    public partial class QuotationUC_Monthly : System.Web.UI.UserControl
    {
        private object _dataItem = null;
        protected int comp_id;
        public string SQLEmpDrop;
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(QuotationUC_Monthly), this.Page);


            drpWorkingdaysWeek.Attributes.Add("onChange", "return CalculateDailyRate();");

            //calling javascript for daily rate
            Response.Write("<script language='javascript'>CalculateDailyRate();</script>");
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            comp_id = Utility.ToInteger(Session["Compid"]);
            Emp_Dropdown();
            Trade_Dropdown();
            this.DataBinding += new EventHandler(Update_SelectValue);
        }

        private void Update_SelectValue(object sender, EventArgs e)
        {
            try
            {
                #region TradeID
                object TradeID = DataBinder.Eval(DataItem, "TradeID");
                if (TradeID == DBNull.Value || Convert.ToInt32(TradeID) == 0)
                {
                    //if dropdown as selected value is 0 then set to 1
                    //drpTrade.SelectedValue = "1";
                }
                else
                {

                    drpTrade.SelectedValue = TradeID.ToString();
                    drpTrade.Enabled = false;
                }
                #endregion

                #region WorkingDays/week
                object drpWorkingdaysWeek1 = DataBinder.Eval(DataItem, "Workingdays");
                if (drpWorkingdaysWeek1 == DBNull.Value || Convert.ToInt32(drpWorkingdaysWeek1) == 0)
                {
                    //if dropdown as selected value is 0 then set to 1
                    //drpTrade.SelectedValue = "1";
                }
                else
                {

                    drpWorkingdaysWeek.SelectedValue = drpWorkingdaysWeek1.ToString();
                }
                #endregion

                #region Employee List
                object EmpCode = DataBinder.Eval(DataItem, "EmpCode");
                if (EmpCode == DBNull.Value || Convert.ToInt32(EmpCode) == 0)
                {
                    //if dropdown as selected value is 0 then set to 1
                    //drpTrade.SelectedValue = "1";
                }
                else
                {
                    drpEmp.SelectedValue = EmpCode.ToString();
                    drpEmp.Enabled = false;
                }
                #endregion

                #region Monthly Salary
                object Monthly = DataBinder.Eval(DataItem, "Monthly");
                txtMonthly.Text = Monthly.ToString();
                #endregion

                #region OT1
                object OT1 = DataBinder.Eval(DataItem, "OT1");
                if (OT1 == DBNull.Value || Convert.ToInt32(OT1) == 0)
                {
                    txtOT1_M.Text = "";
                }
                else
                {
                    txtOT1_M.Text = OT1.ToString().Trim();
                }
                #endregion

                #region OT2
                object OT2 = DataBinder.Eval(DataItem, "OT2");
                if (OT2 == DBNull.Value || Convert.ToInt32(OT2) == 0)
                {
                    txtOT2_M.Text = "";

                }
                else
                {
                    txtOT2_M.Text = OT2.ToString().Trim();
                }
                #endregion

            }
            catch
            {
                throw;
            }
        }

        private void Trade_Dropdown()
        {
            string sSQL = "SELECT [id], [Trade] FROM [Trade] where  Company_id='" + comp_id + "'";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
            drpTrade.Items.Clear();
            drpTrade.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "-1"));

            while (dr.Read())
            {
                drpTrade.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
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
        
        protected void drpTrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            Emp_Dropdown();
        }

        protected void Emp_Dropdown()
        {
            DataSet ds_Emp = new DataSet();


            string QuotationNo = Request.QueryString["Quotation"];
            if (drpTrade.SelectedValue != "")
            {
                SQLEmpDrop = "select (emp_name +''+ emp_lname) as Emp,Emp_code  from Employee where trade_id='" + drpTrade.SelectedValue + "' AND Company_Id='" + comp_id + "'";
            }
            else//when update button is clicked
            {
                //object TradeID = DataBinder.Eval(DataItem, "TradeID");
                SQLEmpDrop = "select (emp_name +''+ emp_lname) as Emp,Emp_code  from Employee where trade_id='" + (string)Session["TradeId"] + "' AND  Company_Id='" + comp_id + "' AND trade_id<>''";
                //SQLEmpDrop = "select (emp_name +''+ emp_lname) as Emp,Emp_code  from Employee";
            }
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQLEmpDrop, null);
            drpEmp.Items.Clear();
            drpEmp.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "-1"));

            while (dr.Read())
            {
                drpEmp.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(0)), Utility.ToString(dr.GetValue(1))));
            }

            //ds_Emp = DataAccess.FetchRS(CommandType.Text, SQLEmpDrop, null);
            //if (ds_Emp.Tables[0].Rows.Count > 0)
            //{
            //    drpEmp.DataSource = ds_Emp.Tables[0];
            //    drpEmp.DataTextField = ds_Emp.Tables[0].Columns["Emp"].ColumnName.ToString();
            //    drpEmp.DataValueField = ds_Emp.Tables[0].Columns["Emp_code"].ColumnName.ToString();
            //    drpEmp.DataBind();
            //}
        }
      

        [AjaxPro.AjaxMethod]//using this method in javascript
        public string calculate_DailyRate(float payrate, float workingdays)
        {
            string valResult = "";
            string SQL = "select dbo.fn_GetDailyRate(" + payrate + "," + workingdays + ")";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQL, null);
            while (dr.Read())
            {
                valResult = Utility.ToString(dr.GetValue(0));
            }
            return Utility.ToString(valResult);
        }


    }
}