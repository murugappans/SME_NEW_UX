using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMEPayroll.EmployeeRoster.Model;
using SMEPayroll.EmployeeRoster.Model.DataSQL;
using System.Data.SqlClient;
using SMEPayroll.Roster;
using System.Data;

namespace SMEPayroll.EmployeeRoster.Service
{
    public class EmployeeOffService
    {
        public static EmployeeOff AssignEmployeeOff(EmployeeOff employeeoff)
        {
            int _count = DataAccess.ExecuteScalarQuery(EmployeeOffSQL.CheckAlreadyAssigned(employeeoff));
            int _Rosetercount = DataAccess.ExecuteScalarQuery(EmployeeOffSQL.CheckAlreadyAssignedRoster(employeeoff));
            if (_Rosetercount > 0) 
                employeeoff.returnMessage = "Warning|This employee is already assigned a roster.";

            if (_count > 0)
                employeeoff.returnMessage = "Warning|This employee is already off on this day.";

            if (_count <= 0 && _Rosetercount<=0)
            {
                int dsRoster = DataAccess.ExecuteNonQuery(EmployeeOffSQL.AssignEmployeeOff(employeeoff), null);
                _count = DataAccess.ExecuteScalarQuery(EmployeeOffSQL.CheckAlreadyAssigned(employeeoff));
                employeeoff.offDayID = _count;
                employeeoff.returnMessage = "";
            }

           return employeeoff;
        }

        public static List<OffDayView> GetEmployeeOffList()
        {
        
            var empList = new List<OffDayView>();
            SqlDataReader dr = DataAccess.GetDataReader(EmployeeOffSQL.GetEmployeeOffDays());
            while (dr.Read())
            {
                empList.Add(new OffDayView()
                {
                    id = dr["offDayID"].ToString(),
                    employeeID = dr["employeeID"].ToString(),
                    employeeName= dr["emp_name"].ToString(),
                    title = dr["title"].ToString(),
                    mobile=dr["phone"].ToString(),
                    start = Convert.ToDateTime(dr["startTime"]).ToString("s"),
                    end = Convert.ToDateTime(dr["endTime"]).ToString("s"),
                    allday = false,
                    returnMessage=""
                    //id = dr.GetValue(dr.GetOrdinal("offDayID")).ToString(),
 
                });
            }

            return empList;
        }


        public static List<OffDayView> GetEmployeeOffListByEmployeeID(int _empid)
        {
        
            var empList = new List<OffDayView>();
            SqlDataReader dr = DataAccess.GetDataReader(EmployeeOffSQL.GetEmployeeOffListByEmployeeID(_empid));
            while (dr.Read())
            {
                empList.Add(new OffDayView()
                {
                    id = dr["offDayID"].ToString(),
                    employeeID = dr["employeeID"].ToString(),
                    employeeName= dr["emp_name"].ToString(),
                    title = dr["title"].ToString(),
                    mobile=dr["phone"].ToString(),
                    start = Convert.ToDateTime(dr["startTime"]).ToString("s"),
                    end = Convert.ToDateTime(dr["endTime"]).ToString("s"),
                    allday = false
                    //id = dr.GetValue(dr.GetOrdinal("offDayID")).ToString(),
 
                });
            }

            return empList;
        }


        public static List<EmployeeOffDayListView> GetEmployeeOffDaysList(EmployeeOffDayListView empoff)
        {

            DataSet dsoff = new DataSet();
            dsoff = DataAccess.FetchRS(CommandType.Text, EmployeeOffSQL.GetEmployeeOffList(empoff), null);
            var _List = new List<EmployeeOffDayListView>();
            _List = dsoff.Tables[0].AsEnumerable().Select(dataRow => new EmployeeOffDayListView
            {
                offDayID = dataRow.Field<int>("offDayID"),
                employeeID = dataRow.Field<int>("employeeID"),
                employeeName = dataRow.Field<string>("emp_name"),
                startTime = dataRow.Field<DateTime>("startTime").ToString("dd-MMM-yyyy"),
                endTime = dataRow.Field<DateTime>("endTime").ToString("dd-MMM-yyyy"),
                phone = dataRow.Field<string>("phone"),
                description = dataRow.Field<string>("description"),
            }).ToList();

              return _List;
        }

        public static EmployeeOff DragRoster(EmployeeOff employeeoff)
        {

            if (employeeoff.offDayID > 0)
            {
                int _count = DataAccess.ExecuteScalarQuery(EmployeeOffSQL.CheckAlreadyAssigned(employeeoff));
                if (_count <= 0)
                {
                    int dsRoster = DataAccess.ExecuteNonQuery(EmployeeOffSQL.DragRoster(employeeoff), null);
                    employeeoff.returnMessage = "Success|Event date changed successfully.";
                }
                else
                    employeeoff.returnMessage = "Warning|Employee is already off on this day.";
            }

            return employeeoff;
        }


        public static OffDayView GetEventByEventID(int _id)
        {
            var _employeeOff = new OffDayView();
            SqlDataReader dr = DataAccess.GetDataReader(EmployeeOffSQL.GetEventByEventID(_id));

            while (dr.Read())
            {
                _employeeOff = new OffDayView()
                {
                    id = dr["offDayID"].ToString(),
                    employeeID = dr["employeeID"].ToString(),
                    employeeName = dr["emp_name"].ToString(),
                    title = dr["title"].ToString(),
                    mobile = dr["phone"].ToString(),
                    offFrom=Convert.ToDateTime(dr["offDateFrom"]).ToString("dd-MMM-yyyy"),
                    offto = Convert.ToDateTime(dr["offDateTo"]).ToString("dd-MMM-yyyy"),
                    start = Convert.ToDateTime(dr["startTime"]).ToString("s"),
                    end = Convert.ToDateTime(dr["endTime"]).ToString("s"),
                    description = dr["description"].ToString(),
                    allday = false
                };
            }

            return _employeeOff;
        }

        public static EmployeeOff UpdateEvent(EmployeeOff employeeoff)
        {
            if (employeeoff.offDayID > 0)
            {
                int dsRoster = DataAccess.ExecuteNonQuery(EmployeeOffSQL.UpdateEvent(employeeoff), null);
            }

            return employeeoff;
        }

        public static int DeleteEvent(int _offdayid)
        {
            if (_offdayid > 0)
            {
                int dsRoster = DataAccess.ExecuteNonQuery(EmployeeOffSQL.DeleteEvent(_offdayid), null);
            }
            return _offdayid;
        }

        public static string GetFormatDateTime(DateTime _dateval)
        {
            return _dateval.ToString("yyyy-MM-dd HH:mm:ss");
        }


    }
}