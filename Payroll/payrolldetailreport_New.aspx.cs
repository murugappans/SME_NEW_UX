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
using System.Text;
using System.IO;

namespace SMEPayroll.Payroll
{
    public partial class payrolldetailreport_New : System.Web.UI.Page
    {
        public string month, year, CompanyID;
        public int Month, Year;
        DateTime dtPRdate1;
        int compid;
        int columncount;
        protected void Page_Load(object sender, EventArgs e)
        {
                compid = Utility.ToInteger(Session["Compid"]);

            if (Utility.ToString(Session["Username"]) == "")
               Response.Redirect("../SessionExpire.aspx");

            month = Request.QueryString["month"].ToString();
            year = Request.QueryString["year"].ToString();  
            CompanyID = Utility.ToString(Session["Compid"]);
            Month = Convert.ToInt32(Request.QueryString["Month"]);
            Year = Convert.ToInt32(Request.QueryString["Year"]);
            
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            string CompanyID = Utility.ToString(Session["Compid"]);
            int Month = Convert.ToInt32(Request.QueryString["Month"]);
            int Year = Convert.ToInt32(Request.QueryString["Year"]);

            string EmpPassID = "";

            if (Session["EmpPassID"] != null)
            {
                EmpPassID = Convert.ToString(Session["EmpPassID"]);
            }
            else
            {
                EmpPassID = "";
            }
            int i = 0;
            //string sSQL = "Sp_generatepayrolladv_AddDet1";
            //string sSQLextData = "";
            //SqlParameter[] parms = new SqlParameter[10];
            //parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(CompanyID));
            //parms[i++] = new SqlParameter("@month", Utility.ToInteger(Month));
            //parms[i++] = new SqlParameter("@year", Utility.ToInteger(Year));
            //parms[i++] = new SqlParameter("@stdatemonth", Request.QueryString["stdatemonth"]);
            //parms[i++] = new SqlParameter("@endatemonth", Request.QueryString["endatemonth"]);
            //parms[i++] = new SqlParameter("@stdatesubmonth", Request.QueryString["stdatesubmonth"]);
            //parms[i++] = new SqlParameter("@endatesubmonth", Request.QueryString["endatesubmonth"]);
            //parms[i++] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"].ToString()));
            ////parms[i++] = new SqlParameter("@EmpPassID", "");
            //if (EmpPassID == "")
            //{
            //    parms[i++] = new SqlParameter("@EmpPassID", "");
            //}
            //else
            //{
            //    parms[i++] = new SqlParameter("@EmpPassID", EmpPassID);
            //}
            //parms[i++] = new SqlParameter("@monthidintbl", Request.QueryString["monthidintbl"]);
            //exec sp_GeneratePayRollAdv @company_id=5,@year=2012,@UserID=306,@month=310,@stdatemonth=1,@endatemonth=30,@stdatesubmonth=16,@endatesubmonth=30,@monthidintbl=310,@DeptId=N'-1'

            string sSQL = "sp_GeneratePayRollAdv";
            string sSQLextData = "";
            SqlParameter[] parms = new SqlParameter[10];
            parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(CompanyID));
            parms[i++] = new SqlParameter("@month", Utility.ToInteger(Month));
            parms[i++] = new SqlParameter("@year", Utility.ToInteger(Year));
            parms[i++] = new SqlParameter("@stdatemonth", Request.QueryString["stdatemonth"]);
            parms[i++] = new SqlParameter("@endatemonth", Request.QueryString["endatemonth"]);
            parms[i++] = new SqlParameter("@stdatesubmonth", Request.QueryString["stdatesubmonth"]);
            parms[i++] = new SqlParameter("@endatesubmonth", Request.QueryString["endatesubmonth"]);
            parms[i++] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"].ToString()));
            parms[i++] = new SqlParameter("@DeptId", Utility.ToInteger(Request.QueryString["dept_id"]));
            parms[i++] = new SqlParameter("@monthidintbl", Request.QueryString["monthidintbl"]);          
           // SqlDataReader drpro = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL, parms);
            DataSet ds = new DataSet();
            ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
            //foreach (DataColumn dc in ds.Tables[0].Columns)
            //{
            //    if (dc.ColumnName != "Basic" || dc.ColumnName != "ot1" || dc.ColumnName != "ot2" || dc.ColumnName != "totaladditions" || dc.ColumnName != "grosswithaddition" || dc.ColumnName != "employeecpfamt" || dc.ColumnName != "totaladditions" || dc.ColumnName != "grosswithaddition" || dc.ColumnName != "employeecpfamt" )
            //    { 
            //    }
            //}
            //emp_code	fullname Basic	ot1	ot2	totaladditions	grosswithaddition	employeecpfamt	cdac_fund	mbmf_fund	sinda_fund	ecf_fund	fundtype			unpaidleaves	totaldeductions	netpay	employercpfamt	cpfgross	sdlfundgrossamount
            //deptname	groupname		BasicPR		totaladditionswonh		ot1rate	ot2rate	nhhrs	ot1hrs	ot2hrs	
            //nh	days_work		ot	cpfordinaryceil	cpfadditionnet				cpfamount	cpf	empcpftype	prage	cpfceiling		
            //fundamount		totalunpaid	paytype	daily_rate_mode	daily_rate	actualbasic	pay_mode	employeegiro	employergiro	girobank	
            //emp_type	workingdaysinweek	cpftype	hourly_rate	join_date	term_date	pr_date	rolldays	wrkgdaysinroll	
            //basicpayinroll	subpaydays	wrkgdaysinsubpay	basicdayrate	basicdayrateworound	basicnorsubpay	
            //unpaidfullday	unpaidhalfday	paidfullday	paidhalfday	unpaiddeduct	unpaidfulldaypr	
            //unpaidhalfdaypr	unpaiddeductpr	add4nw	add4ow	add4aw	add4awnocpf	additionalwages	
            //dedamt	dedcpfamt	actsatdayspan	actsundayspan	
            //actwrkgdaysspan	actprsatdayspan	actprsundayspan	actprwrkgdaysspan	
            //daysworkedrate	cpfordinary	age_group	age	ot1sysrate	
            //ot2sysrate	calculatecpf	cpfemployeeperc	cpfemployerperc	
            ///employeecpfrow	employercpfrow		grosswithoutaddition	
            /////fundgrossamount		cmow	lyow	cyow	cpfawcil	
            //est_awcil	actcil	awcm	awb4cm	awcm_awb4cm	awsubjcpf	
            //mediumurl	time_card_no	fund_optout	sdf_required	payprocessfh	halfsalary	emp_code	emp_name	
            //emp_lname	emp_alias	nationality_id	emp_type	ic_pp_number	
            //wp_exp_date		address	country_id	postal_code	phone	hand_phone	email	time_card_no	
            //sex	religion_id	race_id	marital_status	place_of_birth	date_of_birth	income_taxid	giro_bank	giro_code
            //giro_branch	giro_acct_number	desig_id	dept_id	joining_date	probation_period	confirmation_date	



          




            ds.Tables[0].Columns.Remove("totaladditionswonh");
            ds.Tables[0].Columns.Remove("ot1rate");
           
