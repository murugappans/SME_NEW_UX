using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMEPayroll.Main.Model
{
    public class EmployeeData
    {
        public string id { get; set; }
        public string emp_name { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string eventType { get; set; }
        public string phone { get; set; }
        public string date_of_birth { get; set; }
        public string passport_expiry { get; set; }
        public string joining_date { get; set; }
        public string probation_period { get; set; }
        public string description { get; set; }
        public string createdOn { get; set; }
        public string createdBy { get; set; }
        public string modifiedOn { get; set; }
        public string modifiedBy { get; set; }
        public string returnMessage { get; set; }
        public string successMessage { get; set; }
        public string filterType { get; set; }
        public string className { get; set; }

    }


}