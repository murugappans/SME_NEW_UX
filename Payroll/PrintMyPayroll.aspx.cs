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
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
//using PdfSecurity;
using System.Threading;

namespace SMEPayroll.Payroll
{
    public partial class PrintMyPayroll : System.Web.UI.Page
    {
        public delegate void AsyncMethodCaller();
        int cntemail = 0;
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string strphysicalpath = "";
        int randomnumber;
        SMEPayroll.Model.ANBMailer oANBMailer;
        string compid = "";
        int intcnt;
        DataSet monthDs;
        DataRow[] foundRows;
        DataTable dtFilterFound;
        string sql = null;


        StringBuilder strFailMailMsg = new StringBuilder();
        StringBuilder strPassMailMsg = new StringBuilder();
        private ReportDocument crReportDocument;
        private Database crDatabase;
        private Tables crTables;
        private CrystalDecisions.CrystalReports.Engine.Table crTable;
        private TableLogOnInfo crTableLogOnInfo;
        private ConnectionInfo crConnectionInfo = new ConnectionInfo();
        string from = "";
        string SMTPserver = "";
        string SMTPUser = "";
        string SMTPPass = "";
        string emailreq = "";
        int SMTPPORT = 25;
        string sMonth = "";
        protected string sPDFReportFile = "";
        protected string sPDFReportFile1 = "";
        int empcode;
        string month = "";
        string year = "";
        static int empid;
        String errMsg = "";
        string strstmonth = "";
        string strendmonth = "";
        string _actionMessage = "";//By jammu Offfice

        protected void CallbackMethod(IAsyncResult ar)
        {
            // Retrieve the delegate.
            AsyncMethodCaller caller = (AsyncMethodCaller)ar.AsyncState;

            // Call EndInvoke to retrieve the results.
            caller.EndInvoke(ar);
            Session["result"] = ar;
        }
        protected void GeneratePDFs()
        {
            //method that generates the PDF and stores it on the server
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            strphysicalpath = Request.PhysicalApplicationPath.ToString();
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            ViewState["actionMessage"] = "";//By jammu Offfice
            compid = Session["Compid"].ToString();
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            dataexportmessage.Visible = false;
            if (IsePaySlipEnabled())
                Button3.Enabled = true;
            else
                Button3.Enabled = false;
            int comp_id = Utility.ToInteger(Session["Compid"]);
            SqlDataSource1.ConnectionString = Constants.CONNECTION_STRING;
            if (!IsPostBack)
            {
                #region Yeardropdown
                cmbYear.DataBind();
                #endregion
                randomnumber = Utility.GetRandomNumberInRange(100000, 10000000);
                hiddenrand.Value = randomnumber.ToString();
                cmbYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
                bindMonth();
            }

            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
            }

        }


        protected void Page_PreRender(Object sender, EventArgs E)
        {
            if (RadGrid1.MasterTableView.Items.Count > 0)
            {
                tbRecord.Visible = true;
                TabId.Visible = true;
                RadGrid1.PagerStyle.Visible = true;

            }
            else
            {
                tbRecord.Visible = false;
                TabId.Visible = false;
                RadGrid1.PagerStyle.Visible = false;
            }
        }

        protected void cmbYear_selectedIndexChanged(object sender, EventArgs e)
        {
            Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();
            bindMonth();
            Session["ROWID"] = cmbMonth.SelectedValue.ToString();
        }
        private void bindMonth()
        {
            MonthFill();
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
        void MonthFill()
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

            cmbMonth.DataSource = dtFilterFound;
            cmbMonth.DataTextField = "MonthName";
            cmbMonth.DataValueField = "RowID";
            cmbMonth.DataBind();
            //---by murugan
            cmbMonth2.DataSource = dtFilterFound;
            cmbMonth2.DataTextField = "MonthName";
            cmbMonth2.DataValueField = "RowID";
            cmbMonth2.DataBind();
            SetControlDate();
        }
        private DataSet GenpayrollDetails
        {
            get
            {
                string sSQL = "sp_ApprovePayRoll";
                SqlParameter[] parms = new SqlParameter[5];
                parms[0] = new SqlParameter("@company_id", Utility.ToInteger(Session["Compid"]));
                parms[1] = new SqlParameter("@month", Utility.ToInteger(cmbMonth.SelectedValue));
                parms[2] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
                parms[3] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"].ToString()));
                parms[4] = new SqlParameter("@Status", "G");
                DataSet ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
                return ds;
            }
        }