            ds.Tables[0].Columns.Remove("groupname");
            ds.Tables[0].Columns.Remove("BasicPR");
            ds.Tables[0].Columns.Remove("ot2rate");
            //ds.Tables[0].Columns.Remove("nhhrs");
            //ds.Tables[0].Columns.Remove("ot1hrs");
            //ds.Tables[0].Columns.Remove("ot2hrs");
        
            //ds.Tables[0].Columns.Remove("days_work");
            ds.Tables[0].Columns.Remove("ot");

            ds.Tables[0].Columns.Remove("cpfordinaryceil");
            ds.Tables[0].Columns.Remove("cpfadditionnet");            
            ds.Tables[0].Columns.Remove("cpf");
            ds.Tables[0].Columns.Remove("empcpftype");
            ds.Tables[0].Columns.Remove("prage");

            ds.Tables[0].Columns.Remove("cpfceiling");            
          //  ds.Tables[0].Columns.Remove("totalunpaid");
            ds.Tables[0].Columns.Remove("paytype");
            ds.Tables[0].Columns.Remove("daily_rate_mode");
            ds.Tables[0].Columns.Remove("daily_rate");

            ds.Tables[0].Columns.Remove("pay_mode");
            ds.Tables[0].Columns.Remove("employeegiro");
            ds.Tables[0].Columns.Remove("employergiro");
            ds.Tables[0].Columns.Remove("girobank");
            ds.Tables[0].Columns.Remove("emp_type");

            ds.Tables[0].Columns.Remove("ic_pp_number");
            ds.Tables[0].Columns.Remove("wp_exp_date");

            ds.Tables[0].Columns.Remove("address");
            ds.Tables[0].Columns.Remove("country_id");
            ds.Tables[0].Columns.Remove("postal_code");

            ds.Tables[0].Columns.Remove("phone");
            ds.Tables[0].Columns.Remove("hand_phone");
            ds.Tables[0].Columns.Remove("email");
            
            ds.Tables[0].Columns.Remove("sex");
            ds.Tables[0].Columns.Remove("religion_id");
            ds.Tables[0].Columns.Remove("race_id");

            ds.Tables[0].Columns.Remove("marital_status");
            ds.Tables[0].Columns.Remove("place_of_birth");
            ds.Tables[0].Columns.Remove("date_of_birth");
            ds.Tables[0].Columns.Remove("income_taxid");


            ds.Tables[0].Columns.Remove("giro_bank");
            ds.Tables[0].Columns.Remove("giro_code");
            ds.Tables[0].Columns.Remove("giro_branch");
            ds.Tables[0].Columns.Remove("giro_acct_number");


            ds.Tables[0].Columns.Remove("desig_id");
            ds.Tables[0].Columns.Remove("dept_id");
            ds.Tables[0].Columns.Remove("joining_date");
            ds.Tables[0].Columns.Remove("probation_period");

            ds.Tables[0].Columns.Remove("confirmation_date");
            ds.Tables[0].Columns.Remove("termination_date");
            ds.Tables[0].Columns.Remove("cpf_entitlement");
            ds.Tables[0].Columns.Remove("emp_group_id");
            ds.Tables[0].Columns.Remove("cpf_employer");
            ds.Tables[0].Columns.Remove("cpf_employee");
            ds.Tables[0].Columns.Remove("employee_cpf_acct");

            //employer_cpf_acct	emp_supervisor	ot_entitlement	payment_mode	fw_code	fw_levy	

            ds.Tables[0].Columns.Remove("employer_cpf_acct");
            ds.Tables[0].Columns.Remove("emp_supervisor");
            ds.Tables[0].Columns.Remove("ot_entitlement");
            ds.Tables[0].Columns.Remove("payment_mode");
            ds.Tables[0].Columns.Remove("fw_code");
            ds.Tables[0].Columns.Remove("fw_levy");
				

            ds.Tables[0].Columns.Remove("cchest_fund");
            ds.Tables[0].Columns.Remove("email_payslip");
            ds.Tables[0].Columns.Remove("wh_tax_pct");
            ds.Tables[0].Columns.Remove("wh_tax_amt");
            ds.Tables[0].Columns.Remove("education");
            ds.Tables[0].Columns.Remove("termination_reason");
            ds.Tables[0].Columns.Remove("pay_frequency");
            //payrate	remarks	Images	UserName	Password	GroupID	StatusId						

            ds.Tables[0].Columns.Remove("payrate");
            ds.Tables[0].Columns.Remove("remarks");
            ds.Tables[0].Columns.Remove("Images");
            ds.Tables[0].Columns.Remove("UserName");
            ds.Tables[0].Columns.Remove("Password");
            ds.Tables[0].Columns.Remove("GroupID");
            ds.Tables[0].Columns.Remove("StatusId");

            ds.Tables[0].Columns.Remove("Company_Id");
            ds.Tables[0].Columns.Remove("Insurance_number");
            ds.Tables[0].Columns.Remove("Insurance_expiry");
            ds.Tables[0].Columns.Remove("CSOC_number");
            ds.Tables[0].Columns.Remove("CSOC_expiry");
            ds.Tables[0].Columns.Remove("passport");

            ds.Tables[0].Columns.Remove("passport_expiry");
            ds.Tables[0].Columns.Remove("leave_carry_forward");
            ds.Tables[0].Columns.Remove("Giro_acc_name");
            ds.Tables[0].Columns.Remove("localaddress2");
            ds.Tables[0].Columns.Remove("foreignaddress1");

            ds.Tables[0].Columns.Remove("foreignaddress2");
            ds.Tables[0].Columns.Remove("foreignpostalcode");
            ds.Tables[0].Columns.Remove("pp_issue_date");
            ds.Tables[0].Columns.Remove("leaves_remaining");
            ds.Tables[0].Columns.Remove("wp_application_date");
           
            ds.Tables[0].Columns.Remove("worker_levy");
            ds.Tables[0].Columns.Remove("hourly_rate_mode");
            ds.Tables[0].Columns.Remove("block_no");
            ds.Tables[0].Columns.Remove("street_name");
            ds.Tables[0].Columns.Remove("level_no");

            //							v2rate	v3rate	v4rate	
            ds.Tables[0].Columns.Remove("unit_no");
            ds.Tables[0].Columns.Remove("wdays_per_week");
            ds.Tables[0].Columns.Remove("emp_ref_no");
            ds.Tables[0].Columns.Remove("emp_status");
            ds.Tables[0].Columns.Remove("emp_category");
            ds.Tables[0].Columns.Remove("v1rate");
            ds.Tables[0].Columns.Remove("v2rate");
            ds.Tables[0].Columns.Remove("v3rate");
            ds.Tables[0].Columns.Remove("v4rate");

            ds.Tables[0].Columns.Remove("emp_clsupervisor");
            ds.Tables[0].Columns.Remove("wp_issue_date");
            ds.Tables[0].Columns.Remove("wp_number");
            ds.Tables[0].Columns.Remove("batch_no");
            ds.Tables[0].Columns.Remove("photo_no");
            ds.Tables[0].Columns.Remove("shipyard_quota");
            ds.Tables[0].Columns.Remove("LYTotalOW");

