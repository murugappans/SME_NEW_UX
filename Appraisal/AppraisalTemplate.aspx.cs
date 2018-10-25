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
    public partial class WebForm3 : System.Web.UI.Page
    {
        int creatdby = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
             creatdby = Convert.ToInt32(Session["EmpCode"]);
        }

        [System.Web.Services.WebMethod]
        public static string GetTemplates()
        {
            string ssl = "select * from AppraisalTemplate";
            DataSet data = new DataSet();
            data = DataAccess.FetchRS(CommandType.Text, ssl);

            return JsonConvert.SerializeObject(new { datatb = data.Tables[0] });

        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static int SaveTemplate(int? TempId, string TempName, int Active)
        {
            TempName = TempName.Replace("'", "''");
            string ssl = "Select Id from AppraisalTemplate where Name = '"+TempName+"'";
            int rply = DataAccess.ExecuteScalar(ssl);
            if (TempId.HasValue)
            {
                ssl = "Update AppraisalTemplate set Name='" + TempName;
                ssl += "', Active =" + Active;
                ssl += " where Id=" + TempId;
                if (rply > 0)
                {
                    if (rply == TempId)
                    {
                       
                        DataAccess.ExecuteStoreProc(ssl);
                        return TempId.Value;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                   
                    DataAccess.ExecuteStoreProc(ssl);
                    return TempId.Value;
                }
            }
            else
            {
                if(rply > 0)
                {
                    return 0;
                }
                ssl = "INSERT INTO AppraisalTemplate (Name,Active,CreatedBy,Cretaeddate)";
                int creatdby = Convert.ToInt32(HttpContext.Current.Session["EmpCode"]);
                string dtToday = String.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now);
                ssl += "Output Inserted.Id VALUES('" + TempName + "'," + Active + "," + creatdby + ",'" + dtToday + "')";
                int i = DataAccess.ExecuteScalar(ssl);
                return i;


            }

        }
        [System.Web.Services.WebMethod]
        public static string DeleteTemplate(int Id)
        {
            
            try
            {
                string sSQL = "select * from Appraisal where AppraisalTemplateID =  " + Id;
                DataSet dt = DataAccess.FetchRS(CommandType.Text, sSQL);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    throw new Exception("");
                }
                sSQL = "DELETE FROM AppraisalTemplate where Id = {0}";
                sSQL += " DELETE FROM Appraisal_Template_Objective_Mapping where Template_Id = {0}";
                sSQL = string.Format(sSQL, Utility.ToInteger(Id));
                int i = DataAccess.ExecuteStoreProc(sSQL);
                return "true";
            }
            catch (Exception)
            {

                return "false";
            }


        }

        [System.Web.Services.WebMethod]
        public static string GetObjectives(int TempId)
        {
            string ssl = "select t2.Objective_Id from AppraisalObjectiveTemplate aot ";
            ssl += "LEFT JOIN Appraisal_Template_Objective_Mapping t2 ON t2.Objective_Id = aot.Id where t2.Template_Id =" + TempId;

            ssl += " select Id, ObjectiveName, CategoryID from AppraisalObjectiveTemplate";

            ssl += " Select distinct ac.* from AppraisalCategory as ac inner join AppraisalObjectiveTemplate ao on ac.Id = ao.CategoryID";


            DataSet datatb = new DataSet();
            datatb = DataAccess.FetchRS(CommandType.Text, ssl);
            return JsonConvert.SerializeObject(new { dataObjectivesInTemplte = datatb.Tables[0], dataAllObjectives = datatb.Tables[1], dataCategories = datatb.Tables[2] });

        }

        [System.Web.Services.WebMethod]
        public static int SaveObjectives(int[] ArrID, int TempId)
        {
            string ssl = ""; DataSet data = new DataSet();
            ssl = "Delete from  Appraisal_Template_Objective_Mapping where Objective_Id in (select aot.Id from AppraisalObjectiveTemplate aot LEFT JOIN Appraisal_Template_Objective_Mapping t2 ON t2.Objective_Id = aot.Id where t2.Template_Id = " + TempId + ") and Template_Id =" + TempId;
            foreach (int id in ArrID)
            {
                ssl += "INSERT INTO Appraisal_Template_Objective_Mapping (Objective_Id,Template_Id)";

                ssl += " VALUES('" + id + "'," + TempId + ")";


            }
            int i = DataAccess.ExecuteStoreProc(ssl);
            

            return 1;


        }


    }
}