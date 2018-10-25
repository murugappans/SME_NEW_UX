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

namespace SMEPayroll.Payroll
{
    public partial class paydetailreport : System.Web.UI.Page
    {

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        public double tot_ot1;
        public double tot_ot2;
        public double tot_basicpay;
        public double tot_total_deductions;
        public double tot_total_additions;
        public double tot_netpay;
        public double tot_employercpf;
        public double tot_employeecpf;
        public double tot_fundamt;
        public double tot_grosspay;
        public double tot_cpfGrossPay;
        
        int compid;
        string month = "", year = "";
        protected ArrayList payrolllist;
        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            month = Request.QueryString["month"].ToString();
            year = Request.QueryString["year"].ToString();         

            FetchData();

        }

        private void FetchData()
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
            //string sSQL = "";
            //sSQL = "sp_generatepayroll";        // WHEN GENERAL PAYROLL
            //SqlParameter[] parms = new SqlParameter[4];
            //parms[0] = new SqlParameter("@company_id", compid);
            //parms[1] = new SqlParameter("@month", month);
            //parms[2] = new SqlParameter("@year", year);
            //parms[3] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"].ToString()));
            //SqlDataReader dr = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL, parms);
            int i = 0;
            string sSQL = "sp_GeneratePayRollAdv";
            string sSQLextData = "";
            SqlParameter[] parms = new SqlParameter[11];
            parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(CompanyID));
            parms[i++] = new SqlParameter("@month", Utility.ToInteger(Month));
            parms[i++] = new SqlParameter("@year", Utility.ToInteger(Year));
            parms[i++] = new SqlParameter("@stdatemonth", Request.QueryString["stdatemonth"]);
            parms[i++] = new SqlParameter("@endatemonth", Request.QueryString["endatemonth"]);
            parms[i++] = new SqlParameter("@stdatesubmonth", Request.QueryString["stdatesubmonth"]);
            parms[i++] = new SqlParameter("@endatesubmonth", Request.QueryString["endatesubmonth"]);
            parms[i++] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"].ToString()));
            if (EmpPassID == "")
            {
                parms[i++] = new SqlParameter("@EmpPassID", "");
            }
            else
            {
                parms[i++] = new SqlParameter("@EmpPassID", EmpPassID);
            }
            parms[i++] = new SqlParameter("@DeptId", Utility.ToInteger(Request.QueryString["dept_id"]));
            parms[i++] = new SqlParameter("@monthidintbl", Request.QueryString["monthidintbl"]);
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL, parms);
            payrolllist = new ArrayList();
            while (dr.Read())
            {
                SMEPayroll.Payroll.paylist opaylist = new paylist();
                opaylist.empname = Utility.ToString(dr["Fullname"].ToString());
                opaylist.basicpay = Utility.ToString(dr["ActualBasic"].ToString());
                opaylist.total_additions = Utility.ToString(dr["TotalAdditions"].ToString());
                opaylist.total_deductions = Utility.ToString(dr["TotalDeductions"].ToString());
                opaylist.ot1 = Utility.ToString(dr["OT1"].ToString());
                opaylist.ot2 = Utility.ToString(dr["OT2"].ToString());

                opaylist.netpay = Utility.ToString(dr["NetPay"].ToString());
                opaylist.employeecpf = Utility.ToString(dr["EmployeeCPFAmt"].ToString());
                opaylist.employercpf = Utility.ToString(dr["EmployerCPFAmt"].ToString());

                opaylist.fundtype = Utility.ToString(dr["FundType"].ToString());
                opaylist.fundamt = Utility.ToString(dr["FundAmount"].ToString());
                opaylist.cpfGrossPay = Utility.ToString(dr["CPFGross1"].ToString());
                opaylist.grosspay = Utility.ToString(dr["GrossWithAddition"].ToString());

                payrolllist.Add(opaylist);
                tot_ot1 = tot_ot1 + Utility.ToDouble(dr["OT1"].ToString());
                tot_ot2 = tot_ot2 + Utility.ToDouble(dr["OT2"].ToString());
                tot_basicpay = tot_basicpay + Utility.ToDouble(dr["ActualBasic"].ToString());
                tot_total_deductions = tot_total_deductions + Utility.ToDouble(dr["TotalDeductions"].ToString());
                tot_total_additions = tot_total_additions + Utility.ToDouble(dr["TotalAdditions"].ToString());
                tot_netpay = tot_netpay + Utility.ToDouble(dr["NetPay"].ToString());
                tot_employercpf = tot_employercpf + Utility.ToDouble(dr["EmployerCPFAmt"].ToString());
                tot_employeecpf = tot_employeecpf + Utility.ToDouble(dr["EmployeeCPFAmt"].ToString());
                tot_fundamt = tot_fundamt + Utility.ToDouble(dr["FundAmount"].ToString());
                tot_grosspay = tot_grosspay + Utility.ToDouble(dr["CPFGross"].ToString());
                tot_cpfGrossPay = tot_cpfGrossPay + Utility.ToDouble(dr["CPFGross1"].ToString());
            }        
        }       
    }
    public class paylist
    {
        public string empname;
        public string ot1;
        public string ot2;
        public string basicpay;
        public string total_deductions;
        public string total_additions;
        public string netpay;
        public string employercpf;
        public string employeecpf;
        public string fundtype;
        public string fundamt;
        public string grosspay;
        public string cpfGrossPay;
    }
}