            ds.Tables[0].Columns.Remove("pay_supervisor");
            ds.Tables[0].Columns.Remove("bloodgroup");
            ds.Tables[0].Columns.Remove("agent_id");
            ds.Tables[0].Columns.Remove("mye_cert_id");
            ds.Tables[0].Columns.Remove("eme_cont_per");
            ds.Tables[0].Columns.Remove("eme_cont_per_rel");
            ds.Tables[0].Columns.Remove("eme_cont_per_ph1");
            ds.Tables[0].Columns.Remove("eme_cont_per_ph2");

            ds.Tables[0].Columns.Remove("eme_cont_per_add");
            ds.Tables[0].Columns.Remove("eme_cont_per_rem");
            ds.Tables[0].Columns.Remove("wp_arrival_date");
            ds.Tables[0].Columns.Remove("OWLastYear");
            ds.Tables[0].Columns.Remove("payrolltype");
            ds.Tables[0].Columns.Remove("trade_id");
            ds.Tables[0].Columns.Remove("ComputeCPFFH");
            ds.Tables[0].Columns.Remove("timesupervisor");

            ds.Tables[0].Columns.Remove("ComputeFundFH");
            ds.Tables[0].Columns.Remove("HalfSalary");            
            ds.Tables[0].Columns.Remove("Leave_supervisor");

            ds.Tables[0].Columns.Remove("Designation");
            ds.Tables[0].Columns.Remove("TimeCardId");
            ds.Tables[0].Columns.Remove("Nationality");
            ds.Tables[0].Columns.Remove("Trade");

            ds.Tables[0].Columns.Add("SINDA");
            ds.Tables[0].Columns.Add("MBMF");
            ds.Tables[0].Columns.Add("CDAC");
            ds.Tables[0].Columns.Add("ECF");
           // ds.Tables[0].Columns.Add("Basic/HR");

            //Get The dataset for Additions/deductions
            //http://localhost:3380/Payroll/payrolldetailreport_New.aspx?UserID=305&Month=1&stdatemonth=1&endatemonth=31&stdatesubmonth=1&endatesubmonth=15&monthidintbl=289&Year=2012&company_id=4

            DataSet dsadditions = new DataSet();

            //parms[i++] = new SqlParameter("@stdatesubmonth", Request.QueryString["stdatesubmonth"]);
            //parms[i++] = new SqlParameter("@endatesubmonth", Request.QueryString["endatesubmonth"]);
            //Month
            // Utility.ToInteger(Year)

            string startdate = Month.ToString() + "/" + Request.QueryString["stdatesubmonth"]+ "/"  +  Year.ToString();
            string enddate = Month.ToString() + "/" + Request.QueryString["endatesubmonth"] + "/" + Year.ToString();

            string strAdditions1 = "SELECT ea.emp_code,sum(trx_amount)trx_amount ,AT.[Desc] FROM   emp_additions ea  INNER JOIN additions_types AT ";
            strAdditions1 = strAdditions1 + " ON ea.trx_type = AT.id INNER JOIN employee e on e.emp_code=ea.emp_code WHERE (( trx_period >= '" + startdate + "' AND trx_period <=  '" + enddate + "' ) ";
            strAdditions1 = strAdditions1 + " AND YEAR(trx_period) = " + year + " ) AND AT.cpf = 'Yes' and AT.ClaimCash !='2' AND AT.type_of_wage = 'A' AND ( ea.claimstatus = 'Approved' OR ea.claimstatus IS NULL ) and e.company_id=" + compid + " Group by ea.emp_code,[Desc] ";


            string strAdditions2 = " SELECT ea.emp_code,sum(trx_amount)  ,AT.[Desc] FROM   emp_additions ea INNER JOIN additions_types AT  ON ea.trx_type = AT.id " ;
            strAdditions2 = strAdditions2 + " INNER JOIN employee e on e.emp_code=ea.emp_code WHERE ( ( trx_period >= '" + startdate + "' AND trx_period <=  '" + enddate + "' )AND YEAR(trx_period)= " + year + " ) AND AT.cpf = 'No' and AT.ClaimCash !='2' AND AT.type_of_wage = 'A' ";
            strAdditions2 = strAdditions2 + " AND ( ea.claimstatus = 'Approved' OR ea.claimstatus IS NULL ) and e.company_id=" + compid + "  Group by ea.emp_code,[Desc] ";

            string strAdditions3 = "SELECT ea.emp_code,sum(trx_amount) ,AT.[Desc] FROM   emp_additions ea INNER JOIN additions_types AT ";
            strAdditions3 = strAdditions3 + " ON ea.trx_type = AT.id INNER JOIN employee e on e.emp_code=ea.emp_code  WHERE (( trx_period >= '" + startdate + "' AND trx_period <=  '" + enddate + "')";
            strAdditions3 = strAdditions3 + " AND YEAR(trx_period) = " + year + " ) AND AT.cpf = 'Yes' and AT.ClaimCash !='2' AND ( AT.type_of_wage = 'O'  OR AT.type_of_wage IS NULL ) AND ( ea.claimstatus = 'Approved' OR ea.claimstatus IS NULL ) and e.company_id=" + compid + " Group by ea.emp_code,[Desc]  ";


            string strAdditions4 = "SELECT ea.emp_code,sum(trx_amount),AT.[Desc] FROM   emp_additions ea  INNER JOIN additions_types AT ON ea.trx_type = AT.id INNER JOIN employee e on e.emp_code=ea.emp_code ";
            strAdditions4 = strAdditions4 + " WHERE  ( ( trx_period >= '" + startdate + "' AND trx_period <=  '" + enddate + "') AND YEAR(trx_period) = " + year + " ) AND AT.cpf = 'No' and AT.ClaimCash !='2' AND ( AT.type_of_wage = 'O' OR AT.type_of_wage IS NULL )  AND ( ea.claimstatus = 'Approved' ";
            strAdditions4 = strAdditions4 + " OR ea.claimstatus IS NULL ) and e.company_id=" + compid + "  Group by ea.emp_code,[Desc] ";

            string strAddFinal = strAdditions1 + " UNION ALL " + strAdditions2 + " UNION ALL " + strAdditions3 + " UNION ALL " + strAdditions4;

            DataSet dsAddTypes = new DataSet();
            string strAdditionNames = "Select Distinct[Desc] From (" + strAddFinal + ")A";

            dsadditions = DataAccess.FetchRS(CommandType.Text, strAddFinal, null);
            dsAddTypes = DataAccess.FetchRS(CommandType.Text, strAdditionNames, null);

            //Get Actual Employee Additions and deductions ***

            //exec sp_GetEmployeePayDetails @emp_code=N'5',@Year=N'2012',@Month=N'12',@stdatesubmonth=N'1',@endatesubmonth=N'31',@Day_Work=N'0',@OT1=N'20.44',@OT2=N'13.62',@BasicDayRate=N'0',@OT1Hrs=N'2.00',@OT2Hrs=N'1.00',@OT1Rate=N'10.218',@OT2Rate=N'13.624',@empcpfamount=N'276.00',@ordwages=N'1304.06',@addwages=N'100',@cpfrate=N'((1404.06 - 750) * 0.24)+120',@fundname=N'CDAC',@fundamount=N'0.5',@fundgrossamount=N'1434.0600',@nhhrs=N'0',@hourlyrate=N'6.812',@daysworkedrate=N'0'

