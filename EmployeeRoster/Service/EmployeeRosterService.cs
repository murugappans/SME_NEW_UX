using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMEPayroll.EmployeeRoster.Model;
using SMEPayroll.EmployeeRoster.Model.DataSQL;
using System.Data.SqlClient;
using SMEPayroll.Roster;
using System.Data;
using System.Globalization;

namespace SMEPayroll.EmployeeRoster.Service
{
    public class EmployeeRosterService
    {

        public static employeeroster SaveEmployeeRoster(employeeroster _employeeroster)
        {

            var _employees = new List<EmployeeView>();
            for (int i = 0; i < _employeeroster.employeeIds.Count; i++)
            {
                if (_employeeroster.employeeIds[i].filtertype == "Employee")
                {
                    var _objEmp = new EmployeeView();
                    _objEmp.teamID = DataAccess.ExecuteScalarQuery(EmployeeRosterSQL.GetTeamID(_employeeroster.employeeIds[i].emp_code.Value));
                    _objEmp.emp_code = _employeeroster.employeeIds[i].emp_code;
                    _employees.Add(_objEmp);
                }
                else
                    _employees.AddRange(CheckEmployeeListByGroup(_employeeroster.employeeIds[i].emp_code.Value));
                
            }


            var _seriesID = Guid.NewGuid().ToString();
           // var empList = new List<EmployeeView>();
   
            for (int i = 0; i < _employees.Count; i++)
            {
                _employeeroster.employeeID = _employees[i].emp_code;
                //if (_employeeroster.filterType == "Employee")
                    _employeeroster.teamID = _employees[i].teamID;
                int RosterID = 0;
                int _rostercount = DataAccess.ExecuteScalarQuery(EmployeeRosterSQL.CheckAlreadyAssignedInRosterTable(_employeeroster));
                if (_rostercount <= 0)
                {
                    int dsRoster = DataAccess.ExecuteNonQuery(EmployeeRosterSQL.SaveEmployeeRoster(_employeeroster), null);
                    
                }

               // RosterID = DataAccess.ExecuteNonQuery(EmployeeRosterSQL.CheckAlreadyAssignedInRosterTable(_employeeroster), null);
                RosterID = DataAccess.ExecuteScalarQuery(EmployeeRosterSQL.CheckAlreadyAssignedInRosterTable(_employeeroster));

                if (RosterID > 0)
                {
                    _employeeroster.rosterID = RosterID;
                    int _count = DataAccess.ExecuteScalarQuery(EmployeeRosterSQL.CheckAlreadyAssignedBySamedateInRosterDetail(_employeeroster));
                    if (_count <= 0)
                    {
                        _employeeroster.seriesID = _seriesID;
                       

                        int dsRosterDetail = DataAccess.ExecuteNonQuery(EmployeeRosterSQL.SaveEmployeeRosterDetail(_employeeroster), null);
                        _employeeroster.returnMessage = "Success|Roster created successfully.";
                        //_count = DataAccess.ExecuteScalarQuery(EmployeeRosterSQL.CheckAlreadyAssigned(_employeeroster));
                        //_employeeroster.rosterID = _count;
                    }
                    else
                    {
                        _employeeroster.returnMessage = "Warning|Roster is already scheduled in this timeframe.";
                    }
                }
            }





            //var _seriesID = Guid.NewGuid().ToString();
            //var empList = new List<EmployeeView>();
            //if (_employeeroster.filterType == "Employee")
            //    empList = _employeeroster.employeeIds;
            //else
            //    empList= CheckEmployeeListByGroup(_employeeroster.teamID.Value);

            //for (int i = 0; i < empList.Count; i++)
            //{
            //    _employeeroster.employeeID = empList[i].emp_code;
            //    if (_employeeroster.filterType == "Employee")
            //        _employeeroster.teamID = DataAccess.ExecuteScalarQuery(EmployeeRosterSQL.GetTeamID(_employeeroster.employeeID.Value));
            //    int _count = DataAccess.ExecuteScalarQuery(EmployeeRosterSQL.CheckAlreadyAssignedBySamedate(_employeeroster));
            //    if (_count <= 0)
            //    {

            //        _employeeroster.seriesID =_seriesID;
            //        int dsRoster = DataAccess.ExecuteNonQuery(EmployeeRosterSQL.SaveEmployeeRoster(_employeeroster), null);
            //        _count = DataAccess.ExecuteScalarQuery(EmployeeRosterSQL.CheckAlreadyAssigned(_employeeroster));
            //        _employeeroster.rosterID = _count;
            //    }
            //    else
            //    {
            //        _employeeroster.successMessage = "Roster is already scheduled in this timeframe.";
            //    }
            //}
            if (_employees.Count<1)
            {
                _employeeroster.returnMessage = "Warning|No employee is assigned to this team";
            }
            return _employeeroster;
        }

