using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json;

namespace SMEPayroll.Employee
{
    public partial class Employee_Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            string sqlStr = "";
            string emp_code = Session["EmpCode"].ToString();

        }
        [System.Web.Services.WebMethod]
        public static string GetEmployees()
        {
            string compid = HttpContext.Current.Session["Compid"].ToString();
            DataSet count = new DataSet();
            string ssql = "SELECT isnull(sum(Male),0) as Male ,isnull(sum(Female),0) as Female,Cast(isnull((sum(Male) * 100.0) / sum(Totalemp), 0)AS DECIMAL(18, 2)) as Malepercent,Cast(isnull((sum(Female) * 100.0) / sum(Totalemp), 0)AS DECIMAL(18, 2)) as Femalepercent,isnull(sum(Totalemp),0) as Totalemp,isnull(sum(Localemp),0) as Localemp ,isnull(sum(Foreignemp),0) as Foreignemp,Cast(isnull((sum(Localemp)*100.0)/sum(Totalemp),0)AS DECIMAL(18, 2))  as localpercent,Cast(isnull((sum(Foreignemp)*100.0)/sum(Totalemp),0)AS DECIMAL(18, 2))  as foreignpercent,isnull(sum(GroupA),0) as GroupA ,isnull(sum(GroupB),0) as GroupB,isnull(sum(GroupC),0) as GroupC,Cast(isnull((sum(GroupA)*100.0)/sum(Totalemp),0) AS DECIMAL(18, 2))   as GroupApercent,Cast(isnull((sum(GroupB)*100.0)/sum(Totalemp),0)AS DECIMAL(18, 2))  as GroupBpercent,Cast( isnull((sum(GroupC)*100.0)/sum(Totalemp),0)AS DECIMAL(18, 2))  as GroupCpercent,isnull(sum(newemp),0) as newemp,isnull(sum(terminatedemp),0) as terminatedemp,isnull(sum(prevyearactiveemp),0) as prevyearactiveemp,isnull(sum(newempprevyear),0) as newempprevyear    " +
                           "FROM " +
                           "( " +
                           "SELECT " +
                           "CASE WHEN StatusId in (1)  and termination_date is null  and sex = 'M' THEN count(sex) ELSE NULL END as Male, " +
                           "CASE WHEN StatusId in (1)  and termination_date is null  and sex = 'F' THEN count(sex) ELSE NULL END as Female, " +
                           "CASE WHEN StatusId in (1)  and termination_date is null  and emp_type in('SC','SPR') THEN count(emp_type) ELSE NULL END as Localemp, " +
                           "CASE WHEN StatusId in (1)  and termination_date is null  and emp_type in('DP','EP','WP','SP','OT') THEN count(emp_type) ELSE NULL END as Foreignemp, " +
                           "CASE WHEN StatusId in (1)  and termination_date is null  and DATEDIFF(hour,date_of_birth,GETDATE())/8766 >=13 and DATEDIFF(hour,date_of_birth,GETDATE())/8766 <=30 THEN count(date_of_birth) ELSE NULL END as GroupA, " +
                           "CASE WHEN StatusId in (1)  and termination_date is null  and DATEDIFF(hour,date_of_birth,GETDATE())/8766 >=31 and DATEDIFF(hour,date_of_birth,GETDATE())/8766 <=50 THEN count(date_of_birth) ELSE NULL END as GroupB, " +
                           "CASE WHEN StatusId in (1)  and termination_date is null  and DATEDIFF(hour,date_of_birth,GETDATE())/8766 >=51 THEN count(date_of_birth) ELSE NULL END as GroupC, " +
                           "CASE WHEN StatusId in (1)  and termination_date is null  THEN count(StatusId) ELSE NULL END as Totalemp, " +
                           "CASE WHEN year(joining_date) = YEAR(getdate()) THEN count(joining_date) ELSE NULL END as newemp,  " +
                           "CASE WHEN year(termination_date) = YEAR(getdate()) THEN count(termination_date) ELSE NULL END as terminatedemp,  " +
                           "CASE WHEN StatusId =1 and year(joining_date) != YEAR(getdate()) THEN count(emp_type) ELSE NULL END as prevyearactiveemp,   " +
                           "CASE WHEN year(joining_date) = year(DATEADD(year,-1,GETDATE())) THEN count(joining_date) ELSE NULL END as newempprevyear   " +
                           "from employee " +
                           "where Company_Id = " + compid + " " +
                           "group by StatusId, sex,emp_type,date_of_birth,joining_date,termination_date) x " +
                           "where Male is not null or Female is not null or Localemp is not null or Foreignemp is not null or Totalemp is not null or GroupA is not null or GroupB is not null or GroupC is not null or newemp is not null or terminatedemp is not null or prevyearactiveemp is not null or newempprevyear  is not null; ";
            count = DataAccess.FetchRS(CommandType.Text, ssql, null);
            return JsonConvert.SerializeObject(new { Male = count.Tables[0].Rows[0]["Male"], Female = count.Tables[0].Rows[0]["Female"], Malepercent = count.Tables[0].Rows[0]["Malepercent"], Femalepercent = count.Tables[0].Rows[0]["Femalepercent"], Totalemp = count.Tables[0].Rows[0]["Totalemp"], Localemp = count.Tables[0].Rows[0]["Localemp"], Foreignemp = count.Tables[0].Rows[0]["Foreignemp"], localpercent = count.Tables[0].Rows[0]["localpercent"], foreignpercent = count.Tables[0].Rows[0]["foreignpercent"], GroupA = count.Tables[0].Rows[0]["GroupA"], GroupB = count.Tables[0].Rows[0]["GroupB"], GroupC = count.Tables[0].Rows[0]["GroupC"], GroupApercent = count.Tables[0].Rows[0]["GroupApercent"], GroupBpercent = count.Tables[0].Rows[0]["GroupBpercent"], GroupCpercent = count.Tables[0].Rows[0]["GroupCpercent"], newemp = count.Tables[0].Rows[0]["newemp"], terminatedemp = count.Tables[0].Rows[0]["terminatedemp"], prevyearactiveemp = count.Tables[0].Rows[0]["prevyearactiveemp"], newempprevyear = count.Tables[0].Rows[0]["newempprevyear"] });
        }
    }
}