            //http://localhost:2814/Payroll/payrolldetailreport_New.aspx?UserID=3&Month=12&stdatemonth=1&endatemonth=31&stdatesubmonth=1&endatesubmonth=31&monthidintbl=72&Year=2012&company_id=3&dept_id=-1

            /***********************************************************************************/
            string sSQL1 = "";
            sSQL1 = "Sp_getemployeepaydetails_fordetailreport";


            SqlParameter[] parms1 = new SqlParameter[24];
            DataSet dsAddded = new DataSet();

            foreach (DataRow drnew in dsadditions.Tables[0].Rows)
            {
                int j = 0;
                parms1[j++] = new SqlParameter("@emp_code", drnew["emp_code"].ToString());
                parms1[j++] = new SqlParameter("@Year", Utility.ToString(Request.QueryString["Year"].ToString()));
                parms1[j++] = new SqlParameter("@Month", Utility.ToString(Request.QueryString["Month"].ToString()));
                parms1[j++] = new SqlParameter("@stdatesubmonth", Session["PaySubStartDay"].ToString());
                parms1[j++] = new SqlParameter("@endatesubmonth", Session["PaySubEndDay"].ToString());
                parms1[j++] = new SqlParameter("@Day_Work", '0');
                parms1[j++] = new SqlParameter("@OT1", '0');
                parms1[j++] = new SqlParameter("@OT2", '0');
                parms1[j++] = new SqlParameter("@BasicDayRate", '0');
                parms1[j++] = new SqlParameter("@OT1Hrs", '0');
                parms1[j++] = new SqlParameter("@OT2Hrs", '0');
                parms1[j++] = new SqlParameter("@OT1Rate", '0');
                parms1[j++] = new SqlParameter("@OT2Rate", '0');
                parms1[j++] = new SqlParameter("@empcpfamount", '0');
                parms1[j++] = new SqlParameter("@ordwages", '0');
                parms1[j++] = new SqlParameter("@addwages", '0');
                parms1[j++] = new SqlParameter("@cpfrate", Utility.ToString("0"));
                parms1[j++] = new SqlParameter("@fundname", '-');
                parms1[j++] = new SqlParameter("@fundamount", '0');
                parms1[j++] = new SqlParameter("@fundgrossamount", '0');
                parms1[j++] = new SqlParameter("@nhhrs", '0');
                parms1[j++] = new SqlParameter("@hourlyrate", '0');
                parms1[j++] = new SqlParameter("@daysworkedrate", '0'); //
            parms1[j++] = new SqlParameter("@addtionprorated", "1");

                





           

                dsAddded = DataAccess.ExecuteSPDataSet(sSQL1, parms1);

                foreach (DataRow dremp in dsAddded.Tables[0].Rows)
                {
                    if (drnew["Desc"].ToString().Trim() == dremp["AddType"].ToString().Replace(':',' ').Trim())
                    {   
                        drnew.BeginEdit();
                      drnew["trx_amount"] = dremp["AddAmt"].ToString();
                       
                        drnew.AcceptChanges();
                    }
                }
            }
            /************************************************************************************/

            foreach (DataRow dr in dsAddTypes.Tables[0].Rows)
            {
                ds.Tables[0].Columns.Add(dr["Desc"].ToString());
            }

            //Update Values to rows 
            foreach (DataRow dr1 in ds.Tables[0].Rows)
            {
                string emp_code = dr1["emp_code"].ToString();

                foreach (DataRow dr5 in dsadditions.Tables[0].Rows)
                {
                    string colname =dr5["Desc"].ToString();

                    if (dr5["emp_code"].ToString() == emp_code)
                    {
                        dr1.BeginEdit();
                        dr1[colname] = dr5["trx_amount"].ToString();
                        dr1.AcceptChanges();
                    }
                }            
            }

           //Deductions 


            DataSet dsDecution = new DataSet();
            DataSet dsdeductype = new DataSet();

            string strdeduct = " SELECT ed.emp_code,   sum(trx_amount) trx_amount ,dt.[desc] FROM   emp_deductions ed INNER JOIN deductions_types dt ";
            strdeduct = strdeduct + " ON ed.trx_type = dt.id INNER JOIN employee e on e.emp_code=ed.emp_code  WHERE  (   ( trx_period >= '" + startdate + "'";
            strdeduct = strdeduct + " AND trx_period <=  '" + enddate + "' ) AND YEAR(trx_period) = " + year + " ) AND ( ( ed.fundtype IS NULL OR ed.fundtype = 0 )OR ( ed.fundtype >= 5 AND ed.fundtype <= 8 ) ) and e.company_id = " + compid + " Group by ed.emp_code,[Desc]  ";

            string strdeduct1 = "SELECT ed.emp_code,sum(ed.trx_amount) trx_amount ,dt.[desc]   FROM   emp_deductions ed INNER JOIN deductions_types dt    ON ed.trx_type = dt.id  INNER JOIN employee e on e.emp_code=ed.emp_code WHERE  (( trx_period >= '" + startdate + "' AND trx_period <=  '" + enddate + "'       )AND YEAR(ed.trx_period) = " + year + " ) AND dt.cpf = 'Yes' and e.company_id = " + compid + "  Group by ed.emp_code,[Desc] ";

            string strdeductf = strdeduct + " Union ALL " + strdeduct1;
            string strdeductnames = "Select Distinct[Desc] From (" + strdeduct + ")A";

            dsDecution = DataAccess.FetchRS(CommandType.Text, strdeductf, null);
            dsdeductype = DataAccess.FetchRS(CommandType.Text, strdeductnames, null);

            foreach (DataRow dr in dsdeductype.Tables[0].Rows)
            {
                try {
                    ds.Tables[0].Columns.Add(dr["Desc"].ToString());
                }
                catch (Exception ex) { }
                
            }

            foreach (DataRow drnew in dsDecution.Tables[0].Rows)
            {
                int j = 0;
                parms1[j++] = new SqlParameter("@emp_code", drnew["emp_code"].ToString());
                parms1[j++] = new SqlParameter("@Year", Utility.ToString(Request.QueryString["Year"].ToString()));
                parms1[j++] = new SqlParameter("@Month", Utility.ToString(Request.QueryString["Month"].ToString()));
                parms1[j++] = new SqlParameter("@stdatesubmonth", Session["PaySubStartDay"].ToString());
                parms1[j++] = new SqlParameter("@endatesubmonth", Session["PaySubEndDay"].ToString());
                parms1[j++] = new SqlParameter("@Day_Work", '0');
                parms1[j++] = new SqlParameter("@OT1", '0');
                parms1[j++] = new SqlParameter("@OT2", '0');
                parms1[j++] = new SqlParameter("@BasicDayRate", '0');
                parms1[j++] = new SqlParameter("@OT1Hrs", '0');
                parms1[j++] = new SqlParameter("@OT2Hrs", '0');
                parms1[j++] = new SqlParameter("@OT1Rate", '0');
                parms1[j++] = new SqlParameter("@OT2Rate", '0');
                parms1[j++] = new SqlParameter("@empcpfamount", '0');
                parms1[j++] = new SqlParameter("@ordwages", '0');
                parms1[j++] = new SqlParameter("@addwages", '0');
                parms1[j++] = new SqlParameter("@cpfrate", Utility.ToString("0"));
                parms1[j++] = new SqlParameter("@fundname", '-');
                parms1[j++] = new SqlParameter("@fundamount", '0');
                parms1[j++] = new SqlParameter("@fundgrossamount", '0');
                parms1[j++] = new SqlParameter("@nhhrs", '0');
                parms1[j++] = new SqlParameter("@hourlyrate", '0');
                parms1[j++] = new SqlParameter("@daysworkedrate", '0');
                parms1[j++] = new SqlParameter("@addtionprorated", '0');
                dsAddded = DataAccess.ExecuteSPDataSet(sSQL1, parms1);

                foreach (DataRow dremp in dsAddded.Tables[1].Rows)
                {
                    if (drnew["Desc"].ToString().Trim() == dremp["AddType"].ToString().Replace(':', ' ').Trim())
                    {
                        drnew.BeginEdit();
                        drnew["trx_amount"] = dremp["AddAmt"].ToString();
                        drnew.AcceptChanges();
                    }
                }
            }


