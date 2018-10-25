using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEPayroll.Appraisal.Model
{
    public partial class MyAppraisalList : System.Web.UI.Page
    {
        public static int compid = 0;

        public static string emp_code;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                emp_code = Utility.ToString(Session["EmpCode"]);
                compid = Utility.ToInteger(Session["Compid"]);
            }
        }

        [System.Web.Services.WebMethod]
        public static string GetAppraisals()
        {

            DataSet data = new DataSet();
            string ssl = "select * from Appraisal where EmpId =" + emp_code + " and EnbleToEmployee = 1";
            data = DataAccess.FetchRS(CommandType.Text, ssl);


            return JsonConvert.SerializeObject(new { datatb = data.Tables[0] });

        }

        [System.Web.Services.WebMethod]
        public static bool EnableReply(int AppId)
        {
            DataSet data = new DataSet();
            string ssl = "select * from Appraisal_History where Fk_AppraisalID =" + AppId + " and AppraisalApproverID ="+ emp_code;
            data = DataAccess.FetchRS(CommandType.Text, ssl);
            if(data.Tables[0].Rows.Count > 0)
            return false;
            else
                return true;
        }
    }
}