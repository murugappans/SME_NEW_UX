using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMEPayroll.Appraisal.Model;

namespace SMEPayroll.Appraisal
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string GetObjectives()
        {
            string ssl = "select ac.CategoryName,aot.* from AppraisalObjectiveTemplate aot left join AppraisalCategory ac on ac.Id = aot.CategoryID";
            DataSet data = new DataSet();
            data = DataAccess.FetchRS(CommandType.Text, ssl);

            return JsonConvert.SerializeObject(new { datatb = data.Tables[0] });
         
        }
        [System.Web.Services.WebMethod]
        public static string GetCategories()
        {
            string ssl = "select * from AppraisalCategory";
            DataSet data = new DataSet();
            data = DataAccess.FetchRS(CommandType.Text, ssl);

            return JsonConvert.SerializeObject(new { datatb = data.Tables[0] });
           
        }

        [System.Web.Services.WebMethod]
        public static string DeleteObjective(int Id)
        {
            string sSQL = "Select * from Appraisal_Template_Objective_Mapping where Objective_Id=" + Id;
            DataSet dt = new DataSet();
            try
            {
                dt = DataAccess.FetchRS(CommandType.Text,sSQL);
                if(dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
                {
                    return "false";
                   
                }
                else
                {
                    sSQL = "DELETE FROM AppraisalObjectiveTemplate where Id = {0}";
                    sSQL = string.Format(sSQL, Utility.ToInteger(Id));
                    int i = DataAccess.ExecuteStoreProc(sSQL);
                    return "true";
                }
            }
            catch (Exception)
            {
                return "false";
            }

        }

        [System.Web.Services.WebMethod]
        public static int SaveObjective(EmployeeObjective objObjective)
        {
            string ssl = "Select * from AppraisalObjectiveTemplate where ObjectiveName='" + objObjective.Title + "' and ObjectiveType='" + objObjective.RatingType + "' and CategoryID=" + objObjective.CategoryId + "";
            DataSet dt = DataAccess.FetchRS(CommandType.Text, ssl);
            

            if (objObjective.Id == 0)
            {
                if (dt.Tables[0].Rows.Count > 0)
                {
                    return 0;
                }
                else
                {
                    ssl = "INSERT INTO AppraisalObjectiveTemplate (ObjectiveName,ObjectiveType,CategoryID)";
                    objObjective.Title = objObjective.Title.Replace("'", "''");
                    ssl += "Output Inserted.Id VALUES('" + objObjective.Title + "','" + objObjective.RatingType + "'," + objObjective.CategoryId + ")";
                    int i = DataAccess.ExecuteScalar(ssl);
                    return i;
                }

               
            }
            else
            {
                if (dt.Tables[0].Rows.Count > 0)
                {
                    int id = 0,flag =0;
                    for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                    {
                        id = Convert.ToInt32(dt.Tables[0].Rows[i]["Id"]);
                        if (id != objObjective.Id)
                        {
                            flag++;
                        }

                    }
                    if(flag > 0)
                    return 0;
                }
                ssl = "Update AppraisalObjectiveTemplate set ObjectiveType='" + objObjective.RatingType + "',CategoryID=" + objObjective.CategoryId;
                objObjective.Title = objObjective.Title.Replace("'", "''");
                ssl += ",ObjectiveName='" + objObjective.Title + "' where Id=" + objObjective.Id;
                DataAccess.ExecuteStoreProc(ssl);
                return objObjective.Id;

            }

        }
    }
}