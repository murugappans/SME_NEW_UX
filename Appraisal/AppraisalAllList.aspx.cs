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
    public partial class AppraisalAllList : System.Web.UI.Page
    {
        public static int compid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                
                compid = Utility.ToInteger(Session["Compid"]);
            }
        }

        [System.Web.Services.WebMethod]
        public static string GetAppraisals()
        {

            //--by murugan

            DataSet data = new DataSet();
            string ssl = " select Ap.*,e.emp_name as EmployeeName, d.Designation from Appraisal Ap inner join employee e on e.emp_code = Ap.EmpId left join designation d on d.id  =e.desig_id   where e.company_id = " + compid + " order by ValidFrom desc";

            data = DataAccess.FetchRS(CommandType.Text, ssl);


            return JsonConvert.SerializeObject(new { datatb = data.Tables[0] });

        }
    }
}