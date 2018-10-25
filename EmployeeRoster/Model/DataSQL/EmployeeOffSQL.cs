using SMEPayroll.EmployeeRoster.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SMEPayroll.EmployeeRoster.Model.DataSQL
{
    public class EmployeeOffSQL
    {
        public static string AssignEmployeeOff(EmployeeOff _employeeOffDay)
        {
            var _sql = "INSERT INTO EmployeeOffDay (employeeID,offDateFrom,offDateTo,startTime,endTime,title,description) "
                  + "VALUES(" + _employeeOffDay.employeeID + ",'" + _employeeOffDay.offDateFrom.Value.ToString("yyyy-MM-dd hh:ss") + "','" + _employeeOffDay.offDateTo.Value.ToString("yyyy-MM-dd hh:ss") + "','" + _employeeOffDay.startTime.Value.ToString("yyyy-MM-dd hh:ss") + "','" + _employeeOffDay.endTime.Value.ToString("yyyy-MM-dd hh:ss") + "','" + _employeeOffDay.title + "','" + _employeeOffDay.description + "');";

            return _sql;
        }


        public static string CheckAlreadyAssigned(EmployeeOff _employeeOffDay)
        {
            var _sql = "SELECT * FROM EmployeeOffDay "
                + "WHERE CAST(offDateFrom as date)= '" + _employeeOffDay.offDateFrom.Value.ToString("yyyy-MM-dd") + "' "
                + "AND employeeID=" + _employeeOffDay.employeeID + ";";
            return _sql;
        }

        public static string CheckAlreadyAssignedRoster(EmployeeOff _employeeOffDay)
        {
            var _sql = "SELECT * FROM EmployeeRoster WHERE  (CAST(startTime as date)='" + _employeeOffDay.offDateFrom.Value.ToString("yyyy-MM-dd") + "'  "
                        + "OR  CAST(endTime as date)= '" + _employeeOffDay.endTime.Value.ToString("yyyy-MM-dd") + "')  "
                        + "AND employeeID=" + _employeeOffDay.employeeID + ";";
            return _sql;
        }
        public static string GetEmployeeOffDays()
        {
            var _sql = "SELECT ef.*,e.emp_name,phone FROM EmployeeOffDay ef INNER JOIN employee e on ef.employeeID=e.emp_code;";
            return _sql;
        }

    public static string GetEmployeeOffListByEmployeeID(int _empid)
        {
            var _sql = "SELECT ef.*,e.emp_name,phone FROM EmployeeOffDay ef INNER JOIN employee e on ef.employeeID=e.emp_code WHERE employeeID="+ _empid +" ;";
            return _sql;
        }

        public static string GetEmployeeOffList(EmployeeOffDayListView empoff)
        {
            var _sql = "SELECT ef.*,e.emp_name,phone FROM EmployeeOffDay ef INNER JOIN employee e on ef.employeeID=e.emp_code ";

            if (empoff.filterType == "Employee")
                _sql += "WHERE ef.employeeID='" + empoff.employeeID + "';";
            else
                _sql += "WHERE CAST(offDateFrom as date)>= '" + empoff.offDateFrom.ToString("yyyy-MM-dd") + "' "
                    + "AND CAST(offDateTo as date)<= '" + empoff.offDateTo.ToString("yyyy-MM-dd") + "'";

            return _sql;
        }

        public static string DragRoster(EmployeeOff _employeeOffDay)
        {
            var _empOffDay = EmployeeOffService.GetFormatDateTime(_employeeOffDay.offDateFrom.Value);
            var _sql = "UPDATE employeeoffday SET  "
                       + "offDateFrom='" + _empOffDay + "',"
                       + "offDateTo = '" + _empOffDay + "',"
                       + "startTime = '" + _empOffDay + "',"
                       + "endTime='" + _empOffDay + "'  "
                       + "WHERE offDayID=" + _employeeOffDay.offDayID + ";";
            return _sql;
        }

        public static string GetEventByEventID(int _id)
        {
            var _sql = "";
            _sql = "SELECT ef.*,e.emp_name,phone FROM EmployeeOffDay ef INNER JOIN employee e on ef.employeeID=e.emp_code WHERE ef.offDayID=" + _id + " ;";
            return _sql;
        }


        public static string UpdateEvent(EmployeeOff _employeeOffDay)
        {
            var _sql = "UPDATE employeeoffday SET  "
                       + "description='" + _employeeOffDay.description + "',"
                       + "title = '" + _employeeOffDay.title + "' "
                       + "WHERE offDayID=" + _employeeOffDay.offDayID + ";";
            return _sql;
        }

        public static string DeleteEvent(int _offdayid)
        {
            var _sql = "DELETE FROM employeeoffday WHERE offDayID=" + _offdayid + ";";
            return _sql;
        }

    }
}