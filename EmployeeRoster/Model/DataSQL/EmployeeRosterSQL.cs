using SMEPayroll.Roster;
using System;
using System.Globalization;
using SMEPayroll.EmployeeRoster.Service;
namespace SMEPayroll.EmployeeRoster.Model.DataSQL
{
    public class EmployeeRosterSQL
    {
        public static string SaveEmployeeRoster(employeeroster roster)
        {

            //var _sql = "INSERT INTO employeeroster (projectID,teamID,rosterType,rosterDate,startTime,endTime,title,employeeID,description,seriesID) "
            //           + "VALUES(" + roster.projectID + "," + roster.teamID + ",'" + roster.rosterType + "', "
            //           + "'" + roster.rosterDate.Value.ToString("yyyy-MM-dd hh:ss") + "', "
            //           + "'" + EmployeeRosterService.GetFormatDateTime(roster.startTime.Value) + "', "
            //           + "'" + EmployeeRosterService.GetFormatDateTime(roster.endTime.Value) + "', "
            //           +"'" + roster.title +"', "
            //           +"" + roster.employeeID +", "
            //            + "'" + roster.description + "', "
            //           + "'" + roster.seriesID + "');";
            //return _sql;


            var _sql = "INSERT INTO roster (Company_Id,EmpID,Roster_Name,noRoster) "
           + "VALUES(" + roster.company_id + "," + roster.employeeID + ",'" + roster.title + "', 0);";
            return _sql;


        }

        public static string SaveEmployeeRosterDetail(employeeroster roster)
        {



            // var _sql = "INSERT INTO RosterDetail (Roster_ID,roster_Date,projectID,teamID,rosterType,startTime,endTime,title,description,seriesID) "
            //+ "VALUES(" + roster.employeeID + ",'" + roster.rosterDate.Value.ToString("yyyy-MM-dd hh:ss") + "'," + roster.projectID + "," + roster.teamID + ",'" + roster.rosterType + "', "
            //+ "'" + roster.rosterDate.Value.ToString("yyyy-MM-dd hh:ss") + "', "
            //+ "'" + EmployeeRosterService.GetFormatDateTime(roster.startTime.Value) + "', "
            //+ "'" + EmployeeRosterService.GetFormatDateTime(roster.endTime.Value) + "', "
            //+ "'" + roster.title + "', "
            // + "'" + roster.description + "', "
            //+ "'" + roster.seriesID + "');";
            // return _sql;

            var _sql = "INSERT INTO RosterDetail (Roster_ID,Roster_Date,BreakTimeAfter, "
                        + "InTime,OutTime,EarlyInBy,LateInBy,EarlyOutBy, "
                        + "BreakTimeNHhr,BreakTimeOThr,BreakTimeNH,BreakTimeOT,"
                        + "RosterType,CreateDate,BRKNEXTDAY,Remark, "
                        + "title,projectID,teamID,startTime,endTime,seriesID,shiftEnd,allday)"
                        + "VALUES( "+roster.rosterID+",'"+ roster.rosterDate.Value.ToString("yyyy-MM-dd hh:ss") +"',"+ roster.BreakTimeAfter + ","
                        +"'" + roster.InTime + "','" + roster.OutTime + "','" + roster.EarlyInBy + "','" + roster.LateInBy + "','" + roster.EarlyOutBy + "',"
                        + "'" + roster.BreakTimeNHhr + "','" + roster.BreakTimeOThr + "'," + roster.BreakTimeNH + "," + roster.BreakTimeOT + ","
                        + "'" + roster.rosterType + "','" + DateTime.Today.ToString("yyyy-MM-dd hh:ss") + "','" + roster.BRKNEXTDAY + "','" + roster.description.Replace("'","''") + "',"
                        +"'" + roster.title + "'," + roster.projectID + "," + roster.teamID + ",'" + EmployeeRosterService.GetFormatDateTime(roster.startTime.Value) + "','" + EmployeeRosterService.GetFormatDateTime(roster.endTime.Value) + "','" + roster.seriesID + "','" + EmployeeRosterService.GetFormatDateTime(roster.shiftEnd.Value) + "',"
                        +"'"+ roster.allday +"'"
                        + ");";
            return _sql;

        }