        public static employeeroster UpdateEmployeeRoster(employeeroster _employeeroster)
        {
            if (_employeeroster.rosterID > 0)
            {
                int _count = DataAccess.ExecuteScalarQuery(EmployeeRosterSQL.CheckAlreadyAssignedBySamedateInRosterDetail(_employeeroster));
                if (_count <= 0)
                {
                    int dsRoster = DataAccess.ExecuteNonQuery(EmployeeRosterSQL.UpdateEmployeeRoster(_employeeroster), null);
                    _employeeroster.returnMessage = "Success|Updated successfully.";
                }
                else
                  _employeeroster.returnMessage = "Warning|Roster is already scheduled in this timeframe.";
                
            }
        
            return _employeeroster;
        }


        public static employeeroster DragRoster(employeeroster _employeeroster)
        {
            if (_employeeroster.rosterID > 0)
            {
                int _count = DataAccess.ExecuteScalarQuery(EmployeeRosterSQL.CheckAlreadyAssignedBySamedateInRosterDetail(_employeeroster));

                if (_count <= 0)
                {
                    int dsRoster = DataAccess.ExecuteNonQuery(EmployeeRosterSQL.DragRoster(_employeeroster), null);
                    _employeeroster.returnMessage = "Success|Roster updated successfully.";
                }
                else
                {
                    _employeeroster.returnMessage = "Warning|Employee is already assigned on this day.";
                }

            }
        
            return _employeeroster;
        }
        public static employeeroster ResizeSchedule(employeeroster _employeeroster)
        {
         
            if (_employeeroster.rosterID > 0)
            {
                int _count = DataAccess.ExecuteScalarQuery(EmployeeRosterSQL.CheckAlreadyAssignedBySamedateInRosterDetail(_employeeroster));
                if (_count <= 0)
                {
                    int dsRoster = DataAccess.ExecuteNonQuery(EmployeeRosterSQL.ResizeSchedule(_employeeroster), null);
                    _employeeroster.returnMessage = "Success|Roster updated successfully.";
                }
               else
                {
                    _employeeroster.returnMessage = "Warning|Employee is already assigned on this day.";
                }
            }
        
            return _employeeroster;
        }



