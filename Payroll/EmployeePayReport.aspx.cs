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

namespace SMEPayroll.Payroll
{
    public partial class EmployeePayReport : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        DateTime dtPRdate1;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Session.LCID = 2057;
            //if (Utility.ToString(Session["Username"]) == "")
            //    Response.Redirect("../SessionExpire.aspx");

            tbl1.Style.Add("display", "table");
            tbl2.Style.Add("display", "none");

            DataSet monthDs = new DataSet();
            string strsql = "sp_GetPayrollMonth";// 0,2009,2
            SqlParameter[] params1 = new SqlParameter[3];
            params1[0] = new SqlParameter("@ROWID", "0");
            params1[1] = new SqlParameter("@YEARS", 0);
            //params1[2] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
            params1[2] = new SqlParameter("@PAYTYPE", 0);
            monthDs = DataAccess.ExecuteSPDataSet(strsql, params1);
            string monthstr = "";
            DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + Request.QueryString["qsMonth"]);
            foreach (DataRow drnew in drResults)
            {
                monthstr = drnew["Month"].ToString();
            }
            int MonthInt = Convert.ToInt32(monthstr);
            double dblDeductionWOUnpaid = 0;
            double dblWorkDaysInRoll = 0;
            double dblActWorkDaysInRoll = 0;
            double dblUnpaidLeavesTotal = 0;
            double dblUnpaidLeaves = 0;
            double dblUnpaidLeavesPR = 0;
            double dayswork = 0;
            string strdailybasicrate = "";
            string strpaymode = "";
            string strempcpfrow = "";
            string strempycpfrow = "";
            string agegroup = "";
            string stragegroup = "";
            double dblage = 0;
            string pryears = "";
            string empcpftype = "";
            double dblCPFGross = 0;
            string calcCPF = "";
            string CPFempPerc = "";
            string CPFempyPerc = "";
            string CompanyID = Utility.ToString(Session["Compid"]);
            int Month = Convert.ToInt32(monthstr);
            int Year = Convert.ToInt32(Request.QueryString["qsYear"]);
            string EmpId = Convert.ToString(Request.QueryString["qsEmpID"].ToString());
            int i = 0;
            string sSQL = "sp_GeneratePayRollAdv";
            string sSQLextData = "";
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            SqlParameter[] parms = new SqlParameter[10];
            parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(CompanyID));
            parms[i++] = new SqlParameter("@month", Utility.ToInteger(Request.QueryString["qsMonth"]));
            parms[i++] = new SqlParameter("@year", Utility.ToInteger(Year));
            parms[i++] = new SqlParameter("@stdatemonth", Request.QueryString["st"]);
            parms[i++] = new SqlParameter("@endatemonth", Request.QueryString["en"]);
            
            parms[i++] = new SqlParameter("@stdatesubmonth", Session["PaySubStartDay"].ToString());
            parms[i++] = new SqlParameter("@endatesubmonth", Session["PaySubEndDay"].ToString());
            System.Globalization.DateTimeFormatInfo dtMonth = new System.Globalization.DateTimeFormatInfo();
            parms[i++] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"].ToString()));
            parms[i++] = new SqlParameter("@EmpPassID", EmpId);
            parms[i++] = new SqlParameter("@monthidintbl", Request.QueryString["monthintbl"]);
            //parms[i++] = new SqlParameter("@IsDateCalculation", '1');
            lblPaySlip.Text = dtMonth.GetMonthName(Month) + " " + Year.ToString();

            if (Session["PaySubStartDay"].ToString() == "1" && Session["PaySubEndDay"].ToString() == "15")
            {
                lblPaySlip.Text = dtMonth.GetMonthName(Month) + " First Half " + Year.ToString();
            }
            if (Session["PaySubStartDay"].ToString() == "16")
            {
                lblPaySlip.Text = dtMonth.GetMonthName(Month) + " Second Half " + Year.ToString();
            }
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL, parms);

            DataSet dsTest = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);

            while (dr.Read())
            {

                //dblWorkDaysInRoll = Utility.ToDouble(dr["wrkgdaysinsubpay"].ToString());

                //kumar replaced 
                dblWorkDaysInRoll = Utility.ToDouble(dr["WrkgDaysInRoll"].ToString());
                dblActWorkDaysInRoll = Utility.ToDouble(dr["ActWrkgDaysSpan"].ToString());

                dblUnpaidLeavesTotal = Utility.ToDouble(dr["UnpaidLeaves"].ToString());
                dblUnpaidLeaves = Utility.ToDouble(dr["UnpaidFullDay"].ToString()) + Utility.ToDouble(dr["UnpaidHalfDay"].ToString());
                dblUnpaidLeavesPR = Utility.ToDouble(dr["UnpaidFullDayPR"].ToString()) + Utility.ToDouble(dr["UnpaidHalfDayPR"].ToString());

                if (dr["EmployeeCPFRow"] == DBNull.Value || dr["EmployeeCPFRow"].ToString() == "0")
                {
                    strempcpfrow = Utility.ToString(dr["CPFEmployeePerc"].ToString());
                }
                else
                {
                    strempcpfrow = Utility.ToString(dr["EmployeeCPFRow"].ToString());
                }
                if (dr["EmployerCPFRow"] == DBNull.Value || dr["EmployerCPFRow"].ToString() == "0")
                {
                    strempycpfrow = Utility.ToString(dr["CPFEmployerPerc"].ToString());
                }
                else
                {
                    strempycpfrow = Utility.ToString(dr["EmployerCPFRow"].ToString());
                }
                agegroup = Utility.ToString(dr["Age_Group"].ToString());
                dblage = Utility.ToDouble(dr["Age"].ToString());

                double years = Math.Floor(dblage);
                double days = dblage - years;
                days = Math.Floor(days * 365);

                lbName.Text = Utility.ToString(dr["FullName"].ToString()) + " is " + years + " years" + " & " +  days + " days.";
                lblEmpGroup.Text = Utility.ToString(dr["GroupName"].ToString());
                lblWorkingDays.Text = Utility.ToString(dr["WorkingDaysInWeek"].ToString());

                if (dr["PayType"].ToString().Trim() == "H" && dr["Daily_rate_mode"].ToString().Trim() == "A")
                {
                    strdailybasicrate = dr["BasicDayRate"].ToString();
                }
                else if ((dr["PayType"].ToString().Trim() == "H" && dr["Daily_rate_mode"].ToString().Trim() == "M") || dr["PayType"].ToString().Trim() == "D" && dr["Daily_rate_mode"].ToString().Trim() == "M")
                {
                    strdailybasicrate = dr["Daily_Rate"].ToString();
                }

                switch (dr["Emp_Type"].ToString())
                {
                    case "SC":
                        lblEmpType.Text = "Singapore Citizen";
                        break;
                    case "SPR":
                        lblEmpType.Text = "Singapore Permanent Resident";
                        break;
                    case "SDPR":
                        lblEmpType.Text = "Singapore Permanent Resident";
                        break;
                    case "WP":
                        lblEmpType.Text = "Work Permit";
                        break;
                    case "EP":
                        lblEmpType.Text = "Employment Pass";
                        break;
                    case "DP":
                        lblEmpType.Text = "Dependant Pass";
                        break;
                    case "SP":
                        lblEmpType.Text = "S Pass";
                        break;
                    default:
                        lblEmpType.Text = "-";
                        break;
                }
                lblCPFApp.Text = Utility.ToString(dr["CPF"].ToString());
                lblDayRate.Text = Utility.ToString(dr["BasicDayRate"].ToString());


                if (dr["PayType"].ToString().Trim() == "D")
                {
                    lblDayRate.Text = dr["Daily_Rate"].ToString();
                }


                //if (lblTotalUnpaid.Text.ToString() != "" && lblUnpaid.Text.ToString() != "" && dr["paytype"].ToString().Trim() == "M" && dr["daily_rate_mode"].ToString().Trim() == "M")
                //{
                //    dayrate = Convert.ToDecimal(lblTotalUnpaid.Text) / Convert.ToDecimal(lblUnpaid.Text);
                //    lblDayRatetbl2.Text = dayrate.ToString();
                //}
                //else
                //{
                //    lblDayRatetbl2.Text = lblDayRate.Text;
                //}


                if (dr["DeptName"] != DBNull.Value)
                {
                    lblDept.Text = Utility.ToString(dr["DeptName"].ToString());
                }
                lblHrRate.Text = Utility.ToString(dr["Hourly_Rate"].ToString());
                strpaymode = Utility.ToString(dr["Pay_Mode"].ToString());
                lblOTEnt.Text = Utility.ToString(dr["OT"].ToString());
                DateTime dtJoin = DateTime.Parse(dr["Join_Date"].ToString(), format);
                lblJoinDate.Text = dtJoin.ToString("dd/MM/yyyy");
                lblActualBasic.Text = Utility.ToDouble(dr["ActualBasic"].ToString()).ToString("#0.00");
                lblOT1rate.Text = Utility.ToString(dr["OT1Rate"].ToString()) + " (" + Utility.ToString(dr["Hourly_Rate"].ToString()) + "x" + Utility.ToString(dr["OT1SysRate"].ToString()) + ")";
                DateTime dtTerm =new DateTime();
                if (dr["Term_Date"] != DBNull.Value)
                {
                    dtTerm = DateTime.Parse(dr["Term_Date"].ToString(), format);
                    lblTermDate.Text = dtTerm.ToString("dd/MM/yyyy");
                }
                if (dr["PR_Date"] != DBNull.Value)
                {
                    DateTime dtPRdate = DateTime.Parse(dr["PR_Date"].ToString(), format);
                    lblPRDate.Text = dtPRdate.ToString("dd/MM/yyyy");
                    dtPRdate1 =dtPRdate; 
                }
                lblPayMode.Text = Utility.ToString(dr["Pay_Mode"].ToString());
                lblOT2rate.Text = Utility.ToString(dr["OT2Rate"].ToString()) + " (" + Utility.ToString(dr["Hourly_Rate"].ToString()) + "x" + Utility.ToString(dr["OT2SysRate"].ToString()) + ")";
                calcCPF = Utility.ToString(dr["CalculateCPF"].ToString());
                pryears = Utility.ToString(dr["PRAge"].ToString());
                empcpftype = Utility.ToString(dr["EmpCPFType"].ToString());
                dblCPFGross = Utility.ToDouble(dr["CPFGross1"].ToString());
                CPFempPerc = Utility.ToString(dr["CPFEmployeePerc"].ToString());
                CPFempyPerc = Utility.ToString(dr["CPFEmployerPerc"].ToString());
                if (lblEmpType.Text == "SPR" || lblEmpType.Text == "SDPR")
                {
                    lblEmpType.Text = lblEmpType.Text + " since " + pryears + " years.";
                }


                lblBasicPR.Text = "PR Basic Pay: " + Utility.ToDouble(dr["BasicPR"].ToString()).ToString("#0.00");

                if (dr["BasicPR"].ToString() == "-1")
                {
                    lblPRBasicPay.Text ="";
                    lblBasicPR.Text = "";
                }

           
                if (dr["paytype"].ToString().Trim() == "H")
                {

                    lblWorkDaysInRoll.Text = "-";
                    lblActWorkDaysInRoll.Text = "-";
                }
                else
                {
                    lblWorkDaysInRoll.Text = Utility.ToString(dblWorkDaysInRoll);
                    lblActWorkDaysInRoll.Text = Utility.ToString(dblActWorkDaysInRoll);
                }
                if (dblUnpaidLeavesTotal > 0)
                {
                    lblUnpaid.Text = Utility.ToString(dblUnpaidLeavesTotal);
                }
             //   dblDeductionWOUnpaid = Utility.ToDouble(dr["TotalDeductions"].ToString()) - Utility.ToDouble(dr["TotalUnpaid"].ToString());
                dblDeductionWOUnpaid = Utility.ToDouble(dr["TotalDeductions"].ToString());
                lblTotalUnpaid.Text = Utility.ToString(dr["TotalUnpaid"].ToString());
                lblTotalUnpaidPR.Text = Utility.ToString(dr["UnPaidDeductPR"].ToString());
                lblBasic.Text = "Basic Pay: " + Utility.ToString(dr["Basic"].ToString());
                subpay_day.Text = Utility.ToString(dr["subpay_day"].ToString());
                dayswork = Utility.ToDouble(dr["TotalAdditions"].ToString()) + Utility.ToDouble(dr["OT1"].ToString()) + Utility.ToDouble(dr["OT2"].ToString());
                lblTotAdd.Text = dayswork.ToString("#0.00");
                lblTotDed.Text = dblDeductionWOUnpaid.ToString("#0.00");
                lblNetPay.Text = Utility.ToDouble(dr["NetPay"].ToString()).ToString("#0.00");
                if (!string.IsNullOrEmpty(Utility.ToString(dr["subpay_day"].ToString())))
                {
                    subpay_dailyrate.Text = "{"+Utility.ToString(dr["subpay_dailyrate"].ToString())+" }";
                    
                    subpay_day.Text = "+ ( " + Utility.ToString(dr["subpay_day"].ToString()) + " x " + Utility.ToString(dr["subpay_dailyrate"].ToString())+" )";

                    subbay_workdays.Text = "+ " + Utility.ToString(dr["subpay_day"].ToString());
                    sub_lblHrRate.Text = "{" + Utility.ToString(dr["subpay_hourlyrate"].ToString()) + " }";
                
                }
                if (!string.IsNullOrEmpty(Utility.ToString(dr["UnpaidLeave"].ToString())))
                {
                    sub_lblUnpaidtbl1.Text = Utility.ToString(dr["UnpaidLeave"].ToString());
                    sub_lblTotalUnpaid.Text = Utility.ToString(Utility.ToDouble(dr["UnpaidLeave"].ToString()) * Utility.ToDouble(Utility.ToString(dr["subpay_dailyrate"].ToString())));
                    //sub_lblUnpaidtbl2.Text=Utility.ToString(dr["UnpaidLeave"].ToString());
                    //sub_lblDayRatetbl2.Text = subpay_dailyrate.Text;
                    sub_lblUnpaid.Text = "+ " + Utility.ToString(dr["UnpaidLeave"].ToString());
                    sub_tdunpaid.Visible = true;
                }
                else
                {
                    sub_tdunpaid.Visible = false;
                }

                lblActWorkDaysInRolltbl1.Text = lblActWorkDaysInRoll.Text;
                lblActWorkDaysInRolltbl1PR.Text = Utility.ToDouble(dr["ActPRWrkgDaysSpan"].ToString()).ToString("#0.00");
                
                lblDayRatetbl1.Text = lblDayRate.Text;
               
                decimal dayrate = 0;

                //if ( lblUnpaid.Text!="-" &&   lblTotalUnpaid.Text.ToString() != "" && lblUnpaid.Text.ToString() != "" && dr["paytype"].ToString().Trim() == "M" && dr["daily_rate_mode"].ToString().Trim() == "M")
                //{
                //    dayrate = Convert.ToDecimal(lblTotalUnpaid.Text) / Convert.ToDecimal(lblUnpaid.Text);
                //  lblDayRatetbl2.Text = dayrate.ToString();
                //}
                //else
                //{
                //lblDayRatetbl2.Text = lblDayRate.Text;
                //}
                lblUnpaidtbl1.Text = lblUnpaid.Text;
                //lblUnpaidtbl2.Text = lblUnpaid.Text;
                lblDayRatetbl1PR.Text = lblDayRate.Text;
                lblUnpaidtbl1PR.Text = dblUnpaidLeavesPR.ToString("#0.00");
                lblUnpaidtbl2PR.Text = dblUnpaidLeavesPR.ToString("#0.00");
                lblDayRatetbl2PR.Text = lblDayRate.Text;
                tblRefund.Style.Add("display", "none");

                bool Terminate = false;

                string sql_ter = "SELECT  MONTH(termination_date)FROM  Employee WHERE  Emp_Code=" + EmpId;

                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql_ter, null);
                while (dr1.Read())
                {
                    if (dr["Term_Date"] != DBNull.Value)
                    {
                        if (dtTerm.Month.ToString() == dr1[0].ToString())
                        {
                            Terminate = true;
                        }
                    }
                }
                //Check If EMployee is terminated or not
                if (MonthInt == 12 || Terminate==true)
                {
                   // if (Utility.ToDouble(dr["AWSUBJCPF"]) < 0 || (Month == 12 && Utility.ToDouble(dr["AWSUBJCPF"]) > 0))
                    if (Utility.ToDouble(dr["AWSUBJCPF"]) < 0 || (Month == 12 ))
                    {
                        tblRefund.Style.Add("display", "block");
                        //if ((Month == 12 && Utility.ToDouble(dr["AWSUBJCPF"]) > 0))
                        if (Month == 12)
                        {
                            lblRefAW.Text = "CPF Subject to AW";
                            lblRefAW.ForeColor = Color.Black;
                            lblRefund.ForeColor = Color.Black;

                            if( Utility.ToDouble(dr["AWSUBJCPF"]) < 0)
                            {
                                 lblRefAW.Text = "CPF Refund for AW";
                                 lblRefAW.ForeColor = Color.Red;
                                 lblRefund.ForeColor = Color.Red;
                            
                            }
                        }
                        else
                        {
                            lblRefAW.Text = "CPF Refund for AW";
                            lblRefAW.ForeColor = Color.Red;
                            lblRefund.ForeColor = Color.Red;
                        }
                        lblLYOW.Text = Utility.ToDouble(dr["LYOW"].ToString()).ToString("#0.00");
                        lblCYOW.Text = Utility.ToDouble(dr["CYOW"].ToString()).ToString("#0.00");
                        lblCPFAWCIL.Text = Utility.ToDouble(dr["CPFAWCIL"].ToString()).ToString("#0.00");
                        lblACTCIL.Text = Utility.ToDouble(dr["ACTCIL"].ToString()).ToString("#0.00");
                        lblAWB4CM.Text = Utility.ToDouble(dr["AWB4CM"].ToString()).ToString("#0.00");
                        lblRefund.Text = Utility.ToDouble(dr["AWSUBJCPF"].ToString()).ToString("#0.00");
                    }
                }

                double practualbasicpay = 0;
                practualbasicpay = Utility.ToDouble(dr["BasicPR"].ToString()) - Utility.ToDouble(dr["UnPaidDeductPR"].ToString());
                if (practualbasicpay <= 0)
                {
                    trPRBasic1.Style.Add("display", "none");
                    trPRBasic2.Style.Add("display", "none");
                }
                if ((dr["WrkgDaysInSubPay"].ToString() == dr["ActWrkgDaysSpan"].ToString()) && (dr["WrkgDaysInSubPay"].ToString() == dr["ActPRWrkgDaysSpan"].ToString()))
                {
                    trPRBasic1.Style.Add("display", "none");
                    trPRBasic2.Style.Add("display", "none");
                }

                lblPRBasicPay.Text = practualbasicpay.ToString("#0.00");
                lblGrossPay.Text = Utility.ToDouble(dr["CPFGross1"].ToString()).ToString("#0.00");
                //double cpgross = 0.00;
                //if (Request.QueryString["CPF1"] != "")
                //{
                //    cpgross = Math.Round(Convert.ToDouble(Request.QueryString["CPF1"]),2);
                //}
                lblGrosspayCPF.Text = Utility.ToDouble(dr["CPFGross1"].ToString()).ToString("#0.00");
                if (Utility.ToDouble(dr["EmployerCPFAmt"].ToString()) > 0)
                {
                    lbltxtEmpy.Text = "*Employer CPF Contribution:";
                    lblEmpyCPF.Text = Utility.ToDouble(dr["EmployerCPFAmt"].ToString()).ToString("#0.00");
                }
                else
                {
                    trempy.Style.Add("display", "none");
                }
                
                if (lblPRDate.Text == "01/01/1910")
                {
                    lblPRDate.Text = "-";
                }
                if (lblOTEnt.Text == "N")
                {
                    lblOTEnt.Text = "No";
                    lblOT1rate.Text = "-";
                    lblOT2rate.Text = "-";
                }
                else
                {
                    lblOTEnt.Text = "Yes";
                }
                if (lblCPFApp.Text == "N")
                {
                    lblCPFApp.Text = "No";
                }
                else
                {
                    lblCPFApp.Text = "Yes";
                }

                if (dblUnpaidLeavesTotal <= 0)
                {
                    tdunpaid.Style.Add("display", "none");
                }
                if (dblUnpaidLeavesPR <= 0)
                {
                    tdunpaidPR.Style.Add("display", "none");
                }

                string sSQL1 = "sp_GetEmployeePayDetails";
                SqlParameter[] parms1 = new SqlParameter[24];
                int j = 0;
                parms1[j++] = new SqlParameter("@emp_code", EmpId);
                parms1[j++] = new SqlParameter("@Year", Utility.ToString(Year));
                parms1[j++] = new SqlParameter("@Month", Utility.ToString(Month));
                parms1[j++] = new SqlParameter("@stdatesubmonth", Session["PaySubStartDay"].ToString());
                parms1[j++] = new SqlParameter("@endatesubmonth", Session["PaySubEndDay"].ToString());
                if (Utility.ToString(dr["Days_Work"]) == "")
                {
                    parms1[j++] = new SqlParameter("@Day_Work", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@Day_Work", Utility.ToString(dr["Days_Work"]));
                }
                if (Utility.ToString(dr["OT1"]) == "")
                {
                    parms1[j++] = new SqlParameter("@OT1", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@OT1", Utility.ToString(dr["OT1"]));
                }
                if (Utility.ToString(dr["OT2"]) == "")
                {
                    parms1[j++] = new SqlParameter("@OT2", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@OT2", Utility.ToString(dr["OT2"]));
                }

                if (Utility.ToString(strdailybasicrate) == "")
                {
                    parms1[j++] = new SqlParameter("@BasicDayRate", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@BasicDayRate", Utility.ToString(strdailybasicrate));
                }
                if (Utility.ToString(dr["OT1Hrs"]) == "")
                {
                    parms1[j++] = new SqlParameter("@OT1Hrs", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@OT1Hrs", Utility.ToString(dr["OT1Hrs"]));
                }
                if (Utility.ToString(dr["OT2Hrs"]) == "")
                {
                    parms1[j++] = new SqlParameter("@OT2Hrs", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@OT2Hrs", Utility.ToString(dr["OT2Hrs"]));
                }
                if (Utility.ToString(dr["OT1Rate"]) == "")
                {
                    parms1[j++] = new SqlParameter("@OT1Rate", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@OT1Rate", Utility.ToString(dr["OT1Rate"]));
                }

                if (Utility.ToString(dr["OT2Rate"]) == "")
                {
                    parms1[j++] = new SqlParameter("@OT2Rate", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@OT2Rate", Utility.ToString(dr["OT2Rate"]));
                }

                if (Utility.ToString(dr["EmployeeCPFAmt"]) == "")
                {
                    parms1[j++] = new SqlParameter("@empcpfamount", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@empcpfamount", Utility.ToString(dr["EmployeeCPFAmt"]));
                }

                if (Utility.ToString(dr["CPFOrdinaryCeil"]) == "")
                {
                    parms1[j++] = new SqlParameter("@ordwages", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@ordwages", Utility.ToString(dr["CPFOrdinaryCeil"]));
                }


                //if (Utility.ToString(dr["CPFAdditionNet"]) == "")
                //{
                //    parms1[j++] = new SqlParameter("@addwages", '0');
                //}
                //else
                //{
                //    parms1[j++] = new SqlParameter("@addwages", Utility.ToString(dr["CPFAdditionNet"]));
                //}
                //kumar comment start
                //if (Utility.ToString(dr["TotalAdditionsWONH"]) == "")
                //{
                //    parms1[j++] = new SqlParameter("@addwages", '0');
                //}
                //else
                //{
                //    parms1[j++] = new SqlParameter("@addwages", Utility.ToString(dr["TotalAdditionsWONH"]));
                //}
                //kumar comment end

                if (Utility.ToString(dr["awsubjcpf"]) == "")
                {
                    parms1[j++] = new SqlParameter("@addwages", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@addwages", Utility.ToString(dr["awsubjcpf"]));
                }



                if (Utility.ToString(dr["CPFEmployeePerc"]) == "")
                {
                    parms1[j++] = new SqlParameter("@cpfrate", Utility.ToString(strempcpfrow));
                }
                else
                {
                    parms1[j++] = new SqlParameter("@cpfrate", Utility.ToString(strempcpfrow));
                }
                if (Utility.ToString(dr["FundType"]) == "")
                {
                    parms1[j++] = new SqlParameter("@fundname", '-');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@fundname", Utility.ToString(dr["FundType"]));
                }
                if (Utility.ToDouble(dr["FundAmount"]) <= 0)
                {
                    parms1[j++] = new SqlParameter("@fundamount", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@fundamount", Utility.ToString(dr["FundAmount"]));
                }
                if (Utility.ToDouble(dr["FundGrossAmount"]) <= 0)
                {
                    parms1[j++] = new SqlParameter("@fundgrossamount", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@fundgrossamount", Utility.ToString(dr["FundGrossAmount"]));
                }
                if (Utility.ToDouble(dr["NHHrs"]) == 0)
                {
                    parms1[j++] = new SqlParameter("@nhhrs", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@nhhrs", Utility.ToString(dr["NHHrs"]));
                }
                if (Utility.ToDouble(dr["Hourly_Rate"]) == 0)
                {
                    parms1[j++] = new SqlParameter("@hourlyrate", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@hourlyrate", Utility.ToString(dr["Hourly_Rate"]));
                }
                if (Utility.ToDouble(dr["DaysWorkedRate"]) == 0)
                {
                    parms1[j++] = new SqlParameter("@daysworkedrate", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@daysworkedrate", Utility.ToString(dr["DaysWorkedRate"]));
                }
                parms1[j++] = new SqlParameter("@addtionprorated", "1");


               
                

                DataSet ds = new DataSet();
                ds = DataAccess.ExecuteSPDataSet(sSQL1, parms1);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        RadGrid1.DataSource = ds.Tables[0];
                        RadGrid1.DataBind();
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        RadGrid2.DataSource = ds.Tables[1];
                        RadGrid2.DataBind();
                    }
                }

                double sdlamount = Utility.ToDouble(dr["SDLFundGrossAmount"]);
                Object objsdl = null;
                if (dr["SDF_Required"].ToString().Trim() == "2")
                {
                    if (sdlamount > 0)
                    {
                        objsdl = dr["SDLFundGrossAmount"];
                    }
                    else
                    {
                        objsdl = dr["FundGrossAmount"];
                    }
                }

                if (objsdl != DBNull.Value)
                {
                    int intmonth = Utility.ToInteger(Request.QueryString["qsMonth"]) - 1;
                    lblSDL.Text = "";
                    double fundgrossamount = Utility.ToDouble(objsdl);
                    //string sSql = "Select dbo.fn_getSDLAmount(" + fundgrossamount.ToString() + ")";

                    ////////////////////////////////////////Get Payroll Month
                    string sqlmonth = "SELECT Month FROM payrollmonthlydetail WHERE ROWID=" + intmonth;
                    SqlDataReader dr9 ;
                    dr9 = DataAccess.ExecuteReader(CommandType.Text,sqlmonth,null);
                    int intactualmonth = 1;
                    while (dr9.Read())
                    {
                        intactualmonth = Convert.ToInt32(dr9[0].ToString());
                    } 
                    //ram
                    string sSql = "Select dbo.fn_getSDLAmount(" + fundgrossamount.ToString() + ")";
                   // string sSql = "Select dbo.fn_getSDLAmount(" + fundgrossamount.ToString() + "," + intactualmonth + "," + Year + ")";
                   ////////////////////////////////////////
                   string strSqlDed = "; Select isnull(sum(SDL),0) SDLAmt From prepare_payroll_detail pd Inner Join prepare_payroll_hdr ph on pd.trx_id = ph.trx_id Where ([Status]='A' Or [Status]='G' Or [Status]='P') And Emp_ID=" + EmpId.ToString() + "And ph.end_period=(SELECT PaySubEndDate FROM   PayrollMonthlyDetail  WHERE  ROWID = (" + intmonth.ToString() + "))";

                    DataSet dsSDL = new DataSet();
                    dsSDL = DataAccess.FetchRS(CommandType.Text, (sSql+strSqlDed), null);
                    if (dsSDL != null)
                    {
                        if (dsSDL.Tables[0].Rows.Count > 0)
                        {
                            if (Utility.ToDouble(dsSDL.Tables[0].Rows[0][0].ToString()) <= 0)
                            {
                                //if (intmonth == 12)
                                //{
                                //    trsdl.Attributes.Add("style", "display:block");
                                //}
                                //else
                                //{
                                    trsdl.Attributes.Add("style", "display:none");
                                //}

                            }
                            else
                            {
                                trsdl.Attributes.Add("style", "display:table-row");
                                if (Utility.ToInteger(Session["PaySubStartDay"].ToString()) > 1)
                                {
                                    double sdlamnt = Utility.ToDouble(dsSDL.Tables[0].Rows[0][0].ToString()) - Utility.ToDouble(dsSDL.Tables[1].Rows[0][0].ToString());
                                    if (sdlamnt < 0)
                                    {
                                        sdlamnt = 0;
                                    }
                                    lblSDL.Text = Utility.ToString(sdlamnt);
                                }
                                else
                                {
                                    lblSDL.Text = dsSDL.Tables[0].Rows[0][0].ToString();
                                }

                                    //If PR Date is >1 then overwrite SDL with old salary ...
                                    if (dtPRdate1 != null)
                                    {
                                        //stmonth
                                        // dtTerm = DateTime.Parse(dr["Term_Date"].ToString(), format);
                                        //lblTermDate.Text = dtTerm.ToString("dd/MM/yyyy");

                                        //Check for Month processing salary
                                        string stmonth = Utility.ToString(Request.QueryString["stmonth"]);
                                        DateTime dt1 = DateTime.Parse(stmonth, format);

                                        if (dtPRdate1.Date.Day > 1 && dtPRdate1.Date.Month == dt1.Month && dtPRdate1.Date.Year == dt1.Year)
                                        {
                                            lblSDL.Text = dsSDL.Tables[1].Rows[0][0].ToString();   
                                        }
                                    
                                    }
                                //}
                            }
                        }
                    }
                }
            }
            dr.Close();

            DataSet ds_leave = new DataSet();
            string strSQL = "sp_GetEmployeeLeavePolicy";
            SqlParameter[] pars = new SqlParameter[4];
            pars[0] = new SqlParameter("@empid", Utility.ToInteger(EmpId));
            pars[1] = new SqlParameter("@year", Utility.ToString(Year));
            pars[2] = new SqlParameter("@applydateon", Convert.ToDateTime(System.DateTime.Today));
            pars[3] = new SqlParameter("@filter", -1);
            ds_leave = DataAccess.ExecuteSPDataSet(strSQL, pars);
            RadGrid3.DataSource = ds_leave;
            RadGrid3.DataBind();
            if (strpaymode == "-1")
            {
                lblPayMode.Text = "Cash";
            }
            else if (strpaymode == "-2")
            {
                lblPayMode.Text = "Cheque";
            }
            else
            {
                if (lblPayMode.Text == "")
                {
                    sSQLextData = "SELECT a.id, BANK_ID,B.[DESC],A.BANK_ACCOUNTNO,B.[DESC]+'-'+BANK_ACCOUNTNO 'BANK' FROM GIROBANKS A,BANK B WHERE a.company_id=" + CompanyID + " and A.BANK_ID=B.ID";

                }else
                {
                    sSQLextData = "SELECT a.id, BANK_ID,B.[DESC],A.BANK_ACCOUNTNO,B.[DESC]+'-'+BANK_ACCOUNTNO 'BANK' FROM GIROBANKS A,BANK B WHERE a.company_id=" + CompanyID + " and A.BANK_ID=B.ID And a.id=" + lblPayMode.Text;
                }
                SqlDataReader drExtData = DataAccess.ExecuteReader(CommandType.Text, sSQLextData, null);
                while (drExtData.Read())
                {
                    lblPayMode.Text = drExtData["Bank"].ToString();
                }
                drExtData.Close();
            }

            if (dblage <= 35)
            {
                stragegroup = "Below 35";
            }
            else if (dblage > 35 && dblage <= 50)
            {
                stragegroup = "35 - 50";
            }
            else if (dblage > 50 && dblage <= 55)
            {
                stragegroup = "50 - 55";
            }
            else if (dblage > 55 && dblage <= 60)
            {
                stragegroup = "55 - 60";
            }
            else if (dblage > 60 && dblage <= 65)
            {
                stragegroup = "60 - 65";
            }
            else if (dblage > 65)
            {
                stragegroup = "Above 65";
            }


            if (calcCPF == "Y")
            {
                if (dblCPFGross <= 1500)
                {
                    lblCPFEmpRate.Text = strempcpfrow;
                    lblCPFEmpyrate.Text = strempycpfrow;
                }
                else
                {
                    if (pryears.ToString().Length <= 0)
                    {
                        tbl1.Style.Add("display", "none");
                        tbl2.Style.Add("display", "table");
                    }
                    else
                    {

                        tbl1.Style.Add("display", "table");
                        tbl2.Style.Add("display", "none");
                        sSQLextData = "SELECT age_from, age_to FROM cpf_calcs WHERE (pr_year = " + pryears + ") AND (emp_group = " + empcpftype + " ) AND (" + dblage + " >= age_from) AND (" + dblage + " <= age_to)";
                        SqlDataReader drExtData = DataAccess.ExecuteReader(CommandType.Text, sSQLextData, null);
                        while (drExtData.Read())
                        {
                            stragegroup = Utility.ToString(drExtData["age_from"].ToString()) + " - " + Utility.ToString(drExtData["age_to"].ToString());
                        }
                        drExtData.Close();
                        lblCPFEmpRate.Text = CPFempPerc;
                        lblCPFEmpyrate.Text = CPFempyPerc;
                    }
                }



                lblCPFAgeGrp.Text = stragegroup;
            }
        }


    }
}