        public static string UpdateEmployeeRoster(employeeroster roster)
        {
            //var _sql = "UPDATE employeeroster SET  "
            //           + "rosterDate='" + EmployeeRosterService.GetFormatDateTime(roster.rosterDate.Value) + "', "
            //           + "startTime='" + EmployeeRosterService.GetFormatDateTime(roster.startTime.Value) + "', "
            //           + "endTime= '" + EmployeeRosterService.GetFormatDateTime(roster.endTime.Value) + "', "
            //           + "title='" + roster.title + "', "
            //           + "description='" + roster.description + "' ";
            //if (roster.filterType == "WholeSeries")
            //    _sql += "WHERE seriesID='" + roster.seriesID + "';";
            //else
            //    _sql += "WHERE rosterID=" + roster.rosterID + ";";


            var _sql = "UPDATE RosterDetail SET  "
                       + "Roster_Date='" + EmployeeRosterService.GetFormatDateTime(roster.rosterDate.Value) + "', "
                       + "startTime='" + EmployeeRosterService.GetFormatDateTime(roster.startTime.Value) + "', "
                       + "endTime= '" + EmployeeRosterService.GetFormatDateTime(roster.endTime.Value) + "', "
                       + "title='" + roster.title + "', "
                       + "Remark='" + roster.description.Replace("'", "''") + "', "
                       + "shiftEnd='" + EmployeeRosterService.GetFormatDateTime(roster.shiftEnd.Value) + "', "
                       + "InTime='" + roster.InTime + "', "
                       + "OutTime='" + roster.OutTime + "', "
                       + "EarlyInBy='" + roster.EarlyInBy + "', "
                       + "LateInBy='" + roster.LateInBy + "', "
                       + "BreakTimeNH='" + roster.BreakTimeNH + "', "
                       + "BreakTimeAfter='" + roster.BreakTimeAfter + "', "
                       + "BRKNEXTDAY='" + roster.BRKNEXTDAY + "', "
                       + "BreakTimeOThr='" + roster.BreakTimeOThr + "', "
                       + "BreakTimeOT='" + roster.BreakTimeOT + "', "
                       +"allday='" + roster.allday + "' ";
            if (roster.filterType == "WholeSeries")
                _sql += "WHERE seriesID='" + roster.seriesID + "';";
            else
            {
                _sql += ",seriesID='" + Guid.NewGuid().ToString() + "'";
                _sql += "WHERE ID=" + roster.id + ";";
            }


            return _sql;
        }


        public static string DragRoster(employeeroster roster)
        {
            //var _sql = "UPDATE employeeroster SET  "
            //           + "rosterDate='" + EmployeeRosterService.GetFormatDateTime(roster.rosterDate.Value) + "',"
            //           + "startTime = '" + EmployeeRosterService.GetFormatDateTime(roster.startTime.Value) + "',"
            //           + "endTime='" + EmployeeRosterService.GetFormatDateTime(roster.endTime.Value) + "'  ";
            //if (roster.filterType == "WholeSeries")
            //    _sql += "WHERE seriesID='" + roster.seriesID + "';";
            //else
            //    _sql += "WHERE rosterID=" + roster.rosterID + ";";


            var _sql = "UPDATE RosterDetail SET  "
                   + "Roster_Date='" + EmployeeRosterService.GetFormatDateTime(roster.rosterDate.Value) + "',"
                   + "startTime = '" + EmployeeRosterService.GetFormatDateTime(roster.startTime.Value) + "',"
                   + "endTime='" + EmployeeRosterService.GetFormatDateTime(roster.endTime.Value) + "'  ";
            if (roster.filterType == "WholeSeries")
                _sql += "WHERE seriesID='" + roster.seriesID + "';";
            else
            {
                _sql += ",seriesID='" + Guid.NewGuid().ToString() + "'";
                _sql += "WHERE ID=" + roster.id + ";";
            }

            return _sql;
        }


