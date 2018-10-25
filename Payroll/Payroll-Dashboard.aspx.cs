using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEPayroll.Payroll
{
    public partial class Payroll_Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
        }


        [System.Web.Services.WebMethod]
        public static string GetEmployeeData(string _filter)
        {
            string compid = HttpContext.Current.Session["Compid"].ToString();
            var _year = _filter.Split('|')[0];
            var _days = Convert.ToUInt32(_filter.Split('|')[1].Split('-')[0]);
            var _month = _filter.Split('-')[1];
            var _currentdate = Convert.ToDateTime(_days + "-" + _month + "-" + _year);

            DateTime _datefrom = _currentdate.AddDays(-_days);
            //string ssl = "select count(emp_code), joining_date,emp_status from employee where emp_status=1 and joining_date>='"+ _datefrom.ToString("yyyy-MM-dd") + "' and joining_date<='" + _datefrom.ToString("yyyy-MM-dd") + "' and termination_date is null and  Company_id = " + compid + "  group by joining_date,emp_status ";
            string ssl = ""; //"select count(emp_code) AS existingEmployees from employee where emp_status=1 and joining_date<='" + _datefrom.ToString("yyyy-MM-dd") + "' and termination_date is null and  Company_id = " + compid + "";

            ssl = "select count(emp_code) AS existingEmployees from employee where StatusId=1 and joining_date<='" + _datefrom.ToString("yyyy-MM-dd") + "' and termination_date is null and  Company_id = " + compid + " "
                    +"union all "
                    + "select count(emp_code)AS existingEmployees from employee where StatusId = 1 and joining_date >= '" + _datefrom.ToString("yyyy-MM-dd") + "' and joining_date <= '" + _currentdate.ToString("yyyy-MM-dd") + "'   and termination_date is null and  Company_id = " + compid + " "
                    + "union all "
                    + "select count(emp_code) AS existingEmployees from employee where StatusId = 2 and termination_date >= '" + _datefrom.ToString("yyyy-MM-dd") + "'  and termination_date <= '" + _currentdate.ToString("yyyy-MM-dd") + "' and  Company_id = " + compid + " "
                    + "union all "
                    + "select count(emp_code) AS existingEmployees from employee where StatusId = 1 and joining_date <= '" + _currentdate.ToString("yyyy-MM-dd") + "' and  Company_id = " + compid + "";

            DataSet emloyeedata = new DataSet();
            emloyeedata = DataAccess.FetchRS(CommandType.Text, ssl);
            System.Data.DataColumn newColumn = new System.Data.DataColumn("retMessage", typeof(System.String));
            newColumn.DefaultValue = _month + " " + _year;
            emloyeedata.Tables[0].Columns.Add(newColumn);
            return JsonConvert.SerializeObject(new { emloyeesdata = emloyeedata.Tables[0] });
        }


        [System.Web.Services.WebMethod]
        public static string GetEmployeeDataPrevious(string _filter)
        {
            string compid = HttpContext.Current.Session["Compid"].ToString();
            var _year = _filter.Split('|')[0];
            var _days = Convert.ToUInt32(_filter.Split('|')[1].Split('-')[0]);
            var _month = _filter.Split('-')[1];
            var _currentdate = Convert.ToDateTime(_days + "-" + _month + "-" + _year);
            _currentdate= _currentdate.AddDays(-_days);
            DateTime _datefrom = _currentdate.AddDays(-_days);
            //string ssl = "select count(emp_code), joining_date,emp_status from employee where emp_status=1 and joining_date>='"+ _datefrom.ToString("yyyy-MM-dd") + "' and joining_date<='" + _datefrom.ToString("yyyy-MM-dd") + "' and termination_date is null and  Company_id = " + compid + "  group by joining_date,emp_status ";
            string ssl = ""; //"select count(emp_code) AS existingEmployees from employee where emp_status=1 and joining_date<='" + _datefrom.ToString("yyyy-MM-dd") + "' and termination_date is null and  Company_id = " + compid + "";

            ssl = "select count(emp_code) AS existingEmployees from employee where StatusId=1 and joining_date<='" + _datefrom.ToString("yyyy-MM-dd") + "' and termination_date is null and  Company_id = " + compid + " "
                    + "union all "
                    + "select count(emp_code)AS existingEmployees from employee where StatusId = 1 and joining_date >= '" + _datefrom.ToString("yyyy-MM-dd") + "' and joining_date <= '" + _currentdate.ToString("yyyy-MM-dd") + "'   and termination_date is null and  Company_id = " + compid + " "
                    + "union all "
                    + "select count(emp_code) AS existingEmployees from employee where StatusId =2 and termination_date >= '" + _datefrom.ToString("yyyy-MM-dd") + "'  and termination_date <= '" + _currentdate.ToString("yyyy-MM-dd") + "' and  Company_id = " + compid + " "
                    + "union all "
                    + "select count(emp_code) AS existingEmployees from employee where StatusId = 1 and joining_date <= '" + _currentdate.ToString("yyyy-MM-dd") + "' and  Company_id = " + compid + "";

            DataSet emloyeedata = new DataSet();
            emloyeedata = DataAccess.FetchRS(CommandType.Text, ssl);

            System.Data.DataColumn newColumn = new System.Data.DataColumn("retMessage", typeof(System.String));

            var _prevMonth = Convert.ToDateTime(_days + "-" + _month + "-" + _year).AddMonths(-1).ToString("MMMM");

            newColumn.DefaultValue = _prevMonth + " " + _year;
            emloyeedata.Tables[0].Columns.Add(newColumn);


            return JsonConvert.SerializeObject(new { emloyeesdata = emloyeedata.Tables[0] });
        }


        [System.Web.Services.WebMethod]
        public static string GetDepartmentsData(string _filter)
        {

            string compid = HttpContext.Current.Session["Compid"].ToString();
            var _year = _filter.Split('|')[0];
            var _days = Convert.ToUInt32(_filter.Split('|')[1].Split('-')[0]);
            var _month = _filter.Split('-')[1];
            var _currentdate = Convert.ToDateTime(_days + "-" + _month + "-" + _year);
            DateTime _datefrom = _currentdate.AddDays(-_days);


            var _prevcurrentdate = _currentdate.AddDays(-_days);
            DateTime _prevdatefrom = _prevcurrentdate.AddDays(-_days);


            string ssl = "";

            //ssl = "SELECT t.DeptName, ISNULL(t.existingemployee, 0)AS existingemployee, ISNULL(u.newhiring, 0)AS newhiring, ISNULL(v.resigned, 0)AS resigned, ISNULL(w.total, 0)AS total "
            //   + "FROM "
            //   + "( "
            //       + "SELECT  d.DeptName, COUNT(emp_code) AS existingemployee "

            //       + "FROM employee e "

            //       + "inner join department d on e.dept_id = d.id "
            //      + "where StatusId = 1 and joining_date <= '" + _datefrom.ToString("yyyy-MM-dd") + "' and termination_date is null and  e.Company_id = " + compid + " "
            //     + "group by d.DeptName "
            //   + ") t "

            ssl = "SELECT t.DeptName, ISNULL(s.existingemployee, 0)AS existingemployee, ISNULL(u.newhiring, 0)AS newhiring, ISNULL(v.resigned, 0)AS resigned, ISNULL(w.total, 0)AS total, "
                   + "ISNULL(u2.pexistingemployee,0)as pexistingemployee, ISNULL(v2.pnewhiring,0)as pnewhiring, ISNULL(w2.presigned,0)as presigned ,ISNULL(x2.ptotal,0)as ptotal  "
                    + "FROM "
                    + "( "
                        + "SELECT  d.DeptName FROM employee e "
                        + "inner join department d on e.dept_id = d.id "
                       + "where StatusId = 1 and  e.Company_id = " + compid + " "
                      + "group by d.DeptName "
                    + ") t "

                   + "LEFT OUTER JOIN "
                    + "( "
                        + "SELECT  d.DeptName, COUNT(emp_code) AS existingemployee FROM employee e "
                        + "inner join department d on e.dept_id = d.id "
                     + "where StatusId = 1 and joining_date <= '" + _datefrom.ToString("yyyy-MM-dd") + "' and termination_date is null and  e.Company_id = " + compid + " "
                      + "group by d.DeptName "
                    + ") s ON t.DeptName = s.DeptName "


                    + "LEFT OUTER JOIN "
                    + "( "
                        + "SELECT  d.DeptName, COUNT(emp_code) AS newhiring FROM employee e "
                        + "inner join department d on e.dept_id = d.id "
                     + "where StatusId = 1 and joining_date >= '" + _datefrom.ToString("yyyy-MM-dd") + "' and termination_date is null and  e.Company_id = " + compid + " "
                      + "group by d.DeptName "
                    + ") u ON t.DeptName = u.DeptName "

                    + "LEFT OUTER JOIN "
                    + "( "
                        + "SELECT  d.DeptName, COUNT(emp_code) AS resigned FROM employee e "
                        + "inner join department d on e.dept_id = d.id "
                     + "where StatusId = 2 and termination_date >= '" + _datefrom.ToString("yyyy-MM-dd") + "'  and termination_date <= '" + _currentdate.ToString("yyyy-MM-dd") + "'  and  e.Company_id = " + compid + " "
                      + "group by d.DeptName "
                    + ") v ON t.DeptName = v.DeptName "

                    + "LEFT OUTER JOIN "
                    + "( "
                        + "SELECT  d.DeptName, COUNT(emp_code) AS total "

                        + "FROM employee e "

                        + "inner join department d on e.dept_id = d.id "
                     + "where StatusId = 1 and joining_date <= '" + _datefrom.ToString("yyyy-MM-dd") + "' and  e.Company_id = " + compid + " "
                      + "group by d.DeptName "
                    + ") w ON t.DeptName = w.DeptName "
                    //previous data starts
                    + "LEFT OUTER JOIN "
                    + "( "
                        + "SELECT  d.DeptName, COUNT(emp_code) AS pexistingemployee FROM employee e inner join department d on e.dept_id = d.id "
                        + "where StatusId = 1 and joining_date <= '" + _prevdatefrom.ToString("yyyy-MM-dd") + "' and termination_date is null and  e.Company_id = " + compid + " "
                      + "group by d.DeptName "
                    + ") u2 ON t.DeptName = u2.DeptName "

                      + "LEFT OUTER JOIN "
                    + "( "
                        + "SELECT  d.DeptName, COUNT(emp_code) AS pnewhiring FROM employee e inner join department d on e.dept_id = d.id "
                        + "where StatusId = 1 and joining_date >= '" + _prevdatefrom.ToString("yyyy-MM-dd") + "' and termination_date is null and  e.Company_id = " + compid + " "
                      + "group by d.DeptName "
                    + ") v2 ON t.DeptName = v2.DeptName "


                      + "LEFT OUTER JOIN "
                    + "( "
                        + "SELECT  d.DeptName, COUNT(emp_code) AS presigned FROM employee e inner join department d on e.dept_id = d.id "
                        + "where StatusId = 2 and termination_date >= '" + _prevdatefrom.ToString("yyyy-MM-dd") + "' and termination_date <= '" + _prevcurrentdate.ToString("yyyy-MM-dd") + "' and termination_date is null and  e.Company_id = " + compid + " "
                      + "group by d.DeptName "
                    + ") w2 ON t.DeptName = w2.DeptName "

                    + "LEFT OUTER JOIN "
                    + "( "
                        + "SELECT  d.DeptName, COUNT(emp_code) AS ptotal FROM employee e inner join department d on e.dept_id = d.id "
                        + "where StatusId = 1 and joining_date <= '" + _prevdatefrom.ToString("yyyy-MM-dd") + "' and  e.Company_id = " + compid + " "
                      + "group by d.DeptName "
                    + ") x2 ON t.DeptName = x2.DeptName ";




            DataSet emloyeedata = new DataSet();
            emloyeedata = DataAccess.FetchRS(CommandType.Text, ssl);

            
            return JsonConvert.SerializeObject(new { emloyeesdata = emloyeedata.Tables[0] });

        }
        
        [System.Web.Services.WebMethod]
        public static string GetPayrollSumGross(string _filter)
        {     
            var _year = _filter.Split('|')[0];
            var _days =Convert.ToUInt32(_filter.Split('|')[1].Split('-')[0]);
            var _month = _filter.Split('-')[1];
            var _currentdate = Convert.ToDateTime(_days + "-" + _month + "-" + _year);
            DateTime _currentdateFrom = _currentdate.AddDays(-_days);
            DateTime _prevdateFrom = _currentdateFrom.AddDays(-_days);                                                                                                   

            var ssl = "SELECT  isnull(sum(d.total_gross),0) + isnull(sum(d.employerCPF),0) AS currentGross,isnull(sum(isnull(Convert(numeric(10, 2), convert(varchar(10), DecryptByAsymKey(AsymKey_ID('AsymKey'), basic_pay))), 0)),0) as basicpay, "
                  + "isnull(sum(d.total_additions),0) as additions,isnull(sum(d.total_deductions),0) as deductions,isnull(sum(d.employerCPF),0) as employerCPF,isnull(sum(d.empCPF),0) as employeeCPF, "
                  + "isnull(max(isnull(Convert(numeric(10, 2), convert(varchar(10), DecryptByAsymKey(AsymKey_ID('AsymKey'), NetPay))), 0)), 0) as highpay , "
                  +"isnull(min(isnull(Convert(numeric(10, 2), convert(varchar(10), DecryptByAsymKey(AsymKey_ID('AsymKey'), NetPay))), 0)), 0) as lowpay "
                  + "from prepare_payroll_detail d inner join prepare_payroll_hdr h on d.trx_id = h.trx_id "
                  + "where h.start_period >= '" + _currentdateFrom.ToString("yyyy-MM-dd") + "' and h.end_period <= '" + _currentdate.ToString("yyyy-MM-dd") + "' AND d.[status]='G'"
                  + "UNION ALL "
                  + "SELECT  isnull(sum(d.total_gross),0) + isnull(sum(d.employerCPF),0) AS currentGross,isnull(sum(isnull(Convert(numeric(10, 2), convert(varchar(10), DecryptByAsymKey(AsymKey_ID('AsymKey'), basic_pay))), 0)),0) as basicpay, "
                  + "isnull(sum(d.total_additions),0) as additions,isnull(sum(d.total_deductions),0) as deductions,isnull(sum(d.employerCPF),0) as employerCPF,isnull(sum(d.empCPF),0) as employeeCPF, "
                  + "isnull(max(isnull(Convert(numeric(10, 2), convert(varchar(10), DecryptByAsymKey(AsymKey_ID('AsymKey'), NetPay))), 0)), 0) as highpay , "
                  + "isnull(min(isnull(Convert(numeric(10, 2), convert(varchar(10), DecryptByAsymKey(AsymKey_ID('AsymKey'), NetPay))), 0)), 0) as lowpay "
                  + "from prepare_payroll_detail d inner join prepare_payroll_hdr h on d.trx_id = h.trx_id "
                  + "where h.start_period >= '" + _prevdateFrom.ToString("yyyy-MM-dd") + "' and h.end_period <= '" + _currentdateFrom.ToString("yyyy-MM-dd") + "' AND d.[status]='G'";


            DataSet emloyeedata = new DataSet();
            emloyeedata = DataAccess.FetchRS(CommandType.Text, ssl);
            var _prevMonth = Convert.ToDateTime(_days + "-" + _month + "-" + _year).AddMonths(-1).ToString("MMMM") +" " + _year;
            var _currentMonth = Convert.ToDateTime(_days + "-" + _month + "-" + _year).ToString("MMMM") + " " + _year;
            System.Data.DataColumn newColumn = new System.Data.DataColumn("retMessage", typeof(System.String));
            newColumn.DefaultValue = _prevMonth + "|" + _currentMonth;
            emloyeedata.Tables[0].Columns.Add(newColumn);

            return JsonConvert.SerializeObject(new { emloyeesdata = emloyeedata.Tables[0] });                                                                      

        }

    [System.Web.Services.WebMethod]
        public static string GetPayrollDepartmentChart(string _filter)
        {
            var _year = _filter.Split('|')[0];
            var _days =Convert.ToUInt32(_filter.Split('|')[1].Split('-')[0]);
            var _month = _filter.Split('-')[1];
            var _currentdateFrom = Convert.ToDateTime(_days + "-" + _month + "-" + _year);
            //DateTime _currentdateFrom = _currentdate.AddDays(-_days);
            DateTime _prevdateFrom = _currentdateFrom.AddDays(-_days);

            var ssl = "Select d.DeptName,d.id,isnull(sum(pd.total_gross), 0) + isnull(sum(pd.employerCPF), 0) AS totalgross from employee e "
                      +"inner join department d on e.dept_id = d.id "
                      +"inner join prepare_payroll_detail pd on pd.emp_id = e.emp_code "
                      +"inner join prepare_payroll_hdr phd on phd.trx_id = pd.trx_id "
                      +"where phd.start_period >= '" + _prevdateFrom.ToString("yyyy-MM-dd") + "' and phd.end_period <= '" + _currentdateFrom.ToString("yyyy-MM-dd") + "' AND pd.[status]='G' "
                      +"group by d.id,d.DeptName;";



            DataSet emloyeedata = new DataSet();
            emloyeedata = DataAccess.FetchRS(CommandType.Text, ssl);
            return JsonConvert.SerializeObject(new { emloyeesdata = emloyeedata.Tables[0] });           
        }




        [System.Web.Services.WebMethod]
        public static string GetPayroll(object[] departmentschecked)
        {
            if (departmentschecked[0] == null)
            {
                departmentschecked[0] = "-1";
            }
            if (departmentschecked[1] == null)
            {
                departmentschecked[1] = "-1";
            }
            if (departmentschecked[2] == null)
            {
                departmentschecked[2] = "-1";
            }
            DataSet count = new DataSet();
            string BaiscPay = "THEN isnull(Convert(numeric(10, 2), convert(varchar(10), DecryptByAsymKey(AsymKey_ID('AsymKey'), basic_pay))),0) ELSE NULL END),0) ";
            string NetPay = "THEN isnull(Convert(numeric(10, 2), convert(varchar(10), DecryptByAsymKey(AsymKey_ID('AsymKey'), NetPay))),0) ELSE NULL END),0) ";
            string TotalAdditions = "THEN total_additions ELSE NULL END),0) ";
            string TotalDeductions = "THEN total_deductions ELSE NULL END),0) ";
            string currentmonthquery = "And STATUS in ('G') and  Convert(date, start_period) between DATEADD(month, DATEDIFF(month,0, getdate()), 0) And DATEADD(month, DATEDIFF(month,-1, getdate()), -1) and Convert(date, end_period) between DATEADD(month, DATEDIFF(month, 0, getdate()), 0) And DATEADD(month, DATEDIFF(month, -1, getdate()), -1)  ";
            string firstdepartment = "isnull(sum(CASE WHEN DeptName  in ('" + departmentschecked[0] + "') ";
            string seconddepartment = "isnull(sum(CASE WHEN DeptName  in ('" + departmentschecked[1] + "') ";
            string thirddepartment = "isnull(sum(CASE WHEN DeptName  in ('" + departmentschecked[2] + "') ";
            string ssql = "Select " + firstdepartment + currentmonthquery + BaiscPay + "as BasicPayFirst," + firstdepartment + currentmonthquery + NetPay + "as NetPayFirst," + firstdepartment + currentmonthquery + TotalAdditions + " as TotalAdditionsFirst," +
                          firstdepartment + currentmonthquery + TotalDeductions + "as TotalDeductionsFirst," + seconddepartment + currentmonthquery + BaiscPay + "as BasicPaySecond," + seconddepartment + currentmonthquery + NetPay + "as NetPaySecond," +
                          seconddepartment + currentmonthquery + TotalAdditions + "as TotalAdditionsSecond," + seconddepartment + currentmonthquery + TotalDeductions + "as TotalDeductionsSecond," + thirddepartment + currentmonthquery + BaiscPay + "as BasicPaythird," +
                          thirddepartment + currentmonthquery + NetPay + "as NetPaythird," + thirddepartment + currentmonthquery + TotalAdditions + "as TotalAdditionsthird," + thirddepartment + currentmonthquery + TotalDeductions + "as TotalDeductionsthird from PayRollView1 ;";
            count = DataAccess.FetchRS(CommandType.Text, ssql, null);
            return JsonConvert.SerializeObject(new { BasicPayFirst = count.Tables[0].Rows[0]["BasicPayFirst"], NetPayFirst = count.Tables[0].Rows[0]["NetPayFirst"], TotalAdditionsFirst = count.Tables[0].Rows[0]["TotalAdditionsFirst"], TotalDeductionsFirst = count.Tables[0].Rows[0]["TotalDeductionsFirst"], BasicPaySecond = count.Tables[0].Rows[0]["BasicPaySecond"], NetPaySecond = count.Tables[0].Rows[0]["NetPaySecond"], TotalAdditionsSecond = count.Tables[0].Rows[0]["TotalAdditionsSecond"], TotalDeductionsSecond = count.Tables[0].Rows[0]["TotalDeductionsSecond"], BasicPaythird = count.Tables[0].Rows[0]["BasicPaythird"], NetPaythird = count.Tables[0].Rows[0]["NetPaythird"], TotalAdditionsthird = count.Tables[0].Rows[0]["TotalAdditionsthird"], TotalDeductionsthird = count.Tables[0].Rows[0]["TotalDeductionsthird"] });
        }
        [System.Web.Services.WebMethod]
        public static string GetEmployeesCount(object[] departmentschecked)
        {
            if (departmentschecked[0] == null)
            {
                departmentschecked[0] = "-1";
            }
            if (departmentschecked[1] == null)
            {
                departmentschecked[1] = "-1";
            }
            if (departmentschecked[2] == null)
            {
                departmentschecked[2] = "-1";
            }
            DataSet count = new DataSet();
            string where = "And STATUS in ('G') and Convert(date, start_period) between DATEADD(month, DATEDIFF(month,0, getdate()), 0) And DATEADD(month, DATEDIFF(month,-1, getdate()), -1) and Convert(date, end_period) between DATEADD(month, DATEDIFF(month, 0, getdate()), 0) And DATEADD(month, DATEDIFF(month, -1, getdate()), -1) THEN isnull(emp_code,0) ELSE NULL END),0) ";
            string firstemployee = "isnull(count(CASE WHEN DeptName  in ('" + departmentschecked[0] + "') ";
            string secondemployee = "isnull(count(CASE WHEN DeptName  in ('" + departmentschecked[1] + "') ";
            string thirddemployee = "isnull(count(CASE WHEN DeptName  in ('" + departmentschecked[2] + "') ";
            string ssql = "Select " + firstemployee + where + "as EmployeesFirst," + secondemployee + where  + "as EmployeesSecond," + thirddemployee + where + " as EmployeesThird from PayRollView1 ;";
            count = DataAccess.FetchRS(CommandType.Text, ssql, null);
            return JsonConvert.SerializeObject(new { EmployeesFirst = count.Tables[0].Rows[0]["EmployeesFirst"], EmployeesSecond = count.Tables[0].Rows[0]["EmployeesSecond"], EmployeesThird = count.Tables[0].Rows[0]["EmployeesThird"] });
        }

        [System.Web.Services.WebMethod]
        public static string Getdepartmentsselected(object[] departmentschecked)
        {
            if (departmentschecked[0] == null)
            {
                departmentschecked[0] = "-1";
            }
            if (departmentschecked[1] == null)
            {
                departmentschecked[1] = "-1";
            }
            if (departmentschecked[2] == null)
            {
                departmentschecked[2] = "-1";
            }
            //DataSet count = new DataSet();
            //string ssql = "SELECT ( " +
            //              "select DeptName from department where id = " + departmentschecked[0] + ") AS Firstdepartment, (" +
            //              "select DeptName from department where id =" + departmentschecked[1] + ") AS Secpnddepartment, ( " +
            //              "select DeptName from department where id = " + departmentschecked[2] + ") AS Thirddepartment ;";
           // count = DataAccess.FetchRS(CommandType.Text, ssql, null);
            return JsonConvert.SerializeObject(new { Firstdepartment = departmentschecked[0], Secpnddepartment = departmentschecked[1], Thirddepartment = departmentschecked[2] });

        }
        [System.Web.Services.WebMethod]
        public static string GetDepartments()
        {
            string compid = HttpContext.Current.Session["Compid"].ToString();
            string ssl = "select * from department  where Company_id = "+ compid + "";
            DataSet department = new DataSet();
            department = DataAccess.FetchRS(CommandType.Text, ssl);
            return JsonConvert.SerializeObject(new { departments = department.Tables[0] });

        }
        [System.Web.Services.WebMethod]
        public static string GetPayrollelements()
        {
            string ssql = "select isnull(sum(isnull(Convert(numeric(10, 2), convert(varchar(10), DecryptByAsymKey(AsymKey_ID('AsymKey'), basic_pay))), 0)),0) " +
                          "as BasicPay,isnull(sum(Convert(numeric(10,2),cpfAmount)),0) as CPFAmount,isnull(sum(Convert(numeric(10,2),CPFGrossAmount)),0) as CPFGrossAmount,isnull(sum(Convert(numeric(10,2),empCPF)),0) as empCPF,isnull(sum(Convert(numeric(10,2),employerCPF)),0) as employerCPF, " +
                          "isnull(sum(Convert(numeric(10,2),DHRate)),0) as DHRate,isnull(sum(Convert(numeric(10,2),NHRate)),0) as NHRate,isnull(sum(isnull(Convert(numeric(10, 2), convert(varchar(10), DecryptByAsymKey(AsymKey_ID('AsymKey'), NetPay))), 0)),0) as NetPay, " +
                          "isnull(sum(convert(numeric(10, 2), total_additions)),0) as Totaladditions ,isnull(sum(convert(numeric(10, 2), total_deductions)),0) as Totaldeductions from PayRollView1 " +
                          "where Convert(date, start_period) between DATEADD(month, DATEDIFF(month,0, getdate()), 0) " +
                          "And DATEADD(month, DATEDIFF(month,-1, getdate()), -1) and Convert(date, end_period) between DATEADD(month, DATEDIFF(month, 0, getdate()), 0) " +
                          "And DATEADD(month, DATEDIFF(month, -1, getdate()), -1) " +
                          "And STATUS in ('G');";
            DataSet count = new DataSet();
            count = DataAccess.FetchRS(CommandType.Text, ssql, null);
            return JsonConvert.SerializeObject(new { BasicPay = count.Tables[0].Rows[0]["BasicPay"], CPFAmount = count.Tables[0].Rows[0]["CPFAmount"], CPFGrossAmount = count.Tables[0].Rows[0]["CPFGrossAmount"], empCPF = count.Tables[0].Rows[0]["empCPF"], employerCPF = count.Tables[0].Rows[0]["employerCPF"], DHRate = count.Tables[0].Rows[0]["DHRate"], NHRate = count.Tables[0].Rows[0]["NHRate"], NetPay = count.Tables[0].Rows[0]["NetPay"], Totaladditions = count.Tables[0].Rows[0]["Totaladditions"], Totaldeductions = count.Tables[0].Rows[0]["Totaldeductions"] });

        }


    }
}