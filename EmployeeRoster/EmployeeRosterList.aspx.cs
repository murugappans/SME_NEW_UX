using Newtonsoft.Json;
using SMEPayroll.EmployeeRoster.Service;
using SMEPayroll.Roster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEPayroll.EmployeeRoster
{
    public partial class EmployeeRosterList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
        }

        [System.Web.Services.WebMethod]
        public static string GetEmployeeRosterListByGrouping(employeeroster roster)
        {

            var resourceList = new List<ResourseList>();
            resourceList = EmployeeRosterService.GetEmployeeRosterListByGrouping(roster);

            if (resourceList.Count == 0)
                resourceList = new List<ResourseList>();
            else
                resourceList[0].successMessage = roster.successMessage;

            return JsonConvert.SerializeObject(resourceList);
        }

        [System.Web.Services.WebMethod]
        public static string GetEmployeeRosterListBySeries(Guid _serid,int _empID=0)
        {

            var resourceList = new List<ResourseList>();
            resourceList = EmployeeRosterService.GetEmployeeRosterListBySeries(_serid, _empID);

            //if (resourceList.Count == 0)
            //    resourceList = new List<ResourseList>();
            //else
            //    resourceList[0].successMessage = roster.successMessage;

            return JsonConvert.SerializeObject(resourceList);
        }

    }
}