        public static string ResizeSchedule(employeeroster roster)
        {
            //var _sql = "UPDATE employeeroster SET  "
            //     + "rosterDate='" + EmployeeRosterService.GetFormatDateTime(roster.startTime.Value) + "' "
            //     + ",startTime='" + EmployeeRosterService.GetFormatDateTime(roster.startTime.Value) + "' "
            //           + ",endTime='" + EmployeeRosterService.GetFormatDateTime(roster.endTime.Value) + "' ";
            //if (roster.filterType == "WholeSeries")
            //    _sql += "WHERE seriesID='" + roster.seriesID + "';";
            //else
            //    _sql += "WHERE rosterID=" + roster.rosterID + ";";

            var _sql = "UPDATE RosterDetail SET  "
                     + "Roster_Date='" + EmployeeRosterService.GetFormatDateTime(roster.startTime.Value) + "' "
                     + ",startTime='" + EmployeeRosterService.GetFormatDateTime(roster.startTime.Value) + "' "
                     + ",endTime='" + EmployeeRosterService.GetFormatDateTime(roster.endTime.Value) + "' ";
            if (roster.filterType == "WholeSeries")
                _sql += "WHERE seriesID='" + roster.seriesID + "';";
            else
            {
                _sql += ",seriesID='" + Guid.NewGuid().ToString() + "'";
                _sql += "WHERE ID=" + roster.id + ";";
            }
            return _sql;
        }


        public static string CheckAlreadyAssigned(employeeroster _employeeroster)
        {
            //var _sql = "";
           var _sql = "SELECT * FROM employeeroster WHERE startTime= '" + _employeeroster.startTime.Value.ToString("yyyy-MM-dd hh:ss") + "'  "
                       +"AND endTime='" + _employeeroster.endTime.Value.ToString("yyyy-MM-dd hh:ss") + "'  "
                       +"AND teamID=" + _employeeroster.teamID + " "
                       +"AND projectID=" + _employeeroster.projectID + " "
                       +"AND employeeID=" + _employeeroster.employeeID + ";";
            return _sql;
        }

        public static string CheckAlreadyAssignedBySamedate(employeeroster _employeeroster)
        {
            var _sql = "SELECT * FROM employeeroster WHERE  CAST(rosterDate as date)='" + _employeeroster.rosterDate.Value.ToString("yyyy-MM-dd") + "'  "
                        + "AND  CAST(startTime as date)= '" + _employeeroster.startTime.Value.ToString("yyyy-MM-dd") + "'  "
                        + "AND CAST(endTime as date)='" + _employeeroster.endTime.Value.ToString("yyyy-MM-dd") + "' "
                        + "AND teamID=" + _employeeroster.teamID + " "
                        + "AND projectID=" + _employeeroster.projectID + " "
                        + "AND employeeID=" + _employeeroster.employeeID + ";";
            return _sql;
        }

