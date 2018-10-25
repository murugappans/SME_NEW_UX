using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SMEPayroll.Roster
{
    public class NewSchedule
    {

        public int TrainSch { get; set; }
        public int CompanyID { get; set; }
        public int CerID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public int ScheduleId { get; set; }
        public int TrainderID { get; set; }
        public string EmailReminder { get; set; }
        public string SMSReminder { get; set; }
        public string TermsandConditions { get; set; }
        public string TrainingStatus { get; set; }
        public Nullable<System.DateTime> ModifiedTime { get; set; }
        public Nullable<System.DateTime> ScheduleTime { get; set; }
        public Nullable<System.DateTime> endtime { get; set; }
        public string PaymentType { get; set; }
        public Nullable<double> PaymentAmt { get; set; }
        public string chequFile { get; set; }
        [NotMapped]
        public string myState { get; set; }
        [NotMapped]
        public string StartTime { get; set; }
        [NotMapped]
        public string _EndTime { get; set; }
        [NotMapped]
        public string Traineremail { get; set; }
        [NotMapped]
        public string ClientEmail { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
        public String EmailID { get; set; }
        public String MobileNo { get; set; }
        public Nullable<int> ReminderDays { get; set; }
        [NotMapped]
        public string callFrom { get; set; }
        public String Remarks { get; set; }
        public Nullable<int> TrainingsCompleted { get; set; }
        public string seriesID { get; set; }

    }

    public class NewRoster
    {
        public NewRoster(){
            id = "1";
            resourceId = "1";
            start = "2017-11-08T02:00:00";
            end = "2017-11-08T07:00:00";
            title = "New Schedule";
        }
        public string id { get; set; }
        public string resourceId { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string title { get; set; }
    }

    public class employeeroster: CommonRosterSettings
    {
        public int id { get; set; }
        public int rosterID { get; set; }
        public int? projectID { get; set; }
        public int? teamID { get; set; }
        public int? employeeID { get; set; }
        public string rosterType { get; set; }

        public string title { get; set; }
        public string description { get; set; }
        public DateTime? rosterDate { get; set; }

        public DateTime? startTime { get; set; }

        public DateTime? endTime { get; set; }

        public DateTime? createdOn { get; set; }

        public int? createdBy { get; set; }

        public DateTime? modifiedOn { get; set; }

        public DateTime? modifiedBy { get; set; }
        public string color { get; set; }
        public string filterType { get; set; }
        public string displayFilterType { get; set; }
        public List<EmployeeView> employeeIds { get; set; }
        public string seriesID { get; set; }
        public bool allday { get; set; }
        //New addeded properies Starts

        public string FIFO { get; set; }
        public string Rounding { get; set; }
        public int? BreakTimeAfter { get; set; }
        public int? BreakTimeAftOtFlx { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string EarlyInBy { get; set; }
        public string EarlyOutBy { get; set; }
        public string LateInBy { get; set; }
        public string LateOutBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string Pattern { get; set; }
        //public DateTime shiftEnd { get; set; }
        public int? ClockInBefore { get; set; }
        public int? ClockInAfter { get; set; }
        public int? ClockOutBefore { get; set; }
        public int? ClockOutAfter { get; set; }
        public string BreakTimeNHhr { get; set; }
        public string BreakTimeOThr { get; set; }
        public int? BreakTimeNH { get; set; }
        public int? BreakTimeOT { get; set; }
        public string PullWorkTimein { get; set; }
        public string FlexibleWorkinghr { get; set; }
        public bool NightShift { get; set;}
        public bool BRKNEXTDAY { get; set; }
        public string Remark { get; set; }
        //New addeded properies Ends


        public string returnMessage { get; set; }
        public string successMessage { get; set; }
        public string errorMessage { get; set; }
        public string warningMessage { get; set; }

    }


    public class EmployeeView
    {
        public int? emp_code { get; set; }
        public int? teamID { get; set; }
        public string empName { get; set; }
        public string filtertype { get; set; }
    }

    public class Toastr
    {
        public string returnMessage { get; set; }
        public string successMessage { get; set; }
        public string errorMessage { get; set; }
        public string warningMessage { get; set; }
    }

}