            //Update Values to rows 
            foreach (DataRow dr1 in ds.Tables[0].Rows)
            {
                string emp_code = dr1["emp_code"].ToString();

                foreach (DataRow dr5 in dsDecution.Tables[0].Rows)
                {
                    string colname = dr5["Desc"].ToString();

                    if (dr5["emp_code"].ToString() == emp_code)
                    {
                        dr1.BeginEdit();
                        dr1[colname] = dr5["trx_amount"].ToString();
                        dr1.AcceptChanges();
                    }
                }
            }


            //ds.Tables[0].Columns.Add("");
            //ds.Tables[0].Columns.Add("");
            //ds.Tables[0].Columns.Add("");
            //ds.Tables[0].Columns.Add("");

            //double fundgrossamount=0.00;
            //foreach(DataRow dr in ds.Tables[0].Rows)
            //{
            //      fundgrossamount= Convert.ToDouble(dr["sdlfundgrossamount"]);
            //      string sSql = "Select dbo.fn_getSDLAmount(" + fundgrossamount.ToString() + ")";
            //}
            //       // string sSql = "Select dbo.fn_getSDLAmount(" + fundgrossamount.ToString() + "," + intactualmonth + "," + Year + ")";
            //        ////////////////////////////////////////
            //        string strSqlDed = "; Select isnull(sum(SDL),0) SDLAmt From prepare_payroll_detail pd Inner Join prepare_payroll_hdr ph on pd.trx_id = ph.trx_id Where ([Status]='A' Or [Status]='G' Or [Status]='P') And Emp_ID=" + EmpId.ToString() + "And ph.end_period=(SELECT PaySubEndDate FROM   PayrollMonthlyDetail  WHERE  ROWID = (" + intmonth.ToString() + "))";
            //        DataSet dsSDL = new DataSet();
            //        dsSDL = DataAccess.FetchRS(CommandType.Text, (sSql+strSqlDed), null);
            //        if (dsSDL != null)
            //        {
            //            if (dsSDL.Tables[0].Rows.Count > 0)
            //            {
            //                if (Utility.ToDouble(dsSDL.Tables[0].Rows[0][0].ToString()) <= 0)
            //                {

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{

            //    dr.BeginEdit();
            //    if (dr["fundtype"].ToString() == "SINDA")
            //    {
            //        dr["SINDA"] = dr["fundamount"];
            //        dr["MBMF"] = "0.0";
            //        dr["CDAC"] = "0.0";
            //        dr["ECF"] = "0.0";
            //    }
            //    if (dr["fundtype"].ToString() == "MBMF")
            //    {
            //        dr["MBMF"] = dr["fundamount"];
            //        dr["SINDA"] = "0.0";
            //        dr["CDAC"] = "0.0";
            //        dr["ECF"] = "0.0";
            //    }

            //    if (dr["fundtype"].ToString() == "CDAC")
            //    {
            //        dr["CDAC"] = dr["fundamount"];
            //        dr["SINDA"] = "0.0";
            //        dr["MBMF"] = "0.0";
            //        dr["ECF"] = "0.0";

            //    }

            //    if (dr["fundtype"].ToString() == "ECF")
            //    {
            //        dr["ECF"] = dr["fundamount"];
            //        dr["SINDA"] = "0.0";
            //        dr["MBMF"] = "0.0";
            //        dr["CDAC"] = "0.0";

            //    }
            //    dr.AcceptChanges();
            //}

            //Create A dataset for 

            DataSet final = new DataSet();

            DataTable dtfinal = new DataTable();

            DataColumn column27 = new DataColumn("time_card_no", typeof(System.String));
            column27.Caption = "time_card_no";
            dtfinal.Columns.Add(column27);

            DataColumn column28 = new DataColumn("deptname", typeof(System.String));
            column28.Caption = "deptname";
            dtfinal.Columns.Add(column28);

            DataColumn column1= new DataColumn("emp_code",typeof(System.Int32));
            column1.Caption="Emp_Code";
            dtfinal.Columns.Add(column1);

            DataColumn column2 = new DataColumn("fullname", typeof(System.String));
            column2.Caption = "Full Name";
            dtfinal.Columns.Add(column2);

            DataColumn column3 = new DataColumn("Basic", typeof(System.Double));
            column3.Caption = "BASIC PAY";
            dtfinal.Columns.Add(column3);

            //km
            DataColumn column29 = new DataColumn("DailyEmp(DH+OT1+OT2)(Amt)", typeof(System.String));
            column29.Caption = "DailyEmp(DH+OT1+OT2)";
            dtfinal.Columns.Add(column29);

            DataColumn column30 = new DataColumn("HourlyEmp(NH+OT1+OT2)(Amt)", typeof(System.String));
            column30.Caption = "HourlyEmp(NH+OT1+OT2)";
            dtfinal.Columns.Add(column30);







            //new
            DataColumn column_unpaidleaveAmt = new DataColumn("unpaidleaveAmt", typeof(System.Double));
            column_unpaidleaveAmt.Caption = "unpaidleaveAmt";
            dtfinal.Columns.Add(column_unpaidleaveAmt);

            DataColumn column13 = new DataColumn("unpaidleaves", typeof(System.Double));
            column13.Caption = "unpaidleaves";
            dtfinal.Columns.Add(column13);


          


            DataColumn column24 = new DataColumn("nh", typeof(System.Double));
            column24.Caption = "nh";
            dtfinal.Columns.Add(column24);

            DataColumn column4 = new DataColumn("ot1", typeof(System.Double));
            column4.Caption = "OT1";
            dtfinal.Columns.Add(column4);

            DataColumn column5 = new DataColumn("ot2", typeof(System.Double));
            column5.Caption = "OT2";            
            dtfinal.Columns.Add(column5);
          
            foreach (DataRow dr in dsAddTypes.Tables[0].Rows)
            {
                //string colmnname8 = "column" + p.ToString();
                string colname1 = dr["Desc"].ToString();
                DataColumn colmnname = new DataColumn(colname1, typeof(System.Double));
                column1.Caption = colname1;
                dtfinal.Columns.Add(colmnname);
                //ds.Tables[0].Columns.Add(dr["Desc"].ToString());
            }

            DataColumn column6 = new DataColumn("totaladditions", typeof(System.Double));
            column6.Caption = "totaladditions";
            dtfinal.Columns.Add(column6);

            //DataColumn column7 = new DataColumn("grosswithaddition", typeof(System.Double));
            //column7.Caption = "grosswithaddition";
            //dtfinal.Columns.Add(column7);


            DataColumn column8 = new DataColumn("employeecpfamt", typeof(System.Double));
            column8.Caption = "employeecpfamt";
            dtfinal.Columns.Add(column8);

            DataColumn column9 = new DataColumn("SINDA", typeof(System.Double));
            column9.Caption = "SINDA";
            dtfinal.Columns.Add(column9);

            DataColumn column10 = new DataColumn("MBMF", typeof(System.Double));
            column10.Caption = "MBMF";
            dtfinal.Columns.Add(column10);

            DataColumn column11 = new DataColumn("CDAC", typeof(System.Double));
            column11.Caption = "CDAC";
            dtfinal.Columns.Add(column11);

            DataColumn column12 = new DataColumn("ECF", typeof(System.Double));
            column12.Caption = "ECF";
            dtfinal.Columns.Add(column12);


            //DataColumn column13 = new DataColumn("unpaidleaves", typeof(System.Double));
            //column13.Caption = "unpaidleaves";
            //dtfinal.Columns.Add(column13);

            foreach (DataRow dr in dsdeductype.Tables[0].Rows)
            {
                //ds.Tables[0].Columns.Add(dr["Desc"].ToString());
                //string colmnname8 = "column" + p.ToString();
                string colname1 = dr["Desc"].ToString();
                DataColumn colmnname = new DataColumn(colname1, typeof(System.Double));
                column1.Caption = colname1;
                dtfinal.Columns.Add(colmnname);
            }

            DataColumn column14 = new DataColumn("totaldeductions", typeof(System.Double));
            column14.Caption = "totaldeductions";
            dtfinal.Columns.Add(column14);

            DataColumn column15 = new DataColumn("netpay", typeof(System.Double));
            column15.Caption = "netpay";
            dtfinal.Columns.Add(column15);


            DataColumn column26 = new DataColumn("cpfgross1", typeof(System.Double));
            column26.Caption = "cpfgross1";
            dtfinal.Columns.Add(column26);

            DataColumn column16 = new DataColumn("employercpfamt", typeof(System.Double));
            column16.Caption = "employercpfamt";
            dtfinal.Columns.Add(column16);

            //DataColumn column17 = new DataColumn("cpfamount", typeof(System.Double));
            //column17.Caption = "cpfamount";
            //dtfinal.Columns.Add(column17);

            DataColumn column18 = new DataColumn("SDL", typeof(System.Double));
            column18.Caption = "SDL";
            dtfinal.Columns.Add(column18);

            DataColumn column19 = new DataColumn("sdlfundgrossamount", typeof(System.Double));
            column19.Caption = "sdlfundgrossamount";
            dtfinal.Columns.Add(column19);

            DataColumn column20 = new DataColumn("FundGrossAmount", typeof(System.Double));
            column20.Caption = "FundGrossAmount";
            dtfinal.Columns.Add(column20);


            DataColumn column21 = new DataColumn("sdf_required", typeof(System.Double));
            column21.Caption = "sdf_required";
            dtfinal.Columns.Add(column21);

            DataColumn column22 = new DataColumn("pr_date", typeof(System.String));
            column22.Caption = "pr_date";
            dtfinal.Columns.Add(column22);

            DataColumn column23 = new DataColumn("fundtype", typeof(System.String));
            column23.Caption = "fundtype";
            dtfinal.Columns.Add(column23);

            DataColumn column25 = new DataColumn("fundamount", typeof(System.String));
            column25.Caption = "fundamount";
            dtfinal.Columns.Add(column25);



            
           

            
            final.Tables.Add(dtfinal);

            //Add EMployee Details 

            //Update Values to rows 
            foreach (DataRow dr1 in ds.Tables[0].Rows)
            {
                DataRow drow = final.Tables[0].NewRow();
                drow["emp_code"] = dr1["emp_code"].ToString();
                drow["fullname"] = dr1["fullname"].ToString();
                drow["Basic"] = DBNull.Value.Equals(dr1["Basic"]) ? 0.00m : dr1["Basic"];
                drow["ot1"] = dr1["ot1"].ToString();
                drow["ot2"] = dr1["ot2"].ToString();
                drow["nh"] = dr1["nh"].ToString();

                drow["totaladditions"] = DBNull.Value.Equals(dr1["totaladditions"]) ? 0.00 : dr1["totaladditions"];
                //drow["grosswithaddition"] = dr1["grosswithaddition"].ToString();
                drow["employeecpfamt"] = dr1["employeecpfamt"].ToString();
                drow["unpaidleaves"] = dr1["unpaidleaves"].ToString();
                drow["totaldeductions"] = DBNull.Value.Equals(dr1["totaldeductions"]) ? 0.00m : dr1["totaldeductions"];
                drow["netpay"] = DBNull.Value.Equals(dr1["netpay"]) ? 0.00m : dr1["netpay"];
                drow["employercpfamt"] = dr1["employercpfamt"].ToString();
                //drow["cpfamount"] = dr1["cpfamount"].ToString();
                drow["sdlfundgrossamount"] = dr1["sdlfundgrossamount"].ToString();
                drow["FundGrossAmount"] = dr1["FundGrossAmount"].ToString();
                drow["sdf_required"] = dr1["sdf_required"].ToString();
                drow["pr_date"] = dr1["pr_date"].ToString();
                drow["fundtype"] = dr1["fundtype"].ToString();
                drow["fundamount"] = dr1["fundamount"].ToString();
                drow["cpfgross1"] = dr1["cpfgross1"].ToString();
                drow["time_card_no"] = dr1["time_card_no"].ToString();
                drow["deptname"] = dr1["deptname"].ToString();

                //new
                drow["unpaidleaveAmt"] = DBNull.Value.Equals(dr1["totalunpaid"])? 0.00m :dr1["totalunpaid"];


                drow["DailyEmp(DH+OT1+OT2)(Amt)"] = Utility.ToDouble(dr1["daysworkedrate"].ToString()) + Utility.ToDouble(dr1["ot1"].ToString()) + Utility.ToDouble(dr1["ot2"].ToString());

                drow["HourlyEmp(NH+OT1+OT2)(Amt)"] = Utility.ToDouble(dr1["nh"].ToString()) + Utility.ToDouble(dr1["ot1"].ToString()) + Utility.ToDouble(dr1["ot2"].ToString());
           



                //km
                //if (Utility.ToDouble(dr1["nhhrs"].ToString()) > 0 & Utility.ToDouble(dr1["days_work"].ToString())==0)
                //{

                //    drow["Basic/Daily/hr"] = Utility.ToDouble(dr1["nh"].ToString()) + Utility.ToDouble(dr1["ot1"].ToString()) + Utility.ToDouble(dr1["ot2"].ToString());
                //}
                //else if (Utility.ToDouble(dr1["nhhrs"].ToString()) == 0 & Utility.ToDouble(dr1["days_work"].ToString()) > 0)
                //{
                //    drow["Basic/Daily/hr"] = Utility.ToDouble(dr1["daysworkedrate"].ToString()) + Utility.ToDouble(dr1["ot1"].ToString()) + Utility.ToDouble(dr1["ot2"].ToString());
                //}
                //else
                //{
                //    drow["Basic/Daily/hr"] = "0.00";
                //}



             







                final.Tables[0].Rows.Add(drow);
            }


            //Update Values to rows 
            foreach (DataRow dr1 in final.Tables[0].Rows)
            {
                string emp_code = dr1["emp_code"].ToString();
                double totaladd = 0.0;
                foreach (DataRow dr5 in dsadditions.Tables[0].Rows)
                {
                    string colname = dr5["Desc"].ToString();

                    if (dr5["emp_code"].ToString() == emp_code)
                    {
                        dr1.BeginEdit();
                        dr1[colname] = dr5["trx_amount"].ToString();
                        totaladd = totaladd + Convert.ToDouble(dr5["trx_amount"].ToString());
                        dr1.AcceptChanges();
                    }
                }
                totaladd = totaladd + Convert.ToDouble(dr1["nh"].ToString()) + Convert.ToDouble(dr1["ot1"].ToString()) + Convert.ToDouble(dr1["ot2"].ToString()); 
                dr1["totaladditions"] = totaladd.ToString();
            }



            //Update Values to rows 
            foreach (DataRow dr1 in final.Tables[0].Rows)
            {
                string emp_code = dr1["emp_code"].ToString();

                foreach (DataRow dr5 in dsDecution.Tables[0].Rows)
                {
                    string colname = dr5["Desc"].ToString();

                    if (dr5["emp_code"].ToString() == emp_code)
                    {
                        dr1.BeginEdit();
                        dr1[colname] = dr5["trx_amount"].ToString();
                        dr1.AcceptChanges();
                    }
                }
            }

            //Changes for CpfAmount

            foreach (DataRow dr in final.Tables[0].Rows)
            {
                double cpfamnt = 0.0;
                dr.BeginEdit();
                if (dr["employercpfamt"].ToString() != "")
                {
                    cpfamnt = Convert.ToDouble(dr["employercpfamt"].ToString());
                    if (cpfamnt <= 1)
                    {
                        dr["employercpfamt"] = "0";
                    }
                }

                //cpfamnt = 0;                
                //if (dr["cpfamount"].ToString() != "")
                //{
                //    cpfamnt = Convert.ToDouble(dr["cpfamount"].ToString());
                //    if (cpfamnt <= 1)
                //    {
                //        dr["cpfamount"] = "0";
                //    }
                //}

                double netpay = 0.0;
                if (dr["netpay"].ToString() != "")
                {
                    cpfamnt = Convert.ToDouble(dr["netpay"].ToString());
                    if (cpfamnt <= 1)
                    {
                        dr["netpay"] = "0";
                    }
                } 
         
                //Fund Type
                string fundtype = dr["fundtype"].ToString();

                if (fundtype == "SINDA")
                {
                    dr["SINDA"] = dr["fundamount"].ToString();//fundamount
                    dr["MBMF"] = "0";
                    dr["CDAC"] = "0";
                    dr["ECF"] = "0";
                }

                if (fundtype == "MBMF")
                {
                    dr["MBMF"] = dr["fundamount"].ToString();//fundamount
                    dr["SINDA"] = "0";
                    dr["CDAC"] = "0";
                    dr["ECF"] = "0";

                }

                if (fundtype == "CDAC")
                {
                    dr["CDAC"] = dr["fundamount"].ToString();//fundamount
                    dr["SINDA"] = "0";
                    dr["MBMF"] = "0";
                    dr["ECF"] = "0";

                }

                if (fundtype == "ECF")
                {
                    dr["ECF"] = dr["fundamount"].ToString();//fundamount
                    dr["SINDA"] = "0";
                    dr["MBMF"] = "0";
                    dr["CDAC"] = "0";
                }

                //SDL calcuations

                //    string strprdate = e.Row.Cells[21].Text;
                string sdfRequired = dr["sdf_required"].ToString();

              
              double sdlamount = Utility.ToDouble(dr["sdlfundgrossamount"].ToString());
                if (sdfRequired.Trim() == "2")
                {
                    if (sdlamount > 0)
                    {
                        sdlamount = Utility.ToDouble(dr["SDLFundGrossAmount"].ToString());
                    }
                    else
                    {
                        sdlamount = Utility.ToDouble(dr["FundGrossAmount"].ToString());
                    }
                }

                //    DateTime dbPrdate1 = new DateTime();
                //    if (strprdate != "")
                //    {
                //        //dbPrdate1 = Convert.ToDateTime(strprdate);
                //    }
                //    /********************************************************************************************/


                    if (sdlamount!=0)
                    {
                        int intmonth = Utility.ToInteger(Request.QueryString["monthidintbl"]) - 1;
                        double fundgrossamount = Utility.ToDouble(sdlamount);
                        //string sSql = "Select dbo.fn_getSDLAmount(" + fundgrossamount.ToString() + ")";

                        //////////////////////////////////////////Get Payroll Month
                        string sqlmonth = "SELECT distinct Month FROM payrollmonthlydetail WHERE ROWID=" + intmonth;
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
                        string strSqlDed = "; Select isnull(sum(SDL),0) SDLAmt From prepare_payroll_detail pd Inner Join prepare_payroll_hdr ph on pd.trx_id = ph.trx_id Where ([Status]='A' Or [Status]='G' Or [Status]='P') And Emp_ID=" + dr["emp_code"].ToString() + " And ph.end_period=(SELECT top 1 PaySubEndDate FROM   PayrollMonthlyDetail  WHERE  ROWID = (" + intmonth.ToString() + "))";

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
                                            dr["SDL"] = "0";
                                        }
                                        else
                                        {
                                            dr["SDL"] = sdlAmount.ToString();
                                        }
                                    }
                                    else
                                    {
                                        dr["SDL"] = dsSDL.Tables[0].Rows[0][0].ToString();
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
                dr.AcceptChanges();
            }
            final.Tables[0].Columns.Remove("sdf_required");
            final.Tables[0].Columns.Remove("pr_date");
            final.Tables[0].Columns.Remove("fundtype");
            final.Tables[0].Columns.Remove("fundamount");
            final.Tables[0].Columns.Remove("FundGrossAmount");
            final.Tables[0].Columns.Remove("sdlfundgrossamount");

            columncount = final.Tables[0].Columns.Count;
           
            gridPayDetailReport.DataSource = final;
            gridPayDetailReport.DataBind();
            
        }


