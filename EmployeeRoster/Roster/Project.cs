using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SMEPayroll.Roster
{
     public class Project
    {

        public int ID { get; set; }
        public int Company_ID { get; set; }
        public int Location_ID { get; set; }
        public string Project_ID { get; set; }
        public string Project_Name { get; set; }
        public string isShared { get; set; }
    }



    public class DrowpdownList
    {

        public int mainID { get; set; }
        public string Description { get; set; }
    }


    public class ResourseList:Toastr
    {
        //public ResourseList()
        //{
        //    id = "";
        //    title = "";
        //    start = null;
        //    end = null;
        //    resourceId = "";
        //    projectID = "";
        //    teamID = "";
        //    eventTitle = "";
        //    description = "";
        //    textColor = "";
        //    eventColor = "#378006!important";
        //    start_end = "";
        //    employeeName = "";
        //    projectName = "";
        //    teamName = "";
        //    seriesID = "";
        //}
        public string id { get; set; }
        public string rosterID { get; set; }
        public string title { get; set; }
        public string rosterDate { get; set; }
        public string rosterDateTo { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string eventTitle { get; set; }
        public string resourceId { get; set; }
        public string projectID { get; set; }
        public string teamID { get; set; }
        public string color { get; set; }
        public string textColor { get; set; }
        public string eventColor { get; set; }
        public string description { get; set; }
        public string allday { get; set; }
        public string start_end { get; set; }
        public string timestart { get; set; }
        public string timeend { get; set; }
        public string employeeID { get; set; }
        public string employeeName { get; set; }
        public string projectName { get; set; }
        public string teamName { get; set; }
        public string seriesID { get; set; }
        public List<employeesinseries> employeeList { get; set; }
  
        //Common settings fields Starts
        public string early_in { get; set; }
        public string late_in { get; set; }
        public string early_out { get; set; }          
        public string breakTime_start { get; set; }               
        public string breakTime_hrs { get; set; }             
        public string breakTime_OT_start { get; set; }            
        public string breakTime_OT_hrs { get; set; }          
        public string total_flexi_hrs { get; set; }            
        public string flexi_breakTime_start { get; set; }            
        public string flexi_breakTime_hrs { get; set; }             
        public string flexi_breakTime_OT_start { get; set; }             
        public string flexi_breakTime_OT_hrs { get; set; }            
        public string company_id { get; set; }
        public string shiftEnd { get; set; }
        public string shiftEndTime { get; set; }
        public string BRKNEXTDAY { get; set; }
        //Common settings fields ends

    }


    public class employeerosterView
    {
        public string id { get; set; }
        public string rosterID { get; set; }
        public string teamID { get; set; }
        public string projectID { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string description { get; set; }
        public string rosterDate { get; set; }
        public string rosterType { get; set; }
        public bool allday { get; set; }
        public string seriesID { get; set; }
    }


    public class OffDayView
    {
        public string id { get; set; }
        public string employeeID { get; set; }
        public string employeeName { get; set; }
        public string mobile { get; set; }
        public string title { get; set; }
        public string offFrom { get; set; }
        public string offto { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string eventTitle { get; set; }
        public string color { get; set; }
        public string textColor { get; set; }
        public string eventColor { get; set; }
        public string description { get; set; }
        public bool allday { get; set; }
        public string returnMessage { get; set; }

    }

    public class employeesinseries
    {
        public string seriesID { get; set; }
        public string employeeID { get; set; }
        public string employeeName { get; set; }

    }


    public class CommonRosterSettings
    {
        public int settingid { get; set; }

        public int? early_in { get; set; }

        public int? late_in { get; set; }

        public int? early_out { get; set; }

        public int? breakTime_start { get; set; }

        public int? breakTime_hrs { get; set; }

        public int? breakTime_OT_start { get; set; }

        public int? breakTime_OT_hrs { get; set; }

        public int? total_flexi_hrs { get; set; }

        public int? flexi_breakTime_start { get; set; }

        public int? flexi_breakTime_hrs { get; set; }

        public int? flexi_breakTime_OT_start { get; set; }

        public int? flexi_breakTime_OT_hrs { get; set; }

        public int? company_id { get; set; }

        public DateTime? shiftEnd { get; set; }

    }
}