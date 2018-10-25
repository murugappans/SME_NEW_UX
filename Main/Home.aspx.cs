using Newtonsoft.Json;
using SMEPayroll.Main.Service;
using SMEPayroll.Main.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace SMEPayroll.Main
{
    public partial class Home : System.Web.UI.Page
    {
        static string emp_code = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            string sqlStr = "";
           emp_code = Session["EmpCode"].ToString();
        }

        [System.Web.Services.WebMethod]
        public static string GetEmployees()
        {
            string compid = HttpContext.Current.Session["Compid"].ToString();
            DataSet count = new DataSet();
            int countbd = 0,countemponleave =0, countEmpEndProb=0, countEmpCompletionYear=0, countTotalemployee=0;
            string ssql = "SELECT isnull(sum(Totalemp),0) as Totalemp, isnull(sum(newemp),0) as newemp,isnull(sum(terminatedemp),0) as terminatedemp,isnull(sum(prevyearactiveemp),0) as prevyearactiveemp,isnull(sum(newempprevyear),0) as newempprevyear,isnull(sum(terminatedempprevyear),0) as terminatedempprevyear   " +
                           "FROM " +
                           "( " +
                           "SELECT " +
                           "CASE WHEN StatusId in (1) AND termination_date IS NULL THEN count(StatusId) ELSE NULL END as Totalemp, " +
                           "CASE WHEN year(joining_date) = YEAR(getdate()) THEN count(joining_date) ELSE NULL END as newemp,  " +
                           "CASE WHEN year(termination_date) = year(DATEADD(year,-1,GETDATE())) THEN count(termination_date) ELSE NULL END as terminatedempprevyear,  " +
                           "CASE WHEN year(termination_date) = YEAR(getdate()) THEN count(termination_date) ELSE NULL END as terminatedemp,  " +
                           "CASE WHEN StatusId =1 and year(joining_date) != YEAR(getdate()) THEN count(emp_type) ELSE NULL END as prevyearactiveemp,   " +
                           "CASE WHEN year(joining_date) = year(DATEADD(year,-1,GETDATE())) THEN count(joining_date) ELSE NULL END as newempprevyear   " +
                           "from employee " +
                           "where Company_Id = " + compid + " " +
                           "group by StatusId, sex,emp_type,date_of_birth,joining_date,termination_date) x " +
                           "where  Totalemp is not null or newemp is not null or terminatedemp is not null or prevyearactiveemp is not null or newempprevyear  is not null or terminatedempprevyear is not null; ";
            count = DataAccess.FetchRS(CommandType.Text, ssql, null);
            ssql = "SELECT Count(*) as WholeEmployee from employee where Company_Id = " + compid;
            countTotalemployee =  DataAccess.ExecuteScalar(ssql);
           
            ssql = "Select Count(emp_code) from employee where month(date_of_birth) = month(getdate()) and Company_Id = " + compid; ;
            
            countbd = DataAccess.ExecuteScalar(ssql);

            ssql = "select Count( distinct c.time_card_no) as TimeCardNo from emp_leaves a,emp_leaves_detail b,employee c ,leave_types d  " +
"where b.trx_id = a.trx_id and a.emp_id = c.emp_code and a.leave_type = d.id and a.status = 'Approved'  " +
 "and month(b.leave_date) = month(getdate())  and c.termination_date is null and c.company_id = 4  ";
           
            countemponleave= DataAccess.ExecuteScalar(ssql);

            ssql = "select Count(*) ";            
            ssql += "from employee where company_id = " + compid.ToString() + " AND termination_date is null   AND ";
            ssql += "  (datediff(dd, GETDATE(), dateadd(month, probation_period, joining_date)) <= (select[Days] from[Remainder_Day] where Sno = 8 and Company_Id = "+ compid.ToString()+") ";
            ssql += "and datediff(dd, GETDATE(), dateadd(month, probation_period, joining_date))> 0)  and probation_period<> -1 AND confirmation_date is null and month(dateadd(month, probation_period, joining_date)) = month(GETDATE())" ;
           
            countEmpEndProb = DataAccess.ExecuteScalar(ssql);

            ssql = "select Count( time_card_no) from employee where company_id=" + compid.ToString() + " AND termination_date is null   AND datediff(YY,JOINING_DATE,GETDATE()) >= 0  AND ((Select Month(JOINING_DATE))-(Select Month(GETDATE()))) =0  AND  datediff(YY,JOINING_DATE,GETDATE())>0  ";
            countEmpCompletionYear = DataAccess.ExecuteScalar(ssql);

            return JsonConvert.SerializeObject(new {  Totalemp = count.Tables[0].Rows[0]["Totalemp"],
                newemp = count.Tables[0].Rows[0]["newemp"], terminatedemp = count.Tables[0].Rows[0]["terminatedemp"],
                prevyearactiveemp = count.Tables[0].Rows[0]["prevyearactiveemp"], newempprevyear = count.Tables[0].Rows[0]["newempprevyear"],
                terminatedempprevyear = count.Tables[0].Rows[0]["terminatedempprevyear"], countBday = countbd,
                countemponleave= countemponleave,countEmpEndProb = countEmpEndProb, countEmpCompletionYear = countEmpCompletionYear,
                countTotalemployee = countTotalemployee
            });
        }

        [System.Web.Services.WebMethod]
        public static string GetAllEventList()
        {
            List<EmployeeData> dsList = EmployeeDataService.GetAllEventList();
            return JsonConvert.SerializeObject(dsList);
        }
        [System.Web.Services.WebMethod]
        public static string GetPHList()
        {
            List<EmployeeData> dsList = EmployeeDataService.GetPHList();
            return JsonConvert.SerializeObject(dsList);
        }
        [System.Web.Services.WebMethod]
        public static string GetBirthdayList()
        {
            List<EmployeeData> dsList = EmployeeDataService.GetBirthdayList();
            return JsonConvert.SerializeObject(dsList);
        }

        [System.Web.Services.WebMethod]
        public static string GetPassportExpiryList()
        {
            List<EmployeeData> dsList = EmployeeDataService.GetPassportExpiryList();
            return JsonConvert.SerializeObject(dsList);
        }

        [System.Web.Services.WebMethod]
        public static string GetAllDocsExpiryList()
        {
            List<EmployeeData> dsList = EmployeeDataService.GetAllDocsExpiryList();
            return JsonConvert.SerializeObject(dsList);
        }


        [System.Web.Services.WebMethod]
        public static string GetProbationPeriodExpiryList()
        {
            List<EmployeeData> dsList = EmployeeDataService.GetProbationPeriodExpiryList();
            return JsonConvert.SerializeObject(dsList);
        }
    }
}