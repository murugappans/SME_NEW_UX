using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SMEPayroll.Reports
{
    public class ProjectMontlyReport
    {
        private int emp_Code;

        public int EmpCode
        {
            get { return emp_Code; }
            set { emp_Code = value; }
        }

        private string empName;

        public string EmpName
        {
            get { return empName; }
            set { empName = value; }
        }


        private decimal deductions;

        public decimal Deductions
        {
            get { return deductions; }
            set { deductions = value; }
        }

        private decimal netpay;

        public decimal Netpay
        {
            get { return 1200.00m; }
            set { netpay = value; }
        }
	



        private decimal sallary;

        public decimal Sallary
        {
            get { return sallary; }
            set { sallary = value; }
        }


        private decimal allowance;

        public decimal Allowance
        {
            get { return allowance; }
            set { allowance = value; }
        }


        private decimal total;

        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }


        private string month;
        public string Month
        {
            get { return month; }
            set { month = value; }
        }


        private string year;

        public string Year
        {
            get { return year; }
            set { year = value; }
        }

        private string projectName;

        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }


        private string company;

        public string Company
        {
            get { return company; }
            set { company = value; }
        }

        private string salaryMonth;

        public string SallaryMonth
        {
            get
            {
                return
              "Salary Of Month of " + this.Month.ToString() + " " + this.Year.ToString();
                
                ; }
          
        }
	




    }
}