        public static string CheckAlreadyAssignedBySamedateInRosterDetail(employeeroster _employeeroster)
        {
            //var _sql2 = "SELECT * FROM RosterDetail WHERE "
            //            + "teamID=" + _employeeroster.teamID + " "
            //            + "AND projectID=" + _employeeroster.projectID + " "
            //            + "AND  (startTime >= '" +  EmployeeRosterService.GetFormatDateTime(_employeeroster.startTime.Value) + "'  AND endTime <='" + EmployeeRosterService.GetFormatDateTime(_employeeroster.endTime.Value) + "') "
            //            + "AND Roster_ID=" + _employeeroster.rosterID + ";";


            var _sql = "SELECT * FROM RosterDetail "
                    + "WHERE  Roster_ID = " + _employeeroster.rosterID + "  AND ID<> " + _employeeroster.id + " "
                    + "AND( '" + EmployeeRosterService.GetFormatDateTime(_employeeroster.startTime.Value) + "' >=startTime  AND '" + EmployeeRosterService.GetFormatDateTime(_employeeroster.startTime.Value) + "'<=endTime AND Roster_ID = " + _employeeroster.rosterID + " AND ID<> " + _employeeroster.id + ") "
                    + "AND  ( '" + EmployeeRosterService.GetFormatDateTime(_employeeroster.endTime.Value) + "'>=startTime AND Roster_ID = " + _employeeroster.rosterID + " AND ID<> " + _employeeroster.id + ") "
                    + "OR( startTime>='" + EmployeeRosterService.GetFormatDateTime(_employeeroster.startTime.Value) + "' and endTime <='" + EmployeeRosterService.GetFormatDateTime(_employeeroster.endTime.Value) + "' AND Roster_ID = " + _employeeroster.rosterID + "  AND ID<> " + _employeeroster.id +" ); ";
            //+ "AND  ( '" + EmployeeRosterService.GetFormatDateTime(_employeeroster.endTime.Value) + "'>=startTime  AND '" + EmployeeRosterService.GetFormatDateTime(_employeeroster.endTime.Value) + "'<=endTime AND Roster_ID = " + _employeeroster.rosterID + " AND ID<> " + _employeeroster.id + ");";



            return _sql;
        }
        public static string CheckAlreadyAssignedInRosterTable(employeeroster _employeeroster)
        {
            var _sql = "SELECT ID FROM Roster WHERE "
                        + "Company_Id=" + _employeeroster.company_id + " "
                        + "AND EmpID=" + _employeeroster.employeeID + ";";
            return _sql;
        }
        public static string CheckAlreadyAssignedRosterDate(employeeroster _employeeroster)
        {
            var _sql = "SELECT * FROM employeeroster WHERE  CAST(rosterDate as date)='" + _employeeroster.rosterDate.Value.ToString("yyyy-MM-dd") + "'  "
                        + "AND teamID=" + _employeeroster.teamID + " "
                        + "AND employeeID=" + _employeeroster.employeeID + " "
                        + "AND projectID=" + _employeeroster.projectID + ";";
            return _sql;
        }



        public static string CheckEmployeeCountByGroup(int _groupid)
        {
            var _sql = "SELECT COUNT(Emp_ID) FROM TeamSchedulerAssigned WHERE Team_ID= " + _groupid + ";";
            return _sql;
        }

        public static string GetTeamID(int _empcode)
        {
            var _sql = "SELECT Team_ID FROM TeamSchedulerAssigned WHERE Emp_ID= " + _empcode + ";";
            return _sql;
        }

        public static string GetEmpCode(int _teamID)
        {
            var _sql = "SELECT Emp_ID FROM TeamSchedulerAssigned WHERE Team_ID= " + _teamID + ";";
            return _sql;
        }

        public static string GetEmpCodebySeriesID(Guid _seriesID)
        {
            var _sql = "SELECT r.EmpID from roster r inner join RosterDetail rd on r.ID = rd.Roster_ID where rd.seriesID= '" + _seriesID.ToString() + "';";

            return _sql;
        }
        public static string CheckEmployeeListByGroup(int _groupid)
        {
            var _sql = "SELECT e.emp_code,e.emp_name FROM employee e INNER JOIN TeamSchedulerAssigned  tas ON e.emp_code=tas.emp_ID WHERE tas.Team_ID= " + _groupid + ";";
            return _sql;
        }


        //public static string GetEmployeeRosterListByFilter(employeeroster _employeeroster)
        //{
        //    var _sql = "";
        //    if(_employeeroster.filterType=="Team")
        //     _sql = "Select emp_code,emp_name From employee where GroupID ="+ _employeeroster.teamID + "";
        //    else
        //        _sql = "Select emp_code,emp_name From employee where projectID =" + _employeeroster.projectID + "";
        //    return _sql;
        //}



