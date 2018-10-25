using Newtonsoft.Json;
using SMEPayroll.Appraisal.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEPayroll.Appraisal
{

    //$("#star-10-9").prop('checked')
    public partial class EmployeeAppraisal : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {


        }


        [System.Web.Services.WebMethod]
        public static string GetAppraisalForm(int AppraisalId)
        {
            int Empcode = Convert.ToInt32(HttpContext.Current.Session["EmpCode"]);
           //string ssl = "SELECT Id FROM Appraisal where EmpId = " + Empcode + "  and AppraisalYear='" + DateTime.Now.Year.ToString() + "' and EnbleToEmployee=1 and WFLevel='Level0' ORDER BY ID DESC";
           // int AppraisalId = DataAccess.ExecuteScalar(ssl);
             string ssl = "Select AppraisalName,Id from Appraisal where Id=" + AppraisalId
                + "  Select aot.* from Appraisal_Template_Objective_Mapping atom inner join AppraisalObjectiveTemplate aot on atom.Objective_Id = aot.Id where Template_Id = (Select AppraisalTemplateID from Appraisal where Id = "+ AppraisalId+")"  ;
            DataSet dt = DataAccess.FetchRS(CommandType.Text,ssl);
            if (dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(new { dtAppraisal = dt.Tables[0], dtAppraisalObjectives = dt.Tables[1] });
            }
            return "false";

        }

        [System.Web.Services.WebMethod]
        public static int FeedbackSend(AppraisalHistory[] EmployAppraisalRply, int AppraisalId)
        {
            string ssl = "";
            int Empcode = Convert.ToInt32(HttpContext.Current.Session["EmpCode"]);

            string dtToday = string.Format("{0:yyyy-MM-dd hh:mm}", DateTime.Now);
            foreach (AppraisalHistory item in EmployAppraisalRply)
            {
               
                item.Remark = item.Remark.Replace("'", "''");
                ssl += "INSERT INTO Appraisal_History(Fk_AppraisalID,Fk_ObjectiveID, AppraisalApproverID,Commands,CommandsDate,Remark)";
                ssl += "VALUES(" + AppraisalId + "," + item.ObjectiveId + "," + Empcode + ",'" + item.Comment + "','" + dtToday + "','" + item.Remark + "')";
               
            }

            int rly = DataAccess.ExecuteStoreProc(ssl);
            if (rly > 0)
            {
                ssl = "UPDATE [dbo].[Appraisal] SET  [WFLevel] = 'level2' WHERE Id="+ AppraisalId; 
                DataAccess.ExecuteStoreProc(ssl);
                
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}