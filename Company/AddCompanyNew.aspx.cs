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
using System.Text;
using AuditLibrary;      //Added by Jammu Office
using efdata;
using System.Linq;
using System.Data.Entity;

namespace SMEPayroll.Company
{
    public partial class AddCompanyNew : System.Web.UI.Page
    {
        static int s = 0;
        string compid;
        string cpfceil = "", annualceil = "";
        string basicroundoffdefault = "-1";
        string roundoffdefault = "2";
        string _actionMessage = "";

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        int LoginEmpcode = 0; //Added by Jammu Office
        string companyAliasName;


        protected void rostertype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rostertype.SelectedValue == "1")
            {
                this.overtimemode.Enabled = false;
            }
            else
            {
                this.overtimemode.Enabled = true;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //tblMultiCurrency.Enabled = false;
            //tbsComp.Tabs[10].Enabled = false;

            // AuditContext _context = new AuditContext();
            //var com = _context.Companies.Where(x => x.CompanyId == 4).SingleOrDefault();
            LoginEmpcode = Convert.ToInt32(Session["EmpCode"]); //Added by Jammu Office
            ViewState["actionMessage"] = "";


            if (Session["Country"].ToString() == "383")
            {
                tbsComp.Tabs[2].Visible = false;
                tbsComp.Tabs[6].Visible = false;
            }

            rdMultiCurr.SelectedIndexChanged += new EventHandler(rdMultiCurr_SelectedIndexChanged);
            /* To disable Grid filtering options  */
            //GridFilterMenu menu = RadGrid1.FilterMenu;
            //int i = 0;

            //while (i < menu.Items.Count)
            //{
            //    menu.Items.RemoveAt(i);
            //}
            cmbpayslipformat.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(cmbpayslipformat_SelectedIndexChanged);

            rostertype.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(rostertype_SelectedIndexChanged);

            radItemizeLogo.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(radCustomizeLogo_SelectedIndexChanged);
            //radItemizeLeave.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(radCustomizeLeave_SelectedIndexChanged);
            rdWorkFlow.SelectedIndexChanged += new EventHandler(rdWorkFlow_SelectedIndexChanged);
            rdbGrouping.SelectedIndexChanged += new EventHandler(rdbGrouping_SelectedIndexChanged);

            tbsComp.Tabs[11].Enabled = false; //Added by Sandi on 27/3/2014

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            string SQL = "select monthly_cpf_ceil,annual_cpf_ceil from company where company_id=1";
            //CPF Changes
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQL, null);
            if (dr.Read())
            {
                cpfceil = dr[0].ToString();
                annualceil = dr[1].ToString();
            }
            btnLeaveFF.Click += new EventHandler(btnLeaveFF_Click);

            rdoYes1.Checked = true; //Added by Sandi on 28/03/2014
            Label2.Text = "No. of days ahead a leave can be applied.";
            ////Get the changes for New CPF Table 
            //string month, year;
            //month = DateTime.Now.Month.ToString();
            //year = DateTime.Now.Year.ToString();

            //string date = "01/" + month + "/" + year;
            //string sqlcpf = "Select monthly_cpf_celi ,annual_cpf_ceil from Company_CPF_CEILING where convert(datetime,'" + date + "',103) between convert(datetime,EffectiveDateFrom,103) and convert(datetime,EffectiveDateTo,103)";

            //SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sqlcpf, null);
            //if (dr1.Read())
            //{
            //    cpfceil = dr1[0].ToString();
            //    annualceil = dr1[1].ToString();
            //}
            //MultiCurrency stops


            compid = Request.QueryString["compid"];

            if (compid != null) tbsgiro.Enabled = true;
            else tbsgiro.Enabled = false;


