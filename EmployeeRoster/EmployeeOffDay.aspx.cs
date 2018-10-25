using Newtonsoft.Json;
using SMEPayroll.EmployeeRoster.Model;
using SMEPayroll.EmployeeRoster.Service;
using SMEPayroll.Roster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SMEPayroll.EmployeeRoster
{
    public partial class EmployeeOffSerice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
        }

        [System.Web.Services.WebMethod]
        public static string SaveNewRoseter(EmployeeOff _employeeOffDay)
        {
            EmployeeOff dsRoster = EmployeeOffService.AssignEmployeeOff(_employeeOffDay);


            //GetEmployeeOffList

            //DataSet dsProject = new DataSet();
            //var sSQL = "Select emp_code,emp_name From employee where GroupID =12";
            //dsProject = DataAccess.FetchRS(CommandType.Text, sSQL, null);


            var empList = new OffDayView();
            empList.returnMessage = dsRoster.returnMessage;
            if (dsRoster.offDayID >0)
            {
                empList = new OffDayView()
                {
                    id = dsRoster.offDayID.ToString(),
                    employeeID = dsRoster.employeeID.ToString(),
                    employeeName = dsRoster.employeeName,
                    title = dsRoster.employeeName,
                    start = dsRoster.startTime.Value.ToString("s"),
                    end = dsRoster.endTime.Value.ToString("s"),
                    allday = false,
                    returnMessage= dsRoster.returnMessage
                };
            }
            return JsonConvert.SerializeObject(empList);
        }

        [System.Web.Services.WebMethod]
        public static string GetEmployeeOffList()
        {
            List<OffDayView> dsRoster = EmployeeOffService.GetEmployeeOffList();
            return JsonConvert.SerializeObject(dsRoster);
        }


        [System.Web.Services.WebMethod]
        public static string GetEmployeeOffListByEmployeeID(int _empid)
        {
            List<OffDayView> dsRoster = EmployeeOffService.GetEmployeeOffListByEmployeeID(_empid);
            return JsonConvert.SerializeObject(dsRoster);
        }


        [System.Web.Services.WebMethod]
        public static string GetEmployees()
        {
            string sSQL = "";
            DataSet dsProject = new DataSet();
            sSQL = "Select emp_code,emp_name From employee";
            dsProject = DataAccess.FetchRS(CommandType.Text, sSQL, null);

            var empList = new List<DrowpdownList>();
            empList = dsProject.Tables[0].AsEnumerable().Select(dataRow => new DrowpdownList
            {
                mainID = dataRow.Field<int>("emp_code"),
                //Description = dataRow.Field<string>("Project_ID"),
                Description = dataRow.Field<string>("emp_name")
            }).ToList();

            return JsonConvert.SerializeObject(empList);

        }


        [System.Web.Services.WebMethod]
        public static string DragRoster(EmployeeOff _employeeOffDay)
        {
            var _emproster = new EmployeeOff();
            _emproster = EmployeeOffService.DragRoster(_employeeOffDay);
            List<OffDayView> dsRoster = EmployeeOffService.GetEmployeeOffListByEmployeeID(_employeeOffDay.employeeID.Value);


           // List<OffDayView> dsRoster = EmployeeOffService.GetEmployeeOffList();
            dsRoster[0].returnMessage = _emproster.returnMessage;
            return JsonConvert.SerializeObject(dsRoster);
        }


        [System.Web.Services.WebMethod]
        public static string GetEventByID(int _id)
        {
            var _employeeroster = EmployeeOffService.GetEventByEventID(_id);
            return JsonConvert.SerializeObject(_employeeroster);
        }


        [System.Web.Services.WebMethod]
        public static string UpdateEvent(EmployeeOff _employeeOffDay)
        {
            var obj = new EmployeeOff();
            obj = EmployeeOffService.UpdateEvent(_employeeOffDay);

            List<OffDayView> dsRoster = EmployeeOffService.GetEmployeeOffListByEmployeeID(_employeeOffDay.employeeID.Value);
            return JsonConvert.SerializeObject(dsRoster);
        }


        [System.Web.Services.WebMethod]
        public static string DeleteEvent(EmployeeOff _employeeOffDay)
        {
            var obj = EmployeeOffService.DeleteEvent(_employeeOffDay.offDayID);
            List<OffDayView> dsRoster = EmployeeOffService.GetEmployeeOffListByEmployeeID(_employeeOffDay.employeeID.Value);
            return JsonConvert.SerializeObject(dsRoster);
        }


    }
}