        /// <summary>
        /// /To do add more columns 
        /// </summary>
        /// <param name="_employeeroster"></param>
        /// <returns></returns>
        public static string GetEmployeeRosterListByFilter(employeeroster _employeeroster)
        {

            // Below commented code is old one
            //var _sql = "SELECT  sp.Sub_Project_Name,ug.GroupName,er.*,e.emp_code,e.emp_name From EmployeeRoster er INNER JOIN employee e ON er.teamID=e.GroupID AND er.employeeID=e.emp_code "
            //    + "INNER JOIN SubProject sp ON er.projectID=sp.ID "
            //    +"INNER JOIN UserGroups ug ON er.teamID = ug.GroupID";

            //if (_employeeroster.displayFilterType == "Employee")
            //    _sql = _sql + " WHERE er.employeeID=" + _employeeroster.employeeID + "";
            //else if (_employeeroster.displayFilterType == "Team")
            //    _sql = _sql + " WHERE er.teamID=" + _employeeroster.teamID + "";

            //else if (_employeeroster.displayFilterType == "Project")
            //    _sql = _sql + " WHERE er.projectID=" + _employeeroster.projectID + "";


            var _sql = "SELECT  sp.Sub_Project_Name,ISNULL(ug.Team_Name, '')AS GroupName,r.EmpID,er.*,e.emp_code,e.emp_name From RosterDetail er "
                      + " INNER JOIN roster r ON r.ID = er.Roster_ID "
                    + "INNER JOIN employee e ON r.EmpID=e.emp_code  "
                    + "INNER JOIN SubProject sp ON er.projectID=sp.ID "
                    + "left JOIN TeamScheduler ug ON er.teamID = ug.ID";
            //+ "INNER JOIN UserGroups ug ON er.teamID = ug.GroupID";

            if (_employeeroster.displayFilterType == "Employee")
                _sql = _sql + " WHERE r.EmpID=" + _employeeroster.employeeID + "";
            else if (_employeeroster.displayFilterType == "Team")
                _sql = _sql + " WHERE er.teamID=" + _employeeroster.teamID + "";

            else if (_employeeroster.displayFilterType == "Project")
                _sql = _sql + " WHERE er.projectID=" + _employeeroster.projectID + "";




            return _sql;

        }

        public static string GetEventByEventID(int _id)
        {
            var _sql = "";
           // _sql = "SELECT * From EmployeeRoster  WHERE rosterID=" + _id + "";
            _sql = "SELECT * From RosterDetail  WHERE ID=" + _id + "";
            return _sql;
        }

        public static string GetEventEmployeesBySeriesID(string _seriesid)
        {
            var _sql = "";
            _sql = "SELECT er.seriesID, e.emp_code,e.emp_name From EmployeeRoster er INNER JOIN employee e ON er.teamID=e.GroupID AND er.employeeID=e.emp_code "
                   + "WHERE er.seriesID = '" + _seriesid + "'";
            return _sql;
        }


        public static string ProjectListQuery(int _companyid)
        {
            var _sql = "select SP.ID,SP.Sub_Project_Name + '-' + c.Company_Code as Sub_Project_Name  from SubProject SP " +
                        "inner join Project P on P.ID = SP.Parent_Project_ID " +
                        "inner join Company c on c.Company_Id = P.Company_ID " +
                        "where P.Company_ID = " + _companyid + " or isShared = 'Yes' ;  ";
            return _sql;
        }


        /// <summary>
        /// /To do add more columns 
        /// </summary>
        /// <param name="_employeeroster"></param>
        /// <returns></returns>
        public static string GetEmployeeRosterListByMoreFilters(employeeroster _employeeroster)
        {
            var _sql = "SELECT er.*,sp.Sub_Project_Name as projectName,ISNULL(ug.Team_Name, '')AS teamName, e.emp_code,e.emp_name From RosterDetail er "
                + "INNER JOIN Roster r ON r.ID = er.Roster_ID "
                 + "INNER JOIN employee e ON r.EmpID=e.emp_code  "
                + "INNER JOIN SubProject sp ON er.projectID = sp.ID "
                + "left JOIN TeamScheduler ug ON er.teamID = ug.ID";
            if (_employeeroster.filterType == "Employee")
                _sql = _sql + " WHERE r.empID=" + _employeeroster.employeeID + "";
            else if (_employeeroster.filterType == "Team")
                _sql = _sql + " WHERE er.teamID=" + _employeeroster.teamID + "";

            else if (_employeeroster.filterType == "Project")
                _sql = _sql + " WHERE er.projectID=" + _employeeroster.projectID + "";

            return _sql;

        }