        protected void bindgrid(object sender, EventArgs e)
        {
            //lblMessage.Text = "";
            intcnt = 1;
            cmbYear.Enabled = false;
            cmbMonth.Enabled = false;
            cmbMonth2.Enabled = false;
            imgbtnfetch.Enabled = false;
            Session["ROWID"] = cmbMonth.SelectedValue.ToString();
            Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();
            //this.RadGrid1.DataSource = this.GenpayrollDetails;

            //----------murugan
            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            DataTable dt = new DataTable();

            int month1 = Utility.ToInteger(cmbMonth.SelectedValue);
            int month2 = Utility.ToInteger(cmbMonth2.SelectedValue);

            if (month1 > month2)
            {
               // lblMessage.Text = "Wrong Month Period ...";
                _actionMessage = "Warning|Invalid Month Period..";
                ViewState["actionMessage"] = _actionMessage;
                return;
            }

            for (int k = Utility.ToInteger(cmbMonth.SelectedValue); k <= Utility.ToInteger(cmbMonth2.SelectedValue); k++)
            {


                //if (ds2.Tables[0].Rows.Count > 0)
                //{
                SqlParameter[] parms = new SqlParameter[5];
                parms[0] = new SqlParameter("@company_id", compid);
                parms[1] = new SqlParameter("@month", k.ToString());
                parms[2] = new SqlParameter("@year", cmbYear.SelectedValue.ToString());
                parms[3] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"].ToString()));
                parms[4] = new SqlParameter("@Status", 'G');
                ds2 = DataAccess.ExecuteSPDataSet("Sp_approvemypayroll", parms);
                if (ds.Tables.Count > 0)
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr1 = ds.Tables[0].NewRow();

                        for (int i = 0; i < 18; i++)
                        {
                            dr1[i] = ds2.Tables[0].Rows[0][i];
                        }


                        ds.Tables[0].Rows.Add(dr1);
                    }

                }
                else
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        dt = ds2.Tables[0].Copy();
                        ds.Tables.Add(dt);
                    }

                }

                ds2.Tables.Clear();
                //}

            }
            if (ds.Tables.Count > 0)
            {
                RadGrid1.DataSource = ds;
                RadGrid1.DataBind();
            }


            
        }


        protected void PrintPayroll_Click(object sender, EventArgs e)
        {
            Session["SelFormula"] = null;
            string payslipFormat = GetPayslipFormat();
            string stremp = "";
            Session["SelFormula"] = "{sp_new_payslip_all2.EMP_CODE}=0";
            if (payslipFormat == "6")
            {
                Session["SelFormula"] = " {sp_new_payslip_Timesheet;1.EMP_CODE}=0";
            }
            //Session["SelFormula_sub"] = "{Sp_getemployeeleavepolicy_Report;1.emp_id}=0";
            bool flag = false;
            string empId = "";
            string month_list = "";

            foreach (Telerik.Web.UI.GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

                    if (chkBox.Checked == true)
                    {
                        stremp = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_id"));
                        //Session["SelFormula"] = Session["SelFormula"] + " OR {sp_new_payslip_all2.EMP_CODE}=" + stremp;
                        //Session["SelFormula_sub"] = Session["SelFormula_sub"] + " OR {Sp_getemployeeleavepolicy_Report;1.emp_id}=" + stremp;
                        if (empId == "" || empId == "0")
                        {
                            empId = stremp;
                        }
                        else
                        {
                            // empId = empId + "," + stremp;
                            empId = stremp;
                        }
                        if (payslipFormat == "6")
                        {
                            Session["SelFormula"] = Session["SelFormula"] + " OR {sp_new_payslip_Timesheet;1.EMP_CODE}=" + stremp;
                            //{sp_new_payslip_Timesheet;1.EMP_CODE}=0
                        }
                        else
                        {
                            Session["SelFormula"] = Session["SelFormula"] + " OR {sp_new_payslip_all2.EMP_CODE}=" + stremp;
                        }
                        flag = true;
                    }
                }
            }
            //-------month List to session by  murugan
            foreach (Telerik.Web.UI.GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

                    if (chkBox.Checked == true)
                    {
                        string strmonth = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("month"));
                        //Session["SelFormula"] = Session["SelFormula"] + " OR {sp_new_payslip_all2.EMP_CODE}=" + stremp;
                        //Session["SelFormula_sub"] = Session["SelFormula_sub"] + " OR {Sp_getemployeeleavepolicy_Report;1.emp_id}=" + stremp;
                        if (month_list == "")
                        {
                            month_list = strmonth;
                        }
                        else
                        {
                            month_list = month_list + "," + strmonth;
                        }


                    }
                }
            }

            if (flag == false)
            {
                //lblMessage.Text = "Please Select At least one Month";

            }
            else
            {
                Session["Year_Leave"] = cmbYear.SelectedValue;
                Session["Emp_Leave"] = empid;
                Session["Company_id"] = compid;
                Session["month_list"] = month_list;

                // Not passing in query string, passing as session
                Session["Emp_List"] = null;
                Session["Emp_List"] = empId;
                //Session["month_list"] = month_list;
                empId = "0";
                //

                if (payslipFormat == "1")
                    Response.Redirect("../Reports/PrintReport_mypayslip.aspx?QS=payslip_all1~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|0");
                if (payslipFormat == "2")
                    Response.Redirect("../Reports/PrintReport_mypayslip.aspx?QS=payslip_all2~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|0");
                if (payslipFormat == "3")
                    //KUMAR 15/7/2015 CHANGE TO ALL2 TO ALL3
                    //  Response.Redirect("../Reports/PrintReport.aspx?QS=payslip_all2~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|0");
                    Response.Redirect("../Reports/PrintReport_mypayslip.aspx?QS=payslip_all3~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|0");
                if (payslipFormat == "4")
                    Response.Redirect("../Reports/PrintReport_mypayslip.aspx?QS=PAYSLIP_ALL4~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|0");
                if (payslipFormat == "5")
                    Response.Redirect("../Reports/PrintReport_mypayslip.aspx?QS=PAYSLIP_ALL5~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|0");
                if (payslipFormat == "6")
                    Response.Redirect("../Reports/PrintReport_mypayslip.aspx?QS=PAYSLIP_ALL_TimeSheet_New~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|0");
                if (payslipFormat == "7")
                {
                    Response.Redirect("../Reports/PrintReport_mypayslip.aspx?QS=PAYSLIP_ALL1_L~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|" + empId + "~ReportType|4");
                    //Response.Redirect("../Reports/PrintReport.aspx?QS=PAYSLIP_ALL1_L~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|0~ReportType|4");
                }
                if (payslipFormat == "10") 
                {
                    Response.Redirect("../Reports/PrintReport_mypayslip.aspx?QS=PAYSLIP_ALL10~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|" + empId + "~ReportType|4");
                    //Response.Redirect("../Reports/PrintReport.aspx?QS=PAYSLIP_ALL1_L~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|0~ReportType|4");
                }
                if (payslipFormat == "11")
                {
                    Response.Redirect("../Reports/MOM-Payslip-Form1.aspx?QS=" + cmbMonth.SelectedValue + "~" + cmbYear.SelectedValue + "~" + compid + "~0");
                    //Response.Redirect("../Reports/PrintReport.aspx?QS=PAYSLIP_ALL1_L~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|0~ReportType|4");
                }
                if (payslipFormat == "12")
                {
                    Response.Redirect("../Reports/MOM-Payslip-Form-Header.aspx?QS=" + cmbMonth.SelectedValue + "~" + cmbYear.SelectedValue + "~" + compid + "~0");
                    //Response.Redirect("../Reports/PrintReport.aspx?QS=PAYSLIP_ALL1_L~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|0~ReportType|4");
                }

                if (payslipFormat == "13")
                {
                    Response.Redirect("../Reports/PrintReport_mypayslip.aspx?QS=PAYSLIP_ALL4MOM~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|" + empId + "~ReportType|4");
                    //Response.Redirect("../Reports/PrintReport.aspx?QS=PAYSLIP_ALL1_L~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|0~ReportType|4");

                }
                if (payslipFormat == "14")
                {
                    Response.Redirect("../Reports/PrintReport_mypayslip.aspx?QS=PAYSLIP_ALL5MOM~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|" + empId + "~ReportType|4");
                    //Response.Redirect("../Reports/PrintReport.aspx?QS=PAYSLIP_ALL1_L~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|0~ReportType|4");
                }
            }


        }

        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {
                //GridEditableItem editedItem = e.Item as GridEditableItem;
                //object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["emp_id"];
                //int empid = Utility.ToInteger(id);
                //CallMailMethod(1, empid, 0);

                GridEditableItem editedItem = e.Item as GridEditableItem;
                object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["emp_id"];

                int empid = Utility.ToInteger(id);
                int month = Utility.ToInteger(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["month"]);

                string payslipFormat = GetPayslipFormat();

                //if (payslipFormat == "1")
                //    Response.Redirect("../Reports/PrintReport.aspx?QS=payslip1~month|" + cmbMonth.SelectedValue + "~Year|" + cmbYear.SelectedValue + "~empcode|" + empid);
                //if (payslipFormat == "2")
                //    Response.Redirect("../Reports/PrintReport.aspx?QS=payslip2~month|" + cmbMonth.SelectedValue + "~Year|" + cmbYear.SelectedValue + "~empcode|" + empid);
                //if (payslipFormat == "3")
                //    Response.Redirect("../Reports/PrintReport.aspx?QS=payslip3~month|" + cmbMonth.SelectedValue + "~Year|" + cmbYear.SelectedValue + "~empcode|" + empid);
                //if (payslipFormat == "4")
                //    Response.Redirect("../Reports/PrintReport.aspx?QS=PAYSLIP4~month|" + cmbMonth.SelectedValue + "~Year|" + cmbYear.SelectedValue + "~empcode|" + empid);
                //if (payslipFormat == "5")
                //    Response.Redirect("../Reports/PrintReport.aspx?QS=PAYSLIP5~month|" + cmbMonth.SelectedValue + "~Year|" + cmbYear.SelectedValue + "~empcode|" + empid);

                //Passing parameter value for Leave
                Session["Year_Leave"] = cmbYear.SelectedValue;
                Session["Emp_Leave"] = empid;
               
                Session["Emp_List"] = empid;
                Session["Company_id"] = compid;
                Session["month_list"] = null;
                Session["month_list"] = month;
                if (payslipFormat == "1")
                    Response.Redirect("../Reports/PrintReport.aspx?QS=payslip_all1~month|" + month + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|" + empid);
                if (payslipFormat == "2")
                    Response.Redirect("../Reports/PrintReport.aspx?QS=payslip_all2~month|" + month + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|" + empid);
                if (payslipFormat == "3")
                    Response.Redirect("../Reports/PrintReport.aspx?QS=payslip_all2~month|" + month + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|" + empid);
                // Response.Redirect("../Reports/PrintReport.aspx?QS=payslip_all3~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|" + empid);
                if (payslipFormat == "4")
                    Response.Redirect("../Reports/PrintReport.aspx?QS=PAYSLIP_ALL4~month|" + month + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|" + empid);
                if (payslipFormat == "5")
                    Response.Redirect("../Reports/PrintReport.aspx?QS=PAYSLIP_ALL5~month|" + month + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|" + empid);


                if (payslipFormat == "6")
                    Response.Redirect("../Reports/PrintReport.aspx?QS=PAYSLIP_ALL_TimeSheet_New~month|" + month + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|" + empid);
                if (payslipFormat == "7")
                    Response.Redirect("../Reports/PrintReport.aspx?QS=PAYSLIP_ALL1_L~month|" + month + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|" + empid + "~ReportType|4");
                if (payslipFormat == "10")
                    Response.Redirect("../Reports/PrintReport.aspx?QS=PAYSLIP_ALL10~month|" + month + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|" + empid + "~ReportType|4");
                if (payslipFormat == "11")
                    //Response.Redirect("../Reports/MOM-Payslip-Form1.aspx?QS=" + cmbMonth.SelectedValue + "~" + cmbYear.SelectedValue + "~" + compid + "~" + empid + "");
                    Response.Redirect("../Reports/MOM-Payslip-Form1.aspx?QS=" + month + "~" + cmbYear.SelectedValue + "~" + compid + "~" + empid + "");
                if (payslipFormat == "12")
                    Response.Redirect("../Reports/MOM-Payslip-Form-Header.aspx?QS=" + month + "~" + cmbYear.SelectedValue + "~" + compid + "~" + empid + "");

            }
        }


        protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            RadGrid1.CurrentPageIndex = e.NewPageIndex;
            //RadGrid1.DataSource = this.GenpayrollDetails;
            RadGrid1.DataBind();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (Session["ClickCount"] == null)
            {
                Session["ClickCount"] = "0";
            }
            if (Session["ClickCount"].ToString() == "0")
            {
                Session["ClickCount"] = Convert.ToInt16(Session["ClickCount"]) + 1;
                //CallMailMethod(1, 0);
                AsyncMethodCaller caller = new AsyncMethodCaller(CallMailMethodThread);
                IAsyncResult result = caller.BeginInvoke(new AsyncCallback(CallbackMethod), caller);
                Session["result"] = result;
            }
        }

        void CallMailMethodThread()
        {
            CallMailMethod(1, 0, 1);
        }

        private string GetPayslipFormat()
        {
            string retVal = "1";
            string sSQL = "select isnull(payslip_format,'1') from company where company_id = {0}";
            sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
            while (dr.Read())
            {
                retVal = Utility.ToString(dr.GetValue(0));
            }
            return retVal;
        }

        private bool IsePaySlipEnabled()
        {
            string bTemp = "";
            string sSQL = "SELECT epayslip FROM company WHERE company_id=" + compid;
            System.Data.SqlClient.SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL);
            while (dr.Read())
            {
                bTemp = Utility.ToString(dr.GetValue(0));
            }

            if (bTemp.ToUpper() == "Y")
                return true;
            else
                return false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (RadGrid1.Items.Count > 0)
            {
                RadGrid1.ExportSettings.ExportOnlyData = true;
                //RadGrid1.ExportSettings.IgnorePaging = CheckBox1.Checked;
                RadGrid1.ExportSettings.OpenInNewWindow = true;
                RadGrid1.MasterTableView.ExportToExcel();
            }
            else
            {
                dataexportmessage.Visible = true;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (RadGrid1.Items.Count > 0)
            {
                RadGrid1.ExportSettings.ExportOnlyData = true;
                //RadGrid1.ExportSettings.IgnorePaging = CheckBox1.Checked;
                RadGrid1.ExportSettings.OpenInNewWindow = true;
                RadGrid1.MasterTableView.ExportToWord();
            }
            else
            {
                dataexportmessage.Visible = true;
            }
        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            //RadGrid1.DataSource = this.GenpayrollDetails;
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
                        MonthFill();
                    }
                    cmbMonth.SelectedValue = Utility.ToString(Session["ROWID"]);
                    cmbYear.SelectedValue = Utility.ToString(Session["ROWYEAR"]);
                }
                SetControlDate();
            }


        }


        void SetControlDate()
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

        private string GetPayMonth(string mth)
        {
            string retVal = "1";
            string sSQL = "select [MonthName],Convert(Varchar,PaySubStartDate,103), Convert(Varchar,PaySubEndDate,103)  from PayrollMonthlyDetail where ROWID = {0}";
            sSQL = string.Format(sSQL, Utility.ToInteger(mth));
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
            while (dr.Read())
            {
                retVal = Utility.ToString(dr.GetValue(0));
                strstmonth = Utility.ToString(dr.GetValue(1));
                strendmonth = Utility.ToString(dr.GetValue(2));
            }
            return retVal;
        }


        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }

        protected void sendemail(int empid, string name, string toemail, string strempname)
        {
            //SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(Convert.ToInt16(compid));
            string subject = "EPayslip for the period " + sMonth + "/" + year;
            string Body = "Your payroll has been processed for the month of  " + sMonth + "/" + year + " . Please find attached epayslip.";
            oANBMailer.Subject = "EPayslip for the period " + sMonth + "/" + year;
            oANBMailer.MailBody = "Your payroll has been processed for the month of  " + sMonth + "/" + year + " . Please find attached epayslip.";
            oANBMailer.From = from;
            oANBMailer.To = toemail;

            try
            {
                //((Chilkat.Email)oANBMailer).RemoveAttachmentPaths();
                oANBMailer.Attachment = name;
                string sRetVal = oANBMailer.SendMail();

                if (sRetVal == "")
                {
                    if (toemail.Length > 0)
                    {
                        //strPassMailMsg.Append("<br/>" + strempname);
                        strPassMailMsg.Append("Insert Into EmailTrack Values(" + randomnumber.ToString() + "," + cmbMonth.SelectedValue + ",1,getdate()," + Session["EmpCode"] + "," + empid + ",0,'');");
                    }
                    else
                    {
                        //strFailMailMsg.Append("<br/>" + strempname);
                        strFailMailMsg.Append("Insert Into EmailTrack Values(" + randomnumber.ToString() + "," + cmbMonth.SelectedValue + ",1,getdate()," + Session["EmpCode"] + "," + empid + ",1,'No Email ID Assigned.');");
                    }
                }
                else
                {
                    //strFailMailMsg.Append("<br/>" + strempname);
                    strFailMailMsg.Append("Insert Into EmailTrack Values(" + randomnumber.ToString() + "," + cmbMonth.SelectedValue + ",1,getdate()," + Session["EmpCode"] + "," + empid + ",2,'" + sRetVal.ToString() + "');");
                }
            }
            catch (Exception ex)
            {
                //strFailMailMsg.Append("<br/>" + strempname);
                strFailMailMsg.AppendLine("Insert Into EmailTrack Values(" + randomnumber.ToString() + "," + cmbMonth.SelectedValue + ",1,getdate()," + Session["EmpCode"] + "," + empid + ",3,'" + ex.Message + "');");
            }
            finally
            {
                oANBMailer.CleanMail();
                oANBMailer.ToName = "";
                oANBMailer.CcName = "";
                oANBMailer.FromName = "";
                //oANBMailer = null;
            }
            //}
        }

        protected void sendemailheader(int randomnumberint, int emailcount, string toemail)
        {
            //SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(Convert.ToInt16(compid));
            string subject = "EPayslip for the period " + sMonth + "/" + year;
            string Body = emailcount.ToString() + " e-Payslip sent for the month of  " + sMonth + "/" + year + ", Doc No:" + randomnumberint.ToString();
            oANBMailer.Subject = Body;
            oANBMailer.MailBody = Body;
            oANBMailer.From = from;
            oANBMailer.To = toemail;

            try
            {
                //((Chilkat.Email)oANBMailer).RemoveAttachmentPaths();
                string sRetVal = oANBMailer.SendMail();

                if (sRetVal == "")
                {
                    if (toemail.Length > 0)
                    {
                        //strPassMailMsg.Append("<br/>" + strempname);
                    }
                    else
                    {
                        //strFailMailMsg.Append("<br/>" + strempname);
                    }
                }
                else
                {
                    //strFailMailMsg.Append("<br/>" + strempname);
                }
            }
            catch (Exception ex)
            {
                //strFailMailMsg.Append("<br/>" + strempname);
                //strFailMailMsg.AppendLine("Insert Into EmailTrack Values(" + randomnumber.ToString() + "," + cmbMonth.SelectedValue + ",'Email Payslip',getdate()," + Session["EmpCode"] + "," + empid + ",3,'" + ex.Message + "');");
            }
            finally
            {
                oANBMailer.CleanMail();
                //oANBMailer = null;
            }
            //}
        }

        public static void ShowMessageBox(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (message.EndsWith("."))
                    message = message.Substring(0, message.Length - 1);
            }
            StringBuilder sbScript = new StringBuilder();
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");
            HttpContext.Current.Response.Write(sbScript);
        }



        protected void CallMailMethod(int iType, int empcode, int issend)
        {
            try
            {
                //randomnumber = Utility.GetRandomNumberInRange(100000, 10000000);
                randomnumber = Convert.ToInt32(hiddenrand.Value);
                Button3.Text = "Email Payslip, Doc No: " + randomnumber.ToString();
                //this.Button3.Attributes.Add("disabled", "true");
                string sQS = "";
                string payslipFormat = GetPayslipFormat();
                if (iType == 1)
                {
                    if (payslipFormat == "1")
                    {
                        sQS = "payslip1~month|" + cmbMonth.SelectedValue + "~Year|" + cmbYear.SelectedValue + "~empcode|" + empcode;
                    }
                    if (payslipFormat == "2")
                    {
                        sQS = "payslip2~month|" + cmbMonth.SelectedValue + "~Year|" + cmbYear.SelectedValue + "~empcode|" + empcode;
                    }
                    if (payslipFormat == "3")
                    {
                        sQS = "payslip3~month|" + cmbMonth.SelectedValue + "~Year|" + cmbYear.SelectedValue + "~empcode|" + empcode;
                    }
                    if (payslipFormat == "4")
                    {
                        sQS = "payslip4~month|" + cmbMonth.SelectedValue + "~Year|" + cmbYear.SelectedValue + "~empcode|" + empcode;
                    }
                    if (payslipFormat == "5")
                    {
                        sQS = "payslip5~month|" + cmbMonth.SelectedValue + "~Year|" + cmbYear.SelectedValue + "~empcode|" + empcode;
                    }


                    if (payslipFormat == "6")
                    {
                        //sQS = "payslip_all5~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|0";
                        //sQS = "PAYSLIP_ALL_TimeSheet_New~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|0";
                        sQS = "PAYSLIP_ALL_TimeSheet_New~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~empcode|" + empcode;
                    }

                    if (payslipFormat == "7")
                    {
                        //sQS = "PAYSLIP_ALL1_L~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|0~ReportType|4";
                        sQS = "PAYSLIP_ALL1_L~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~empcode|" + empcode;
                    }

                    if (payslipFormat == "10")
                    {
                        //sQS = "PAYSLIP_ALL1_L~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~compid|" + compid + "~empcode|0~ReportType|4";
                        sQS = "PAYSLIP_ALL10~month|" + cmbMonth.SelectedValue + "~year|" + cmbYear.SelectedValue + "~empcode|" + empcode;
                    }


                }
                string strempemail = "";
                string strempname = "";

                compid = Utility.ToString(Session["Compid"]);
                empid = Utility.ToInteger(Session["EmpCode"]);

                //string sQS = Utility.ToString(Request.QueryString["QS"]);
                string[] sParams = sQS.Split('~');

                string sReportFile = strphysicalpath + @"Reports\" + sParams[0] + ".rpt";


                string[] sTemp3 = sParams[1].Split('|');
                string ParamName3 = "@" + sTemp3[0];
                string ParamVal3 = sTemp3[1];
                month = ParamVal3;

                string[] sTemp1 = sParams[2].Split('|');
                string ParamName1 = "@" + sTemp1[0];
                string ParamVal1 = sTemp1[1];
                year = ParamVal1;

                if (month == "1")
                    sMonth = "January";
                if (month == "2")
                    sMonth = "February";
                if (month == "3")
                    sMonth = "March";
                if (month == "4")
                    sMonth = "April";
                if (month == "5")
                    sMonth = "May";
                if (month == "6")
                    sMonth = "June";
                if (month == "7")
                    sMonth = "July";
                if (month == "8")
                    sMonth = "August";
                if (month == "9")
                    sMonth = "September";
                if (month == "10")
                    sMonth = "October";
                if (month == "11")
                    sMonth = "November";
                if (month == "12")
                    sMonth = "December";


                sMonth = GetPayMonth(month);


                DataSet ds = new DataSet();
                //string sSQL = "Select pd.emp_id, Replace(isnull(em.emp_name,'')+' '+isnull(em.emp_lname,''),'/','') emp_name, em.password,em.email,em.email_payslip From prepare_payroll_hdr ph Inner Join prepare_payroll_detail pd on ph.trx_id = pd.trx_id Inner Join Employee em on pd.emp_id = em.emp_code where pd.status ='G' And  (Convert(DateTime,ph.start_period,103) >= Convert(DateTime,'" + strstmonth + "',103)  And Convert(DateTime,ph.end_period,103) <= Convert(DateTime,'" + strendmonth + "',103)) And em.company_id='" + Session["Compid"].ToString() + "' Order By Replace(isnull(em.emp_name,'')+' '+isnull(em.emp_lname,''),'/','')";
                string sSQL = "SELECT c.email_SMTP_server, c.email_username, c.email_password, c.email_sender_domain, c.email_sender_name, c.email_reply_address, c.email_reply_name, c.email_smtp_port, c.email,   c.email_sender,c.pwdrequired FROM   company c  WHERE  c.company_id ='" + Session["Compid"].ToString() + "'";
                ds = GetDataSet(sSQL);
                string strpwdreq = "";
                strpwdreq = Utility.ToString(ds.Tables[0].Rows[0]["pwdrequired"].ToString().Trim());

                crReportDocument = new ReportDocument();
                crReportDocument.Load(sReportFile);

                crConnectionInfo.ServerName = Constants.DB_SERVER;
                crConnectionInfo.DatabaseName = Constants.DB_NAME;
                crConnectionInfo.UserID = Constants.DB_UID;
                crConnectionInfo.Password = Constants.DB_PWD;

                crDatabase = crReportDocument.Database;
                crTables = crDatabase.Tables;

                for (int i = 0; i < crTables.Count; i++)
                {
                    crTable = crTables[i];
                    crTableLogOnInfo = crTable.LogOnInfo;
                    crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                    crTable.ApplyLogOnInfo(crTableLogOnInfo);
                    crTable.Location = crTable.Name;
                }
                CrystalDecisions.Shared.DiskFileDestinationOptions dfdoReport = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                crReportDocument.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                crReportDocument.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                crReportDocument.ExportOptions.DestinationOptions = dfdoReport;

                oANBMailer = new SMEPayroll.Model.ANBMailer(Convert.ToInt16(compid));

                from = Utility.ToString(ds.Tables[0].Rows[0]["email_sender"].ToString().Trim());
                SMTPserver = Utility.ToString(ds.Tables[0].Rows[0]["email_SMTP_server"].ToString().Trim());
                SMTPUser = Utility.ToString(ds.Tables[0].Rows[0]["email_username"].ToString().Trim());
                SMTPPass = Utility.ToString(ds.Tables[0].Rows[0]["email_password"].ToString().Trim());
                SMTPPORT = Utility.ToInteger(ds.Tables[0].Rows[0]["email_smtp_port"].ToString().Trim());

                //for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                //{
                string strpdffilein = "";
                string strpdffileou = "";
                string strpwd = "";

                foreach (Telerik.Web.UI.GridItem item in RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            long ret;
                            strempemail = "";
                            //emailreq = Utility.ToString(ds.Tables[0].Rows[j]["email_payslip"].ToString().Trim());
                            //empcode = Utility.ToInteger(ds.Tables[0].Rows[j]["emp_id"]);
                            //strempname = Utility.ToString(ds.Tables[0].Rows[j]["emp_name"]);
                            //strempemail = Utility.ToString(ds.Tables[0].Rows[j]["email"].ToString().Trim());

                            empcode = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_id"));
                            strempname = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name"));
                            emailreq = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("email_payslip"));
                            strempemail = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("email"));

                            sPDFReportFile = empcode + "-" + strempname + "-" + sMonth + year + ".pdf";
                            sParams[3] = "empcode|" + Utility.ToString(empcode);
                            CrystalDecisions.Shared.ParameterValues pv = null;
                            CrystalDecisions.Shared.ParameterDiscreteValue pdv = null;

                            strpdffilein = strphysicalpath + @"Documents\TempReports\" + sPDFReportFile;
                            strpdffileou = strphysicalpath + @"Documents\TempReports\Pwd_" + sPDFReportFile;

                            // assigning parameters.
                            for (int i = 1; i < sParams.Length; i++)
                            {
                                string[] sTemp = sParams[i].Split('|');
                                string ParamName = "@" + sTemp[0];
                                string ParamVal = sTemp[1];

                                pv = new CrystalDecisions.Shared.ParameterValues();
                                pdv = new CrystalDecisions.Shared.ParameterDiscreteValue();
                                pdv.Value = ParamVal;
                                pv.Add(pdv);
                                crReportDocument.DataDefinition.ParameterFields[ParamName].ApplyCurrentValues(pv);
                            }
                            dfdoReport.DiskFileName = strpdffilein;
                            crReportDocument.Export();

                            if (strpwdreq == "Yes")
                            {
                                // Export 2 PDF.
                                //PdfSecurity.CPdfCryptClass pdf = new CPdfCryptClass();
                                //pdf.SetCode("98988D5CC53C747D121970");
                                //string pwdenc = encrypt.SyDecrypt(Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("password")));
                                //ret = pdf.EncryptPdf(strpdffilein, ENCRYPTTYPE.EC_128, (int)(PdfSecurity.PdfSecurity.PS_ALLOWCOPY | PdfSecurity.PdfSecurity.PS_ALLOWPRINTING), "owner", pwdenc, strpdffileou);
                                //strpdffilein = strpdffileou;
                            }
                            if (issend == 1)
                            {
                                if (emailreq == "Y")
                                {
                                    if (strempemail != "")
                                    {
                                        sendemail(empcode, strpdffilein, strempemail, strempname);
                                        cntemail++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strFailMailMsg.AppendLine("Insert Into EmailTrack Values(" + randomnumber.ToString() + "," + cmbMonth.SelectedValue + ",1,getdate()," + Session["EmpCode"] + "," + empcode + ",4,'" + ex.Message + "');");
                //string errMsg = ex.Message;
                //Response.Write("<script language='javascript'> alert(" + errMsg + ");</script>");
            }
            finally
            {
                Session["ClickCount"] = "0";
                crReportDocument.Close();
                crReportDocument.Dispose();
                crReportDocument = null;
                //this.Button3.Attributes.Add("disabled", "false");
            }
            if (issend == 1)
            {
                if (strPassMailMsg.Length > 0)
                {
                    //ShowMessageBox("Email Send successfully to: <br/>" + strPassMailMsg.ToString());
                    int retVal = DataAccess.ExecuteStoreProc(strPassMailMsg.ToString());
                }
                if (strFailMailMsg.Length > 0)
                {
                    //ShowMessageBox("Error While sending Email to: <br/>" + strFailMailMsg.ToString());
                    int retVal = DataAccess.ExecuteStoreProc(strFailMailMsg.ToString());
                }

                if (emailreq == "Y")
                {
                    if (Session["EmpEmail"].ToString().Length > 0)
                    {
                        sendemailheader(randomnumber, cntemail, Session["EmpEmail"].ToString());
                    }
                }
            }

        }



        //Toolbar
        #region Toolbar and Exporting

        //column to hide wile export
        protected void HideGridColumnseExport()
        {
            RadGrid1.MasterTableView.GetColumn("TemplateColumn").Visible = false;
            RadGrid1.MasterTableView.GetColumn("GridClientSelectColumn").Visible = false;
            //RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;// UniqueName="DeleteColumn"
        }

        protected void tbRecord_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            if (e.Item.Text == "Excel" || e.Item.Text == "Word")
            {
                HideGridColumnseExport();
            }

            GridSettingsPersister obj2 = new GridSettingsPersister();
            obj2.ToolbarButtonClick(e, RadGrid1, Utility.ToString(Session["Username"]));

        }

        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }

        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("110", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);

        }

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            GridSettingsPersister objCount = new GridSettingsPersister();
            objCount.RowCount(e, tbRecord);
        }


        #endregion
        //Toolbar End

        protected void Radgrid1_databound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {

                GridDataItem item = (GridDataItem)e.Item;
                string strEmpCode = item["month"].Text.ToString();
                // ((TextBox)item.Cells[11].Controls[1]).Enabled = false;
                // ((TextBox)item.Cells[12].Controls[1]).Enabled = false;




            }

        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                string strmonth = Convert.ToString(e.Item.Cells[5].Text).ToString();
                //GridEditableItem editedItem = e.Item as GridEditableItem;
                // object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["month"];

                // if (e.Item is GridDataItem)
                // {
                //  GridDataItem gridRow = (GridDataItem)e.Item;
                //  string strmonth = gridRow["month"].Text;


                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "select MonthName from [PayrollMonthlyDetail] where rowid=" + strmonth, null);
                if (dr.Read())
                {
                    e.Item.Cells[5].Text = dr[0].ToString();
                    // gridRow["month"].Text = dr[0].ToString();

                }
                
            }
        }



    }
}
