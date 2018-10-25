using Newtonsoft.Json;

using System;
using System.Data;


namespace SMEPayroll.Appraisal
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

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
        public static int SaveCategory(int? CatId, string catName)
        {
            catName = catName.Replace("'", "''");
            string ssl = "Select Id from AppraisalCategory where CategoryName = '" + catName + "'";
            int rply = DataAccess.ExecuteScalar(ssl);

            if (CatId.HasValue)
            {
                ssl = "Update AppraisalCategory set CategoryName='" + catName;
                ssl += "' where Id=" + CatId;
                if (rply > 0)
                {
                    if (rply == CatId)
                    {

                        DataAccess.ExecuteStoreProc(ssl);
                        return CatId.Value;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {

                    DataAccess.ExecuteStoreProc(ssl);
                    return CatId.Value;
                }

               
            }
            else
            {
                if (rply > 0)
                {
                    return 0;
                }
                ssl = "INSERT INTO AppraisalCategory (CategoryName)";

                ssl += "Output Inserted.Id VALUES('" + catName + "')";
                int i = DataAccess.ExecuteScalar(ssl);
                return i;


            }

        }

        [System.Web.Services.WebMethod]
        public static string DeleteCategory(int Id)
        {

            string sSQL = "DELETE FROM AppraisalCategory where Id = {0}";
            sSQL = string.Format(sSQL, Utility.ToInteger(Id));
            try
            {
                int i = DataAccess.ExecuteStoreProc(sSQL);
                return "true";
            }
            catch (Exception)
            {

                return "false";
            }


        }

        [System.Web.Services.WebMethod]
        public static string GetObjectives(int CatId)
        {
            string ssl = "select ac.CategoryName,aot.* from AppraisalObjectiveTemplate aot left join AppraisalCategory ac on ac.Id = aot.CategoryID where CategoryID <>" + CatId;
            DataSet data = new DataSet();
            data = DataAccess.FetchRS(CommandType.Text, ssl);
           
            return JsonConvert.SerializeObject(new { datatb = data.Tables[0] });

        }


        [System.Web.Services.WebMethod]
        public static int SaveObjectives(int[] ArrID, int CatId)
        {
            string objname, ssl = ""; DataSet data = new DataSet();
           
            foreach (int id in ArrID)
            {
                ssl = "select * from AppraisalObjectiveTemplate where Id=" + id;
                data = DataAccess.FetchRS(CommandType.Text, ssl);
                if (data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0)
                {
                    ssl = "INSERT INTO AppraisalObjectiveTemplate (ObjectiveName,ObjectiveType,CategoryID)";
                    objname = data.Tables[0].Rows[0]["ObjectiveName"].ToString().Replace("'", "''");
                    ssl += "VALUES('" + objname + "','" + data.Tables[0].Rows[0]["ObjectiveType"] + "'," + CatId + ")";
                    int i = DataAccess.ExecuteStoreProc(ssl);
                   
                }
            }
           
            
           
                return 1;
            
            
        }
    }
}