        public static string GetEmployeeRosterListBySeries(Guid _seriesid, int _empID = 0)
        {
            var _sql = "SELECT er.*,sp.Sub_Project_Name as projectName,ISNULL(ug.Team_Name, '')AS teamName, e.emp_code,e.emp_name From RosterDetail er "
                + "INNER JOIN Roster r ON r.ID = er.Roster_ID "
                 + "INNER JOIN employee e ON r.EmpID=e.emp_code  "
                + "INNER JOIN SubProject sp ON er.projectID = sp.ID "
                + "left JOIN TeamScheduler ug ON er.teamID = ug.ID "; 
                if(_empID>0)
                _sql += " WHERE er.seriesID='" + _seriesid.ToString() + "' AND e.emp_code="+ _empID +"";
                else
                _sql += " WHERE er.seriesID='" + _seriesid.ToString() + "'";

            return _sql;

        }
        public static string GetEmployeeRosterListByGrouping(employeeroster _employeeroster)
        {
            // var _sql = "SELECT CAST(rd.starttime AS date) AS starttime, count(rd.ID) AS rostercount,rd.seriesID,rd.title FROM RosterDetail rd "
            // + "INNER JOIN Roster r ON r.ID = rd.Roster_ID ";


            var _sql = "SELECT starttime  AS starttime,rd.endTime AS endtime, count(rd.ID) AS rostercount,rd.seriesID,rd.title,rd.InTime,rd.OutTime ,sp.Sub_Project_Name FROM RosterDetail rd "
                      + "INNER JOIN Roster r ON r.ID = rd.Roster_ID "
                      + "INNER JOIN SubProject sp ON rd.projectID = sp.ID ";

            if (_employeeroster.filterType == "Employee")
                _sql = _sql + " WHERE r.empID=" + _employeeroster.employeeID + "";
            else if (_employeeroster.filterType == "Team")
                _sql = _sql + " WHERE rd.teamID=" + _employeeroster.teamID + "";

            else if (_employeeroster.filterType == "Project")
                _sql = _sql + " WHERE rd.projectID=" + _employeeroster.projectID + " ";

            if (_employeeroster.startTime != null && _employeeroster.endTime != null)
                _sql = _sql + "AND  (CAST(rd.startTime AS DATE) >= '" + EmployeeRosterService.GetFormatDateTime(_employeeroster.startTime.Value) + "'  AND CAST(rd.endTime AS DATE) <='" + EmployeeRosterService.GetFormatDateTime(_employeeroster.endTime.Value) + "') ";
                _sql = _sql + "GROUP BY starttime, rd.endTime,rd.seriesID,rd.title,rd.InTime,rd.OutTime,sp.Sub_Project_Name  order by starttime desc;";
            //_sql = _sql + "GROUP BY CAST(rd.starttime AS date),rd.seriesID,rd.title;";

            return _sql;

        }

        public static string GetRosterCommonSettings(int _companyID)
        {
            var _sql = "SELECT * FROM common_roster_settings WHERE company_id="+ _companyID +" ;";
            return _sql;
        }

        public static string DeleteEvent(employeeroster _roster)
        {
            var _sql = "DELETE FROM RosterDetail WHERE ID=" + _roster.id + ";";

            if (_roster.filterType == "WholeSeries")
                _sql = "DELETE FROM RosterDetail WHERE seriesID='" + _roster.seriesID + "';";
          
           return _sql;
        }
    }
}