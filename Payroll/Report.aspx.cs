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
using System.Collections.Generic;


using System.Net;

namespace SMEPayroll.Payroll
{
    public partial class Report : System.Web.UI.Page
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
        DataTable dt;
        decimal grossPay;
        decimal deduction;
        decimal Netpay;
        decimal totalAddition;
        protected void Page_Load(object sender, EventArgs e)
        {
            /* To disable Grid filtering options  */
            Telerik.Web.UI.GridFilterMenu menu = RadGrid1.FilterMenu;
            int i = 0;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(SubmitPayroll));

            dataexportmessage.Visible = false;
            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }
            grossPay =0;
            //btnsubapprove.Attributes.Add("onclick", "this.disabled=true;" );
            //Page.ClientScript.GetPostBackEventReference(btnsubapprove, "");

            if (chkList.SelectedValue == "Detailed")
            {

                RadGrid1.Visible = true;
                RadGrid2.Visible = false;
                RadGrid3.Visible = false;
                RadGrid5.Visible = false;

            }

            if (chkList.SelectedValue == "Summary")
            {
                RadGrid1.Visible = false;
                RadGrid2.Visible = true;
                RadGrid3.Visible = false;
                RadGrid5.Visible = false;
            }

            if (chkList.SelectedValue == "Recon")
            {
                RadGrid1.Visible = false;
                RadGrid2.Visible = false;
                RadGrid3.Visible = true;
                RadGrid5.Visible = false;
            }

            if (chkList.SelectedValue == "Company")
            {
                RadGrid1.Visible = false;
                RadGrid2.Visible = false;
                RadGrid3.Visible = false;
                RadGrid5.Visible = true;
            }


            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource1.ConnectionString = Constants.CONNECTION_STRING;
            SqlDataSource3.ConnectionString = Constants.CONNECTION_STRING;
            SqlDataSource2.ConnectionString = Constants.CONNECTION_STRING;

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

                if (Session["GroupName"].ToString().ToUpper() != "SUPER ADMIN")
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
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();

            }
            RadGrid1.PreRender+=new EventHandler(RadGrid1_PreRender);
            RadGrid1.DataBound += new EventHandler(RadGrid1_DataBound);

            //Check for WorkFlow number 2
            if (strWF == "2" && Session["PayrollWF"]!=null)
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

        }

        void RadGrid1_DataBound(object sender, EventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");

            //if (RadGrid1.Rows.Count > 0)
            //{
            //    //int TotalRows = gridPayDetailReport.Rows.Count;
            //    //int TotalCol = gridPayDetailReport.Rows[0].Cells.Count;
            //    //int FixedCol = 4;
            //    //int ComputedCol = FixedCol;

            //    //gridPayDetailReport.FooterRow.Cells[FixedCol - 1].Text = "Total : ";

            //    //for (int i = 4; i < columncount; i++)
            //    //{
            //    //    double sum = 0.000;

            //    //    for (int j = 0; j < TotalRows; j++)
            //    //    {
            //    //        if (gridPayDetailReport.Rows[j].Cells[i].Text != "")
            //    //        {
            //    //            sum += gridPayDetailReport.Rows[j].Cells[i].Text != "&nbsp;" ? double.Parse(gridPayDetailReport.Rows[j].Cells[i].Text) : 0.000;
            //    //        }
            //    //    }
            //    //    RadGrid1.FooterRow.Cells[3].Text = "1000";//sum.ToString("#.00");
                
               
            //}


        }

        protected void deptID_databound(object sender, EventArgs e)
        {
            deptID.Items.Insert(0, new ListItem("ALL", "-1"));
        }
        protected void Page_PreRender(Object sender, EventArgs E)  
        {
          
                    if (chkList.SelectedValue == "Detailed")
                    {

                            if (RadGrid1.MasterTableView.Items.Count > 0)
                            {
                                tbRecord.Visible = true;
                                TabId.Visible = true;

                                btndetail.Visible = true;
                                btnPayrollDetail.Visible = true;
                                btnPayroll.Visible = true;
                                RadGrid1.PagerStyle.Visible = true;
                            }
                            else
                            {
                                tbRecord.Visible = false;
                                TabId.Visible = false;


                                btndetail.Visible = false;
                                btnPayrollDetail.Visible = false;
                                btnPayroll.Visible = false;
                                RadGrid1.PagerStyle.Visible = false;
                            }
                    }

                    if (chkList.SelectedValue == "Summary")
                    {
                            if (RadGrid2.MasterTableView.Items.Count > 0)
                            {
                                tbRecord.Visible = true;
                                TabId.Visible = true;

                                btndetail.Visible = true;
                                btnPayrollDetail.Visible = true;
                                btnPayroll.Visible = true;
                                RadGrid2.PagerStyle.Visible = true;
                            }
                            else
                            {
                                tbRecord.Visible = false;
                                TabId.Visible = false;


                                btndetail.Visible = false;
                                btnPayrollDetail.Visible = false;
                                btnPayroll.Visible = false;
                                RadGrid2.PagerStyle.Visible = false;
                            }
                    }

                    if (chkList.SelectedValue == "Recon")
                    {
                            if (RadGrid3.MasterTableView.Items.Count > 0)
                            {
                                tbRecord.Visible = true;
                                TabId.Visible = true;

                                btndetail.Visible = true;
                                btnPayrollDetail.Visible = true;
                                btnPayroll.Visible = true;
                                RadGrid3.PagerStyle.Visible = true;
                            }
                            else
                            {
                                tbRecord.Visible = false;
                                TabId.Visible = false;


                                btndetail.Visible = false;
                                btnPayrollDetail.Visible = false;
                                btnPayroll.Visible = false;
                                RadGrid3.PagerStyle.Visible = false;
                            }
                    }

                    if (chkList.SelectedValue == "Company")
                    {
                        if (RadGrid5.MasterTableView.Items.Count > 0)
                        {
                            tbRecord.Visible = true;
                            TabId.Visible = true;

                            btndetail.Visible = true;
                            btnPayrollDetail.Visible = true;
                            btnPayroll.Visible = true;
                            RadGrid5.PagerStyle.Visible = true;
                        }
                        else
                        {
                            tbRecord.Visible = false;
                            TabId.Visible = false;


                            btndetail.Visible = false;
                            btnPayrollDetail.Visible = false;
                            btnPayroll.Visible = false;
                            RadGrid5.PagerStyle.Visible = false;
                        }
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

        protected void btnsubapprove_click(object sender, EventArgs e)
        {
            btnsubapprove.Enabled = false;
            
            bool blnisrecsel = false;
            if (Session["SubmitClickCount"] == null)
            {
                Session["SubmitClickCount"] = "0";
            }
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

                            string status = "P";
                            int i = 0;
                            SqlParameter[] parms = new SqlParameter[54];
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
                            parms[i++] = new SqlParameter("@daysworkedrate", Utility.ToDouble(daysworkedrate));

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
                                        param1[3] = new SqlParameter("@UserID", Utility.ToInteger(empid));
                                        param1[4] = new SqlParameter("@Status", "P");
                                        ds = DataAccess.FetchRS(CommandType.StoredProcedure, "sp_ApprovePayRoll", param1);

                                        if (ds.Tables.Count > 0)
                                        {
                                            foreach (DataRow dr in ds.Tables[0].Rows)
                                            {
                                                //exec sp_payroll_Update @trx_id=4671,@trxdate=N'30/01/2012 11:01:01',@status=N'A'
                                                SqlParameter[] param2 = new SqlParameter[3];
                                                int c = 0;
                                                param2[c++] = new SqlParameter("@trx_id", Utility.ToInteger(dr["trx_id"].ToString()));
                                                param2[c++] = new SqlParameter("@trxdate", String.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now));
                                               // param2[c++] = new SqlParameter("@trxdate", Utility.ToString(System.DateTime.Now.Month + "/" + System.DateTime.Now.Day + "/" + System.DateTime.Now.Year));
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
                                        param3[3] = new SqlParameter("@UserID", Utility.ToInteger(empid));
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
                                    ErrMsg = "<font color = 'Red'>Unable to update the status.Please Try again!</font>";
                                }
                            }
                        }

                    }
                }
                Session["SubmitClickCount"] = "0";
                sendemail();
                RadGrid1.DataBind();

                if (blnisrecsel == false)
                {
                    ShowMessageBox("Please Select Employees to Submit Payroll");
                }

            }
        }

        

        protected void sendemail()
        {
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

                SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(Utility.ToInteger(Session["Compid"]));

                oANBMailer.Subject = subject;
                oANBMailer.MailBody = body;
                oANBMailer.From = from;
                oANBMailer.To = to;
                oANBMailer.Cc = cc;

                try
                {
                    string sRetVal = oANBMailer.SendMail();
                    if (sRetVal == "")
                        //Response.Write("<Font color=green size=3> An email has been sent to " + to + "</Font> <BR />");
                        lblLoading.Text = "An email has been sent to " + to + "";

                    else
                        // Response.Write("<Font color=red size=3> An error occurred: Details are as follows <BR />" + sRetVal + "</Font>");
                        lblLoading.Text = "An error occurred while Email";

                }
                catch (Exception ex)
                {
                    string errMsg = ex.Message;
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

        [AjaxPro.AjaxMethod]
        public string btndetail_Click(int monthid, int yearid)
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
                //Session["EmpPassID"] = dr["emp_code"].ToString();
                str = "paydetailreport.aspx?UserID=" + Session["EmpCode"].ToString() + "&Month=" + dr["Month"].ToString() + "&stdatemonth=" + Session["PayStartDay"].ToString() + "&endatemonth=" + Session["PayEndDay"].ToString() + "&stdatesubmonth=" + Session["PaySubStartDay"].ToString() + "&endatesubmonth=" + Session["PaySubEndDay"].ToString() + "&monthidintbl=" + monthid.ToString() + "&Year=" + yearid.ToString() + "&company_id=" + Session["Compid"].ToString() + "&EmpPassID=" + Session["EmpPassID"];
            }
            string popupScript = "<script language='javascript'>" + "window.open('', '" + str + "', " + "'width=1000, height=1000, menubar=yes, resizable=yes')" + "</script>";
            return str;
        }

        //[AjaxPro.AjaxMethod]
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
                str = "payrolldetailreport.aspx?EmpPassID=&UserID=" + Session["EmpCode"].ToString() + "&Month=" + Session["ROWID"].ToString() + "&stdatemonth=" + Session["PayStartDay"].ToString() + "&endatemonth=" + Session["PayEndDay"].ToString() + "&stdatesubmonth=" + Session["PaySubStartDay"].ToString() + "&endatesubmonth=" + Session["PaySubEndDay"].ToString() + "&monthidintbl=" + Session["ROWID"].ToString() + "&Year=" + Session["ROWYEAR"].ToString() + "&company_id=" + Session["Compid"].ToString();
            }
            else
            {
                str = "payrolldetailreport.aspx?EmpPassID=" + empid + "&UserID=" + Session["EmpCode"].ToString() + "&Month=" + Session["ROWID"].ToString() + "&stdatemonth=" + Session["PayStartDay"].ToString() + "&endatemonth=" + Session["PayEndDay"].ToString() + "&stdatesubmonth=" + Session["PaySubStartDay"].ToString() + "&endatesubmonth=" + Session["PaySubEndDay"].ToString() + "&monthidintbl=" + Session["ROWID"].ToString() + "&Year=" + Session["ROWYEAR"].ToString() + "&company_id=" + Session["Compid"].ToString();
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
            if (e.Item is GridFooterItem)
            {
                GridFooterItem footer = (GridFooterItem)e.Item;
                //(footer["Template1"].FindControl("TextBox2") as TextBox).Text = sum.ToString();
                //clientID = (footer["Template1"].FindControl("TextBox2") as TextBox).ClientID;
                string GrosppPay = "";
                GrosppPay = "<table valign=\"top\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\"  width=\"100%\"><tr><td>Gross Pay :</td><td>" + grossPay.ToString() + "</td></tr><tr><td>Total Additions:</td><td>" + totalAddition + "</td></tr></table>";
                footer["TotalAdditionsWONH"].Text = GrosppPay;// "Gross Pay :" + grossPay.ToString();

                GrosppPay = "<table valign=\"top\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\"  width=\"100%\"><tr><td>Total Deduction :</td><td>" + deduction.ToString() + "</td></tr></table>";
                footer["TotalDeductions"].Text = GrosppPay;// "Gross Pay :" + grossPay.ToString();

                GrosppPay = "<table valign=\"top\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\"  width=\"100%\"><tr><td>Net Pay :</td><td>" + Netpay.ToString() + "</td></tr></table>";
                footer["Netpay"].Text = GrosppPay;// "Gross Pay :" + grossPay.ToString();

                //strEarnings = "<table valign=\"top\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\"  width=\"100%\"><tr><td>RegularPay</td><td>" + item["Basic"].Text.ToString() + "</td></tr>";
                //strEarnings = strEarnings + "<tr><td>Employer CPF</td><td>" + item["employercpfamt"].Text.ToString() + " </td></tr><tr><td>Gross Pay</td><td>" + item["GrossWithAddition"].Text.ToString() + "</td></tr>";
                //strours = "<table valign=\"top\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\" width=\"100%\"><tr><td>" + "-" + "</td></tr><tr><td>-</td></tr>";
            }

            double netpay = 0;
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem item = e.Item as GridDataItem;
                System.Web.UI.WebControls.HyperLink Img = (System.Web.UI.WebControls.HyperLink)item.FindControl("Image3");
                string strMediumUrl = e.Item.Cells[37].Text;
                string strmsg = "javascript:ShowInsert('" + strMediumUrl + "');";
                Img.Attributes.Add("onclick", strmsg);
                if (e.Item.Cells[26].Text != "&nbsp;")
                {
                    netpay = Utility.ToDouble(e.Item.Cells[27].Text);
                }
                if (netpay < 0)
                {
                    e.Item.Cells[26].BackColor = Color.Red;
                }
                if (e.Item.Cells[50].Text.ToString() == "0")
                {
                    ((CheckBox)item["GridClientSelectColumn"].Controls[0]).Visible = false;
                }
                //exec sp_GetEmployeePayDetails @emp_code=N'12',@Year=N'2011',@Month=N'1',@stdatesubmonth=N'1',@endatesubmonth=N'31',
                //@Day_Work=N'0',@OT1=N'406.19',@OT2=N'236.94',@BasicDayRate=N'0',@OT1Hrs=N'16.00',@OT2Hrs=N'7.00',@OT1Rate=N'25.38691999995',
                //@OT2Rate=N'33.8492266666',@empcpfamount=N'1203',@ordwages=N'4125.13',@addwages=N'1890',@cpfrate=N'20',@fundname=N'CDAC',
                //@fundamount=N'1',@fundgrossamount=N'6015.13',@nhhrs=N'0',@hourlyrate=N'16.9246133333',@daysworkedrate=N'0'


                string monthstr = "";
                DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + Request.QueryString["Month"]);
                foreach (DataRow drnew in drResults)
                {
                    monthstr = drnew["Month"].ToString();
                }
                int Month = Convert.ToInt32(monthstr);


                //SDL calcuations

                //    string strprdate = e.Row.Cells[21].Text;
                string sdfRequired = e.Item.Cells[51].Text;

                double sdlamount = Utility.ToDouble(e.Item.Cells[41].Text);
                //if (sdfRequired.Trim() == "2")
                //{
                //    if (sdlamount > 0)
                //    {
                //        sdlamount = Utility.ToDouble(e.Item.Cells[64].Text);
                //    }
                //    else
                //    {
                //        sdlamount = Utility.ToDouble(e.Item.Cells[38].Text);
                //    }
                //}

                //    DateTime dbPrdate1 = new DateTime();
                //    if (strprdate != "")
                //    {
                //        //dbPrdate1 = Convert.ToDateTime(strprdate);
                //    }
                //    /********************************************************************************************/
                double sdlfundtamt = 0.00;

                //if (sdlamount != 0)
                //{
                    int intmonth = Utility.ToInteger(Request.QueryString["monthidintbl"]) - 1;
                    double fundgrossamount = Utility.ToDouble(sdlamount);
                    //string sSql = "Select dbo.fn_getSDLAmount(" + fundgrossamount.ToString() + ")";

                    //////////////////////////////////////////Get Payroll Month
                    string sqlmonth = "SELECT Month FROM payrollmonthlydetail WHERE ROWID=" + intmonth;
                    SqlDataReader dr9;
                    dr9 = DataAccess.ExecuteReader(CommandType.Text, sqlmonth, null);
                    int intactualmonth = 1;
                    while (dr9.Read())
                    {
                        intactualmonth = Convert.ToInt32(dr9[0].ToString());
                    }
                    string sSql = "Select dbo.fn_getSDLAmount(" + fundgrossamount.ToString() + ")";
                    // string sSql = "Select dbo.fn_getSDLAmount(" + fundgrossamount.ToString() + "," + intactualmonth + "," + Year + ")";
                    ////////////////////////////////////////
                    string strSqlDed = "; Select isnull(sum(SDL),0) SDLAmt From prepare_payroll_detail pd Inner Join prepare_payroll_hdr ph on pd.trx_id = ph.trx_id Where ([Status]='A' Or [Status]='G' Or [Status]='P') And Emp_ID=" + e.Item.Cells[4].Text + " And ph.end_period=(SELECT PaySubEndDate FROM   PayrollMonthlyDetail  WHERE  ROWID = (" + intmonth.ToString() + "))";
                    string sdlopt = "; select  [sdf_required]  from employee where emp_code=" + e.Item.Cells[4].Text + "";
                    DataSet dsSDL = new DataSet();
                    dsSDL = DataAccess.FetchRS(CommandType.Text, (sSql + strSqlDed + sdlopt), null);

                    if (dsSDL != null)
                    {
                        
                        if (dsSDL.Tables[0].Rows.Count > 0)
                        {
                            if (Utility.ToDouble(dsSDL.Tables[0].Rows[0][0].ToString()) <= 0)
                            {
                                //if (intmonth == 12)
                                //{
                                //  trsdl.Attributes.Add("style", "display:block");
                                //}
                                //else
                                //{
                                //  trsdl.Attributes.Add("style", "display:none");
                                //}
                            }
                            else
                            {
                                //trsdl.Attributes.Add("style", "display:block");
                                if (Utility.ToInteger(Session["PaySubStartDay"].ToString()) > 1)
                                {
                                    double sdlAmount = Utility.ToDouble(Utility.ToDouble(dsSDL.Tables[0].Rows[0][0].ToString()) - Utility.ToDouble(dsSDL.Tables[1].Rows[0][0].ToString()));
                                    if (sdlAmount < 0)
                                    {
                                        sdlfundtamt = 0.00;
                                    }
                                    else
                                    {
                                        sdlfundtamt = Convert.ToDouble(sdlAmount.ToString());
                                    }
                                }
                                else
                                {
                                    sdlfundtamt = Convert.ToDouble(dsSDL.Tables[0].Rows[0][0].ToString());
                                }
                                //If PR Date is >1 then overwrite SDL with old salary ...
                                //if (dtPRdate1 != null)
                                //{
                                //    //stmonth
                                //    // dtTerm = DateTime.Parse(dr["Term_Date"].ToString(), format);
                                //    //lblTermDate.Text = dtTerm.ToString("dd/MM/yyyy");
                                //    //Check for Month processing salary
                                //    string stmonth = Utility.ToString(Request.QueryString["stdatesubmonth"]);
                                //    DateTime dt1 = DateTime.Parse(stmonth, format);
                                //    if (dtPRdate1.Date.Day > 1 && dtPRdate1.Date.Month == dt1.Month && dtPRdate1.Date.Year == dt1.Year)
                                //    {
                                //        e.Row.Cells[1].Text = dsSDL.Tables[1].Rows[0][0].ToString();
                                //    }
                                //}
                                //}
                            }
                        }
                        //if (dsSDL.Tables[2].Rows.Count > 0)
                        //{
                        //   if(Convert.ToDouble(dsSDL.Tables[2].Rows[0][0].ToString())==1)
                        //   {
                        //       sdlfundtamt = 0.00;
                        //   }
                        //}
                    }


                    string sSQL1 = "sp_GetEmployeePayDetails";
                    SqlParameter[] parms1 = new SqlParameter[24];
                    int j = 0;
                    parms1[j++] = new SqlParameter("@emp_code", item["Emp_Code"].Text);
                    parms1[j++] = new SqlParameter("@Year", Utility.ToString(Request.QueryString["Year"]));
                    parms1[j++] = new SqlParameter("@Month", Utility.ToString(Month));
                    parms1[j++] = new SqlParameter("@stdatesubmonth", Session["PaySubStartDay"].ToString());
                    parms1[j++] = new SqlParameter("@endatesubmonth", Session["PaySubEndDay"].ToString());
                    if (Utility.ToString(item["Days_Work"].Text) == "")
                    {
                        parms1[j++] = new SqlParameter("@Day_Work", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@Day_Work", Utility.ToString(item["Days_Work"].Text));
                    }
                    if (Utility.ToString(item["OT1"].Text) == "")
                    {
                        parms1[j++] = new SqlParameter("@OT1", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@OT1", Utility.ToString(item["OT1"].Text));
                    }
                    if (Utility.ToString(item["OT2"].Text) == "")
                    {
                        parms1[j++] = new SqlParameter("@OT2", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@OT2", Utility.ToString(item["OT2"].Text));
                    }

                    if (Utility.ToString(item["Daily_Rate"].Text) == "") //Daily_Rate
                    {
                        parms1[j++] = new SqlParameter("@BasicDayRate", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@BasicDayRate", Utility.ToString(item["Daily_Rate"].Text));
                    }
                    if (Utility.ToString(item["OT1Hrs"].Text) == "")
                    {
                        parms1[j++] = new SqlParameter("@OT1Hrs", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@OT1Hrs", Utility.ToString(item["OT1Hrs"].Text));
                    }
                    if (Utility.ToString(item["OT2Hrs"].Text) == "")
                    {
                        parms1[j++] = new SqlParameter("@OT2Hrs", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@OT2Hrs", Utility.ToString(item["OT2Hrs"].Text));
                    }
                    if (Utility.ToString(item["OT1Rate"].Text) == "")
                    {
                        parms1[j++] = new SqlParameter("@OT1Rate", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@OT1Rate", Utility.ToString(item["OT1Rate"].Text));
                    }

                    if (Utility.ToString(item["OT2Rate"].Text) == "")
                    {
                        parms1[j++] = new SqlParameter("@OT2Rate", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@OT2Rate", Utility.ToString(item["OT2Rate"].Text));
                    }

                    if (Utility.ToString(item["EmployeeCPFAmt"].Text) == "")
                    {
                        parms1[j++] = new SqlParameter("@empcpfamount", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@empcpfamount", Utility.ToString(item["EmployeeCPFAmt"].Text));
                    }

                    if (Utility.ToString(item["CPFOrdinaryCeil"].Text) == "")
                    {
                        parms1[j++] = new SqlParameter("@ordwages", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@ordwages", Utility.ToString(item["CPFOrdinaryCeil"].Text));
                    }

                    if (Utility.ToString(item["CPFAdditionNet"].Text) == "")
                    {
                        parms1[j++] = new SqlParameter("@addwages", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@addwages", Utility.ToString(item["CPFAdditionNet"].Text));
                    }

                    //HardCoded Value
                    //if (Utility.ToString(item["CPFEmployeePerc"].Text) == "")
                    //{
                    parms1[j++] = new SqlParameter("@cpfrate", 20);//Utility.ToString(item["CPFEmployeePerc"].Text));
                    //}
                    //else
                    //{
                    //    parms1[j++] = new SqlParameter("@cpfrate", 20);// new SqlParameter("@cpfrate", Utility.ToString(item["CPFEmployeePerc"].Text));
                    //}
                    if (Utility.ToString(item["FundType"].Text) == "")
                    {
                        parms1[j++] = new SqlParameter("@fundname", '-');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@fundname", Utility.ToString(item["FundType"].Text));
                    }
                    if (Utility.ToDouble(item["FundAmount"].Text) <= 0)
                    {
                        parms1[j++] = new SqlParameter("@fundamount", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@fundamount", Utility.ToString(item["FundAmount"].Text));
                    }
                    if (Utility.ToDouble(item["FundGrossAmount"].Text) <= 0)
                    {
                        parms1[j++] = new SqlParameter("@fundgrossamount", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@fundgrossamount", Utility.ToString(item["FundGrossAmount"].Text));
                    }
                    if (Utility.ToDouble(item["NHHrs"].Text) == 0)
                    {
                        parms1[j++] = new SqlParameter("@nhhrs", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@nhhrs", Utility.ToString(item["NHHrs"].Text));
                    }
                    if (Utility.ToDouble(item["Hourly_Rate"].Text) == 0)
                    {
                        parms1[j++] = new SqlParameter("@hourlyrate", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@hourlyrate", Utility.ToString(item["Hourly_Rate"].Text));
                    }
                    if (Utility.ToDouble(item["DaysWorkedRate"].Text) == 0)
                    {
                        parms1[j++] = new SqlParameter("@daysworkedrate", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@daysworkedrate", Utility.ToString(item["DaysWorkedRate"].Text));
                    }

                    parms1[j++] = new SqlParameter("@addtionprorated","1");

                

                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL1, parms1);

                    //font: 11px/11px 'Segoe UI', Tahoma;

                    string strEarnings = "";
                    string strours = "";

                    strEarnings = "<table valign=\"top\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\"  width=\"100%\"><tr><td>Regular Pay</td><td>" + item["Basic"].Text.ToString() + "</td></tr>";
                    strours = "<table valign=\"top\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\" width=\"100%\"><tr><td>" + "-" + "</td></tr><tr><td>-</td></tr>";
                    //GrossWithAddition

                    grossPay = grossPay + Convert.ToDecimal(item["CPFGross1"].Text.ToString());
                    deduction = deduction + Convert.ToDecimal(item["TotalDeductions"].Text.ToString());
                    Netpay = Netpay + Convert.ToDecimal(item["Netpay"].Text.ToString());
                    totalAddition = totalAddition + Convert.ToDecimal(item["TotalAdditionsWONH"].Text.ToString()) + Convert.ToDecimal(item["NH"].Text.ToString()) + Convert.ToDecimal(item["OT1"].Text.ToString()) + Convert.ToDecimal(item["OT2"].Text.ToString());

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string strValue = dr[0].ToString().Trim();
                        strValue = strValue.Remove(0, strValue.Length - 4);
                        if (strValue == "OT1:")
                        {
                            strValue = "OT1";
                            strours = strours + "<tr><td>" + item["OT1Hrs"].Text + "</td></tr>";
                            strEarnings = strEarnings + "<tr><td>" + "OT1 / " + item["OT1Rate"].Text.ToString() + "</td><td>" + dr[1].ToString() + "</td></tr>";
                        }
                        else if (strValue == "OT2:")
                        {
                            strValue = "OT2";
                            strours = strours + "<tr><td>" + item["OT2Hrs"].Text + "</td></tr>";
                            strEarnings = strEarnings + "<tr><td>" + "OT2 / " + item["OT2Rate"].Text.ToString() + "</td><td>" + dr[1].ToString() + "</td></tr>";
                        }
                        else if (strValue == "Rate")
                        {


                        }
                        else
                        {
                            strours = strours + "<tr><td>" + "-" + "</td></tr>";
                            strEarnings = strEarnings + "<tr><td>" + dr[0].ToString() + "</td><td>" + dr[1].ToString() + "</td></tr>";
                        }
                    }
                    strEarnings = strEarnings + "<tr><td>Gross Pay</td><td>" + item["CPFGross1"].Text.ToString() + " </td></tr>";
                    strEarnings = strEarnings + "<tr><td>Employer CPF</td><td>" + item["employercpfamt"].Text.ToString() + " </td></tr>";
                    strEarnings = strEarnings + "<tr><td>SDL</td><td>" + sdlfundtamt.ToString() + " </td></tr>";
                    strEarnings = strEarnings + "</table>";
                    strours = strours + "</table>";

                    if (strEarnings.Length > 0)
                    {
                        item["TotalAdditionsWONH"].Text = strEarnings;// "Regular Pay     " + item["Basic"].Text;
                    }
                    if (strours.Length > 0)
                    {
                        item["OT1Hrs"].Text = strours;// "Regular Pay    " + item["Basic"].Text;
                    }

                    string strDeductions = "";
                    strDeductions = "<table valign=\"top\" style=\"font:10px; font-family:Courier New\" Height=\"100%\" width=\"100%\"><tr><td>Employee Cpf " + "</td><td> " + item["employeecpfamt"].Text.ToString() + " </td></tr>";
                    //strDeductions = "<tr><td>Unpaid " + "</td><td> " + item["totalunpaid"].Text.ToString() + " </td></tr>";
                    //add unpaid leave column in GridView 1



                    if (item["fundtype"].Text != "")
                    {
                        strDeductions = strDeductions + "<tr><td>" + item["fundtype"].Text + "</td><td>  " + item["fundamount"].Text + "  </td></tr>";
                    }
                    if (item["totalunpaid"].Text != "0.00")
                    {
                        strDeductions = strDeductions + "<tr><td>Unpaid Leave</td><td>  " + item["totalunpaid"].Text + "  </td></tr>";
                    }
                    
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        if (dr[0].ToString().Contains("fund") || dr[0].ToString().Contains("CDAC") || dr[0].ToString().Contains("SINDA") || dr[0].ToString().Contains("MBMF") || dr[0].ToString().Contains("ECF") || dr[0].ToString().Contains("CCHEST") || dr[0].ToString().Contains("CPF Rate") || dr[0].ToString().Contains("CPF"))
                        {

                        }
                        else
                        {
                            strDeductions = strDeductions + "<tr><td>" + dr[0].ToString() + "</td><td> " + dr[1].ToString() + " </td></tr>";
                        }
                    }

                    strDeductions = strDeductions + "</table>";
                    if (strDeductions.Length > 0)
                    {
                        item["TotalDeductions"].Text = strDeductions;
                    }

                    string sql = "Select payrolltype from employee Where emp_code =" + Convert.ToInt32(item["Emp_Code"].Text.ToString());

                    SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                    while (dr1.Read())
                    {
                        if (Utility.ToString(dr1.GetValue(0)) == "1")
                        {
                            item["Basic"].Text = " Monthly <br/>" + item["Basic"].Text.ToString();
                        }
                        else
                        {
                            item["Basic"].Text = " Bi-Monthly <br/>" + item["Basic"].Text.ToString();
                        }
                    }


              //  }
            }
        }


        protected void RadGrid5_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem item = e.Item as GridDataItem;

                //Get All Employyes whose Department is same and call SP and get data for Additions and deductions as well.........
                DataSet dstemp = new DataSet();
                DataTable dttemp1 = new DataTable();
                dttemp1 = dt.Copy();

                dstemp.Tables.Add(dttemp1);


                string deptName = "select DeptName from department where Company_id =" + Utility.ToInteger(Session["compid"]);
                SqlDataReader dr;
                dr = DataAccess.ExecuteReader(CommandType.Text, deptName, null);

                DataSet dsfinal = new DataSet();
                dsfinal = dstemp.Clone();
                DataSet department = new DataSet();

                string monthstr = "";
                DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + Request.QueryString["Month"]);
                foreach (DataRow drnew in drResults)
                {
                    monthstr = drnew["Month"].ToString();
                }
                int Month = Convert.ToInt32(monthstr);

                string dtStartDate;
                string dtEndDate;

                dtStartDate = Convert.ToString(Month.ToString() + "/" + Session["PaySubStartDay"].ToString() + "/" + Utility.ToString(Request.QueryString["Year"]));
                dtEndDate = Convert.ToString(Month.ToString() + "/" + Session["PaySubEndDay"].ToString() + "/" + Utility.ToString(Request.QueryString["Year"]));

                // foreach (DataRow drdata in dstemp.Tables[0].Rows)
                //{
                //    days = days + Convert.ToDouble(drdata["Days_Work"].ToString());
                //    hours = hours + Convert.ToDouble(drdata["OT1Hrs"].ToString());
                //    hours1 = hours1 + Convert.ToDouble(drdata["OT2Hrs"].ToString());
                //    regPay = regPay + Convert.ToDouble(drdata["Basic"].ToString());
                //    employerCpf = employerCpf + Convert.ToDouble(drdata["employercpfamt"].ToString());
                //    employeeCpf = employeeCpf + Convert.ToDouble(drdata["employeecpfamt"].ToString());
                //    netPay1 = netPay1 + Convert.ToDouble(drdata["netpay"].ToString());
                //    basicPay = basicPay + Convert.ToDouble(drdata["Basic"].ToString());                
                //}

                DataSet ds = new DataSet();
                foreach (DataRow drdata in dstemp.Tables[0].Rows)
                {
                    string sSQL1 = "sp_GetEmployeePayDetails_New";
                    SqlParameter[] parms1 = new SqlParameter[23];

                    int j = 0;
                    parms1[j++] = new SqlParameter("@emp_code", drdata["Emp_Code"].ToString());
                    parms1[j++] = new SqlParameter("@Year", Utility.ToString(Request.QueryString["Year"]));
                    parms1[j++] = new SqlParameter("@Month", Utility.ToString(Month));
                    parms1[j++] = new SqlParameter("@stdatesubmonth", Session["PaySubStartDay"].ToString());
                    parms1[j++] = new SqlParameter("@endatesubmonth", Session["PaySubEndDay"].ToString());
                    if (Utility.ToString(drdata["Days_Work"].ToString()) == "")
                    {
                        parms1[j++] = new SqlParameter("@Day_Work", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@Day_Work", Utility.ToString(drdata["Days_Work"].ToString()));
                    }
                    if (Utility.ToString(drdata["OT1"].ToString()) == "")
                    {
                        parms1[j++] = new SqlParameter("@OT1", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@OT1", Utility.ToString(drdata["OT1"].ToString()));
                    }
                    if (Utility.ToString(drdata["OT2"].ToString()) == "")
                    {
                        parms1[j++] = new SqlParameter("@OT2", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@OT2", Utility.ToString(drdata["OT2"].ToString()));
                    }

                    if (Utility.ToString(drdata["Daily_Rate"].ToString()) == "") //Daily_Rate
                    {
                        parms1[j++] = new SqlParameter("@BasicDayRate", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@BasicDayRate", Utility.ToString(drdata["Daily_Rate"].ToString()));
                    }
                    if (Utility.ToString(drdata["OT1Hrs"].ToString()) == "")
                    {
                        parms1[j++] = new SqlParameter("@OT1Hrs", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@OT1Hrs", Utility.ToString(drdata["OT1Hrs"].ToString()));
                    }
                    if (Utility.ToString(drdata["OT2Hrs"].ToString()) == "")
                    {
                        parms1[j++] = new SqlParameter("@OT2Hrs", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@OT2Hrs", Utility.ToString(drdata["OT2Hrs"].ToString()));
                    }
                    if (Utility.ToString(drdata["OT1Rate"].ToString()) == "")
                    {
                        parms1[j++] = new SqlParameter("@OT1Rate", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@OT1Rate", Utility.ToString(drdata["OT1Rate"].ToString()));
                    }

                    if (Utility.ToString(drdata["OT2Rate"].ToString()) == "")
                    {
                        parms1[j++] = new SqlParameter("@OT2Rate", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@OT2Rate", Utility.ToString(drdata["OT2Rate"].ToString()));
                    }

                    if (Utility.ToString(drdata["EmployeeCPFAmt"].ToString()) == "")
                    {
                        parms1[j++] = new SqlParameter("@empcpfamount", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@empcpfamount", Utility.ToString(drdata["EmployeeCPFAmt"].ToString()));
                    }

                    if (Utility.ToString(drdata["CPFOrdinaryCeil"].ToString()) == "")
                    {
                        parms1[j++] = new SqlParameter("@ordwages", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@ordwages", Utility.ToString(drdata["CPFOrdinaryCeil"].ToString()));
                    }

                    if (Utility.ToString(drdata["CPFAdditionNet"].ToString()) == "")
                    {
                        parms1[j++] = new SqlParameter("@addwages", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@addwages", Utility.ToString(drdata["CPFAdditionNet"].ToString()));
                    }

                    //HardCoded Value
                    //if (Utility.ToString(item["CPFEmployeePerc"].Text) == "")
                    //{
                    parms1[j++] = new SqlParameter("@cpfrate", 20);//Utility.ToString(item["CPFEmployeePerc"].Text));
                    //}
                    //else
                    //{
                    //    parms1[j++] = new SqlParameter("@cpfrate", 20);// new SqlParameter("@cpfrate", Utility.ToString(item["CPFEmployeePerc"].Text));
                    //}
                    if (Utility.ToString(drdata["FundType"].ToString()) == "")
                    {
                        parms1[j++] = new SqlParameter("@fundname", '-');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@fundname", Utility.ToString(drdata["FundType"].ToString()));
                    }
                    if (Utility.ToDouble(drdata["FundAmount"].ToString()) <= 0)
                    {
                        parms1[j++] = new SqlParameter("@fundamount", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@fundamount", Utility.ToString(drdata["FundAmount"].ToString()));
                    }
                    if (Utility.ToDouble(drdata["FundGrossAmount"].ToString()) <= 0)
                    {
                        parms1[j++] = new SqlParameter("@fundgrossamount", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@fundgrossamount", Utility.ToString(drdata["FundGrossAmount"].ToString()));
                    }
                    if (Utility.ToDouble(drdata["NHHrs"].ToString()) == 0)
                    {
                        parms1[j++] = new SqlParameter("@nhhrs", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@nhhrs", Utility.ToString(drdata["NHHrs"].ToString()));
                    }
                    if (Utility.ToDouble(drdata["Hourly_Rate"].ToString()) == 0)
                    {
                        parms1[j++] = new SqlParameter("@hourlyrate", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@hourlyrate", Utility.ToString(drdata["Hourly_Rate"].ToString()));
                    }
                    if (Utility.ToDouble(drdata["DaysWorkedRate"].ToString()) == 0)
                    {
                        parms1[j++] = new SqlParameter("@daysworkedrate", '0');
                    }
                    else
                    {
                        parms1[j++] = new SqlParameter("@daysworkedrate", Utility.ToString(drdata["DaysWorkedRate"].ToString()));
                    }

                  

                 





                    ds = DataAccess.ExecuteSPDataSet(sSQL1, parms1);

                    if (department.Tables.Count == 0)
                    {
                        department=ds.Clone();
                        department.Merge(ds, true);
                    }
                    else 
                    {
                        foreach (DataRow dr5 in ds.Tables[0].Rows)
                        {
                            DataRow dr7 = department.Tables[0].NewRow();
                            dr7[0] = dr5[0];
                            dr7[1] = dr5[1];
                            dr7[2] = dr5[2];
                            department.Tables[0].Rows.Add(dr7);
                        }

                        foreach (DataRow dr1 in ds.Tables[1].Rows)
                        {
                            DataRow dr9 = department.Tables[1].NewRow();
                            dr9[0] = dr1[0];
                            dr9[1] = dr1[1];
                            dr9[2] = dr1[2];
                            department.Tables[1].Rows.Add(dr9);
                        }
                    }
                }

                

                string strEarnings = "";
                string strours = "";

                strEarnings = "<table valign=\"top\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\"  width=\"100%\"><tr><td>RegularPay</td><td>" + item["Basic"].Text.ToString() + "</td></tr>";
                strours = "<table valign=\"top\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\" width=\"100%\"><tr><td>" + "-" + "</td></tr><tr><td>-</td></tr>";
                
                // if (department.Tables.Count > 0)
                // {

                DataView view = new DataView(department.Tables[0]);
                DataTable distinctValues = view.ToTable(true, "AddType");

                foreach (DataRow drnew in distinctValues.Rows)
                {
                    string strValue = drnew[0].ToString().Trim();
                    //strValue = strValue.Remove(0, strValue.Length - 4);
                    if (strValue == "OT1")
                    {
                        strours = strours + "<tr><td>" + item["OT1Hrs"].Text.ToString() + "</td></tr>";
                    }
                    else if (strValue == "OT2")
                    {
                        strours = strours + "<tr><td>" + item["OT2Hrs"].Text.ToString() + "</td></tr>";
                    }
                    else
                    {
                        strours = strours + "<tr><td>" + "-" + "</td></tr>";
                    }
                    //strours = strours + "<tr><td>" + "-" + "</td></tr>";
                    //strEarnings = strEarnings + "<tr><td>" + drnew[0].ToString() + "</td><td>" + drnew[1].ToString() + "</td></tr>";
                }

                //Get Earning Details 


                if (department.Tables.Count > 0)
                {
                    //Get Add Deductios as SUM                    
                    DataSet dsAdditions = new DataSet();
                    dsAdditions = department.Clone();

                    //Get Distinct Column Names for Deductions
                    List<string> lstAdd = new List<string>();

                    foreach (DataRow drnew1 in department.Tables[0].Rows)
                    {
                        if (lstAdd.Count == 0)
                        {
                            lstAdd.Add(drnew1[0].ToString());
                            DataRow newInRow2 = dsAdditions.Tables[0].NewRow();
                            newInRow2[0] = drnew1[0].ToString().TrimStart().TrimStart();
                            newInRow2[1] = "0";
                            dsAdditions.Tables[0].Rows.Add(newInRow2);
                        }
                        else
                        {
                            for (int i = 0; i < lstAdd.Count; i++) // Loop through List with for
                            {
                                if (!lstAdd.Contains(drnew1[0].ToString().TrimStart().TrimStart()))
                                {
                                    lstAdd.Add(drnew1[0].ToString());
                                    DataRow newInRow5 = dsAdditions.Tables[0].NewRow();
                                    newInRow5[0] = drnew1[0].ToString().TrimStart().TrimStart();
                                    newInRow5[1] = "0";
                                    dsAdditions.Tables[0].Rows.Add(newInRow5);
                                }
                            }
                        }
                    }

                    foreach (DataRow drded in department.Tables[0].Rows)
                    {
                        foreach (DataRow drnew8 in dsAdditions.Tables[0].Rows)
                        {
                            if (drded[0].ToString() == drnew8[0].ToString())
                            {
                                drnew8.BeginEdit();
                                double val1 = 0;
                                double val2 = 0;
                                if (drded[1].ToString() != "")
                                {
                                    val1 = Convert.ToDouble(drded[1].ToString());
                                }
                                if (drnew8[1].ToString() != "")
                                {
                                    val2 = Convert.ToDouble(drnew8[1].ToString());
                                }
                                drnew8[1] = Convert.ToString(Convert.ToDouble(val1) + Convert.ToDouble(val2));
                                drnew8.AcceptChanges();
                            }
                        }
                    }
                    foreach (DataRow drnew1 in dsAdditions.Tables[0].Rows)
                    {
                        if (drnew1[0].ToString().Contains("Fund") || drnew1[0].ToString().Contains("CPF Rate") || drnew1[0].ToString().Contains("CPF"))
                        {

                        }
                        else
                        {
                           // strDeductions = strDeductions + "<tr><td>" + drnew1[0].ToString() + "</td><td> " + drnew1[1].ToString() + " </td></tr>";
                            strEarnings = strEarnings + "<tr><td>" + drnew1[0].ToString() + "</td><td>" + drnew1[1].ToString() + "</td></tr>";
                        }
                    }
                }
                strEarnings = strEarnings + "<tr><td>Gross Pay</td><td>" + item["CPFGross1"].Text.ToString() + " </td></tr>";
                strEarnings = strEarnings + "<tr><td>Employer CPF</td><td>" + item["employercpfamt"].Text.ToString() + " </td></tr>";
                //strEarnings = strEarnings + "<tr><td>Gross Pay</td><td>" + item["GrossWithAddition"].Text.ToString() + " </td></tr>";
                strEarnings = strEarnings + "<tr><td>SDL*</td><td>" + item["Trade"].Text.ToString() + " </td></tr>";
                strEarnings = strEarnings + "</table>";
                strours = strours + "</table>";

                if (strEarnings.Length > 0)
                {
                    item["TotalAdditionsWONH"].Text = strEarnings;// "Regular Pay     " + item["Basic"].Text;
                }
                if (strours.Length > 0)
                {
                    item["OT1Hrs"].Text = strours;// "Regular Pay    " + item["Basic"].Text;
                }

                string strDeductions = "";
                strDeductions = "<table valign=\"top\" style=\"font:10px; font-family:Courier New\" Height=\"100%\" width=\"100%\"><tr><td>Employee Cpf " + "</td><td> " + item["employeecpfamt"].Text.ToString() + " </td></tr>";

                string fundtype = item["fundtype"].Text;
                double totalunpaid = 0;

                try { 
                    totalunpaid = Convert.ToDouble(item["totalunpaid"].Text.ToString());
                }
                catch (Exception ex) { }
                if (fundtype != "&nbsp;" )
                {
                    strDeductions = strDeductions + "<tr><td>" + item["fundtype"].Text + "</td><td>  " + item["fundamount"].Text + "  </td></tr>";
                }
                else
                {
                    //strDeductions = strDeductions + "<tr><td>" + item["fundtype"].Text + "</td><td>  " + item["fundamount"].Text + "  </td></tr>";
                }

                if ( totalunpaid > 0)
                {
                    strDeductions = strDeductions + "<tr><td>Unpaid Leave</td><td>  " + item["totalunpaid"].Text + "  </td></tr>";    
                }
                
               
                if (department.Tables.Count > 0)
                {
                    //Get Add Deductios as SUM                    
                    DataSet dsdeductions = new DataSet();
                    dsdeductions = department.Clone();

                    //Get Distinct Column Names for Deductions
                    List<string> lstdeuct = new List<string>();

                    foreach (DataRow drnew1 in department.Tables[1].Rows)
                    {
                        if (lstdeuct.Count == 0)
                        {
                            lstdeuct.Add(drnew1[0].ToString());
                            DataRow newInRow = dsdeductions.Tables[1].NewRow();
                            newInRow[0] = drnew1[0].ToString().TrimStart().TrimStart();
                            newInRow[1] = "0";
                            dsdeductions.Tables[1].Rows.Add(newInRow);
                        }
                        else
                        {
                            for (int i = 0; i < lstdeuct.Count; i++) // Loop through List with for
                            {
                                if (!lstdeuct.Contains(drnew1[0].ToString().TrimStart().TrimStart()))
                                {
                                    lstdeuct.Add(drnew1[0].ToString());
                                    DataRow newInRow1 = dsdeductions.Tables[1].NewRow();
                                    newInRow1[0] = drnew1[0].ToString().TrimStart().TrimStart();
                                    newInRow1[1] = "0";
                                    dsdeductions.Tables[1].Rows.Add(newInRow1);
                                }
                            }
                        }
                    }

                    foreach (DataRow drded in department.Tables[1].Rows)
                    {
                        foreach (DataRow drnew8 in dsdeductions.Tables[1].Rows)
                        {
                            if (drded[0].ToString() == drnew8[0].ToString())
                            {
                                drnew8.BeginEdit();
                                drnew8[1] = Convert.ToString(Convert.ToDouble(drded[1].ToString()) + Convert.ToDouble(drnew8[1].ToString()));
                                drnew8.AcceptChanges();
                            }
                        }
                    }
                    foreach (DataRow drnew1 in dsdeductions.Tables[1].Rows)
                    {
                        if (drnew1[0].ToString().Contains("Fund") || drnew1[0].ToString().Contains("CPF Rate") || drnew1[0].ToString().Contains("CPF"))
                        {

                        }
                        else
                        {
                            strDeductions = strDeductions + "<tr><td>" + drnew1[0].ToString() + "</td><td> " + drnew1[1].ToString() + " </td></tr>";
                        }
                    }
                }
                if (strDeductions.Length > 0)
                {
                    strDeductions = strDeductions + "</table>";
                    item["TotalDeductions"].Text = strDeductions;
                }


                string dtStartDate1 = "";
                string dtEndDate1 = "";

                dtStartDate = Convert.ToString(Month.ToString() + "/" + Session["PaySubStartDay"].ToString() + "/" + Utility.ToString(Request.QueryString["Year"]));
                dtEndDate = Convert.ToString(Month.ToString() + "/" + Session["PaySubEndDay"].ToString() + "/" + Utility.ToString(Request.QueryString["Year"]));

                int year, day, month = 0;
                DateTime dttime;
                if (dtStartDate != null)
                {
                    //Month /Date
                    TimeSpan ts = new TimeSpan(0, 24, 0, 0);
                    //dtStartDate1 = Convert.ToDateTime(dtStartDate).Subtract(ts).ToString("MM/dd/yyyy");

                    year = Utility.ToInteger(Request.QueryString["Year"]);
                    month = Utility.ToInteger(Month.ToString());
                    day = Utility.ToInteger(Session["PaySubStartDay"].ToString());
                    dttime = new DateTime(year, month, day);
                    dtStartDate1 = dttime.ToString("MM/dd/yyyy");

                }

                if (dtEndDate != null)
                {
                    //DateTime value = new DateTime(2010, 1, 18);



                    year = Utility.ToInteger(Request.QueryString["Year"]);
                    month = Utility.ToInteger(Month.ToString());
                    day = Utility.ToInteger(Session["PaySubEndDay"].ToString());

                    TimeSpan ts1 = new TimeSpan(0, 24, 0, 0);
                    dttime = new DateTime(year, month, day);
                    dtEndDate1 = dttime.ToString("MM/dd/yyyy");
                }



                string sqlQuery = "Select  count(*) New from employee where dept_id in (Select id from department Where Company_id=" + Utility.ToInteger(Session["compid"]).ToString() ;
                sqlQuery = sqlQuery + ") AND convert(DateTime,joining_date,103) >='" + dtStartDate1 + "' AND joining_date <='" + dtEndDate1 + "'  And convert(Datetime, termination_date,103) is null Union All ";
                sqlQuery = sqlQuery + "Select  count(*) Existing from employee where dept_id in (Select id from department Where Company_id= " + Utility.ToInteger(Session["compid"]).ToString();
                sqlQuery = sqlQuery + " ) AND  convert(DateTime,joining_date,103) <='" + dtStartDate1 + "' AND (convert(Datetime, termination_date,103) >='" + dtEndDate1 + "' OR convert(Datetime, termination_date,103) is Null)  Union All ";
                sqlQuery = sqlQuery + " Select  count(*)  Resigned from employee where  dept_id in ";
                sqlQuery = sqlQuery + "(Select id from department Where Company_id=" + Utility.ToInteger(Session["compid"]).ToString();// + //" and DeptName ='" + item["DeptName"].Text.ToString().Trim() + "')";
                sqlQuery = sqlQuery + " ) AND convert(Datetime, termination_date,103) >='" + dtStartDate1 + "' AND termination_date <='" + dtEndDate1 + "'  And termination_date is Not null";
                


                //string sqlQuery = "Select  count(*) New from employee where  ";
                //sqlQuery = sqlQuery + " joining_date >'" + dtStartDate.ToString() + "' AND joining_date <'" + dtEndDate.ToString() + "' AND Company_id= " + Utility.ToInteger(Session["compid"]).ToString() + "  And termination_date is null Union All ";
                //sqlQuery = sqlQuery + "Select  count(*) Existing from employee where   Company_id= " + Utility.ToInteger(Session["compid"]).ToString();
                //sqlQuery = sqlQuery + " AND joining_date <'" + dtStartDate.ToString() + "' AND (termination_date >'" + dtEndDate.ToString() + "' OR termination_date is Null)  Union All ";
                //sqlQuery = sqlQuery + " Select  count(*)  Resigned from employee where Company_id= " + Utility.ToInteger(Session["compid"]).ToString() ;
                ////sqlQuery = sqlQuery + "(Select id from department Where Company_id=" + Utility.ToInteger(Session["compid"]).ToString() + " and DeptName ='" + item["DeptName"].Text.ToString().Trim() + "')";
                //sqlQuery = sqlQuery + " AND termination_date >'" + dtStartDate.ToString() + "' AND termination_date <'" + dtEndDate.ToString() + "'  And termination_date is Not null";

                DataSet dsdept = new DataSet();

                dsdept = DataAccess.FetchRS(CommandType.Text, sqlQuery, null);

                string sqlQuery1 = "Select   time_card_no + '  ( ' + emp_name + ' ' + emp_lname + ' ) ' Empl from employee where  ";
                sqlQuery1 = sqlQuery1 + " joining_date >'" + dtStartDate.ToString() + "' AND joining_date <'" + dtEndDate.ToString() + "' AND Company_id= " + Utility.ToInteger(Session["compid"]).ToString() + "  And termination_date is null ; ";
                sqlQuery1 = sqlQuery1 + "Select  time_card_no + '  ( ' + emp_name + ' ' + emp_lname + ' ) ' Empl from employee where   Company_id= " + Utility.ToInteger(Session["compid"]).ToString();
                sqlQuery1 = sqlQuery1 + " AND joining_date <'" + dtStartDate.ToString() + "' AND (termination_date >'" + dtEndDate.ToString() + "' OR termination_date is Null)  ; ";
                sqlQuery1 = sqlQuery1 + " Select  time_card_no + '  ( ' + emp_name + ' ' + emp_lname + ' ) ' Empl from employee where Company_id= " + Utility.ToInteger(Session["compid"]).ToString();
                sqlQuery = sqlQuery + "(Select id from department Where Company_id=" + Utility.ToInteger(Session["compid"]).ToString() + " and DeptName ='" + item["DeptName"].Text.ToString().Trim() + "')";
                sqlQuery1 = sqlQuery1 + " AND termination_date >'" + dtStartDate.ToString() + "' AND termination_date <='" + dtEndDate.ToString() + "'  And termination_date is Not null";

                DataSet dsemployee = new DataSet();

                dsemployee = DataAccess.FetchRS(CommandType.Text, sqlQuery1, null);

                string deptName1 = "";

                if (dsdept != null)
                {
                    deptName1 = item["DeptName"].Text + " \n ";
                    deptName1 = deptName1 + " New:" + dsdept.Tables[0].Rows[0][0].ToString() + " \n";
                    //New
                    foreach (DataRow dr1 in dsemployee.Tables[0].Rows)
                    {
                    //    deptName1 = deptName1 + dr1[0].ToString() + "\n"; 
                    }
                    deptName1 = deptName1 + " Existing:" + dsdept.Tables[0].Rows[1][0].ToString() + " \n";
                    //Existing
                    foreach (DataRow dr1 in dsemployee.Tables[1].Rows)
                    {
                     //   deptName1 = deptName1 + dr1[0].ToString() + "\n"; 
                    }
                    //Resigned
                    deptName1 = deptName1 + " Resigned:" + dsdept.Tables[0].Rows[2][0].ToString() + "\n";
                    foreach (DataRow dr1 in dsemployee.Tables[2].Rows)
                    {
                       // deptName1 = deptName1 + dr1[0].ToString() + "\n"; 
                    }
                }
               

                if (deptName1.Length > 0)
                {
                    if (dsdept.Tables.Count > 0)
                    {
                        item["DeptName"].Text = deptName1;
                        //if (dsdept.Tables[0].Rows[0][0].ToString() == "0" && dsdept.Tables[0].Rows[1][0].ToString() == "0" && dsdept.Tables[0].Rows[2][0].ToString() == "0")
                        //{

                        //}
                        //else
                        //{
                            
                        //}
                    }
                }
            }
        }


        protected void RadGrid2_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem item = e.Item as GridDataItem;
                
                //Get All Employyes whose Department is same and call SP and get data for Additions and deductions as well.........
                DataSet dstemp = new DataSet();
                DataTable dttemp1 = new DataTable();
                dttemp1 = dt.Copy();

                dstemp.Tables.Add(dttemp1);


                string deptName = "select DeptName from department where Company_id =" + Utility.ToInteger(Session["compid"]);
                SqlDataReader dr;
                dr = DataAccess.ExecuteReader(CommandType.Text, deptName, null);

                DataSet dsfinal = new DataSet();
                dsfinal = dstemp.Clone();
                DataSet department = new DataSet();

                string monthstr = "";
                DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + Request.QueryString["Month"]);
                foreach (DataRow drnew in drResults)
                {
                    monthstr = drnew["Month"].ToString();
                }
                int Month = Convert.ToInt32(monthstr);

                string dtStartDate;
                string dtEndDate;

                string dtStartDate1="";
                string dtEndDate1="";

                dtStartDate = Convert.ToString(Month.ToString() + "/" + Session["PaySubStartDay"].ToString() + "/" + Utility.ToString(Request.QueryString["Year"]));
                dtEndDate = Convert.ToString(Month.ToString() + "/" + Session["PaySubEndDay"].ToString() + "/" + Utility.ToString(Request.QueryString["Year"]));

                int year, day, month = 0;
                DateTime dttime  ;
                if(dtStartDate!=null)
                {
                    //Month /Date
                    TimeSpan ts = new TimeSpan(0, 24, 0, 0);
                    //dtStartDate1 = Convert.ToDateTime(dtStartDate).Subtract(ts).ToString("MM/dd/yyyy");

                    year = Utility.ToInteger(Request.QueryString["Year"]);
                    month = Utility.ToInteger(Month.ToString());
                    day = Utility.ToInteger(Session["PaySubStartDay"].ToString());
                        dttime = new DateTime(year,month,day);
                    dtStartDate1 = dttime.ToString("MM/dd/yyyy");
                  
                }

                if(dtEndDate!=null)
                {
                    //DateTime value = new DateTime(2010, 1, 18);

                    
                    
                    year =Utility.ToInteger(Request.QueryString["Year"]);
                    month = Utility.ToInteger(Month.ToString());
                    day =Utility.ToInteger(Session["PaySubEndDay"].ToString());

                    TimeSpan ts1 = new TimeSpan(0, 24, 0, 0);
                     dttime = new DateTime(year,month,day);
                    dtEndDate1 = dttime.ToString("MM/dd/yyyy");
                }
               

                //while (dr.Read())
                //{
                    //string strdeptName = dr[0].ToString();
                double totalunpaid = 0.00;

                DataView dataView = dstemp.Tables[0].DefaultView;
                dataView.RowFilter = "deptname='" + item["DeptName"].Text + "'";
                for (int i = 0; i < dataView.Count; i++)
                {
                    totalunpaid += double.Parse(dataView[i]["totalunpaid"].ToString());
                }

                    DataRow[] drread;
                    drread = dstemp.Tables[0].Select("deptname='" + item["DeptName"].Text + "'");

                    if (drread.Length > 0)
                    {
                       
                        DataSet ds = new DataSet();
                        foreach (DataRow drdata in drread)
                        {
                            string sSQL1 = "sp_GetEmployeePayDetails_New";
                            SqlParameter[] parms1 = new SqlParameter[23];
                            
                            int j = 0;
                            parms1[j++] = new SqlParameter("@emp_code", drdata["Emp_Code"].ToString());
                            parms1[j++] = new SqlParameter("@Year", Utility.ToString(Request.QueryString["Year"]));
                            parms1[j++] = new SqlParameter("@Month", Utility.ToString(Month));
                            parms1[j++] = new SqlParameter("@stdatesubmonth", Session["PaySubStartDay"].ToString());
                            parms1[j++] = new SqlParameter("@endatesubmonth", Session["PaySubEndDay"].ToString());
                            if (Utility.ToString(drdata["Days_Work"].ToString()) == "")
                            {
                                parms1[j++] = new SqlParameter("@Day_Work", '0');
                            }
                            else
                            {
                                parms1[j++] = new SqlParameter("@Day_Work", Utility.ToString(drdata["Days_Work"].ToString()));
                            }
                            if (Utility.ToString(drdata["OT1"].ToString()) == "")
                            {
                                parms1[j++] = new SqlParameter("@OT1", '0');
                            }
                            else
                            {
                                parms1[j++] = new SqlParameter("@OT1", Utility.ToString(drdata["OT1"].ToString()));
                            }
                            if (Utility.ToString(drdata["OT2"].ToString()) == "")
                            {
                                parms1[j++] = new SqlParameter("@OT2", '0');
                            }
                            else
                            {
                                parms1[j++] = new SqlParameter("@OT2", Utility.ToString(drdata["OT2"].ToString()));
                            }

                            if (Utility.ToString(drdata["Daily_Rate"].ToString()) == "") //Daily_Rate
                            {
                                parms1[j++] = new SqlParameter("@BasicDayRate", '0');
                            }
                            else
                            {
                                parms1[j++] = new SqlParameter("@BasicDayRate", Utility.ToString(drdata["Daily_Rate"].ToString()));
                            }
                            if (Utility.ToString(drdata["OT1Hrs"].ToString()) == "")
                            {
                                parms1[j++] = new SqlParameter("@OT1Hrs", '0');
                            }
                            else
                            {
                                parms1[j++] = new SqlParameter("@OT1Hrs", Utility.ToString(drdata["OT1Hrs"].ToString()));
                            }
                            if (Utility.ToString(drdata["OT2Hrs"].ToString()) == "")
                            {
                                parms1[j++] = new SqlParameter("@OT2Hrs", '0');
                            }
                            else
                            {
                                parms1[j++] = new SqlParameter("@OT2Hrs", Utility.ToString(drdata["OT2Hrs"].ToString()));
                            }
                            if (Utility.ToString(drdata["OT1Rate"].ToString()) == "")
                            {
                                parms1[j++] = new SqlParameter("@OT1Rate", '0');
                            }
                            else
                            {
                                parms1[j++] = new SqlParameter("@OT1Rate", Utility.ToString(drdata["OT1Rate"].ToString()));
                            }

                            if (Utility.ToString(drdata["OT2Rate"].ToString()) == "")
                            {
                                parms1[j++] = new SqlParameter("@OT2Rate", '0');
                            }
                            else
                            {
                                parms1[j++] = new SqlParameter("@OT2Rate", Utility.ToString(drdata["OT2Rate"].ToString()));
                            }

                            if (Utility.ToString(drdata["EmployeeCPFAmt"].ToString()) == "")
                            {
                                parms1[j++] = new SqlParameter("@empcpfamount", '0');
                            }
                            else
                            {
                                parms1[j++] = new SqlParameter("@empcpfamount", Utility.ToString(drdata["EmployeeCPFAmt"].ToString()));
                            }

                            if (Utility.ToString(drdata["CPFOrdinaryCeil"].ToString()) == "")
                            {
                                parms1[j++] = new SqlParameter("@ordwages", '0');
                            }
                            else
                            {
                                parms1[j++] = new SqlParameter("@ordwages", Utility.ToString(drdata["CPFOrdinaryCeil"].ToString()));
                            }

                            if (Utility.ToString(drdata["CPFAdditionNet"].ToString()) == "")
                            {
                                parms1[j++] = new SqlParameter("@addwages", '0');
                            }
                            else
                            {
                                parms1[j++] = new SqlParameter("@addwages", Utility.ToString(drdata["CPFAdditionNet"].ToString()));
                            }

                            //HardCoded Value
                            //if (Utility.ToString(item["CPFEmployeePerc"].Text) == "")
                            //{
                            parms1[j++] = new SqlParameter("@cpfrate", 20);//Utility.ToString(item["CPFEmployeePerc"].Text));
                            //}
                            //else
                            //{
                            //    parms1[j++] = new SqlParameter("@cpfrate", 20);// new SqlParameter("@cpfrate", Utility.ToString(item["CPFEmployeePerc"].Text));
                            //}
                            if (Utility.ToString(drdata["FundType"].ToString()) == "")
                            {
                                parms1[j++] = new SqlParameter("@fundname", '-');
                            }
                            else
                            {
                                parms1[j++] = new SqlParameter("@fundname", Utility.ToString(drdata["FundType"].ToString()));
                            }
                            if (Utility.ToDouble(drdata["FundAmount"].ToString()) <= 0)
                            {
                                parms1[j++] = new SqlParameter("@fundamount", '0');
                            }
                            else
                            {
                                parms1[j++] = new SqlParameter("@fundamount", Utility.ToString(drdata["FundAmount"].ToString()));
                            }
                            if (Utility.ToDouble(drdata["FundGrossAmount"].ToString()) <= 0)
                            {
                                parms1[j++] = new SqlParameter("@fundgrossamount", '0');
                            }
                            else
                            {
                                parms1[j++] = new SqlParameter("@fundgrossamount", Utility.ToString(drdata["FundGrossAmount"].ToString()));
                            }
                            if (Utility.ToDouble(drdata["NHHrs"].ToString()) == 0)
                            {
                                parms1[j++] = new SqlParameter("@nhhrs", '0');
                            }
                            else
                            {
                                parms1[j++] = new SqlParameter("@nhhrs", Utility.ToString(drdata["NHHrs"].ToString()));
                            }
                            if (Utility.ToDouble(drdata["Hourly_Rate"].ToString()) == 0)
                            {
                                parms1[j++] = new SqlParameter("@hourlyrate", '0');
                            }
                            else
                            {
                                parms1[j++] = new SqlParameter("@hourlyrate", Utility.ToString(drdata["Hourly_Rate"].ToString()));
                            }
                            if (Utility.ToDouble(drdata["DaysWorkedRate"].ToString()) == 0)
                            {
                                parms1[j++] = new SqlParameter("@daysworkedrate", '0');
                            }
                            else
                            {
                                parms1[j++] = new SqlParameter("@daysworkedrate", Utility.ToString(drdata["DaysWorkedRate"].ToString()));
                            }
                      

                           
                            




                            ds = DataAccess.ExecuteSPDataSet(sSQL1, parms1);

                            if (department.Tables.Count == 0)
                            {
                                department = ds.Copy();
                            }
                            else
                            {
                                foreach (DataRow dr4 in ds.Tables[0].Rows)
                                {
                                    bool flag = true;
                                    foreach (DataRow dr7 in department.Tables[0].Rows)
                                    {
                                        if (dr4["AddType"].ToString() == dr7["AddType"].ToString())
                                        {

                                            dr7["AddAmt"] = Convert.ToString(Convert.ToDouble(dr7["AddAmt"].ToString()) + Convert.ToDouble(dr4["AddAmt"].ToString()));
                                            department.AcceptChanges();
                                            flag = false;
                                        }                                     
                                    }
                                    if (flag)
                                    {
                                        DataRow drnew = department.Tables[0].NewRow();
                                        Random rd = new Random();
                                        drnew[0] = dr4[0].ToString();
                                        if (dr4[1].ToString() != "")
                                        {
                                            drnew[1] = dr4[1].ToString();
                                        }
                                        else
                                        {
                                            drnew[1] = "0";
                                        }
                                        drnew[2] = rd.Next().ToString();
                                        department.Tables[0].Rows.Add(drnew);
                                        department.AcceptChanges();
                                    }
                                    //department.Tables[0].Rows.Add(dr4);
                                }

                                foreach (DataRow dr5 in ds.Tables[1].Rows)
                                {
                                    //department.Tables[1].Rows.Add(dr5);

                                    bool flag = true;
                                    foreach (DataRow dr7 in department.Tables[1].Rows)
                                    {
                                        if (dr5["AddType"].ToString() == dr7["AddType"].ToString())
                                        {

                                            dr7["AddAmt"] = Convert.ToString(Convert.ToDouble(dr7["AddAmt"].ToString()) + Convert.ToDouble(dr5["AddAmt"].ToString()));
                                            department.AcceptChanges();
                                            flag = false;
                                        }
                                    }
                                    if (flag)
                                    {
                                        DataRow drnew = department.Tables[1].NewRow();
                                        Random rd = new Random();
                                        drnew[0] = dr5[0].ToString();
                                        drnew[1] = dr5[1].ToString();
                                        drnew[2] = rd.Next().ToString();
                                        department.Tables[1].Rows.Add(drnew);
                                        department.AcceptChanges();
                                    }
                                }
                            
                            }
                        }

                    }

                    string strEarnings = "";
                    string strours = "";

                    strEarnings = "<table valign=\"top\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\"  width=\"100%\"><tr><td>RegularPay</td><td>" + item["Basic"].Text.ToString() + "</td></tr>";
                    strours = "<table valign=\"top\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\" width=\"100%\"><tr><td>" + "-" + "</td></tr><tr><td>-</td></tr>";

                   // if (department.Tables.Count > 0)
                   // {
                        foreach (DataRow drnew in department.Tables[0].Rows)
                        {
                            string strValue = drnew[0].ToString().Trim();
                            //strValue = strValue.Remove(0, strValue.Length - 4);
                            if (strValue == "OT1")
                            {
                                strours = strours + "<tr><td>" + item["OT1Hrs"].Text.ToString() + "</td></tr>";
                            }
                            else if (strValue == "OT2")
                            {
                                strours = strours + "<tr><td>" + item["OT2Hrs"].Text.ToString() + "</td></tr>";
                            }
                            else
                            {
                                strours = strours + "<tr><td>" + "-" + "</td></tr>";
                            }
                            
                            //strours = strours + "<tr><td>" + "-" + "</td></tr>";
                            decimal earning = Convert.ToDecimal(drnew[1].ToString());
                            drnew[1] = earning.ToString("0.00");
                            strEarnings = strEarnings + "<tr><td>" + drnew[0].ToString() + "</td><td>" + drnew[1].ToString() + "</td></tr>";
                        }
                    //}
                        decimal cpfgross = Convert.ToDecimal(item["CPFGross1"].Text.ToString());
                        item["CPFGross1"].Text = cpfgross.ToString("0.00");

                        decimal employercpf = Convert.ToDecimal(item["employercpfamt"].Text.ToString());
                        item["employercpfamt"].Text = employercpf.ToString("0.00");

                        decimal sdl = Convert.ToDecimal(item["Trade"].Text.ToString());
                        item["Trade"].Text = sdl.ToString("0.00");


                        strEarnings = strEarnings + "<tr><td>Gross Pay</td><td>" + item["CPFGross1"].Text + " </td></tr>";
                        strEarnings = strEarnings + "<tr><td>Employer CPF</td><td>" + item["employercpfamt"].Text + " </td></tr>";                    
                        //strEarnings = strEarnings + "<tr><td>Gross Pay</td><td>" + item["GrossWithAddition"].Text.ToString() + " </td></tr>";
                        strEarnings = strEarnings + "<tr><td>SDL*</td><td>" + item["Trade"].Text.ToString() + " </td></tr>";                    
                    
                        strEarnings = strEarnings + "</table>";
                        strours = strours + "</table>";

                        if (strEarnings.Length > 0)
                        {
                            item["TotalAdditionsWONH"].Text = strEarnings;// "Regular Pay     " + item["Basic"].Text;
                        }
                        if (strours.Length > 0)
                        {
                            item["OT1Hrs"].Text = strours;// "Regular Pay    " + item["Basic"].Text;
                        }

                        string strDeductions = "";
                        strDeductions = "<table valign=\"top\" style=\"font:10px; font-family:Courier New\" Height=\"100%\" width=\"100%\"><tr><td>Employee Cpf " + "</td><td> " + item["employeecpfamt"].Text.ToString() + " </td></tr>";

                        if (totalunpaid > 0)
                        {
                            strDeductions = strDeductions + "<tr><td>Unpaid Leave</td><td>" + totalunpaid.ToString("0.00") + " </td></tr>";                    
                        }
                        
                        //if (item["fundtype"].Text != "")
                        //{
                        //    strDeductions = strDeductions + "<tr><td>" + item["fundtype"].Text + "</td><td>  " + item["fundamount"].Text + "  </td></tr>";
                        //}
                        //if (department.Tables.Count > 0)
                       // {
                            foreach (DataRow drnew1 in department.Tables[1].Rows)
                            {
                                //if (dr[0].ToString().Contains("Fund") || dr[0].ToString().Contains("CPF Rate") || dr[0].ToString().Contains("CPF"))
                                //{

                                //}
                                //else
                                //{
                                strDeductions = strDeductions + "<tr><td>" + drnew1[0].ToString() + "</td><td> " + drnew1[1].ToString() + " </td></tr>";
                                //}
                            }
                       // }
                        if (strDeductions.Length > 0)
                        {
                            strDeductions = strDeductions + "</table>";    
                            item["TotalDeductions"].Text = strDeductions;
                        }

                        
                            


                        string sqlQuery = "Select  count(*) New from employee where dept_id in (Select id from department Where Company_id=" + Utility.ToInteger(Session["compid"]).ToString() + "  and DeptName ='" + item["DeptName"].Text.ToString().Trim() + "' ) ";
                        sqlQuery = sqlQuery + "AND convert(DateTime,joining_date,103) >='" + dtStartDate1 + "' AND joining_date <='" + dtEndDate1 + "'  And convert(Datetime, termination_date,103) is null Union All ";
                        sqlQuery = sqlQuery + "Select  count(*) Existing from employee where dept_id in (Select id from department Where Company_id= " + Utility.ToInteger(Session["compid"]).ToString() + "  and DeptName ='" + item["DeptName"].Text.ToString().Trim() + "' ) ";
                        sqlQuery = sqlQuery + " AND  convert(DateTime,joining_date,103) <='" + dtStartDate1 + "' AND (convert(Datetime, termination_date,103) >='" + dtEndDate1 + "' OR convert(Datetime, termination_date,103) is Null)  Union All ";
                        sqlQuery = sqlQuery + " Select  count(*)  Resigned from employee where  dept_id in ";
                        sqlQuery = sqlQuery + "(Select id from department Where Company_id=" + Utility.ToInteger(Session["compid"]).ToString() + " and DeptName ='" + item["DeptName"].Text.ToString().Trim() + "')";
                        sqlQuery = sqlQuery + "AND convert(Datetime, termination_date,103) >='" + dtStartDate1 + "' AND termination_date <='" + dtEndDate1 + "'  And termination_date is Not null";

                        DataSet dsdept = new DataSet();

                        dsdept = DataAccess.FetchRS(CommandType.Text, sqlQuery, null);
                        string deptName1 = "";

                        if (dsdept != null)
                        {
                            deptName1 = item["DeptName"].Text + " \n ";
                            deptName1 = deptName1 + " New     :" + dsdept.Tables[0].Rows[0][0].ToString();
                            deptName1 = deptName1 + " Existing:" + dsdept.Tables[0].Rows[1][0].ToString();
                            deptName1 = deptName1 + " Resigned:" + dsdept.Tables[0].Rows[2][0].ToString();
                        }

                        if (deptName1.Length > 0)
                        {
                            if (dsdept.Tables.Count > 0)
                            {
                                if (dsdept.Tables[0].Rows[0][0].ToString() == "0" && dsdept.Tables[0].Rows[1][0].ToString() == "0" && dsdept.Tables[0].Rows[2][0].ToString() == "0")
                                {

                                }
                                else
                                {
                                    item["DeptName"].Text = deptName1;
                                }
                            }
                        }

            }
        }


        protected void RadGrid3_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                //GridDataItem item = e.Item as GridDataItem;

                ////Get All Employyes whose Department is same and call SP and get data for Additions and deductions as well.........
                //DataSet dstemp = new DataSet();
                //DataTable dttemp1 = new DataTable();
                //dttemp1 = dt.Copy();

                //dstemp.Tables.Add(dttemp1);


                //string deptName = "select DeptName from department where Company_id =" + Utility.ToInteger(Session["compid"]);
                //SqlDataReader dr;
                //dr = DataAccess.ExecuteReader(CommandType.Text, deptName, null);

                //DataSet dsfinal = new DataSet();
                //dsfinal = dstemp.Clone();
                //DataSet department = new DataSet();

                //string monthstr = "";
                //DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + Request.QueryString["Month"]);
                //foreach (DataRow drnew in drResults)
                //{
                //    monthstr = drnew["Month"].ToString();
                //}
                //int Month = Convert.ToInt32(monthstr);

                //string dtStartDate;
                //string dtEndDate;

                //dtStartDate = Convert.ToString(Month.ToString() + "/" + Session["PaySubStartDay"].ToString() + "/" + Utility.ToString(Request.QueryString["Year"]));
                //dtEndDate = Convert.ToString(Month.ToString() + "/" + Session["PaySubEndDay"].ToString() + "/" + Utility.ToString(Request.QueryString["Year"]));




                ////while (dr.Read())
                ////{
                ////string strdeptName = dr[0].ToString();
                //DataRow[] drread;
                //drread = dstemp.Tables[0].Select("deptname='" + item["DeptName"].Text + "'");

                //if (drread.Length > 0)
                //{

                //    DataSet ds = new DataSet();
                //    foreach (DataRow drdata in drread)
                //    {
                //        string sSQL1 = "sp_GetEmployeePayDetails_New";
                //        SqlParameter[] parms1 = new SqlParameter[23];

                //        int j = 0;
                //        parms1[j++] = new SqlParameter("@emp_code", drdata["Emp_Code"].ToString());
                //        parms1[j++] = new SqlParameter("@Year", Utility.ToString(Request.QueryString["Year"]));
                //        parms1[j++] = new SqlParameter("@Month", Utility.ToString(Month));
                //        parms1[j++] = new SqlParameter("@stdatesubmonth", Session["PaySubStartDay"].ToString());
                //        parms1[j++] = new SqlParameter("@endatesubmonth", Session["PaySubEndDay"].ToString());
                //        if (Utility.ToString(drdata["Days_Work"].ToString()) == "")
                //        {
                //            parms1[j++] = new SqlParameter("@Day_Work", '0');
                //        }
                //        else
                //        {
                //            parms1[j++] = new SqlParameter("@Day_Work", Utility.ToString(drdata["Days_Work"].ToString()));
                //        }
                //        if (Utility.ToString(drdata["OT1"].ToString()) == "")
                //        {
                //            parms1[j++] = new SqlParameter("@OT1", '0');
                //        }
                //        else
                //        {
                //            parms1[j++] = new SqlParameter("@OT1", Utility.ToString(drdata["OT1"].ToString()));
                //        }
                //        if (Utility.ToString(drdata["OT2"].ToString()) == "")
                //        {
                //            parms1[j++] = new SqlParameter("@OT2", '0');
                //        }
                //        else
                //        {
                //            parms1[j++] = new SqlParameter("@OT2", Utility.ToString(drdata["OT2"].ToString()));
                //        }

                //        if (Utility.ToString(drdata["Daily_Rate"].ToString()) == "") //Daily_Rate
                //        {
                //            parms1[j++] = new SqlParameter("@BasicDayRate", '0');
                //        }
                //        else
                //        {
                //            parms1[j++] = new SqlParameter("@BasicDayRate", Utility.ToString(drdata["Daily_Rate"].ToString()));
                //        }
                //        if (Utility.ToString(drdata["OT1Hrs"].ToString()) == "")
                //        {
                //            parms1[j++] = new SqlParameter("@OT1Hrs", '0');
                //        }
                //        else
                //        {
                //            parms1[j++] = new SqlParameter("@OT1Hrs", Utility.ToString(drdata["OT1Hrs"].ToString()));
                //        }
                //        if (Utility.ToString(drdata["OT2Hrs"].ToString()) == "")
                //        {
                //            parms1[j++] = new SqlParameter("@OT2Hrs", '0');
                //        }
                //        else
                //        {
                //            parms1[j++] = new SqlParameter("@OT2Hrs", Utility.ToString(drdata["OT2Hrs"].ToString()));
                //        }
                //        if (Utility.ToString(drdata["OT1Rate"].ToString()) == "")
                //        {
                //            parms1[j++] = new SqlParameter("@OT1Rate", '0');
                //        }
                //        else
                //        {
                //            parms1[j++] = new SqlParameter("@OT1Rate", Utility.ToString(drdata["OT1Rate"].ToString()));
                //        }

                //        if (Utility.ToString(drdata["OT2Rate"].ToString()) == "")
                //        {
                //            parms1[j++] = new SqlParameter("@OT2Rate", '0');
                //        }
                //        else
                //        {
                //            parms1[j++] = new SqlParameter("@OT2Rate", Utility.ToString(drdata["OT2Rate"].ToString()));
                //        }

                //        if (Utility.ToString(drdata["EmployeeCPFAmt"].ToString()) == "")
                //        {
                //            parms1[j++] = new SqlParameter("@empcpfamount", '0');
                //        }
                //        else
                //        {
                //            parms1[j++] = new SqlParameter("@empcpfamount", Utility.ToString(drdata["EmployeeCPFAmt"].ToString()));
                //        }

                //        if (Utility.ToString(drdata["CPFOrdinaryCeil"].ToString()) == "")
                //        {
                //            parms1[j++] = new SqlParameter("@ordwages", '0');
                //        }
                //        else
                //        {
                //            parms1[j++] = new SqlParameter("@ordwages", Utility.ToString(drdata["CPFOrdinaryCeil"].ToString()));
                //        }

                //        if (Utility.ToString(drdata["CPFAdditionNet"].ToString()) == "")
                //        {
                //            parms1[j++] = new SqlParameter("@addwages", '0');
                //        }
                //        else
                //        {
                //            parms1[j++] = new SqlParameter("@addwages", Utility.ToString(drdata["CPFAdditionNet"].ToString()));
                //        }

                //        //HardCoded Value
                //        //if (Utility.ToString(item["CPFEmployeePerc"].Text) == "")
                //        //{
                //        parms1[j++] = new SqlParameter("@cpfrate", 20);//Utility.ToString(item["CPFEmployeePerc"].Text));
                //        //}
                //        //else
                //        //{
                //        //    parms1[j++] = new SqlParameter("@cpfrate", 20);// new SqlParameter("@cpfrate", Utility.ToString(item["CPFEmployeePerc"].Text));
                //        //}
                //        if (Utility.ToString(drdata["FundType"].ToString()) == "")
                //        {
                //            parms1[j++] = new SqlParameter("@fundname", '-');
                //        }
                //        else
                //        {
                //            parms1[j++] = new SqlParameter("@fundname", Utility.ToString(drdata["FundType"].ToString()));
                //        }
                //        if (Utility.ToDouble(drdata["FundAmount"].ToString()) <= 0)
                //        {
                //            parms1[j++] = new SqlParameter("@fundamount", '0');
                //        }
                //        else
                //        {
                //            parms1[j++] = new SqlParameter("@fundamount", Utility.ToString(drdata["FundAmount"].ToString()));
                //        }
                //        if (Utility.ToDouble(drdata["FundGrossAmount"].ToString()) <= 0)
                //        {
                //            parms1[j++] = new SqlParameter("@fundgrossamount", '0');
                //        }
                //        else
                //        {
                //            parms1[j++] = new SqlParameter("@fundgrossamount", Utility.ToString(drdata["FundGrossAmount"].ToString()));
                //        }
                //        if (Utility.ToDouble(drdata["NHHrs"].ToString()) == 0)
                //        {
                //            parms1[j++] = new SqlParameter("@nhhrs", '0');
                //        }
                //        else
                //        {
                //            parms1[j++] = new SqlParameter("@nhhrs", Utility.ToString(drdata["NHHrs"].ToString()));
                //        }
                //        if (Utility.ToDouble(drdata["Hourly_Rate"].ToString()) == 0)
                //        {
                //            parms1[j++] = new SqlParameter("@hourlyrate", '0');
                //        }
                //        else
                //        {
                //            parms1[j++] = new SqlParameter("@hourlyrate", Utility.ToString(drdata["Hourly_Rate"].ToString()));
                //        }
                //        if (Utility.ToDouble(drdata["DaysWorkedRate"].ToString()) == 0)
                //        {
                //            parms1[j++] = new SqlParameter("@daysworkedrate", '0');
                //        }
                //        else
                //        {
                //            parms1[j++] = new SqlParameter("@daysworkedrate", Utility.ToString(drdata["DaysWorkedRate"].ToString()));
                //        }

                //        ds = DataAccess.ExecuteSPDataSet(sSQL1, parms1);

                //        if (department.Tables.Count == 0)
                //        {
                //            department = ds.Copy();
                //        }
                //        else
                //        {
                //            foreach (DataRow dr4 in ds.Tables[0].Rows)
                //            {
                //                bool flag = true;
                //                foreach (DataRow dr7 in department.Tables[0].Rows)
                //                {
                //                    if (dr4["AddType"].ToString() == dr7["AddType"].ToString())
                //                    {

                //                        dr7["AddAmt"] = Convert.ToString(Convert.ToDouble(dr7["AddAmt"].ToString()) + Convert.ToDouble(dr4["AddAmt"].ToString()));
                //                        department.AcceptChanges();
                //                        flag = false;
                //                    }
                //                }
                //                if (flag)
                //                {
                //                    DataRow drnew = department.Tables[0].NewRow();
                //                    Random rd = new Random();
                //                    drnew[0] = dr4[0].ToString();
                //                    drnew[1] = dr4[1].ToString();
                //                    drnew[2] = rd.Next().ToString();
                //                    department.Tables[0].Rows.Add(drnew);
                //                    department.AcceptChanges();
                //                }
                //                //department.Tables[0].Rows.Add(dr4);
                //            }

                //            foreach (DataRow dr5 in ds.Tables[1].Rows)
                //            {
                //                //department.Tables[1].Rows.Add(dr5);

                //                bool flag = true;
                //                foreach (DataRow dr7 in department.Tables[1].Rows)
                //                {
                //                    if (dr5["AddType"].ToString() == dr7["AddType"].ToString())
                //                    {

                //                        dr7["AddAmt"] = Convert.ToString(Convert.ToDouble(dr7["AddAmt"].ToString()) + Convert.ToDouble(dr5["AddAmt"].ToString()));
                //                        department.AcceptChanges();
                //                        flag = false;
                //                    }
                //                }
                //                if (flag)
                //                {
                //                    DataRow drnew = department.Tables[1].NewRow();
                //                    Random rd = new Random();
                //                    drnew[0] = dr5[0].ToString();
                //                    drnew[1] = dr5[1].ToString();
                //                    drnew[2] = rd.Next().ToString();
                //                    department.Tables[1].Rows.Add(drnew);
                //                    department.AcceptChanges();
                //                }
                //            }

                //        }
                //    }

                //}

                //string strEarnings = "";
                //string strours = "";

                //strEarnings = "<table valign=\"top\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\"  width=\"100%\"><tr><td>RegularPay</td><td>" + item["Basic"].Text.ToString() + "</td></tr>";
                //strEarnings = strEarnings + "<tr><td>Employer CPF</td><td>" + item["employercpfamt"].Text.ToString() + " </td></tr>";
                //strours = "<table valign=\"top\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\" width=\"100%\"><tr><td>" + "-" + "</td></tr><tr><td>-</td></tr>";

                //// if (department.Tables.Count > 0)
                //// {
                //foreach (DataRow drnew in department.Tables[0].Rows)
                //{
                //    string strValue = drnew[0].ToString().Trim();
                //    //strValue = strValue.Remove(0, strValue.Length - 4);
                //    if (strValue == "OT1")
                //    {
                //        strours = strours + "<tr><td>" + item["OT1Hrs"].Text.ToString() + "</td></tr>";
                //    }
                //    else if (strValue == "OT2")
                //    {
                //        strours = strours + "<tr><td>" + item["OT2Hrs"].Text.ToString() + "</td></tr>";
                //    }
                //    else
                //    {
                //        strours = strours + "<tr><td>" + "-" + "</td></tr>";
                //    }
                //    //strours = strours + "<tr><td>" + "-" + "</td></tr>";
                //    strEarnings = strEarnings + "<tr><td>" + drnew[0].ToString() + "</td><td>" + drnew[1].ToString() + "</td></tr>";
                //}
                ////}

                //strEarnings = strEarnings + "</table>";
                //strours = strours + "</table>";

                //if (strEarnings.Length > 0)
                //{
                //    item["TotalAdditionsWONH"].Text = strEarnings;// "Regular Pay     " + item["Basic"].Text;
                //}
                //if (strours.Length > 0)
                //{
                //    item["OT1Hrs"].Text = strours;// "Regular Pay    " + item["Basic"].Text;
                //}

                //string strDeductions = "";
                //strDeductions = "<table valign=\"top\" style=\"font:10px; font-family:Courier New\" Height=\"100%\" width=\"100%\"><tr><td>Employee Cpf " + "</td><td> " + item["employeecpfamt"].Text.ToString() + " </td></tr>";

                ////if (item["fundtype"].Text != "")
                ////{
                ////    strDeductions = strDeductions + "<tr><td>" + item["fundtype"].Text + "</td><td>  " + item["fundamount"].Text + "  </td></tr>";
                ////}
                ////if (department.Tables.Count > 0)
                //// {
                //foreach (DataRow drnew1 in department.Tables[1].Rows)
                //{
                //    //if (dr[0].ToString().Contains("Fund") || dr[0].ToString().Contains("CPF Rate") || dr[0].ToString().Contains("CPF"))
                //    //{

                //    //}
                //    //else
                //    //{
                //    strDeductions = strDeductions + "<tr><td>" + drnew1[0].ToString() + "</td><td> " + drnew1[1].ToString() + " </td></tr>";
                //    //}
                //}
                //// }
                //if (strDeductions.Length > 0)
                //{
                //    strDeductions = strDeductions + "</table>";
                //    item["TotalDeductions"].Text = strDeductions;
                //}




                //string sqlQuery = "Select  count(*) New from employee where dept_id in (Select id from department Where Company_id=" + Utility.ToInteger(Session["compid"]).ToString() + "  and DeptName ='" + item["DeptName"].Text.ToString().Trim() + "' ) ";
                //sqlQuery = sqlQuery + "AND joining_date >'" + dtStartDate.ToString() + "' AND joining_date <'" + dtEndDate.ToString() + "'  And termination_date is null Union All ";
                //sqlQuery = sqlQuery + "Select  count(*) Existing from employee where dept_id in (Select id from department Where Company_id= " + Utility.ToInteger(Session["compid"]).ToString() + "  and DeptName ='" + item["DeptName"].Text.ToString().Trim() + "' ) ";
                //sqlQuery = sqlQuery + " AND joining_date <'" + dtStartDate.ToString() + "' AND (termination_date >'" + dtEndDate.ToString() + "' OR termination_date is Null)  Union All ";
                //sqlQuery = sqlQuery + " Select  count(*)  Resigned from employee where  dept_id in ";
                //sqlQuery = sqlQuery + "(Select id from department Where Company_id=" + Utility.ToInteger(Session["compid"]).ToString() + " and DeptName ='" + item["DeptName"].Text.ToString().Trim() + "')";
                //sqlQuery = sqlQuery + "AND termination_date >'" + dtStartDate.ToString() + "' AND termination_date <'" + dtEndDate.ToString() + "'  And termination_date is Not null";

                //DataSet dsdept = new DataSet();

                //dsdept = DataAccess.FetchRS(CommandType.Text, sqlQuery, null);
                //string deptName1 = "";

                //if (dsdept != null)
                //{
                //    deptName1 = item["DeptName"].Text + " \n ";
                //    deptName1 = deptName1 + " New     :" + dsdept.Tables[0].Rows[0][0].ToString();
                //    deptName1 = deptName1 + " Existing:" + dsdept.Tables[0].Rows[1][0].ToString();
                //    deptName1 = deptName1 + " Resigned:" + dsdept.Tables[0].Rows[2][0].ToString();
                //}

                //if (deptName1.Length > 0)
                //{
                //    if (dsdept.Tables.Count > 0)
                //    {
                //        if (dsdept.Tables[0].Rows[0][0].ToString() == "0" && dsdept.Tables[0].Rows[1][0].ToString() == "0" && dsdept.Tables[0].Rows[2][0].ToString() == "0")
                //        {

                //        }
                //        else
                //        {
                //            item["DeptName"].Text = deptName1;
                //        }
                //    }
                //}

            }
        }


        protected void bindgrid(object sender, ImageClickEventArgs e)
        {
            if (strEmpvisible != "")
            {
                Session["EmpPassID"] = strEmpvisible;
            }
            else
            {
                Session["EmpPassID"] = "";
            }

            btnsubapprove.Enabled = true;
            CallBeforeMonthFill();
            SetControlDate();
            intcnt = 1;
            //cmbYear.Enabled = false;
            //cmbMonth.Enabled = false;
            //imgbtnfetch.Enabled = false;

            //deptID.Enabled = false;



            RadGrid1.DataBind();
            if (chkList.SelectedValue == "Summary" )
            {
                //sp_GeneratePayRollAdv

                //DataSet ds = new DataSet();
                //string ssql = "sp_GeneratePayRollAdv";// 0,2009,2
                //SqlParameter[] parms = new SqlParameter[3];
                //parms[i++] = new SqlParameter("@ROWID", "0");
                //parms[i++] = new SqlParameter("@YEARS", 0);
                //parms[i++] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
                //monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);

                //(System.Data.DataView)SqlDataSource.Select(arg);

                DataView dv = new DataView();
                dt = new DataTable();
                dv = (System.Data.DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                dt = dv.ToTable();



                //Get Dataset in temporary value

                DataSet dstemp = new DataSet();
                dstemp.Tables.Add(dt);

                string deptName = "select DeptName from department where Company_id =" + Utility.ToInteger(Session["compid"]);
                SqlDataReader dr;

                dr = DataAccess.ExecuteReader(CommandType.Text, deptName, null);

                DataSet dsfinal = new DataSet();
                dsfinal = dstemp.Clone();

                while (dr.Read())
                {
                    string strdeptName = dr[0].ToString();
                    DataRow[] drread;
                    drread = dstemp.Tables[0].Select("deptname='" + strdeptName + "'");

                    if (drread.Length > 0)
                    {
                        //DataRow drnew = new DataRow();
                        DataRow drnew = dsfinal.Tables[0].NewRow();
                        drnew["deptname"] = strdeptName;


                        double days = 0;
                        double hours = 0;
                        double regPay = 0;
                        double employerCpf = 0;
                        double employeeCpf = 0;
                        double netPay1 = 0;
                        double basicPay = 0;
                        double hours1 = 0;
                        double cpfgross1 = 0;
                        double GrossWithAddition = 0;
                        double sdlCalcuations = 0;
                        foreach (DataRow drdata in drread)
                        {
                                        days = days + Convert.ToDouble(drdata["Days_Work"].ToString());
                                        hours = hours + Convert.ToDouble(drdata["OT1Hrs"].ToString());
                                        hours1 = hours1 + Convert.ToDouble(drdata["OT2Hrs"].ToString());
                                        regPay = regPay + Convert.ToDouble(drdata["Basic"].ToString());
                                        employerCpf = employerCpf + Convert.ToDouble(drdata["employercpfamt"].ToString());
                                        employeeCpf = employeeCpf + Convert.ToDouble(drdata["employeecpfamt"].ToString());
                                        netPay1 = netPay1 + Convert.ToDouble(drdata["netpay"].ToString());
                                        basicPay = basicPay + Convert.ToDouble(drdata["Basic"].ToString());
                                        cpfgross1 = cpfgross1 + Convert.ToDouble(drdata["cpfgross1"].ToString());
                                        GrossWithAddition = GrossWithAddition +Convert.ToDouble(drdata["GrossWithAddition"].ToString());

                                        //SDL calcuations
                                        // string strprdate = e.Row.Cells[21].Text;sdf_required SDLFundGrossAmount FundGrossAmount time_card_no
                                        string sdfRequired = drdata["sdf_required"].ToString();

                                        double sdlamount = Utility.ToDouble(drdata["SDLFundGrossAmount"].ToString());
                                       // double sdlamount = Utility.ToDouble(drdata["CPFGross1"].ToString());
                            
                                        //if (sdfRequired.Trim() == "2")
                                        //{
                                        //    if (sdlamount > 0)
                                        //    {
                                        //        sdlamount = Utility.ToDouble(drdata["CPFGross1"].ToString());
                                        //    }
                                        //    else
                                        //    {
                                        //        sdlamount = Utility.ToDouble(drdata["FundGrossAmount"].ToString());
                                        //    }
                                        //}

                                        //    DateTime dbPrdate1 = new DateTime();
                                        //    if (strprdate != "")
                                        //    {
                                        //        //dbPrdate1 = Convert.ToDateTime(strprdate);
                                        //    }
                                        //    /********************************************************************************************/
                                        double sdlfundtamt = 0.00;
                              //if (sdlamount != 0 && drdata["time_card_no"].ToString() != "" && drdata["time_card_no"].ToString() != "&nbsp;")
                              //          {
                                        if (sdlamount != 0)
                                        {
                                            int intmonth = Utility.ToInteger(Request.QueryString["monthidintbl"]) - 1;
                                            double fundgrossamount = Utility.ToDouble(sdlamount);
                                            //string sSql = "Select dbo.fn_getSDLAmount(" + fundgrossamount.ToString() + ")";

                                            //////////////////////////////////////////Get Payroll Month
                                            string sqlmonth = "SELECT Month FROM payrollmonthlydetail WHERE ROWID=" + intmonth;
                                            SqlDataReader dr9;
                                            dr9 = DataAccess.ExecuteReader(CommandType.Text, sqlmonth, null);
                                            int intactualmonth = 1;
                                            while (dr9.Read())
                                            {
                                                intactualmonth = Convert.ToInt32(dr9[0].ToString());
                                            }
                                            string sSql = "Select dbo.fn_getSDLAmount(" + fundgrossamount.ToString() + ")";
                                            // string sSql = "Select dbo.fn_getSDLAmount(" + fundgrossamount.ToString() + "," + intactualmonth + "," + Year + ")";
                                            ////////////////////////////////////////emp_code
                                            string strSqlDed = "; Select isnull(sum(SDL),0) SDLAmt From prepare_payroll_detail pd Inner Join prepare_payroll_hdr ph on pd.trx_id = ph.trx_id Where ([Status]='A' Or [Status]='G' Or [Status]='P') And Emp_ID=" + drdata["emp_code"].ToString() + " And ph.end_period=(SELECT PaySubEndDate FROM   PayrollMonthlyDetail  WHERE  ROWID = (" + intmonth.ToString() + "))";

                                            DataSet dsSDL = new DataSet();
                                            dsSDL = DataAccess.FetchRS(CommandType.Text, (sSql + strSqlDed), null);
                                            if (dsSDL != null)
                                            {
                                                if (dsSDL.Tables[0].Rows.Count > 0)
                                                {
                                                    if (Utility.ToDouble(dsSDL.Tables[0].Rows[0][0].ToString()) <= 0)
                                                    {
                                                        //if (intmonth == 12)
                                                        //{
                                                        //  trsdl.Attributes.Add("style", "display:block");
                                                        //}
                                                        //else
                                                        //{
                                                        //  trsdl.Attributes.Add("style", "display:none");
                                                        //}
                                                    }
                                                    else
                                                    {
                                                        //trsdl.Attributes.Add("style", "display:block");
                                                        if (Utility.ToInteger(Session["PaySubStartDay"].ToString()) > 1)
                                                        {
                                                            double sdlAmount = Utility.ToDouble(Utility.ToDouble(dsSDL.Tables[0].Rows[0][0].ToString()) - Utility.ToDouble(dsSDL.Tables[1].Rows[0][0].ToString()));
                                                            if (sdlAmount < 0)
                                                            {
                                                                sdlfundtamt = 0.00;
                                                            }
                                                            else
                                                            {
                                                                sdlfundtamt = Convert.ToDouble(sdlAmount.ToString());
                                                            }
                                                        }
                                                        else
                                                        {
                                                            sdlfundtamt = Convert.ToDouble(dsSDL.Tables[0].Rows[0][0].ToString());
                                                        }
                                                        //If PR Date is >1 then overwrite SDL with old salary ...
                                                        //if (dtPRdate1 != null)
                                                        //{
                                                        //    //stmonth
                                                        //    // dtTerm = DateTime.Parse(dr["Term_Date"].ToString(), format);
                                                        //    //lblTermDate.Text = dtTerm.ToString("dd/MM/yyyy");
                                                        //    //Check for Month processing salary
                                                        //    string stmonth = Utility.ToString(Request.QueryString["stdatesubmonth"]);
                                                        //    DateTime dt1 = DateTime.Parse(stmonth, format);
                                                        //    if (dtPRdate1.Date.Day > 1 && dtPRdate1.Date.Month == dt1.Month && dtPRdate1.Date.Year == dt1.Year)
                                                        //    {
                                                        //        e.Row.Cells[1].Text = dsSDL.Tables[1].Rows[0][0].ToString();
                                                        //    }
                                                        //}
                                                        //}
                                                    }
                                                }
                                            }
                                        }

                                        sdlCalcuations = sdlCalcuations + sdlfundtamt;                



                        }
                        drnew["Days_Work"] = days.ToString();
                        drnew["OT1Hrs"] = hours.ToString();
                        drnew["OT2Hrs"] = hours1.ToString();
                        drnew["Basic"] = regPay.ToString();
                        drnew["employercpfamt"] = employerCpf.ToString();
                        drnew["employeecpfamt"] = employeeCpf.ToString();
                        drnew["netpay"] = netPay1.ToString();
                        drnew["Basic"] = basicPay.ToString();
                        drnew["cpfgross1"] = cpfgross1.ToString();
                        drnew["GrossWithAddition"] = GrossWithAddition.ToString();
                        drnew["Trade"] = sdlCalcuations.ToString();
                        dsfinal.Tables[0].Rows.Add(drnew);
                    }
                }
                //Department Radgrid2
                //Change Dataset as per values 
                RadGrid2.DataSource = dsfinal;
                RadGrid2.DataBind();
            }


            if (chkList.SelectedValue == "Company")
            {
                //sp_GeneratePayRollAdv

                //DataSet ds = new DataSet();
                //string ssql = "sp_GeneratePayRollAdv";// 0,2009,2
                //SqlParameter[] parms = new SqlParameter[3];
                //parms[i++] = new SqlParameter("@ROWID", "0");
                //parms[i++] = new SqlParameter("@YEARS", 0);
                //parms[i++] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
                //monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);

                //(System.Data.DataView)SqlDataSource.Select(arg);

                DataView dv = new DataView();
                dt = new DataTable();
                dv = (System.Data.DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
                dt = dv.ToTable();
                

                //Get Dataset in temporary value

                DataSet dstemp = new DataSet();
                dstemp.Tables.Add(dt);

                string deptName = "select DeptName from department where Company_id =" + Utility.ToInteger(Session["compid"]);
                SqlDataReader dr;

                dr = DataAccess.ExecuteReader(CommandType.Text, deptName, null);

                DataSet dsfinal = new DataSet();
                dsfinal = dstemp.Clone();

                double days = 0;
                double hours = 0;
                double regPay = 0;
                double employerCpf = 0;
                double employeeCpf = 0;
                double netPay1 = 0;
                double basicPay = 0;
                double hours1 = 0;
                double CPFGross1 = 0;
                double GrossWithAddition = 0;
                double sdlCalcuations = 0;
                double totalunpaid = 0;
                foreach (DataRow drdata in dstemp.Tables[0].Rows)
                {
                    days = days + Convert.ToDouble(drdata["Days_Work"].ToString());
                    hours = hours + Convert.ToDouble(drdata["OT1Hrs"].ToString());
                    hours1 = hours1 + Convert.ToDouble(drdata["OT2Hrs"].ToString());
                    regPay = regPay + Convert.ToDouble(drdata["Basic"].ToString());
                    employerCpf = employerCpf + Convert.ToDouble(drdata["employercpfamt"].ToString());
                    employeeCpf = employeeCpf + Convert.ToDouble(drdata["employeecpfamt"].ToString());
                    netPay1 = netPay1 + Convert.ToDouble(drdata["netpay"].ToString());
                    basicPay = basicPay + Convert.ToDouble(drdata["Basic"].ToString());
                    CPFGross1 = CPFGross1 + Convert.ToDouble(drdata["CPFGross1"].ToString());
                    GrossWithAddition = GrossWithAddition + Convert.ToDouble(drdata["GrossWithAddition"].ToString());
                    totalunpaid = totalunpaid + Convert.ToDouble(drdata["totalunpaid"].ToString());


                            //SDL calcuations
                            // string strprdate = e.Row.Cells[21].Text;sdf_required SDLFundGrossAmount FundGrossAmount time_card_no
                            string sdfRequired = drdata["sdf_required"].ToString();

                            double sdlamount = Utility.ToDouble(drdata["SDLFundGrossAmount"].ToString()); 
                            //if (sdfRequired.Trim() == "2")
                            //{
                            //    if (sdlamount > 0)
                            //    {
                            //        sdlamount = Utility.ToDouble(drdata["CPFGross1"].ToString());
                            //    }
                            //    else
                            //    {
                            //        sdlamount = Utility.ToDouble(drdata["FundGrossAmount"].ToString());
                            //    }
                            //}

                            //    DateTime dbPrdate1 = new DateTime();
                            //    if (strprdate != "")
                            //    {
                            //        //dbPrdate1 = Convert.ToDateTime(strprdate);
                            //    }
                            //    /********************************************************************************************/
                            double sdlfundtamt = 0.00;

                            if (sdlamount != 0)
                            {
                                int intmonth = Utility.ToInteger(Request.QueryString["monthidintbl"]) - 1;
                                double fundgrossamount = Utility.ToDouble(sdlamount);
                                //string sSql = "Select dbo.fn_getSDLAmount(" + fundgrossamount.ToString() + ")";

                                //////////////////////////////////////////Get Payroll Month
                                string sqlmonth = "SELECT Month FROM payrollmonthlydetail WHERE ROWID=" + intmonth;
                                SqlDataReader dr9;
                                dr9 = DataAccess.ExecuteReader(CommandType.Text, sqlmonth, null);
                                int intactualmonth = 1;
                                while (dr9.Read())
                                {
                                    intactualmonth = Convert.ToInt32(dr9[0].ToString());
                                }
                                string sSql = "Select dbo.fn_getSDLAmount(" + fundgrossamount.ToString() + ")";
                                // string sSql = "Select dbo.fn_getSDLAmount(" + fundgrossamount.ToString() + "," + intactualmonth + "," + Year + ")";
                                ////////////////////////////////////////emp_code
                                string strSqlDed = "; Select isnull(sum(SDL),0) SDLAmt From prepare_payroll_detail pd Inner Join prepare_payroll_hdr ph on pd.trx_id = ph.trx_id Where ([Status]='A' Or [Status]='G' Or [Status]='P') And Emp_ID=" + drdata["emp_code"].ToString() + " And ph.end_period=(SELECT PaySubEndDate FROM   PayrollMonthlyDetail  WHERE  ROWID = (" + intmonth.ToString() + "))";

                                DataSet dsSDL = new DataSet();
                                dsSDL = DataAccess.FetchRS(CommandType.Text, (sSql + strSqlDed), null);
                                if (dsSDL != null)
                                {
                                    if (dsSDL.Tables[0].Rows.Count > 0)
                                    {
                                        if (Utility.ToDouble(dsSDL.Tables[0].Rows[0][0].ToString()) <= 0)
                                        {
                                            //if (intmonth == 12)
                                            //{
                                            //  trsdl.Attributes.Add("style", "display:block");
                                            //}
                                            //else
                                            //{
                                            //  trsdl.Attributes.Add("style", "display:none");
                                            //}
                                        }
                                        else
                                        {
                                            //trsdl.Attributes.Add("style", "display:block");
                                            if (Utility.ToInteger(Session["PaySubStartDay"].ToString()) > 1)
                                            {
                                                double sdlAmount = Utility.ToDouble(Utility.ToDouble(dsSDL.Tables[0].Rows[0][0].ToString()) - Utility.ToDouble(dsSDL.Tables[1].Rows[0][0].ToString()));
                                                if (sdlAmount < 0)
                                                {
                                                    sdlfundtamt = 0.00;
                                                }
                                                else
                                                {
                                                    sdlfundtamt = Convert.ToDouble(sdlAmount.ToString());
                                                }
                                            }
                                            else
                                            {
                                                sdlfundtamt = Convert.ToDouble(dsSDL.Tables[0].Rows[0][0].ToString());
                                            }
                                            //If PR Date is >1 then overwrite SDL with old salary ...
                                            //if (dtPRdate1 != null)
                                            //{
                                            //    //stmonth
                                            //    // dtTerm = DateTime.Parse(dr["Term_Date"].ToString(), format);
                                            //    //lblTermDate.Text = dtTerm.ToString("dd/MM/yyyy");
                                            //    //Check for Month processing salary
                                            //    string stmonth = Utility.ToString(Request.QueryString["stdatesubmonth"]);
                                            //    DateTime dt1 = DateTime.Parse(stmonth, format);
                                            //    if (dtPRdate1.Date.Day > 1 && dtPRdate1.Date.Month == dt1.Month && dtPRdate1.Date.Year == dt1.Year)
                                            //    {
                                            //        e.Row.Cells[1].Text = dsSDL.Tables[1].Rows[0][0].ToString();
                                            //    }
                                            //}
                                            //}
                                        }
                                    }
                                }
                            }

                    sdlCalcuations =sdlCalcuations+ sdlfundtamt;
                }
                //while (dr.Read())
                //{
                //    string strdeptName = dr[0].ToString();
                //    DataRow[] drread;
                //    drread = dstemp.Tables[0].Select("deptname='" + strdeptName + "'");

                //    if (drread.Length > 0)
                //    {
                //        //DataRow drnew = new DataRow();
                      
                     

                //        foreach (DataRow drdata in drread)
                //        {
                           
                //        }
                //        drnew["Days_Work"] = days.ToString();
                //        drnew["OT1Hrs"] = hours.ToString();
                //        drnew["OT2Hrs"] = hours1.ToString();
                //        drnew["Basic"] = regPay.ToString();
                //        drnew["employercpfamt"] = employerCpf.ToString();
                //        drnew["employeecpfamt"] = employeeCpf.ToString();
                //        drnew["netpay"] = netPay1.ToString();
                //        drnew["Basic"] = basicPay.ToString();
                //    }
                //}

                DataRow drnew = dsfinal.Tables[0].NewRow();

                drnew["Days_Work"]       = days.ToString();
                drnew["OT1Hrs"]          = hours.ToString();
                drnew["OT2Hrs"]          = hours1.ToString();
                drnew["Basic"]           = regPay.ToString();
                drnew["employercpfamt"]  = employerCpf.ToString();
                drnew["employeecpfamt"]  = employeeCpf.ToString();
                drnew["netpay"]          = netPay1.ToString();
                drnew["Basic"]           = basicPay.ToString();
                drnew["CPFGross1"]       = CPFGross1.ToString();
                drnew["GrossWithAddition"] = GrossWithAddition.ToString();
                drnew["Trade"] = sdlCalcuations.ToString();
                drnew["totalunpaid"] = totalunpaid.ToString();
                    
                dsfinal.Tables[0].Rows.Add(drnew);

                //Change Dataset as per values 
                //for (int col = dsfinal.Tables[0].Columns.Count - 1; col >= 0; col--)
                //{
                //    bool removeColumn = true;
                //    foreach (DataRow row in dsfinal.Tables[0].Rows)
                //    {
                //        if (!row.IsNull(col))
                //        {
                //            removeColumn = false;
                //            break;
                //        }
                //    }
                //    if (removeColumn)
                //        dsfinal.Tables[0].Columns.RemoveAt(col);
                //}

                RadGrid5.DataSource = dsfinal;
                RadGrid5.DataBind();
            }


            if (chkList.SelectedValue == "Recon")
            {
                            DataView dv = new DataView();
                            dt = new DataTable();
                            dv = (System.Data.DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                            dt = dv.ToTable();

                            //Get Dataset in temporary value
                            DataSet dstemp = new DataSet();
                            dstemp.Tables.Add(dt);
                    
                         

                            //DataRow drnew = new DataRow();
                            DataRow drnew = dstemp.Tables[0].NewRow();
                            drnew["FullName"] = "Less:";

                            string strstartMonth = "";
                            string strEndMonth = "";

                            strstartMonth = cmbMonth.SelectedItem.Text + " : " + cmbYear.SelectedItem.Text;
                            string year = "";
                            if (cmbMonth.SelectedIndex == 0 || cmbMonth.SelectedIndex == 12)
                            {
                                strEndMonth = cmbMonth.Items[cmbMonth.Items.Count - 1].Text + " : " + cmbYear.Items[cmbYear.SelectedIndex - 1].Text;
                                year = cmbYear.Items[cmbYear.SelectedIndex - 1].Text;  
                            }
                            else
                            {
                                strEndMonth = cmbMonth.Items[cmbMonth.SelectedIndex - 1].Text + " : " + cmbYear.Items[cmbYear.SelectedIndex].Text;
                                year = cmbYear.Items[cmbYear.SelectedIndex].Text;
                            }
                            //  strEndMonth = "";

                            decimal basicCM = 0;
                            decimal basicLM = 0;


                            foreach (DataRow dr in dstemp.Tables[0].Rows)
                            {
                                if (dr["Basic"].ToString() != "")
                                {
                                    basicCM = basicCM + Convert.ToDecimal(dr["Basic"].ToString());
                                }
                            }

                            //Get Data For Previous Month .....
                            string monthPrev = "";
                            int rowids = 0;
                            rowids = Convert.ToInt32(cmbMonth.Items[cmbMonth.SelectedIndex].Value) - 1;
                            DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + rowids);
                            foreach (DataRow drnew1 in drResults)
                            {
                                monthPrev = drnew1["Month"].ToString();
                            }
                            int Month = Convert.ToInt32(monthPrev);

                            string paySD = "";
                            string payED = "";
                            string paySSD = "";
                            string paySED = "";
                            string paySSDa = "";
                            string paySEDa = "";
                            

                            foreach (DataRow dr in drResults)
                            {
                                paySD = dr["PayStartDay"].ToString();
                                payED = dr["PayEndDay"].ToString();
                                paySSD = dr["PaySubStartDay"].ToString();
                                paySED = dr["PaySubEndDay"].ToString();
                                paySSDa = dr["PaySubStartDate"].ToString();
                                paySEDa = dr["PaySubEndDate"].ToString();
                            }

                          
                            int i = 0;

                            DataSet dsprev = new DataSet();
                            string ssql = "sp_ApprovePayRoll";// 0,2009,2
                            SqlParameter[] parms = new SqlParameter[6];
                            parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(Session["compid"]));
                            parms[i++] = new SqlParameter("@month", Utility.ToInteger(rowids));
                            parms[i++] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"]));

                            parms[i++] = new SqlParameter("@year", Utility.ToInteger(year));
                            parms[i++] = new SqlParameter("@Status", "G");
                            if (deptID.Items[deptID.SelectedIndex].Text == "ALL")
                            {
                                parms[i++] = new SqlParameter("@DeptId", -1);
                            }
                            else
                            {
                                parms[i++] = new SqlParameter("@DeptId", deptID.SelectedValue);
                            }
                            dsprev = DataAccess.ExecuteSPDataSet(ssql, parms);

                          

                            if (dsprev.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dsprev.Tables[0].Rows)
                                {
                                    basicLM = basicLM + Convert.ToDecimal(dr["basic_pay"].ToString());
                                }                            
                            }

                            decimal diff =  Convert.ToDecimal(basicCM - basicLM);                          
                                
                            //string s=string.Format("{0:0.00}",d); 
                            string strPayment = "";
                            strPayment = "<table valign=\"top\"table border=\"1\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\"  width=\"50%\"><tr><td>Regular Pay for " + strstartMonth + "</td><td>" + basicCM.ToString() + "</td></tr>";
                            strPayment = strPayment + "<tr style=\"border:1px;border-style:none\"><td> " + "Regular Pay for " + strEndMonth + " </td><td> " + basicLM.ToString() + " </td></tr>";
                            strPayment = strPayment + "<tr style=\"border:1px;border-style:none\"><td> " + "Regular Pay Adjustment </td><td>" + diff.ToString() + " </td></tr></table>";
                            drnew["DeptName"] = strPayment;
                            drnew["emp_code"] = -1;          
                            dstemp.Tables[0].Rows.Add(drnew);

                            //New Row For Resigned Employyes in current Month ...

                            //Get The Employee ID resigned in current Month

                                            string monthstr = "";
                                            DataRow[] drResults1 = monthDs.Tables[0].Select("RowID = " + Convert.ToInt32(cmbMonth.Items[cmbMonth.SelectedIndex].Value));                                            
                                            foreach (DataRow drnew1 in drResults1)
                                            {
                                                monthstr = drnew1["Month"].ToString();
                                            }
                                            int Month1 = Convert.ToInt32(monthstr);


                                            paySD = "";
                                            payED = "";
                                            paySSD = "";
                                            paySED = "";
                                            paySSDa = "";
                                            paySEDa = "";

                                            foreach (DataRow dr in drResults1)
                                            {
                                                paySD = dr["PayStartDay"].ToString();
                                                payED = dr["PayEndDay"].ToString();
                                                paySSD = dr["PaySubStartDay"].ToString();
                                                paySED = dr["PaySubEndDay"].ToString();
                                                paySSDa = dr["PaySubStartDate"].ToString();
                                                paySEDa = dr["PaySubEndDate"].ToString();
                                            }

                                            string dtStartDate;
                                            string dtEndDate;

                                            dtStartDate = Convert.ToString(Month1.ToString() + "/" + paySSD + "/" + Utility.ToString(Request.QueryString["Year"]));
                                            dtEndDate = Convert.ToString(Month1.ToString() + "/" + paySED + "/" + Utility.ToString(Request.QueryString["Year"]));

                                            string sqlQuery = "";
                                            string deptName1 = deptID.Items[deptID.SelectedIndex].Text;

                                            
                                            if (deptName1 == "ALL")
                                            {
                                                sqlQuery = " Select  emp_code,termination_date from employee where  ";
                                                sqlQuery = sqlQuery + " convert(Datetime, termination_date,103) >='" + dtStartDate.ToString() + "' AND convert(Datetime, termination_date,103) <='" + dtEndDate.ToString() + "'  And convert(Datetime, termination_date,103) is Not null and Company_Id=" + Utility.ToInteger(Session["compid"]);
                                            }
                                            else
                                            {
                                                sqlQuery = " Select  emp_code,termination_date from employee where  dept_id in ";
                                                sqlQuery = sqlQuery + "(Select id from department Where Company_id=" + Utility.ToInteger(Session["compid"]).ToString() + " and DeptName ='" + deptName1.Trim() + "')";
                                                sqlQuery = sqlQuery + "AND convert(Datetime, termination_date,103) >='" + dtStartDate.ToString() + "' AND convert(Datetime, termination_date,103) <='" + dtEndDate.ToString() + "'  And convert(Datetime, termination_date,103) is Not null and Company_Id=" + Utility.ToInteger(Session["compid"]);
                                            }

                                            DataSet empcodeds = new DataSet();
                                            empcodeds = DataAccess.FetchRS(CommandType.Text, sqlQuery, null);
                                            //DataRow drnew = new DataRow();   

                                            int count1 = 0;
                                            decimal total = 0;
                                            if (empcodeds.Tables[0].Rows.Count > 0)
                                            {
                                                string strResign = "";
                                                DataRow drnew1 = dstemp.Tables[0].NewRow();
                                                foreach (DataRow drnew3 in empcodeds.Tables[0].Rows)
                                                {
                                                    DateTime dtresign=new DateTime();

                                                    decimal balance = 0;
                                                    decimal monthly = 0;
                                                    decimal prorated = 0;

                                                    if (drnew3["termination_date"].ToString() != "")
                                                    {
                                                        dtresign = Convert.ToDateTime(drnew3["termination_date"].ToString());
                                                    }
                                                    if (count1 == 0)
                                                    {
                                                        if (dsprev.Tables[0].Rows.Count > 0)
                                                        {
                                                            foreach (DataRow dr in dsprev.Tables[0].Rows)
                                                            {
                                                                if (dr["emp_id"].ToString() == drnew3["emp_code"].ToString())
                                                                {
                                                                    monthly= Convert.ToDecimal(dr["basic_pay"].ToString());
                                                                }
                                                            }
                                                        }

                                                        foreach (DataRow dr in dstemp.Tables[0].Rows)
                                                        {
                                                            if (dr["emp_code"].ToString() == drnew3["emp_code"].ToString())
                                                            {
                                                                balance = monthly - Convert.ToDecimal(dr["Basic"].ToString());
                                                            }
                                                        }


                                                        string timecard = "Select time_card_no + '  ( ' + emp_name + ' ' + emp_lname + ' ) '  From employee where emp_code=" + drnew3["Emp_Code"].ToString() + " and Company_Id=" + Utility.ToInteger(Session["compid"]);

                                                        SqlDataReader dr4 = null;

                                                        dr4 = DataAccess.ExecuteReader(CommandType.Text, timecard, null);

                                                        timecard = "";
                                                        while (dr4.Read())
                                                        {
                                                            if (dr4[0] != null)
                                                            {
                                                                timecard = dr4[0].ToString();
                                                            }
                                                        }
                                                        prorated = monthly - balance;                                                      
                                                        total = total + balance;
                                                        drnew1["FullName"] = "Less:";
                                                        strResign = "<table border=\"1\" valign=\"top\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\"  width=\"50%\"><tr  style=\"border:1px\"><td>Resignation for " + strstartMonth + "</td><td>EmpCode</td><td>Resigned</td><td>Balance</td><td>Monthly</td><td>Prorated</td></tr>";
                                                        strResign = strResign + "<tr style=\"border:1px;border-style:none\"><td></td><td>" + timecard + "</td><td>" + dtresign.Date.Day + "/" + dtresign.Date.Month + "/" + dtresign.Date.Year + "</td><td>" + balance.ToString() + "</td><td>" + monthly.ToString() + "</td><td>" + prorated.ToString() + " </td></tr>";
                                                    }
                                                    else
                                                    {
                                                        if (dsprev.Tables[0].Rows.Count > 0)
                                                        {
                                                            foreach (DataRow dr in dsprev.Tables[0].Rows)
                                                            {
                                                                if (dr["emp_id"].ToString() == drnew3["emp_code"].ToString())
                                                                {
                                                                    monthly = Convert.ToDecimal(dr["basic_pay"].ToString());
                                                                }
                                                            }
                                                        }

                                                        foreach (DataRow dr in dstemp.Tables[0].Rows)
                                                        {
                                                            if (dr["emp_code"].ToString() == drnew3["emp_code"].ToString())
                                                            {
                                                                balance = monthly - Convert.ToDecimal(dr["Basic"].ToString());
                                                            }
                                                        }
                                                        prorated = monthly - balance;
                                                        total = total + balance;


                                                        string timecard = "Select time_card_no + '  ( ' + emp_name + ' ' + emp_lname + ' ) '  From employee where emp_code=" + drnew3["Emp_Code"].ToString() + " and Company_Id=" + Utility.ToInteger(Session["compid"]);

                                                        SqlDataReader dr4 = null;

                                                        dr4 = DataAccess.ExecuteReader(CommandType.Text, timecard, null);

                                                        timecard = "";
                                                        while (dr4.Read())
                                                        {
                                                            if (dr4[0] != null)
                                                            {
                                                                timecard = dr4[0].ToString();
                                                            }
                                                        }
                                                        strResign = strResign + "<tr  style=\"border:1px;border-style:none\"><td></td><td>" + timecard + "</td><td>" + dtresign.Date.Day + "/" + dtresign.Date.Month + "/" + dtresign.Date.Year + "</td><td>" + balance.ToString() + "</td><td>" + monthly.ToString() + "</td><td>" + prorated.ToString() + " </td></tr>";
                                                        //strResign = "<table valign=\"top\"  style=\"font: 12px;font-family:Courier New\" Height=\"100%\"  width=\"50%\"><tr><td>Regular Pay for " + strstartMonth + "</td><td>" + basicCM.ToString() + "</td></tr>";
                                                        //strResign = strResign + "<tr><td> " + "Regular Pay for " + strEndMonth + " </td><td> " + basicLM.ToString() + " </td></tr>";
                                                        //strResign = strResign + "<tr><td> " + "Regular Pay Adjustment </td><td>" + diff.ToString() + " </td></tr></table>";                                                                             
                                                    }                                                    
                                                    count1 = count1 + 1;                                                   
                                                }
                                                strResign = strResign + "<tr  style=\"border:1px;border-style:none\"><td>Total</td><td></td><td></td><td>" + total.ToString() + "</td><td></td></tr>";
                                                strResign = strResign + "</table>";
                                                drnew1["DeptName"] = strResign;
                                                drnew1["emp_code"] = -1;
                                                dstemp.Tables[0].Rows.Add(drnew1);
                                                //DataRow drnew11 = dstemp.Tables[0].NewRow();
                                                //strResign = "<table><tr  style=\"border:1px;border-style:solid\"><td></td><td>Total</td><td></td><td>" + total.ToString() + "</td><td></td></tr></table>";
                                                //drnew11["FullName"] = "";
                                                //drnew11["DeptName"] = strResign;
                                                //drnew11["emp_code"] = -1;
                                                //dstemp.Tables[0].Rows.Add(drnew11);
                                            }

                                            //Add New Employee in Current month...

                                            sqlQuery = "";
                                            if (deptName1 == "ALL")
                                            {
                                                sqlQuery = " Select  emp_code,joining_date,convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'), payrate)) as payrate from employee where  Company_Id=" + Utility.ToInteger(Session["compid"]);
                                                sqlQuery = sqlQuery + " and  joining_date >='" + dtStartDate.ToString() + "' AND joining_date <='" + dtEndDate.ToString() + "'";
                                            }
                                            else
                                            {
                                                sqlQuery = " Select  emp_code,joining_date,convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'), payrate)) as payrate from employee where  dept_id in ";
                                                sqlQuery = sqlQuery + "(Select id from department Where Company_id=" + Utility.ToInteger(Session["compid"]).ToString() + " and DeptName ='" + deptName1.Trim() + "')";
                                                sqlQuery = sqlQuery + " AND joining_date >'" + dtStartDate.ToString() + "' AND joining_date <'" + dtEndDate.ToString() + "' AND Company_Id=" + Utility.ToInteger(Session["compid"]);
                                            }

                                            DataSet empJoin = new DataSet();
                                            empJoin = DataAccess.FetchRS(CommandType.Text, sqlQuery, null);

                                            count1 = 0;
                                            total = 0;
                                            if (empJoin.Tables[0].Rows.Count > 0)
                                            {
                                                string strJoined = "";
                                                DataRow drnew1 = dstemp.Tables[0].NewRow();
                                                foreach (DataRow drnew3 in empJoin.Tables[0].Rows)
                                                {
                                                    DateTime dtJoin = new DateTime();
                                                    decimal balance = 0;
                                                    decimal monthly = 0;
                                                    decimal prorated = 0;

                                                    if (drnew3["joining_date"].ToString() != "")
                                                    {
                                                        dtJoin = Convert.ToDateTime(drnew3["joining_date"].ToString());
                                                    }
                                                    if (count1 == 0)
                                                    {
                                                        if (dsprev.Tables[0].Rows.Count > 0)
                                                        {
                                                            foreach (DataRow dr in dsprev.Tables[0].Rows)
                                                            {
                                                                if (dr["emp_id"].ToString() == drnew3["emp_code"].ToString())
                                                                {
                                                                    balance = Convert.ToDecimal(dr["basic_pay"].ToString());
                                                                }
                                                            }
                                                        }

                                                        decimal basic_pay = 0;
                                                        foreach (DataRow dr in dstemp.Tables[0].Rows)
                                                        {
                                                            if (dr["emp_code"].ToString() == drnew3["emp_code"].ToString())
                                                            {
                                                                basic_pay = Convert.ToDecimal(dr["Basic"].ToString());
                                                            }
                                                        }
                                                        //prorated = monthly - balance;
                                                    
                                                        monthly = Convert.ToDecimal(drnew3["payrate"].ToString());
                                                          
                                                        prorated = basic_pay; // added by Su Mon
                                                        //total = total + balance;
                                                        total = total + prorated;
                                                        if (balance == 0)
                                                        {
                                                            balance = monthly - prorated;
                                                            total = prorated;
                                                        }
                                                        drnew1["FullName"] = "Add:";

                                                        string timecard = "Select time_card_no + '  ( ' + emp_name + ' ' + emp_lname + ' ) '  From employee where emp_code=" + drnew3["Emp_Code"].ToString() + " and  Company_Id=" + Utility.ToInteger(Session["compid"]);

                                                        SqlDataReader dr4 = null;

                                                        dr4 = DataAccess.ExecuteReader(CommandType.Text, timecard, null);

                                                        timecard = "";
                                                        while (dr4.Read())
                                                        {
                                                            if (dr4[0] != null)
                                                            {
                                                                timecard = dr4[0].ToString();
                                                            }
                                                        }

                                                        strJoined = "<table border=\"1\" valign=\"top\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\"  width=\"50%\"><tr  style=\"border:1px\"><td>New Employee for " + strstartMonth + "</td><td>EmpCode</td><td>Joined</td><td>Prorated</td><td>Monthly</td><td>Balance</td></tr>";
                                                        strJoined = strJoined + "<tr style=\"border:1px;border-style:none\"><td></td><td>" + timecard + "</td><td>" + dtJoin.Date.Day + "/" + dtJoin.Date.Month + "/" + dtJoin.Date.Year + "</td><td>" + prorated.ToString() + "</td><td>" + monthly.ToString() + "</td><td>" + balance.ToString() + " </td></tr>";
                                                    }
                                                    else
                                                    {
                                                        if (dsprev.Tables[0].Rows.Count > 0)
                                                        {
                                                            foreach (DataRow dr in dsprev.Tables[0].Rows)
                                                            {
                                                                if (dr["emp_id"].ToString() == drnew3["emp_code"].ToString())
                                                                {
                                                                    balance = Convert.ToDecimal(dr["basic_pay"].ToString());
                                                                }
                                                            }
                                                        }

                                                        foreach (DataRow dr in dstemp.Tables[0].Rows)
                                                        {
                                                            if (dr["emp_code"].ToString() == drnew3["emp_code"].ToString())
                                                            {
                                                                monthly = Convert.ToDecimal(dr["Basic"].ToString());
                                                            }
                                                        }
                                                        //prorated = monthly - balance;
                                                        prorated = monthly;
                                                        monthly = decimal.Parse(drnew3["payrate"].ToString());
                                                        balance = monthly - prorated;
                                                        total = total + prorated;
                                                        drnew1["FullName"] = "Add:";


                                                        String timecard1 = "Select time_card_no + '  ( ' + emp_name + ' ' + emp_lname + ' ) '  From employee where emp_code=" + drnew3["Emp_Code"].ToString();

                                                        SqlDataReader dr1;

                                                        dr1 = DataAccess.ExecuteReader(CommandType.Text, timecard1, null);

                                                        timecard1 = "";
                                                        while (dr1.Read())
                                                        {
                                                            if (dr1[0] != null)
                                                            {
                                                                timecard1 = dr1[0].ToString();
                                                            }
                                                        }

                                                        strJoined = strJoined + "<tr style=\"border:1px;border-style:none\"><td></td><td>" + timecard1 + "</td><td>" + dtJoin.Date.Day + "/" + dtJoin.Date.Month + "/" + dtJoin.Date.Year + "</td><td>" + balance.ToString() + "</td><td>" + monthly.ToString() + "</td><td>" + prorated.ToString() + " </td></tr>";
                                                    }
                                                    count1 = count1 + 1;

                                                }
                                                strJoined = strJoined + "<tr  style=\"border:1px;border-style:none\"><td>Total</td><td></td><td></td><td>" + total.ToString() + "</td><td></td></tr>";
                                                strJoined = strJoined + "</table>";
                                                drnew1["DeptName"] = strJoined;
                                                drnew1["emp_code"] = -1;
                                                dstemp.Tables[0].Rows.Add(drnew1);
                                            }
                                            /****************************Add employeess with salary increment in first month**************************************/
                                            sqlQuery = "";


                                            string monthstrprev = "";
                                            DataRow[] drResultsPrev = monthDs.Tables[0].Select("RowID = " + (Convert.ToInt32(cmbMonth.Items[cmbMonth.SelectedIndex].Value)-1));
                                            foreach (DataRow drnew1 in drResultsPrev)
                                            {
                                                monthstrprev = drnew1["Month"].ToString();
                                            }
                                            int Monthprev = Convert.ToInt32(monthstrprev);


                                            paySD = "";
                                            payED = "";
                                            paySSD = "";
                                            paySED = "";
                                            paySSDa = "";
                                            paySEDa = "";

                                            foreach (DataRow dr in drResultsPrev)
                                            {
                                                paySD = dr["PayStartDay"].ToString();
                                                payED = dr["PayEndDay"].ToString();
                                                paySSD = dr["PaySubStartDay"].ToString();
                                                paySED = dr["PaySubEndDay"].ToString();
                                                paySSDa = dr["PaySubStartDate"].ToString();
                                                paySEDa = dr["PaySubEndDate"].ToString();
                                            }

                                            string dtStartDatePrev;
                                            string dtEndDatePrev;
                                            if (Monthprev != 12)
                                            {
                                                dtStartDatePrev = Convert.ToString(Monthprev.ToString() + "/" + paySSD + "/" + Utility.ToString(Request.QueryString["Year"]));
                                                dtEndDatePrev = Convert.ToString(Monthprev.ToString() + "/" + paySED + "/" + Utility.ToString(Request.QueryString["Year"]));
                                            }
                                            else
                                            {
                                                dtStartDatePrev = Convert.ToString(Monthprev.ToString() + "/" + paySSD + "/" + Utility.ToString(Utility.ToInteger(Request.QueryString["Year"])-1));
                                                dtEndDatePrev = Convert.ToString(Monthprev.ToString() + "/" + paySED + "/" + Utility.ToString(Utility.ToInteger(Request.QueryString["Year"]) - 1));
                                            }

                                            
                                            if (deptName1 == "ALL")
                                            {
                                                //sqlQuery = " Select  emp_code,joining_date from employee where  Company_Id=" + Utility.ToInteger(Session["compid"]);
                                                //sqlQuery = sqlQuery + " and  joining_date >='" + dtStartDate.ToString() + "' AND joining_date <='" + dtEndDate.ToString() + "'";
                                                sqlQuery = "select convert(varchar(10), EP.FromDate,103) EffDate,E.emp_name,convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),EP.Payrate)) Salary,";
                                                sqlQuery =sqlQuery  + " E.emp_code,E.time_card_no TimeCard  from dbo.EmployeePayHistory EP,Employee E ";
                                                sqlQuery =sqlQuery  + " where   convert(Datetime, E.termination_date,103)  is  null  AND EP.Emp_ID = E.emp_code AND E.Company_Id=" + Utility.ToInteger(Session["compid"]);
                                                sqlQuery = sqlQuery + " AND convert(datetime,EP.FromDate,103) >='" + dtStartDate.ToString() + "' AND convert(datetime,EP.FromDate,103) <='" + dtEndDate.ToString() + "'";
                                                sqlQuery = sqlQuery + " AND convert(datetime,E.joining_date,103)<='" + dtStartDate.ToString() + "' ";

                                            }
                                            else
                                            {
                                                //sqlQuery = " Select  emp_code,joining_date from employee where  dept_id in ";
                                                //sqlQuery = sqlQuery + "(Select id from department Where Company_id=" + Utility.ToInteger(Session["compid"]).ToString() + " and DeptName ='" + deptName1.Trim() + "')";
                                                //sqlQuery = sqlQuery + " AND joining_date >'" + dtStartDate.ToString() + "' AND joining_date <'" + dtEndDate.ToString() + "' AND Company_Id=" + Utility.ToInteger(Session["compid"]);
                                                sqlQuery = "select convert(varchar(10), EP.FromDate,103) EffDate,E.emp_name,convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),EP.Payrate)) Salary,";
                                                sqlQuery = sqlQuery + " E.emp_code,E.time_card_no TimeCard  from dbo.EmployeePayHistory EP,Employee E ";
                                                sqlQuery = sqlQuery + " where   convert(Datetime, E.termination_date,103)  is  null  AND EP.Emp_ID = E.emp_code AND E.Company_Id=" + Utility.ToInteger(Session["compid"]);
                                                sqlQuery = sqlQuery + " AND convert(datetime,EP.FromDate,103) >='" + dtStartDate.ToString() + "' AND convert(datetime,EP.FromDate,103) <='" + dtEndDate.ToString() + "'";
                                                sqlQuery = sqlQuery + " AND convert(datetime,E.joining_date,103)<='" + dtStartDate.ToString() + "' AND E.dept_id IN(Select id from department Where Company_id=" +Utility.ToInteger(Session["compid"]).ToString() + " and DeptName ='" + deptName1.Trim() + "')";
                                            }
                                            DataSet empSalInCurr = new DataSet();
                                            DataSet empSalInPrev = new DataSet();                                            
                                            empSalInCurr = DataAccess.FetchRS(CommandType.Text, sqlQuery, null);
                                
                                           
                                            if (empSalInCurr.Tables[0].Rows.Count > 0)
                                            { 
                                                string  strnew ="";
                                                DataRow drnewIncc = dstemp.Tables[0].NewRow();
                                                int cnt = 0;
                                                drnewIncc["FullName"] = "Add:";                                                
                                                double totalDiff = 0.00;
                                                foreach (DataRow dr9 in empSalInCurr.Tables[0].Rows)
                                                {   
                                                    //Get PrevMonth Salary for employee
                                                    string strPrev1 = "select Top 1 convert(varchar(10), EP.FromDate,103) EffDate,EP.ID,E.emp_name,convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),EP.Payrate)) Salary,";
                                                           strPrev1  =strPrev1+" E.emp_code,E.time_card_no TimeCard  from dbo.EmployeePayHistory EP,Employee E  where   convert(Datetime, E.termination_date,103)  is null  AND EP.Emp_ID =E.emp_code   AND E.Company_Id=" + Utility.ToInteger(Session["compid"]).ToString();
                                                           strPrev1 = strPrev1 + " AND convert(datetime,EP.FromDate,103) <'" + dtStartDatePrev + "' AND convert(datetime,EP.ToDate,103) >= '" + dtStartDatePrev + "' AND convert(datetime,E.joining_date,103)<='" + dtStartDatePrev + "' AND EP.Emp_ID = " + dr9["emp_code"].ToString() + "  AND EP.ToDate is not null order by  convert(Datetime, EP.FromDate,103) Asc ";

                                                    empSalInPrev = DataAccess.FetchRS(CommandType.Text, strPrev1, null);
                                                    double salPrev =0.00;
                                                    double salDiff =0.00;
                                                    if(empSalInPrev.Tables[0].Rows.Count>0)
                                                    {
                                                        if(empSalInPrev.Tables[0].Rows.Count>0)
                                                        {
                                                            foreach(DataRow drprev in empSalInPrev.Tables[0].Rows)
                                                            {
                                                                salPrev= Convert.ToDouble(drprev["Salary"].ToString());
                                                                salDiff= Convert.ToDouble(dr9["Salary"].ToString())- Convert.ToDouble(drprev["Salary"].ToString());
                                                                totalDiff = totalDiff + salDiff;
                                                            }
                                                        
                                                        }
                                                    }
                                                    if (cnt == 0)
                                                    {
                                                        strnew = "<table border=\"1\" valign=\"top\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\"  width=\"50%\"><tr  style=\"border:1px\"><td>Salary increment For " + strstartMonth + "</td><td>EmpCode</td><td>Effective</td><td>Amount</td><td>Current</td><td>Previous</td></tr>";
                                                        strnew = strnew + "<tr style=\"border:1px;border-style:none\"><td></td><td>" + dr9["TimeCard"].ToString() + "(" + dr9["emp_name"].ToString() + ")" + "</td><td>" + dr9["EffDate"].ToString() + "</td><td>" + salDiff + "</td><td>" + dr9["Salary"].ToString() + "</td><td>" + salPrev + "</td></tr>";
                                                    }
                                                    else
                                                    {
                                                        strnew = strnew + "<tr style=\"border:1px;border-style:none\"><td></td><td>" + dr9["TimeCard"].ToString() + "(" + dr9["emp_name"].ToString() + ")" + "</td><td>" + dr9["EffDate"].ToString() + "</td><td>" + salDiff + "</td><td>" + dr9["Salary"].ToString() + "</td><td>" + salPrev + "</td></tr>";
                                                    }
                                                    cnt = cnt + 1;
                                                
                                                }
                                                if (empSalInCurr.Tables[0].Rows.Count > 0)
                                                {
                                                    strnew = strnew + "<tr style=\"border:1px;border-style:none\"><td></td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td></tr>";
                                                    strnew = strnew + "<tr style=\"border:1px;border-style:none\"><td></td><td>" + "" + "</td><td>" + "" + "</td><td>" + totalDiff + "</td><td>" + "" + "</td><td>" + "" + "</td></tr>";
                                                    strnew = strnew + "</table>";
                                                    drnewIncc["DeptName"] = strnew;
                                                    drnewIncc["emp_code"] = -1;
                                                    dstemp.Tables[0].Rows.Add(drnewIncc);
                                                }
                                                
                                            }
                                            
                                            /******************************************************************************************/
                                            //********************************************************************************************************
                                            //Add New Employee in Last month...

                                             string strPrevMonth = "";
                   
                                            monthPrev = "";
                                            rowids = 0;
                                            rowids = Convert.ToInt32(cmbMonth.Items[cmbMonth.SelectedIndex].Value) - 1;
                                            DataRow[] drResults2 = monthDs.Tables[0].Select("RowID = " + rowids);
                                            foreach (DataRow drnew1 in drResults2)
                                            {
                                                monthPrev = drnew1["Month"].ToString();
                                            }
                                            int Month3 = Convert.ToInt32(monthPrev);

                                            paySD = "";
                                            payED = "";
                                            paySSD = "";
                                            paySED = "";
                                            paySSDa = "";
                                            paySEDa = "";

                                            foreach (DataRow dr in drResults2)
                                            {
                                                paySD = dr["PayStartDay"].ToString();
                                                payED = dr["PayEndDay"].ToString();
                                                paySSD = dr["PaySubStartDay"].ToString();
                                                paySED = dr["PaySubEndDay"].ToString();
                                                paySSDa = dr["PaySubStartDate"].ToString();
                                                paySEDa = dr["PaySubEndDate"].ToString();
                                            }

                                            dtStartDate = Convert.ToString(Month3.ToString() )+ "/" + paySSD.ToString() + "/" + Utility.ToString(year);
                                            dtEndDate = Convert.ToString(Month3.ToString() )+ "/" + paySED.ToString() + "/" + Utility.ToString(year);

                            
                                            //if (cmbMonth.SelectedIndex == 0)
                                            //{
                                            //    strPrevMonth = cmbMonth.Items[cmbMonth.Items.Count - 1].Text + " : " + cmbYear.Items[cmbYear.SelectedIndex - 1].Text;                                                
                                            //}
                                            //else
                                            //{
                                            //    strPrevMonth = cmbMonth.Items[cmbMonth.SelectedIndex - 1].Text + " : " + cmbYear.Items[cmbYear.SelectedIndex].Text;                                                
                                            //}

                                            //strstartMonth = cmbMonth.SelectedItem.Text + " : " + cmbYear.SelectedItem.Text;
                                            //string year = "";
                                            if (cmbMonth.SelectedIndex == 0 || cmbMonth.SelectedIndex == 12)
                                            {
                                                strPrevMonth = cmbMonth.Items[cmbMonth.Items.Count - 1].Text + " : " + cmbYear.Items[cmbYear.SelectedIndex - 1].Text;
                                                year = cmbYear.Items[cmbYear.SelectedIndex - 1].Text;
                                            }
                                            else
                                            {
                                                strPrevMonth = cmbMonth.Items[cmbMonth.SelectedIndex - 1].Text + " : " + cmbYear.Items[cmbYear.SelectedIndex].Text;
                                                year = cmbYear.Items[cmbYear.SelectedIndex].Text;
                                            }
                                            // 


                                            sqlQuery = "";                                            
                                            if (deptName1 == "ALL")
                                            {
                                                sqlQuery = " Select  emp_code,joining_date from employee where  Company_Id=" + Utility.ToInteger(Session["compid"]);
                                                sqlQuery = sqlQuery + " and  joining_date >='" + dtStartDate.ToString() + "' AND joining_date <='" + dtEndDate.ToString() + "'";
                                            }
                                            else
                                            {
                                                sqlQuery = " Select  emp_code,joining_date from employee where  dept_id in ";
                                                sqlQuery = sqlQuery + "(Select id from department Where Company_id=" + Utility.ToInteger(Session["compid"]).ToString() + " and DeptName ='" + deptName1.Trim() + "')";
                                                sqlQuery = sqlQuery + " AND joining_date >='" + dtStartDate.ToString() + "' AND joining_date <='" + dtEndDate.ToString() + "' AND Company_Id=" + Utility.ToInteger(Session["compid"]);
                                            }

                                            empJoin = new DataSet();
                                            empJoin = DataAccess.FetchRS(CommandType.Text, sqlQuery, null);

                                            count1 = 0;
                                            total = 0;
                                            if (empJoin.Tables[0].Rows.Count > 0)
                                            {
                                                string strJoined = "";
                                                DataRow drnew1 = dstemp.Tables[0].NewRow();
                                                foreach (DataRow drnew3 in empJoin.Tables[0].Rows)
                                                {
                                                    DateTime dtJoin = new DateTime();
                                                    decimal balance = 0;
                                                    decimal monthly = 0;
                                                    decimal prorated = 0;

                                                    if (drnew3["joining_date"].ToString() != "")
                                                    {
                                                        dtJoin = Convert.ToDateTime(drnew3["joining_date"].ToString());
                                                    }
                                                    if (count1 == 0)
                                                    {
                                                        if (dsprev.Tables[0].Rows.Count > 0)
                                                        {
                                                            foreach (DataRow dr in dsprev.Tables[0].Rows)
                                                            {
                                                                if (dr["emp_id"].ToString() == drnew3["emp_code"].ToString())
                                                                {
                                                                    balance = Convert.ToDecimal(dr["basic_pay"].ToString());
                                                                }
                                                            }
                                                        }

                                                        foreach (DataRow dr in dstemp.Tables[0].Rows)
                                                        {
                                                            if (dr["emp_code"].ToString() == drnew3["emp_code"].ToString())
                                                            {
                                                                monthly = Convert.ToDecimal(dr["Basic"].ToString());
                                                            }
                                                        }
                                                        //prorated = monthly - balance;
                                                        //total = total + balance;
                                                        
                                                        // last month new employee
                                                        prorated = balance;
                                                        balance = monthly - prorated ;
                                                        total = total + balance;
                                                        
                                                        drnew1["FullName"] = "Add:";

                                                        string timecard = "Select time_card_no + '  ( ' + emp_name + ' ' + emp_lname + ' ) '  From employee where emp_code=" + drnew3["Emp_Code"].ToString() + " and  Company_Id=" + Utility.ToInteger(Session["compid"]);

                                                        SqlDataReader dr4 =null;

                                                        dr4 = DataAccess.ExecuteReader(CommandType.Text, timecard, null);

                                                        timecard = "";
                                                        while(dr4.Read())
                                                        {
                                                            if (dr4[0] != null)
                                                            {
                                                                timecard = dr4[0].ToString();
                                                            }
                                                        }

                                                        strJoined = "<table border=\"1\" valign=\"top\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\"  width=\"50%\"><tr  style=\"border:1px\"><td>New Employee for " + strPrevMonth + "</td><td>EmpCode</td><td>Joined</td><td>Balance</td><td>Monthly</td><td>Prorated</td></tr>";
                                                        strJoined = strJoined + "<tr style=\"border:1px;border-style:none\"><td></td><td>" + timecard + "</td><td>" + dtJoin.Date.Day + "/" + dtJoin.Date.Month + "/" + dtJoin.Date.Year + "</td><td>" + balance.ToString() + "</td><td>" + monthly.ToString() + "</td><td>" + prorated.ToString() + " </td></tr>";
                                                    }
                                                    else
                                                    {
                                                        if (dsprev.Tables[0].Rows.Count > 0)
                                                        {
                                                            foreach (DataRow dr in dsprev.Tables[0].Rows)
                                                            {
                                                                if (dr["emp_id"].ToString() == drnew3["emp_code"].ToString())
                                                                {
                                                                    balance = Convert.ToDecimal(dr["basic_pay"].ToString());
                                                                }
                                                            }
                                                        }

                                                        foreach (DataRow dr in dstemp.Tables[0].Rows)
                                                        {
                                                            if (dr["emp_code"].ToString() == drnew3["emp_code"].ToString())
                                                            {
                                                                monthly = Convert.ToDecimal(dr["Basic"].ToString());
                                                            }
                                                        }
                                                        prorated = monthly - balance;
                                                        total = total + balance;
                                                        drnew1["FullName"] = "Add:";


                                                        String timecard1 = "Select time_card_no + '  ( ' + emp_name + ' ' + emp_lname + ' ) '  From employee where emp_code=" + drnew3["Emp_Code"].ToString();

                                                        SqlDataReader dr1 ;

                                                        dr1 = DataAccess.ExecuteReader(CommandType.Text, timecard1, null);

                                                        timecard1 = "";
                                                        while (dr1.Read())
                                                        {
                                                            if (dr1[0] != null)
                                                            {
                                                                timecard1 = dr1[0].ToString();
                                                            }
                                                        }

                                                        strJoined = strJoined + "<tr style=\"border:1px;border-style:none\"><td></td><td>" + timecard1 + "</td><td>" + dtJoin.Date.Day + "/" + dtJoin.Date.Month + "/" + dtJoin.Date.Year + "</td><td>" + balance.ToString() + "</td><td>" + monthly.ToString() + "</td><td>" + prorated.ToString() + " </td></tr>";
                                                    }
                                                    count1 = count1 + 1;
                                                   
                                                }
                                                strJoined = strJoined + "<tr  style=\"border:1px;border-style:none\"><td>Total</td><td></td><td></td><td>" + total.ToString() + "</td><td></td></tr>";
                                                strJoined = strJoined + "</table>";                                               
                                                drnew1["DeptName"] = strJoined;
                                                drnew1["emp_code"] = -1;
                                                dstemp.Tables[0].Rows.Add(drnew1);
                                            }

                                        //********************************************************************************************************
                                            //Resignation  Employee in Last month...
                                            monthPrev = "";
                                            rowids = 0;
                                            rowids = Convert.ToInt32(cmbMonth.Items[cmbMonth.SelectedIndex].Value) - 1;
                                            DataRow[] drResults3 = monthDs.Tables[0].Select("RowID = " + rowids);
                                            foreach (DataRow drnew1 in drResults3)
                                            {
                                                monthPrev = drnew1["Month"].ToString();
                                                strstartMonth = drnew1["MonthName"].ToString();
                                            }
                                            int Month4 = Convert.ToInt32(monthPrev);

                                            paySD = "";
                                            payED = "";
                                            paySSD = "";
                                            paySED = "";
                                            paySSDa = "";
                                            paySEDa = "";

                                            foreach (DataRow dr in drResults2)
                                            {
                                                paySD = dr["PayStartDay"].ToString();
                                                payED = dr["PayEndDay"].ToString();
                                                paySSD = dr["PaySubStartDay"].ToString();
                                                paySED = dr["PaySubEndDay"].ToString();
                                                paySSDa = dr["PaySubStartDate"].ToString();
                                                paySEDa = dr["PaySubEndDate"].ToString();
                                            }

                                            dtStartDate = Convert.ToString(Month3.ToString()) + "/" + paySSD.ToString() + "/" + Utility.ToString(year);
                                            dtEndDate = Convert.ToString(Month3.ToString()) + "/" + paySED.ToString() + "/" + Utility.ToString(year);
                                            //*********************************************************************************************************************
                                            //Get One More Previous Month To get the basic of employee
                                            DataSet dstemp1 = new DataSet();

                                            //Get Data For Previous Month .....                                                                                       
                                            rowids = Convert.ToInt32(cmbMonth.Items[cmbMonth.SelectedIndex].Value) - 2;
                                            drResults = monthDs.Tables[0].Select("RowID = " + rowids);
                                            foreach (DataRow drnew1 in drResults)
                                            {
                                                monthPrev = drnew1["Month"].ToString();
                                            }
                                            Month = Convert.ToInt32(monthPrev);

                                            paySD = "";
                                            payED = "";
                                            paySSD = "";
                                            paySED = "";
                                            paySSDa = "";
                                            paySEDa = "";

                                            foreach (DataRow dr in drResults)
                                            {
                                                paySD = dr["PayStartDay"].ToString();
                                                payED = dr["PayEndDay"].ToString();
                                                paySSD = dr["PaySubStartDay"].ToString();
                                                paySED = dr["PaySubEndDay"].ToString();
                                                paySSDa = dr["PaySubStartDate"].ToString();
                                                paySEDa = dr["PaySubEndDate"].ToString();
                                            }

                                           
                                            i = 0;

                                            DataSet dsprevprev = new DataSet();
                                            ssql = "sp_ApprovePayRoll";// 0,2009,2
                                            parms = new SqlParameter[6];
                                            parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(Session["compid"]));
                                            parms[i++] = new SqlParameter("@month", Utility.ToInteger(rowids));
                                            parms[i++] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"]));
                                            parms[i++] = new SqlParameter("@year", Utility.ToInteger(year));
                                            parms[i++] = new SqlParameter("@Status", "G");
                                            if (deptID.Items[deptID.SelectedIndex].Text == "ALL")
                                            {
                                                parms[i++] = new SqlParameter("@DeptId", -1);
                                            }
                                            else
                                            {
                                                parms[i++] = new SqlParameter("@DeptId", deptID.SelectedValue);
                                            }
                                            dsprevprev = DataAccess.ExecuteSPDataSet(ssql, parms);

                                            //*********************************************************************************************************************

                                            sqlQuery = "";
                                            if (deptName1 == "ALL")
                                            {
                                                sqlQuery = " Select  emp_code,termination_date from employee where Company_id=" + Utility.ToInteger(Session["compid"]);
                                                sqlQuery = sqlQuery + "AND  termination_date >'" + dtStartDate.ToString() + "' AND termination_date <'" + dtEndDate.ToString() + "'";
                                            }
                                            else
                                            {
                                                sqlQuery = " Select  emp_code,termination_date from employee where  Company_id=" + Utility.ToInteger(Session["compid"])+" AND  dept_id in ";
                                                sqlQuery = sqlQuery + "(Select id from department Where Company_id=" + Utility.ToInteger(Session["compid"]).ToString() + " and DeptName ='" + deptName1.Trim() + "')";
                                                sqlQuery = sqlQuery + "AND termination_date >'" + dtStartDate.ToString() + "' AND termination_date <'" + dtEndDate.ToString() + "'";
                                            }

                                            DataSet empter = new DataSet();
                                            empter = DataAccess.FetchRS(CommandType.Text, sqlQuery, null);
                                            total = 0;
                                            count1 = 0;
                                            if (empter.Tables[0].Rows.Count > 0)
                                            {
                                                string strter = "";
                                                DataRow drnew1 = dstemp.Tables[0].NewRow();
                                                foreach (DataRow drnew3 in empter.Tables[0].Rows)
                                                {
                                                   
                                                    DateTime dtTer = new DateTime();

                                                    decimal balance = 0;
                                                    decimal monthly = 0;
                                                    decimal prorated = 0;

                                                    if (drnew3["termination_date"].ToString() != "")
                                                    {
                                                        dtTer = Convert.ToDateTime(drnew3["termination_date"].ToString());
                                                    }
                                                    if (count1 == 0)
                                                    {
                                                        if (dsprev.Tables[0].Rows.Count > 0)
                                                        {
                                                            foreach (DataRow dr in dsprev.Tables[0].Rows)
                                                            {
                                                                if (dr["emp_id"].ToString() == drnew3["emp_code"].ToString())
                                                                {
                                                                    prorated = Convert.ToDecimal(dr["basic_pay"].ToString());
                                                                }
                                                            }
                                                        }

                                                        foreach (DataRow dr in dsprevprev.Tables[0].Rows)
                                                        {
                                                            if (dr["emp_id"].ToString() == drnew3["emp_code"].ToString())
                                                            {
                                                                monthly = Convert.ToDecimal(dr["basic_pay"].ToString());
                                                            }
                                                        }
                                                        balance = monthly - prorated;
                                                        total = total + prorated;


                                                        String timecard5 = "Select time_card_no + '  ( ' + emp_name + ' ' + emp_lname + ' ) '  From employee where emp_code=" + drnew3["Emp_Code"].ToString();

                                                        SqlDataReader dr5;

                                                        dr5 = DataAccess.ExecuteReader(CommandType.Text, timecard5, null);

                                                        timecard5 = "";
                                                        while (dr5.Read())
                                                        {
                                                            if (dr5[0] != null)
                                                            {
                                                                timecard5 = dr5[0].ToString();
                                                            }
                                                        }
                                                        drnew1["FullName"] = "Less:";
                                                        strter = "<table border=\"1\" valign=\"top\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\"  width=\"50%\"><tr  style=\"border:1px\"><td>Resignation for " + strPrevMonth + "</td><td>EmpCode</td><td>Resigned</td><td>Prorated</td><td>Monthly</td><td>Balance</td></tr>";
                                                        strter = strter + "<tr style=\"border:1px;border-style:none\"><td></td><td>" + timecard5 + "</td><td>" + dtTer.Date.Day + "/" + dtTer.Date.Month + "/" + dtTer.Date.Year + "</td><td>" + prorated.ToString() + "</td><td>" + monthly.ToString() + "</td><td>" + balance.ToString() + " </td></tr>";
                                                    }
                                                    else
                                                    {
                                                        if (dsprev.Tables[0].Rows.Count > 0)
                                                        {
                                                            foreach (DataRow dr in dsprev.Tables[0].Rows)
                                                            {
                                                                if (dr["emp_id"].ToString() == drnew3["emp_code"].ToString())
                                                                {
                                                                    prorated = Convert.ToDecimal(dr["basic_pay"].ToString());
                                                                }
                                                            }
                                                        }

                                                        foreach (DataRow dr in dsprevprev.Tables[0].Rows)
                                                        {
                                                            if (dr["emp_id"].ToString() == drnew3["emp_code"].ToString())
                                                            {
                                                                monthly = Convert.ToDecimal(dr["basic_pay"].ToString());
                                                            }
                                                        }
                                                        balance = monthly - prorated;
                                                        total = total + prorated;
                                                        drnew1["FullName"] = "Less:";


                                                        String timecard88 = "Select time_card_no + '  ( ' + emp_name + ' ' + emp_lname + ' ) '  From employee where emp_code=" + drnew3["Emp_Code"].ToString() + " AND Company_Id=" + Utility.ToInteger(Session["compid"]);

                                                        SqlDataReader dr88;

                                                        dr88 = DataAccess.ExecuteReader(CommandType.Text, timecard88, null);

                                                        timecard88 = "";
                                                        while (dr88.Read())
                                                        {
                                                            if (dr88[0] != null)
                                                            {
                                                                timecard88 = dr88[0].ToString();
                                                            }
                                                        }





                                                        //strter = "<table border=\"2\" valign=\"top\"  style=\"font: 12px;font-family:Courier New\" Height=\"100%\"  width=\"50%\"><tr  style=\"border:1px\"><td>Resignation for " + strPrevMonth + "</td><td>EmpCode</td><td>Resigned</td><td>Prorated</td><td>Monthly</td><td>Balance</td></tr>";
                                                        strter = strter + "<tr style=\"border:1px;border-style:none\"><td></td><td>" + timecard88 + "</td><td>" + dtTer.Date.Day + "/" + dtTer.Date.Month + "/" + dtTer.Date.Year + "</td><td>" + prorated.ToString() + "</td><td>" + monthly.ToString() + "</td><td>" + balance.ToString() + " </td></tr>";
                                                    }
                                                    //strter = strter + "</table>";
                                                    count1 = count1 + 1;
                                                    //drnew1["DeptName"] = strter;
                                                    //drnew1["emp_code"] = -1;    
                                                    //dstemp.Tables[0].Rows.Add(drnew1);
                                                }

                                                strter = strter + "<tr  style=\"border:1px;border-style:none\"><td>Total</td><td></td><td></td><td>" + total.ToString() + "</td><td></td></tr>";
                                                strter = strter + "</table>";
                                                drnew1["DeptName"] = strter;
                                                drnew1["emp_code"] = -1;
                                                dstemp.Tables[0].Rows.Add(drnew1);
                                            }

                                            /****************************Add employeess with salary increment in first month**************************************/
                                            sqlQuery = "";
                                            monthstrprev = "";
                                            drResultsPrev = monthDs.Tables[0].Select("RowID = " + (Convert.ToInt32(cmbMonth.Items[cmbMonth.SelectedIndex].Value) - 2));
                                            foreach (DataRow drnew1 in drResultsPrev)
                                            {
                                                monthstrprev = drnew1["Month"].ToString();
                                                
                                            }
                                            Monthprev = Convert.ToInt32(monthstrprev);


                                            paySD = "";
                                            payED = "";
                                            paySSD = "";
                                            paySED = "";
                                            paySSDa = "";
                                            paySEDa = "";

                                            foreach (DataRow dr in drResultsPrev)
                                            {
                                                paySD = dr["PayStartDay"].ToString();
                                                payED = dr["PayEndDay"].ToString();
                                                paySSD = dr["PaySubStartDay"].ToString();
                                                paySED = dr["PaySubEndDay"].ToString();
                                                paySSDa = dr["PaySubStartDate"].ToString();
                                                paySEDa = dr["PaySubEndDate"].ToString();
                                            }

                                            dtStartDatePrev="";
                                            dtEndDatePrev = "";
                                            string yers = "";
                                           
                                            if (Monthprev != 12)
                                            {
                                                dtStartDatePrev = Convert.ToString(Monthprev.ToString() + "/" + paySSD + "/" + Utility.ToString(Request.QueryString["Year"]));
                                                dtEndDatePrev = Convert.ToString(Monthprev.ToString() + "/" + paySED + "/" + Utility.ToString(Request.QueryString["Year"]));
                                                yers = Utility.ToString(Request.QueryString["Year"]);
                                            }
                                            else
                                            {
                                                dtStartDatePrev = Convert.ToString(Monthprev.ToString() + "/" + paySSD + "/" + Utility.ToString(Utility.ToInteger(Request.QueryString["Year"]) - 1));
                                                dtEndDatePrev = Convert.ToString(Monthprev.ToString() + "/" + paySED + "/" + Utility.ToString(Utility.ToInteger(Request.QueryString["Year"]) - 1));
                                                yers =  Utility.ToString(Utility.ToInteger(Request.QueryString["Year"]) - 1);
                                            }

                                            sqlQuery="";
                                            if (deptName1 == "ALL")
                                            {
                                                //sqlQuery = " Select  emp_code,joining_date from employee where  Company_Id=" + Utility.ToInteger(Session["compid"]);
                                                //sqlQuery = sqlQuery + " and  joining_date >='" + dtStartDate.ToString() + "' AND joining_date <='" + dtEndDate.ToString() + "'";
                                                sqlQuery = "select convert(varchar(10), EP.FromDate,103) EffDate,E.emp_name,convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),EP.Payrate)) Salary,";
                                                sqlQuery = sqlQuery + " E.emp_code,E.time_card_no TimeCard  from dbo.EmployeePayHistory EP,Employee E ";
                                                sqlQuery = sqlQuery + " where   convert(Datetime, E.termination_date,103)  is  null  AND EP.Emp_ID = E.emp_code AND E.Company_Id=" + Utility.ToInteger(Session["compid"]);
                                                sqlQuery = sqlQuery + " AND convert(datetime,EP.FromDate,103) >='" + dtStartDate.ToString() + "' AND convert(datetime,EP.FromDate,103) <='" + dtEndDate.ToString() + "'";
                                                sqlQuery = sqlQuery + " AND convert(datetime,E.joining_date,103)<='" + dtStartDate.ToString() + "' ";

                                            }
                                            else
                                            {
                                                //sqlQuery = " Select  emp_code,joining_date from employee where  dept_id in ";
                                                //sqlQuery = sqlQuery + "(Select id from department Where Company_id=" + Utility.ToInteger(Session["compid"]).ToString() + " and DeptName ='" + deptName1.Trim() + "')";
                                                //sqlQuery = sqlQuery + " AND joining_date >'" + dtStartDate.ToString() + "' AND joining_date <'" + dtEndDate.ToString() + "' AND Company_Id=" + Utility.ToInteger(Session["compid"]);
                                                sqlQuery = "select convert(varchar(10), EP.FromDate,103) EffDate,E.emp_name,convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),EP.Payrate)) Salary,";
                                                sqlQuery = sqlQuery + " E.emp_code,E.time_card_no TimeCard  from dbo.EmployeePayHistory EP,Employee E ";
                                                sqlQuery = sqlQuery + " where   convert(Datetime, E.termination_date,103)  is  null  AND EP.Emp_ID = E.emp_code AND E.Company_Id=" + Utility.ToInteger(Session["compid"]);
                                                sqlQuery = sqlQuery + " AND convert(datetime,EP.FromDate,103) >='" + dtStartDate.ToString() + "' AND convert(datetime,EP.FromDate,103) <='" + dtEndDate.ToString() + "'";
                                                sqlQuery = sqlQuery + " AND convert(datetime,E.joining_date,103)<='" + dtStartDate.ToString() + "' AND E.dept_id IN(Select id from department Where Company_id=" + Utility.ToInteger(Session["compid"]).ToString() + " and DeptName ='" + deptName1.Trim() + "')";
                                            }
                                            empSalInCurr = new DataSet();
                                            empSalInPrev = new DataSet();
                                            empSalInCurr = DataAccess.FetchRS(CommandType.Text, sqlQuery, null);
                                            
                                            if (empSalInCurr.Tables[0].Rows.Count > 0)
                                            {
                                                 string strnew = "";
                                                DataRow drnewIncclast = dstemp.Tables[0].NewRow();
                                                int cnt = 0;
                                                int check = 0;
                                                drnewIncclast["FullName"] = "Add:";
                                                double totalDiff = 0.00;
                                               

                                                foreach (DataRow dr9 in empSalInCurr.Tables[0].Rows)
                                                {
                                                    
                                                    //Get PrevMonth Salary for employee
                                                    string strPrev1 = "";
                                                    //strPrev1 = "select Top 1 convert(Datetime, EP.FromDate,103) EffDate,EP.ID,E.emp_name,convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),EP.Payrate)) Salary,";
                                                    strPrev1 = "select Top 1 convert(varchar(10), EP.FromDate,103) EffDate,EP.ID,E.emp_name,convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),EP.Payrate)) Salary,";
                                                    strPrev1 = strPrev1 + " E.emp_code,E.time_card_no TimeCard  from dbo.EmployeePayHistory EP,Employee E  where   convert(Datetime, E.termination_date,103)  is null  AND EP.Emp_ID =E.emp_code   AND E.Company_Id=" + Utility.ToInteger(Session["compid"]).ToString();
                                                    strPrev1 = strPrev1 + " AND convert(datetime,EP.FromDate,103) <'" + dtStartDatePrev + "' AND convert(datetime,EP.ToDate,103) >= '" + dtStartDatePrev + "' AND convert(datetime,E.joining_date,103)<='" + dtStartDatePrev + "' AND EP.Emp_ID = " + dr9["emp_code"].ToString() + "  AND EP.ToDate is not null order by  convert(Datetime, EP.FromDate,103) Asc ";

                                                  
                                                    empSalInPrev = DataAccess.FetchRS(CommandType.Text, strPrev1, null);
                                                    double salPrev = 0.00;
                                                    double  salDiff = 0.00;                                                    
                                                    if (empSalInPrev.Tables[0].Rows.Count > 0)
                                                    {
                                                        if (empSalInPrev.Tables[0].Rows.Count > 0)
                                                        {
                                                            DateTime effdate = new DateTime();
                                                            effdate = Convert.ToDateTime(empSalInPrev.Tables[0].Rows[0]["EffDate"].ToString()).Date;                                                           
                                                            DateTime curredatenew = new DateTime();
                                                            curredatenew = Convert.ToDateTime(dtStartDate);
                                                            int flag = 0;
                                                            if (curredatenew.Month != 1)
                                                            {
                                                                if (effdate.Month == curredatenew.Month - 1 && effdate.Year == curredatenew.Year)
                                                                {
                                                                    flag = 0;

                                                                }
                                                                else
                                                                {
                                                                    flag = 1;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (effdate.Month == 12 && effdate.Year == curredatenew.Year - 1)
                                                                {
                                                                    flag = 0;

                                                                }
                                                                else
                                                                {
                                                                    flag = 1;
                                                                }
                                                            }

                                                            foreach (DataRow drprev in empSalInPrev.Tables[0].Rows)
                                                            {
                                                                double pervsalary = 0.0;
                                                                double.TryParse(drprev["Salary"].ToString(), out pervsalary);

                                                                double currentsalary = 0.0;
                                                                double.TryParse(dr9["Salary"].ToString(), out currentsalary);


                                                                salDiff = currentsalary - pervsalary;
                                                                totalDiff = totalDiff + salDiff;
                                                                //if (flag == 1)
                                                                //{
                                                                //    salPrev = Convert.ToDouble(dr9["Salary"].ToString());
                                                                //    totalDiff = 0;
                                                                //    salDiff = 0;
                                                                //}
                                                            }
                                                            
                                                        }
                                                        else {
                                                            check = -1;
                                                        }
                                                        
                                                    }
                                                 
                                                    if (cnt == 0 && check == 0)
                                                    {
                                                        strnew = "<table border=\"1\" valign=\"top\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\"  width=\"50%\"><tr  style=\"border:1px\"><td>Salary increment For " + strstartMonth + ":" + yers + "</td><td>EmpCode</td><td>Effective</td><td>Amount</td><td>Current</td><td>Previous</td></tr>";
                                                        strnew = strnew + "<tr style=\"border:1px;border-style:none\"><td></td><td>" + dr9["TimeCard"].ToString() + "(" + dr9["emp_name"].ToString() + ")" + "</td><td>" + Convert.ToDateTime(dr9["EffDate"].ToString()).ToString("dd/MM/yyyy") + "</td><td>" + salDiff + "</td><td>" + dr9["Salary"].ToString() + "</td><td>" + salPrev + "</td></tr>";
                                                    }
                                                    else if(cnt > 0 && check == 0)
                                                    {
                                                        strnew = strnew + "<tr style=\"border:1px;border-style:none\"><td></td><td>" + dr9["TimeCard"].ToString() + "(" + dr9["emp_name"].ToString() + ")" + "</td><td>" + Convert.ToDateTime(dr9["EffDate"].ToString()).ToString("dd/MM/yyyy") + "</td><td>" + salDiff + "</td><td>" + dr9["Salary"].ToString() + "</td><td>" + salPrev + "</td></tr>";
                                                    }
                                                    cnt = cnt + 1;
                                                    check = 0;

                                                }
                                                //strnew = strnew + "<tr style=\"border:1px;border-style:none\"><td></td><td>" + "" + "</td><td>" + "" + "</td><td>" + " " + "</td><td>" + "" + "</td><td>" + "" + "</td></tr>";
                                                strnew = strnew + "<tr style=\"border:1px;border-style:none\"><td></td><td>" + "" + "</td><td>" + "" + "</td><td>" + totalDiff + "</td><td>" + "" + "</td><td>" + "" + "</td></tr>";
                                                strnew = strnew + "</table>";
                                                drnewIncclast["DeptName"] = strnew;
                                                drnewIncclast["emp_code"] = -1;
                                                dstemp.Tables[0].Rows.Add(drnewIncclast);
                                            }

                                            /***********************************************************************************************************/
                                            //Last Row Difference 
                                            //diff
                                            DataRow drnew6 = dstemp.Tables[0].NewRow();
                                            drnew6["emp_code"] = -1;
                                            drnew6["FullName"] = "Regular Pay Adjustment:";
                                            drnew6["DeptName"] = "<table border=\"1\" valign=\"top\"  style=\"font: 10px;font-family:Courier New\" Height=\"100%\"  width=\"50%\"><tr style=\"border:1px;border-style:none\"><td></td><td>" + diff.ToString() + "</td></tr></table>";
                                            dstemp.Tables[0].Rows.Add(drnew6);


                                            DataRow[] rowToDelete = dstemp.Tables[0].Select("emp_code<>-1");

                                            if (rowToDelete.Length > 0)
                                            {
                                                foreach (DataRow drdel in rowToDelete)
                                                {
                                                    dstemp.Tables[0].Rows.Remove(drdel);
                                                    dstemp.AcceptChanges();
                                                }
                                            }

                                            //dstemp.AcceptChanges();
                            //Change Dataset as per values Required For Example 
                            RadGrid3.DataSource = dstemp;                
                            RadGrid3.DataBind();
            }

            Session["ROWID"] = cmbMonth.SelectedValue.ToString();
            Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();

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

                //if (strEmpvisible != "")
                //{
                //    Session["EmpPassID"] = strEmpvisible;
                //}
                //else
                //{
                //    Session["EmpPassID"] = "";
                //}
            }

            
        }
        [AjaxPro.AjaxMethod]
        public string btnPayrollDetail_Click(int monthid, int yearid)
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
                str = "payrolldetailreport_New.aspx?UserID=" + Session["EmpCode"].ToString() + "&Month=" + dr["Month"].ToString() + "&stdatemonth=" + Session["PayStartDay"].ToString() + "&endatemonth=" + Session["PayEndDay"].ToString() + "&stdatesubmonth=" + Session["PaySubStartDay"].ToString() + "&endatesubmonth=" + Session["PaySubEndDay"].ToString() + "&monthidintbl=" + monthid.ToString() + "&Year=" + yearid.ToString() + "&company_id=" + Session["Compid"].ToString();
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


        //Toolbar
        #region Toolbar and Exporting

        //column to hide wile export
        protected void HideGridColumnseExport()
        {
                    if (chkList.SelectedValue == "Detailed")
                    {

                        RadGrid1.MasterTableView.GetColumn("TemplateColumn").Visible = false;
                        RadGrid1.MasterTableView.GetColumn("GridClientSelectColumn").Visible = false;
                        RadGrid1.MasterTableView.GetColumn("Image").Visible = false;

                    }

                    if (chkList.SelectedValue == "Summary")
                    {
                        RadGrid2.MasterTableView.GetColumn("TemplateColumn").Visible = false;
                        RadGrid2.MasterTableView.GetColumn("GridClientSelectColumn").Visible = false;
                        RadGrid2.MasterTableView.GetColumn("Image").Visible = false;
                    }

                    if (chkList.SelectedValue == "Recon")
                    {
                        RadGrid3.MasterTableView.GetColumn("TemplateColumn").Visible = false;
                        RadGrid3.MasterTableView.GetColumn("GridClientSelectColumn").Visible = false;
                        RadGrid3.MasterTableView.GetColumn("Image").Visible = false;
                    }

                    if (chkList.SelectedValue == "Company")
                    {
                        RadGrid5.MasterTableView.GetColumn("TemplateColumn").Visible = false;
                        RadGrid5.MasterTableView.GetColumn("GridClientSelectColumn").Visible = false;
                        RadGrid5.MasterTableView.GetColumn("Image").Visible = false;
                    }
        }

                    protected void tbRecord_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
                    {
                        if (e.Item.Text == "Excel" || e.Item.Text == "Word")
                        {
                            HideGridColumnseExport();
                        }

                        GridSettingsPersister obj2 = new GridSettingsPersister();
                        if (chkList.SelectedValue == "Detailed")
                        {

                            obj2.ToolbarButtonClick_Rpt(e, RadGrid1, Utility.ToString(Session["Username"]), 10);

                        }

                        if (chkList.SelectedValue == "Summary")
                        {
                            obj2.ToolbarButtonClick_Rpt(e, RadGrid2, Utility.ToString(Session["Username"]), 10);
                        }

                        if (chkList.SelectedValue == "Recon")
                        {
                            obj2.ToolbarButtonClick_Rpt(e, RadGrid3, Utility.ToString(Session["Username"]), 10);
                        }

                        if (chkList.SelectedValue == "Company")
                        {
                            obj2.ToolbarButtonClick_Rpt(e, RadGrid5, Utility.ToString(Session["Username"]), 10);
                        }

                    }

                    protected void LoadGridSettingsPersister()//call directly from page load
                    {
                        GridSettingsPersister obj = new GridSettingsPersister();
  
                        if (chkList.SelectedValue == "Detailed")
                        {
                            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
                        }

                        if (chkList.SelectedValue == "Summary")
                        {
                            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid2);

                        }

                        if (chkList.SelectedValue == "Recon")
                        {
                            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid3);

                        }
                        if (chkList.SelectedValue == "Company")
                        {
                            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid5);

                        }
                    }

                    protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
                    {
                        GridSettingsPersister obj1 = new GridSettingsPersister();
                        string reportName ="";
                        reportName = "Payroll Register ( Emp Detailed ) For " + cmbMonth.SelectedItem.Text + " " + cmbYear.SelectedItem.Text ;
                        string processPeriod ="Process Period : ALL";
                        string department = " Department  :" + deptID.SelectedItem.Text;
                        obj1.ExportGridHeader_Rpt("103", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e, 10, reportName, processPeriod, department);

                    }



                    protected void RadGrid2_GridExporting(object source, GridExportingArgs e)
                    {
                        GridSettingsPersister obj1 = new GridSettingsPersister();
                        string reportName = "";
                        reportName = "Payroll Register ( Dept ) For " + cmbMonth.SelectedItem.Text + " " + cmbYear.SelectedItem.Text;
                        string processPeriod = "Process Period : ALL";
                        string department = " Department  :" + deptID.SelectedItem.Text;
                        obj1.ExportGridHeader_Rpt("103", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e, 10, reportName, processPeriod, department);

                    }

                    protected void RadGrid5_GridExporting(object source, GridExportingArgs e)
                    {
                        GridSettingsPersister obj1 = new GridSettingsPersister();
                        string reportName = "";
                        reportName = "Payroll Register ( Company ) For " + cmbMonth.SelectedItem.Text + " " + cmbYear.SelectedItem.Text;
                        string processPeriod = "Process Period : ALL";
                        string department = " Department  :" + deptID.SelectedItem.Text;
                        obj1.ExportGridHeader_Rpt("103", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e, 10, reportName, processPeriod, department);

                    }

                    protected void RadGrid3_GridExporting(object source, GridExportingArgs e)
                    {
                        GridSettingsPersister obj1 = new GridSettingsPersister();
                        string reportName = "";
                        reportName = "Payroll Register ( Recon ) For " + cmbMonth.SelectedItem.Text + " " + cmbYear.SelectedItem.Text;
                        string processPeriod = "Process Period : ALL";
                        string department = " Department  :" + deptID.SelectedItem.Text;
                        obj1.ExportGridHeader_Rpt("103", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e, 10, reportName, processPeriod, department);

                    }

                    protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
                    {
                        GridSettingsPersister objCount = new GridSettingsPersister();
                        objCount.RowCount(e, tbRecord);
                    }

                    protected void RadGrid2_ItemCreated(object sender, GridItemEventArgs e)
                    {
                        GridSettingsPersister objCount = new GridSettingsPersister();
                        objCount.RowCount(e, tbRecord);
                    }

                    protected void RadGrid5_ItemCreated(object sender, GridItemEventArgs e)
                    {
                        GridSettingsPersister objCount = new GridSettingsPersister();
                        objCount.RowCount(e, tbRecord);
                    }


                    protected void RadGrid3_ItemCreated(object sender, GridItemEventArgs e)
                    {
                        GridSettingsPersister objCount = new GridSettingsPersister();
                        objCount.RowCount(e, tbRecord);
                    }

        #endregion
        //Toolbar End




    }
}
