using Newtonsoft.Json;
using SMEPayroll.EmployeeRoster.Model;
using SMEPayroll.EmployeeRoster.Service;
using SMEPayroll.Roster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEPayroll.EmployeeRoster
{
    public partial class EmployeeOffList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string GetEmployeeOffList(EmployeeOffDayListView empoff)
        {
            var resourceList = new List<EmployeeOffDayListView>();
            resourceList = EmployeeOffService.GetEmployeeOffDaysList(empoff);

            if (resourceList.Count == 0)
                resourceList = new List<EmployeeOffDayListView>();
            //else
            //resourceList[0].successMessage = resourceList.successMessage;

            return JsonConvert.SerializeObject(resourceList);
        }

        [System.Web.Services.WebMethod]
        public static string GetEmployeeList()
        {
            int compID = Utility.ToInteger(HttpContext.Current.Session["Compid"]);
            string sSQL = "";
                  
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

    }
}