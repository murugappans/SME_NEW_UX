using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMEPayroll.Main.Model;
using SMEPayroll.Main.Model.DataSQL;
using System.Data.SqlClient;
using SMEPayroll.Roster;
using System.Data;

namespace SMEPayroll.Main.Service
{
    public class EmployeeDataService
    {

        public static List<EmployeeData> GetAllEventList()
        {
            DataSet dsdata = new DataSet();
            dsdata = DataAccess.FetchRS(CommandType.Text, EmployeeDataSQL.GetAllEventList(), null);
            var _sno = 0;
            var _List = new List<EmployeeData>();
            _List = dsdata.Tables[0].AsEnumerable().Select(dataRow => new EmployeeData
            {
                id = (_sno+=1).ToString(),//dataRow.Field<int>("emp_code").ToString(),
                emp_name = dataRow.Field<string>("title"),
                title = dataRow.Field<string>("title"),
                start = getEventStartEnd(dataRow),
                end= getEventStartEnd(dataRow),
                eventType = dataRow.Field<string>("_type"),
                className = getEventClass(dataRow),
                description = "all events",
            }).ToList();

            return _List;

        }
        private static string getEventStartEnd(DataRow dataRow)
        {
            var _type = dataRow.Field<string>("_type");
            if (_type == "Birthday")
                return dataRow.Field<DateTime>("start").ToString(2018 + "-MM-dd");
            else if (_type == "PassportExpiry")
                return dataRow.Field<DateTime>("start").ToString("yyyy-MM-dd");
            else if (_type == "Publicholiday")
                return dataRow.Field<DateTime>("start").ToString("yyyy-MM-dd");
            else if (_type == "ProbationPeriod")
            {
                int _probperiod = dataRow.Field<int>("other");
                DateTime _joiningdate = dataRow.Field<DateTime>("start");
                DateTime _probenddate = _joiningdate.AddMonths(_probperiod);
                return _probenddate.ToString("yyyy-MM-dd");
                //return dataRow.Field<DateTime>("passport_expiry").ToString("dd-MMM-yyyy");
            }

            return _type;
        }

        private static string getEventClass(DataRow dataRow)
        {
            var _type = dataRow.Field<string>("_type");
            if (_type == "Birthday")
                return "m-fc-event--danger m-fc-event--solid-focus";
            else if (_type == "PassportExpiry")
                return "m-fc-event--light  m-fc-event--solid-danger";
            else if (_type == "Publicholiday")
                return "m-fc-event--info m-fc-event--solid-accent";
            else if (_type == "ProbationPeriod")
                return "m-fc-event--light m-fc-event--solid-warning Publicholiday";
            return "m-fc-event--light  m-fc-event--solid-danger";
        }

        private static string getEventDescription(DataRow dataRow)
        {
            var _type = dataRow.Field<string>("_type");
            if (_type == "Birthday")
                return "m-fc-event--danger m-fc-event--solid-focus";
            else if (_type == "PassportExpiry")
                return "m-fc-event--light  m-fc-event--solid-danger";
            else if (_type == "Publicholiday")
                return "m-fc-event--light m-fc-event--solid-warning";
            else if (_type == "ProbationPeriod")
                return "m-fc-event--light  m-fc-event--solid-danger";
            return "m-fc-event--light  m-fc-event--solid-danger";
        }

        public static List<EmployeeData> GetBirthdayList()
        {
            DataSet dsdata = new DataSet();
            dsdata = DataAccess.FetchRS(CommandType.Text, EmployeeDataSQL.GetBirthdayList(), null);
            var _List = new List<EmployeeData>();
            _List = dsdata.Tables[0].AsEnumerable().Select(dataRow => new EmployeeData
            {
                id = dataRow.Field<int>("emp_code").ToString(),
                emp_name = dataRow.Field<string>("emp_name"),
                title = dataRow.Field<string>("emp_name"),
                date_of_birth = dataRow.Field<DateTime>("date_of_birth").ToString(2018 + "-MM-dd") ,
                //passport_expiry = dataRow.Field<DateTime>("passport_expiry").ToString("dd-MMM-yyyy"),
                joining_date = dataRow.Field<DateTime>("joining_date").ToString(2018 + "-MM-dd"),
                start = dataRow.Field<DateTime>("date_of_birth").ToString(2018 + "-MM-dd"),
                end = dataRow.Field<DateTime>("date_of_birth").ToString(2018 + "-MM-dd"),
                className = "m-fc-event--danger m-fc-event--solid-focus Birthday",
                description = dataRow.Field<string>("emp_name") +" birthday is on " + dataRow.Field<DateTime>("date_of_birth").ToString("dd-MMM"),
                eventType = "Birthday"
            }).ToList();

            return _List;

        }


