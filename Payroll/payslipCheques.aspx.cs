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
    public partial class payslipCheques : System.Web.UI.Page
    {
        string compid = "";
        SqlParameter[] parms;
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        static string empname = "";
        static int varEmpCode;
        int intcnt;
        int PayrollType = 0;
        string sql = null;
        DataSet monthDs;
        DataSet empDs;
        DataRow[] foundRows;
        DataTable dtFilterFound;
        protected void Page_Load(object sender, EventArgs e)
        {
            /* To disable Grid filtering options  */
            compid = Session["Compid"].ToString();
            ViewState["actionMessage"] = "";
            GridFilterMenu menu = RadGrid1.FilterMenu;
            int i = 0;
            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }

            SqlDataSource3.ConnectionString = Session["ConString"].ToString();
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            lblerror.Text = "";
            SqlDataSource1.ConnectionString = Constants.CONNECTION_STRING;
            empname = Session["Emp_Name"].ToString();
            varEmpCode = Utility.ToInteger(Session["EmpCode"]);

            int comp_id = Utility.ToInteger(Session["Compid"]);
            if (!IsPostBack)
            {
                DataSet ds = DataAccess.FetchRSDS(CommandType.Text, "SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC");
                cmbYear.DataTextField = "id";
                cmbYear.DataValueField = "id";
                cmbYear.DataSource = ds;
                cmbYear.DataBind();

                RadGrid1.ExportSettings.FileName = "Payslip_Cheques_Details";
                sql = "select PayrollType  from company where company_id = " + comp_id;
                PayrollType = DataAccess.ExecuteScalar(sql, null);

                cmbYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
                MonthFill(1);
            }
            

            RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Bottom;

        }

        protected void cmbYear_selectedIndexChanged(object sender, EventArgs e)
        {
            Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();
            MonthFill(1);
            Session["ROWID"] = cmbMonth.SelectedValue.ToString();
        }

        private void bindMonth()
        {
            MonthFill(0);
        }

        void CallBeforeMonthFill()
        {
            int i = 0;
            string ssql = "sp_GetPayrollMonth";// 0,2009,2
            SqlParameter[] parms = new SqlParameter[3];
            parms[i++] = new SqlParameter("@ROWID", "0");
            parms[i++] = new SqlParameter("@YEARS", 0);
            parms[i++] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
            monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);
            dtFilterFound = new DataTable();
            dtFilterFound = monthDs.Tables[0].Clone();
        }

        void MonthFill(int bindmonth)
        {
            CallBeforeMonthFill();
            int i = 0;
            string ssql = "sp_GetPayrollMonth";// 0,2009,2
            SqlParameter[] parms = new SqlParameter[3];
            parms[i++] = new SqlParameter("@ROWID", "0");
            parms[i++] = new SqlParameter("@YEARS", 0);
            parms[i++] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
            monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);
            dtFilterFound = new DataTable();
            dtFilterFound = monthDs.Tables[0].Clone();

            if (Session["ROWID"] == null)
            {
                foundRows = monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "'");
                foreach (DataRow dr in monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "'"))
                {
                    dtFilterFound.ImportRow(dr);
                }
            }
            else
            {
                foundRows = monthDs.Tables[0].Select("YEAR = '" + Session["ROWYEAR"].ToString() + "'");
                foreach (DataRow dr in monthDs.Tables[0].Select("YEAR = '" + Session["ROWYEAR"].ToString() + "'"))
                {
                    dtFilterFound.ImportRow(dr);
                }
            }
            if (bindmonth == 1)
            {
                cmbMonth.DataSource = dtFilterFound;
                cmbMonth.DataTextField = "MonthName";
                cmbMonth.DataValueField = "RowID";
                cmbMonth.DataBind();
                
            }
            SetControlDate();
        }

        protected void ChangeYearMonth()
        {
            if (Session["CurrentMonth"] == null)
            {
                Session["CurrentYear"] = cmbYear.SelectedItem.Value;
            }
            else
            {
                Session["CurrentYear"] = cmbYear.SelectedItem.Value;
            }
        }

        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Boolean flag = false;
            DataRow[] drResults;
            int returnval = 0;
            if (e.CommandName == "UpdateAll")
            {
                IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
                string empcode = "";
                string paySlipRemarks = "";
                foreach (GridItem item in RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;

                        //GridDataItem dataItem = (GridDataItem)item;
                        TextBox txtbox = (TextBox)dataItem.FindControl("txtRemarks");
                        paySlipRemarks = txtbox.Text.ToString();
                        empcode = this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code").ToString();

                        CallBeforeMonthFill();
                        drResults = monthDs.Tables[0].Select("RowId=" + cmbMonth.SelectedValue);
                        foreach (DataRow dr in drResults)
                        {
                            string sDate = Utility.ToDate1(dr["PaySubStartDate"].ToString());
                            string eDate = Utility.ToDate1(dr["PaySubEndDate"].ToString());
                            sDate = Convert.ToDateTime(sDate).ToString("MM/dd/yyyy", format);
                            eDate = Convert.ToDateTime(eDate).ToString("MM/dd/yyyy", format);
                            string SqlUpdate = "sp_UpdatePayslipCheques";
                            int i = 0;
                            SqlParameter[] param = new SqlParameter[6];
                            param[i++] = new SqlParameter("@rowId", Utility.ToInteger(cmbMonth.SelectedValue));
                            param[i++] = new SqlParameter("@empCode", Utility.ToString(empcode));
                            param[i++] = new SqlParameter("@startDate", Utility.ToString(sDate));
                            param[i++] = new SqlParameter("@endDate", Utility.ToString(eDate));
                            param[i++] = new SqlParameter("@chequeno", Utility.ToString(paySlipRemarks));
                            param[i++] = new SqlParameter("@remarksBy", Utility.ToString(varEmpCode));

                            returnval = returnval + Convert.ToInt32(DataAccess.ExecuteStoreProc(SqlUpdate, param));
                        }
                        if (returnval > 0)
                            //  lblerror.Text = "Employee Payslip cheques Entered/Updated Successfully";
                            ViewState["actionMessage"] = "Success|Employee Payslip cheques Entered/Updated Successfully";

                    }
                }
            }

            this.RadGrid1.DataSource = this.GenpayrollDetails;
            RadGrid1.DataBind();

        }
        protected void getData(object sender, ImageClickEventArgs e)
        {
            string ErrMsg = "";
            binddata();
        }
        protected void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            binddata();
        }
        protected void binddata()
        {
            string month = null;
            empDs = new DataSet();
            month = cmbMonth.SelectedItem.Value.ToString();
            string sSQL = "SP_GetEmployeeCheques";
            parms = new SqlParameter[3];
            parms[0] = new SqlParameter("@compId", Utility.ToInteger(compid));
            parms[1] = new SqlParameter("@Month", Utility.ToInteger(month.Trim()));
            parms[2] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"])); //Senthil for Group Management
            empDs = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);
            RadGrid1.DataSource = empDs;
            RadGrid1.DataBind();


        }

        private DataSet GenpayrollDetails
        {
            get
            {
                string sSQL = "SP_GetEmployeecheques";
                parms = new SqlParameter[3];
                parms[0] = new SqlParameter("@compId", Utility.ToInteger(compid));
                parms[1] = new SqlParameter("@Month", Utility.ToInteger(cmbMonth.SelectedValue));
                parms[2] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"]));  //Senthil for Group Management
                DataSet ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
                return ds;
            }
        }
        protected void bindgrid(object sender, EventArgs e)
        {
            intcnt = 1;
            cmbYear.Enabled = false;
            cmbMonth.Enabled = false;
            imgbtnfetch.Enabled = false;
            Session["ROWID"] = cmbMonth.SelectedValue.ToString();
            Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();
            this.RadGrid1.DataSource = this.GenpayrollDetails;
            RadGrid1.DataBind();
        }

        void SetControlDate()
        {
            if (cmbMonth.SelectedValue.ToString() != "")
            {
                IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
                DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + cmbMonth.SelectedValue);
                foreach (DataRow dr in drResults)
                {
                    Session["PayStartDay"] = dr["PayStartDay"].ToString();
                    Session["PayEndDay"] = dr["PayEndDay"].ToString();
                    Session["PaySubStartDay"] = dr["PaySubStartDay"].ToString();
                    Session["PaySubEndDay"] = dr["PaySubEndDay"].ToString();
                    Session["PaySubStartDate"] = dr["PaySubStartDate"].ToString();
                    Session["PaySubEndDate"] = dr["PaySubEndDate"].ToString();
                }
            }
        }


        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (Session["ROWID"] == null)
            {
            }
            else
            {
                if (intcnt == 1)
                {
                    cmbMonth.SelectedValue = Utility.ToString(Session["ROWID"]);
                    cmbYear.SelectedValue = Utility.ToString(Session["ROWYEAR"]);
                    CallBeforeMonthFill();
                }
                else
                {
                    if (IsPostBack == true)
                    {
                        MonthFill(0);
                    }
                    cmbMonth.SelectedValue = Utility.ToString(Session["ROWID"]);
                    cmbYear.SelectedValue = Utility.ToString(Session["ROWYEAR"]);
                }
                SetControlDate();
            }


        }


        protected void btnExportExcel_click(object sender, EventArgs e)
        {
            //ExportToExcel(sqlRptDs, 0, Response, "EmployeeReports");

            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.IgnorePaging = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToExcel();
        }


    }
}