       protected void CustomersGridView_RowDataBound(Object sender, GridViewRowEventArgs e)
        {


           
           //IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    string fundtype = e.Row.Cells[22].Text;
            //    double cpfamount =0.00;

            //    if (e.Row.Cells[16].Text != "")
            //    {
            //        cpfamount = Convert.ToDouble(e.Row.Cells[16].Text);
            //    }
            //    if (cpfamount < 1)
            //    {
            //        e.Row.Cells[16].Text = "0";
            //    }

            //    if (fundtype == "SINDA")
            //    {
            //        e.Row.Cells[9].Text = "0";
            //        e.Row.Cells[10].Text = "0";
            //        e.Row.Cells[11].Text = "0";
            //    }

            //    if (fundtype == "MBMF")
            //    {
            //        e.Row.Cells[8].Text = "0";
            //        e.Row.Cells[10].Text = "0";
            //        e.Row.Cells[11].Text = "0";
            //    }

            //    if (fundtype == "CDAC")
            //    {
            //        e.Row.Cells[8].Text = "0";
            //        e.Row.Cells[9].Text = "0";
            //        e.Row.Cells[11].Text = "0";
            //    }

            //    if (fundtype == "ECF")
            //    {
            //        e.Row.Cells[8].Text = "0";
            //        e.Row.Cells[9].Text = "0";
            //        e.Row.Cells[10].Text = "0";
            //    }

            //    string strprdate = e.Row.Cells[21].Text;
            //    string sdfRequired = e.Row.Cells[20].Text;

            //    double sdlamount = Utility.ToDouble(e.Row.Cells[18].Text);
            //    if (sdfRequired.Trim() == "2")
            //    {
            //        if (sdlamount > 0)
            //        {
            //            sdlamount = Utility.ToDouble(e.Row.Cells[18].Text);
            //        }
            //        else
            //        {
            //            sdlamount = Utility.ToDouble(e.Row.Cells[19].Text);
            //        }
            //    }

            //    DateTime dbPrdate1 = new DateTime();
            //    if (strprdate != "")
            //    {
            //        //dbPrdate1 = Convert.ToDateTime(strprdate);
            //    }
            //    /********************************************************************************************/


            //    if (sdlamount!=0)
            //    {
            //        int intmonth = Utility.ToInteger(Request.QueryString["monthidintbl"]) - 1;
            //        double fundgrossamount = Utility.ToDouble(sdlamount);
            //        //string sSql = "Select dbo.fn_getSDLAmount(" + fundgrossamount.ToString() + ")";

            //        //////////////////////////////////////////Get Payroll Month
            //        string sqlmonth = "SELECT Month FROM payrollmonthlydetail WHERE ROWID=" + intmonth;
            //        SqlDataReader dr9;
            //        dr9 = DataAccess.ExecuteReader(CommandType.Text, sqlmonth, null);
            //        int intactualmonth = 1;
            //        while (dr9.Read())
            //        {
            //            intactualmonth = Convert.ToInt32(dr9[0].ToString());
            //        }
            //        string sSql = "Select dbo.fn_getSDLAmount(" + fundgrossamount.ToString() + ")";
            //        // string sSql = "Select dbo.fn_getSDLAmount(" + fundgrossamount.ToString() + "," + intactualmonth + "," + Year + ")";
            //        ////////////////////////////////////////
            //        string strSqlDed = "; Select isnull(sum(SDL),0) SDLAmt From prepare_payroll_detail pd Inner Join prepare_payroll_hdr ph on pd.trx_id = ph.trx_id Where ([Status]='A' Or [Status]='G' Or [Status]='P') And Emp_ID=" + e.Row.Cells[0].Text + "And ph.end_period=(SELECT PaySubEndDate FROM   PayrollMonthlyDetail  WHERE  ROWID = (" + intmonth.ToString() + "))";

            //        DataSet dsSDL = new DataSet();
            //        dsSDL = DataAccess.FetchRS(CommandType.Text, (sSql + strSqlDed), null);
            //        if (dsSDL != null)
            //        {
            //            if (dsSDL.Tables[0].Rows.Count > 0)
            //            {
            //                if (Utility.ToDouble(dsSDL.Tables[0].Rows[0][0].ToString()) <= 0)
            //                {
            //                    //if (intmonth == 12)
            //                    //{
            //                    //  trsdl.Attributes.Add("style", "display:block");
            //                    //}
            //                    //else
            //                    //{
            //                    //  trsdl.Attributes.Add("style", "display:none");
            //                    //}
            //                }
            //                else
            //                {
            //                    //trsdl.Attributes.Add("style", "display:block");
            //                    if (Utility.ToInteger(Session["PaySubStartDay"].ToString()) > 1)
            //                    {
            //                        double sdlAmount = Utility.ToDouble(Utility.ToDouble(dsSDL.Tables[0].Rows[0][0].ToString()) - Utility.ToDouble(dsSDL.Tables[1].Rows[0][0].ToString()));
            //                        if (sdlAmount < 0)
            //                        {
            //                            e.Row.Cells[17].Text = "0";
            //                        }
            //                        else
            //                        {
            //                            e.Row.Cells[17].Text = sdlAmount.ToString();
            //                        }
            //                    }
            //                    else
            //                    {
            //                        e.Row.Cells[17].Text = dsSDL.Tables[0].Rows[0][0].ToString();
            //                    }
            //                    //If PR Date is >1 then overwrite SDL with old salary ...
            //                    //if (dtPRdate1 != null)
            //                    //{
            //                    //    //stmonth
            //                    //    // dtTerm = DateTime.Parse(dr["Term_Date"].ToString(), format);
            //                    //    //lblTermDate.Text = dtTerm.ToString("dd/MM/yyyy");
            //                    //    //Check for Month processing salary
            //                    //    string stmonth = Utility.ToString(Request.QueryString["stdatesubmonth"]);
            //                    //    DateTime dt1 = DateTime.Parse(stmonth, format);
            //                    //    if (dtPRdate1.Date.Day > 1 && dtPRdate1.Date.Month == dt1.Month && dtPRdate1.Date.Year == dt1.Year)
            //                    //    {
            //                    //        e.Row.Cells[1].Text = dsSDL.Tables[1].Rows[0][0].ToString();
            //                    //    }
            //                    //}
            //                    //}
            //                }
            //            }
            //        }
            //    }


            //}
        }

        protected void gridPayDetailReport_DataBound(object sender, EventArgs e)
        {
            

            if (gridPayDetailReport.Rows.Count > 0)
            {

              
                int TotalRows = gridPayDetailReport.Rows.Count;
                int TotalCol = gridPayDetailReport.Rows[0].Cells.Count;
                int FixedCol = 4;
                int ComputedCol =  FixedCol;

                gridPayDetailReport.FooterRow.Cells[FixedCol - 1].Text = "Total : ";

                for (int i = 4; i < columncount ; i++)
                {
                    double sum = 0.000;

                    for (int j = 0; j < TotalRows; j++)
                    {
                        if (gridPayDetailReport.Rows[j].Cells[i].Text != "")
                        {
                            sum += gridPayDetailReport.Rows[j].Cells[i].Text != "&nbsp;" ? double.Parse(gridPayDetailReport.Rows[j].Cells[i].Text) : 0.000;
                        }
                    }
                    gridPayDetailReport.FooterRow.Cells[i].Text = sum.ToString("#.00");
                }
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string attachment = "attachment; filename=PayrollDetails.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gridPayDetailReport.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            //confirms that an HtmlForm control is rendered for the
            //specified ASP.NET server control at run time.
        }

       


    }
}