        public static List<EmployeeData> GetPassportExpiryList()
        {
            DataSet dsdata = new DataSet();
            dsdata = DataAccess.FetchRS(CommandType.Text, EmployeeDataSQL.GetPassportExpiryList(), null);
            var _List = new List<EmployeeData>();
            _List = dsdata.Tables[0].AsEnumerable().Select(dataRow => new EmployeeData
            {
                id = dataRow.Field<int>("emp_code").ToString(),
                emp_name = dataRow.Field<string>("emp_name"),
                title = dataRow.Field<string>("emp_name"),
                //joining_date = dataRow.Field<DateTime>("joining_date").ToString("dd-MMM-yyyy"),
                start = dataRow.Field<DateTime>("ExpiryDate").ToString("yyyy-MM-dd"),
                end = dataRow.Field<DateTime>("ExpiryDate").ToString("yyyy-MM-dd"),
                className = "m-fc-event--light  m-fc-event--solid-danger PassportExpiry",
                description = dataRow.Field<string>("emp_name") + " Passport expires on this date",
                eventType = "PassportExpiry"
            }).ToList();

            return _List;

        }

        public static List<EmployeeData> GetAllDocsExpiryList()
        {
            DataSet dsdata = new DataSet();
            dsdata = DataAccess.FetchRS(CommandType.Text, EmployeeDataSQL.GetAllDocsExpiryList(), null);
            var _List = new List<EmployeeData>();
            _List = dsdata.Tables[0].AsEnumerable().Select(dataRow => new EmployeeData
            {
                id = dataRow.Field<int>("emp_code").ToString(),
                emp_name = dataRow.Field<string>("emp_name"),
                title = dataRow.Field<string>("emp_name"),
                //joining_date = dataRow.Field<DateTime>("joining_date").ToString("dd-MMM-yyyy"),
                start = dataRow.Field<DateTime>("ExpiryDate").ToString("yyyy-MM-dd"),
                end = dataRow.Field<DateTime>("ExpiryDate").ToString("yyyy-MM-dd"),
                className = "m-fc-event--light  m-fc-event--solid-danger",
                description = dataRow.Field<string>("emp_name") + " Passport expires on this date",
                eventType = "PassportExpiry"
            }).ToList();

            return _List;

        }



        public static List<EmployeeData> GetPHList()
        {
            DataSet dsdata = new DataSet();
            dsdata = DataAccess.FetchRS(CommandType.Text, EmployeeDataSQL.GetPHList(), null);
            var _List = new List<EmployeeData>();
            _List = dsdata.Tables[0].AsEnumerable().Select(dataRow => new EmployeeData
            {
                id = dataRow.Field<int>("ID").ToString(),
                title = dataRow.Field<string>("holiday_name"),
                start = dataRow.Field<DateTime>("holiday_date").ToString("yyyy-MM-dd"),
                end = dataRow.Field<DateTime>("holiday_date").ToString("yyyy-MM-dd"),
                className= "m-fc-event--info m-fc-event--solid-accent",
                description= dataRow.Field<string>("holiday_name"),
                eventType ="Publicholiday"
            }).ToList();

            return _List;

        }


        public static List<EmployeeData> GetProbationPeriodExpiryList()
        {
            DataSet dsdata = new DataSet();
            dsdata = DataAccess.FetchRS(CommandType.Text, EmployeeDataSQL.GetProbationPeriodExpiryList(), null);
            var _List = new List<EmployeeData>();
            _List = dsdata.Tables[0].AsEnumerable().Select(dataRow => new EmployeeData
            {
                id = dataRow.Field<int>("emp_code").ToString(),
                emp_name = dataRow.Field<string>("emp_name"),
                title = dataRow.Field<string>("emp_name"),
                joining_date = dataRow.Field<DateTime>("joining_date").ToString("dd-MMM-yyyy"),
                start = getProbationPeriod(dataRow),
                end = getProbationPeriod(dataRow),
                className = "m-fc-event--light m-fc-event--solid-warning Publicholiday",
                description = dataRow.Field<string>("emp_name") +" Probation period ends on this date",
                eventType = "ProbationPeriod"
            }).ToList();

            return _List;

        }
        private static string getProbationPeriod(DataRow dataRow)
        {
            int _probperiod = dataRow.Field<int>("probation_period");
            DateTime _joiningdate = dataRow.Field<DateTime>("joining_date");
            DateTime _probenddate = _joiningdate.AddMonths(_probperiod);
            return _probenddate.ToString("dd-MMM-yyyy");
        }

    }
}