        public static List<ResourseList> GetEmployeeRosterList(employeeroster _employeeroster)
        {

            DataSet dsProject = new DataSet();
            dsProject = DataAccess.FetchRS(CommandType.Text, EmployeeRosterSQL.GetEmployeeRosterListByFilter(_employeeroster), null);
            var empList = new List<ResourseList>();
            empList = dsProject.Tables[0].AsEnumerable().Select(dataRow => new ResourseList
            {
             
                id = dataRow.Field<int>("ID").ToString(),
                rosterID = dataRow.Field<int>("Roster_ID").ToString(),
                title = dataRow.Field<string>("emp_name"),
                start = dataRow.Field<DateTime>("startTime").ToString("yyyy-MM-dd HH:mm:ss"),
                end = dataRow.Field<DateTime>("endTime").ToString("yyyy-MM-dd HH:mm:ss"),
                rosterDateTo = dataRow.Field<DateTime>("endTime").ToString("yyyy-MM-dd HH:mm:ss"),
                resourceId = dataRow.Field<int>("ID").ToString(),
                projectID = dataRow.Field<int>("projectID").ToString(),
                teamID = dataRow.Field<int>("teamID").ToString(),
                eventTitle = dataRow.Field<string>("title"),
                description = dataRow.Field<string>("Remark"),
                textColor = "yellow",
                eventColor = "#378006!important",
                timestart = dataRow.Field<DateTime>("startTime").ToString("yyyy-MM-dd HH:mm:ss"),
                timeend = dataRow.Field<DateTime>("endTime").ToString("yyyy-MM-dd HH:mm:ss"),
                //start_end = dataRow.Field<DateTime>("endTime").Subtract(dataRow.Field<DateTime>("startTime")).Hours.ToString(),
                start_end = getdifference(Convert.ToDateTime(dataRow.Field<DateTime>("startTime").ToString("yyyy-MM-dd HH:mm:ss")), Convert.ToDateTime(dataRow.Field<DateTime>("endTime").ToString("yyyy-MM-dd HH:mm:ss"))).ToString(),
                employeeName = dataRow.Field<string>("emp_name"),
                employeeID = dataRow.Field<int>("EmpID").ToString(),
                seriesID = dataRow.Field<Guid>("seriesID").ToString(),
                teamName = dataRow.Field<string>("GroupName").ToString(),
                projectName = dataRow.Field<string>("Sub_Project_Name").ToString(),
                employeeList = new List<employeesinseries>(),
                allday = dataRow.Field<bool>("allday").ToString()
            }).ToList();

            //TimeSpan diff = DateTime.Parse(empList[0].end) - DateTime.Parse(empList[0].start);
            //double hours = diff.TotalHours;

            // DateTime dtFrom = DateTime.Parse(empList[0].end);
            //DateTime dtTo = DateTime.Parse(empList[0].start);

            // int timeDiff = dtFrom.Subtract(dtTo).Hours;
            if(empList.Count()>0)
            empList[0].employeeList = GetEmployeeList(empList[0].seriesID.ToString());

            return empList;
        }

        private static string getdifference(DateTime _start, DateTime _end)
        {

            TimeSpan _timeSpanStart = TimeSpan.Parse(_start.ToString().Split(' ')[1], CultureInfo.InvariantCulture);
            TimeSpan _timeSpanEnd = TimeSpan.Parse(_end.ToString().Split(' ')[1], CultureInfo.InvariantCulture);

            var days = (_end - _start).Days + 1;




            var _totalhrs = _timeSpanEnd.Subtract(_timeSpanStart);

            var _hours = _end.Subtract(_start).Hours;
            TimeSpan diff = _end - _start;

            double hours = diff.TotalHours;

            //double _span = _totalhrs.TotalMinutes*days;

            TimeSpan _exactSpan = TimeSpan.FromMinutes((_totalhrs.TotalMinutes * days));
            var _totalhours = _exactSpan.TotalHours.ToString().Split('.')[0];
            var _totalminutes = _exactSpan.Minutes;
            var _exacttime = _totalhours.ToString() + ":" + _totalminutes.ToString();

            //return hours;
            var _thours = diff.Hours.ToString();
            var _tmins = diff.Minutes.ToString();
            var _ttime = _thours;
            if (_tmins != "0")
                _ttime = _thours + "." + _tmins;
            return _exacttime; //(double.Parse(_ttime)*days).ToString();//Todo to be fixed on saturday
        }

