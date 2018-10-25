using Newtonsoft.Json;
using SMEPayroll.EmployeeRoster.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEPayroll.Roster
{
    public partial class RosterBox : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            if (!Page.IsPostBack)
            {
                var objSettings= EmployeeRosterService.GetRosterCommonSettings(Convert.ToInt32(Session["Compid"]));
                Session["RosterCommonSettings"] = objSettings; //JsonConvert.SerializeObject(objSettings);
                string json = JsonConvert.SerializeObject(objSettings);
                //ClientScript.RegisterClientScriptBlock(GetType(), "sas", "<script>var _josn=new Object(); _json='" + json +"'';</script>", true);
                 // ScriptManager.RegisterStartupScript(this,GetType(), "sas", "<script>var json = '"+objSettings+"'';</script>", true);
                ViewState["viewstateRosterCommonSettings"] = objSettings;
            }
        }

        [System.Web.Services.WebMethod]
        public static string GetCommonSettings()
        {
            var objSettings = EmployeeRosterService.GetRosterCommonSettings(Convert.ToInt32(HttpContext.Current.Session["Compid"]));
          
          return JsonConvert.SerializeObject(objSettings);
       
        }


        [System.Web.Services.WebMethod]
        public static string NewSchedule(string dateString)
        {
            return "Hello " + dateString + Environment.NewLine + "The Current Time is: "
                + DateTime.Now.ToString();
        }



        [System.Web.Services.WebMethod]
        public static string NewEmployeeRoster(NewRoster roster)
        {
            return JsonConvert.SerializeObject(roster);
        }

        [System.Web.Services.WebMethod]
        public static string SaveRoseter(NewRoster roster)
        {
            //var _roster = new NewRoster()
            //{
            //    id = "2",
            //    resourceId = "2",
            //    start = "2017-11-15T02:00:00",
            //    end = "2017-11-15T07:00:00",
            //    title = "second Schedule"
            //};

            var _roster = new NewRoster()
            {
                id = "2",
                resourceId = "2",
                start = "2017-11-15T02:00:00",
                end = "2017-11-15T07:00:00",
                title = "second Schedule"
            };

            return JsonConvert.SerializeObject(_roster);
        }


        [System.Web.Services.WebMethod]
        public static string GetCurrentTime(string name)
        {
            return "Hello " + name + Environment.NewLine + "The Current Time is: "
                + DateTime.Now.ToString();
        }


        //////////////////
        [System.Web.Services.WebMethod]
        public static string GetProjects(string _viewBy)
        {
            int compID = Utility.ToInteger(HttpContext.Current.Session["Compid"]);
            string sSQL="";
            if (_viewBy == "Project")
            {
                //DataSet dsProject = new DataSet();
                //sSQL = "Select ID,Project_ID,Project_Name From Project";
                //dsProject = DataAccess.FetchRS(CommandType.Text, sSQL, null);

                //var empList = new List<DrowpdownList>();
                //empList = dsProject.Tables[0].AsEnumerable().Select(dataRow => new DrowpdownList
                //{
                //    mainID = dataRow.Field<int>("ID"),
                //    Description = dataRow.Field<string>("Project_Name")
                //}).ToList();
                var empList  = EmployeeRosterService.getProjectList(compID);
                return JsonConvert.SerializeObject(empList);
            }

            else if (_viewBy == "Team")
            {
                DataSet dsTeam = new DataSet();
                //sSQL = "Select * FROM UserGroups";
                sSQL = "Select * FROM TeamScheduler WHERE Company_Id="+ Convert.ToInt32(HttpContext.Current.Session["Compid"]) + " ORDER BY Team_Name ASC";
                dsTeam = DataAccess.FetchRS(CommandType.Text, sSQL, null);

                var empList = new List<DrowpdownList>();
                empList = dsTeam.Tables[0].AsEnumerable().Select(dataRow => new DrowpdownList
                {
                    mainID = dataRow.Field<int>("ID"),
                    Description = dataRow.Field<string>("Team_Name")
                }).ToList();

                return JsonConvert.SerializeObject(empList);
            }

            else if (_viewBy == "Employee")
            {
                DataSet dsTeam = new DataSet();
                sSQL = "Select emp_code,emp_name FROM Employee";
                dsTeam = DataAccess.FetchRS(CommandType.Text, sSQL, null);

                var empList = new List<DrowpdownList>();
                empList = dsTeam.Tables[0].AsEnumerable().Select(dataRow => new DrowpdownList
                {
                    mainID = dataRow.Field<int>("emp_code"),
                    Description = dataRow.Field<string>("emp_name")
                }).ToList();

                return JsonConvert.SerializeObject(empList);
            }

            return _viewBy;
        }

        [System.Web.Services.WebMethod]
        public static string SaveNewRoseter(employeeroster roster)
        {

            bool _isSessionTimeout = CheckSessionTimeout();
            if (_isSessionTimeout)
                return JsonConvert.SerializeObject("SessionTimeout");

            // roster.company_id = Session["Compid"]);

            TimeSpan _timeSpanStart = TimeSpan.Parse(roster.startTime.Value.ToString().Split(' ')[1], CultureInfo.InvariantCulture);
            TimeSpan _timeSpanEnd = TimeSpan.Parse(roster.endTime.Value.ToString().Split(' ')[1], CultureInfo.InvariantCulture);

            roster.startTime = roster.rosterDate + _timeSpanStart;
            //roster.endTime = roster.rosterDate + _timeSpanEnd;

            DateTime _dateto = Convert.ToDateTime(roster.endTime.ToString().Split(' ')[0]);

            roster.endTime = _dateto + _timeSpanEnd;

            ///////////////////C A L C U L A T E C O M M O N S E T T I N G S///////////////////
            roster = CalculateCommonSettings(roster);
            // roster.LateOutBy = _earlyOutBy.ToString();
            ///////////////////C A L C U L A T E C O M M O N S E T T I N G S///////////////////
            var empList = new employeeroster();
            empList = EmployeeRosterService.SaveEmployeeRoster(roster);
            var resourceList = new List<ResourseList>();
            resourceList = EmployeeRosterService.GetEmployeeRosterList(roster);

            resourceList[0].returnMessage = roster.returnMessage;
            //resourceList[0].successMessage = roster.successMessage;
            return JsonConvert.SerializeObject(resourceList);
        }


        private static employeeroster CalculateCommonSettings(employeeroster roster)
        {

            ///////////////////C A L C U L A T E C O M M O N S E T T I N G S///////////////////
            TimeSpan _timeSpanStart = TimeSpan.Parse(roster.startTime.Value.ToString().Split(' ')[1], CultureInfo.InvariantCulture);
            TimeSpan _timeSpanEnd = TimeSpan.Parse(roster.endTime.Value.ToString().Split(' ')[1], CultureInfo.InvariantCulture);
            TimeSpan _inTime = _timeSpanStart;
            TimeSpan _outTime = _timeSpanEnd;
            TimeSpan _earlyInBy = _inTime.Subtract(TimeSpan.FromMinutes(roster.early_in.Value));
            TimeSpan _lateInBy = _inTime.Add(TimeSpan.FromMinutes(roster.late_in.Value));
            //_lateInBy = TimeSpan.Parse(_lateInBy.TotalHours + ":"+ _lateInBy.Minutes + ":" + _lateInBy.TotalHours);
            TimeSpan _earlyOutBy = _outTime.Subtract(TimeSpan.FromMinutes(roster.early_out.Value));
            TimeSpan _breakTimeNHhr = _inTime.Add(TimeSpan.FromMinutes(roster.breakTime_start.Value));
            //TimeSpan _breakTimeOThr = _outTime.Add(TimeSpan.FromMinutes(roster.BreakTimeOT.Value)); To do

            // TimeSpan _lateOutBy = _outTime.Add(TimeSpan.FromMinutes(roster.LateOutBy.Value));
            int _breakTimeNH = roster.breakTime_hrs.Value;
            int _breakTimeOT = roster.BreakTimeOT != null ? roster.BreakTimeOT.Value : 0;

            int _breakTimeAfter = roster.breakTime_start != null ? roster.breakTime_start.Value : 0;

            roster.InTime = _inTime.ToString();
            roster.OutTime = _outTime.ToString();
            if (_inTime.ToString() == "00:00:00")
            {
                TimeSpan _tempspan = TimeSpan.Parse("23:59:59");
                roster.EarlyInBy = _tempspan.Subtract(TimeSpan.FromMinutes(roster.early_in.Value)).ToString();
            }
            else
            {
                roster.EarlyInBy = _earlyInBy.ToString();
            }
            roster.LateInBy = _lateInBy.ToString();
            roster.EarlyOutBy = _earlyOutBy.ToString();
            roster.BreakTimeNH = _breakTimeNH;
            roster.BreakTimeOT = _breakTimeOT;
            roster.BreakTimeNHhr = _breakTimeNHhr.ToString();
            roster.BreakTimeAfter = _breakTimeAfter;
            //roster.BreakTimeOThr = _breakTimeOThr.ToString(); Todo
            //roster.shiftEnd=
            // roster.LateOutBy = _earlyOutBy.ToString();
            ///////////////////C A L C U L A T E C O M M O N S E T T I N G S///////////////////

            return roster;
        }

        [System.Web.Services.WebMethod]
        public static string UpdateEmployeeRoster(employeeroster roster)
        {
            bool _isSessionTimeout = CheckSessionTimeout();
            if (_isSessionTimeout)
                return JsonConvert.SerializeObject("SessionTimeout");
            roster = CalculateCommonSettings(roster);
            var empList = new employeeroster();
            empList = EmployeeRosterService.UpdateEmployeeRoster(roster);
            var resourceList = new List<ResourseList>();
            resourceList = EmployeeRosterService.GetEmployeeRosterList(roster);
            //resourceList[0].successMessage = roster.successMessage;
            resourceList[0].returnMessage = roster.returnMessage;
            return JsonConvert.SerializeObject(resourceList);
        }


        [System.Web.Services.WebMethod]
        public static string DragRoster(employeeroster roster)
        {
            bool _isSessionTimeout = CheckSessionTimeout();
            if (_isSessionTimeout)
                return JsonConvert.SerializeObject("SessionTimeout");

            var _emproster = new employeeroster();
            _emproster = EmployeeRosterService.DragRoster(roster);
            var resourceList = new List<ResourseList>();
            resourceList = EmployeeRosterService.GetEmployeeRosterList(roster);
            resourceList[0].returnMessage = _emproster.returnMessage;
            return JsonConvert.SerializeObject(resourceList);
        }


        [System.Web.Services.WebMethod]
        public static string ResizeSchedule(employeeroster roster)
        {
            bool _isSessionTimeout = CheckSessionTimeout();
            if (_isSessionTimeout)
                return JsonConvert.SerializeObject("SessionTimeout");
            var _emproster = new employeeroster();
            _emproster = EmployeeRosterService.ResizeSchedule(roster);
            var resourceList = new List<ResourseList>();
            resourceList = EmployeeRosterService.GetEmployeeRosterList(roster);
            resourceList[0].returnMessage = roster.returnMessage;
            return JsonConvert.SerializeObject(resourceList);
        }

        [System.Web.Services.WebMethod]
        public static string GetEmployeeRosterList(employeeroster roster)
        {
            bool _isSessionTimeout = CheckSessionTimeout();
            if (_isSessionTimeout)
                return JsonConvert.SerializeObject("SessionTimeout");
            var resourceList = new List<ResourseList>();
            resourceList = EmployeeRosterService.GetEmployeeRosterList(roster);
            return JsonConvert.SerializeObject(resourceList);

        }

        [System.Web.Services.WebMethod]
        public static string GetEventByID(int _id)
        {
            bool _isSessionTimeout = CheckSessionTimeout();
            if (_isSessionTimeout)
                return JsonConvert.SerializeObject("SessionTimeout");
            var _employeeroster = EmployeeRosterService.GetEventByEventID(_id);
            return JsonConvert.SerializeObject(_employeeroster);
        }

        [System.Web.Services.WebMethod]
        public static string GetRoseterList(employeeroster roster)
        {

            var resourceList = new List<ResourseList>();
            resourceList = EmployeeRosterService.GetEmployeeRosterListByMoreFilters(roster);

            if (resourceList.Count == 0)
                resourceList = new List<ResourseList>();
            else
                resourceList[0].successMessage = roster.successMessage;

            return JsonConvert.SerializeObject(resourceList);
        }


        [System.Web.Services.WebMethod]
        public static string DeleteEvent(employeeroster _roster)
        {
            bool _isSessionTimeout = CheckSessionTimeout();
            if (_isSessionTimeout)
                return JsonConvert.SerializeObject("SessionTimeout");

            var obj = EmployeeRosterService.DeleteEvent(_roster);
            var resourceList = new List<ResourseList>();
            resourceList = EmployeeRosterService.GetEmployeeRosterList(_roster);
            return JsonConvert.SerializeObject(resourceList);
        }

        private static bool CheckSessionTimeout()
        {

            if (Utility.ToString(HttpContext.Current.Session["Username"]) == "")
                return true;
            return false;
        }

        [System.Web.Services.WebMethod]
        public static string KeepSessionalive()
        {
            HttpContext.Current.Session.Timeout = 60;
            return JsonConvert.SerializeObject("hello");
        }


    }
}