using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEPayroll.Appraisal
{
    public partial class AppraisalList : System.Web.UI.Page
    {
        public static string emp_code;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                emp_code = Utility.ToString(Session["EmpCode"]);
               // compid = Utility.ToInteger(Session["Compid"]);
            }
        }

        [System.Web.Services.WebMethod]
        public static string GetAppraisals()
        {
            string ssl = "select distinct AppraisalName, ValidFrom, ValidEnd  from Appraisal where ApprisalApprover in (select ID from EmployeeWorkFlowLevel where PayRollGroupID in";
                ssl += "(select PayrollGroupID from EmployeeAssignedToPayrollGroup where Emp_ID = "+emp_code+" and WorkflowTypeID = 5)) and EnbleToEmployee = 0";

            DataSet data = new DataSet();
            data = DataAccess.FetchRS(CommandType.Text, ssl);

            return JsonConvert.SerializeObject(new { datatb = data.Tables[0] });

        }

        [System.Web.Services.WebMethod]
        public static string EnableToUser(string AppName)
        {
            string ssl = "";

            ssl = "UPDATE [dbo].[Appraisal] SET EnbleToEmployee = 1 WHERE AppraisalName='" + AppName + "'";


            int rly = DataAccess.ExecuteStoreProc(ssl);
            if (rly > 0)
                return "true";
            else
                return "false";

        }

    }
}