        public static List<employeesinseries> GetEmployeeList(string _seriesid)
        {
            var _emplist = new List<employeesinseries>();
 
            DataSet dsemployee = new DataSet();
            dsemployee = DataAccess.FetchRS(CommandType.Text, EmployeeRosterSQL.GetEventEmployeesBySeriesID(_seriesid), null);

            _emplist = dsemployee.Tables[0].AsEnumerable().Select(dataRow => new employeesinseries
            {
                seriesID = dataRow.Field<Guid>("seriesID").ToString(),
                employeeID = dataRow.Field<int>("emp_code").ToString(),
                employeeName = dataRow.Field<string>("emp_name")

            }).ToList();

            return _emplist;
        }
        public static employeerosterView GetEventByEventID1(int _id)
        {
            var _employeeroster = new employeerosterView();
            SqlDataReader dr = DataAccess.GetDataReader(EmployeeRosterSQL.GetEventByEventID(_id));

            while (dr.Read())
            {
                _employeeroster = new employeerosterView()
                {
                    id = dr["ID"].ToString(),
                    rosterID = dr["Roster_ID"].ToString(),
                    teamID = dr["teamID"].ToString(),
                    projectID = dr["projectID"].ToString(),
                    rosterDate = Convert.ToDateTime(dr["Roster_Date"]).ToString("yyyy-MM-dd"),
                    title = dr["title"].ToString(),
                    start = Convert.ToDateTime(dr["startTime"]).ToString("HH:mm:ss"),
                    end = Convert.ToDateTime(dr["endTime"]).ToString("HH:mm:ss"),
                    description = dr["Remark"].ToString(),
                    allday = false,
                    seriesID = dr["seriesID"].ToString()
                };
            }

            return _employeeroster;
        }


        public static ResourseList GetEventByEventID(int _id)
        {
            var _employeeroster = new ResourseList();
            DataSet dsProject = new DataSet();

            var _emproster = new employeeroster();

            dsProject = DataAccess.FetchRS(CommandType.Text, EmployeeRosterSQL.GetEventByEventID(_id), null);
            var empList = new List<ResourseList>();
            empList = dsProject.Tables[0].AsEnumerable().Select(dataRow => new ResourseList
            {
                id = dataRow.Field<int>("ID").ToString(),
                rosterID = dataRow.Field<int>("Roster_ID").ToString(),
                rosterDate = dataRow.Field<DateTime>("Roster_Date").ToString("dd-MMM-yyyy"),
                rosterDateTo = dataRow.Field<DateTime>("endTime").ToString("dd-MMM-yyyy"),
                teamID = dataRow.Field<int>("teamID").ToString(),
                projectID = dataRow.Field<int>("projectID").ToString(),
                title = dataRow.Field<string>("title"),
                start = dataRow.Field<DateTime>("startTime").ToString("HH:mm:ss"),
                end = dataRow.Field<DateTime>("endTime").ToString("HH:mm:ss"),
                description = dataRow.Field<string>("Remark"),
                allday = dataRow.Field<bool>("allday").ToString().ToLower(),
                seriesID = dataRow.Field<Guid>("seriesID").ToString(),
                shiftEnd= dataRow.Field<DateTime>("shiftEnd").ToString("dd-MMM-yyyy"),
                shiftEndTime = dataRow.Field<DateTime>("shiftEnd").ToString("HH:mm:ss"),
                BRKNEXTDAY = dataRow.Field<string>("BRKNEXTDAY"),
                breakTime_hrs= dataRow.Field<int>("BreakTimeNH").ToString(),
                breakTime_start = dataRow.Field<int>("breakTimeAfter").ToString(),
                early_in= calctimespan(dataRow.Field<string>("InTime"),dataRow.Field<string>("EarlyInBy")),
                late_in = calctimespan(dataRow.Field<string>("InTime"),dataRow.Field<string>("LateInBy")),

                employeeList = new List<employeesinseries>()
            }).ToList();

     
           // _employeeroster = CalculateReverseCommonSettings(empList[0]);
            return empList[0];
        }

        private static string calctimespan(string from, string to)
        {
            TimeSpan _timeSpanStart = TimeSpan.Parse(from, CultureInfo.InvariantCulture);
            TimeSpan _timeSpanEnd = TimeSpan.Parse(to, CultureInfo.InvariantCulture);
            double timeDifference = _timeSpanStart.Subtract(_timeSpanEnd).Minutes;
            return Math.Abs(timeDifference).ToString();
        }

