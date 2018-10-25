using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SMEPayroll
{
    public class SMEPayrollLicensing
    {
        public static bool CanAddNewEmployee()
        {
            try
            {
                int iTotalEmployeesInDB = 0, iTotalEmployeesAllowed;
                string sSQL = "";

                string sKey = System.Configuration.ConfigurationManager.AppSettings["SYS_CONFIG"];
                if (sKey == "" || sKey.Length != 96)
                {
                    return false;
                }

                string[] skey = new string[4];
                skey[0] = "0x59185499C345D05F92CED";
                skey[1] = "1FC2CF2BD2C8BCE8D3462EF";
                skey[2] = "0749EF3CDC4096C6EC516D5";
                skey[3] = "10115D05EA097524FB22C22";

                sKey = sKey.ToUpper().ToString();

                for (int i = 0; i <= 3; i++)
                {
                    sKey = sKey.Replace(skey[i].ToUpper().ToString(), "");
                }

                sKey = sKey.Replace("X", "");

                iTotalEmployeesAllowed = Utility.ToInteger(sKey.Replace("X", ""));

                sSQL = "SELECT count(DISTINCT ic_pp_number) FROM employee WHERE company_id <> 1 and termination_date is null";
                System.Data.SqlClient.SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                
                while (dr.Read())
                {
                    iTotalEmployeesInDB = Utility.ToInteger(dr.GetValue(0));
                }

                if (iTotalEmployeesInDB >= iTotalEmployeesAllowed)
                    return false;
                else
                    return true;

            }

            catch (Exception ex)
            {
                string sMsg = ex.Message.ToString();
                return false;
            }

       }
        
    }
    
    
}
