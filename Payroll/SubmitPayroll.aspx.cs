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
using System.Net.Mail;
using System.Drawing;
using System.IO;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;

namespace SMEPayroll.Payroll
{
    public partial class SubmitPayroll : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        //protected string strEmpBlockID = "0";
        static string empname = "";
        static int EmpCode;
        string sql = null;
        int intcnt;
        DataSet monthDs;
        DataRow[] foundRows;
        DataTable dtFilterFound;
        string sSQL = null;
        int comp_id;
        string strWF = "";
        string strEmpvisible = "";
        int month_exact;
        decimal dblWorkDaysInRoll = 0.0m;
        decimal dblActWorkDaysInRoll = 0.0m;
        string _actionMessage = "";//By jammu Offfice
        protected void Page_Load(object sender, EventArgs e)
        {
            /* To disable Grid filtering options  */
            ViewState["AuditActionModuleRef"] = "";
           string payroll = Request.QueryString["payroll"];
            if(payroll == "continue")
            {
                _actionMessage = "Success|Submitted Successfully";
                ViewState["actionMessage"] = _actionMessage;
            }
            else
            {
                ViewState["actionMessage"] = "";//By jammu Offfice
            }


            Telerik.Web.UI.GridFilterMenu menu = RadGrid1.FilterMenu;
            int i = 0;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(SubmitPayroll));