        private static ResourseList CalculateReverseCommonSettings(ResourseList roster)
        {


            TimeSpan _timeSpanStart = TimeSpan.Parse(roster.start.ToString().Split(' ')[1], CultureInfo.InvariantCulture);
            TimeSpan _timeSpanEnd = TimeSpan.Parse(roster.end.ToString().Split(' ')[1], CultureInfo.InvariantCulture);
            TimeSpan _inTime = _timeSpanStart;
            TimeSpan _outTime = _timeSpanEnd;

            TimeSpan timeearly;
            if (!TimeSpan.TryParse(roster.early_in, out timeearly))
            {
                // handle validation error
            }

           // double _early=_inTime.Subtract(TimeSpan.FromMinutes(timeearly));

            //int _earlyInBy = _inTime.Subtract(TimeSpan.FromMinutes(TimeSpan.Parse(roster.early_in));
            //TimeSpan _lateInBy = _inTime.Add(TimeSpan.FromMinutes(roster.late_in));
            //TimeSpan _earlyOutBy = _outTime.Subtract(TimeSpan.FromMinutes(roster.early_out.Value));
            //TimeSpan _breakTimeNHhr = _inTime.Add(TimeSpan.FromMinutes(roster.breakTime_start.Value));
            //        int _breakTimeNH = roster.breakTime_hrs;
            //int _breakTimeOT = roster.BreakTimeOT != null ? roster.BreakTimeOT.Value : 0;
            //int _breakTimeAfter = roster.breakTime_start != null ? roster.breakTime_start.Value : 0;

            //roster.InTime = _inTime.ToString();
            //roster.OutTime = _outTime.ToString();
            //roster.EarlyInBy = _earlyInBy.ToString();
            //roster.LateInBy = _lateInBy.ToString();
            //roster.EarlyOutBy = _earlyOutBy.ToString();
            //roster.BreakTimeNH = _breakTimeNH;
            //roster.BreakTimeOT = _breakTimeOT;
            //roster.BreakTimeNHhr = _breakTimeNHhr.ToString();
            //roster.BreakTimeAfter = _breakTimeAfter;


            return roster;
        }

        public static List<EmployeeView> CheckEmployeeListByGroup(int _teamid)
        {

            DataSet dsProject = new DataSet();
            dsProject = DataAccess.FetchRS(CommandType.Text, EmployeeRosterSQL.CheckEmployeeListByGroup(_teamid), null);
            var empList = new List<EmployeeView>();
            empList = dsProject.Tables[0].AsEnumerable().Select(dataRow => new EmployeeView
            {
                emp_code = dataRow.Field<int>("emp_code"),
                teamID = _teamid,
                empName = dataRow.Field<string>("emp_name")            

            }).ToList();
            return empList;
        }


        public static string GetFormatDateTime(DateTime _dateval)
        {
            return _dateval.ToString("yyyy-MM-dd HH:mm:ss.000");
        }
        public static IEnumerable<ProjectsDisplay> getProjectList(int _companyid)
        {
            DataSet dsProject = new DataSet();
            var _list = DataAccess.FetchRS(CommandType.Text, EmployeeRosterSQL.ProjectListQuery(_companyid), null);
            var _result = new List<ProjectsDisplay>();
            _result = _list.Tables[0].AsEnumerable().Select(dataRow => new ProjectsDisplay
            {
                mainID = dataRow.Field<int>("ID"),
                Description = dataRow.Field<string>("Sub_Project_Name")
            }).ToList();
            return _result;
        }

