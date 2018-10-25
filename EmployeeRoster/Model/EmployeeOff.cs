using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMEPayroll.EmployeeRoster.Model
{
    public class EmployeeOff
    {
        public int offDayID { get; set; }

        public int? employeeID { get; set; }
        public string employeeName { get; set; }
        public string phone { get; set; }

        public DateTime? offDateFrom { get; set; }

        public DateTime? offDateTo { get; set; }

        public DateTime? startTime { get; set; }

        public DateTime? endTime { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public DateTime? createdOn { get; set; }

        public int? createdBy { get; set; }

        public DateTime? modifiedOn { get; set; }

        public DateTime? modifiedBy { get; set; }

        public string returnMessage { get; set; }
        public string successMessage { get; set; }
        public string filterType { get; set; }

    }

    public class EmployeeOffDayListView
    {
        public int offDayID { get; set; }

        public int? employeeID { get; set; }
        public string employeeName { get; set; }
        public string phone { get; set; }

        public DateTime offDateFrom { get; set; }

        public DateTime offDateTo { get; set; }

        public string startTime { get; set; }

        public string endTime { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public string createdOn { get; set; }

        public int? createdBy { get; set; }

        public DateTime? modifiedOn { get; set; }

        public DateTime? modifiedBy { get; set; }

        public string returnMessage { get; set; }
        public string successMessage { get; set; }
        public string filterType { get; set; }

    }
}