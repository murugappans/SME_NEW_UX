using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using efdata;
namespace SMEPayroll.Appraisal
{
    public partial class ManagerAppraisalList : System.Web.UI.Page
    {
        public static string txt = "";
        public static int compid = 0;

        public static string emp_code;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
               
                emp_code = Utility.ToString(Session["EmpCode"]);
                compid = Utility.ToInteger(Session["Compid"]);
            }

            if (Utility.AllowedAction1(Session["Username"].ToString(), "Approve and Reject Leaves for all") == true)
            {
                txt = "approver";
            }
            else
            {
                txt = "noapprover";
            }
        }
        [System.Web.Services.WebMethod]
        public static string GetAppraisals()
        {
           
            //--by murugan

            DataSet data = new DataSet();        
            SqlParameter[] param1 = new SqlParameter[3];

            param1[0] = new SqlParameter("@txt", txt);
            param1[1] = new SqlParameter("@company_id", compid);
            param1[2] = new SqlParameter("@UserID", emp_code);
            data = DataAccess.FetchRS(CommandType.StoredProcedure, "sp_pending_appraisal", param1);
           

            return JsonConvert.SerializeObject(new { datatb = data.Tables[0] });

        }
    }
}