        public static List<ResourseList> GetEmployeeRosterListByMoreFilters(employeeroster _employeeroster)
        {
     
            DataSet dsProject = new DataSet();
            dsProject = DataAccess.FetchRS(CommandType.Text, EmployeeRosterSQL.GetEmployeeRosterListByMoreFilters(_employeeroster), null);
            var empList = new List<ResourseList>();
            empList = dsProject.Tables[0].AsEnumerable().Select(dataRow => new ResourseList
            {
                id = dataRow.Field<int>("ID").ToString(),
                rosterID = dataRow.Field<int>("Roster_ID").ToString(),
                title = dataRow.Field<string>("title"),
                start = dataRow.Field<DateTime>("startTime").ToString("dd MMM yyy|hh:mm tt", CultureInfo.InvariantCulture),
                end = dataRow.Field<DateTime>("endTime").ToString("dd MMM yyy|hh:mm tt", CultureInfo.InvariantCulture),
                resourceId = dataRow.Field<int>("ID").ToString(),
                projectID = dataRow.Field<int>("projectID").ToString(),
                teamID = dataRow.Field<int>("teamID").ToString(),
                eventTitle = dataRow.Field<string>("title"),
                description = dataRow.Field<string>("remark"),
                textColor = "yellow",
                eventColor = "#378006!important",
                //start_end = dataRow.Field<DateTime>("endTime").Subtract(dataRow.Field<DateTime>("startTime")).Hours.ToString(),
                start_end = getdifference(Convert.ToDateTime(dataRow.Field<DateTime>("startTime").ToString("yyyy-MM-dd HH:mm:ss")), Convert.ToDateTime(dataRow.Field<DateTime>("endTime").ToString("yyyy-MM-dd HH:mm:ss"))).ToString(),
                employeeName = dataRow.Field<string>("emp_name"),
               projectName = dataRow.Field<string>("projectName"),
                teamName = dataRow.Field<string>("teamName"),
                seriesID= dataRow.Field<string>("teamName")
            }).ToList();
            return empList;
        }

        public static List<ResourseList> GetEmployeeRosterListByGrouping(employeeroster _employeeroster)
        {

            DataSet dsProject = new DataSet();
            dsProject = DataAccess.FetchRS(CommandType.Text, EmployeeRosterSQL.GetEmployeeRosterListByGrouping(_employeeroster), null);
            var empList = new List<ResourseList>();
            empList = dsProject.Tables[0].AsEnumerable().Select(dataRow => new ResourseList
            {
                seriesID = dataRow.Field<Guid>("seriesID").ToString(),   
                title = dataRow.Field<string>("title"),
                start = dataRow.Field<DateTime>("startTime").ToString("dd MMM yyy|hh:mm tt", CultureInfo.InvariantCulture),
                end = dataRow.Field<DateTime>("endTime").ToString("dd MMM yyy|hh:mm tt", CultureInfo.InvariantCulture),
                color =dataRow.Field<int>("rostercount").ToString(),
                projectName = dataRow.Field<string>("Sub_Project_Name").ToString(),
                timestart= dataRow.Field<string>("InTime").ToString(),
                timeend = dataRow.Field<string>("OutTime").ToString(),
                start_end = getdifference(Convert.ToDateTime(dataRow.Field<DateTime>("starttime").ToString("yyyy-MM-dd HH:mm:ss")), Convert.ToDateTime(dataRow.Field<DateTime>("endtime").ToString("yyyy-MM-dd HH:mm:ss"))).ToString(),
                //start_end = getdifference(Convert.ToDateTime(dataRow.Field<string>("InTime")), Convert.ToDateTime(dataRow.Field<string>("OutTime"))).ToString(),
            }).ToList();
            return empList;
        }