            dataexportmessage.Visible = false;
            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }

            //btnsubapprove.Attributes.Add("onclick", "this.disabled=true;" );
            //Page.ClientScript.GetPostBackEventReference(btnsubapprove, "");


            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
        
            SqlDataSource1.ConnectionString = Constants.CONNECTION_STRING;
            SqlDataSource3.ConnectionString = Constants.CONNECTION_STRING;
            xmldtYear1.ConnectionString = Session["ConString"].ToString();

            if (Session["strWF"] == null)
            {
                string sqlWF = "Select WorkFlowID,WFPAY,WFLEAVE,WFEMP,WFCLAIM,WFReport,WFTimeSheet from company WHERE Company_Id=" + comp_id;
                DataSet dsWF = new DataSet();
                dsWF = DataAccess.FetchRS(CommandType.Text, sqlWF, null);

                if (dsWF.Tables.Count > 0)
                {
                    if (dsWF.Tables[0].Rows.Count > 0)
                    {
                        strWF = dsWF.Tables[0].Rows[0][0].ToString();
                        Session["strWF"] = strWF;
                    }
                }
            }
            else
            {
                strWF = (string)Session["strWF"];
            }


            empname = Session["Emp_Name"].ToString();
            EmpCode = Utility.ToInteger(Session["EmpCode"]);

            comp_id = Utility.ToInteger(Session["Compid"]);
            if (!IsPostBack)
            {

                #region Yeardropdown
                cmbYear.DataBind();
                #endregion
                cmbYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
                bindMonth();
                string s = Session["GroupName"].ToString().ToUpper();

                if (s != "SUPER ADMIN")
                {
                    if (Session["PayrollWF"] != null)
                    {
                        if (strWF == "2" && Session["PayrollWF"].ToString() != "")
                        {
                            SessionParameter empid = new SessionParameter();
                            empid.Name = "EmpPassID";
                            empid.Type = TypeCode.String;
                            empid.SessionField = "EmpPassID";

                            if (SqlDataSource1.SelectParameters.Contains(empid) == false)
                            {
                                SqlDataSource1.SelectParameters.Add(empid);
                            }
                        }
                    }
                }

            }
            SqlDataSource1.Selecting += new SqlDataSourceSelectingEventHandler(SqlDataSource1_Selecting);
            // btnsubapprove.Text = "Submit For " + cmbMonth.SelectedItem.Text;

            if (Session["processPayroll"] != null)
            {
                if ((string)Session["processPayroll"].ToString() == "0")
                {
                    //btnsubapprove.Text = "{Submit/Approve/Generate Payroll For }" + cmbMonth.SelectedItem.Text;
                    btnsubapprove.Text = "{Submit/Approve/Generate Payroll }";//+ cmbMonth.SelectedItem.Text;

                }

            }

            if (!IsPostBack)
            {
                RadGrid1.ExportSettings.FileName = "Employee_SubmitPayroll_List";
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
                rdPayDatePicker.SelectedDate = DateTime.Today;
            }
            RadGrid1.PreRender += new EventHandler(RadGrid1_PreRender);


            //Check for WorkFlow number 2
            if (strWF == "2" && Session["PayrollWF"] != null)
            {
                if (Session["PayrollWF"].ToString() == "1")
                {

                    if (Session["GroupName"].ToString().ToUpper() != "SUPER ADMIN")
                    {
                        strEmpvisible = "";
                        if (Session["dsEmpSup"] != null)
                        {
                            if (Session["dsEmpWF"] != null)
                            {
                                DataSet dstemp = new DataSet();
                                dstemp = (DataSet)Session["dsEmpWF"];
                                foreach (DataRow dr in dstemp.Tables[0].Rows)
                                {
                                    if (strEmpvisible == "")
                                    {
                                        strEmpvisible = dr["Emp_ID"].ToString();
                                    }
                                    else
                                    {
                                        strEmpvisible = strEmpvisible + "," + dr["Emp_ID"].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //strEmpvisible = "5,127";

            if (strEmpvisible != "")
            {
                Session["EmpPassID"] = strEmpvisible;
            }
            else
            {
                Session["EmpPassID"] = "";
            }


            //RadProgressArea1.Localization.Uploaded = "Total Progress";
            // RadProgressArea1.Localization.UploadedFiles = "Progress";
            // RadProgressArea1.Localization.CurrentFileName = "Progress in action: ";
            //RadProgressArea1.Localization.CurrentFileName = "";
            
        }


        protected void deptID_databound(object sender, EventArgs e)
        {
            deptID.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        protected void Page_PreRender(Object sender, EventArgs E)
        {
            if (RadGrid1.MasterTableView.Items.Count > 0)
            {
                //tbRecord.Visible = true;
               // TabId.Visible = true;

                btndetail.Visible = true;
                btnPayrollDetail.Visible = true;
                btnPayroll.Visible = true;
                btnReportAll.Visible = true;
                chkDateSelection.Visible = true;
                btnsubapprove.Visible = true;//By jammu ofice
                lblpaymentdate.Visible = true;//By jammu ofice
                rdPayDatePicker.Visible = true;
                RadGrid1.PagerStyle.Visible = true;
                paylbl.Visible = true;
            }
            else
            {
                //tbRecord.Visible = false;
               // TabId.Visible = false;

                btnReportAll.Visible = false;
                btndetail.Visible = false;
                btnPayrollDetail.Visible = false;
                btnPayroll.Visible = false;
                chkDateSelection.Visible = false;
                btnsubapprove.Visible = false;//By jammu ofice
                lblpaymentdate.Visible = false;//By jammu ofice
                rdPayDatePicker.Visible = false;
                RadGrid1.PagerStyle.Visible = false;
                paylbl.Visible = false;
            }
        }

        void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            e.Command.CommandTimeout = 1000000;
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
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1;
            SetControlDate();
        }

        public static void ShowMessageBox(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (message.EndsWith("."))
                    message = message.Substring(0, message.Length - 1);
            }
            StringBuilder sbScript = new StringBuilder(50);
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");
            HttpContext.Current.Response.Write(sbScript);
        }

        //StringBuilder sb_name = new StringBuilder();
        List<NameList> nlist = new List<NameList>();

        private static readonly Object obj = new Object();
        protected void btnsubapprove_click(object sender, EventArgs e)
        {
            
         
           sendemail();
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            lock (obj)
            {
                Stopwatch sw = Stopwatch.StartNew();
                sw.Start();
                // btnsubapprove.Enabled = false;

                bool blnisrecsel = false;
                if (Session["SubmitClickCount"] == null)
                {
                    Session["SubmitClickCount"] = "0";
                }
                if (chkDateSelection.Checked == true)
                {

                    if (Session["SubmitClickCount"].ToString() == "0")
                    {
                        Session["SubmitClickCount"] = Convert.ToInt16(Session["SubmitClickCount"]) + 1;
                        foreach (Telerik.Web.UI.GridItem item in RadGrid1.MasterTableView.Items)
                        {
                            if (item is GridItem)
                            {
                                GridDataItem dataItem = (GridDataItem)item;
                                CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                                if (chkBox.Checked == true)
                                {
                                    blnisrecsel = true;
                                    int empid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Emp_Code"));


                                    //
                                    string empname = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("FullName"));
                                    NameList _obj = new NameList();
                                    _obj.Name = empname;
                                    nlist.Add(_obj);

                                    //UpdateProgressContext(Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("FullName")), RadGrid1.MasterTableView.Items.Count);

                                    double NHRate = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Hourly_Rate"));
                                    double basicpay = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Basic"));
                                    double OT1Rate = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("OT1Rate"));
                                    double OT2Rate = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("OT2Rate"));
                                    double NH_wh = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("NHHrs"));
                                    double OT1_wh = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("OT1Hrs"));
                                    double OT2_wh = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("OT2Hrs"));
                                    double NH_e = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("NH"));
                                    double OT1_e = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("OT1"));
                                    double OT2_e = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("OT2"));
                                    double additions = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("TotalAdditions"));
                                    double deductions = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("TotalDeductions"));
                                    double Wdays = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Days_Work"));
                                    double NetPay = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Netpay"));
                                    string ot_entitlement = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("OT"));
                                     double cpfAdd_Ordinary = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("CPFOrdinaryCeil"));
                                   // double cpfAdd_Ordinary = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("cpfordinary"));
                                    double cpfAdd_Additional = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("CPFAdditionNet"));
                                    double cpfNet = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("CPFGross"));
                                    double empCPF = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("EmployeeCPFAmt"));
                                    double employerCPF = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("EmployerCPFAmt"));
                                    
                                    double cpfAmount = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("CPFAmount"));
                                    string cpf_entitlement = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("CPF"));
                                    int empcpftype = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("EmpCPFType"));
                                    double pr_years = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("PRAge"));
                                    double cpf_ceiling = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("CPFCeiling"));
                                    string fund_type = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("FundType"));
                                    double fund_amount = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("FundAmount"));
                                    double unpaid_leaves = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("UnPaidLeaves"));
                                    double unpaid_leaves_amount = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("TotalUnPaid"));
                                    double total_gross = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("GrossWithAddition"));
                                    string pay_mode = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Pay_Mode"));
                                    string employee_giroacc = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("EmployeeGiro"));
                                    string employer_giroacc = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("EmployerGiro"));
                                    string giro_bank = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("GiroBank"));

                                    double fundgrossamount = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("FundGrossAmount"));
                                    double sdlfundgrossamount = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("SDLFundGrossAmount"));

                                    double CMOW = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("CMOW"));
                                    double LYOW = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("LYOW"));
                                    double CYOW = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("CYOW"));
                                    double CPFAWCIL = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("CPFAWCIL"));
                                    double EST_AWCIL = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("EST_AWCIL"));
                                    double ACTCIL = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("ACTCIL"));
                                    double AWCM = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("AWCM"));
                                    double AWB4CM = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("AWB4CM"));
                                    double AWCM_AWB4CM = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("AWCM_AWB4CM"));
                                    double AWSUBJCPF = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("AWSUBJCPF"));
                                    int SDFREQUIRED = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("SDF_REQUIRED"));
                                    double dailyrate = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Daily_Rate"));
                                    double daysworkedrate = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("DaysWorkedRate"));
                                    double CPFGross1 = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("CPFGross1"));
                                    TextBox txtPayment = (TextBox)this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].FindControl("txtPaymentDate");
                                    string paymentDate = Convert.ToDateTime(txtPayment.Text).ToString("dd/MM/yyyy", format);

                                    double mfc = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("mfc"));

                                    double mvc = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("mvc"));

                                    double cpfActual_Odinary = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("cpfordinary"));
                                    double cpfActual_Additional = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("awcm"));
                                    double wlevy = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("wlevy"));





                                    //double fundgrossamount = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("FundGrossAmount"));

                                    //     dblWorkDaysInRoll = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("WrkgDaysInRoll"));
                                    //     dblActWorkDaysInRoll = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("ActWrkgDaysSpan"));

                                    //     double addition_prorated = 0.0;

                                    //     if (dblActWorkDaysInRoll <= dblWorkDaysInRoll)
                                    //     {

                                    //         addition_prorated = (dblActWorkDaysInRoll / dblWorkDaysInRoll);
                                    //     }

                                    //     SqlParameter[] parmsadd = new SqlParameter[10];
                                    //     int j = 0;



                                    //     parmsadd[j++] = new SqlParameter("@company_id", Utility.ToInteger(Session["Compid"].ToString()));
                                    //     parmsadd[j++] = new SqlParameter("@month", Utility.ToInteger(cmbMonth.SelectedValue.ToString()));
                                    //     parmsadd[j++] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue.ToString()));
                                    //     parmsadd[j++] = new SqlParameter("@stdatemonth", Session["PayStartDay"].ToString());
                                    //     parmsadd[j++] = new SqlParameter("@endatemonth", Session["PayEndDay"].ToString());

                                    //     parmsadd[j++] = new SqlParameter("@stdatesubmonth", Session["PaySubStartDay"].ToString());
                                    //     parmsadd[j++] = new SqlParameter("@endatesubmonth", Session["PaySubEndDay"].ToString());


                                    //     parmsadd[j++] = new SqlParameter("@EmpPassID", empid);
                                    //     parmsadd[j++] = new SqlParameter("@monthidintbl", Utility.ToInteger(cmbMonth.SelectedValue.ToString()));
                                    //     parmsadd[j++] = new SqlParameter("@prorate", addition_prorated);


                                    //string   sSQLadd = "updateadditionprorated";   
                                    ////





                                    string status = "P";
                                    int i = 0;
                                    SqlParameter[] parms = new SqlParameter[61];
                                    parms[i++] = new SqlParameter("@emp_id", Utility.ToInteger(empid));
                                    parms[i++] = new SqlParameter("@basic_pay", Utility.ToDouble(basicpay));
                                    parms[i++] = new SqlParameter("@NHRate", Utility.ToDouble(NHRate));
                                    parms[i++] = new SqlParameter("@OT1Rate", Utility.ToDouble(OT1Rate));
                                    parms[i++] = new SqlParameter("@OT2Rate", Utility.ToDouble(OT2Rate));
                                    parms[i++] = new SqlParameter("@NH_wh", Utility.ToDouble(NH_wh));
                                    parms[i++] = new SqlParameter("@OT1_wh", Utility.ToDouble(OT1_wh));
                                    parms[i++] = new SqlParameter("@OT2_wh", Utility.ToDouble(OT2_wh));
                                    parms[i++] = new SqlParameter("@NH_e", Utility.ToDouble(NH_e));
                                    parms[i++] = new SqlParameter("@OT1_e", Utility.ToDouble(OT1_e));
                                    parms[i++] = new SqlParameter("@OT2_e", Utility.ToDouble(OT2_e));
                                    parms[i++] = new SqlParameter("@Wdays", Utility.ToDouble(Wdays));
                                    parms[i++] = new SqlParameter("@NetPay", NetPay.ToString());
                                    parms[i++] = new SqlParameter("@total_additions", Utility.ToDouble(additions));
                                    parms[i++] = new SqlParameter("@total_deductions", Utility.ToDouble(deductions));
                                    parms[i++] = new SqlParameter("@ot_entitlement", Utility.ToString(ot_entitlement));
                                    parms[i++] = new SqlParameter("@cpfadd_ord", Utility.ToDouble(cpfAdd_Ordinary));
                                    parms[i++] = new SqlParameter("@cpfadd_additional", Utility.ToDouble(cpfAdd_Additional));
                                    if (cpf_entitlement != "Y")
                                    {
                                        cpfNet = 0.0;
                                    }
                                    else {
                                        cpfNet = CPFGross1;
                                    }
                                    parms[i++] = new SqlParameter("@cpf_net", Utility.ToDouble(cpfNet));
                                     parms[i++] = new SqlParameter("@empCPF", Utility.ToDouble(empCPF));
                                    parms[i++] = new SqlParameter("@employerCPF", Utility.ToDouble(employerCPF));
                                    parms[i++] = new SqlParameter("@cpfAmount", Utility.ToDouble(cpfAmount));
                                    parms[i++] = new SqlParameter("@cpfEntitlement", Utility.ToString(cpf_entitlement));
                                    parms[i++] = new SqlParameter("@empCpfType", Utility.ToDouble(empcpftype));
                                    parms[i++] = new SqlParameter("@pr_years", Utility.ToDouble(pr_years));
                                    parms[i++] = new SqlParameter("@cpf_ceiling", Utility.ToDouble(cpf_ceiling));
                                    parms[i++] = new SqlParameter("@fund_type", Utility.ToString(fund_type));
                                    parms[i++] = new SqlParameter("@fund_amount", Utility.ToDouble(fund_amount));
                                    parms[i++] = new SqlParameter("@status", Utility.ToString(status));
                                    parms[i++] = new SqlParameter("@month", Utility.ToInteger(cmbMonth.SelectedValue));
                                    parms[i++] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
                                    parms[i++] = new SqlParameter("@unpaid_leaves", Utility.ToDouble(unpaid_leaves));
                                    parms[i++] = new SqlParameter("@unpaid_leaves_amount", Utility.ToDouble(unpaid_leaves_amount));
                                    parms[i++] = new SqlParameter("@total_gross", Utility.ToDouble(total_gross));
                                    parms[i++] = new SqlParameter("@pay_mode", Utility.ToString(pay_mode));
                                    parms[i++] = new SqlParameter("@employee_giroacc", Utility.ToString(employee_giroacc));
                                    parms[i++] = new SqlParameter("@employer_giroacc", Utility.ToString(employer_giroacc));
                                    parms[i++] = new SqlParameter("@giro_bank", Utility.ToString(giro_bank));
                                    parms[i++] = new SqlParameter("@submitted_by", Utility.ToString(EmpCode));
                                    parms[i++] = new SqlParameter("@fundgrossamount", Utility.ToDouble(fundgrossamount));
                                    parms[i++] = new SqlParameter("@sdlfundgrossamount", Utility.ToDouble(sdlfundgrossamount));
                                    parms[i++] = new SqlParameter("@CMOW", Utility.ToDouble(CMOW));
                                    parms[i++] = new SqlParameter("@LYOW", Utility.ToDouble(LYOW));
                                    parms[i++] = new SqlParameter("@CYOW", Utility.ToDouble(CYOW));
                                    parms[i++] = new SqlParameter("@CPFAWCIL", Utility.ToDouble(CPFAWCIL));
                                    parms[i++] = new SqlParameter("@EST_AWCIL", Utility.ToDouble(EST_AWCIL));
                                    parms[i++] = new SqlParameter("@ACTCIL", Utility.ToDouble(ACTCIL));
                                    parms[i++] = new SqlParameter("@AWCM", Utility.ToDouble(AWCM));
                                    parms[i++] = new SqlParameter("@AWB4CM", Utility.ToDouble(AWB4CM));
                                    parms[i++] = new SqlParameter("@AWCM_AWB4CM", Utility.ToDouble(AWCM_AWB4CM));
                                    parms[i++] = new SqlParameter("@AWCPF", Utility.ToDouble(AWSUBJCPF));
                                    parms[i++] = new SqlParameter("@sdfrequired", Utility.ToInteger(SDFREQUIRED));
                                    parms[i++] = new SqlParameter("@dailyrate", Utility.ToDouble(dailyrate));
                                    if (Utility.ToDouble(Wdays) <= 0)
                                    {
                                        parms[i++] = new SqlParameter("@daysworkedrate", Utility.ToDouble(0));
                                    }
                                    else
                                    {
                                        parms[i++] = new SqlParameter("@daysworkedrate", Utility.ToDouble(daysworkedrate));
                                    }
                                    parms[i++] = new SqlParameter("@CPFGrossAmount", Utility.ToDouble(CPFGross1));
                                    parms[i++] = new SqlParameter("@PaymentDate", Convert.ToDateTime(paymentDate));

                                    parms[i++] = new SqlParameter("@mfc", mfc);
                                    parms[i++] = new SqlParameter("@mvc", mvc);

                                    parms[i++] = new SqlParameter("@cpfActual_Odinary", cpfActual_Odinary);
                                    parms[i++] = new SqlParameter("@cpfActual_Additional", cpfActual_Additional);
                                    parms[i++] = new SqlParameter("@wlevy", Convert.ToDecimal(wlevy));

                                    //parms[i++] = new SqlParameter("@fundgrossamount", Utility.ToDouble(fundgrossamount));

                                    sSQL = "sp_payroll_add";


                                    try
                                    {
                                        if (Session["processPayroll"] == null)
                                        {
                                            DataAccess.ExecuteStoreProc(sSQL, parms);
                                        }

                                        if (Session["processPayroll"] != null)
                                        {

                                            if ((string)Session["processPayroll"].ToString() == "1")
                                            {
                                                DataAccess.ExecuteStoreProc(sSQL, parms);
                                                //   DataAccess.ExecuteStoreProc(sSQLadd, parmsadd);

                                            }

                                            //Process the Payroll For Selected Employee As Submit/Approve/Generate
                                            if ((string)Session["processPayroll"].ToString() == "0")
                                            {
                                                DataAccess.ExecuteStoreProc(sSQL, parms);

                                                DataSet ds = new DataSet();
                                                SqlParameter[] param1 = new SqlParameter[5];
                                                param1[0] = new SqlParameter("@company_id", Utility.ToInteger(Session["compid"]));
                                                param1[1] = new SqlParameter("@month", Utility.ToInteger(cmbMonth.SelectedValue));
                                                param1[2] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
                                                param1[3] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"].ToString()));
                                                param1[4] = new SqlParameter("@Status", "P");

                                                ds = DataAccess.FetchRS(CommandType.StoredProcedure, "Sp_approvepayroll", param1);
                                                //exec sp_ApprovePayRoll @company_id=4,@month=318,@year=2013,@UserID=305,@Status=N'P',@DeptId=N'34'


                                                if (ds.Tables.Count > 0)
                                                {
                                                    foreach (DataRow dr in ds.Tables[0].Rows)
                                                    {
                                                        //exec sp_payroll_Update @trx_id=4671,@trxdate=N'30/01/2012 11:01:01',@status=N'A'
                                                        SqlParameter[] param2 = new SqlParameter[3];
                                                        int c = 0;
                                                        param2[c++] = new SqlParameter("@trx_id", Utility.ToInteger(dr["trx_id"].ToString()));
                                                        param2[c++] = new SqlParameter("@trxdate", String.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now));
                                                     //   param2[c++] = new SqlParameter("@trxdate", Utility.ToString(System.DateTime.Now.Month + "/" + System.DateTime.Now.Day + "/" + System.DateTime.Now.Year));
                                                        char App = 'A';
                                                        param2[c++] = new SqlParameter("@status", "A");
                                                        DataAccess.ExecuteStoreProc("sp_payroll_Update", param2);
                                                    }
                                                }

                                                //Generate the Payroll

                                                //exec sp_ApprovePayRoll @company_id=3,@month=61,@year=2012,@UserID=88,@Status=N'A'

                                                //exec sp_payroll_Update @trx_id=4671,@status=N'G',@trxdate=N'30/01/2012 11:04:28'

                                                ds = null;

                                                SqlParameter[] param3 = new SqlParameter[5];
                                                param3[0] = new SqlParameter("@company_id", Utility.ToInteger(Session["compid"]));
                                                param3[1] = new SqlParameter("@month", Utility.ToInteger(cmbMonth.SelectedValue));
                                                param3[2] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
                                                param3[3] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"].ToString()));
                                                param3[4] = new SqlParameter("@Status", "A");
                                                ds = DataAccess.FetchRS(CommandType.StoredProcedure, "sp_ApprovePayRoll", param3);

                                                if (ds.Tables.Count > 0)
                                                {
                                                    foreach (DataRow dr in ds.Tables[0].Rows)
                                                    {
                                                        //exec sp_payroll_Update @trx_id=4671,@status=N'G',@trxdate=N'30/01/2012 11:04:28'
                                                        SqlParameter[] param4 = new SqlParameter[3];
                                                        param4[0] = new SqlParameter("trx_id", Utility.ToInteger(dr["trx_id"].ToString()));
                                                        param4[1] = new SqlParameter("@trxdate", String.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now));

                                                       // param4[1] = new SqlParameter("@trxdate", Utility.ToString(System.DateTime.Now.Month + "/" + System.DateTime.Now.Day + "/" + System.DateTime.Now.Year));
                                                        char App1 = 'G';
                                                        param4[2] = new SqlParameter("@status", "G");
                                                        DataAccess.ExecuteStoreProc("sp_payroll_Update", param4);
                                                    }
                                                }


                                            }
                                        }
                                       
                                    }
                                    catch (Exception ex)
                                    {
                                        string ErrMsg = ex.Message;
                                        if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                                        {
                                            ErrMsg = "<font color = 'Red'>Unable to update the status.Try again!</font>";
                                        }
                                    }
                                }
                            }
                        }
                        Session["SubmitClickCount"] = "0";

                        // RadGrid1.DataBind();

                        if (blnisrecsel == false)
                        {
                            //ShowMessageBox("Select Employees to Submit Payroll");
                            _actionMessage = "Warning|Select Employees to Submit Payroll";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                    }
                }
                else
                {
                    // ShowMessageBox("Choose payment date");
                    _actionMessage = "Warning|Choose payment date";
                    ViewState["actionMessage"] = _actionMessage;
                }
                sw.Stop();
                TimeSpan ts = sw.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                // ShowMessageBox("Payroll processed. time taken"+ elapsedTime);
            }

            if (nlist.Count >= 1)
            {
                Session["Name"] = nlist;
                Response.Redirect("../Payroll/ProcessedPayroll.aspx");

            }
        }

        protected void bindgrid(object sender, EventArgs e)
        {
            //UpdateProgressContext("", 100);
            //btnsubapprove.Enabled = false;
            if (strEmpvisible != "")
            {
                Session["EmpPassID"] = strEmpvisible;
            }
            else
            {
                Session["EmpPassID"] = ""; 
                //Session["EmpPassID"] = null;
            }


            CallBeforeMonthFill();
            SetControlDate();
            intcnt = 1;
            cmbYear.Enabled = false;
            cmbMonth.Enabled = false;
            imgbtnfetch.Enabled = false;

            string com_id = Session["Compid"].ToString();
            string emp_id = Session["EmpCode"].ToString();
            string _cmpyear = cmbYear.SelectedValue.ToString();
            string _cmpmonth =cmbMonth.SelectedValue.ToString();
            string _dptid= deptID.SelectedValue.ToString();
           // deptID.Enabled = false;

           
            try
            {
                RadGrid1.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
            // GridToolBar.Visible = true;

            //foreach (Telerik.Web.UI.GridItem item in RadGrid1.MasterTableView.Items)
            //{
            //    if (item is GridItem)
            //    {
            //        GridDataItem dataItem = (GridDataItem)item;
            //        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];


            //        int empid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Emp_Code"));
            //        dblWorkDaysInRoll = Convert.ToDecimal(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("WrkgDaysInRoll"));
            //        dblActWorkDaysInRoll = Convert.ToDecimal(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("ActWrkgDaysSpan"));

            //        decimal paidfullday = Convert.ToDecimal(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("paidfullday"));
            //        decimal paidhalfday = Convert.ToDecimal(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("paidhalfday"));
            //        decimal unpaidfullday = Convert.ToDecimal(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("unpaidfullday"));
            //        decimal unpaidhaldday = Convert.ToDecimal(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("unpaidhalfday"));







            //        decimal addition_prorated = 0.0m;

            //        if (dblActWorkDaysInRoll != 0.0m)
            //        {

            //            if (dblActWorkDaysInRoll <= dblWorkDaysInRoll)
            //            {

            //                addition_prorated = (dblActWorkDaysInRoll / dblWorkDaysInRoll);
            //            }
            //        }

            //        decimal leave_prorated = 0.0m;

            //        if (dblActWorkDaysInRoll != 0.0m)
            //        {

            //            if (dblActWorkDaysInRoll <= dblWorkDaysInRoll)
            //            {

            //                leave_prorated = ((dblActWorkDaysInRoll - (paidfullday + paidhalfday+unpaidfullday + unpaidhaldday))                                
            //                    / dblWorkDaysInRoll);
            //            }
            //        }

            //        decimal unpaidleave_prorated = 0.0m;

            //        if (dblActWorkDaysInRoll != 0.0m)
            //        {

            //            if (dblActWorkDaysInRoll <= dblWorkDaysInRoll)
            //            {

            //                unpaidleave_prorated = ((dblActWorkDaysInRoll - (unpaidfullday + unpaidhaldday))
            //                    / dblWorkDaysInRoll);
            //            }
            //        }





            //        SqlParameter[] parmsadd = new SqlParameter[12];
                
            //        int j = 0;
            //        string sSQLadd = "updateadditionprorated";
            //        string sSQLduc = "updatedeductionprorated";


            //        parmsadd[j++] = new SqlParameter("@company_id", Utility.ToInteger(Session["Compid"].ToString()));
            //        parmsadd[j++] = new SqlParameter("@month", Utility.ToInteger(cmbMonth.SelectedValue.ToString()));
            //        parmsadd[j++] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue.ToString()));
            //        parmsadd[j++] = new SqlParameter("@stdatemonth", Session["PayStartDay"].ToString());
            //        parmsadd[j++] = new SqlParameter("@endatemonth", Session["PayEndDay"].ToString());

            //        parmsadd[j++] = new SqlParameter("@stdatesubmonth", Session["PaySubStartDay"].ToString());
            //        parmsadd[j++] = new SqlParameter("@endatesubmonth", Session["PaySubEndDay"].ToString());


            //        parmsadd[j++] = new SqlParameter("@EmpPassID", empid);
            //        parmsadd[j++] = new SqlParameter("@monthidintbl", Utility.ToInteger(cmbMonth.SelectedValue.ToString()));
            //        parmsadd[j++] = new SqlParameter("@prorate", addition_prorated);
            //        parmsadd[j++] = new SqlParameter("@leaveprorate", leave_prorated);
            //        parmsadd[j++] = new SqlParameter("@unpaidprorate", unpaidleave_prorated);
            //     //   DataAccess.ExecuteStoreProc(sSQLadd, parmsadd);
            //        //DataAccess.ExecuteStoreProc(sSQLduc, parmsadd);

            //    }
            //}

            Session["ROWID"] = cmbMonth.SelectedValue.ToString();
            Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();
            IFormatProvider formatdate = new System.Globalization.CultureInfo("en-GB", true);
            try
            {
                foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;


                        string strEmpCode = this.RadGrid1.Items[dataItem.ItemIndex]["Emp_Code"].Text.ToString();
                        TextBox txtPayDate = (TextBox)dataItem.FindControl("txtPaymentDate");
                        SqlConnection conn = new SqlConnection(Constants.CONNECTION_STRING);

                        DataTable dtPayData = new DataTable();
                        DataSet rptPayDs = new DataSet();

                        // Grab each of the values from our windows form         
                        string sqlPayQuery = "sp_GetPayDateByEmployee";
                        SqlParameter[] parmsAdd = new SqlParameter[3];
                        parmsAdd[0] = new SqlParameter("@month", cmbMonth.SelectedValue);
                        parmsAdd[1] = new SqlParameter("@year", cmbYear.SelectedValue);
                        parmsAdd[2] = new SqlParameter("@empcode", Convert.ToInt32(strEmpCode));
                        rptPayDs = DataAccess.FetchRS(CommandType.StoredProcedure, sqlPayQuery, parmsAdd);
                        dtPayData = rptPayDs.Tables[0];

                        if (dtPayData.Rows.Count > 0)
                        {
                            txtPayDate.Text = dtPayData.Rows[0]["PaymentDate"].ToString();
                        }
                        else
                        {
                            txtPayDate.Text = "";
                        }
                        //Audit part
                        ViewState["AuditActionModuleRef"] = "8||Payroll_module||0";
                    }
                }
            }
            catch (Exception err)
            {
               // ShowMessageBox("Error in data " + err.Message.ToString());
                _actionMessage = "Warning|Error in data " + err.Message.ToString();
                ViewState["actionMessage"] = _actionMessage;
            }

        }

        protected void sendemail()
        {
            bool result = false;
            string month = "";
            int i = 0;
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string ssql = "sp_GetPayrollMonth";// 0,2009,2
            SqlParameter[] parms = new SqlParameter[3];
            parms[i++] = new SqlParameter("@ROWID", "0");
            parms[i++] = new SqlParameter("@YEARS", 0);
            parms[i++] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
            monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);

            DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + cmbMonth.SelectedValue);
            foreach (DataRow drnew in drResults)
            {
                month = drnew["Month"].ToString();
                Session["PayStartDate"] = drnew["PayStartDate"].ToString();
            }

            string from = "";
            string to = "";
            string SMTPserver = "";
            string SMTPUser = "";
            string SMTPPass = "";
            string approver = empname;
            string year = cmbYear.SelectedValue;
            string emailreq = "";
            int SMTPPORT = 25;
            string body = "";
            string cc = "";

            string SQL = "select email from employee where Emp_Code=" + EmpCode;
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQL, null);
            while (dr.Read())
            {
                from = Utility.ToString(dr.GetValue(0));
            }

            if (string.IsNullOrEmpty(from))
            {
                string SQL_com = "select email_sender from company where Company_Id=" + comp_id;
                SqlDataReader dr_com = DataAccess.ExecuteReader(CommandType.Text, SQL_com, null);
                while (dr_com.Read())
                {
                    from = Utility.ToString(dr_com.GetValue(0));
                }
            }

            sSQL = "sp_submit_email1";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@comp_id", Utility.ToInteger(Session["compid"]));
            SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL, parm);
            while (dr1.Read())
            {
                to = Utility.ToString(dr1.GetValue(6));
                SMTPserver = Utility.ToString(dr1.GetValue(0));
                SMTPUser = Utility.ToString(dr1.GetValue(1));
                SMTPPass = Utility.ToString(dr1.GetValue(2));
                emailreq = Utility.ToString(dr1.GetValue(5)).ToLower();
                SMTPPORT = Utility.ToInteger(dr1.GetValue(4));
                body = Utility.ToString(dr1.GetValue(3));
                cc = Utility.ToString(dr1.GetValue(8));
            }
            if (emailreq == "yes")
            {
                string subject = "Payroll for the period " + month + "/" + year;
                body = body.Replace("@month", month);
                body = body.Replace("@year", year);
                body = body.Replace("@hr", empname);

                //kumar
                #region Get SSl required
                string SSL = "";
                string sqll = "select sslrequired from company where Company_Id='" + Utility.ToInteger(this.comp_id) + "'";
                SqlDataReader dr_ssl = DataAccess.ExecuteReader(CommandType.Text, sqll, null);
                if (dr_ssl.HasRows)
                {
                    while (dr_ssl.Read())
                    {
                        SSL = Utility.ToString(dr_ssl.GetValue(0));
                    }
                }
                #endregion

                SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(SMTPserver, SMTPUser, SMTPPass, from, SMTPUser, SMTPPORT, SSL);

                //SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(Utility.ToInteger(Session["Compid"]));

                oANBMailer.Subject = subject;
                oANBMailer.MailBody = body;

                oANBMailer.From = from;

                //oANBMailer.From = "ravi@anbgroup.com";

                oANBMailer.To = to;
                oANBMailer.Cc = cc;

                //try
                //{
                //    string sRetVal = oANBMailer.SendMail();
                //    if (sRetVal == "")
                //        //Response.Write("<Font color=green size=3> An email has been sent to " + to + "</Font> <BR />");
                //        lblLoading.Text = "An email has been sent to " + to + "";

                //    else
                //        // Response.Write("<Font color=red size=3> An error occurred: Details are as follows <BR />" + sRetVal + "</Font>");
                //        lblLoading.Text = "An error occurred while Email";

                //}
                //catch (Exception ex)
                //{
                //    string errMsg = ex.Message;
                //}

                string sRetVal = "";

                try
                {
                    //string sRetVal = oANBMailer.SendMail();

                    sRetVal = oANBMailer.SendMail("Payroll", empname, "", "", "SubmitPayroll");

                    if (sRetVal != "SUCCESS")
                    {
                        // lblLoading1.Text = "An email has been sent to " + to + "";
                        Response.Redirect("../Payroll/EmailErrorPayroll.aspx?Err=" + Server.UrlEncode(sRetVal));

                    }
                    //else
                    //{
                    //    //lblLoading1.Text = "An error occurred while Email";
                    //    //r -showing error message
                    //    //strMessage = strMessage + "<br/>" + "Error Occured While Sending Mail. - " + sRetVal;
                    //    //r
                    //    //lblMsg1.Text = strMessage;


                    //}
                }
                catch (Exception ex)
                {
                    string errMsg = ex.Message;
                    Response.Redirect("../Payroll/EmailErrorPayroll.aspx?Err=" + Server.UrlEncode(sRetVal));
                }
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            //RadGrid1.DataSourceID = "SqlDataSource1";
            //if (RadGrid1.Items.Count > 0)
            //{
            //    RadGrid1.ExportSettings.ExportOnlyData = true;
            //    RadGrid1.ExportSettings.IgnorePaging = CheckBox1.Checked;
            //    RadGrid1.ExportSettings.OpenInNewWindow = true;
            //    RadGrid1.MasterTableView.ExportToExcel();
            //}
            //else
            //{
            //    dataexportmessage.Visible = true;
            //}
        }


        // [AjaxPro.AjaxMethod]
        protected void btndetail_Click(object sender, EventArgs e)
        {
            int monthid = (short)Utility.ToInteger(cmbMonth.SelectedValue);
            int yearid = (short)Utility.ToInteger(cmbYear.SelectedValue);

            int DEPTID = (short)Utility.ToInteger(deptID.SelectedValue);
            int i = 0;
            string ssql = "sp_GetPayrollMonth";// 0,2009,2
            SqlParameter[] parms = new SqlParameter[3];
            parms[i++] = new SqlParameter("@ROWID", "0");
            parms[i++] = new SqlParameter("@YEARS", 0);
            parms[i++] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
            monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);
            DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + monthid.ToString());
            string str = "";
            foreach (DataRow dr in drResults)
            {
                Session["PayStartDay"] = dr["PayStartDay"].ToString();
                Session["PayEndDay"] = dr["PayEndDay"].ToString();
                Session["PaySubStartDay"] = dr["PaySubStartDay"].ToString();
                Session["PaySubEndDay"] = dr["PaySubEndDay"].ToString();
                //Session["EmpPassID"] = dr["emp_code"].ToString();
                //str = "paydetailreport.aspx?UserID=" + Session["EmpCode"].ToString() + "&Month=" + dr["Month"].ToString() + "&stdatemonth=" + Session["PayStartDay"].ToString() + "&endatemonth=" + Session["PayEndDay"].ToString() + "&stdatesubmonth=" + Session["PaySubStartDay"].ToString() + "&endatesubmonth=" + Session["PaySubEndDay"].ToString() + "&monthidintbl=" + monthid.ToString() + "&Year=" + yearid.ToString() + "&company_id=" + Session["Compid"].ToString() + "&EmpPassID=" + Session["EmpPassID"] + "&dept_id=" + DEPTID.ToString();
                str = "payrolldetailreport_New.aspx?UserID=" + Session["EmpCode"].ToString() + "&Month=" + dr["Month"].ToString() + "&stdatemonth=" + Session["PayStartDay"].ToString() + "&endatemonth=" + Session["PayEndDay"].ToString() + "&stdatesubmonth=" + Session["PaySubStartDay"].ToString() + "&endatesubmonth=" + Session["PaySubEndDay"].ToString() + "&monthidintbl=" + monthid.ToString() + "&Year=" + yearid.ToString() + "&company_id=" + Session["Compid"].ToString() + "&EmpPassID=" + Session["EmpPassID"] + "&dept_id=" + DEPTID.ToString();
               // str = "payrolldetailsreport_approved.aspx?UserID=" + Session["EmpCode"].ToString() + "&Month=" + dr["Month"].ToString() + "&stdatemonth=" + Session["PayStartDay"].ToString() + "&endatemonth=" + Session["PayEndDay"].ToString() + "&stdatesubmonth=" + Session["PaySubStartDay"].ToString() + "&endatesubmonth=" + Session["PaySubEndDay"].ToString() + "&monthidintbl=" + monthid.ToString() + "&Year=" + yearid.ToString() + "&company_id=" + Session["Compid"].ToString() + "&EmpPassID=" + Session["EmpPassID"] + "&dept_id=" + DEPTID.ToString();

            }
            string popupScript = "<script language='javascript'>" + "window.open('', '" + str + "', " + "'width=1000, height=1000, menubar=yes, resizable=yes')" + "</script>";
            HttpContext.Current.Response.Write("<SCRIPT language='Javascript'>window.open('" + str + "');</SCRIPT>");
            ViewState["AuditActionModuleRef"] = "12||Payroll_module||0";
        }

        [AjaxPro.AjaxMethod]
        protected void btnSummary_Click(object sender, EventArgs e)
        {
            //int i = 0;
            //string ssql = "sp_GetPayrollMonth";// 0,2009,2
            //SqlParameter[] parms = new SqlParameter[3];
            //parms[i++] = new SqlParameter("@ROWID", "0");
            //parms[i++] = new SqlParameter("@YEARS", 0);
            //parms[i++] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
            //monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);
            //DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + monthid.ToString());
            string str = "";
            string empid = "0";
            foreach (Telerik.Web.UI.GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        empid = empid + "," + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Emp_Code"));
                    }
                }
            }


            if (empid == "0")
            {
                str = "paydetailreport.aspx?EmpPassID=&UserID=" + Session["EmpCode"].ToString() + "&Month=" + Session["ROWID"].ToString() + "&stdatemonth=" + Session["PayStartDay"].ToString() + "&endatemonth=" + Session["PayEndDay"].ToString() + "&stdatesubmonth=" + Session["PaySubStartDay"].ToString() + "&endatesubmonth=" + Session["PaySubEndDay"].ToString() + "&monthidintbl=" + Session["ROWID"].ToString() + "&Year=" + Session["ROWYEAR"].ToString() + "&company_id=" + Session["Compid"].ToString() + "&dept_id=" + deptID.SelectedValue.ToString();
            }
            else
            {
                str = "paydetailreport.aspx?EmpPassID=" + empid + "&UserID=" + Session["EmpCode"].ToString() + "&Month=" + Session["ROWID"].ToString() + "&stdatemonth=" + Session["PayStartDay"].ToString() + "&endatemonth=" + Session["PayEndDay"].ToString() + "&stdatesubmonth=" + Session["PaySubStartDay"].ToString() + "&endatesubmonth=" + Session["PaySubEndDay"].ToString() + "&monthidintbl=" + Session["ROWID"].ToString() + "&Year=" + Session["ROWYEAR"].ToString() + "&company_id=" + Session["Compid"].ToString() + "&dept_id=" + deptID.SelectedValue.ToString();
            }
            string popupScript = "<script language='javascript'>" + "window.open('', '" + str + "', " + "'width=1000, height=1000, menubar=yes, resizable=yes')" + "</script>";
            HttpContext.Current.Response.Write("<SCRIPT language='Javascript'>window.open('" + str + "');</SCRIPT>");
            //Audit
            ViewState["AuditActionModuleRef"] = "12||Payroll_module||0";
            //Audit
            //return str;
        }

        [AjaxPro.AjaxMethod]
        protected void btnPayroll_Click(object sender, EventArgs e)
        {
            //int i = 0;
            //string ssql = "sp_GetPayrollMonth";// 0,2009,2
            //SqlParameter[] parms = new SqlParameter[3];
            //parms[i++] = new SqlParameter("@ROWID", "0");
            //parms[i++] = new SqlParameter("@YEARS", 0);
            //parms[i++] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
            //monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);
            //DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + monthid.ToString());
            string str = "";
            string empid = "0";
            foreach (Telerik.Web.UI.GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        empid = empid + "," + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Emp_Code"));
                    }
                }
            }

            if (empid == "0")
            {
                str = "payrolldetailreport.aspx?EmpPassID=&UserID=" + Session["EmpCode"].ToString() + "&Month=" + Session["ROWID"].ToString() + "&stdatemonth=" + Session["PayStartDay"].ToString() + "&endatemonth=" + Session["PayEndDay"].ToString() + "&stdatesubmonth=" + Session["PaySubStartDay"].ToString() + "&endatesubmonth=" + Session["PaySubEndDay"].ToString() + "&monthidintbl=" + Session["ROWID"].ToString() + "&Year=" + Session["ROWYEAR"].ToString() + "&company_id=" + Session["Compid"].ToString() + "&dept_id=" + deptID.SelectedValue.ToString();
            }
            else
            {
                str = "payrolldetailreport.aspx?EmpPassID=" + empid + "&UserID=" + Session["EmpCode"].ToString() + "&Month=" + Session["ROWID"].ToString() + "&stdatemonth=" + Session["PayStartDay"].ToString() + "&endatemonth=" + Session["PayEndDay"].ToString() + "&stdatesubmonth=" + Session["PaySubStartDay"].ToString() + "&endatesubmonth=" + Session["PaySubEndDay"].ToString() + "&monthidintbl=" + Session["ROWID"].ToString() + "&Year=" + Session["ROWYEAR"].ToString() + "&company_id=" + Session["Compid"].ToString() + "&dept_id=" + deptID.SelectedValue.ToString();
            }
            string popupScript = "<script language='javascript'>" + "window.open('', '" + str + "', " + "'width=1000, height=1000, menubar=yes, resizable=yes')" + "</script>";
            HttpContext.Current.Response.Write("<SCRIPT language='Javascript'>window.open('" + str + "');</SCRIPT>");


            //return str;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            RadGrid1.DataSourceID = "SqlDataSource1";
            if (RadGrid1.Items.Count > 0)
            {
                //RadGrid1.ExportSettings.ExportOnlyData = true;
                //RadGrid1.ExportSettings.IgnorePaging = CheckBox1.Checked;
                //RadGrid1.ExportSettings.OpenInNewWindow = true;
                //RadGrid1.MasterTableView.ExportToWord();
            }
            else
            {
                dataexportmessage.Visible = true;
            }

            //WebRequest mywebReq;
            //WebResponse mywebResp;
            //StreamReader sr;
            //string strHTML;
            //StreamWriter sw;

            //string strurl = Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf("/")) + "/EmployeePayReport.aspx?qsEmpID=11&qsMonth=151&qsYear=2010&st=1&en=30&stmonth=1/10/2010&endmonth=15/10/2010&monthintbl=151";
            //mywebReq = WebRequest.Create(strurl);
            //mywebResp = mywebReq.GetResponse();
            //sr = new StreamReader(mywebResp.GetResponseStream());
            //strHTML = sr.ReadToEnd();
            //sw = File.CreateText(Server.MapPath("temp.html"));
            //sw.WriteLine(strHTML);
            //sw.Close();
            //Response.WriteFile(Server.MapPath("temp.html"));
        }

        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {

        }

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            double netpay = 0;
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem item = e.Item as GridDataItem;
                System.Web.UI.WebControls.HyperLink Img = (System.Web.UI.WebControls.HyperLink)item.FindControl("Image3");
                string strMediumUrl = e.Item.Cells[37].Text;
                //http://localhost:2814/Payroll/EmployeePayReport.aspx?qsEmpID=384&qsMonth=70&qsYear=2012&st=1&en=31&stmonth=1/10/2012&endmonth=31/10/2012&monthintbl=70
                string strmsg = "javascript:ShowInsert('" + strMediumUrl + "&CPF1=" + item["CPFGross"].Text + "');";
                Img.Attributes.Add("onclick", strmsg);
                if (e.Item.Cells[26].Text != "&nbsp;")
                {
                    netpay = Utility.ToDouble(e.Item.Cells[26].Text);
                }
                if (netpay < 0)
                {
                    e.Item.Cells[26].BackColor = Color.Red;
                }
                //if (e.Item.Cells[50].Text.ToString() == "0")
                //{
                //    ((CheckBox)item["GridClientSelectColumn"].Controls[0]).Visible = false;

                //    // hiding detail column link
                //    HyperLink HlLink = (HyperLink)e.Item.FindControl("Image3");
                //    HlLink.Text = "";

                //}
                if (e.Item is GridItem)
                {
                    GridDataItem dataItem = e.Item as GridDataItem;
                    string strvar = dataItem["PayProcessFH"].Text.ToString();
                    if (strvar == "0")
                    {
                        ((CheckBox)item["GridClientSelectColumn"].Controls[0]).Visible = false;

                        // hiding detail column link
                        HyperLink HlLink = (HyperLink)e.Item.FindControl("Image3");
                        HlLink.Text = "";
                    }
                }

                RadGrid1.MasterTableView.GetColumn("TemplateColumn").Display = false;

                // //foreach (GridDataItem item in RadGrid1.MasterTableView.Items)
                //// {
                //     if (strEmpvisible != "")
                //     {
                //         char char1 = ',';
                //         string[] array = strEmpvisible.Split(char1);
                //         foreach (string str in array)
                //         {
                //             if (item["Emp_Code"].Text == str)
                //             {
                //                 e.Item.Visible = true;
                //             }
                //             else
                //             {
                //                 e.Item.Visible = false;
                //             }
                //         }

                //     }
                //// }               
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
                        MonthFill();
                    }
                    cmbMonth.SelectedValue = Utility.ToString(Session["ROWID"]);
                    cmbYear.SelectedValue = Utility.ToString(Session["ROWYEAR"]);
                }
                SetControlDate();
            }
            //---------murugan
            Session["ROWID"] = cmbMonth.SelectedValue.ToString();
            Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();
            IFormatProvider formatdate = new System.Globalization.CultureInfo("en-GB", true);
            try
            {
                foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;


                        string strEmpCode = this.RadGrid1.Items[dataItem.ItemIndex]["Emp_Code"].Text.ToString();
                        TextBox txtPayDate = (TextBox)dataItem.FindControl("txtPaymentDate");
                        SqlConnection conn = new SqlConnection(Constants.CONNECTION_STRING);

                        DataTable dtPayData = new DataTable();
                        DataSet rptPayDs = new DataSet();

                        // Grab each of the values from our windows form         
                        string sqlPayQuery = "sp_GetPayDateByEmployee";
                        SqlParameter[] parmsAdd = new SqlParameter[3];
                        parmsAdd[0] = new SqlParameter("@month", cmbMonth.SelectedValue);
                        parmsAdd[1] = new SqlParameter("@year", cmbYear.SelectedValue);
                        parmsAdd[2] = new SqlParameter("@empcode", Convert.ToInt32(strEmpCode));
                        rptPayDs = DataAccess.FetchRS(CommandType.StoredProcedure, sqlPayQuery, parmsAdd);
                        dtPayData = rptPayDs.Tables[0];

                        if (dtPayData.Rows.Count > 0)
                        {
                            txtPayDate.Text = dtPayData.Rows[0]["PaymentDate"].ToString();
                        }
                        else
                        {
                           // txtPayDate.Text = "";
                        }


                    }
                }
            }
            catch (Exception err)
            {
                ShowMessageBox("Error in data " + err.Message.ToString());
            }

        }

        protected void chkPayment_CheckedChanged(object sender, EventArgs e)
        {

            IFormatProvider formatdate = new System.Globalization.CultureInfo("en-GB", true);
            try
            {
                foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
                {
                    if (rdPayDatePicker.SelectedDate == null)
                    {
                        chkDateSelection.Checked = false;
                        throw new Exception("Invalid Payment Date");
                    }
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        TextBox txtPayDate = (TextBox)dataItem.FindControl("txtPaymentDate");
                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                        if (chkDateSelection.Checked == true && chkBox.Checked ==true )
                        {
                            btnsubapprove.Enabled = true;
                            string strEmpCode = this.RadGrid1.Items[dataItem.ItemIndex]["Emp_Code"].Text.ToString();

                            SqlConnection conn = new SqlConnection(Constants.CONNECTION_STRING);

                            DataTable dtPayData = new DataTable();
                            DataSet rptPayDs = new DataSet();

                            // Grab each of the values from our windows form         
                            string sqlPayQuery = "sp_GetPayDateByEmployee";
                            SqlParameter[] parmsAdd = new SqlParameter[3];
                            parmsAdd[0] = new SqlParameter("@month", cmbMonth.SelectedValue);
                            parmsAdd[1] = new SqlParameter("@year", cmbYear.SelectedValue);
                            parmsAdd[2] = new SqlParameter("@empcode", Convert.ToInt32(strEmpCode));
                            rptPayDs = DataAccess.FetchRS(CommandType.StoredProcedure, sqlPayQuery, parmsAdd);
                            dtPayData = rptPayDs.Tables[0];

                            if (dtPayData.Rows.Count > 0 && dtPayData.Rows[0]["PaymentDate"].ToString() != string.Empty)
                            {
                                txtPayDate.Text = dtPayData.Rows[0]["PaymentDate"].ToString();
                            }
                            else
                            {
                                txtPayDate.Text = rdPayDatePicker.SelectedDate.Value.ToString("dd/MM/yyyy", formatdate);
                            }

                        }
                        else
                        {
                            txtPayDate.Text = "";
                           // btnsubapprove.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                //ShowMessageBox("Error in data " + err.Message.ToString());
                _actionMessage = "Warning|Error in data " + err.Message.ToString();
                ViewState["actionMessage"] = _actionMessage;
            }

        }

        void SetControlDate()
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + cmbMonth.SelectedValue);
            foreach (DataRow dr in drResults)
            {
                string s = dr["PayStartDay"].ToString();
                s = dr["PayEndDay"].ToString();
                s = dr["PaySubStartDay"].ToString();
                s = dr["PaySubEndDay"].ToString();
                s = dr["PaySubStartDate"].ToString();
                s= dr["PaySubEndDate"].ToString();
               

                Session["PayStartDay"] = dr["PayStartDay"].ToString();
                Session["PayEndDay"] = dr["PayEndDay"].ToString();
                Session["PaySubStartDay"] = dr["PaySubStartDay"].ToString();
                Session["PaySubEndDay"] = dr["PaySubEndDay"].ToString();
                Session["PaySubStartDate"] = dr["PaySubStartDate"].ToString();
                Session["PaySubEndDate"] = dr["PaySubEndDate"].ToString();
                Session["IsDateCalculation"] = "1";
                //if (strEmpvisible != "")
                //{
                //    Session["EmpPassID"] = strEmpvisible;
                //}
                //else
                //{
                //    Session["EmpPassID"] = "";
                //}
            }

            //+ "&dept_id=" + deptID.SelectedValue.ToString()
        }
        [AjaxPro.AjaxMethod]
        public string btnPayrollDetail_Click(int monthid, int yearid, int DEPTID)
        {
            int i = 0;
            string ssql = "sp_GetPayrollMonth";// 0,2009,2
            SqlParameter[] parms = new SqlParameter[3];
            parms[i++] = new SqlParameter("@ROWID", "0");
            parms[i++] = new SqlParameter("@YEARS", 0);
            parms[i++] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
            monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);
            DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + monthid.ToString());
            string str = "";
            foreach (DataRow dr in drResults)
            {
                Session["PayStartDay"] = dr["PayStartDay"].ToString();
                Session["PayEndDay"] = dr["PayEndDay"].ToString();
                Session["PaySubStartDay"] = dr["PaySubStartDay"].ToString();
                Session["PaySubEndDay"] = dr["PaySubEndDay"].ToString();
                //if (strEmpvisible != "")
                //{
                //    Session["EmpPassID"] = dr["emp_code"].ToString();
                //}
                str = "payrolldetailreport_New.aspx?UserID=" + Session["EmpCode"].ToString() + "&Month=" + dr["Month"].ToString() + "&stdatemonth=" + Session["PayStartDay"].ToString() + "&endatemonth=" + Session["PayEndDay"].ToString() + "&stdatesubmonth=" + Session["PaySubStartDay"].ToString() + "&endatesubmonth=" + Session["PaySubEndDay"].ToString() + "&monthidintbl=" + monthid.ToString() + "&Year=" + yearid.ToString() + "&company_id=" + Session["Compid"].ToString() + "&dept_id=" + DEPTID.ToString();
            }
            string popupScript = "<script language='javascript'>" + "window.open('', '" + str + "', " + "'width=1000, height=1000, menubar=yes, resizable=yes')" + "</script>";
            return str;
        }
        //private void InitializeComponent()
        //{
        //    this.Init += new System.EventHandler(this.SubmitPayroll_Init);
        //    this.Load += new System.EventHandler(this.Page_Load);
        //    this.imgbtnfetch.Click += new ImageClickEventHandler(this.bindgrid);
        //    this.Button4.Click += new System.EventHandler(this.Button1_Click);
        //    this.Button5.Click += new System.EventHandler(this.Button2_Click);
        //    this.btnsubapprove.Click += new System.EventHandler(this.btnsubapprove_click);

        //}
        protected void btnReportAll_Click(object sender, EventArgs e)
        {
            //int i = 0;
            //string ssql = "sp_GetPayrollMonth";// 0,2009,2
            //SqlParameter[] parms = new SqlParameter[3];
            //parms[i++] = new SqlParameter("@ROWID", "0");
            //parms[i++] = new SqlParameter("@YEARS", 0);
            //parms[i++] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
            //monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);
            //DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + monthid.ToString());
            string str = "";
            string empid = "0";
            foreach (Telerik.Web.UI.GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        empid = empid + "," + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Emp_Code"));
                    }
                }
            }

            if (empid == "0")
            {
                str = "Report.aspx?EmpPassID=&UserID=" + Session["EmpCode"].ToString() + "&Month=" + Session["ROWID"].ToString() + "&stdatemonth=" + Session["PayStartDay"].ToString() + "&endatemonth=" + Session["PayEndDay"].ToString() + "&stdatesubmonth=" + Session["PaySubStartDay"].ToString() + "&endatesubmonth=" + Session["PaySubEndDay"].ToString() + "&monthidintbl=" + Session["ROWID"].ToString() + "&Year=" + Session["ROWYEAR"].ToString() + "&company_id=" + Session["Compid"].ToString();
            }
            else
            {
                str = "Report.aspx?EmpPassID=" + empid + "&UserID=" + Session["EmpCode"].ToString() + "&Month=" + Session["ROWID"].ToString() + "&stdatemonth=" + Session["PayStartDay"].ToString() + "&endatemonth=" + Session["PayEndDay"].ToString() + "&stdatesubmonth=" + Session["PaySubStartDay"].ToString() + "&endatesubmonth=" + Session["PaySubEndDay"].ToString() + "&monthidintbl=" + Session["ROWID"].ToString() + "&Year=" + Session["ROWYEAR"].ToString() + "&company_id=" + Session["Compid"].ToString();
            }

            //if (empid == "0")
            //{
            //    str = "payrolldetailreport.aspx?EmpPassID=&UserID=" + Session["EmpCode"].ToString() + "&Month=" + Session["ROWID"].ToString() + "&stdatemonth=" + Session["PayStartDay"].ToString() + "&endatemonth=" + Session["PayEndDay"].ToString() + "&stdatesubmonth=" + Session["PaySubStartDay"].ToString() + "&endatesubmonth=" + Session["PaySubEndDay"].ToString() + "&monthidintbl=" + Session["ROWID"].ToString() + "&Year=" + Session["ROWYEAR"].ToString() + "&company_id=" + Session["Compid"].ToString();
            //}
            //else
            //{
            //    str = "payrolldetailreport.aspx?EmpPassID=" + empid + "&UserID=" + Session["EmpCode"].ToString() + "&Month=" + Session["ROWID"].ToString() + "&stdatemonth=" + Session["PayStartDay"].ToString() + "&endatemonth=" + Session["PayEndDay"].ToString() + "&stdatesubmonth=" + Session["PaySubStartDay"].ToString() + "&endatesubmonth=" + Session["PaySubEndDay"].ToString() + "&monthidintbl=" + Session["ROWID"].ToString() + "&Year=" + Session["ROWYEAR"].ToString() + "&company_id=" + Session["Compid"].ToString();
            //}
            string popupScript = "<script language='javascript'>" + "window.open('', '" + str + "', " + "'width=1200, height=3000, menubar=yes, resizable=yes')" + "</script>";
            HttpContext.Current.Response.Write("<SCRIPT language='Javascript'>window.open('" + str + "');</SCRIPT>");


            //return str;
        }

        //Toolbar
        #region Toolbar and Exporting

        //column to hide wile export
        protected void HideGridColumnseExport()
        {
            RadGrid1.MasterTableView.GetColumn("TemplateColumn").Visible = false;
            RadGrid1.MasterTableView.GetColumn("GridClientSelectColumn").Visible = false;
            RadGrid1.MasterTableView.GetColumn("Image").Visible = false;
        }

        //protected void tbRecord_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        //{
        //    if (e.Item.Text == "Excel" || e.Item.Text == "Word")
        //    {
        //        HideGridColumnseExport();
        //    }

        //    GridSettingsPersister obj2 = new GridSettingsPersister();
        //    obj2.ToolbarButtonClick(e, RadGrid1, Utility.ToString(Session["Username"]));

        //    if (e.Item.Text == "Graph")
        //    {
        //        //var scheme = Request.Url.Scheme; // will get http, https, etc.
        //        //var host = Request.Url.Host; // will get www.mywebsite.com
        //        //var port = Request.Url.Port; // will get the port
        //        //var path = Request.Url.AbsolutePath; //

        //        string strServer = System.Configuration.ConfigurationManager.AppSettings["DB_SERVER"];
        //        string strServerUser = System.Configuration.ConfigurationManager.AppSettings["DB_UID"];
        //        string strDB = System.Configuration.ConfigurationManager.AppSettings["DB_NAME"];
        //        string strDB_PWD = System.Configuration.ConfigurationManager.AppSettings["DB_PWD"];

        //        String pServer = Server.UrlEncode(strServer);
        //        String pServerUser = Server.UrlEncode(strServerUser);
        //        String pDB = Server.UrlEncode(strDB);
        //        String pDB_PWD = Server.UrlEncode(strDB_PWD);

        //        string strUserID = HttpUtility.UrlEncode(Session["EmpCode"].ToString());
        //        string strMonth = HttpUtility.UrlEncode(cmbMonth.SelectedValue.ToString());
        //        string strstdatemonth = HttpUtility.UrlEncode(Session["PayStartDay"].ToString());
        //        string strendatemonth = HttpUtility.UrlEncode(Session["PayEndDay"].ToString());
        //        string strstdatesubmonth = HttpUtility.UrlEncode(Session["PaySubStartDay"].ToString());
        //        string strendatesubmonth = HttpUtility.UrlEncode(Session["PaySubEndDay"].ToString());
        //        string strmonthidintbl = HttpUtility.UrlEncode(cmbMonth.SelectedValue);
        //        string strYear = HttpUtility.UrlEncode(cmbYear.SelectedValue.ToString());
        //        string strcompany_id = HttpUtility.UrlEncode(Session["Compid"].ToString());
        //        string strMonthName = HttpUtility.UrlEncode(cmbMonth.SelectedItem.ToString());

        //        //string url = scheme + "://" + host + ":" + port + "/Payroll/PayrollChart.aspx?UserID=" + Session["EmpCode"].ToString() + "&Month=" + cmbMonth.SelectedValue.ToString() + "&stdatemonth=" + Session["PayStartDay"].ToString() + "&endatemonth=" + Session["PayEndDay"].ToString() + "&stdatesubmonth=" + Session["PaySubStartDay"].ToString() + "&endatesubmonth=" + Session["PaySubEndDay"].ToString() + "&monthidintbl=" + Utility.ToInteger(cmbMonth.SelectedValue) + "&Year=" + cmbYear.SelectedValue.ToString() + "&company_id=" + Session["Compid"].ToString();
        //        string url = "http://localhost/DashBoardPayroll/DashBoardPayroll.xbap?Server=" + pServer + "&ServerUserID=" + pServerUser + "&DBName=" + pDB + "&DBPassword=" + pDB_PWD + "&UserID=" + strUserID + "&Month=" + strMonth + "&stdatemonth=" + strstdatemonth + "&endatemonth=" + strendatemonth + "&stdatesubmonth=" + strstdatesubmonth + "&endatesubmonth=" + strendatesubmonth + "&monthidintbl=" + strmonthidintbl + "&Year=" + strYear + "&company_id=" + strcompany_id + "&Monthname=" + strMonthName;
        //        ClientScript.RegisterStartupScript(this.GetType(), "OpenWin", "<script>openNewWin('" + url + "')</script>"); 
        //    }
        //}

        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }

        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("103", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);
        }

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            //GridSettingsPersister objCount = new GridSettingsPersister();
            //objCount.RowCount(e, tbRecord);
        }
        #endregion
        //Toolbar End


        #region Progressbar
        private void UpdateProgressContext(string emp, int count)
        {
            const int total = 100;

            RadProgressContext progress = RadProgressContext.Current;
            progress.Speed = "N/A";

            for (int i = 0; i < total; i++)
            {
                progress.PrimaryTotal = 1;
                progress.PrimaryValue = 1;
                progress.PrimaryPercent = 100;

                progress.SecondaryTotal = total;
                progress.SecondaryValue = i;
                progress.SecondaryPercent = i;


                progress.CurrentOperationText = "Processing Payroll :" + emp.ToString();

                if (!Response.IsClientConnected)
                {
                    //Cancel button was clicked or the browser was closed, so stop processing
                    break;
                }

                progress.TimeEstimated = (total - i) * 100;
                //Stall the current thread for 0.1 seconds
                System.Threading.Thread.Sleep(200);
            }
        }
        #endregion
    }

    public class NameList
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

    }
}