            if (!IsPostBack)
           {
                LoadFormData();
                chkWF.Items[0].Attributes.Add("style", "display:none");
                // chkWF.Items[2].Attributes.Add("style", "display:none");
                chkWF.Items[4].Attributes.Add("style", "display:none");
                //  chkWF.Items[5].Attributes.Add("style", "display:none");
               
            }
            if (IsPostBack)
            {
               
                chkWF.Items[0].Attributes.Add("style", "display:none");
               
                chkWF.Items[4].Attributes.Add("style", "display:none");
              

            }
            if (rdMultiCurr.SelectedValue == "0")
            {
                tblMultiCurrency.Enabled = false;
                tbsComp.Tabs[10].Enabled = false;
            }
            if (rdMultiCurr.SelectedValue == "1")
            {
                tblMultiCurrency.Enabled = true;
                tbsComp.Tabs[10].Enabled = true;
            }



        }


        private void LoadPayslipFormateData()
        {
            if (cmbpayslipformat.SelectedValue == "7")
            {
                string strQuery1 = "Select * from [Report_Settings] Where COMPANYID=" + compid + "  AND PAYSLIPFORMAT=" + cmbpayslipformat.SelectedValue;

                SqlDataReader dr2;

                dr2 = DataAccess.ExecuteReader(CommandType.Text, strQuery1, null);
                tblPaySlipSetup1.Visible = true;
                while (dr2.Read())
                {
                    if (dr2["NAME"].ToString() == "-1")
                    {
                        radPayNameYesNo.Checked = false;
                    }
                    else
                    {
                        radPayNameYesNo.Checked = true;
                        txtPayName.Text = dr2["NAME"].ToString();
                    }

                    if (dr2["IDNO"].ToString() == "-1")
                    {
                        radPayIDNOYesNo.Checked = false;
                    }
                    else
                    {
                        radPayIDNOYesNo.Checked = true;
                        txtPayIDNO.Text = dr2["IDNO"].ToString();
                    }

                    if (dr2["SALFORMONTH"].ToString() == "-1")
                    {
                        radPaySalMonthYesNo.Checked = false;
                    }
                    else
                    {
                        radPaySalMonthYesNo.Checked = true;
                        txtPaySALMONTH.Text = dr2["SALFORMONTH"].ToString();
                    }

                    if (dr2["YEAR"].ToString() == "-1")
                    {
                        radPayYear.Checked = false;
                    }
                    else
                    {
                        radPayYear.Checked = true;
                        txtPayYEAR.Text = dr2["YEAR"].ToString();
                    }

                    if (dr2["EARNINGS"].ToString() == "-1")
                    {
                        radPayEarnings.Checked = false;
                    }
                    else
                    {
                        radPayEarnings.Checked = true;
                        txtPayEARNINGS.Text = dr2["EARNINGS"].ToString();
                    }

                    if (dr2["DEDUCTIONS"].ToString() == "-1")
                    {
                        radPayDeductions.Checked = false;
                    }
                    else
                    {
                        radPayDeductions.Checked = true;
                        txtPayDEDUCTIONS.Text = dr2["DEDUCTIONS"].ToString();
                    }

                    if (dr2["TOTALGROSS"].ToString() == "-1")
                    {
                        radPayTotalGross.Checked = false;
                    }
                    else
                    {
                        radPayTotalGross.Checked = true;
                        txtPayTOTALGROSS.Text = dr2["TOTALGROSS"].ToString();
                    }

                    if (dr2["CPFGROSS"].ToString() == "-1")
                    {
                        radPayCpfGross.Checked = false;
                    }
                    else
                    {
                        radPayCpfGross.Checked = true;
                        txtPayCpfGross.Text = dr2["CPFGROSS"].ToString();
                    }

                    if (dr2["EMPLOYERCPF"].ToString() == "-1")
                    {
                        radPayEmployerCpf.Checked = false;
                    }
                    else
                    {
                        radPayEmployerCpf.Checked = true;
                        txtPayEMPLOYERCPF.Text = dr2["EMPLOYERCPF"].ToString();
                    }

                    if (dr2["TOTALDEDUCTION"].ToString() == "-1")
                    {
                        radPayTotalDeduction.Checked = false;
                    }
                    else
                    {
                        radPayTotalDeduction.Checked = true;
                        txtPayTOTALDEDUCTION.Text = dr2["TOTALDEDUCTION"].ToString();
                    }

                    if (dr2["NETPAYMENT"].ToString() == "-1")
                    {
                        radPayNETPAYMENT.Checked = false;
                    }
                    else
                    {
                        radPayNETPAYMENT.Checked = true;
                        txtPayNETPAYMENT.Text = dr2["NETPAYMENT"].ToString();
                    }

                    if (dr2["YEARTODATE"].ToString() == "-1")
                    {
                        radPayYEARTODATE.Checked = false;
                    }
                    else
                    {
                        radPayYEARTODATE.Checked = true;
                        txtPayYEARTODATE.Text = dr2["YEARTODATE"].ToString();
                    }

                    if (dr2["YEATODATEEMPLOYERCPF"].ToString() == "-1")
                    {
                        radPayYEATODATEEMPLOYERCPF.Checked = false;
                    }
                    else
                    {
                        radPayYEATODATEEMPLOYERCPF.Checked = true;
                        txtPayYEATODATEEMPLOYERCPF.Text = dr2["YEATODATEEMPLOYERCPF"].ToString();
                    }

                    if (dr2["LOGOMGT"].ToString() == "1")
                    {
                        radPayLOGOMGT.SelectedIndex = 0;
                    }
                    if (dr2["LOGOMGT"].ToString() == "2")
                    {
                        radPayLOGOMGT.SelectedIndex = 1;
                    }
                    if (dr2["LOGOMGT"].ToString() == "3")
                    {
                        radPayLOGOMGT.SelectedIndex = 2;
                    }

                    if (dr2["LOGOMGT"].ToString() == "4")
                    {
                        radPayLOGOMGT.SelectedIndex = 3;
                    }
                    ////////////////////////////////////////////////////////////
                    if (dr2["LEAVEDETAILS"].ToString() == "1")
                    {
                        radPayLEAVEDETAILS.SelectedIndex = 0;
                    }
                    if (dr2["LEAVEDETAILS"].ToString() == "2")
                    {
                        radPayLEAVEDETAILS.SelectedIndex = 1;
                    }
                    ////////////////////////////////////////////////////////////
                    if (dr2["ADDITIONSDETAILS"].ToString() == "1")
                    {
                        radPayEARNINGDETAILS.SelectedIndex = 0;
                    }
                    if (dr2["ADDITIONSDETAILS"].ToString() == "2")
                    {
                        radPayEARNINGDETAILS.SelectedIndex = 1;
                    }
                    if (dr2["ADDITIONSDETAILS"].ToString() == "3")
                    {
                        radPayEARNINGDETAILS.SelectedIndex = 2;
                    }
                    ////////////////////////////////////////////////////////////
                    if (dr2["DEPTNAME"].ToString() == "-1")
                    {
                        radPayDEPTNAME.Checked = false;
                    }
                    else
                    {
                        radPayDEPTNAME.Checked = true;
                        txtPayDepartmentName.Text = dr2["DEPTNAME"].ToString();
                    }

                    if (dr2["TRADE"].ToString() == "-1")
                    {
                        radPayTrade.Checked = false;
                    }
                    else
                    {
                        radPayTrade.Checked = true;
                        txtPayTrade.Text = dr2["TRADE"].ToString();
                    }

                    if (dr2["DESIGNATION"].ToString() == "-1")
                    {
                        radPayDesignation.Checked = false;
                    }
                    else
                    {
                        radPayDesignation.Checked = true;
                        txtPayDesignation.Text = dr2["DESIGNATION"].ToString();
                    }
                }
            }
            else if (cmbpayslipformat.SelectedValue == "10")
            {
                string strQuery2 = "Select * from [Report_Settings] Where COMPANYID=" + compid + "  AND PAYSLIPFORMAT=" + cmbpayslipformat.SelectedValue;

                SqlDataReader dr3;

                dr3 = DataAccess.ExecuteReader(CommandType.Text, strQuery2, null);
                tblPayslipSetup2.Visible = true;
                while (dr3.Read())
                {
                    //By Jammu Office
                    //if (dr3["NAME"].ToString() == "-1")
                    //{
                    //    radCustomizePayNameYesNo.SelectedIndex = 1;
                    //}
                    //else
                    //{
                    //    radCustomizePayNameYesNo.SelectedIndex = 0;
                    //    txtCustomizePayName.Text = dr3["NAME"].ToString();
                    //}
                    if (dr3["NAME"].ToString() == "-1")
                    {
                        radCustomizePayNameYesNo.Checked = false;
                    }
                    else
                    {
                        radCustomizePayNameYesNo.Checked = true;
                        txtCustomizePayName.Text = dr3["NAME"].ToString();
                    }


                    //if (dr3["IDNO"].ToString() == "-1")
                    //{
                    //    radCustomizeIdNo.SelectedIndex = 1;
                    //}
                    //else
                    //{
                    //    radCustomizeIdNo.SelectedIndex = 0;
                    //    txtCustomizeIdNo.Text = dr3["IDNO"].ToString();
                    //}
                    if (dr3["IDNO"].ToString() == "-1")
                    {
                        radCustomizeIdNo.Checked = false;
                    }
                    else
                    {
                        radCustomizeIdNo.Checked = true;
                        txtCustomizeIdNo.Text = dr3["IDNO"].ToString();
                    }




                    //if (dr3["SALFORMONTH"].ToString() == "-1")
                    //{
                    //    radCustomizeSalaryForMonth.SelectedIndex = 1;
                    //}
                    //else
                    //{
                    //    radCustomizeSalaryForMonth.SelectedIndex = 0;
                    //    txtCustomizeSalary.Text = dr3["SALFORMONTH"].ToString();
                    //}
                    if (dr3["SALFORMONTH"].ToString() == "-1")
                    {
                        radCustomizeSalaryForMonth.Checked = false;
                    }
                    else
                    {
                        radCustomizeSalaryForMonth.Checked = true;
                        txtCustomizeSalary.Text = dr3["SALFORMONTH"].ToString();
                    }



                    //if (dr3["YEAR"].ToString() == "-1")
                    //{
                    //    radCustomizePayYear.SelectedIndex = 1;
                    //}
                    //else
                    //{
                    //    radCustomizePayYear.SelectedIndex = 0;
                    //    txtCustomizePayYear.Text = dr3["YEAR"].ToString();
                    //}
                    if (dr3["YEAR"].ToString() == "-1")
                    {
                        radCustomizePayYear.Checked = false;
                    }
                    else
                    {
                        radCustomizePayYear.Checked = true;
                        txtCustomizePayYear.Text = dr3["YEAR"].ToString();
                    }
                    

                    //if (dr3["EARNINGS"].ToString() == "-1")
                    //{
                    //    radCustomizeEarnings.SelectedIndex = 1;
                    //}
                    //else
                    //{
                    //    radCustomizeEarnings.SelectedIndex = 0;
                    //    txtCustomizeEarnings.Text = dr3["EARNINGS"].ToString();
                    //}
                    if (dr3["EARNINGS"].ToString() == "-1")
                    {
                        radCustomizeEarnings.Checked = false;
                    }
                    else
                    {
                        radCustomizeEarnings.Checked = true;
                        txtCustomizeEarnings.Text = dr3["EARNINGS"].ToString();
                    }

                    //if (dr3["DEDUCTIONS"].ToString() == "-1")
                    //{
                    //    radCustomizeDeductions.SelectedIndex = 1;
                    //}
                    //else
                    //{
                    //    radCustomizeDeductions.SelectedIndex = 0;
                    //    txtCustomizeDeductions.Text = dr3["DEDUCTIONS"].ToString();
                    //}
                    if (dr3["DEDUCTIONS"].ToString() == "-1")
                    {
                        radCustomizeDeductions.Checked = false;
                    }
                    else
                    {
                        radCustomizeDeductions.Checked = true;
                        txtCustomizeDeductions.Text = dr3["DEDUCTIONS"].ToString();
                    }




                    //if (dr3["TOTALGROSS"].ToString() == "-1")
                    //{
                    //    radCustomizeTotalGross.SelectedIndex = 1;
                    //}
                    //else
                    //{
                    //    radCustomizeTotalGross.SelectedIndex = 0;
                    //    txtCustomizeTotalGross.Text = dr3["TOTALGROSS"].ToString();
                    //}
                    if (dr3["TOTALGROSS"].ToString() == "-1")
                    {
                        radCustomizeTotalGross.Checked = false;
                    }
                    else
                    {
                        radCustomizeTotalGross.Checked = true;
                        txtCustomizeTotalGross.Text = dr3["TOTALGROSS"].ToString();
                    }




                    if (dr3["CPFGROSS"].ToString() == "-1")
                    {
                        radCustomizeCpfGross.Checked = false;
                    }
                    else
                    {
                        radCustomizeCpfGross.Checked = true;
                        txtCustomizeCpfGross.Text = dr3["CPFGROSS"].ToString();
                    }

                    if (dr3["EMPLOYERCPF"].ToString() == "-1")
                    {
                        radCustomizeEmployerCpf.Checked = false;
                    }
                    else
                    {
                        radCustomizeEmployerCpf.Checked = true;
                        txtCustomizeEmployerCpf.Text = dr3["EMPLOYERCPF"].ToString();
                    }

                    if (dr3["TOTALDEDUCTION"].ToString() == "-1")
                    {
                        radCustomizeTotalDeduction.Checked = false;
                    }
                    else
                    {
                        radCustomizeTotalDeduction.Checked = true;
                        txtCustomizeTotalDeduction.Text = dr3["TOTALDEDUCTION"].ToString();
                    }

                    if (dr3["NETPAYMENT"].ToString() == "-1")
                    {
                        radCustomizeNetPayment.Checked = false;
                    }
                    else
                    {
                        radCustomizeNetPayment.Checked = true;
                        txtCustomizeNetPayment.Text = dr3["NETPAYMENT"].ToString();
                    }

                    if (dr3["YEARTODATE"].ToString() == "-1")
                    {
                        radCustomizeYearToDate.Checked = false;
                    }
                    else
                    {
                        radCustomizeYearToDate.Checked = true;
                        txtCustomizeYearToDate.Text = dr3["YEARTODATE"].ToString();
                    }

                    if (dr3["YEATODATEEMPLOYERCPF"].ToString() == "-1")
                    {
                        radCustomizeYearToDateEmployerCPF.Checked = false;
                    }
                    else
                    {
                        radCustomizeYearToDateEmployerCPF.Checked = true;
                        txtCustomizeYearToDateEmployerCPF.Text = dr3["YEATODATEEMPLOYERCPF"].ToString();
                    }

                    if (dr3["LOGOMGT"].ToString() == "1")
                    {
                        radCustomizeLogoManagement.SelectedIndex = 0;
                    }
                    if (dr3["LOGOMGT"].ToString() == "2")
                    {
                        radCustomizeLogoManagement.SelectedIndex = 1;
                    }
                    if (dr3["LOGOMGT"].ToString() == "3")
                    {
                        radCustomizeLogoManagement.SelectedIndex = 2;
                    }

                    if (dr3["LOGOMGT"].ToString() == "4")
                    {
                        radCustomizeLogoManagement.SelectedIndex = 3;
                    }
                    ////////////////////////////////////////////////////////////
                    if (dr3["LEAVEDETAILS"].ToString() == "1")
                    {
                        radCustomizePayLEAVEDETAILS.SelectedIndex = 0;
                    }
                    if (dr3["LEAVEDETAILS"].ToString() == "2")
                    {
                        radCustomizePayLEAVEDETAILS.SelectedIndex = 1;
                    }
                    ////////////////////////////////////////////////////////////
                    if (dr3["ADDITIONSDETAILS"].ToString() == "1")
                    {
                        radCUSTOMIZEPayEARNINGDETAILS.SelectedIndex = 0;
                    }
                    if (dr3["ADDITIONSDETAILS"].ToString() == "2")
                    {
                        radCUSTOMIZEPayEARNINGDETAILS.SelectedIndex = 1;
                    }
                    if (dr3["ADDITIONSDETAILS"].ToString() == "3")
                    {
                        radCUSTOMIZEPayEARNINGDETAILS.SelectedIndex = 2;
                    }
                    ////////////////////////////////////////////////////////////
                    if (dr3["DEPTNAME"].ToString() == "-1")
                    {
                        radCustomizeDepartmentName.Checked = false;
                    }
                    else
                    {
                        radCustomizeDepartmentName.Checked = true;
                        txtCustomizeDepartmentName.Text = dr3["DEPTNAME"].ToString();
                    }

                    if (dr3["TRADE"].ToString() == "-1")
                    {
                        radCustomizeTrade.Checked = false;
                    }
                    else
                    {
                        radCustomizeTrade.Checked = true;
                        txtCustomizeTrade.Text = dr3["TRADE"].ToString();
                    }

                    if (dr3["DESIGNATION"].ToString() == "-1")
                    {
                        radCustomizeDesignation.Checked = false;
                    }
                    else
                    {
                        radCustomizeDesignation.Checked = true;
                        txtCustomizeDesignation.Text = dr3["DESIGNATION"].ToString();
                    }

                    if (dr3["DOB"].ToString() == "-1")
                    {
                        radCustomizeDOB.Checked = false;
                    }
                    else
                    {
                        radCustomizeDOB.Checked = true;
                        txtCustomizeDOB.Text = dr3["DOB"].ToString();
                    }
                    if (dr3["TIMECARDNO"].ToString() == "-1")
                    {
                        radCustomizeTimecardNo.Checked = false;
                    }
                    else
                    {
                        radCustomizeTimecardNo.Checked = true;
                        txtCustomizeTimecardNo.Text = dr3["TIMECARDNO"].ToString();
                    }
                    if (dr3["JOININGDATE"].ToString() == "-1")
                    {
                        radCustomizeJoiningDate.Checked = false;
                    }
                    else
                    {
                        radCustomizeJoiningDate.Checked = true;
                        txtCustomizeJoiningDate.Text = dr3["JOININGDATE"].ToString();
                    }
                    if (dr3["TERMINATIONDATE"].ToString() == "-1")
                    {
                        radCustomizeTerminationDate.Checked = false;
                    }
                    else
                    {
                        radCustomizeTerminationDate.Checked = true;
                        txtCustomizeTerminationDate.Text = dr3["TERMINATIONDATE"].ToString();
                    }

                    if (dr3["BUSINESSUNIT"].ToString() == "-1")
                    {
                        radCustomizeBusinessUnit.Checked = false;
                    }
                    else
                    {
                        radCustomizeBusinessUnit.Checked = true;
                        txtCustomizeBusinessUnit.Text = dr3["BUSINESSUNIT"].ToString();
                    }

                    if (dr3["PAYSLIPPERIOD"].ToString() == "-1")
                    {
                        radCustomizePayslipPeriod.Checked = false;
                    }
                    else
                    {
                        radCustomizePayslipPeriod.Checked = true;
                        txtCustomizePayslipPeriod.Text = dr3["PAYSLIPPERIOD"].ToString();
                    }
                    if (dr3["OVERTIMEPERIOD"].ToString() == "-1")
                    {
                        radCustomizeOvertimePeriod.Checked = false;
                    }
                    else
                    {
                        radCustomizeOvertimePeriod.Checked = true;
                        txtCustomizeOvertimePeriod.Text = dr3["OVERTIMEPERIOD"].ToString();
                    }
                    if (dr3["TOTALADDITIONS"].ToString() == "-1")
                    {
                        radCustomizeTotalAdditions.Checked = false;
                    }
                    else
                    {
                        radCustomizeTotalAdditions.Checked = true;
                        txtCustomizeTotalAdditions.Text = dr3["TOTALADDITIONS"].ToString();
                    }
                    if (dr3["DATEOFPAYMENT"].ToString() == "-1")
                    {
                        radCustomizeDateOfPayment.Checked = false;
                    }
                    else
                    {
                        radCustomizeDateOfPayment.Checked = true;
                        txtCustomizeDateOfPayment.Text = dr3["DATEOFPAYMENT"].ToString();
                    }
                    if (dr3["MODEOFPAYMENT"].ToString() == "-1")
                    {
                        radCustomizeModeOfPayment.Checked = false;
                    }
                    else
                    {
                        radCustomizeModeOfPayment.Checked = true;
                        txtCustomizeModeOfPayment.Text = dr3["MODEOFPAYMENT"].ToString();
                    }
                    if (dr3["YEARTODATEEMPLOYEECPF"].ToString() == "-1")
                    {
                        radYearToDateEmployeeCPF.Checked = false;
                    }
                    else
                    {
                        radYearToDateEmployeeCPF.Checked = true;
                        txtYearToDateEmployeeCPF.Text = dr3["YEARTODATEEMPLOYEECPF"].ToString();
                    }
                    if (dr3["REMARKS"].ToString() == "-1")
                    {
                        radCustomizeRemarks.Checked = false;
                    }
                    else
                    {
                        radCustomizeRemarks.Checked = true;
                        txtCustomizeRemarks.Text = dr3["REMARKS"].ToString();
                    }
                    if (dr3["CHEQUENO"].ToString() == "-1")
                    {
                        radChequeNumber.Checked = false;
                    }
                    else
                    {
                        radChequeNumber.Checked = true;
                        txtChequeNumber.Text = dr3["CHEQUENO"].ToString();
                    }
                }
            }
            else if (cmbpayslipformat.SelectedValue == "12")
            {
                string strQuery3 = "Select * from [Report_Settings] Where COMPANYID=" + compid + "  AND PAYSLIPFORMAT=" + cmbpayslipformat.SelectedValue;

                SqlDataReader dr4;

                dr4 = DataAccess.ExecuteReader(CommandType.Text, strQuery3, null);
                tblMOMItemized.Visible = true;
                while (dr4.Read())
                {


                    if (dr4["LOGOMGT"].ToString() == "1")
                    {
                        radItemizeLogoManagement.SelectedIndex = 0;
                    }
                    if (dr4["LOGOMGT"].ToString() == "2")
                    {
                        radItemizeLogoManagement.SelectedIndex = 1;
                    }
                    if (dr4["LOGOMGT"].ToString() == "3")
                    {
                        radItemizeLogoManagement.SelectedIndex = 2;
                    }

                    if (dr4["LOGOMGT"].ToString() == "4")
                    {
                        radItemizeLogoManagement.SelectedIndex = 3;
                    }
                    ////////////////////////////////////////////////////////////
                    //if (dr4["LEAVEDETAILS"].ToString() == "1")
                    //{
                    //    radItemizeLEAVEDETAILS.SelectedIndex = 0;
                    //}
                    //if (dr4["LEAVEDETAILS"].ToString() == "2")
                    //{
                    //    radItemizeLEAVEDETAILS.SelectedIndex = 1;
                    //}
                    ////////////////////////////////////////////////////////////

                }

            }
        }

        private void LoadFormData()
        {


            //Currecny Data binding
            DataSet dscurr = new DataSet();
            string sqlCurr = "Select id, Currency + ':-->' + Symbol Curr from currency";
            dscurr = DataAccess.FetchRS(CommandType.Text, sqlCurr, null);
            drpCurrency.DataSource = dscurr.Tables[0];
            drpCurrency.DataTextField = dscurr.Tables[0].Columns["Curr"].ColumnName.ToString();
            drpCurrency.DataValueField = dscurr.Tables[0].Columns["id"].ColumnName.ToString();
            drpCurrency.DataBind();

            dropdown_binding();
            string sql = "Select * From HourTransfer";
            DataSet dsts = new DataSet();
            dsts = DataAccess.FetchRS(CommandType.Text, sql, null);
            cmbPublicHoliday.DataSource = dsts;
            cmbPublicHoliday.DataTextField = "TranferName";
            cmbPublicHoliday.DataValueField = "ID";
            cmbPublicHoliday.DataBind();

            cmbSunday.DataSource = dsts;
            cmbSunday.DataTextField = "TranferName";
            cmbSunday.DataValueField = "ID";
            cmbSunday.DataBind();

            cmbRosterNa.DataSource = dsts;
            cmbRosterNa.DataTextField = "TranferName";
            cmbRosterNa.DataValueField = "ID";
            cmbRosterNa.DataBind();

           
            //-comment by murugan
            //if (rdWorkFlow.SelectedIndex == 0 || rdWorkFlow.SelectedIndex == 1)
            //{
            //    chkWF.Items[0].Enabled = false;
            //    chkWF.Items[1].Enabled = false;
            //    chkWF.Items[2].Enabled = false;
            //    chkWF.Items[3].Enabled = false;
            //    chkWF.Items[4].Enabled = false;
            //    chkWF.Items[5].Enabled = false;
            //}
            //else
            //{
            //    chkWF.Items[0].Enabled = false;
            //    chkWF.Items[1].Enabled = false;
            //    chkWF.Items[2].Enabled = false;
            //    chkWF.Items[3].Enabled = true;
            //    chkWF.Items[4].Enabled = false;
            //    chkWF.Items[5].Enabled = false;
            //}



            if (compid != null)
            {
                txtCompCode.Enabled = false;// diasable the prefix textbox while Edit

                s = 1;
                DataSet Compset = new DataSet();
                //CPF Changes
                string Str = " select UnpaidLAmount,company_code,company_name,phone,email,website,city,Fax,address2,postal_code,";
                Str += "country,auth_person,designation,Address,Auth_email,cpf_ref_no,currency,no_work_days,day_hours,";
                Str += "monthly_cpf_ceil,annual_cpf_ceil,ytd_earning,sdf_income,sdf_percent,min_sdf_contrib,email_leavealert,email_payalert,";
                Str += " payslip_format,working_days, Payroll_Approval, Payroll_Authority,email_sender,email_SMTP_server,email_username,";
                Str += "email_password,email_sender_domain,email_sender_name,email_reply_address,email_reply_name,email_SMTP_port,state,";
                Str += "timesheet_approve,epayslip,leave_model,email_claim_sender_name,email_claim_reply_name,email_claimalert,";
                Str += "company_roc,company_type,sslrequired,pwdrequired,ccmail,ccalert_claims,ccalert_leaves,day_minute,basicrnd,additionsrnd,deductionsrnd,netpayrnd,payrolltype,email_leave_delete,isTSRemarks,projectassign,SalaryGLCode,EmployeeCPFGLCode,EmployerCPFGLCode,FundAmtGLCode,SDLAmtGLCode,AccountGLCode,UnpaidLeaGLCode,TsPublicH,Sunday,NoRoster,FIFO,Rounding,SendEmail,EmpProcessor,ProcessEmail,isMaster=case when isMaster Is null then 0 else isMaster end,isMasterEmpTemp= case when isMasterEmpTemp Is null then 0 else isMasterEmpTemp end,AdvTs,LeaveFFDate,WorkFlowID,WFEMP,WFLEAVE,WFCLAIM,WFPAY,WFReport,WFTimeSheet,AppTSProcess,AppLeaveProcess,AppClaimsProcess,FOWL,CurrencyID,ClaimsCash,ConversionOpt,MultiCurr,RemarksYN,NormalHrBT,OverTHrBT,AdvClaims,isnull(OffDay1,'') OffDay1,isnull(OffDay2,'') OffDay2,isnull(HalfDay1,0) HalfDay1,isnull(UseLeaveCal,0) UseLeaveCal,isnull(CustomizedCal,'') CustomizedCal,isnull(LeaveDayAhead,'') LeaveDayAhead,isnull(IncludingCurrentDay,0) IncludingCurrentDay,isnull(RosterType,1)As RosterType ,isnull(WeekRosterOtMode,'true')as WeekRosterOtMode,loginWithOutComany,mobileTimeSheet,GroupManage,showroster,showAccountNo,EnableAutoReminder,incompleatemonthManvalRate,isApproveDate,ccTimeSheet,OTseparate,OTGLCode,SDLPayableGL,SalaryPayableGL,CPFPayableGL,TSrequired,WFAppraisal,APPrequired  from company where Company_Id=" + compid + ""; //Added two columns by Sandi on 31/03/2014

                Compset = DataAccess.FetchRS(CommandType.Text, Str, null);

                // Added by Su Mon
                string OffDay1 = "";
                string OffDay2 = "";
                bool HalfDay1 = false;
                bool UseLeaveCal = false;
                string CustomizedCal = "";
                string showAccountNoValue = "1";
                string incompleatemonthManvalRate = "0";

                CustomizedCal = Compset.Tables[0].Rows[0]["CustomizedCal"].ToString();
                OffDay1 = Compset.Tables[0].Rows[0]["OffDay1"].ToString();
                OffDay2 = Compset.Tables[0].Rows[0]["OffDay2"].ToString();
                UseLeaveCal = bool.Parse(Compset.Tables[0].Rows[0]["UseLeaveCal"].ToString());

                if (!string.IsNullOrEmpty(Compset.Tables[0].Rows[0]["EnableAutoReminder"].ToString()))
                {

                    this.autoreminder.SelectedValue = Compset.Tables[0].Rows[0]["EnableAutoReminder"].ToString();

                }
                if (!string.IsNullOrEmpty(Compset.Tables[0].Rows[0]["showAccountNo"].ToString()))
                {
                    showAccountNoValue = Compset.Tables[0].Rows[0]["showAccountNo"].ToString();

                }
                if (!string.IsNullOrEmpty(Compset.Tables[0].Rows[0]["incompleatemonthManvalRate"].ToString()))
                {
                    incompleatemonthManvalRate = Compset.Tables[0].Rows[0]["incompleatemonthManvalRate"].ToString();

                }



                if (showAccountNoValue == "1")
                {
                    this.ShowBankAcNo.Checked = true;
                }
                else
                {
                    this.ShowBankAcNo.Checked = false;
                }

                if (incompleatemonthManvalRate == "1")
                {
                    this.incommanuvalRate.Checked = true;
                }
                else
                {
                    this.incommanuvalRate.Checked = false;
                }


                if (CustomizedCal == "default" || CustomizedCal == "d")
                {
                    rdoYes.Checked = true;
                    rdoYes1.Checked = true; //Added by Sandi on 26/03/2014
                }
                else
                {
                    rdoNo.Checked = true;
                    rdoNo1.Checked = true; //Added by Sandi on 26/03/2014
                }

                if (CustomizedCal == "half" || CustomizedCal == "h")
                {
                    tbsComp.Tabs[11].Enabled = true; //Added by Sandi on 27/03/2014
                    rdoNo.Checked = true;
                    rdoNo1.Checked = true; //Added by Sandi on 26/03/2014

                    rdoHide.Checked = true;
                    if (OffDay1 != "")
                    {
                        cmdOffDay1.SelectedValue = OffDay1;
                    }
                    if (OffDay2 != "")
                    {
                        cmdOffDay2.SelectedValue = OffDay2;
                    }

                    if (UseLeaveCal == true)
                    {
                        rdoLeaveCal.Checked = true;
                    }
                }

                if (CustomizedCal == "full" || CustomizedCal == "f")
                {
                    tbsComp.Tabs[11].Enabled = true; //Added by Sandi on 27/03/2014
                    rdoNo.Checked = true;
                    rdoNo1.Checked = true; //Added by Sandi on 26/03/2014
                    rdoShow.Checked = true;

                    if (UseLeaveCal == true)
                    {
                        rdoLeaveCal.Checked = true;
                    }
                }

                // End added
                //Added by Sandi on 31/3/2014
                int LeaveDayAhead = Convert.ToInt32(Compset.Tables[0].Rows[0]["LeaveDayAhead"].ToString());
                rtxtLeaveDayAhead.Text = Compset.Tables[0].Rows[0]["LeaveDayAhead"].ToString();
                bool IncludeCurrentDay = bool.Parse(Compset.Tables[0].Rows[0]["IncludingCurrentDay"].ToString());

                if (IncludeCurrentDay == true)
                {
                    chkIncludingCurrentDay.Checked = true;
                }
                else
                {
                    chkIncludingCurrentDay.Checked = false;
                }
                //End Added

                //RemarksYN,NormalHrBT,OverTHrBT
                if (System.DBNull.Value == Compset.Tables[0].Rows[0]["AdvClaims"])
                {
                    chkAdClaims.Checked = false;
                }
                else if (Compset.Tables[0].Rows[0]["AdvClaims"].ToString() == "0")
                {
                    chkAdClaims.Checked = false;
                }
                else if (Compset.Tables[0].Rows[0]["AdvClaims"].ToString() == "1")
                {
                    chkAdClaims.Checked = true;
                }


                //if (!string.IsNullOrEmpty(Compset.Tables[0].Rows[0]["AdvClaims"].ToString()))
                //{
                //    loginWithOutComany.SelectedValue = Compset.Tables[0].Rows[0]["AdvClaims"].ToString();
                //}

                this.mobilescancode.Checked = false;
                if (!string.IsNullOrEmpty(Compset.Tables[0].Rows[0]["mobileTimeSheet"].ToString()))
                {

                    if (Compset.Tables[0].Rows[0]["mobileTimeSheet"].ToString() == "True")
                    {
                        this.mobilescancode.Checked = true;
                    }
                }

                this.showroster.Checked = false;
                if (!string.IsNullOrEmpty(Compset.Tables[0].Rows[0]["showroster"].ToString()))
                {

                    if (Compset.Tables[0].Rows[0]["showroster"].ToString() == "True")
                    {
                        this.showroster.Checked = true;
                    }
                }





                //RemarksYN,NormalHrBT,OverTHrBT
                if (System.DBNull.Value == Compset.Tables[0].Rows[0]["RemarksYN"])
                {
                    chkBoxTs.Items[0].Selected = false;
                }
                else if (Compset.Tables[0].Rows[0]["RemarksYN"].ToString() == "A")
                {
                    chkBoxTs.Items[0].Selected = false;
                }
                else if (Compset.Tables[0].Rows[0]["RemarksYN"].ToString() == "RE")
                {
                    chkBoxTs.Items[0].Selected = true;
                }

                if (System.DBNull.Value == Compset.Tables[0].Rows[0]["NormalHrBT"])
                {
                    chkBoxTs.Items[1].Selected = false;
                }
                else if (Compset.Tables[0].Rows[0]["NormalHrBT"].ToString() == "A")
                {
                    chkBoxTs.Items[1].Selected = false;
                }
                else if (Compset.Tables[0].Rows[0]["NormalHrBT"].ToString() == "NOB")
                {
                    chkBoxTs.Items[1].Selected = true;
                }

                if (System.DBNull.Value == Compset.Tables[0].Rows[0]["OverTHrBT"])
                {
                    chkBoxTs.Items[2].Selected = false;
                }
                else if (Compset.Tables[0].Rows[0]["OverTHrBT"].ToString() == "A")
                {
                    chkBoxTs.Items[2].Selected = false;
                }
                else if (Compset.Tables[0].Rows[0]["OverTHrBT"].ToString() == "OTB")
                {
                    chkBoxTs.Items[2].Selected = true;
                }
                /////////////////////////////////////////////////////////////////////
                if (System.DBNull.Value == Compset.Tables[0].Rows[0]["ClaimsCash"])
                {
                    chkClaims.Checked = false;
                }
                else if (Compset.Tables[0].Rows[0]["ClaimsCash"].ToString() == "1")
                {
                    chkClaims.Checked = false;
                }
                else if (Compset.Tables[0].Rows[0]["ClaimsCash"].ToString() == "2")
                {
                    chkClaims.Checked = true;
                }

                //MultiCurrency start
                if (System.DBNull.Value == Compset.Tables[0].Rows[0]["MultiCurr"])
                {
                    rdMultiCurr.SelectedValue = "0";
                    tblMultiCurrency.Enabled = false;
                    tbsComp.Tabs[10].Enabled = false;
                }
                else if (Compset.Tables[0].Rows[0]["MultiCurr"].ToString() == "0")
                {
                    rdMultiCurr.SelectedValue = "0";
                    tblMultiCurrency.Enabled = false;
                    tbsComp.Tabs[10].Enabled = false;
                }
                else if (Compset.Tables[0].Rows[0]["MultiCurr"].ToString() == "1")
                {
                    rdMultiCurr.SelectedValue = "1";
                    tblMultiCurrency.Enabled = true;
                    tbsComp.Tabs[10].Enabled = true;
                }

                //MultiCurrency stops
                //if (rdMultiCurr.SelectedValue == "0")
                //{
                //    drpConv.Enabled = false;
                //    drpConv.SelectedValue = "1";
                //    drpCurrency.SelectedIndex = 0;
                //    drpCurrency.Enabled = false;
                //}


                if (System.DBNull.Value != Compset.Tables[0].Rows[0]["CurrencyID"])
                {
                    //int val = Convert.ToInt32(Compset.Tables[0].Rows[0]["CurrencyID"].ToString());
                    drpCurrency.SelectedValue = Compset.Tables[0].Rows[0]["CurrencyID"].ToString();
                }
                else
                {
                    drpCurrency.SelectedValue = "2";
                }

                if (System.DBNull.Value != Compset.Tables[0].Rows[0]["ConversionOpt"])
                {
                    drpConv.SelectedValue = Compset.Tables[0].Rows[0]["ConversionOpt"].ToString();
                }
                else
                {
                    drpConv.SelectedValue = "1";
                }

                //(D)Settings
                if (System.DBNull.Value == Compset.Tables[0].Rows[0]["UnpaidLAmount"])
                {
                    chkLeave.Checked = true;
                }
                else if (Compset.Tables[0].Rows[0]["UnpaidLAmount"].ToString() == "1")
                {
                    chkLeave.Checked = true;
                }
                else if (Compset.Tables[0].Rows[0]["UnpaidLAmount"].ToString() == "0")
                {
                    chkLeave.Checked = false;
                }

                if (System.DBNull.Value != Compset.Tables[0].Rows[0]["SendEmail"]) //first time value will be null
                {
                    if ((bool)Compset.Tables[0].Rows[0]["SendEmail"])
                    {
                        cbxEmailAlert.SelectedValue = "Yes";
                        drpEmpProc1.Visible = true;
                    }
                    else
                    {
                        cbxEmailAlert.SelectedValue = "No";
                        drpEmpProc1.Visible = false;
                    }
                }
                else
                {
                    cbxEmailAlert.SelectedValue = "No";
                    drpEmpProc1.Visible = false;
                }

                if (System.DBNull.Value != Compset.Tables[0].Rows[0]["AdvTs"])
                {
                    int val = Convert.ToInt32(Compset.Tables[0].Rows[0]["AdvTs"].ToString());

                    if (val == -1)
                    {
                        radAdvanceTs.SelectedIndex = 0;
                        Label1.Visible = false;
                        txtMinutes.Visible = false;
                    }
                    else
                    {
                        radAdvanceTs.SelectedIndex = 1;
                        txtMinutes.Text = Compset.Tables[0].Rows[0]["AdvTs"].ToString();

                        Label1.Visible = true;
                        txtMinutes.Visible = true;
                    }
                }

                if (System.DBNull.Value == Compset.Tables[0].Rows[0]["FOWL"])
                {
                    chkWL.Checked = true;
                }
                else if (Compset.Tables[0].Rows[0]["FOWL"].ToString() == "1")
                {
                    chkWL.Checked = true;
                }
                else if (Compset.Tables[0].Rows[0]["FOWL"].ToString() == "0")
                {
                    chkWL.Checked = false;
                }

                if (System.DBNull.Value != Compset.Tables[0].Rows[0]["EmpProcessor"]) //first time value will be null
                {
                    if ((bool)Compset.Tables[0].Rows[0]["EmpProcessor"])
                    {
                        drpEmpProc1.SelectedValue = "Processer";
                        txtProcesserEmail.Visible = true;
                        txtProcesserEmail.Text = (string)Compset.Tables[0].Rows[0]["ProcessEmail"];
                    }
                    else
                    {
                        txtProcesserEmail.Visible = false;
                        drpEmpProc1.SelectedValue = "Employee";
                    }

                }
                else
                {
                    txtProcesserEmail.Visible = false;
                    drpEmpProc1.SelectedValue = "Employee";
                }
                //Leaves LeaveFFDate
                if (Compset.Tables[0].Rows[0]["LeaveFFDate"] != System.DBNull.Value)
                {
                    //{}
                    if (Compset.Tables[0].Rows[0]["LeaveFFDate"].ToString() != "{}")
                    {
                        radLFort.SelectedDate = Convert.ToDateTime(Compset.Tables[0].Rows[0]["LeaveFFDate"]);
                        if (radLFort.SelectedDate.Value.Year == DateTime.Now.Year)
                        {
                            int val = DateTime.Now.Date.CompareTo(radLFort.SelectedDate.Value.Date);
                            if (val >= 0)
                            {
                                btnLeaveFF.Enabled = true;
                            }

                        }
                    }
                }
                //Changes for Work Flow...
                chkWF.Items[0].Attributes.Add("style", "display:none");
                chkWF.Items[4].Attributes.Add("style", "display:none");
                chkWF.Items[3].Attributes.Add("style", "display:none");
                if (Compset.Tables[0].Rows[0]["WorkFlowID"] == System.DBNull.Value)
                {
                    rdWorkFlow.SelectedIndex = 0;
                    chkWF.Items[0].Enabled = false;     //-murugan
                    chkWF.Items[1].Enabled = false;
                    chkWF.Items[2].Enabled = false;
                    chkWF.Items[3].Enabled = false;
                    chkWF.Items[4].Enabled = false;
                    chkWF.Items[5].Enabled = false;
                    chkWF.Items[6].Enabled = false; //appraisal
                    rdbGrouping.Enabled = true;
                    chkAdClaims.Enabled = true;
                }

                else if (Compset.Tables[0].Rows[0]["WorkFlowID"].ToString() == "-1")
                {
                    rdWorkFlow.SelectedIndex = 0;
                    chkWF.Items[0].Enabled = false;     //-murugan
                    chkWF.Items[1].Enabled = false;
                    chkWF.Items[2].Enabled = false;
                    chkWF.Items[3].Enabled = false;
                    chkWF.Items[4].Enabled = false;
                    chkWF.Items[5].Enabled = false;
                    chkWF.Items[6].Enabled = false; //appraisal
                    rdbGrouping.Enabled = true;
                    chkAdClaims.Enabled = true;
                }

                else if (Compset.Tables[0].Rows[0]["WorkFlowID"].ToString() == "1")
                {
                    rdWorkFlow.SelectedIndex = 1;
                    chkWF.Items[0].Attributes.Add("style", "display:none");
                    chkWF.Items[1].Enabled = true;
                    chkWF.Items[1].Attributes.Add("style", "display:block");
                    chkWF.Items[2].Enabled = true;
                    chkWF.Items[2].Attributes.Add("style", "display:block");
                    chkWF.Items[3].Attributes.Add("style", "display:none");
                     chkWF.Items[4].Attributes.Add("style", "display:none");
                    chkWF.Items[5].Enabled = true;
                    chkWF.Items[5].Attributes.Add("style", "display:block");
                    chkWF.Items[6].Enabled = true;
                    chkWF.Items[6].Attributes.Add("style", "display:block"); //appraisal
                    rdbGrouping.Enabled = false;
                    chkAdClaims.Enabled = false;
                }

                else if (Compset.Tables[0].Rows[0]["WorkFlowID"].ToString() == "2")
                {
                    rdWorkFlow.SelectedIndex = 2;
                    chkWF.Items[0].Attributes.Add("style", "display:none");
                    chkWF.Items[1].Attributes.Add("style", "display:none");
                    chkWF.Items[2].Attributes.Add("style", "display:none");
                    chkWF.Items[3].Enabled = true;
                    chkWF.Items[3].Attributes.Add("style", "display:block");
                     chkWF.Items[4].Attributes.Add("style", "display:none");
                    chkWF.Items[5].Attributes.Add("style", "display:none");
                    chkWF.Items[6].Attributes.Add("style", "display:none");//appraisal
                    rdbGrouping.Enabled = false;
                    chkAdClaims.Enabled = false;
                }


                //Changes for Work Flow...
                if (Compset.Tables[0].Rows[0]["WFEMP"] == System.DBNull.Value)
                {
                    chkWF.Items[0].Selected = false;
                }
                else
                {
                    if (Compset.Tables[0].Rows[0]["WFEMP"].ToString() == "1")
                        chkWF.Items[0].Selected = true;
                }

                //Changes for Work Flow...
                if (Compset.Tables[0].Rows[0]["WFLEAVE"] == System.DBNull.Value)
                {
                    chkWF.Items[1].Selected = false;
                }
                else
                {
                    if (Compset.Tables[0].Rows[0]["WFLEAVE"].ToString() == "1")
                        chkWF.Items[1].Selected = true;
                }


                //Changes for Work Flow...
                if (Compset.Tables[0].Rows[0]["WFCLAIM"] == System.DBNull.Value)
                {
                    chkWF.Items[2].Selected = false;
                }
                else
                {
                    if (Compset.Tables[0].Rows[0]["WFCLAIM"].ToString() == "1")
                        chkWF.Items[2].Selected = true;
                }

                if (Compset.Tables[0].Rows[0]["WFPAY"] == System.DBNull.Value)
                {
                    chkWF.Items[3].Selected = false;
                }
                else
                {
                    if (Compset.Tables[0].Rows[0]["WFPAY"].ToString() == "1")
                        chkWF.Items[3].Selected = true;
                }


                if (Compset.Tables[0].Rows[0]["WFReport"] == System.DBNull.Value)
                {
                    chkWF.Items[4].Selected = false;
                }
                else
                {
                    if (Compset.Tables[0].Rows[0]["WFReport"].ToString() == "1")
                        chkWF.Items[4].Selected = true;
                }

                if (Compset.Tables[0].Rows[0]["WFTimeSheet"] == System.DBNull.Value)
                {
                    chkWF.Items[5].Selected = false;
                }
                else
                {
                    if (Compset.Tables[0].Rows[0]["WFTimeSheet"].ToString() == "1")
                        chkWF.Items[5].Selected = true;
                }
                if (Compset.Tables[0].Rows[0]["WFAppraisal"] == System.DBNull.Value)
                {
                    chkWF.Items[6].Selected = false;
                }
                else
                {
                    if (Compset.Tables[0].Rows[0]["WFAppraisal"].ToString() == "1")
                        chkWF.Items[6].Selected = true;
                }

                this.rostertype.SelectedValue = Compset.Tables[0].Rows[0]["RosterType"].ToString();
                this.overtimemode.SelectedValue = Compset.Tables[0].Rows[0]["WeekRosterOtMode"].ToString();

                //

                //murugan

                radListTSApp.SelectedValue = Compset.Tables[0].Rows[0]["TSrequired"].ToString();
                string s1= Compset.Tables[0].Rows[0]["APPrequired"].ToString();

                if (s1 == "True")
                    radListALAp.SelectedValue = "1";
                else

                    radListALAp.SelectedValue = "0";


                //Changes for Approval Process
                if (Compset.Tables[0].Rows[0]["AppTSProcess"] == System.DBNull.Value || Compset.Tables[0].Rows[0]["AppTSProcess"].ToString() == "1")
                {
                    //chkWF.Items[5].Selected = false;
                    radListPayrollApp.SelectedIndex = 0;
                }
                else
                {
                    if (Compset.Tables[0].Rows[0]["AppTSProcess"].ToString() == "0")
                        radListPayrollApp.SelectedIndex = 1;
                }

                if (Compset.Tables[0].Rows[0]["AppLeaveProcess"] == System.DBNull.Value || Compset.Tables[0].Rows[0]["AppLeaveProcess"].ToString() == "1")
                {
                    //chkWF.Items[5].Selected = false;
                    radListLeaveApp.SelectedIndex = 0;

                }
                else
                {
                    if (Compset.Tables[0].Rows[0]["AppLeaveProcess"].ToString() == "0")
                    {
                        radListLeaveApp.SelectedIndex = 1;

                    }
                }
                if (Compset.Tables[0].Rows[0]["GroupManage"] == System.DBNull.Value || Compset.Tables[0].Rows[0]["GroupManage"].ToString() == "0")
                {
                    //chkWF.Items[5].Selected = false;
                    rdbGrouping.SelectedValue = "0";
                    rdWorkFlow.Items[1].Enabled = true;
                    rdWorkFlow.Items[2].Enabled = true;
                }
                else
                {
                    if (Compset.Tables[0].Rows[0]["GroupManage"].ToString() == "1")
                    {
                        rdbGrouping.SelectedValue = "1";
                        rdWorkFlow.Items[1].Enabled = false;
                        rdWorkFlow.Items[2].Enabled = false;
                    }
                }

                if (Compset.Tables[0].Rows[0]["AppClaimsProcess"] == System.DBNull.Value || Compset.Tables[0].Rows[0]["AppClaimsProcess"].ToString() == "1")
                {
                    //chkWF.Items[5].Selected = false;
                    radListClaimApp.SelectedIndex = 0;

                }
                else
                {
                    if (Compset.Tables[0].Rows[0]["AppClaimsProcess"].ToString() == "0")
                    {
                        radListClaimApp.SelectedIndex = 1;

                    }
                }


                txtCompCode.Text = Utility.ToString(Compset.Tables[0].Rows[0]["company_code"]);
                txtCompName.Text = Utility.ToString(Compset.Tables[0].Rows[0]["company_name"]);
                txtCompemail.Value = Utility.ToString(Compset.Tables[0].Rows[0]["email"]);
                txtCompfax.Value = Utility.ToString(Compset.Tables[0].Rows[0]["Fax"]);
                txtCompcity.Value = Utility.ToString(Compset.Tables[0].Rows[0]["city"]);
                txtCompperson.Value = Utility.ToString(Compset.Tables[0].Rows[0]["auth_person"]);
                txtCompPhone.Value = Utility.ToString(Compset.Tables[0].Rows[0]["phone"]);
                txtauth_emai.Value = Utility.ToString(Compset.Tables[0].Rows[0]["Auth_email"]);
                txtcompaddress.Text = Utility.ToString(Compset.Tables[0].Rows[0]["Address"]);
                txtCompstate.Value = Utility.ToString(Compset.Tables[0].Rows[0]["state"]);
                //CPF Changes

                ////Get the changes for New CPF Table 
                string month, year;
                month = DateTime.Now.Month.ToString();
                year = DateTime.Now.Year.ToString();

                string date = "01/" + month + "/" + year;
                string sqlcpf = "Select monthly_cpf_celi ,annual_cpf_ceil from Company_CPF_CEILING where convert(datetime,'" + date + "',103) between convert(datetime,EffectiveDateFrom,103) and convert(datetime,EffectiveDateTo,103)";

                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sqlcpf, null);
                if (dr1.Read())
                {
                    cpfceil = dr1[0].ToString();
                    annualceil = dr1[1].ToString();
                }
                txtmonthly_cpf_ceil.Value = Utility.ToString(cpfceil);

                //txtmonthly_cpf_ceil.Value = Utility.ToString(Compset.Tables[0].Rows[0]["monthly_cpf_ceil"]);
                txtwebsite.Value = Utility.ToString(Compset.Tables[0].Rows[0]["website"]);
                //txtannual_cpf_ceil.Value = Utility.ToString(Compset.Tables[0].Rows[0]["annual_cpf_ceil"]);

                txtannual_cpf_ceil.Value = Utility.ToString(annualceil);

                cmbCountry.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["country"]);
                txtdesign.Value = Utility.ToString(Compset.Tables[0].Rows[0]["designation"]);
                txtcompany_roc.Text = Utility.ToString(Compset.Tables[0].Rows[0]["company_roc"]);
                drpcompany_type.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["company_type"]);
                cmbpayslipformat.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["payslip_format"]);
                rdtimesheet.Value = Utility.ToString(Compset.Tables[0].Rows[0]["timesheet_approve"]);
                RdApproval.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["Payroll_Approval"]);
                cmbworkingdays.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["no_work_days"]);
                cmbworkingdays1.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["no_work_days"]); //Added by Sandi on 26/03/2014
                txthrs_day.Value = Utility.ToString(Compset.Tables[0].Rows[0]["day_hours"]);
                txtmin_day.Value = Utility.ToString(Compset.Tables[0].Rows[0]["day_minute"]);

                txtemailsender_address.Value = Utility.ToString(Compset.Tables[0].Rows[0]["email_sender"]);
                txtemailuser.Value = Utility.ToString(Compset.Tables[0].Rows[0]["email_username"]);

                //txtemail_replyaddress.Value = Utility.ToString(Compset.Tables[0].Rows[0]["email_reply_address"]);
                if (Utility.ToString(Compset.Tables[0].Rows[0]["email_reply_address"]) != "")
                {
                    Editortxtemail_replyaddress.Content = Utility.ToString(Compset.Tables[0].Rows[0]["email_reply_address"]);
                }
                else
                {
                    Editortxtemail_replyaddress.Content = "Greetings, @approver has @status your applied leaves from @from_date to @to_date <br />REMARKS: @reason;Thanks and Regards Advanced & Best Technologies Pte Ltd Office: 6837 2336 | 6223 7996 Fax: 6220 4532 www.anbgroup.com";
                }
                //txtemail_replyname.Value = Utility.ToString(Compset.Tables[0].Rows[0]["email_reply_name"]);
                if (Utility.ToString(Compset.Tables[0].Rows[0]["email_reply_name"]) != "")
                {
                    Editortxtemail_replyname.Content = Utility.ToString(Compset.Tables[0].Rows[0]["email_reply_name"]);
                }
                else
                {
                    Editortxtemail_replyname.Content = "Greetings, Payroll for the period  @month / @year has been submitted  by @hr for your appropal.Please review the payroll and update the status.Thanks and Regards Advanced & Best Technologies Pte Ltd Office: 6837 2336 | 6223 7996 Fax: 6220 4532 www.anbgroup.com";
                }

                // txtemail_leavedel.Value = Utility.ToString(Compset.Tables[0].Rows[0]["email_leave_delete"]);
                if (Utility.ToString(Compset.Tables[0].Rows[0]["email_leave_delete"]) != "")
                {
                    Editortxtemail_leavedel.Content = Utility.ToString(Compset.Tables[0].Rows[0]["email_leave_delete"]);
                }
                else
                {
                    Editortxtemail_leavedel.Content = "Greetings,  Leave Applied Deleted of: @emp_name. Type of Leave Applied:@leave_type. Period of Leave Application: @from_date to @to_date. Paid leave:@paid_leaves, Unpaid leave:@unpaid_leaves.Status: @status.Thanks and Regards Advanced & Best Technologies Pte Ltd Office: 6837 2336 | 6223 7996 Fax: 6220 4532 www.anbgroup.com";

                }

                emailpwd.Attributes.Add("value", Utility.ToString(Compset.Tables[0].Rows[0]["email_password"]));
                txtemail_sendername.Value = Utility.ToString(Compset.Tables[0].Rows[0]["email_sender_name"]);

                //r
                if (Utility.ToString(Compset.Tables[0].Rows[0]["email_sender_name"]) != "")
                {
                    EditorLevReq.Content = Utility.ToString(Compset.Tables[0].Rows[0]["email_sender_name"]);
                }
                else
                {
                    EditorLevReq.Content = "Greetings,"
                                           + " Leave application submitted by: @emp_name."
                                            + "Type of leave applied:@leave_type."
                                            + "Leave balance as of today: @leave_balance."
                                            + "Period of leave application: @from_date to @to_date."
                                           + " Paid leave:@paid_leaves, Unpaid leave:@unpaid_leaves."
                                           + " AM or PM (applicable only for 0.5 day leave): @timesession"

                                           + " Thanks and Regards"
                                           + " Advanced & Best Technologies Pte Ltd"
                                           + " Office: 6837 2336 | 6223 7996 Fax: 6220 4532"
                                           + " www.anbgroup.com";
                }

                txtemailsender_domain.Value = Utility.ToString(Compset.Tables[0].Rows[0]["email_sender_domain"]);
                txtsmtpserver.Value = Utility.ToString(Compset.Tables[0].Rows[0]["email_SMTP_server"]);
                txtsmtpport.Value = Utility.ToString(Compset.Tables[0].Rows[0]["email_SMTP_port"]);

                txtpostalcode.Text = Utility.ToString(Compset.Tables[0].Rows[0]["postal_code"]);
                txtcompaddress2.Text = Utility.ToString(Compset.Tables[0].Rows[0]["address2"]);

                cmbemailleave.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["email_leavealert"]);

                cmbemailpay.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["email_payalert"]);
                if (radListPayrollApp.SelectedValue == "1")
                {
                    cmbemailpay.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["email_payalert"]);
                }
                else
                {
                    cmbemailpay.SelectedValue = "No";
                }
                //cmbEmailPaySlip.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["epayslip"]);

                cmbEmailPaySlip.Checked = Utility.ToString(Compset.Tables[0].Rows[0]["epayslip"]) == "Y" ? true : false;

                cmbLeaveModel.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["leave_model"]);
                // txtclaim_sendername.Value = Utility.ToString(Compset.Tables[0].Rows[0]["email_claim_sender_name"]);

                if (Utility.ToString(Compset.Tables[0].Rows[0]["email_claim_sender_name"]) != "")
                {
                    Editortxtclaim_sendername.Content = Utility.ToString(Compset.Tables[0].Rows[0]["email_claim_sender_name"]);
                }
                else
                {
                    Editortxtclaim_sendername.Content = "Greetings,@emp_name has requested claim for the month of  @month @year; Thanks and Regards Advanced & Best Technologies Pte Ltd Office: 6837 2336 | 6223 7996 Fax: 6220 4532 www.anbgroup.com";
                }
                //txtemailclaim_replyname.Value = Utility.ToString(Compset.Tables[0].Rows[0]["email_claim_reply_name"]);

                if (Utility.ToString(Compset.Tables[0].Rows[0]["email_claim_reply_name"]) != "")
                {
                    Editortxtemailclaim_replyname.Content = Utility.ToString(Compset.Tables[0].Rows[0]["email_claim_reply_name"]);
                }
                else
                {
                    Editortxtemailclaim_replyname.Content = "Greetings, @approver has @status your applied claim for the month of @month @year;Thanks and Regards Advanced & Best Technologies Pte Ltd Office: 6837 2336 | 6223 7996 Fax: 6220 4532 www.anbgroup.com";
                }

                cmbclaim.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["email_claimalert"]);
                ddlssl.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["sslrequired"]);
                cmbEPayPwd.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["pwdrequired"]);
                txtccmail.Value = Utility.ToString(Compset.Tables[0].Rows[0]["ccmail"]);
                cmbccclaim.Value = Utility.ToString(Compset.Tables[0].Rows[0]["ccalert_claims"]);
                cmbccleave.Value = Utility.ToString(Compset.Tables[0].Rows[0]["ccalert_leaves"]);
                cmbLeaveRoundoff.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["basicrnd"]);
                cmbAdditionsRoundoff.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["additionsrnd"]);
                cmbDeductionsRoundoff.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["deductionsrnd"]);
                cmbNetPayRoundoff.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["netpayrnd"]);
                cmbPayrollType.SelectedValue = Convert.ToString(Compset.Tables[0].Rows[0]["payrolltype"].ToString());
                cmbAssignType.SelectedValue = Convert.ToString(Compset.Tables[0].Rows[0]["projectassign"].ToString());

                txtSalaryGL.Value = Utility.ToString(Compset.Tables[0].Rows[0]["SalaryGLCode"]);
                txtEmpCPFGL.Value = Utility.ToString(Compset.Tables[0].Rows[0]["EmployeeCPFGLCode"]);
                txtEmpyCPFGL.Value = Utility.ToString(Compset.Tables[0].Rows[0]["EmployerCPFGLCode"]);
                txtFundGL.Value = Utility.ToString(Compset.Tables[0].Rows[0]["FundAmtGLCode"]);
                txtSDLGL.Value = Utility.ToString(Compset.Tables[0].Rows[0]["SDLAmtGLCode"]);
                txtacccompGL.Value = Utility.ToString(Compset.Tables[0].Rows[0]["AccountGLCode"]);
                txtunpaidGL.Value = Utility.ToString(Compset.Tables[0].Rows[0]["UnpaidLeaGLCode"]);

                //DropDownbox
                cmbPublicHoliday.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["TsPublicH"]);
                cmbSunday.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["Sunday"]);
                cmbRosterNa.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["NoRoster"]);

                cmbIsMaster.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["isMaster"]);
                cmbtempEmp.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["isMasterEmpTemp"]);

                loginWithOutComany.SelectedValue = Utility.ToString_ku(Compset.Tables[0].Rows[0]["loginWithOutComany"]);

                if (Utility.ToString(Compset.Tables[0].Rows[0]["FIFO"]) == "1")
                {
                    chkFiFo.SelectedValue = "FIFO";
                }
                cmbRound.SelectedValue = Utility.ToString(Compset.Tables[0].Rows[0]["Rounding"]);

                //TsPublicH,Sunday,NoRoster,FIFO,Rounding

                if (Compset.Tables[0].Rows[0]["isTSRemarks"] != null)
                {
                    if ((bool)Compset.Tables[0].Rows[0]["isTSRemarks"] == true)
                    {
                        rdtsremarks.Value = "1";
                    }
                    else
                    {
                        rdtsremarks.Value = "0";
                    }
                }
                //--murugan

                if (Compset.Tables[0].Rows[0]["isApproveDate"].ToString() == "1")
                {
                    chkApprovalDate.Checked = true;

                }
                else
                {
                    chkApprovalDate.Checked = false;
                }
                txtcctimesheet.Value = Compset.Tables[0].Rows[0]["ccTimeSheet"].ToString();

                if (Compset.Tables[0].Rows[0]["OTseparate"].ToString() == "True")
                {
                    lblOTGL.Visible = true;
                    txtOTGL.Visible = true;
                    chk_LGOT.Checked = true;
                }
                else
                {
                    lblOTGL.Visible = false;
                    txtOTGL.Visible = false;
                    chk_LGOT.Checked = false;
                }
               
                txtOTGL.Value = Compset.Tables[0].Rows[0]["OTGLCode"].ToString();
               

                txtSDLpayable.Value = Compset.Tables[0].Rows[0]["SDLPayableGL"].ToString();
                txtSalarypayable.Value = Compset.Tables[0].Rows[0]["SalaryPayableGL"].ToString();
                txtCPFpayable.Value = Compset.Tables[0].Rows[0]["CPFPayableGL"].ToString();

                //Get Data For Payslip format etc

                LoadPayslipFormateData();







                #region Load Alias Name on Page Load
                string SQLcompAliasName = " select AliasName from Company_Alias  where Company_id='" + compid + "'";
                SqlDataReader dr_alias = DataAccess.ExecuteReader(CommandType.Text, SQLcompAliasName, null);
                while (dr_alias.Read())
                {
                    companyAliasName = Utility.ToString(dr_alias.GetValue(0));
                }
                if (companyAliasName != "" && companyAliasName != null)
                {
                    txtAlias.Text = companyAliasName.ToString();
                }
                else
                {
                    txtAlias.Text = "";
                }
                #endregion




            }
            else
            {
                s = 0;
                //CPF Changes
                txtmonthly_cpf_ceil.Value = cpfceil;
                txtannual_cpf_ceil.Value = annualceil;
                cmbLeaveRoundoff.SelectedValue = basicroundoffdefault;
                cmbAdditionsRoundoff.SelectedValue = roundoffdefault;
                cmbDeductionsRoundoff.SelectedValue = roundoffdefault;
                cmbNetPayRoundoff.SelectedValue = roundoffdefault;
                rdMultiCurr.SelectedIndex = 0;
            }


        }
        public void radListPayrollApp_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (radListPayrollApp.SelectedValue == "1")
            {
                cmbemailpay.Enabled = true;
                cmbemailpay.SelectedValue = "Yes";
            }
            else
            {
                cmbemailpay.SelectedValue = "No";
                cmbemailpay.Enabled = false;

            }

        }

        void rdMultiCurr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdMultiCurr.SelectedValue.ToString() == "0")
            {
                tblMultiCurrency.Enabled = false;
                tbsComp.Tabs[10].Enabled = false;
            }
            if (rdMultiCurr.SelectedValue.ToString() == "1")
            {
                tblMultiCurrency.Enabled = true;
                tbsComp.Tabs[10].Enabled = true;
            }
        }

        void cmbpayslipformat_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            LoadPayslipFormateData();


            if (cmbpayslipformat.SelectedValue == "7" || cmbpayslipformat.SelectedValue == "10")
            {
                this.Button5.Visible = false;
            }
            else
            {

                this.Button5.Visible = true;
            }

            if (cmbpayslipformat.SelectedValue == "7")
            {
                trPaySlipSetup1.Visible = true;
                tblPaySlipSetup1.Visible = true;
                tblMOMItemized.Visible = false;
                trPayslipSetup2.Visible = false;
                tblPayslipSetup2.Visible = false;
            }
            else if (cmbpayslipformat.SelectedValue == "10")
            {
                trPaySlipSetup1.Visible = false;
                trPayslipSetup2.Visible = true;
                tblPayslipSetup2.Visible = true;
                tblPaySlipSetup1.Visible = false;
                tblMOMItemized.Visible = false;



                lblCustomizePayYear.Visible = false;
                radCustomizePayYear.Visible = false;
                txtCustomizePayYear.Visible = false;
                lblCustomizeSalary.Visible = false;
                radCustomizeSalaryForMonth.Visible = false;
                txtCustomizeSalary.Visible = false;

            }
            else if (cmbpayslipformat.SelectedValue == "12")
            {
                trPaySlipSetup1.Visible = false;
                tblMOMItemized.Visible = true;
                tblPaySlipSetup1.Visible = false;
                trPayslipSetup2.Visible = false;
                tblPayslipSetup2.Visible = false;
            }
            else
            {
                trPaySlipSetup1.Visible = false;
                tblPaySlipSetup1.Visible = false;
                trPayslipSetup2.Visible = false;
                tblPayslipSetup2.Visible = false;
                tblMOMItemized.Visible = false;
            }
        }
        void radCustomizeLogo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (radItemizeLogo.SelectedValue == "1")
            {
                radItemizeLogoManagement.Enabled = true;

            }
            else if (radItemizeLogo.SelectedValue == "2")
            {
                radItemizeLogoManagement.Enabled = false;

            }

        }
        void radCustomizeLeave_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //if (radItemizeLeave.SelectedValue == "1")
            //{
            //    radItemizeLEAVEDETAILS.Enabled = true;

            //}
            //else if (radItemizeLeave.SelectedValue == "2")
            //{
            //    radItemizeLEAVEDETAILS.Enabled = false;

            //}

        }
        void rdWorkFlow_SelectedIndexChanged(object sender, EventArgs e)
        {
            //--------murugan
            if (rdWorkFlow.SelectedIndex == 0)
            {
                chkWF.Items[0].Enabled = false;
                chkWF.Items[1].Enabled = false;
                chkWF.Items[2].Enabled = false;
                chkWF.Items[3].Enabled = false;
                chkWF.Items[4].Enabled = false;
                chkWF.Items[5].Enabled = false;

                chkWF.Items[0].Selected = false;
                chkWF.Items[1].Selected = false;
                chkWF.Items[2].Selected = false;
                chkWF.Items[3].Selected = false;
                chkWF.Items[4].Selected = false;
                chkWF.Items[5].Selected = false;
                chkWF.Items[0].Attributes.Add("style", "display:none");
                chkWF.Items[1].Attributes.Add("style", "display:block");
                chkWF.Items[2].Attributes.Add("style", "display:block");
                chkWF.Items[3].Attributes.Add("style", "display:block");
                chkWF.Items[4].Attributes.Add("style", "display:none");
                chkWF.Items[5].Attributes.Add("style", "display:block");
                chkWF.Items[6].Attributes.Add("style", "display:block");
                rdbGrouping.Enabled = true;
                chkAdClaims.Enabled = true;
            }
            else if (rdWorkFlow.SelectedIndex == 1)
            {
                chkWF.Items[0].Attributes.Add("style", "display:none");
                chkWF.Items[1].Enabled = true;
                chkWF.Items[1].Selected = true;
                chkWF.Items[1].Attributes.Add("style", "display:block");
                chkWF.Items[2].Enabled = true;
                chkWF.Items[2].Attributes.Add("style", "display:block");
                chkWF.Items[3].Attributes.Add("style", "display:none");
                chkWF.Items[4].Attributes.Add("style", "display:none");
                chkWF.Items[5].Enabled = true;
                chkWF.Items[5].Attributes.Add("style", "display:block");
                chkWF.Items[6].Enabled = true;
                chkWF.Items[6].Attributes.Add("style", "display:block");

                //chkWF.Items[0].Enabled = false;
                //chkWF.Items[1].Enabled = false;
                //chkWF.Items[2].Enabled = false;
                //chkWF.Items[3].Enabled = false;
                //chkWF.Items[4].Enabled = false;
                //chkWF.Items[5].Enabled = false;
                rdbGrouping.Enabled = false;
                //  chkAdClaims.Enabled = false;
            }
            else
            {
                chkWF.Items[0].Attributes.Add("style", "display:none");
                chkWF.Items[1].Attributes.Add("style", "display:none");
                chkWF.Items[2].Attributes.Add("style", "display:none");
                chkWF.Items[3].Enabled = true;
                chkWF.Items[3].Selected = true;
                chkWF.Items[3].Attributes.Add("style", "display:block");
                chkWF.Items[4].Attributes.Add("style", "display:none");
                chkWF.Items[5].Attributes.Add("style", "display:none");
                chkWF.Items[6].Attributes.Add("style", "display:none");
                rdbGrouping.Enabled = false;
                //    chkAdClaims.Enabled = false;
                //chkWF.Items[0].Enabled = false;
                //chkWF.Items[1].Enabled = false;
                //chkWF.Items[2].Enabled = false;
                //chkWF.Items[3].Enabled = true;
                //chkWF.Items[4].Enabled = false;
                //chkWF.Items[5].Enabled = false;


            }
        }
        void rdbGrouping_SelectedIndexChanged(object sender, EventArgs e)
        {
            //--------murugan
            if (rdbGrouping.SelectedIndex == 0)
            {
                rdWorkFlow.Items[1].Enabled = false;
                rdWorkFlow.Items[2].Enabled = false;
                chkWF.Items[0].Attributes.Add("style", "display:none");
                chkWF.Items[1].Attributes.Add("style", "display:block");
                chkWF.Items[2].Attributes.Add("style", "display:none");
                chkWF.Items[3].Attributes.Add("style", "display:block");
                chkWF.Items[4].Attributes.Add("style", "display:none");
                chkWF.Items[5].Attributes.Add("style", "display:none");
            }
            else if (rdbGrouping.SelectedIndex == 1)
            {
                rdWorkFlow.Items[1].Enabled = true;
                rdWorkFlow.Items[2].Enabled = true;
                chkWF.Items[0].Attributes.Add("style", "display:none");
                chkWF.Items[1].Attributes.Add("style", "display:block");
                chkWF.Items[2].Attributes.Add("style", "display:none");
                chkWF.Items[3].Attributes.Add("style", "display:block");
                chkWF.Items[4].Attributes.Add("style", "display:none");
                chkWF.Items[5].Attributes.Add("style", "display:none");

            }

        }
        void btnLeaveFF_Click(object sender, EventArgs e)
        {

            string msg = "";
            try
            {
                //Get First Leaves (Last Year Leaves )
                string strLeaves = "select LY_Leaves_Bal,Emp_ID from employeeleavesallowed where Leave_Type=8 AND Leave_year=" + DateTime.Now.Year;

                SqlDataReader dr;
                dr = DataAccess.ExecuteReader(CommandType.Text, strLeaves, null);

                string empcode = "";

                while (dr.Read())
                {
                    int emp_code = 0;
                    double lastYearLeaves = 0;
                    emp_code = Convert.ToInt32(dr["Emp_ID"].ToString());
                    lastYearLeaves = Convert.ToDouble(dr["LY_Leaves_Bal"].ToString());
                    //Call SP To get The Details exec sp_GetEmployeeLeavePolicy @empid=321,@year=2012,@applydateon='Apr  4 2012 12:00:00:000AM',@filter=-1

                    DataSet ds_leave = new DataSet();
                    //string sSQL = "SELECT [id], [type] FROM [leave_types] WHERE id IN (select leave_type from EmployeeLeavesAllowed where ";
                    //sSQL += " emp_ID = {0} And leave_year = {1})";
                    //sSQL = string.Format(sSQL, Utility.ToInteger(drpname.SelectedValue), Utility.ToInteger(cmbLeaveYear.SelectedValue));
                    //ds_leave = getDataSet(sSQL);
                    string sSQL = "sp_GetEmployeeLeavePolicy";
                    SqlParameter[] parms = new SqlParameter[4];
                    parms[0] = new SqlParameter("@empid", Utility.ToInteger(emp_code));
                    parms[1] = new SqlParameter("@year", Utility.ToString(DateTime.Now.Year));
                    parms[2] = new SqlParameter("@applydateon", Convert.ToDateTime(DateTime.Now));
                    parms[3] = new SqlParameter("@filter", -1);
                    ds_leave = DataAccess.ExecuteSPDataSet(sSQL, parms);

                    //Get Employess Which are already Transfered in next year leaves table "leaves_forefited_New"
                    string strEmpFor = "select emp_code from leaves_forefited_New Where Year=" + Utility.ToInteger(DateTime.Now.Year) + " AND Company_Id=" + Convert.ToInt32(compid);
                    SqlDataReader dr11;

                    dr11 = DataAccess.ExecuteReader(CommandType.Text, strEmpFor, null);


                    bool exists = false;
                    while (dr11.Read())
                    {
                        if (dr11[0].ToString() == emp_code.ToString())
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (exists == false)
                    {
                        //Leaves Taken Annually
                        double leaves_anaualTaken = 0;
                        if (ds_leave != null)
                        {
                            if (ds_leave.Tables.Count > 0)
                            {
                                if (ds_leave.Tables[0].Rows[0]["TYPE"].ToString() == "Annual Leave")
                                {
                                    if (ds_leave.Tables[0].Rows[0]["totalleavestaken"] != null)
                                    {
                                        leaves_anaualTaken = Convert.ToDouble(ds_leave.Tables[0].Rows[0]["totalleavestaken"].ToString());
                                    }
                                }

                                if (leaves_anaualTaken < lastYearLeaves)
                                {
                                    double leavesUpdate = lastYearLeaves - leaves_anaualTaken;
                                    double leaveforbit = 0.00;
                                    leaveforbit = lastYearLeaves - leavesUpdate;
                                    //   double leavesUpdate = 0.00;
                                    //Make Leaves Zero for all employess in this company...
                                    string sqlUpdate = "Update employeeleavesallowed SET LY_Leaves_Bal =" + leaveforbit + "  FROM employeeleavesallowed ";
                                    sqlUpdate = sqlUpdate + " WHERE  LEave_Type = 8  and leave_year=" + DateTime.Now.Year + "  and Emp_Id=" + emp_code;
                                    int cnt = DataAccess.ExecuteNonQuery(sqlUpdate, null);
                                    //Insert into new Table
                                    if (cnt > 0)
                                    {
                                        //LeaveType
                                        //Year
                                        //Company_Id
                                        //lastYearLeaves
                                        //leaves_anaualTaken
                                        //emp_code
                                        string insert = "INSERT INTO leaves_forefited_New(LeaveType,Year,Company_Id,lastYearLeaves,leaves_anaualTaken,emp_code) VALUES (8," + DateTime.Now.Year + "," + Convert.ToInt32(Request.QueryString["compid"].ToString()) + "," + lastYearLeaves + "," + leaves_anaualTaken + "," + emp_code + ")";
                                        int cnt1 = DataAccess.ExecuteNonQuery(insert, null);
                                        if (empcode == "")
                                        {
                                            empcode = emp_code.ToString();
                                        }
                                        else
                                        {
                                            empcode = empcode + "," + emp_code.ToString();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


                if (empcode.Length > 0)
                {
                    msg = "Leaves forfeited  for EmpCode " + empcode + " Employees";
                }
                else
                {
                    msg = "Leaves Already forfeited ";
                }
                ShowMessageBox(msg);
            }
            catch (Exception ex)
            {
                ShowMessageBox(msg + "," + ex.Message.ToString());
            }
        }



        private DataSet CompanyDetails
        {
            get
            {
                string sSQL = "";
                DataSet ds = new DataSet();
                if (compid == null)
                {
                    sSQL = "SELECT  b.[desc], b.id,b.[code], b.[bank_code] ,[bank_branch], a.[id] giroid,[value_date],[giro_acc_name], [bank_accountno], a.company_bankcode, a.[company_id],a.[approvercode],a.[operatorcode],[CurrencyID] FROM [Girobanks] a, bank b WHERE a.bank_id=b.id  and a.Temp ='" + Session.SessionID + "'";
                }
                else
                {
                    sSQL = "SELECT  b.[desc], b.id,b.[code], b.[bank_code], [bank_branch], a.[id] giroid,[value_date],[giro_acc_name], [bank_accountno], a.company_bankcode, a.[company_id], a.[approvercode],a.[operatorcode],[CurrencyID] FROM [Girobanks] a, bank b WHERE a.bank_id=b.id and a.[company_id] =" + compid;
                }
                ds = ds = DataAccess.FetchRS(CommandType.Text, sSQL, null); ;
                return ds;
            }
        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.RadGrid1.DataSource = this.CompanyDetails;
        }

        protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["giroid"];
            string giroid = id.ToString();
            string bankbranch = (userControl.FindControl("txtbranch") as TextBox).Text;
            string bankaccno = (userControl.FindControl("txtbankaccno") as TextBox).Text;
            string bankaccname = (userControl.FindControl("txtgiroaccountname") as TextBox).Text;
            string compbankcode = (userControl.FindControl("compbankcode") as TextBox).Text;
            string valuedate = (userControl.FindControl("txtvaluedate") as TextBox).Text;
            string approvercode = (userControl.FindControl("txtapprover") as TextBox).Text;
            string operatorcode = (userControl.FindControl("txtoperator") as TextBox).Text;
            string currencyID = (userControl.FindControl("drpCurrency") as DropDownList).SelectedItem.Value;

            string sSQL = "update girobanks set bank_branch='" + bankbranch + "',value_date='" + valuedate + "',company_bankcode='" + compbankcode + "',bank_accountno='" + bankaccno + "',giro_acc_name='" + bankaccname + "', approvercode='" + approvercode + "',operatorcode='" + operatorcode + "',CurrencyID= " + currencyID + " where company_id=" + compid + " and  id=" + giroid;

            int retVal = DataAccess.ExecuteStoreProc(sSQL);
            if (retVal == 1)
            {
               // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Information updated successfully"));
                _actionMessage = "Success|Information updated successfully";
                ViewState["actionMessage"] = _actionMessage;
            }

            else
            {
               // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to update the record"));
                _actionMessage = "Warning|Unable to update the record";
                ViewState["actionMessage"] = _actionMessage;
            }


        }

        protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

            string bankname = (userControl.FindControl("drpbankname") as DropDownList).SelectedItem.Value;
            string bankbranch = (userControl.FindControl("txtbranch") as TextBox).Text;
            string bankaccno = (userControl.FindControl("txtbankaccno") as TextBox).Text;
            string bankaccname = (userControl.FindControl("txtgiroaccountname") as TextBox).Text;
            string compbankcode = (userControl.FindControl("compbankcode") as TextBox).Text;
            string valuedate = (userControl.FindControl("txtvaluedate") as TextBox).Text;
            string approvercode = (userControl.FindControl("txtapprover") as TextBox).Text;
            string operatorcode = (userControl.FindControl("txtoperator") as TextBox).Text;
            string currencyID = (userControl.FindControl("drpCurrency") as DropDownList).SelectedItem.Value;

            string sSQL = "";
            sSQL = "Insert into girobanks (bank_id, bank_branch, bank_accountno,giro_acc_name,company_id,value_date,company_bankcode, approvercode, operatorcode,CurrencyID)";
            sSQL = sSQL + " Values(" + bankname + ", '" + bankbranch + "', '" + bankaccno + "','" + bankaccname + "', " + compid + ",'" + valuedate + "','" + compbankcode + "','" + approvercode + "','" + operatorcode + "'," + currencyID + ")";
            try
            {
                int retVal = DataAccess.ExecuteStoreProc(sSQL);
                _actionMessage = "Success| Information added successfully";
                ViewState["actionMessage"] = _actionMessage;
            }
            catch (Exception ex)
            {
                string ErrMsg = "Some Error Occured. Please try again later.";
                if (ex.Message.IndexOf("PRIMARY KEY constraint", 1) > 0)
                {
                    ErrMsg = "Unable to add the record.Please try again.";
                   // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to add record. Reason:</font> " + ErrMsg));
                  
                    e.Canceled = true;
                }
                     _actionMessage = "Warning|Unable to add record. Reason: " + ErrMsg;
                    ViewState["actionMessage"] = _actionMessage;
            }
            RadGrid1.Rebind();
        }



        protected void dropdown_binding()
        {
            DataSet ds_country = new DataSet();
            string SQL = "select Id Country_ID , Country from country order by 1";
            ds_country = DataAccess.FetchRS(CommandType.Text, SQL, null);
            cmbCountry.DataSource = ds_country.Tables[0];
            cmbCountry.DataTextField = ds_country.Tables[0].Columns["Country"].ColumnName.ToString();
            cmbCountry.DataValueField = ds_country.Tables[0].Columns["Country_ID"].ColumnName.ToString();


            cmbCountry.DataBind();

        }



        protected void RadGrid2_PreRender(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
            }
        }

        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }

        private DataSet UserDetails
        {
            get
            {
                string sSQL = "";
                DataSet ds = new DataSet();
                if (Convert.ToString(Session["GroupName"]) == "Super Admin") //Senthil Added-08/26/2015
                {
                    if (compid == null)
                    {

                        sSQL = "select a.emp_code,isnull(a.emp_name,'')+' '+isnull(a.emp_lname,'')  emp_name, a.UserName, a.Password,b.GroupID,b.GroupName,c.Status,c.StatusId, a.Email from employee a Inner Join UserGroups b on a.groupid  = b.groupid And a.company_id = b.company_id Inner Join UserStatus c on a.statusid = c.statusid Where a.Company_ID=0 order by emp_name";
                    }
                    else
                    {

                        sSQL = "select a.emp_code,isnull(a.emp_name,'')+' '+isnull(a.emp_lname,'')  emp_name, a.UserName, a.Password,b.GroupID,b.GroupName,c.Status,c.StatusId, a.Email from employee a Inner Join UserGroups b on a.groupid  = b.groupid And a.company_id = b.company_id Inner Join UserStatus c on a.statusid = c.statusid Where a.Company_ID=" + compid + " order by emp_name";
                    }
                }
                else
                {

                    if (compid == null)
                    {

                        sSQL = "select a.emp_code,isnull(a.emp_name,'')+' '+isnull(a.emp_lname,'')  emp_name, a.UserName, a.Password,b.GroupID,b.GroupName,c.Status,c.StatusId, a.Email from employee a Inner Join UserGroups b on a.groupid  = b.groupid And a.company_id = b.company_id Inner Join UserStatus c on a.statusid = c.statusid Where a.Company_ID=0 and a.emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR a.emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") order by emp_name";
                    }
                    else
                    {
                        if (Utility.GetGroupStatus(Convert.ToInt32(compid)) == 1)
                        {


                            sSQL = "select a.emp_code,isnull(a.emp_name,'')+' '+isnull(a.emp_lname,'')  emp_name, a.UserName, a.Password,b.GroupID,b.GroupName,c.Status,c.StatusId, a.Email from employee a Inner Join UserGroups b on a.groupid  = b.groupid And a.company_id = b.company_id Inner Join UserStatus c on a.statusid = c.statusid Where a.Company_ID=" + compid + " and a.emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR a.emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") order by emp_name";
                        }
                        else
                        {
                            sSQL = "select a.emp_code,isnull(a.emp_name,'')+' '+isnull(a.emp_lname,'')  emp_name, a.UserName, a.Password,b.GroupID,b.GroupName,c.Status,c.StatusId, a.Email from employee a Inner Join UserGroups b on a.groupid  = b.groupid And a.company_id = b.company_id Inner Join UserStatus c on a.statusid = c.statusid Where a.Company_ID=" + compid + " order by emp_name";
                        }
                    }
                }

                ds = GetDataSet(sSQL);
                return ds;
            }
        }

        protected void RadGrid2_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.RadGrid2.DataSource = this.UserDetails;
        }

        int returnval;
        protected void RadGrid2_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["UserName"];
            object emp_code = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["emp_code"];
            string User = id.ToString();
            string sSQL = "sp_user_update";
            string password = (userControl.FindControl("txtpwd") as TextBox).Text;
            string passwordenc = encrypt.SyEncrypt(password).ToString();

            string conpwd = (userControl.FindControl("txtconpwd") as TextBox).Text;
            if (password == "")
                passwordenc = "";

            string email = (userControl.FindControl("txtEmail") as TextBox).Text;
            string status = (userControl.FindControl("drpUserStatus") as DropDownList).SelectedItem.Value;
            string group = (userControl.FindControl("drpUserGrp") as DropDownList).SelectedItem.Value;
            string groupname = (userControl.FindControl("drpUserGrp") as DropDownList).SelectedItem.Text;

            int i = 0;
            SqlParameter[] param = new SqlParameter[2];
            param[i++] = new SqlParameter("@CompanyID", Utility.ToInteger(compid));
            param[i++] = new SqlParameter("@EmployeeID", Utility.ToString(emp_code));
            int retVal = 0;
            string sSQL1 = "sp_CheckSuperAdminCount";


            //int returnval = Convert.ToInt32(DataAccess.ExecuteSPScalar(sSQL1, param));
            // if (!string.IsNullOrEmpty(DataAccess.ExecuteSPScalar(sSQL1, param)))
            if (DataAccess.ExecuteSPScalar(sSQL1, param) != "")
            {
                returnval = Convert.ToInt32(DataAccess.ExecuteSPScalar(sSQL1, param));
            }
            else
            {
                returnval = 0;
            }


            i = 0;
            SqlParameter[] parms = new SqlParameter[5];
            parms[i++] = new SqlParameter("@UserName", Utility.ToString(User));
            parms[i++] = new SqlParameter("@Password", Utility.ToString(passwordenc));
            parms[i++] = new SqlParameter("@GroupId", Utility.ToInteger(group));
            parms[i++] = new SqlParameter("@StatusId", Utility.ToInteger(status));
            parms[i++] = new SqlParameter("@Email", Utility.ToString(email));


            //if (returnval == 1 && (status == "2" || status == "3" || status == "4")) // ONE SuperAdmin 
            //   {
            //       if (groupname == "Super Admin")
            //       {
            //           RadGrid2.Controls.Add(new LiteralControl("<font color = 'Red'>User record cannot be updated. <br/> There should be atleast ONE active SuperAdmin in the system."));
            //       }
            //       else
            //       {
            //           retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
            //           RadGrid2.Controls.Add(new LiteralControl("<font color = 'Red'>User record has been updated."));
            //       }
            //   }
            //   else
            //   {
            if (returnval == 1 && groupname.ToUpper() != "SUPER ADMIN")
            {
               // RadGrid2.Controls.Add(new LiteralControl("<font color = 'Red'>User record cannot be updated. <br/> There should be atleast ONE active SuperAdmin in the system."));
                _actionMessage = "Warning|User record cannot be updated.There should be atleast ONE active SuperAdmin in the system.";
                ViewState["actionMessage"] = _actionMessage;
            }
            else
            {
                retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
               // RadGrid2.Controls.Add(new LiteralControl("<font color = 'Red'>User record has been updated."));
                _actionMessage = "success|User record has been updated.";
                ViewState["actionMessage"] = _actionMessage;
            }
            // }
        }



        protected void RadGrid2_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                string status = dataItem["status"].Text;
                if (status == "Active" || Session["GroupName"].ToString().ToUpper() == "SUPER ADMIN")
                {
                    dataItem["Editcolumn"].Visible = true;
                }
                else
                {
                    dataItem["Editcolumn"].Visible = false;
                }

            }
            //--------------murugan
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                string status = dataItem["status"].Text;
                if (status == "Active")
                {
                    dataItem["Editcolumn"].Visible = true;
                }
                else
                {
                    dataItem["Editcolumn"].Visible = false;
                }

            }

            //-------------

            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Manage Users")) == false)
            {
                RadGrid2.MasterTableView.GetColumn("EditColumn").Visible = false;
            }
        }
        protected void RadGrid2_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Boolean flag = false;
            ArrayList emailList = new ArrayList();
            string userId = null;
            string pwd = null;
            string empName = null;
            int comp_id = 0;
            string emailId = null;
            if (e.CommandName == "UpdateAll")
            {
                foreach (GridItem item in RadGrid2.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        TextBox txtbox = (TextBox)dataItem.FindControl("txtEmail");
                        string email = Utility.ToString(txtbox.Text);
                        if (email != "")
                        {
                            emailList.Add(email);
                        }

                    }
                }
                if (emailList.Count > 0)
                {
                    for (int i = 0; i < emailList.Count; i++)
                    {
                        emailId = Convert.ToString(emailList[i]);
                        string sqlquery = " select userName,password,emp_name +''+ emp_lname as EmpName  from employee where email = '" + emailId + "'  and Company_Id ='" + compid + "'";
                        DataSet ds = DataAccess.FetchRS(CommandType.Text, sqlquery, null);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            userId = Utility.ToString(ds.Tables[0].Rows[0][0].ToString());
                            pwd = encrypt.SyDecrypt(Utility.ToString(ds.Tables[0].Rows[0][1].ToString()));
                            comp_id = Utility.ToInteger(compid);
                            empName = Utility.ToString(ds.Tables[0].Rows[0][2].ToString());
                            sendemail(userId, pwd, comp_id, emailId, empName);
                        }
                    }
                }
            }
            if (e.Item is GridDataItem && e.CommandName == "SendSingleEmail")
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                TextBox txtbox = (TextBox)dataItem.FindControl("txtEmail");
                emailId = Utility.ToString(txtbox.Text);
                comp_id = Utility.ToInteger(compid);
                userId = dataItem["UserName"].Text;
                empName = dataItem["emp_name"].Text;
                pwd = encrypt.SyDecrypt(dataItem["Password"].Text);
                if (emailId.Length > 0)
                {
                    sendemail(userId, pwd, comp_id, emailId, empName);
                }
                else
                {
                    // Response.Write("<script language='javascript'> alert('Email Id Is Blank ,Please Update Email Id') </script>");
                    _actionMessage = "Warning|Email Id Is Blank ,Please Update Email Id.";
                    ViewState["actionMessage"] = _actionMessage;
                }
            }
        }


        private DataSet CSNDetails
        {
            get
            {
                string sSQL = "";
                DataSet ds = new DataSet();
                if (compid == null)
                {
                    compid = "0";
                }
                sSQL = "select id, roc, type, srno,CSN from CPFFiles where Company_Id=" + compid;
                ds = GetDataSet(sSQL);
                return ds;
            }
        }
        protected void sendemail(string userid, string pwd, int compid, string email, string eName)
        {
            try
            {
                string from = "Administrator";
                string to = email;
                string SMTPserver = "";
                string SMTPUser = "";
                string SMTPPass = "";
                string emailreq = "";
                int SMTPPORT = 25;
                StringBuilder mailBody;

                string subject = "Password for User Id : " + userid;
                string Body = "Passport for the Account Of :" + userid;
                SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(compid);
                oANBMailer.Subject = "Login Information ";
                string MailBody = "Greetings " + eName + "; Following are your Login particulars. -##- User ID:" + userid + " -##- Password: " + pwd;

                compid = Utility.ToInteger(Session["Compid"]);
                string strSQL = "Select email_login from company where company_id=" + compid + "";
                DataSet dsLogin = DataAccess.FetchRS(CommandType.Text, strSQL, null);

                if (dsLogin.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        if (dsLogin.Tables[0].Rows[0][0].ToString() != "")
                        {
                            MailBody = dsLogin.Tables[0].Rows[0][0].ToString();
                        }
                    }
                    catch (Exception ex) { }
                }

                MailBody = MailBody.Replace("@emp_name", eName);
                MailBody = MailBody.Replace("@user_name", userid);
                MailBody = MailBody.Replace("@password", pwd);
                MailBody = MailBody.Replace("-##-", Environment.NewLine);
                MailBody = MailBody.Replace("-##-", Environment.NewLine);

                oANBMailer.MailBody = MailBody;
                oANBMailer.From = from;
                oANBMailer.To = email;
                try
                {
                    string sRetVal = oANBMailer.SendMail("Login", eName, DateTime.Now.Date.ToShortDateString(), DateTime.Now.Date.ToShortDateString(), "Send Login Info");
                    if (sRetVal != "")
                    {
                        _actionMessage = "Success|An email has been sent to " + eName;
                        ViewState["actionMessage"] = _actionMessage;
                    }
                }
                catch (Exception ex)
                {
                    _actionMessage = "Warning|Some error occured while sending email to" + eName;
                    ViewState["actionMessage"] = _actionMessage;
                }
            }
            catch (Exception ex)
            {
                _actionMessage = "Warning|Some error occured while sending email to" + eName;
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        protected void RadGrid4_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.RadGrid4.DataSource = this.CSNDetails;
        }



        protected void RadGrid4_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            string company_roc = (userControl.FindControl("txtROC") as TextBox).Text.Trim();
            string company_type = (userControl.FindControl("txtType") as TextBox).Text.Trim();
            string company_srn = (userControl.FindControl("txtSlNo") as TextBox).Text.Trim();
            string csn = company_roc + "-" + company_type + "-" + company_srn;
            string sSQL = "";
            sSQL = "SELECT COUNT(CSN) FROM CPFFiles WHERE UPPER(CSN) = UPPER('" + csn + "')";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
            if (dr.Read())
            {
                if (Utility.ToInteger(dr[0].ToString()) == 0)
                {
                    sSQL = "";
                int retVal = 0;
                sSQL = "Insert into CPFFiles (Company_ID,ROC,Type,Srno,CSN,Created,LastModified)";
                sSQL = sSQL + " Values(" + compid + ", '" + company_roc + "', '" + company_type + "','" + company_srn + "', '" + csn.ToUpper() + "', getdate(), getdate()" + ")";
                try
                {
                    retVal = DataAccess.ExecuteStoreProc(sSQL);
                    _actionMessage = "success|Information added successfully";
                    ViewState["actionMessage"] = _actionMessage;
                }
                catch (Exception ex)
                {
                    string ErrMsg = ex.Message;
                    if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                    {
                        ErrMsg = "Some error occured!Please try again later.";
                      //  RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to add record. Reason:</font> " + ErrMsg));
                        _actionMessage = "Warning|Unable to add record. Reason: " + ErrMsg;
                        ViewState["actionMessage"] = _actionMessage;
                        e.Canceled = true;
                    }
                }
                RadGrid4.Rebind();
                }
                else
                {
                    //  RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>CSN for the company already exists. Please choose a different CSN."));
                    _actionMessage = "Warning|CSN for the compnay already exists. Please choose a different CSN.";
                    ViewState["actionMessage"] = _actionMessage;
                }
            }

        }

        protected void RadGrid4_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            string csnid = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);
            string company_csn = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["CSN"]);
            string company_roc = (userControl.FindControl("txtROC") as TextBox).Text.Trim();
            string company_type = (userControl.FindControl("txtType") as TextBox).Text.Trim();
            string company_srn = (userControl.FindControl("txtSlNo") as TextBox).Text.Trim();
            string csn = company_roc + "-" + company_type + "-" + company_srn;

            string sSQL = "";
            sSQL = "SELECT COUNT(CSN) FROM CPFFiles WHERE UPPER(CSN) = UPPER('" + csn + "')" + "AND id !=" + Convert.ToInt32(csnid);
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);

            if (dr.Read())
            {
                if (Utility.ToInteger(dr[0].ToString()) == 0)
                {
                    sSQL = "update CPFFIles set ROC='" + company_roc + "',Type='" + company_type.ToUpper() + "',SrNo='" + company_srn + "', CSN='" + csn.ToUpper() + "' where id=" + csnid;
                    sSQL += ";" + "update Employee set employer_cpf_acct='" + company_roc + "-" + company_type + "-" + company_srn + "' where employer_cpf_acct = '" + company_csn + "'";
                    int retVal = DataAccess.ExecuteStoreProc(sSQL);
                    if (retVal > 1)
                    {
                        //RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>Information updated successfully"));
                        _actionMessage = "success|Information updated successfully";
                       
                    }
                    else
                    {
                        //RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to update the record"));
                        _actionMessage = "Warning|Unable to update the record";
                       
                    }
                    ViewState["actionMessage"] = _actionMessage;
                    this.RadGrid4.DataSource = this.CSNDetails;
                    RadGrid4.Rebind();
                }

                else
                {
                   // RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>CSN for the compnay already exists. Please choose a different CSN."));
                    _actionMessage = "Warning|CSN for the compnay already exists. Please choose a different CSN.";
                    ViewState["actionMessage"] = _actionMessage;
                }
            }
        }

        protected void RadGrid4_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string csnid = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);
                string company_csn = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["CSN"]);

                string SQL1 = "select count(employer_cpf_acct) from employee where employer_cpf_acct= '" + company_csn + "'";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQL1, null);
                if (dr.Read())
                {
                    if (Utility.ToInteger(dr[0].ToString()) > 0)
                    {
                      //  RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete this record.CPF is in use."));
                        _actionMessage = "Warning|Unable to delete this record.CPF is in use.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        string sSQL = "DELETE FROM CPFFiles where id ='{0}'";
                        sSQL = string.Format(sSQL, csnid);
                        int i = DataAccess.ExecuteStoreProc(sSQL);
                    }
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = "Some error occured.";
               // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the User. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Unable to delete the User. Reason: " + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }

        protected void txtTestEmail_Click(object sender, EventArgs e)
        {
            SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(txtsmtpserver.Value.ToString(), txtemailuser.Value.ToString(), emailpwd.Text, txtemailsender_address.Value.ToString(), txtemailsender_address.Value.ToString(), Utility.ToInteger(txtsmtpport.Value), ddlssl.SelectedItem.Value);

            oANBMailer.Subject = "Test Email from WebPortal";
            oANBMailer.MailBody = "This is test mail from WebPortal";
            oANBMailer.From = txtemailsender_address.Value.ToString();
            oANBMailer.To = txtemailsender_domain.Value.ToString();

            try
            {
                string sRetVal = oANBMailer.SendMail();
                if (sRetVal == "")
                {
                   // Response.Write("<script language = 'Javascript'>alert('Test Successful.');</script>");
                    _actionMessage = "Success|Test Successful." ;
                    ViewState["actionMessage"] = _actionMessage;
                }
                else
                {
                    //Response.Write("<BR><BR><font color='red'>" + sRetVal + "</font><BR><BR>");
                    lblerror.Text = "Error:" + sRetVal;
                    // Response.Write("<script language = 'Javascript'>alert('There Problem to send email please contact adminstrator  " + sRetVal + "');</script>");
                    _actionMessage = "Warning|There is some problem in sending email please contact adminstrator.";
                    ViewState["actionMessage"] = _actionMessage;
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                // Response.Write("<script language = 'Javascript'>alert('There Problem to send email please contact adminstrator. " + str + "');</script>");
                _actionMessage = "Warning|There is some problem in sending email please contact adminstrator.";
                ViewState["actionMessage"] = _actionMessage;
            }

        }

        bool EmpProc;
        string ProceserEmail;
        protected void btnsave_Click(object sender, EventArgs e)
        {

            #region insert
            if (s == 0)
            {
                //Validation to check whether Prefix code exist already
                string SQLValidate = " select * from Company where Company_Code='" + txtCompCode.Text + "'";
                SqlDataReader dr_valid = DataAccess.ExecuteReader(CommandType.Text, SQLValidate, null);
                if (dr_valid.HasRows)
                {
                    //lblerror.Text = "Prefix Exist Already";
                    //ShowMessageBox("Prefix Exist Already");
                    _actionMessage = "Warning|Prefix Exist Already.";
                    ViewState["actionMessage"] = _actionMessage;
                    return;
                }
                else
                {
                    //
                    string Compcode = txtCompCode.Text.Trim();
                    string password1 = Compcode + "_Admin";
                    string password = encrypt.SyEncrypt(password1);
                    string CompName = txtCompName.Text.Trim();
                    if (CompName != "" && Compcode != "")
                    {
                        string CompPhone = txtCompPhone.Value;
                        string website = txtwebsite.Value;
                        string Compemail = txtCompemail.Value;
                        string Compcity = txtCompcity.Value;
                        string Compfax = txtCompfax.Value;
                        string Country = cmbCountry.SelectedValue;
                        string Compperson = txtCompperson.Value;
                        string Compstate = txtCompstate.Value;
                        string designation = txtdesign.Value;
                        string company_roc = txtcompany_roc.Text;
                        string company_type = drpcompany_type.SelectedItem.Value;
                        string compaddress = txtcompaddress.Text;
                        string auth_emai = txtauth_emai.Value;
                        string hrs_day = txthrs_day.Value;
                        string min_day = txtmin_day.Value;
                        //CPF Changes No Need to do here
                        string monthly_cpf_ceil = txtmonthly_cpf_ceil.Value;
                        string annual_cpf_ceil = txtannual_cpf_ceil.Value;
                        string varpayapproval = RdApproval.SelectedValue;
                        string no_work_days = cmbworkingdays1.SelectedValue; //Added by Sandi on 26/03/2014
                        no_work_days = cmbworkingdays.SelectedValue;
                        string payslipformat = cmbpayslipformat.SelectedValue;
                        string email_sender = txtemailsender_address.Value;
                        string email_username = txtemailuser.Value;
                        //string email_replyaddress = txtemail_replyaddress.Value;
                        string email_replyaddress = Editortxtemail_replyaddress.Content.ToString();
                        // string email_replyname = txtemail_replyname.Value;
                        string email_replyname = Editortxtemail_replyname.Content;
                        // string email_leavedel = txtemail_leavedel.Value;
                        string email_leavedel = Editortxtemail_leavedel.Content;
                        string email_password = emailpwd.Text;
                        //r
                        //string email_sendername = txtemail_sendername.Value;
                        string email_sendername = EditorLevReq.Content.ToString();
                        string email_senderdomain = txtemailsender_domain.Value;
                        string email_smtpserver = txtsmtpserver.Value;
                        string email_smtpport = txtsmtpport.Value;
                        string timesheet = rdtimesheet.Value;
                        string tsremarks = rdtsremarks.Value;
                        string compaddress2 = txtcompaddress2.Text;
                        string postalcode = txtpostalcode.Text;
                        //string email_claim_sender_name = txtclaim_sendername.Value;
                        string email_claim_sender_name = Editortxtclaim_sendername.Content;
                        //string email_claim_reply_name = txtemailclaim_replyname.Value;
                        string email_claim_reply_name = Editortxtemailclaim_replyname.Content;
                        string sslrequired = ddlssl.SelectedValue;
                        string pwdrequired = cmbEPayPwd.SelectedValue;
                        string ccmail = txtccmail.Value;
                        string ccmailclaim = cmbccclaim.Value;
                        string ccmailleave = cmbccleave.Value;
                        string leaveroundoff = cmbLeaveRoundoff.SelectedValue;
                        string additionsroundoff = cmbAdditionsRoundoff.SelectedValue;
                        string deductionsroundoff = cmbDeductionsRoundoff.SelectedValue;
                        string netpayroundoff = cmbNetPayRoundoff.SelectedValue;
                        int payrolltype = Convert.ToInt16(cmbPayrollType.SelectedValue);
                        int projectassign = Convert.ToInt16(cmbAssignType.SelectedValue);
                        string SalaryGLCode = txtSalaryGL.Value;
                        string EmployeeCPFGLCode = txtEmpCPFGL.Value;
                        string EmployerCPFGLCode = txtEmpyCPFGL.Value;
                        string FundAmtGLCode = txtFundGL.Value;
                        string SDLAmtGLCode = txtSDLGL.Value;
                        string AccountGLCode = txtacccompGL.Value;
                        string UnpaidLeaGLCode = txtunpaidGL.Value;
                        string strPH = cmbPublicHoliday.SelectedValue.ToString();
                        string strSunday = cmbSunday.SelectedValue.ToString();
                        string strRosNa = cmbRosterNa.SelectedValue.ToString();
                        int ROund = Convert.ToInt32(cmbRound.SelectedValue.ToString());
                        int chk = 0;
                        if (chkFiFo.SelectedValue.ToString() == "FIFO")
                        {
                            chk = 1;
                        }

                        string OffDay1 = "";
                        string OffDay2 = "";
                        bool HalfDay1 = false;
                        bool UseLeaveCal = false;
                        string CustomizedCal = "default";
                        //Added by Sandi on 31/3/2014
                        int LeaveDayAhead;
                        if (rtxtLeaveDayAhead.Text == "")
                        {
                            LeaveDayAhead = 0;
                        }
                        else
                        {
                            LeaveDayAhead = Convert.ToInt32(rtxtLeaveDayAhead.Text);
                        }
                        bool IncludingCurrentDay;
                        if (chkIncludingCurrentDay.Checked == true)
                        {
                            IncludingCurrentDay = true;
                        }
                        else
                        {
                            IncludingCurrentDay = false;
                        }

                        bool mobileTimeSheet = false;
                        if (this.mobilescancode.Checked == true)
                            mobileTimeSheet = true;



                        bool showroster_ = false;
                        if (this.showroster.Checked == true)
                            showroster_ = true;
                        //End Added

                        int showAcNo = 1;
                        if (this.ShowBankAcNo.Checked == false)
                            showAcNo = 0;

                        int enableautoreminder = 0;
                        if (this.autoreminder.SelectedValue == "1")
                            enableautoreminder = 1;


                        int i = 0;
                        SqlParameter[] parms = new SqlParameter[123];
                        parms[i++] = new SqlParameter("@Company_Code", Utility.ToString(Compcode));
                        parms[i++] = new SqlParameter("@Company_name", Utility.ToString(CompName));
                        parms[i++] = new SqlParameter("@phone", Utility.ToInteger(CompPhone));
                        parms[i++] = new SqlParameter("@email", Utility.ToString(Compemail));
                        parms[i++] = new SqlParameter("@website", Utility.ToString(website));
                        parms[i++] = new SqlParameter("@city", Utility.ToString(Compcity));
                        parms[i++] = new SqlParameter("@Fax", Utility.ToString(Compfax));
                        parms[i++] = new SqlParameter("@country", Utility.ToInteger(Country));
                        parms[i++] = new SqlParameter("@auth_person", Utility.ToString(Compperson));
                        parms[i++] = new SqlParameter("@designation", designation);
                        parms[i++] = new SqlParameter("@company_roc", company_roc);
                        parms[i++] = new SqlParameter("@company_type", company_type);
                        parms[i++] = new SqlParameter("@Address", Utility.ToString(compaddress));
                        parms[i++] = new SqlParameter("@Auth_email", Utility.ToString(auth_emai));
                        //CPF Changes
                        parms[i++] = new SqlParameter("@monthly_cpf_ceil", Utility.ToInteger(monthly_cpf_ceil));
                        parms[i++] = new SqlParameter("@annual_cpf_ceil", Utility.ToInteger(annual_cpf_ceil));
                        parms[i++] = new SqlParameter("@payslip_format", Utility.ToString(payslipformat));
                        parms[i++] = new SqlParameter("@Payroll_Approval", Utility.ToInteger(varpayapproval));
                        parms[i++] = new SqlParameter("@no_work_days", Utility.ToDouble(no_work_days));
                        parms[i++] = new SqlParameter("@day_hours", Utility.ToDouble(hrs_day));
                        parms[i++] = new SqlParameter("@day_min", Utility.ToDouble(min_day));
                        parms[i++] = new SqlParameter("@email_sender", Utility.ToString(email_sender));
                        parms[i++] = new SqlParameter("@email_username", Utility.ToString(email_username));
                        parms[i++] = new SqlParameter("@email_reply_address", Utility.ToString(email_replyaddress));
                        parms[i++] = new SqlParameter("@email_reply_name", Utility.ToString(email_replyname));
                        parms[i++] = new SqlParameter("@email_leavedel", Utility.ToString(email_leavedel));
                        parms[i++] = new SqlParameter("@email_password", Utility.ToString(email_password));
                        parms[i++] = new SqlParameter("@email_sender_name", Utility.ToString(email_sendername));
                        parms[i++] = new SqlParameter("@email_sender_domain", Utility.ToString(email_senderdomain));
                        parms[i++] = new SqlParameter("@email_SMTP_server", Utility.ToString(email_smtpserver));
                        parms[i++] = new SqlParameter("@email_SMTP_port", Utility.ToString(email_smtpport));
                        parms[i++] = new SqlParameter("@timesheet_approve", Utility.ToInteger(timesheet));
                        parms[i++] = new SqlParameter("@SessionID", Session.SessionID);
                        parms[i++] = new SqlParameter("@state", Utility.ToString(txtCompstate.Value));
                        parms[i++] = new SqlParameter("@password", password);
                        parms[i++] = new SqlParameter("@address2", compaddress2);
                        parms[i++] = new SqlParameter("@postal_code", postalcode);
                        parms[i++] = new SqlParameter("@email_leavealert", cmbemailleave.SelectedValue);
                        parms[i++] = new SqlParameter("@email_payalert", cmbemailpay.SelectedValue);
                        // parms[i++] = new SqlParameter("@epayslip", cmbEmailPaySlip.SelectedValue);
                        parms[i++] = new SqlParameter("@epayslip", cmbEmailPaySlip.Checked == true ? "Y" : "N");

                        parms[i++] = new SqlParameter("@leave_model", Utility.ToInteger(cmbLeaveModel.SelectedValue));
                        parms[i++] = new SqlParameter("@email_claim_sender_name", Utility.ToString(email_claim_sender_name));
                        parms[i++] = new SqlParameter("@email_claim_reply_name", Utility.ToString(email_claim_reply_name));
                        parms[i++] = new SqlParameter("@email_claimalert", cmbclaim.SelectedValue);
                        parms[i++] = new SqlParameter("@sslrequired", sslrequired);
                        parms[i++] = new SqlParameter("@pwdrequired", pwdrequired);
                        parms[i++] = new SqlParameter("@ccmail", ccmail);
                        parms[i++] = new SqlParameter("@ccclaimalert", ccmailclaim);
                        parms[i++] = new SqlParameter("@ccleavealert", ccmailleave);
                        parms[i++] = new SqlParameter("@leaveroundoff", leaveroundoff);
                        parms[i++] = new SqlParameter("@additionsroundoff", additionsroundoff);
                        parms[i++] = new SqlParameter("@deductionsroundoff", deductionsroundoff);
                        parms[i++] = new SqlParameter("@netpayroundoff", netpayroundoff);
                        parms[i++] = new SqlParameter("@payrolltype", payrolltype);
                        parms[i++] = new SqlParameter("@tsremarks", Utility.ToInteger(tsremarks));
                        parms[i++] = new SqlParameter("@projectassign", Utility.ToInteger(projectassign));
                        parms[i++] = new SqlParameter("@SalaryGLCode", SalaryGLCode);
                        parms[i++] = new SqlParameter("@EmployeeCPFGLCode", EmployeeCPFGLCode);
                        parms[i++] = new SqlParameter("@EmployerCPFGLCode", EmployerCPFGLCode);
                        parms[i++] = new SqlParameter("@FundAmtGLCode", FundAmtGLCode);
                        parms[i++] = new SqlParameter("@SDLAmtGLCode", SDLAmtGLCode);
                        parms[i++] = new SqlParameter("@AccountGLCode", AccountGLCode);
                        parms[i++] = new SqlParameter("@UnpaidLeaGLCode", UnpaidLeaGLCode);
                        parms[i++] = new SqlParameter("@PublicHoliday", strPH);
                        parms[i++] = new SqlParameter("@Sunday", strSunday);
                        parms[i++] = new SqlParameter("@RosterNa", strRosNa);
                        parms[i++] = new SqlParameter("@Round", ROund);
                        parms[i++] = new SqlParameter("@FIFO", chk);
                        parms[i++] = new SqlParameter("@isMaster", cmbIsMaster.SelectedValue);
                        parms[i++] = new SqlParameter("@isMasterEmpTemp", cmbtempEmp.SelectedValue);
                        parms[i++] = new SqlParameter("@loginWithOutComany", loginWithOutComany.SelectedValue);
                        parms[i++] = new SqlParameter("@showBankAcNo", showAcNo);
                        parms[i++] = new SqlParameter("@EnableAutoreminder", enableautoreminder);

                        #region TimeSheet (D)Settings
                        bool SendEmail;

                        if (cbxEmailAlert.SelectedValue == "Yes")
                        {
                            SendEmail = true;
                        }
                        else
                        {
                            SendEmail = false;
                        }

                        if (drpEmpProc1.Visible)
                        {
                            if (drpEmpProc1.SelectedValue == "Employee")
                            {
                                EmpProc = false;
                                ProceserEmail = "";
                            }
                            else if (drpEmpProc1.SelectedValue == "Processer")
                            {
                                EmpProc = true;
                                ProceserEmail = txtProcesserEmail.Text;
                            }
                        }
                        else
                        {
                            EmpProc = false;
                            ProceserEmail = "";
                        }


                        // string sqlUpdate = "UPDATE [dbo].[Company] SET [SendEmail] = '" + SendEmail + "' ,[EmpProcessor] = '" + EmpProc + "'  ,[ProcessEmail] = '" + ProceserEmail + "' WHERE Company_Id='" + Utility.ToString(compid) + "'";
                        //DataAccess.FetchRS(CommandType.Text, sqlUpdate, null);


                        #endregion

                        parms[i++] = new SqlParameter("@SendEmail", SendEmail);
                        parms[i++] = new SqlParameter("@EmpProc", EmpProc);
                        parms[i++] = new SqlParameter("@ProcessEmail", ProceserEmail);

                        if (radAdvanceTs.SelectedValue == "No")
                        {
                            parms[i++] = new SqlParameter("@AdvTs", txtMinutes.Text);
                        }
                        else
                        {
                            parms[i++] = new SqlParameter("@AdvTs", -1);
                        }
                        if (radLFort.SelectedDate != null)
                        {
                            parms[i++] = new SqlParameter("@LeaveFFDate", Convert.ToDateTime(radLFort.SelectedDate));
                        }
                        else
                        {
                            parms[i++] = new SqlParameter("@LeaveFFDate", System.DBNull.Value);
                        }

                        parms[i++] = new SqlParameter("@WorkFlowID", rdWorkFlow.SelectedValue);

                        if (rdWorkFlow.SelectedValue.ToString() == "-1")
                        {
                            parms[i++] = new SqlParameter("@WFEMP", System.DBNull.Value);
                            parms[i++] = new SqlParameter("@WFLEAVE", System.DBNull.Value);
                            parms[i++] = new SqlParameter("@WFCLAIM", System.DBNull.Value);
                            parms[i++] = new SqlParameter("@WFPAY", System.DBNull.Value);
                            parms[i++] = new SqlParameter("@WFReport", System.DBNull.Value);
                            parms[i++] = new SqlParameter("@WFTimeSheet", System.DBNull.Value);
                        }
                        else
                        {
                            if (chkWF.Items[0].Selected == true)
                            {
                                parms[i++] = new SqlParameter("@WFEMP", 1);
                            }
                            else
                            {
                                parms[i++] = new SqlParameter("@WFEMP", System.DBNull.Value);
                            }

                            if (chkWF.Items[1].Selected == true)
                            {
                                parms[i++] = new SqlParameter("@WFLEAVE", 1);
                            }
                            else
                            {
                                parms[i++] = new SqlParameter("@WFLEAVE", System.DBNull.Value);
                            }

                            if (chkWF.Items[2].Selected == true)
                            {
                                parms[i++] = new SqlParameter("@WFCLAIM", 1);
                            }
                            else
                            {
                                parms[i++] = new SqlParameter("@WFCLAIM", System.DBNull.Value);
                            }

                            if (chkWF.Items[3].Selected == true)
                            {
                                parms[i++] = new SqlParameter("@WFPAY", 1);
                            }
                            else
                            {
                                parms[i++] = new SqlParameter("@WFPAY", System.DBNull.Value);
                            }

                            if (chkWF.Items[4].Selected == true)
                            {
                                parms[i++] = new SqlParameter("@WFReport", 1);
                            }
                            else
                            {
                                parms[i++] = new SqlParameter("@WFReport", System.DBNull.Value);
                            }

                            if (chkWF.Items[5].Selected == true)
                            {
                                parms[i++] = new SqlParameter("@WFTimeSheet", 1);
                            }
                            else
                            {
                                parms[i++] = new SqlParameter("@WFTimeSheet", System.DBNull.Value);
                            }

                        }

                        //
                        if (radListPayrollApp.SelectedValue == "1")
                        {
                            parms[i++] = new SqlParameter("@AppTSProcess", 1);
                        }
                        else
                        {
                            parms[i++] = new SqlParameter("@AppTSProcess", 0);
                        }



                        if (radListLeaveApp.SelectedValue == "1")
                        {
                            parms[i++] = new SqlParameter("@AppLeaveProcess", 1);
                        }
                        else
                        {
                            parms[i++] = new SqlParameter("@AppLeaveProcess", 0);
                        }

                        if (radListClaimApp.SelectedValue == "1")
                        {
                            parms[i++] = new SqlParameter("@AppClaimsProcess", 1);
                        }
                        else
                        {
                            parms[i++] = new SqlParameter("@AppClaimsProcess", 0);
                        }

                        if (chkWL.Checked)
                        {
                            parms[i++] = new SqlParameter("@FOWL", 1);
                        }
                        else
                        {
                            parms[i++] = new SqlParameter("@FOWL", 0);
                        }

                        if (chkLeave.Checked)
                        {
                            parms[i++] = new SqlParameter("@UnpaidLAmount", 1);
                        }
                        else
                        {
                            parms[i++] = new SqlParameter("@UnpaidLAmount", 0);
                        }

                        //
                        parms[i++] = new SqlParameter("@CurrencyID", Convert.ToInt32(drpCurrency.SelectedValue));

                        if (chkClaims.Checked)
                        {
                            parms[i++] = new SqlParameter("@ClaimsCash", 1);
                        }
                        else
                        {
                            parms[i++] = new SqlParameter("@ClaimsCash", 2);
                        }

                        parms[i++] = new SqlParameter("@ConversionOpt", Convert.ToInt32(drpConv.SelectedValue));
                        parms[i++] = new SqlParameter("@MultiCurr", Convert.ToInt32(rdMultiCurr.SelectedValue));

                        string remakYn = "-1";
                        string normalHrBt = "-1";
                        string overTimeBT = "-1";

                        if (chkBoxTs.Items[0].Selected)
                        {
                            remakYn = chkBoxTs.Items[0].Value;
                        }

                        if (chkBoxTs.Items[1].Selected)
                        {
                            normalHrBt = chkBoxTs.Items[1].Value;
                        }

                        if (chkBoxTs.Items[2].Selected)
                        {
                            overTimeBT = chkBoxTs.Items[2].Value;
                        }

                        parms[i++] = new SqlParameter("@RemarksYN", remakYn);
                        parms[i++] = new SqlParameter("@NormalHrBT", normalHrBt);
                        parms[i++] = new SqlParameter("@OverTHrBT", overTimeBT);
                        int convval = 0;

                        if (chkAdClaims.Checked)
                            convval = 1;
                        parms[i++] = new SqlParameter("@AdvClaims", convval);
                        // Added by Su Mon 
                        parms[i++] = new SqlParameter("@OffDay1", OffDay1);
                        parms[i++] = new SqlParameter("@OffDay2", OffDay2);
                        parms[i++] = new SqlParameter("@HalfDay1", HalfDay1);
                        parms[i++] = new SqlParameter("@UseLeaveCal", UseLeaveCal);
                        parms[i++] = new SqlParameter("@CustomizedCal", CustomizedCal);
                        // End added
                        parms[i++] = new SqlParameter("@LeaveDayAhead", LeaveDayAhead); //Added by Sandi on 31/3/2014
                        parms[i++] = new SqlParameter("@IncludingCurrentDay", IncludingCurrentDay); //Added by Sandi on 31/3/2014

                        parms[i++] = new SqlParameter("@mobileTimeSheet", mobileTimeSheet);

                        parms[i++] = new SqlParameter("@showroster", showroster_);
                        //kumar added for roster type


                        if (this.rostertype.SelectedValue == "2")
                        {
                            parms[i++] = new SqlParameter("@RosterType", 2);
                        }
                        else
                        {
                            parms[i++] = new SqlParameter("@RosterType", 1);
                        }

                        if (this.overtimemode.SelectedValue == "False")
                        {
                            parms[i++] = new SqlParameter("@WeekRosterOtMode", "False");
                        }
                        else
                        {
                            parms[i++] = new SqlParameter("@WeekRosterOtMode", "True");
                        }
                        if (rdbGrouping.SelectedValue == "1")
                        {
                            parms[i++] = new SqlParameter("@GroupManage", 1);
                        }
                        else
                        {
                            parms[i++] = new SqlParameter("@GroupManage", int.MinValue);
                        }

                        int incompleatemonthManvalRate = 0;
                        if (this.incommanuvalRate.Checked == true)
                            incompleatemonthManvalRate = 1;

                        parms[i++] = new SqlParameter("@incompleatemonthManvalRate", incompleatemonthManvalRate);
                        if (chkApprovalDate.Checked)
                        {
                            parms[i++] = new SqlParameter("@isApproveDate", 1);
                        }
                        else
                        {
                            parms[i++] = new SqlParameter("@isApproveDate", 0);
                        }

                        parms[i++] = new SqlParameter("@ccTimeSheet", txtcctimesheet.Value.ToString());


                        if (chk_LGOT.Checked)
                        {
                            parms[i++] = new SqlParameter("@OTseparate", "1");
                        }
                        else
                        {
                            parms[i++] = new SqlParameter("@OTseparate", "0");
                        }
                        parms[i++] = new SqlParameter("@OTGLCode", txtOTGL.Value.ToString());
                        parms[i++] = new SqlParameter("@SDLPayableGL", txtSDLpayable.Value.ToString());
                        parms[i++] = new SqlParameter("@SalaryPayableGL", txtSalarypayable.Value.ToString());
                        parms[i++] = new SqlParameter("@CPFPayableGL", txtCPFpayable.ToString());

                        if (radListTSApp.SelectedValue == "1")
                        {
                            parms[i++] = new SqlParameter("@TSrequired", "1");
                        }
                        else
                        {
                            parms[i++] = new SqlParameter("@TSrequired", "0");
                        }

                        if (chkWF.Items[6].Selected == true) //muru
                        {
                            parms[i++] = new SqlParameter("@WFAppraisal", "1");
                        }
                        else
                        {
                            parms[i++] = new SqlParameter("@WFAppraisal", "0");
                        }

                        parms[i++] = new SqlParameter("@email_appraisal", "NO");

                        if (radListALAp.SelectedValue == "1")
                        {
                            parms[i++] = new SqlParameter("@APPrequired", "1");
                        }
                        else
                        {
                            parms[i++] = new SqlParameter("@APPrequired", "0");
                        }

                        int ClaimTotalForPaySlip = 0;
                        if (this.ClaimTotalForPaySlip.Checked == true)
                            ClaimTotalForPaySlip = 1;
                        parms[i++] = new SqlParameter("@ClaimTotalForPaySlip", ClaimTotalForPaySlip);


                        string sSQL = "sp_comp_add";
                        try
                        {

                            int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                            if (retVal >= 1)
                            {
                                lblerror.ForeColor = System.Drawing.Color.Green;
                            }

                            //Insert Data in database 

                            int compayid = 0;
                            string strCompany = "Select company_id from company where company_name ='" + Utility.ToString(CompName) + "'";
                            SqlDataReader dr;
                            dr = DataAccess.ExecuteReader(CommandType.Text, strCompany, null);

                            while (dr.Read())
                            {
                                compayid = Utility.ToInteger(dr[0].ToString());
                            }

                            //Inserting Alias Name
                            if (txtAlias.Text.ToString() != "" || txtAlias.Text.ToString() != null)
                            {
                                string InsertAliasname = "INSERT INTO [Company_Alias]([Company_id],[AliasName]) VALUES  ('" + compayid + "','" + txtAlias.Text.ToString() + "' )";
                                DataAccess.ExecuteNonQuery(InsertAliasname, null);
                            }
                            //


                            // compayid = Utility.ToInteger(compid);
                            if (tblPaySlipSetup1.Visible)
                            {
                                //string strDeletePaslipFormat = "DELETE FROM Report_Settings WHERE COMPANYID=" + compayid + "  AND PAYSLIPFORMAT=" + cmbpayslipformat.SelectedValue;
                                //int rtval1 = DataAccess.ExecuteNonQuery(strDeletePaslipFormat, null);                      

                                string strName = "";
                                string strIdno = "";
                                string strSalForMonth = "";
                                string strYear = "";
                                string strEarnings = "";
                                string strDeductions = "";
                                string strTOTALGROSS = "";
                                string strCPFGROSS = "";
                                string strEMPLOYERCPF = "";
                                string strTOTALDEDUCTION = "";
                                string strNETPAYMENT = "";
                                string strYEARTODATE = "";
                                string strYEATODATEEMPLOYERCPF = "";

                                int logomgt = -1;
                                string strDEPTNAME = "";
                                string strTRADE = "";
                                string strDESIGNATION = "";
                                //By Jammu Office
                                //if (radPayNameYesNo.SelectedValue == "2")
                                if (radPayNameYesNo.Checked == false)
                                {
                                    strName = "-1";
                                }
                                else
                                {
                                    strName = txtPayName.Text;
                                }

                                //if (radPayIDNOYesNo.SelectedValue == "2")
                                    if (radPayIDNOYesNo.Checked == false)
                                    {
                                    strIdno = "-1";
                                }
                                else
                                {
                                    strIdno = txtPayIDNO.Text;
                                }

                                //if (radPaySalMonthYesNo.SelectedValue == "2")
                                if (radPaySalMonthYesNo.Checked == false)
                                {
                                    strSalForMonth = "-1";
                                }
                                else
                                {
                                    strSalForMonth = txtPaySALMONTH.Text;
                                }

                                //if (radPayYear.SelectedValue == "2")
                                if (radPayYear.Checked == false)
                                {
                                    strYear = "-1";
                                }
                                else
                                {
                                    strYear = txtPayYEAR.Text;
                                }

                                //if (radPayEarnings.SelectedValue == "2")
                                if (radPayEarnings.Checked == false)
                                {
                                    strEarnings = "-1";
                                }
                                else
                                {
                                    strEarnings = txtPayEARNINGS.Text;
                                }

                                //if (radPayDeductions.SelectedValue == "2")
                                if (radPayDeductions.Checked == false)
                                {
                                    strDeductions = "-1";
                                }
                                else
                                {
                                    strDeductions = txtPayDEDUCTIONS.Text;
                                }

                                //if (radPayTotalGross.SelectedValue == "2")
                                if (radPayTotalGross.Checked == false)
                                {
                                    strTOTALGROSS = "-1";
                                }
                                else
                                {
                                    strTOTALGROSS = txtPayTOTALGROSS.Text;
                                }

                                //if (radPayCpfGross.SelectedValue == "2")
                                if (radPayCpfGross.Checked == false)
                                {
                                    strCPFGROSS = "-1";
                                }
                                else
                                {
                                    strCPFGROSS = txtPayCpfGross.Text;

                                }

                                //if (radPayEmployerCpf.SelectedValue == "2")
                                if (radPayEmployerCpf.Checked == false)
                                {
                                    strEMPLOYERCPF = "-1";
                                }
                                else
                                {

                                    strEMPLOYERCPF = txtPayEMPLOYERCPF.Text;
                                }

                                //if (radPayTotalDeduction.SelectedValue == "2")
                                if (radPayTotalDeduction.Checked == false)
                                {
                                    strTOTALDEDUCTION = "-1";
                                }
                                else
                                {
                                    strTOTALDEDUCTION = txtPayTOTALDEDUCTION.Text;

                                }

                                //if (radPayNETPAYMENT.SelectedValue == "2")
                                if (radPayNETPAYMENT.Checked == false)
                                {
                                    strNETPAYMENT = "-1";
                                }
                                else
                                {
                                    strNETPAYMENT = txtPayNETPAYMENT.Text;
                                }

                                //if (radPayYEARTODATE.SelectedValue == "2")
                                if (radPayYEARTODATE.Checked == false)
                                {
                                    strYEARTODATE = "-1";
                                }
                                else
                                {
                                    strYEARTODATE = txtPayYEARTODATE.Text;
                                }

                                //if (radPayYEATODATEEMPLOYERCPF.SelectedValue == "2")
                                if (radPayYEATODATEEMPLOYERCPF.Checked == false)
                                {
                                    strYEATODATEEMPLOYERCPF = "-1";
                                }
                                else
                                {
                                    strYEATODATEEMPLOYERCPF = txtPayYEATODATEEMPLOYERCPF.Text;
                                }

                                if (radPayLOGOMGT.SelectedValue == "1")
                                {
                                    logomgt = 1;
                                }

                                if (radPayLOGOMGT.SelectedValue == "2")
                                {
                                    logomgt = 2;
                                }
                                if (radPayLOGOMGT.SelectedValue == "3")
                                {
                                    logomgt = 3;
                                }

                                if (radPayLOGOMGT.SelectedValue == "4")
                                {
                                    logomgt = 4;
                                }

                                //if (radPayDEPTNAME.SelectedValue == "2")
                                if (radPayDEPTNAME.Checked == false)
                                {
                                    strDEPTNAME = "-1";
                                }
                                else
                                {
                                    strDEPTNAME = txtPayDepartmentName.Text;
                                }

                                //if (radPayTrade.SelectedValue == "2")
                                if (radPayTrade.Checked == false)
                                {
                                    strTRADE = "-1";
                                }
                                else
                                {
                                    strTRADE = txtPayTrade.Text;
                                }


                                //if (radPayDesignation.SelectedValue == "2")
                                if (radPayDesignation.Checked == false)
                                {
                                    strDESIGNATION = "-1";
                                }
                                else
                                {
                                    strDESIGNATION = txtPayDesignation.Text;
                                }

                                string strPayleaveDetails = "";
                                string strAddDetails = "";
                                ////////////////////////////////////////////////////////////
                                if (radPayLEAVEDETAILS.SelectedValue == "1")
                                {
                                    strPayleaveDetails = "1";
                                }
                                if (radPayLEAVEDETAILS.SelectedValue == "2")
                                {
                                    strPayleaveDetails = "2";
                                }
                                ////////////////////////////////////////////////////////////
                                if (radPayEARNINGDETAILS.SelectedValue == "1")
                                {
                                    strAddDetails = "1";
                                }
                                if (radPayEARNINGDETAILS.SelectedValue == "2")
                                {
                                    strAddDetails = "2";
                                }
                                if (radPayEARNINGDETAILS.SelectedValue == "3")
                                {
                                    strAddDetails = "3";
                                }
                                ////////////////////////////////////////////////////////////



                                string strInsert = "INSERT INTO [Report_Settings]([PAYSLIPFORMAT],[NAME],[IDNO],[SALFORMONTH],[YEAR],[EARNINGS],[DEDUCTIONS],[TOTALGROSS],[CPFGROSS],[EMPLOYERCPF],[TOTALDEDUCTION]";
                                strInsert = strInsert + ",[NETPAYMENT],[YEARTODATE],[YEATODATEEMPLOYERCPF],[COMPANYID],[LOGOMGT],[DEPTNAME],[TRADE],[DESIGNATION],[LEAVEDETAILS],[ADDITIONSDETAILS])VALUES('" + cmbpayslipformat.SelectedValue + "','" + strName + "','" + strIdno + "','";
                                strInsert = strInsert + strSalForMonth + "','" + strYear + "','" + strEarnings + "','" + strDeductions + "','" + strTOTALGROSS + "','" + strCPFGROSS + "','" + strEMPLOYERCPF + "','" + strTOTALDEDUCTION + "','" + strNETPAYMENT + "','";
                                strInsert = strInsert + strYEARTODATE + "','" + strYEATODATEEMPLOYERCPF + "'," + compayid + "," + logomgt + ",'" + strDEPTNAME + "','" + strTRADE + "','" + strDESIGNATION + "'," + strPayleaveDetails + "," + strAddDetails + ")";

                                int data = DataAccess.ExecuteNonQuery(strInsert, null);

                            }
                            else if (tblPayslipSetup2.Visible)
                            {

                                string strCustName = "";
                                string strCustIdno = "";
                                string strCustSalForMonth = "";
                                string strCustYear = "";
                                string strCustEarnings = "";
                                string strCustDeductions = "";
                                string strCustTOTALGROSS = "";
                                string strCustCPFGROSS = "";
                                string strCustEMPLOYERCPF = "";
                                string strCustTOTALDEDUCTION = "";
                                string strCustNETPAYMENT = "";
                                string strCustYEARTODATE = "";
                                string strCustYEATODATEEMPLOYERCPF = "";

                                int logomgtCust = -1;
                                string strCustDEPTNAME = "";
                                string strCustTRADE = "";
                                string strCustDESIGNATION = "";
                                string strCustDOB = "";
                                string strCustTimeCardNo = "";
                                string strCustJoiningDate = "";
                                string strCustTerminationDate = "";
                                string strCustBusinessUnit = "";
                                string strCustPayslipPeriod = "";
                                string strCustTotalAdditions = "";
                                string strCustDateOfPayment = "";
                                string strCustModeOfPayment = "";
                                string strCustYearToDateEmployeeCPF = "";
                                string strCustRemarks = "";
                                string strCustOvertimePeriod = "";
                                string strCustChequeNumber = "";

                                //By Jammu Office
                                //if (radCustomizePayNameYesNo.SelectedValue == "2")
                                //{
                                //    strCustName = "-1";
                                //}
                                //else
                                //{
                                //    strCustName = txtCustomizePayName.Text;
                                //}
                                if (radCustomizePayNameYesNo.Checked)
                                {
                                        strCustName = txtCustomizePayName.Text;
                                    }
                                else
                                {
                                        strCustName = "-1";
                                    }


                                //if (radCustomizeIdNo.SelectedValue == "2")
                                //{
                                //    strCustIdno = "-1";
                                //}
                                //else
                                //{
                                //    strCustIdno = txtCustomizeIdNo.Text;
                                //}
                                if (radCustomizeIdNo.Checked)
                                {
                                    strCustIdno = txtCustomizeIdNo.Text;
                                }
                                else
                                {
                                    strCustIdno = "-1";
                                }



                                //if (radCustomizeSalaryForMonth.SelectedValue == "2")
                                //    {
                                //        strCustSalForMonth = "-1";
                                //    }
                                //    else
                                //    {
                                //        strCustSalForMonth = txtCustomizeSalary.Text;
                                //    }
                                if (radCustomizeSalaryForMonth.Checked == false)
                                {
                                    strCustSalForMonth = "-1";
                                }
                                else
                                {
                                    strCustSalForMonth = txtCustomizeSalary.Text;
                                }







                                //if (radCustomizePayYear.SelectedValue == "2")
                                //    {
                                //        strCustYear = "-1";
                                //    }
                                //    else
                                //    {
                                //        strCustYear = txtCustomizePayYear.Text;
                                //    }
                                if (radCustomizePayYear.Checked == false)
                                {
                                    strCustYear = "-1";
                                }
                                else
                                {
                                    strCustYear = txtCustomizePayYear.Text;
                                }





                                //if (radCustomizeEarnings.SelectedValue == "2")
                                    if (radCustomizeEarnings.Checked == false)
                                    {
                                        strCustEarnings = "-1";
                                    }
                                    else
                                    {
                                        strCustEarnings = txtCustomizeEarnings.Text;
                                    }


                                    //if (radCustomizeDeductions.SelectedValue == "2")
                                    if (radCustomizeDeductions.Checked == false)
                                    {
                                        strCustDeductions = "-1";
                                    }
                                    else
                                    {
                                        strCustDeductions = txtCustomizeDeductions.Text;
                                    }

                                    //if (radCustomizeTotalGross.SelectedValue == "2")
                                    if (radCustomizeTotalGross.Checked == false)
                                    {
                                        strCustTOTALGROSS = "-1";
                                    }
                                    else
                                    {
                                        strCustTOTALGROSS = txtCustomizeTotalGross.Text;
                                    }


                                    // jaspreet

                                    //if (radCustomizeCpfGross.SelectedValue == "2")
                                    if (radCustomizeCpfGross.Checked == false)
                                    {
                                        strCustCPFGROSS = "-1";
                                    }
                                    else
                                    {
                                        strCustCPFGROSS = txtCustomizeCpfGross.Text;
                                    }

                                    //if (radCustomizeEmployerCpf.SelectedValue == "2")
                                    if (radCustomizeEmployerCpf.Checked == false)
                                    {
                                        strCustEMPLOYERCPF = "-1";
                                    }
                                    else
                                    {
                                        //strCustEMPLOYERCPF = txtPayEMPLOYERCPF.Text;
                                        strCustEMPLOYERCPF = txtCustomizeEmployerCpf.Text;
                                    }

                                    //if (radCustomizeTotalDeduction.SelectedValue == "2")
                                    if (radCustomizeTotalDeduction.Checked == false)
                                    {
                                        strCustTOTALDEDUCTION = "-1";
                                    }
                                    else
                                    {
                                        strCustTOTALDEDUCTION = txtCustomizeTotalDeduction.Text;

                                    }

                                    //if (radCustomizeNetPayment.SelectedValue == "2")
                                    if (radCustomizeNetPayment.Checked == false)
                                    {
                                        strCustNETPAYMENT = "-1";
                                    }
                                    else
                                    {
                                        strCustNETPAYMENT = txtCustomizeNetPayment.Text;
                                    }

                                    //if (radCustomizeYearToDate.SelectedValue == "2")
                                    if (radCustomizeYearToDate.Checked == false)
                                    {
                                        strCustYEARTODATE = "-1";
                                    }
                                    else
                                    {
                                        strCustYEARTODATE = txtCustomizeYearToDate.Text;
                                    }

                                    //if (radCustomizeYearToDateEmployerCPF.SelectedValue == "2")
                                    if (radCustomizeYearToDateEmployerCPF.Checked == false)
                                    {
                                        strCustYEATODATEEMPLOYERCPF = "-1";
                                    }
                                    else
                                    {
                                        strCustYEATODATEEMPLOYERCPF = txtCustomizeYearToDateEmployerCPF.Text;
                                    }

                                    if (radCustomizeLogoManagement.SelectedValue == "1")
                                    {
                                        logomgtCust = 1;
                                    }

                                    if (radCustomizeLogoManagement.SelectedValue == "2")
                                    {
                                        logomgtCust = 2;
                                    }
                                    if (radCustomizeLogoManagement.SelectedValue == "3")
                                    {
                                        logomgtCust = 3;
                                    }

                                    if (radCustomizeLogoManagement.SelectedValue == "4")
                                    {
                                        logomgtCust = 4;
                                    }

                                    //if (radCustomizeDepartmentName.SelectedValue == "2")
                                    if (radCustomizeDepartmentName.Checked == false)
                                    {
                                        strCustDEPTNAME = "-1";
                                    }
                                    else
                                    {
                                        strCustDEPTNAME = txtCustomizeDepartmentName.Text;
                                    }

                                    //if (radCustomizeTrade.SelectedValue == "2")
                                    if (radCustomizeTrade.Checked == false)
                                    {
                                        strCustTRADE = "-1";
                                    }
                                    else
                                    {
                                        strCustTRADE = txtCustomizeTrade.Text;
                                    }


                                    //if (radCustomizeDesignation.SelectedValue == "2")
                                    if (radCustomizeDesignation.Checked == false)
                                    {
                                        strCustDESIGNATION = "-1";
                                    }
                                    else
                                    {
                                        strCustDESIGNATION = txtCustomizeDesignation.Text;
                                    }


                                    //if (radCustomizeDOB.SelectedValue == "2")
                                    if (radCustomizeDOB.Checked == false)
                                    {
                                        strCustDOB = "-1";
                                    }
                                    else
                                    {
                                        strCustDOB = txtCustomizeDOB.Text;
                                    }

                                    //if (radCustomizeTimecardNo.SelectedValue == "2")
                                    if (radCustomizeTimecardNo.Checked == false)
                                    {
                                        strCustTimeCardNo = "-1";
                                    }
                                    else
                                    {
                                        strCustTimeCardNo = txtCustomizeTimecardNo.Text;
                                    }

                                    //if (radCustomizeJoiningDate.SelectedValue == "2")
                                    if (radCustomizeJoiningDate.Checked == false)
                                    {
                                        strCustJoiningDate = "-1";
                                    }
                                    else
                                    {
                                        strCustJoiningDate = txtCustomizeJoiningDate.Text;
                                    }

                                    //if (radCustomizeTerminationDate.SelectedValue == "2")
                                    if (radCustomizeTerminationDate.Checked == false)
                                    {
                                        strCustTerminationDate = "-1";
                                    }
                                    else
                                    {
                                        strCustTerminationDate = txtCustomizeTerminationDate.Text;
                                    }


                                    //if (radCustomizeBusinessUnit.SelectedValue == "2")
                                    if (radCustomizeBusinessUnit.Checked == false)
                                    {
                                        strCustBusinessUnit = "-1";
                                    }
                                    else
                                    {
                                        strCustBusinessUnit = txtCustomizeBusinessUnit.Text;
                                    }

                                    //if (radCustomizePayslipPeriod.SelectedValue == "2")
                                    if (radCustomizePayslipPeriod.Checked == false)
                                    {
                                        strCustPayslipPeriod = "-1";
                                    }
                                    else
                                    {
                                        strCustPayslipPeriod = txtCustomizePayslipPeriod.Text;
                                    }

                                    //if (radCustomizeOvertimePeriod.SelectedValue == "2")
                                    if (radCustomizeOvertimePeriod.Checked == false)
                                    {
                                        strCustOvertimePeriod = "-1";
                                    }
                                    else
                                    {
                                        strCustOvertimePeriod = txtCustomizeOvertimePeriod.Text;
                                    }

                                    //if (radCustomizeTotalAdditions.SelectedValue == "2")
                                    if (radCustomizeTotalAdditions.Checked == false)
                                    {
                                        strCustTotalAdditions = "-1";
                                    }
                                    else
                                    {
                                        strCustTotalAdditions = txtCustomizeTotalAdditions.Text;
                                    }

                                    //if (radCustomizeDateOfPayment.SelectedValue == "2")
                                    if (radCustomizeDateOfPayment.Checked == false)
                                    {
                                        strCustDateOfPayment = "-1";
                                    }
                                    else
                                    {
                                        strCustDateOfPayment = txtCustomizeDateOfPayment.Text;
                                    }



                                    //if (radCustomizeModeOfPayment.SelectedValue == "2")
                                    if (radCustomizeModeOfPayment.Checked == false)
                                    {
                                        strCustModeOfPayment = "-1";
                                    }
                                    else
                                    {
                                        strCustModeOfPayment = txtCustomizeModeOfPayment.Text;
                                    }


                                    //if (radYearToDateEmployeeCPF.SelectedValue == "2")
                                    if (radYearToDateEmployeeCPF.Checked == false)
                                    {
                                        strCustYearToDateEmployeeCPF = "-1";
                                    }
                                    else
                                    {
                                        strCustYearToDateEmployeeCPF = txtYearToDateEmployeeCPF.Text;
                                    }

                                    //if (radCustomizeRemarks.SelectedValue == "2")
                                    if (radCustomizeRemarks.Checked == false)
                                    {
                                        strCustRemarks = "-1";
                                    }
                                    else
                                    {
                                        strCustRemarks = txtCustomizeRemarks.Text;
                                    }

                                    //if (radChequeNumber.SelectedValue == "2")
                                    if (radChequeNumber.Checked == false)
                                    {
                                        strCustChequeNumber = "-1";
                                    }
                                    else
                                    {
                                        strCustChequeNumber = txtChequeNumber.Text;
                                    }

                                    string strCustomizePayleaveDetails = "";
                                    string strCustomizeAddDetails = "";
                                    ////////////////////////////////////////////////////////////
                                    if (radCustomizePayLEAVEDETAILS.SelectedValue == "1")
                                    {
                                        strCustomizePayleaveDetails = "1";
                                    }
                                    if (radCustomizePayLEAVEDETAILS.SelectedValue == "2")
                                    {
                                        strCustomizePayleaveDetails = "2";
                                    }
                                    ////////////////////////////////////////////////////////////
                                    if (radCUSTOMIZEPayEARNINGDETAILS.SelectedValue == "1")
                                    {
                                        strCustomizeAddDetails = "1";
                                    }
                                    if (radCUSTOMIZEPayEARNINGDETAILS.SelectedValue == "2")
                                    {
                                        strCustomizeAddDetails = "2";
                                    }
                                    if (radCUSTOMIZEPayEARNINGDETAILS.SelectedValue == "3")
                                    {
                                        strCustomizeAddDetails = "3";
                                    }
                                    ////////////////////////////////////////////////////////////



                                    string strInsert = "INSERT INTO [Report_Settings]([PAYSLIPFORMAT],[NAME],[IDNO],[SALFORMONTH],[YEAR],[EARNINGS],[DEDUCTIONS],[TOTALGROSS],[CPFGROSS],[EMPLOYERCPF],[TOTALDEDUCTION]";
                                    strInsert = strInsert + ",[NETPAYMENT],[YEARTODATE],[YEATODATEEMPLOYERCPF],[COMPANYID],[LOGOMGT],[DEPTNAME],[TRADE],[DESIGNATION],[LEAVEDETAILS],[ADDITIONSDETAILS],[DOB],[TIMECARDNO],[JOININGDATE],[TERMINATIONDATE],[BUSINESSUNIT],[PAYSLIPPERIOD],[OVERTIMEPERIOD],[TOTALADDITIONS],[DATEOFPAYMENT],[MODEOFPAYMENT],[YEARTODATEEMPLOYEECPF],[REMARKS],[CHEQUENO])VALUES('" + cmbpayslipformat.SelectedValue + "','" + strCustName + "','" + strCustIdno + "','";
                                    strInsert = strInsert + strCustSalForMonth + "','" + strCustYear + "','" + strCustEarnings + "','" + strCustDeductions + "','" + strCustTOTALGROSS + "','" + strCustCPFGROSS + "','" + strCustEMPLOYERCPF + "','" + strCustTOTALDEDUCTION + "','" + strCustNETPAYMENT + "','";
                                    strInsert = strInsert + strCustYEARTODATE + "','" + strCustYEATODATEEMPLOYERCPF + "'," + compayid + "," + logomgtCust + ",'" + strCustDEPTNAME + "','" + strCustTRADE + "','" + strCustDESIGNATION + "'," + strCustomizePayleaveDetails + "," + strCustomizeAddDetails + ",'" + strCustDOB + "','" + strCustTimeCardNo + "','" + strCustJoiningDate + "','" + strCustTerminationDate + "','" + strCustBusinessUnit + "','" + strCustPayslipPeriod + "','" + strCustOvertimePeriod + "','" + strCustTotalAdditions + "','" + strCustDateOfPayment + "','" + strCustModeOfPayment + "','" + strCustYearToDateEmployeeCPF + "','" + strCustRemarks + "','" + strCustChequeNumber + "')";

                                    int data = DataAccess.ExecuteNonQuery(strInsert, null);
                                    }
                                else if (tblMOMItemized.Visible)
                                {
                                    int itemizelogmgt = -1;
                                    if (radItemizeLogo.SelectedValue != "2")
                                    {

                                        if (radItemizeLogoManagement.SelectedValue == "1")
                                        {
                                            itemizelogmgt = 1;
                                        }

                                        if (radItemizeLogoManagement.SelectedValue == "2")
                                        {
                                            itemizelogmgt = 2;
                                        }
                                        if (radItemizeLogoManagement.SelectedValue == "3")
                                        {
                                            itemizelogmgt = 3;
                                        }

                                        if (radItemizeLogoManagement.SelectedValue == "4")
                                        {
                                            itemizelogmgt = 4;
                                        }
                                    }
                                    else
                                    {
                                        itemizelogmgt = -1;
                                    }


                                    string strItemizePayleaveDetails = "-1";

                                    ////////////////////////////////////////////////////////////
                                    //if (radItemizeLogo.SelectedValue != "2")
                                    //{
                                    //    if (radItemizeLEAVEDETAILS.SelectedValue == "1")
                                    //    {
                                    //        strItemizePayleaveDetails = "1";
                                    //    }
                                    //    if (radItemizeLEAVEDETAILS.SelectedValue == "2")
                                    //    {
                                    //        strItemizePayleaveDetails = "2";
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    strItemizePayleaveDetails = "-1";
                                    //}
                                    string strInsert = "INSERT INTO [Report_Settings]([PAYSLIPFORMAT],[NAME],[IDNO],[SALFORMONTH],[YEAR],[EARNINGS],[DEDUCTIONS],[TOTALGROSS],[CPFGROSS],[EMPLOYERCPF],[TOTALDEDUCTION]";
                                    strInsert = strInsert + ",[NETPAYMENT],[YEARTODATE],[YEATODATEEMPLOYERCPF],[COMPANYID],[LOGOMGT],[DEPTNAME],[TRADE],[DESIGNATION],[LEAVEDETAILS],[ADDITIONSDETAILS],[DOB],[TIMECARDNO],[JOININGDATE],[TERMINATIONDATE],[BUSINESSUNIT],[PAYSLIPPERIOD],[OVERTIMEPERIOD],[TOTALADDITIONS],[DATEOFPAYMENT],[MODEOFPAYMENT],[YEARTODATEEMPLOYEECPF],[REMARKS],[CHEQUENO])VALUES('" + cmbpayslipformat.SelectedValue + "','" + string.Empty + "','" + string.Empty + "','";
                                    strInsert = strInsert + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','";
                                    strInsert = strInsert + string.Empty + "','" + string.Empty + "'," + compayid + "," + itemizelogmgt + ",'" + string.Empty + "','" + string.Empty + "','" + string.Empty + "'," + strItemizePayleaveDetails + ",'" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "')";



                                    //string strInsert = "INSERT INTO [Report_Settings]([PAYSLIPFORMAT],[COMPANYID],[LOGOMGT],[LEAVEDETAILS])VALUES('" + cmbpayslipformat.SelectedValue + "'," + compayid + "," + itemizelogmgt + "," + strItemizePayleaveDetails + ")";

                                    int data = DataAccess.ExecuteNonQuery(strInsert, null);

                                }

                            }
                        catch (Exception ex)
                        {
                            string ErrMsg = "Some error occured.";
                            lblerror.ForeColor = System.Drawing.Color.Red;
                            if (ex.Message.IndexOf("PRIMARY KEY constraint", 1) > 0)
                            {

                                ErrMsg = "Warning|Username already Exist, Please Enter some other Username";
                            }
                            _actionMessage = ErrMsg;
                            Session["actionMessage"] = _actionMessage;
                            // lblerror.Text = ErrMsg;
                        }
                    }
                }

                #region Add default rights for the default user
                try
                {
                    //string sqlq = "Declare @count int "
                    //            + " set @count = 0 "
                    //            + " DELETE FROM GroupRights "
                    //            + " Select @count=COUNT(groupid) from GroupRights where UPPER([groupid])  = 1 "
                    //            + " if (@count = 0) "
                    //            + " Begin"
                    //                + " Insert Into GroupRights "
                    //                + " Select UG.GroupID,UR.RightID From UserRights UR "
                    //                + " CROSS JOIN  UserGroups UG WHERE UG.GroupName='Super Admin' "
                    //            + " End";


                    #region new
                    //string sqlq = "Declare @count int "
                    //   + " set @count = 0 "
                    //   + "   Select  @count=COUNT(a.GroupID) from GroupRights a inner Join UserGroups b On a.GroupID=b.GroupID  where GroupName='Super Admin'  and company_id='" + compid + "'"
                    //   + " if (@count = 0) "
                    //   + " Begin"
                    //       + " Insert Into GroupRights "
                    //       + " Select UG.GroupID,UR.RightID From UserRights UR "
                    //       + " CROSS JOIN  UserGroups UG WHERE UG.GroupName='Super Admin' "
                    //   + " End";
                    #endregion


                    //DataAccess.ExecuteNonQuery(sqlq, null);
                }
                catch (Exception ee)
                {
                    // lblerror.Text = ee.Message.ToString();
                    _actionMessage = "Warning|Some error occured";
                    Session["actionMessage"] = _actionMessage;
                }
                #endregion

                Response.Redirect("ShowCompanies.aspx");
            }
            #endregion Insert

            #region update
            else
            {
                string Compcode = txtCompCode.Text.Trim();
                string CompName = txtCompName.Text.Trim();
                if (CompName != "" && Compcode != "")
                {

                    string CompPhone = txtCompPhone.Value;
                    string website = txtwebsite.Value;
                    string Compemail = txtCompemail.Value;
                    string Compcity = txtCompcity.Value;
                    string Compfax = txtCompfax.Value;
                    string Country = cmbCountry.SelectedValue;
                    string Compperson = txtCompperson.Value;
                    string Compstate = txtCompstate.Value;
                    string designation = txtdesign.Value;
                    string company_roc = txtcompany_roc.Text;
                    string company_type = drpcompany_type.SelectedItem.Value;
                    string compaddress = txtcompaddress.Text;
                    string auth_emai = txtauth_emai.Value;
                    string hrs_day = txthrs_day.Value;
                    string min_day = txtmin_day.Value;
                    //CPF Changes
                    string monthly_cpf_ceil = txtmonthly_cpf_ceil.Value;
                    string annual_cpf_ceil = txtannual_cpf_ceil.Value;
                    string varpayapproval = RdApproval.SelectedValue;
                    string no_work_days = cmbworkingdays1.SelectedValue; //Added by Sandi on 26/03/2014
                    if (rdoNo1.Checked == true) //Added by Senthil on 12/05/2015
                    {
                        cmbworkingdays1.Enabled = false;
                        no_work_days = cmbworkingdays.SelectedValue;
                    }
                    else
                    {
                        cmbworkingdays1.Enabled = true;
                        cmbworkingdays.Enabled = false;

                    }
                    string payslipformat = cmbpayslipformat.SelectedValue;
                    string email_sender = txtemailsender_address.Value;
                    string email_username = txtemailuser.Value;
                    //string email_replyaddress = txtemail_replyaddress.Value;
                    string email_replyaddress = Editortxtemail_replyaddress.Content;

                    //string email_replyname = txtemail_replyname.Value;
                    string email_replyname = Editortxtemail_replyname.Content;

                    //string email_leavedel = txtemail_leavedel.Value;
                    string email_leavedel = Editortxtemail_leavedel.Content;
                    string email_password = emailpwd.Text;

                    //r
                    //string email_sendername = txtemail_sendername.Value;
                    string email_sendername = EditorLevReq.Content;

                    string email_senderdomain = txtemailsender_domain.Value;
                    string email_smtpserver = txtsmtpserver.Value;
                    string email_smtpport = txtsmtpport.Value;
                    string timesheet = rdtimesheet.Value;
                    string tsremarks = rdtsremarks.Value;

                    string compaddress2 = txtcompaddress2.Text;
                    string postalcode = txtpostalcode.Text;
                    // string email_claim_sender_name = txtclaim_sendername.Value;
                    string email_claim_sender_name = Editortxtclaim_sendername.Content;

                    //string email_claim_reply_name = txtemailclaim_replyname.Value;
                    string email_claim_reply_name = Editortxtemailclaim_replyname.Content;

                    string sslrequired = ddlssl.SelectedValue;
                    string pwdrequired = cmbEPayPwd.SelectedValue;
                    string ccmail = txtccmail.Value;
                    string ccmailclaim = cmbccclaim.Value;
                    string ccmailleave = cmbccleave.Value;
                    string leaveroundoff = cmbLeaveRoundoff.SelectedValue;
                    string additionsroundoff = cmbAdditionsRoundoff.SelectedValue;
                    string deductionsroundoff = cmbDeductionsRoundoff.SelectedValue;
                    string netpayroundoff = cmbNetPayRoundoff.SelectedValue;
                    int payrolltype = Convert.ToInt16(cmbPayrollType.SelectedValue);
                    int projectassign = Convert.ToInt16(cmbAssignType.SelectedValue);

                    string SalaryGLCode = txtSalaryGL.Value;
                    string EmployeeCPFGLCode = txtEmpCPFGL.Value;
                    string EmployerCPFGLCode = txtEmpyCPFGL.Value;
                    string FundAmtGLCode = txtFundGL.Value;
                    string SDLAmtGLCode = txtSDLGL.Value;
                    string AccountGLCode = txtacccompGL.Value;
                    string UnpaidLeaGLCode = txtunpaidGL.Value;

                    string strPH = cmbPublicHoliday.SelectedValue.ToString();
                    string strSunday = cmbSunday.SelectedValue.ToString();
                    string strRosNa = cmbRosterNa.SelectedValue.ToString();

                    int ROund = Convert.ToInt32(cmbRound.SelectedValue.ToString());
                    int chk = 0;
                    if (chkFiFo.SelectedValue.ToString() == "FIFO")
                    {
                        chk = 1;
                    }

                    // Add by Su Mon
                    string OffDay1 = "";
                    string OffDay2 = "";
                    bool HalfDay1 = false;
                    bool UseLeaveCal = false;
                    string CustomizedCal = "default";
                    //Added by Sandi on 31/3/2014
                    int LeaveDayAhead;

                    if (rtxtLeaveDayAhead.Text == "")
                    {
                        LeaveDayAhead = 0;
                    }
                    else
                    {
                        LeaveDayAhead = Convert.ToInt32(rtxtLeaveDayAhead.Text);
                    }

                    bool IncludingCurrentDay;

                    if (chkIncludingCurrentDay.Checked == true)
                    {
                        IncludingCurrentDay = true;
                    }
                    else
                    {
                        IncludingCurrentDay = false;
                    }
                    //End Added

                    if (rdoNo.Checked == true)
                    {
                        if (rdoHide.Checked == true)// select fixed off days for all employees
                        {
                            CustomizedCal = "half";
                            OffDay1 = cmdOffDay1.SelectedValue.ToString();
                            OffDay2 = cmdOffDay2.SelectedValue.ToString();
                            if (cmbworkingdays.SelectedValue == "5.5")
                            {
                                if (OffDay1 == "")
                                {
                                   // ShowMessageBox("Please choose Off Day 1!");
                                    _actionMessage = "Warning|Please choose Off Day 1!";
                                    ViewState["actionMessage"] = _actionMessage;
                                    return;
                                }
                                if (OffDay2 == "")
                                {
                                    //ShowMessageBox("Please choose Off Day 2!");
                                    _actionMessage = "Warning|Please choose Off Day 2!";
                                    ViewState["actionMessage"] = _actionMessage;
                                    return;
                                }

                                HalfDay1 = true;
                            }
                            if (cmbworkingdays.SelectedValue == "6")
                            {
                                if (OffDay1 == "")
                                {
                                   // ShowMessageBox("Please choose Off Day 1!");
                                    _actionMessage = "Warning|Please choose Off Day 1!";
                                    ViewState["actionMessage"] = _actionMessage;
                                    return;
                                }
                                OffDay2 = "";
                            }

                            if (cmbworkingdays.SelectedValue == "5")
                            {
                                if (OffDay1 == "")
                                {
                                    //ShowMessageBox("Please choose Off Day 1!");
                                    _actionMessage = "Warning|Please choose Off Day 1!";
                                    ViewState["actionMessage"] = _actionMessage;
                                    return;
                                }
                                if (OffDay2 == "")
                                {
                                    //ShowMessageBox("Please choose Off Day 2!");
                                    _actionMessage = "Warning|Please choose Off Day 2!";
                                    ViewState["actionMessage"] = _actionMessage;
                                    return;
                                }
                            }
                        }
                        if (CustomizedCal == "h" || CustomizedCal == "half")
                        {
                            if (rdoLeaveCal.Checked == true)
                            {
                                UseLeaveCal = true;
                            }
                        }
                        if (rdoShow.Checked == true)
                        {
                            CustomizedCal = "full";
                            if (rdoLeaveCal.Checked == true)
                            {
                                UseLeaveCal = true;
                            }
                        }

                    }
                    // End added

                    Session["TimeSheetApproved"] = timesheet;
                    int i = 0;
                    SqlParameter[] parms = new SqlParameter[119];
                    parms[0] = new SqlParameter("@Company_Id", Utility.ToString(compid));
                    parms[1] = new SqlParameter("@Company_Code", Utility.ToString(Compcode));
                    parms[2] = new SqlParameter("@Company_name", Utility.ToString(CompName));
                    parms[3] = new SqlParameter("@phone", Utility.ToInteger(CompPhone));
                    parms[4] = new SqlParameter("@email", Utility.ToString(Compemail));
                    parms[5] = new SqlParameter("@website", Utility.ToString(website));

                    parms[6] = new SqlParameter("@city", Utility.ToString(Compcity));
                    parms[7] = new SqlParameter("@Fax", Utility.ToString(Compfax));
                    parms[8] = new SqlParameter("@country", Utility.ToInteger(Country));
                    parms[9] = new SqlParameter("@auth_person", Utility.ToString(Compperson));
                    parms[10] = new SqlParameter("@designation", designation);
                    parms[11] = new SqlParameter("@company_roc", company_roc);
                    parms[12] = new SqlParameter("@company_type", company_type);

                    parms[13] = new SqlParameter("@Address", Utility.ToString(compaddress));
                    parms[14] = new SqlParameter("@Auth_email", Utility.ToString(auth_emai));
                    //CPF Changes No Need to Do here
                    parms[15] = new SqlParameter("@monthly_cpf_ceil", Utility.ToInteger(monthly_cpf_ceil));

                    parms[16] = new SqlParameter("@annual_cpf_ceil", Utility.ToInteger(annual_cpf_ceil));
                    parms[17] = new SqlParameter("@payslip_format", Utility.ToString(payslipformat));

                    parms[18] = new SqlParameter("@Payroll_Approval", Utility.ToInteger(varpayapproval));
                    parms[19] = new SqlParameter("@no_work_days", Utility.ToDouble(no_work_days));
                    parms[20] = new SqlParameter("@day_hours", Utility.ToDouble(hrs_day));
                    parms[21] = new SqlParameter("@day_min", Utility.ToDouble(min_day));

                    parms[22] = new SqlParameter("@email_sender", Utility.ToString(email_sender));
                    parms[23] = new SqlParameter("@email_username", Utility.ToString(email_username));
                    parms[24] = new SqlParameter("@email_reply_address", Utility.ToString(email_replyaddress));
                    parms[25] = new SqlParameter("@email_reply_name", Utility.ToString(email_replyname));
                    parms[26] = new SqlParameter("@email_leavedel", Utility.ToString(email_leavedel));
                    parms[27] = new SqlParameter("@email_password", Utility.ToString(email_password));
                    parms[28] = new SqlParameter("@email_sender_name", Utility.ToString(email_sendername));
                    parms[29] = new SqlParameter("@email_sender_domain", Utility.ToString(email_senderdomain));
                    parms[30] = new SqlParameter("@email_SMTP_server", Utility.ToString(email_smtpserver));
                    parms[31] = new SqlParameter("@email_SMTP_port", Utility.ToString(email_smtpport));
                    parms[32] = new SqlParameter("@timesheet_approve", Utility.ToInteger(timesheet));
                    parms[33] = new SqlParameter("@state", Utility.ToString(txtCompstate.Value));


                    parms[34] = new SqlParameter("@address2", compaddress2);
                    parms[35] = new SqlParameter("@postal_code", postalcode);

                    parms[36] = new SqlParameter("@email_leavealert", cmbemailleave.SelectedValue);
                    parms[37] = new SqlParameter("@email_payalert", cmbemailpay.SelectedValue);

                    //parms[38] = new SqlParameter("@epayslip", cmbEmailPaySlip.SelectedValue);
                    parms[38] = new SqlParameter("@epayslip", cmbEmailPaySlip.Checked == true ? "Y" : "N");

                    parms[39] = new SqlParameter("@leave_model", Utility.ToInteger(cmbLeaveModel.SelectedValue));
                    parms[40] = new SqlParameter("@email_claim_sender_name", Utility.ToString(email_claim_sender_name));
                    parms[41] = new SqlParameter("@email_claim_reply_name", Utility.ToString(email_claim_reply_name));
                    parms[42] = new SqlParameter("@email_claimalert", cmbclaim.SelectedValue);
                    parms[43] = new SqlParameter("@sslrequired", sslrequired);
                    parms[44] = new SqlParameter("@pwdrequired", pwdrequired);
                    parms[45] = new SqlParameter("@ccmail", ccmail);
                    parms[46] = new SqlParameter("@ccclaimalert", ccmailclaim);
                    parms[47] = new SqlParameter("@ccleavealert", ccmailleave);

                    parms[48] = new SqlParameter("@leaveroundoff", leaveroundoff);
                    parms[49] = new SqlParameter("@additionsroundoff", additionsroundoff);
                    parms[50] = new SqlParameter("@deductionsroundoff", deductionsroundoff);
                    parms[51] = new SqlParameter("@netpayroundoff", netpayroundoff);
                    parms[52] = new SqlParameter("@payrolltype", payrolltype);
                    parms[53] = new SqlParameter("@tsremarks", Utility.ToInteger(tsremarks));
                    parms[54] = new SqlParameter("@projectassign", Utility.ToInteger(projectassign));

                    parms[55] = new SqlParameter("@SalaryGLCode", SalaryGLCode);
                    parms[56] = new SqlParameter("@EmployeeCPFGLCode", EmployeeCPFGLCode);
                    parms[57] = new SqlParameter("@EmployerCPFGLCode", EmployerCPFGLCode);
                    parms[58] = new SqlParameter("@FundAmtGLCode", FundAmtGLCode);
                    parms[59] = new SqlParameter("@SDLAmtGLCode", SDLAmtGLCode);
                    parms[60] = new SqlParameter("@AccountGLCode", AccountGLCode);
                    parms[61] = new SqlParameter("@UnpaidLeaGLCode", UnpaidLeaGLCode);

                    parms[62] = new SqlParameter("@PublicHoliday", strPH);
                    parms[63] = new SqlParameter("@Sunday", strSunday);
                    parms[64] = new SqlParameter("@RosterNa", strRosNa);
                    parms[65] = new SqlParameter("@Round", ROund);
                    parms[66] = new SqlParameter("@FIFO", chk);

                    parms[67] = new SqlParameter("@isMaster", cmbIsMaster.SelectedValue);
                    parms[68] = new SqlParameter("@isMasterEmpTemp", cmbtempEmp.SelectedValue);



                    int retVal = 0;
                    try
                    {
                        #region TimeSheet (D)Settings
                        bool SendEmail;

                        if (cbxEmailAlert.SelectedValue == "Yes")
                        {
                            SendEmail = true;
                        }
                        else
                        {
                            SendEmail = false;
                        }


                        if (drpEmpProc1.Visible)
                        {

                            if (drpEmpProc1.SelectedValue == "Employee")
                            {
                                EmpProc = false;
                                ProceserEmail = "";
                            }
                            else if (drpEmpProc1.SelectedValue == "Processer")
                            {
                                EmpProc = true;
                                ProceserEmail = txtProcesserEmail.Text;
                            }
                        }
                        else
                        {
                            drpEmpProc1.Visible = false;
                            EmpProc = false;
                            ProceserEmail = "";
                        }


                        parms[69] = new SqlParameter("@ProcessEmail", ProceserEmail);


                        //@ProcessEmai

                        if (radAdvanceTs.SelectedValue == "No")
                        {
                            parms[70] = new SqlParameter("@AdvTs", txtMinutes.Text);
                        }
                        else
                        {
                            parms[70] = new SqlParameter("@AdvTs", -1);
                        }

                        if (radLFort.SelectedDate != null)
                        {
                            //parms[i++] = new SqlParameter("@LeaveFFDate", Convert.ToDateTime(radLFort.SelectedDate));
                            parms[71] = new SqlParameter("@LeaveFFDate", Convert.ToDateTime(radLFort.SelectedDate));
                        }
                        else
                        {
                            parms[71] = new SqlParameter("@LeaveFFDate", System.DBNull.Value);
                        }

                        parms[72] = new SqlParameter("@WorkFlowID", rdWorkFlow.SelectedValue);

                        if (rdWorkFlow.SelectedValue.ToString() == "-1")
                        {
                            parms[73] = new SqlParameter("@WFEMP", System.DBNull.Value);
                            parms[74] = new SqlParameter("@WFLEAVE", System.DBNull.Value);
                            parms[75] = new SqlParameter("@WFCLAIM", System.DBNull.Value);
                            parms[76] = new SqlParameter("@WFPAY", System.DBNull.Value);
                            parms[77] = new SqlParameter("@WFReport", System.DBNull.Value);
                            parms[78] = new SqlParameter("@WFTimeSheet", System.DBNull.Value);
                        }
                        else
                        {
                            if (chkWF.Items[0].Selected == true)
                            {
                                parms[73] = new SqlParameter("@WFEMP", 1);
                            }
                            else
                            {
                                parms[73] = new SqlParameter("@WFEMP", System.DBNull.Value);
                            }

                            if (chkWF.Items[1].Selected == true)
                            {
                                parms[74] = new SqlParameter("@WFLEAVE", 1);
                            }
                            else
                            {
                                parms[74] = new SqlParameter("@WFLEAVE", System.DBNull.Value);
                            }

                            if (chkWF.Items[2].Selected == true)
                            {
                                parms[75] = new SqlParameter("@WFCLAIM", 1);
                            }
                            else
                            {
                                parms[75] = new SqlParameter("@WFCLAIM", System.DBNull.Value);
                            }


                            //r
                            if (rdWorkFlow.SelectedValue.ToString() == "2")
                            {
                                parms[76] = new SqlParameter("@WFPAY", 1);
                            }
                            else
                            {
                                parms[76] = new SqlParameter("@WFPAY", System.DBNull.Value);
                            }



                            //if (chkWF.Items[3].Selected == true)
                            //{
                            //    parms[76] = new SqlParameter("@WFPAY", 1);
                            //}
                            //else
                            //{
                            //    parms[76] = new SqlParameter("@WFPAY", System.DBNull.Value);
                            //}

                            if (chkWF.Items[4].Selected == true)
                            {
                                parms[77] = new SqlParameter("@WFReport", 1);
                            }
                            else
                            {
                                parms[77] = new SqlParameter("@WFReport", System.DBNull.Value);
                            }

                            if (chkWF.Items[5].Selected == true)
                            {
                                parms[78] = new SqlParameter("@WFTimeSheet", 1);
                            }
                            else
                            {
                                parms[78] = new SqlParameter("@WFTimeSheet", System.DBNull.Value);
                            }

                        }



                        if (radListPayrollApp.SelectedValue == "1")
                        {
                            parms[79] = new SqlParameter("@AppTSProcess", 1);
                        }
                        else
                        {
                            parms[79] = new SqlParameter("@AppTSProcess", SqlDbType.Int);
                            parms[79].Value = 0;
                        }

                        if (radListLeaveApp.SelectedValue == "1")
                        {
                            parms[80] = new SqlParameter("@AppLeaveProcess", 1);
                        }
                        else
                        {
                            //parms[80] = new SqlParameter("@AppLeaveProcess", 0);
                            parms[80] = new SqlParameter("@AppLeaveProcess", SqlDbType.Int);
                            parms[80].Value = 0;
                        }

                        if (radListClaimApp.SelectedValue == "1")
                        {
                            parms[81] = new SqlParameter("@AppClaimsProcess", 1);
                        }
                        else
                        {
                            //parms[81] = new SqlParameter("@AppClaimsProcess", 0);
                            parms[81] = new SqlParameter("@AppClaimsProcess", SqlDbType.Int);
                            parms[81].Value = 0;
                        }

                        if (chkWL.Checked)
                        {
                            parms[82] = new SqlParameter("@FOWL", SqlDbType.Int);
                            parms[82].Value = 1;
                        }
                        else
                        {
                            parms[82] = new SqlParameter("@FOWL", SqlDbType.Int);
                            parms[82].Value = 0;
                        }

                        if (chkLeave.Checked == true)
                        {
                            parms[83] = new SqlParameter("@UnpaidLAmount", SqlDbType.Int);
                            parms[83].Value = 1;
                        }
                        else
                        {
                            parms[83] = new SqlParameter("@UnpaidLAmount", SqlDbType.Int);
                            parms[83].Value = 0;
                        }


                        parms[84] = new SqlParameter("@CurrencyID", Convert.ToInt32(drpCurrency.SelectedValue));

                        if (chkClaims.Checked)
                        {
                            parms[85] = new SqlParameter("@ClaimsCash", SqlDbType.Int);
                            parms[85].Value = 2;
                        }
                        else
                        {
                            parms[85] = new SqlParameter("@ClaimsCash", SqlDbType.Int);
                            parms[85].Value = 1;
                        }
                        parms[86] = new SqlParameter("@ConversionOpt", Convert.ToInt32(drpConv.SelectedValue));
                        parms[87] = new SqlParameter("@MultiCurr", Convert.ToInt32(rdMultiCurr.SelectedValue));

                        string remakYn = "A";
                        string normalHrBt = "A";
                        string overTimeBT = "A";

                        if (chkBoxTs.Items[0].Selected)
                        {
                            remakYn = chkBoxTs.Items[0].Value;
                        }

                        if (chkBoxTs.Items[1].Selected)
                        {
                            normalHrBt = chkBoxTs.Items[1].Value;
                        }

                        if (chkBoxTs.Items[2].Selected)
                        {
                            overTimeBT = chkBoxTs.Items[2].Value;
                        }

                        parms[88] = new SqlParameter("@RemarksYN", remakYn);
                        parms[89] = new SqlParameter("@NormalHrBT", normalHrBt);
                        parms[90] = new SqlParameter("@OverTHrBT", overTimeBT);

                        int convval = 0;

                        if (chkAdClaims.Checked)
                            convval = 1;
                        parms[91] = new SqlParameter("@AdvClaims", convval);

                        // Added by Su Mon 
                        parms[92] = new SqlParameter("@OffDay1", OffDay1);
                        parms[93] = new SqlParameter("@OffDay2", OffDay2);
                        parms[94] = new SqlParameter("@HalfDay1", HalfDay1);
                        parms[95] = new SqlParameter("@UseLeaveCal", UseLeaveCal);
                        parms[96] = new SqlParameter("@CustomizedCal", CustomizedCal);
                        // End added
                        parms[97] = new SqlParameter("@LeaveDayAhead", LeaveDayAhead); //Added by Sandi on 31/3/2014
                        parms[98] = new SqlParameter("@IncludingCurrentDay", IncludingCurrentDay); //Added by Sandi on 31/3/2014

                        //kumar added for roster type


                        if (this.rostertype.SelectedValue == "2")
                        {
                            parms[99] = new SqlParameter("@RosterType", 2);
                        }
                        else
                        {
                            parms[99] = new SqlParameter("@RosterType", 1);
                        }

                        if (this.overtimemode.SelectedValue == "False")
                        {
                            parms[100] = new SqlParameter("@WeekRosterOtMode", "False");
                        }
                        else
                        {
                            parms[100] = new SqlParameter("@WeekRosterOtMode", "True");
                        }

                        if (this.loginWithOutComany.SelectedValue == "1")
                        {
                            parms[101] = new SqlParameter("@loginWithOutComany", "1");
                        }
                        else
                        {
                            parms[101] = new SqlParameter("@loginWithOutComany", "0");
                        }

                        bool mobileTimeSheet = false;

                        if (this.mobilescancode.Checked == true)
                            mobileTimeSheet = true;

                        bool _showroster = false;

                        if (this.showroster.Checked == true)
                            _showroster = true;



                        parms[102] = new SqlParameter("@mobileTimeSheet", mobileTimeSheet);
                        if (rdbGrouping.SelectedValue == "1")
                        {
                            parms[103] = new SqlParameter("@GroupManage", 1);
                        }
                        else
                        {
                            parms[103] = new SqlParameter("@GroupManage", SqlDbType.Int);
                            parms[103].Value = 0;
                        }


                        parms[104] = new SqlParameter("@showroster", _showroster);

                        int showAcNo = 1;
                        if (this.ShowBankAcNo.Checked == false)
                            showAcNo = 0;
                        parms[105] = new SqlParameter("@showBankAcNo", showAcNo);



                        int enbleautoreminder = 0;
                        if (this.autoreminder.SelectedValue == "1")
                            enbleautoreminder = 1;

                        parms[106] = new SqlParameter("@EnableAutoreminder", enbleautoreminder);
                        //add by kumar for incomplemonth manval rate
                        int incompleatemonthManvalRate = 0;
                        if (this.incommanuvalRate.Checked == true)
                            incompleatemonthManvalRate = 1;

                        parms[107] = new SqlParameter("@incompleatemonthManvalRate", incompleatemonthManvalRate);

                        if (chkApprovalDate.Checked) //murugan
                        {
                            parms[108] = new SqlParameter("@isApproveDate", "1");
                        }
                        else
                        {
                            parms[108] = new SqlParameter("@isApproveDate", "0");
                        }

                        parms[109] = new SqlParameter("@ccTimeSheet", txtcctimesheet.Value.ToString());
                        if (chk_LGOT.Checked)
                        {
                            parms[110] = new SqlParameter("@OTseparate", true);
                        }
                        else
                        {
                            parms[110] = new SqlParameter("@OTseparate", false);

                        }
                        string strot = txtOTGL.Value.ToString();
                        parms[111] = new SqlParameter("@OTGLCode", txtOTGL.Value.ToString());
                        parms[112] = new SqlParameter("@SDLPayableGL", txtSDLpayable.Value.ToString());
                        parms[113] = new SqlParameter("@SalaryPayableGL", txtSalarypayable.Value.ToString());
                        parms[114] = new SqlParameter("@CPFPayableGL", txtCPFpayable.Value.ToString());
                        parms[115] = new SqlParameter("@TSrequired", radListTSApp.SelectedValue.ToString());

                        int ClaimTotalForPaySlip = 0;
                        if (this.ClaimTotalForPaySlip.Checked == true)
                            ClaimTotalForPaySlip = 1;
                        parms[116] = new SqlParameter("@ClaimTotalForPaySlip", ClaimTotalForPaySlip);

                        if (chkWF.Items[6].Selected == true)
                        {
                            parms[117] = new SqlParameter("@WFAppraisal", "1");
                        }
                        else
                        {
                            parms[117] = new SqlParameter("@WFAppraisal", "0");
                        }
                       
                        parms[118] = new SqlParameter("@APPrequired", radListALAp.SelectedValue.ToString());

                        //Added by Jammu Office
                        #region Audit                                  
                        int Compid = Convert.ToInt32(compid);
                        using (AuditContext _Auditcontext = new AuditContext())
                        {
                            var comp = _Auditcontext.Companies.Where(c => c.CompanyId == Compid).FirstOrDefault();
                            //_Auditcontext.Entry(comp).State = EntityState.Modified;
                            //var list= _Auditcontext.ChangeTracker.Entries().Where(p => p.State == EntityState.Modified).ToList();
                            var NewValues = new AuditLibrary.Company()
                            {
                                CompanyId = Compid,
                                CompanyName = Utility.ToString(CompName),
                                CompanyCode = Utility.ToString(Compcode),
                                Phone = Utility.ToInteger(CompPhone),
                                Email = Utility.ToString(Compemail),
                                Website = Utility.ToString(website),
                                City = Utility.ToString(Compcity),
                                Fax = Utility.ToString(Compfax),
                                Country = Utility.ToInteger(Country),
                                AuthPerson = Utility.ToString(Compperson),
                                Designation = designation,
                                CompanyRoc = company_roc,
                                CompanyType = company_type,
                                Address = Utility.ToString(compaddress),
                                AuthEmail = Utility.ToString(auth_emai),
                                //CPF Changes No Need to Do here
                                MonthlyCpfCeil = Utility.ToInteger(monthly_cpf_ceil),
                                AnnualCpfCeil = Utility.ToInteger(annual_cpf_ceil),
                                PayslipFormat = Utility.ToString(payslipformat),
                                PayrollApproval = Utility.ToInteger(varpayapproval),
                                NoWorkDays = Utility.ToDouble(no_work_days),
                                DayHours = Utility.ToDouble(hrs_day),
                                DayMinute = Utility.ToDouble(min_day),
                                EmailSender = Utility.ToString(email_sender),
                                EmailUsername = Utility.ToString(email_username),
                                EmailReplyAddress = Utility.ToString(email_replyaddress),
                                EmailReplyName = Utility.ToString(email_replyname),
                                EmailLeaveDelete = Utility.ToString(email_leavedel),
                                EmailPassword = Utility.ToString(email_password),
                                EmailSenderName = Utility.ToString(email_sendername),
                                EmailSenderDomain = Utility.ToString(email_senderdomain),
                                EmailSmtpServer = Utility.ToString(email_smtpserver),
                                EmailSmtpPort = Utility.ToString(email_smtpport),
                                TimesheetApprove = Utility.ToInteger(timesheet),
                                State = Utility.ToString(txtCompstate.Value),
                                Address2 = compaddress2,
                                PostalCode = postalcode,
                                EmailLeavealert = cmbemailleave.SelectedValue,
                                EmailPayalert = cmbemailpay.SelectedValue,
                                Epayslip = cmbEmailPaySlip.Checked == true ? "Y" : "N",
                                LeaveModel = Utility.ToInteger(cmbLeaveModel.SelectedValue),
                                EmailClaimSenderName = Utility.ToString(email_claim_sender_name),
                                EmailClaimReplyName = Utility.ToString(email_claim_reply_name),
                                EmailClaimalert = cmbclaim.SelectedValue,
                                Sslrequired = sslrequired,
                                Pwdrequired = pwdrequired,
                                Ccmail = ccmail,
                                CcalertClaims = ccmailclaim,
                                CcalertLeaves = ccmailleave,
                                Basicrnd = Utility.ToInteger(leaveroundoff),
                                Additionsrnd = Utility.ToInteger(additionsroundoff),
                                Deductionsrnd = Utility.ToInteger(deductionsroundoff),
                                Netpayrnd = Utility.ToInteger(netpayroundoff),
                                Payrolltype = payrolltype,
                                // IsTsRemarks = !string.IsNullOrEmpty(tsremarks),
                                SalaryGlCode = SalaryGLCode,
                                EmployeeCpfglCode = EmployeeCPFGLCode,
                                EmployerCpfglCode = EmployerCPFGLCode,
                                FundAmtGlCode = FundAmtGLCode,
                                SdlAmtGlCode = SDLAmtGLCode,
                                AccountGlCode = AccountGLCode,
                                UnpaidLeaGlCode = UnpaidLeaGLCode,
                                TsPublicH = strPH,
                                Sunday = strSunday,
                                NoRoster = strRosNa,
                                Rounding = ROund,
                                Fifo = Utility.ToString(chk),
                                //EmpProcessor = !string.IsNullOrEmpty(ProceserEmail),
                                //  LeaveFfDate = Convert.ToDateTime(parms[71].Value)
                                //IsMaster = cmbIsMaster.SelectedValue,
                                //IsMasterEmpTemp = cmbtempEmp.SelectedValue

                            };
                            var AuditRepository = new AuditRepository();

                            AuditRepository.CreateAuditTrail(AuditActionType.Update, LoginEmpcode, Compid, comp, NewValues);
                        }

                        #endregion

                        string sSQL = "sp_comp_update";


                        string sqlUpdate = "UPDATE [dbo].[Company] SET [SendEmail] = '" + SendEmail + "' ,[EmpProcessor] = '" + EmpProc + "'  ,[ProcessEmail] = '" + ProceserEmail + "' WHERE Company_Id='" + Utility.ToString(compid) + "'";
                        DataAccess.FetchRS(CommandType.Text, sqlUpdate, null);

                        #endregion

                        retVal = DataAccess.ExecuteStoreProc(sSQL, parms);

                        //
                        SqlDataReader dr_check = DataAccess.ExecuteReader(CommandType.Text, "select * from Company_Alias where [Company_id]='" + Utility.ToString(compid) + "' ", null);
                        if (dr_check.HasRows)
                        {
                            //Update Alias Name
                            string UpdateAliasname = "UPDATE [Company_Alias]  SET [AliasName] = '" + txtAlias.Text.ToString() + "'  WHERE [Company_id]='" + Utility.ToString(compid) + "'";
                            DataAccess.ExecuteNonQuery(UpdateAliasname, null);
                        }
                        else
                        {
                            //Inserting Alias Name
                            string InsertAliasname1 = "INSERT INTO [Company_Alias]([Company_id],[AliasName]) VALUES  ('" + Utility.ToString(compid) + "','" + txtAlias.Text.ToString() + "' )";
                            DataAccess.ExecuteNonQuery(InsertAliasname1, null);
                            //
                        }
                        //


                        //Update changes for CompayPaySlip Format
                        int compayid = 0;
                        compayid = Utility.ToInteger(compid);
                        if (tblPaySlipSetup1.Visible)
                        {
                            string strDeletePaslipFormat = "DELETE FROM Report_Settings WHERE COMPANYID=" + compayid + "  AND PAYSLIPFORMAT=" + cmbpayslipformat.SelectedValue;
                            int rtval1 = DataAccess.ExecuteNonQuery(strDeletePaslipFormat, null);


                            string strName = "";
                            string strIdno = "";
                            string strSalForMonth = "";
                            string strYear = "";
                            string strEarnings = "";
                            string strDeductions = "";
                            string strTOTALGROSS = "";
                            string strCPFGROSS = "";
                            string strEMPLOYERCPF = "";
                            string strTOTALDEDUCTION = "";
                            string strNETPAYMENT = "";
                            string strYEARTODATE = "";
                            string strYEATODATEEMPLOYERCPF = "";

                            int logomgt = -1;
                            string strDEPTNAME = "";
                            string strTRADE = "";
                            string strDESIGNATION = "";
                            //By Jammu Office
                            //if (radPayNameYesNo.SelectedValue == "2")
                            if (radPayNameYesNo.Checked == false)
                                {
                                strName = "-1";
                            }
                            else
                            {
                                strName = txtPayName.Text;
                            }

                            //if (radPayIDNOYesNo.SelectedValue == "2")
                                if (radPayIDNOYesNo.Checked == false)
                                {
                                strIdno = "-1";
                            }
                            else
                            {
                                strIdno = txtPayIDNO.Text;
                            }

                            //if (radPaySalMonthYesNo.SelectedValue == "2")
                                if (radPaySalMonthYesNo.Checked == false)
                                {
                                strSalForMonth = "-1";
                            }
                            else
                            {
                                strSalForMonth = txtPaySALMONTH.Text;
                            }

                            //if (radPayYear.SelectedValue == "2")
                            if (radPayYear.Checked == false)
                            {
                                strYear = "-1";
                            }
                            else
                            {
                                strYear = txtPayYEAR.Text;
                            }

                            //if (radPayEarnings.SelectedValue == "2")
                            if (radPayEarnings.Checked == false)
                            {
                                strEarnings = "-1";
                            }
                            else
                            {
                                strEarnings = txtPayEARNINGS.Text;
                            }

                            //if (radPayDeductions.SelectedValue == "2")
                            if (radPayDeductions.Checked == false)
                            {
                                strDeductions = "-1";
                            }
                            else
                            {
                                strDeductions = txtPayDEDUCTIONS.Text;
                            }

                            //if (radPayTotalGross.SelectedValue == "2")
                            if (radPayTotalGross.Checked == false)
                            {
                                strTOTALGROSS = "-1";
                            }
                            else
                            {
                                strTOTALGROSS = txtPayTOTALGROSS.Text;
                            }

                            //if (radPayCpfGross.SelectedValue == "2")
                            if (radPayCpfGross.Checked == false)
                            {
                                strCPFGROSS = "-1";
                            }
                            else
                            {
                                strCPFGROSS = txtPayCpfGross.Text;
                            }

                            //if (radPayEmployerCpf.SelectedValue == "2")
                            if (radPayEmployerCpf.Checked == false)
                            {
                                strEMPLOYERCPF = "-1";
                            }
                            else
                            {
                                strEMPLOYERCPF = txtPayEMPLOYERCPF.Text;
                            }

                            //if (radPayTotalDeduction.SelectedValue == "2")
                            if (radPayTotalDeduction.Checked == false)
                            {
                                strTOTALDEDUCTION = "-1";
                            }
                            else
                            {
                                strTOTALDEDUCTION = txtPayTOTALDEDUCTION.Text;

                            }

                            //if (radPayNETPAYMENT.SelectedValue == "2")
                            if (radPayNETPAYMENT.Checked == false)
                            {
                                strNETPAYMENT = "-1";
                            }
                            else
                            {
                                strNETPAYMENT = txtPayNETPAYMENT.Text;
                            }

                            //if (radPayYEARTODATE.SelectedValue == "2")
                            if (radPayYEARTODATE.Checked == false)
                            {
                                strYEARTODATE = "-1";
                            }
                            else
                            {
                                strYEARTODATE = txtPayYEARTODATE.Text;
                            }

                            //if (radPayYEATODATEEMPLOYERCPF.SelectedValue == "2")
                            if (radPayYEATODATEEMPLOYERCPF.Checked == false)
                            {
                                strYEATODATEEMPLOYERCPF = "-1";
                            }
                            else
                            {
                                strYEATODATEEMPLOYERCPF = txtPayYEATODATEEMPLOYERCPF.Text;
                            }

                            if (radPayLOGOMGT.SelectedValue == "1")
                            {
                                logomgt = 1;
                            }

                            if (radPayLOGOMGT.SelectedValue == "2")
                            {
                                logomgt = 2;
                            }
                            if (radPayLOGOMGT.SelectedValue == "3")
                            {
                                logomgt = 3;
                            }

                            if (radPayLOGOMGT.SelectedValue == "4")
                            {
                                logomgt = 4;
                            }

                            //if (radPayDEPTNAME.SelectedValue == "2")
                            if (radPayDEPTNAME.Checked == false)
                            {
                                strDEPTNAME = "-1";
                            }
                            else
                            {
                                strDEPTNAME = txtPayDepartmentName.Text;
                            }

                            //if (radPayTrade.SelectedValue == "2")
                            if (radPayTrade.Checked == false)
                            {
                                strTRADE = "-1";
                            }
                            else
                            {
                                strTRADE = txtPayTrade.Text;
                            }


                            //if (radPayDesignation.SelectedValue == "2")
                            if (radPayDesignation.Checked == false)
                            {
                                strDESIGNATION = "-1";
                            }
                            else
                            {
                                strDESIGNATION = txtPayDesignation.Text;
                            }


                            string strPayleaveDetails = "";
                            string strAddDetails = "";
                            ////////////////////////////////////////////////////////////
                            if (radPayLEAVEDETAILS.SelectedValue == "1")
                            {
                                strPayleaveDetails = "1";
                            }
                            if (radPayLEAVEDETAILS.SelectedValue == "2")
                            {
                                strPayleaveDetails = "2";
                            }
                            ////////////////////////////////////////////////////////////
                            if (radPayEARNINGDETAILS.SelectedValue == "1")
                            {
                                strAddDetails = "1";
                            }
                            if (radPayEARNINGDETAILS.SelectedValue == "2")
                            {
                                strAddDetails = "2";
                            }
                            if (radPayEARNINGDETAILS.SelectedValue == "3")
                            {
                                strAddDetails = "3";
                            }
                            ////////////////////////////////////////////////////////////



                            string strInsert = "INSERT INTO [Report_Settings]([PAYSLIPFORMAT],[NAME],[IDNO],[SALFORMONTH],[YEAR],[EARNINGS],[DEDUCTIONS],[TOTALGROSS],[CPFGROSS],[EMPLOYERCPF],[TOTALDEDUCTION]";
                            strInsert = strInsert + ",[NETPAYMENT],[YEARTODATE],[YEATODATEEMPLOYERCPF],[COMPANYID],[LOGOMGT],[DEPTNAME],[TRADE],[DESIGNATION],[LEAVEDETAILS],[ADDITIONSDETAILS])VALUES('" + cmbpayslipformat.SelectedValue + "','" + strName + "','" + strIdno + "','";
                            strInsert = strInsert + strSalForMonth + "','" + strYear + "','" + strEarnings + "','" + strDeductions + "','" + strTOTALGROSS + "','" + strCPFGROSS + "','" + strEMPLOYERCPF + "','" + strTOTALDEDUCTION + "','" + strNETPAYMENT + "','";
                            strInsert = strInsert + strYEARTODATE + "','" + strYEATODATEEMPLOYERCPF + "'," + compayid + "," + logomgt + ",'" + strDEPTNAME + "','" + strTRADE + "','" + strDESIGNATION + "'," + strPayleaveDetails + "," + strAddDetails + ")";


                            //string strInsert = "INSERT INTO [Report_Settings]([PAYSLIPFORMAT],[NAME],[IDNO],[SALFORMONTH],[YEAR],[EARNINGS],[DEDUCTIONS],[TOTALGROSS],[CPFGROSS],[EMPLOYERCPF],[TOTALDEDUCTION]";
                            //strInsert = strInsert + ",[NETPAYMENT],[YEARTODATE],[YEATODATEEMPLOYERCPF],[COMPANYID],[LOGOMGT],[DEPTNAME],[TRADE],[DESIGNATION])VALUES('" + cmbpayslipformat.SelectedValue + "','" + strName + "','" + strIdno + "','";
                            //strInsert = strInsert + strSalForMonth + "','" + strYear + "','" + strEarnings + "','" + strDeductions + "','" + strTOTALGROSS + "','" + strCPFGROSS + "','" + strEMPLOYERCPF + "','" + strTOTALDEDUCTION + "','" + strNETPAYMENT + "','";
                            //strInsert = strInsert + strYEARTODATE + "','" + strYEATODATEEMPLOYERCPF + "'," + compayid + "," + logomgt + ",'" + strDEPTNAME + "','" + strTRADE + "','" + strDESIGNATION + "')";

                            int data = DataAccess.ExecuteNonQuery(strInsert, null);
                        }
                        else if (tblPayslipSetup2.Visible) //Senthil-Added-Payslip formate on 27/10/2015
                        {
                            string strDeletePaslipFormat = "DELETE FROM Report_Settings WHERE COMPANYID=" + compayid + "  AND PAYSLIPFORMAT=" + cmbpayslipformat.SelectedValue;
                            int rtvalCust1 = DataAccess.ExecuteNonQuery(strDeletePaslipFormat, null);


                            string strCustName = "";
                            string strCustIdno = "";
                            string strCustSalForMonth = "";
                            string strCustYear = "";
                            string strCustEarnings = "";
                            string strCustDeductions = "";
                            string strCustTOTALGROSS = "";
                            string strCustCPFGROSS = "";
                            string strCustEMPLOYERCPF = txtPayEMPLOYERCPF.Text;
                            string strCustTOTALDEDUCTION = "";
                            string strCustNETPAYMENT = "";
                            string strCustYEARTODATE = "";
                            string strCustYEATODATEEMPLOYERCPF = "";

                            int logomgtCust = -1;
                            string strCustDEPTNAME = "";
                            string strCustTRADE = "";
                            string strCustDESIGNATION = "";
                            string strCustDOB = "";
                            string strCustTimeCardNo = "";
                            string strCustJoiningDate = "";
                            string strCustTerminationDate = "";
                            string strCustBusinessUnit = "";
                            string strCustPayslipPeriod = "";
                            string strCustTotalAdditions = "";
                            string strCustDateOfPayment = "";
                            string strCustModeOfPayment = "";
                            string strCustYearToDateEmployeeCPF = "";
                            string strCustRemarks = "";
                            string strCustOvertimePeriod = "";
                            string strCustChequeNumber = "";

                            //By Jammu Office
                            //if (radCustomizePayNameYesNo.SelectedValue == "2")
                            //{
                            //    strCustName = "-1";
                            //}
                            //else
                            //{
                            //    strCustName = txtCustomizePayName.Text;
                            //}
                            if (radCustomizePayNameYesNo.Checked)
                                {
                                    strCustName = txtCustomizePayName.Text;
                                }
                                else
                                {
                                    strCustName = "-1";
                                }



                            //    if (radCustomizeIdNo.SelectedValue == "2")
                            //{
                            //    strCustIdno = "-1";
                            //}
                            //else
                            //{
                            //    strCustIdno = txtCustomizeIdNo.Text;
                            //}

                            if (radCustomizeIdNo.Checked)
                            {
                                 strCustIdno = txtCustomizeIdNo.Text;
                            }
                            else
                            {
                                strCustIdno = "-1";
                            }





                            //if (radCustomizeSalaryForMonth.SelectedValue == "2")
                            //{
                            //    strCustSalForMonth = "-1";
                            //}
                            //else
                            //{
                            //    strCustSalForMonth = txtCustomizeSalary.Text;
                            //}
                            if (radCustomizeSalaryForMonth.Checked == false)
                            {
                                strCustSalForMonth = "-1";
                            }
                            else
                            {
                                strCustSalForMonth = txtCustomizeSalary.Text;
                            }



                            //if (radCustomizePayYear.SelectedValue == "2")
                            //{
                            //    strCustYear = "-1";
                            //}
                            //else
                            //{
                            //    strCustYear = txtCustomizePayYear.Text;
                            //}
                            if (radCustomizePayYear.Checked == false)
                            {
                                strCustYear = "-1";
                            }
                            else
                            {
                                strCustYear = txtCustomizePayYear.Text;
                            }



                            //if (radCustomizeEarnings.SelectedValue == "2")
                                if (radCustomizeEarnings.Checked == false)
                                {
                                strCustEarnings = "-1";
                            }
                            else
                            {
                                strCustEarnings = txtCustomizeEarnings.Text;
                            }

                            //if (radCustomizeDeductions.SelectedValue == "2")
                            if (radCustomizeDeductions.Checked == false)
                            {
                                strCustDeductions = "-1";
                            }
                            else
                            {
                                strCustDeductions = txtCustomizeDeductions.Text;
                            }

                            //if (radCustomizeTotalGross.SelectedValue == "2")
                                if (radCustomizeTotalGross.Checked == false)
                                {
                                strCustTOTALGROSS = "-1";
                            }
                            else
                            {
                                strCustTOTALGROSS = txtCustomizeTotalGross.Text;
                            }

                            //jaspreet2

                            //if (radCustomizeCpfGross.SelectedValue == "2")
                                if (radCustomizeCpfGross.Checked == false)
                                {
                                strCustCPFGROSS = "-1";
                            }
                            else
                            {
                                strCustCPFGROSS = txtCustomizeCpfGross.Text;

                            }

                            //if (radCustomizeEmployerCpf.SelectedValue == "2")
                                if (radCustomizeEmployerCpf.Checked == false)
                                {
                                strCustEMPLOYERCPF = "-1";
                            }
                            else
                            {
                                // strCustEMPLOYERCPF = txtPayEMPLOYERCPF.Text;
                                strCustEMPLOYERCPF = txtCustomizeEmployerCpf.Text;
                            }

                            //if (radCustomizeTotalDeduction.SelectedValue == "2")
                                if (radCustomizeTotalDeduction.Checked == false)
                                {
                                strCustTOTALDEDUCTION = "-1";
                            }
                            else
                            {
                                strCustTOTALDEDUCTION = txtCustomizeTotalDeduction.Text;

                            }

                            //if (radCustomizeNetPayment.SelectedValue == "2")
                                if (radCustomizeNetPayment.Checked == false)
                                {
                                strCustNETPAYMENT = "-1";
                            }
                            else
                            {
                                strCustNETPAYMENT = txtCustomizeNetPayment.Text;
                            }

                            //if (radCustomizeYearToDate.SelectedValue == "2")
                                if (radCustomizeYearToDate.Checked == false)
                                {
                                strCustYEARTODATE = "-1";
                            }
                            else
                            {
                                strCustYEARTODATE = txtCustomizeYearToDate.Text;
                            }

                            //if (radCustomizeYearToDateEmployerCPF.SelectedValue == "2")
                                if (radCustomizeYearToDateEmployerCPF.Checked == false)
                                {
                                strCustYEATODATEEMPLOYERCPF = "-1";
                            }
                            else
                            {
                                strCustYEATODATEEMPLOYERCPF = txtCustomizeYearToDateEmployerCPF.Text;
                            }

                            if (radCustomizeLogoManagement.SelectedValue == "1")
                            {
                                logomgtCust = 1;
                            }

                            if (radCustomizeLogoManagement.SelectedValue == "2")
                            {
                                logomgtCust = 2;
                            }
                            if (radCustomizeLogoManagement.SelectedValue == "3")
                            {
                                logomgtCust = 3;
                            }

                            if (radCustomizeLogoManagement.SelectedValue == "4")
                            {
                                logomgtCust = 4;
                            }

                            //if (radCustomizeDepartmentName.SelectedValue == "2")
                                if (radCustomizeDepartmentName.Checked == false)
                                {
                                strCustDEPTNAME = "-1";
                            }
                            else
                            {
                                strCustDEPTNAME = txtCustomizeDepartmentName.Text;
                            }

                            //if (radCustomizeTrade.SelectedValue == "2")
                                if (radCustomizeTrade.Checked == false)
                                {
                                strCustTRADE = "-1";
                            }
                            else
                            {
                                strCustTRADE = txtCustomizeTrade.Text;
                            }


                            //if (radCustomizeDesignation.SelectedValue == "2")
                                if (radCustomizeDesignation.Checked == false)
                                {
                                strCustDESIGNATION = "-1";
                            }
                            else
                            {
                                strCustDESIGNATION = txtCustomizeDesignation.Text;
                            }


                            //if (radCustomizeDOB.SelectedValue == "2")
                                if (radCustomizeDOB.Checked == false)
                                {
                                strCustDOB = "-1";
                            }
                            else
                            {
                                strCustDOB = txtCustomizeDOB.Text;
                            }
                            //if (radCustomizeTimecardNo.SelectedValue == "2")
                                if (radCustomizeTimecardNo.Checked == false)
                                {
                                strCustTimeCardNo = "-1";
                            }
                            else
                            {
                                strCustTimeCardNo = txtCustomizeTimecardNo.Text;
                            }

                            //if (radCustomizeJoiningDate.SelectedValue == "2")
                                if (radCustomizeJoiningDate.Checked == false)
                                {
                                strCustJoiningDate = "-1";
                            }
                            else
                            {
                                strCustJoiningDate = txtCustomizeJoiningDate.Text;
                            }

                            //if (radCustomizeTerminationDate.SelectedValue == "2")
                                if (radCustomizeTerminationDate.Checked == false)
                                {
                                strCustTerminationDate = "-1";
                            }
                            else
                            {
                                strCustTerminationDate = txtCustomizeTerminationDate.Text;
                            }


                            //if (radCustomizeBusinessUnit.SelectedValue == "2")
                                if (radCustomizeBusinessUnit.Checked == false)
                                {
                                strCustBusinessUnit = "-1";
                            }
                            else
                            {
                                strCustBusinessUnit = txtCustomizeBusinessUnit.Text;
                            }

                            //if (radCustomizePayslipPeriod.SelectedValue == "2")
                                if (radCustomizePayslipPeriod.Checked == false)
                                {
                                strCustPayslipPeriod = "-1";
                            }
                            else
                            {
                                strCustPayslipPeriod = txtCustomizePayslipPeriod.Text;
                            }

                            //if (radCustomizeOvertimePeriod.SelectedValue == "2")
                                if (radCustomizeOvertimePeriod.Checked == false)
                                {
                                strCustOvertimePeriod = "-1";
                            }
                            else
                            {
                                strCustOvertimePeriod = txtCustomizeOvertimePeriod.Text;
                            }


                            //if (radCustomizeTotalAdditions.SelectedValue == "2")
                                if (radCustomizeTotalAdditions.Checked == false)
                                {
                                strCustTotalAdditions = "-1";
                            }
                            else
                            {
                                strCustTotalAdditions = txtCustomizeTotalAdditions.Text;
                            }

                            //if (radCustomizeDateOfPayment.SelectedValue == "2")
                                if (radCustomizeDateOfPayment.Checked == false)
                                {
                                strCustDateOfPayment = "-1";
                            }
                            else
                            {
                                strCustDateOfPayment = txtCustomizeDateOfPayment.Text;
                            }



                            //if (radCustomizeModeOfPayment.SelectedValue == "2")
                                if (radCustomizeModeOfPayment.Checked == false)
                                {
                                strCustModeOfPayment = "-1";
                            }
                            else
                            {
                                strCustModeOfPayment = txtCustomizeModeOfPayment.Text;
                            }


                            //if (radYearToDateEmployeeCPF.SelectedValue == "2")
                                if (radYearToDateEmployeeCPF.Checked == false)
                                {
                                strCustYearToDateEmployeeCPF = "-1";
                            }
                            else
                            {
                                strCustYearToDateEmployeeCPF = txtYearToDateEmployeeCPF.Text;
                            }

                            //if (radCustomizeRemarks.SelectedValue == "2")
                                if (radCustomizeRemarks.Checked == false)
                                {
                                strCustRemarks = "-1";
                            }
                            else
                            {
                                strCustRemarks = txtCustomizeRemarks.Text;
                            }

                            //if (radChequeNumber.SelectedValue == "2")
                                if (radChequeNumber.Checked == false)
                                {
                                strCustChequeNumber = "-1";
                            }
                            else
                            {
                                strCustChequeNumber = txtChequeNumber.Text;
                            }
                            string strCustomizePayleaveDetails = "";
                            string strCustomizeAddDetails = "";
                            ////////////////////////////////////////////////////////////
                            if (radCustomizePayLEAVEDETAILS.SelectedValue == "1")
                            {
                                strCustomizePayleaveDetails = "1";
                            }
                            if (radCustomizePayLEAVEDETAILS.SelectedValue == "2")
                            {
                                strCustomizePayleaveDetails = "2";
                            }
                            ////////////////////////////////////////////////////////////
                            if (radCUSTOMIZEPayEARNINGDETAILS.SelectedValue == "1")
                            {
                                strCustomizeAddDetails = "1";
                            }
                            if (radCUSTOMIZEPayEARNINGDETAILS.SelectedValue == "2")
                            {
                                strCustomizeAddDetails = "2";
                            }
                            if (radCUSTOMIZEPayEARNINGDETAILS.SelectedValue == "3")
                            {
                                strCustomizeAddDetails = "3";
                            }
                            ////////////////////////////////////////////////////////////



                            string strInsert = "INSERT INTO [Report_Settings]([PAYSLIPFORMAT],[NAME],[IDNO],[SALFORMONTH],[YEAR],[EARNINGS],[DEDUCTIONS],[TOTALGROSS],[CPFGROSS],[EMPLOYERCPF],[TOTALDEDUCTION]";
                            strInsert = strInsert + ",[NETPAYMENT],[YEARTODATE],[YEATODATEEMPLOYERCPF],[COMPANYID],[LOGOMGT],[DEPTNAME],[TRADE],[DESIGNATION],[LEAVEDETAILS],[ADDITIONSDETAILS],[DOB],[TIMECARDNO],[JOININGDATE],[TERMINATIONDATE],[BUSINESSUNIT],[PAYSLIPPERIOD],[OVERTIMEPERIOD],[TOTALADDITIONS],[DATEOFPAYMENT],[MODEOFPAYMENT],[YEARTODATEEMPLOYEECPF],[REMARKS],[CHEQUENO])VALUES('" + cmbpayslipformat.SelectedValue + "','" + strCustName + "','" + strCustIdno + "','";
                            strInsert = strInsert + strCustSalForMonth + "','" + strCustYear + "','" + strCustEarnings + "','" + strCustDeductions + "','" + strCustTOTALGROSS + "','" + strCustCPFGROSS + "','" + strCustEMPLOYERCPF + "','" + strCustTOTALDEDUCTION + "','" + strCustNETPAYMENT + "','";
                            strInsert = strInsert + strCustYEARTODATE + "','" + strCustYEATODATEEMPLOYERCPF + "'," + compayid + "," + logomgtCust + ",'" + strCustDEPTNAME + "','" + strCustTRADE + "','" + strCustDESIGNATION + "'," + strCustomizePayleaveDetails + "," + strCustomizeAddDetails + ",'" + strCustDOB + "','" + strCustTimeCardNo + "','" + strCustJoiningDate + "','" + strCustTerminationDate + "','" + strCustBusinessUnit + "','" + strCustPayslipPeriod + "','" + strCustOvertimePeriod + "','" + strCustTotalAdditions + "','" + strCustDateOfPayment + "','" + strCustModeOfPayment + "','" + strCustYearToDateEmployeeCPF + "','" + strCustRemarks + "','" + strCustChequeNumber + "')";


                            int data = DataAccess.ExecuteNonQuery(strInsert, null);
                        }
                        else if (tblMOMItemized.Visible)
                        {
                            string strDeletePaslipFormat = "DELETE FROM Report_Settings WHERE COMPANYID=" + compayid + "  AND PAYSLIPFORMAT=" + cmbpayslipformat.SelectedValue;
                            int rtvalCust1 = DataAccess.ExecuteNonQuery(strDeletePaslipFormat, null);

                            int itemizelogomgt = -1;
                            if (radItemizeLogo.SelectedValue != "2")
                            {

                                if (radItemizeLogoManagement.SelectedValue == "1")
                                {
                                    itemizelogomgt = 1;
                                }

                                if (radItemizeLogoManagement.SelectedValue == "2")
                                {
                                    itemizelogomgt = 2;
                                }
                                if (radItemizeLogoManagement.SelectedValue == "3")
                                {
                                    itemizelogomgt = 3;
                                }

                                if (radItemizeLogoManagement.SelectedValue == "4")
                                {
                                    itemizelogomgt = 4;
                                }
                            }
                            else
                            {
                                itemizelogomgt = -1;
                            }


                            string strItemizePayleaveDetails = "-1";

                            ////////////////////////////////////////////////////////////
                            //if (radItemizeLogo.SelectedValue != "2")
                            //{
                            //    if (radItemizeLEAVEDETAILS.SelectedValue == "1")
                            //    {
                            //        strItemizePayleaveDetails = "1";
                            //    }
                            //    if (radItemizeLEAVEDETAILS.SelectedValue == "2")
                            //    {
                            //        strItemizePayleaveDetails = "2";
                            //    }
                            //}
                            //else
                            //{
                            //    strItemizePayleaveDetails = "-1";
                            //}
                            string strInsert = "INSERT INTO [Report_Settings]([PAYSLIPFORMAT],[NAME],[IDNO],[SALFORMONTH],[YEAR],[EARNINGS],[DEDUCTIONS],[TOTALGROSS],[CPFGROSS],[EMPLOYERCPF],[TOTALDEDUCTION]";
                            strInsert = strInsert + ",[NETPAYMENT],[YEARTODATE],[YEATODATEEMPLOYERCPF],[COMPANYID],[LOGOMGT],[DEPTNAME],[TRADE],[DESIGNATION],[LEAVEDETAILS],[ADDITIONSDETAILS],[DOB],[TIMECARDNO],[JOININGDATE],[TERMINATIONDATE],[BUSINESSUNIT],[PAYSLIPPERIOD],[OVERTIMEPERIOD],[TOTALADDITIONS],[DATEOFPAYMENT],[MODEOFPAYMENT],[YEARTODATEEMPLOYEECPF],[REMARKS],[CHEQUENO])VALUES('" + cmbpayslipformat.SelectedValue + "','" + string.Empty + "','" + string.Empty + "','";
                            strInsert = strInsert + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','";
                            strInsert = strInsert + string.Empty + "','" + string.Empty + "'," + compayid + "," + itemizelogomgt + ",'" + string.Empty + "','" + string.Empty + "','" + string.Empty + "'," + strItemizePayleaveDetails + ",'" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "','" + string.Empty + "')";


                            // string strInsert = "INSERT INTO [Report_Settings]([PAYSLIPFORMAT],[COMPANYID],[LOGOMGT],[LEAVEDETAILS])VALUES('" + cmbpayslipformat.SelectedValue + "'," + compayid + "," + itemizelogomgt + "," + strItemizePayleaveDetails + ")";

                            int data = DataAccess.ExecuteNonQuery(strInsert, null);

                        }



                        //Insert New Row in database

                        if (retVal >= 1)
                        {
                            Utility.GetLoginOKCompRunDB(compid, Session["Username"].ToString());

                            lblerror.ForeColor = System.Drawing.Color.Green;
                            // lblerror.Text = "Information updated successfully.";
                            _actionMessage = "Success|Information updated successfully.";
                            Session["actionMessage"] = _actionMessage;
                        }
                    }
                    catch (Exception ex)
                    {
                        string ErrMsg = "Some error occured.";
                        lblerror.ForeColor = System.Drawing.Color.Red;
                        //lblerror.Text = ErrMsg;
                        _actionMessage = "Warning|"+ErrMsg;
                        Session["actionMessage"] = _actionMessage;
                    }
                }

                Response.Redirect("ShowCompanies.aspx");
            }
            #endregion update

        }

        protected void btnUpdateLeave_Click(object sender, EventArgs e)
        {
            try
            {
                string OffDay1 = "";
                string OffDay2 = "";
                bool HalfDay1 = false;
                bool UseLeaveCal = false;
                string CustomizedCal = "default";

                if (rdoNo.Checked == true)
                {
                    if (rdoHide.Checked == true)// select fixed off days for all employees
                    {
                        CustomizedCal = "half";
                        OffDay1 = cmdOffDay1.SelectedValue.ToString();
                        OffDay2 = cmdOffDay2.SelectedValue.ToString();
                        if (cmbworkingdays.SelectedValue == "5.5")
                        {
                            HalfDay1 = true;
                        }
                        if (cmbworkingdays.SelectedValue == "6")
                        {
                            OffDay2 = "";
                        }
                    }
                    if (rdoShow.Checked == true)
                    {
                        CustomizedCal = "full";
                        if (rdoLeaveCal.Checked == true)
                        {
                            UseLeaveCal = true;
                        }
                    }

                }

                string strSQL = "";
                if (CustomizedCal == "h" || CustomizedCal == "half")
                {
                    try
                    {
                        DataSet dsEmployee = new DataSet();
                        int emp_code;
                        strSQL = "Select emp_code from employee where company_id=" + compid + ";";
                        dsEmployee = DataAccess.FetchRS(CommandType.Text, strSQL, null);

                        foreach (DataRow dr in dsEmployee.Tables[0].Rows)
                        {
                            emp_code = int.Parse(dr["emp_code"].ToString());
                            strSQL = "Delete from emp_off_fixed where emp_code=" + emp_code + ";";

                            strSQL = strSQL + "Insert into emp_off_fixed values(" + emp_code + "," + OffDay1 + "," + OffDay2 + ",'" + HalfDay1 + "'); ";
                            try
                            {
                                DataAccess.ExecuteNonQuery(strSQL, null);
                            }
                            catch (Exception ex) { }

                        }

                        strSQL = "Update employee set wdays_per_week=" + cmbworkingdays.SelectedValue + ",CustomizedCal='half' where company_id=" + compid + "";
                        DataAccess.ExecuteNonQuery(strSQL, null);

                    }
                    catch (Exception ex) { }

                }
                if (CustomizedCal == "f" || CustomizedCal == "full")
                {
                    strSQL = "Update employee set wdays_per_week=" + cmbworkingdays.SelectedValue + ",CustomizedCal='full' where company_id=" + compid + "";
                    DataAccess.ExecuteNonQuery(strSQL, null);
                }
                if (CustomizedCal == "d" || CustomizedCal == "default")
                {
                    strSQL = "Update employee set wdays_per_week=" + cmbworkingdays.SelectedValue + ",CustomizedCal='default' where company_id=" + compid + "";
                    DataAccess.ExecuteNonQuery(strSQL, null);
                }
                // End added
            }
            catch (Exception ex) { }
        }

        #region (d)Setting
        //checking - if check box is checked then show the dropdown to send Email
        protected void CboxSendEmail_CheckedChanged(object sender, EventArgs e)
        {
            lblEmail.Visible = false;
            if (cbxEmailAlert.SelectedValue == "Yes")
            {
                drpEmpProc1.Visible = true;
                drpEmpProc1.ClearSelection();
                //drpEmpProc1.SelectedValue = "Employee";
                //drpEmpProc1.SelectedItem.Text = "Employee";
                lbldrpEmpProc.Visible = true;
            }
            else
            {
                lbldrpEmpProc.Visible = false;
                drpEmpProc1.Visible = false;
                txtProcesserEmail.Visible = false;
            }
        }

        //IF Processer is Selected then show the textbox to get the Email address
        protected void drpEmpProc_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (drpEmpProc1.SelectedValue == "Processer")
            {
                lblEmail.Visible = true;
                txtProcesserEmail.Visible = true;
            }
            else
            {
                lblEmail.Visible = false;
                txtProcesserEmail.Visible = false;
            }
        }

        protected void radAdvanceTs_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (radAdvanceTs.SelectedValue == "No")
            {
                Label1.Visible = true;
                txtMinutes.Visible = true;
            }
            else
            {
                Label1.Visible = false;
                txtMinutes.Visible = false;
            }
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

        #endregion


    }
}