 public static List<ResourseList> GetEmployeeRosterListBySeries(Guid _seriesid, int _empID = 0)
        {

            if (_empID ==1)
            {
                _empID = DataAccess.ExecuteScalarQuery(EmployeeRosterSQL.GetEmpCodebySeriesID(_seriesid));
            }


            DataSet dsProject = new DataSet();
            dsProject = DataAccess.FetchRS(CommandType.Text, EmployeeRosterSQL.GetEmployeeRosterListBySeries(_seriesid,_empID), null);
            var empList = new List<ResourseList>();
            empList = dsProject.Tables[0].AsEnumerable().Select(dataRow => new ResourseList
            {
                id = dataRow.Field<int>("ID").ToString(),
                rosterID = dataRow.Field<int>("Roster_ID").ToString(),
                title = dataRow.Field<string>("title"),
                start = dataRow.Field<DateTime>("startTime").ToString("dd MMM yyy|hh:mm tt", CultureInfo.InvariantCulture),
                end = dataRow.Field<DateTime>("endTime").ToString("dd MMM yyy|hh:mm tt", CultureInfo.InvariantCulture),
                resourceId = dataRow.Field<int>("ID").ToString(),
                projectID = dataRow.Field<int>("projectID").ToString(),
                teamID = dataRow.Field<int>("teamID").ToString(),
                eventTitle = dataRow.Field<string>("title"),
                description = dataRow.Field<string>("remark"),
                textColor = "yellow",
                eventColor = "#378006!important",
                //start_end = dataRow.Field<DateTime>("endTime").Subtract(dataRow.Field<DateTime>("startTime")).Hours.ToString(),
                start_end = getdifference(Convert.ToDateTime(dataRow.Field<DateTime>("startTime").ToString("yyyy-MM-dd HH:mm:ss")), Convert.ToDateTime(dataRow.Field<DateTime>("endTime").ToString("yyyy-MM-dd HH:mm:ss"))).ToString(),
                employeeName = dataRow.Field<string>("emp_name"),
               projectName = dataRow.Field<string>("projectName"),
                teamName = dataRow.Field<string>("teamName"),
                seriesID= dataRow.Field<string>("teamName")
            }).ToList();
            return empList;
        }

        public static CommonRosterSettings GetRosterCommonSettings(int _companyID)
        {
            var _employeeroster = new CommonRosterSettings();
            SqlDataReader dr = DataAccess.GetDataReader(EmployeeRosterSQL.GetRosterCommonSettings(_companyID));

            while (dr.Read())
            {
                _employeeroster = new CommonRosterSettings()
                {
                    settingid = Convert.ToInt32(dr["id"]),
                    early_in = Convert.ToInt32(dr["early_in"]),
                    late_in = Convert.ToInt32(dr["late_in"]),
                    early_out = Convert.ToInt32(dr["early_out"]),
                    breakTime_start = Convert.ToInt32(dr["breakTime_start"]),
                    breakTime_hrs = Convert.ToInt32(dr["breakTime_hrs"]),
                    breakTime_OT_start = Convert.ToInt32(dr["breakTime_OT_start"]),
                    breakTime_OT_hrs = Convert.ToInt32(dr["breakTime_OT_hrs"]),
                    total_flexi_hrs = Convert.ToInt32(dr["total_flexi_hrs"]),
                    flexi_breakTime_start = Convert.ToInt32(dr["flexi_breakTime_start"]),
                    flexi_breakTime_hrs = Convert.ToInt32(dr["flexi_breakTime_hrs"]),
                    flexi_breakTime_OT_start = Convert.ToInt32(dr["flexi_breakTime_OT_start"]),
                    flexi_breakTime_OT_hrs = Convert.ToInt32(dr["flexi_breakTime_OT_hrs"]),
                    company_id = Convert.ToInt32(dr["early_in"]),
                    shiftEnd = null
                };
            }

            return _employeeroster;
        }


        public static int DeleteEvent(employeeroster _roster)
        {
            if (_roster.id > 0)
            {
                int dsRoster = DataAccess.ExecuteNonQuery(EmployeeRosterSQL.DeleteEvent(_roster), null);

                //if(_roster.filterType=="Wholeseries")

            }
            return _roster.id;
        }
    }
    public partial class ProjectsDisplay
    {
        public int mainID { get; set; }
        public string Description { get; set; }
    }
}



